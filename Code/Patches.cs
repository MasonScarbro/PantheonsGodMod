using ai.behaviours;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;
using static Mono.Security.X509.X520;
//Harmony Patches
namespace GodsAndPantheons
{
    [HarmonyPatch(typeof(ActorBase), "nextJobActor")]
    public class summonedonejobapply
    {
        static void Postfix(ref string __result, ActorBase pActor)
        {
            if(pActor.hasTrait("Summoned One"))
            {
                __result = "SummonedJob";
            }
        }
    }
    [HarmonyPatch(typeof(KingdomBehCheckKing), "findKing")]
    public class findking
    {
        static Actor GetKing(Kingdom pKingdom)
        {
            List<Actor> list = new List<Actor>();
            Debug.Log("what");
            foreach(Actor a in pKingdom.units)
            {
                if (Traits.IsGod(a))
                {
                    list.Add(a);
                }
            }
            if(list.Count > 0)
            {
                list.Sort(new Comparison<Actor>(ListSorters.sortUnitByAge));
                return list[0];
            }
            return null;
        }
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var found = false;
            foreach (var instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Ldnull && !found)
                {
                    yield return new CodeInstruction(OpCodes.Ldarg_1);
                    yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(findking), "GetKing"));
                    found = true;
                }
                else
                {
                    yield return instruction;
                }
            }
        }
    }
    [HarmonyPatch(typeof(TooltipLibrary), "showTrait")]
    public class showdemistats
    {
        static void Prefix(ref TooltipData pData)
        {
            if(Config.selectedUnit == null)
            {
                return;
            }
            if(pData.trait.id == "Demi God" || pData.trait.id == "Lesser God")
            {
                pData.trait.base_stats = Traits.GetDemiStats(Config.selectedUnit.data);
            }
        }
    }
    [HarmonyPatch(typeof(BaseSimObject), "canAttackTarget")]
    public class DontAttack
    {
        static bool Prefix(BaseSimObject __instance, BaseSimObject pTarget)
        {
            if (Main.savedSettings.HunterAssasins)
            {
                if (pTarget.hasStatus("Invisible") || __instance.hasStatus("Invisible"))
                {
                    return false;
                }
                if (__instance.isActor() && __instance.a.hasTrait("God Hunter") && __instance.a.data.health < __instance.getMaxHealth() * (__instance.hasStatus("powerup") ? 0.5 : 0.25))
                {
                    return false;
                }
            }
            return true;
        }
    }
    [HarmonyPatch(typeof(ActorBase), "clearAttackTarget")]
    public class KEEPATTACKING
    {
        static bool Prefix(ActorBase __instance)
        {
            if (__instance.hasTrait("God Hunter") && Main.savedSettings.HunterAssasins)
            {
                BaseSimObject? a = __instance.attackTarget;
                if (a != null)
                {
                    if (a.isActor())
                    {
                        if (Traits.IsGod(a.a) && a.isAlive() && !__instance.hasStatus("Invisible") && __instance.data.health >= __instance.getMaxHealth() * (__instance.hasStatus("powerup") ? 0.5 : 0.25)) { return false; }
                    }
                }
            }
            return true;
        }
    }
    [HarmonyPatch(typeof(Actor), "newKillAction")]
    public class isgodkiller
    {
        static void Prefix(Actor __instance, Actor pDeadUnit)
        {
            bool isgod = Traits.IsGod(pDeadUnit);
            if (isgod)
            {
                __instance.addTrait("God Killer");
                __instance.data.get("godskilled", out int godskilled);
                __instance.data.set("godskilled", godskilled + (isgod ? 1 : 0));
            }
            if(__instance.hasTrait("God Hunter"))
            {
                Traits.SuperRegeneration(__instance, 100, isgod ? 30 : 5);
                if (isgod)
                {
                    __instance.addStatusEffect("powerup", 10);
                }
            }
        }
    }
    [HarmonyPatch(typeof(StatusEffectData), "update")]
    public class finishinvisibility
    {
        static void Postfix(StatusEffectData __instance)
        {
            if (__instance.finished && __instance.asset.id == "Invisible")
            {
                __instance._simObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
        }
    }
    [HarmonyPatch(typeof(BaseSimObject), "finishStatusEffect")]
    public class finishinvisibility2
    {
        static void Postfix(string pID, BaseSimObject __instance)
        {
            if(__instance.activeStatus_dict == null) { return; }
            if (__instance.activeStatus_dict.ContainsKey(pID))
            {
                if(pID == "Invisible")
                {
                    __instance.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                }
            }
        }
    }
    [HarmonyPatch(typeof(CityBehProduceUnit), "checkGreatClan")]
    public class InheritGodTraits
    {
        static void Postfix(Actor pParent1, Actor pParent2, Clan __result)
        {
            if (Child != null)
            {
                int parents = pParent2 != null ? 2 : 1;
                int godparents = Traits.IsGod(pParent1) ? 1 : 0;
                int demiparents = pParent1.data.hasTrait("Demi God") ? 1 : 0;
                int lesserparents = pParent1.hasTrait("Lesser God") ? 1 : 0;
                List<string> godtraits = new List<string>(Traits.GetGodTraits(pParent1));
                AddRange(godtraits, Traits.Getinheritedgodtraits(pParent1.data));
                if (parents == 2)
                {
                    AddRange(godtraits, Traits.GetGodTraits(pParent2));
                    AddRange(godtraits, Traits.Getinheritedgodtraits(pParent2.data));
                    godparents += Traits.IsGod(pParent2) ? 1 : 0;
                    demiparents += pParent2.data.hasTrait("Demi God") ? 1 : 0;
                    lesserparents += pParent2.hasTrait("Lesser God") ? 1 : 0;
                }
                float chancemult = .75f;
                chancemult += godparents / 2;
                chancemult += lesserparents / 4;
                chancemult += demiparents / 8;
                int importantgenes = godparents + lesserparents;
                Actor? chief = __result?.getChief();
                if (chief != null)
                {
                    if (Traits.IsGod(chief))
                    {
                        Traits.MakeLesserGod(godtraits, ref Child, chancemult);
                        return;
                    }
                }
                if (parents == importantgenes)
                {
                   Traits.MakeLesserGod(godtraits, ref Child, chancemult);
                    return;
                }
                else if(importantgenes == 1 || demiparents+importantgenes == parents)
                {
                  Traits.MakeDemiGod(godtraits, ref Child, chancemult);
                  return;
                }
                Traits.AutoTrait(Child, godtraits, true, chancemult);
                Child = null;
            }
        }
        public static ActorData Child;
        static void AddRange(List<string> list, List<string> range)
        {
            foreach (string s in range)
            {
                if (!list.Contains(s))
                {
                    list.Add(s);
                }
            }
        }
    }
    [HarmonyPatch(typeof(ActorData), "inheritTraits")]
    public class ChildData
    {
        static void Postfix(ActorData __instance, List<string> pTraits)
        {
            if (InheritGodTraits.Child == null && (Traits.GetGodTraits(pTraits, true, true).Count > 0))
            {
                InheritGodTraits.Child = __instance;
            }
        }
    }
    [HarmonyPatch(typeof(ActorBase), "calculateFertility")]
    public class UseDemiStats
    {
        static void Postfix(ActorBase __instance)
        {
            if (__instance.hasTrait("Demi God") || __instance.hasTrait("Lesser God"))
            {
                mergeStats(Traits.GetDemiStats(__instance.data).stats_list, ref __instance.stats);
            }
        }
        static void mergeStats(ListPool<BaseStatsContainer> pStats, ref BaseStats __instance)
        {
            for (int i = 0; i < pStats.Count; i++)
            {
                __instance[pStats[i].id] += pStats[i].value;
            }
        }
    }
    [HarmonyPatch(typeof(MapBox), "applyAttack")]
    public class KnowledgeGodEnemySwap
    {
        static void Prefix(AttackData pData, ref BaseSimObject pTargetToCheck)
        {
            if (pTargetToCheck.isActor() && pData.initiator != null)
            {
                if (pTargetToCheck.a.hasTrait("God Of Knowledge"))
                {
                    if (Toolbox.randomChance(Traits.GetEnhancedChance("God Of Knowledge", "EnemySwap%")))
                    {
                        WorldTile tile = pTargetToCheck.currentTile;
                        ListPool<BaseSimObject> enemies = EnemiesFinder.findEnemiesFrom(tile, pTargetToCheck.kingdom, -1).list;
                        Actor enemytoswap = null;
                        foreach (BaseSimObject enemy in enemies)
                        {
                            if (enemy.isActor() && enemy != pData.initiator && enemy != pTargetToCheck)
                            {
                                enemytoswap = enemy.a;
                                break;
                            }
                        }
                        if (enemytoswap != null)
                        {
                            enemytoswap.cancelAllBeh();
                            EffectsLibrary.spawnAt("fx_teleport_blue", tile.posV3, enemytoswap.stats[S.scale]);
                            EffectsLibrary.spawnAt("fx_teleport_blue", enemytoswap.currentTile.posV3, pTargetToCheck.stats[S.scale]);
                            pTargetToCheck.a.spawnOn(enemytoswap.currentTile, 3f);
                            enemytoswap.spawnOn(tile, 3f);
                            pTargetToCheck = enemytoswap;
                        }
                    }
                }
            }
        }
    }
}
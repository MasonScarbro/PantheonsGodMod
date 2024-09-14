using ai.behaviours;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;
using static GodsAndPantheons.Traits;
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
            foreach(Actor a in pKingdom.units)
            {
                if (IsGod(a))
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
                    yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(findking), nameof(GetKing)));
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
        static BaseStats getdemistats(ActorTrait trait)
        {
            if(trait.id == "Demi God" || trait.id == "Lesser God")
            {
                return GetDemiStats(Config.selectedUnit.data);
            }
            return trait.base_stats;
        }
        
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction code in instructions)
            {
                if (code.opcode == OpCodes.Ldfld && code.operand is FieldInfo && ((FieldInfo)code.operand).Name == "base_stats")
                {
                    yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(showdemistats), nameof(getdemistats)));
                }
                else
                {
                    yield return code;
                }
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
                        if (IsGod(a.a) && a.isAlive() && !__instance.hasStatus("Invisible") && __instance.data.health >= __instance.getMaxHealth() * (__instance.hasStatus("powerup") ? 0.5 : 0.25)) { return false; }
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
            bool isgod = IsGod(pDeadUnit);
            if (isgod)
            {
                __instance.addTrait("God Killer");
                __instance.data.get("godskilled", out int godskilled);
                __instance.data.set("godskilled", godskilled + (isgod ? 1 : 0));
            }
            if(__instance.hasTrait("God Hunter"))
            {
                SuperRegeneration(__instance, 100, isgod ? 30 : 5);
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
    [HarmonyPatch(typeof(BehMakeBaby), "makeBaby")]
    public class InheritGodTraitsFromNonCitizens
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction code in instructions)
            {
                yield return code;
                if (code.opcode == OpCodes.Stloc_2)
                {
                    yield return new CodeInstruction(OpCodes.Ldloc_2);
                    yield return new CodeInstruction(OpCodes.Ldarg_1);
                    yield return new CodeInstruction(OpCodes.Ldarg_2);
                    yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(InheritGodTraitsFromNonCitizens), nameof(MakeBaby)));
                }
            }
        }
        public static void MakeBaby(Actor child, Actor pParent1, Actor pParent2)
        {
            InheritGodTraits.inheritgodtraits(child.data, pParent1, pParent2, null);
        }
    }
    [HarmonyPatch(typeof(CityBehProduceUnit), "produceNewCitizen")]
    public class InheritGodTraits
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction code in instructions)
            {
                yield return code;
                if (code.opcode == OpCodes.Stloc_S && code.operand is LocalBuilder builder && builder.LocalIndex == 5)
                {
                    yield return new CodeInstruction(OpCodes.Ldloc_S, 4);
                    yield return new CodeInstruction(OpCodes.Ldloc_0);
                    yield return new CodeInstruction(OpCodes.Ldloc_1);
                    yield return new CodeInstruction(OpCodes.Ldloc_S, 5);
                    yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(InheritGodTraits), nameof(inheritgodtraits)));
                }
            }
        }
        public static void inheritgodtraits(ActorData child, Actor pParent1, Actor pParent2, Clan GreatClan)
        {
            int parents = pParent2 != null ? 2 : 1;
            int godparents = IsGod(pParent1) ? 1 : 0;
            int demiparents = pParent1.data.hasTrait("Demi God") ? 1 : 0;
            int lesserparents = pParent1.hasTrait("Lesser God") ? 1 : 0;
            List<string> godtraits = new List<string>(GetGodTraits(pParent1));
            AddRange(godtraits, Getinheritedgodtraits(pParent1.data));
            if (parents == 2)
            {
                AddRange(godtraits, GetGodTraits(pParent2));
                AddRange(godtraits, Getinheritedgodtraits(pParent2.data));
                godparents += IsGod(pParent2) ? 1 : 0;
                demiparents += pParent2.data.hasTrait("Demi God") ? 1 : 0;
                lesserparents += pParent2.hasTrait("Lesser God") ? 1 : 0;
            }
            float chancemult = godparents > 0 ? 1 : 0.75f;
            chancemult += (godparents-1) / 2;
            chancemult += lesserparents / 4;
            chancemult += demiparents / 8;
            int importantgenes = godparents + lesserparents;
            Actor? chief = GreatClan?.getChief();
            if (chief != null)
            {
                if (IsGod(chief))
                {
                    MakeLesserGod(godtraits, ref child, chancemult);
                    return;
                }
            }
            if (parents == importantgenes)
            {
                MakeLesserGod(godtraits, ref child, chancemult);
                return;
            }
            else if (importantgenes == 1 || demiparents + importantgenes == parents)
            {
                MakeDemiGod(godtraits, ref child, chancemult);
                return;
            }
            AutoTrait(child, godtraits, true, chancemult);
        }
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
    [HarmonyPatch(typeof(ActorBase), "updateStats")]
    public class UseDemiStats
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            bool found = false;
            var codes = new List<CodeInstruction>(instructions);
            for (int i = 0; i < codes.Count; i++)
            {
                if (codes[i].opcode == OpCodes.Ldsfld && codes[i + 1].opcode == OpCodes.Stloc_S && !found)
                {
                    found = true;
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(UseDemiStats), nameof(mergedemistats)));
                }
                yield return codes[i];
            }
        }
        static void mergedemistats(ActorBase __instance)
        {
            if (__instance.hasTrait("Demi God") || __instance.hasTrait("Lesser God"))
            {
                mergeStats(GetDemiStats(__instance.data).stats_list, ref __instance.stats);
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
                    if (Toolbox.randomChance(GetEnhancedChance("God Of Knowledge", "EnemySwap%")))
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
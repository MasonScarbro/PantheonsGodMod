using ai.behaviours;
using HarmonyLib;
using SimpleJSON;
using SleekRender;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;
using static GodsAndPantheons.Traits;
//Harmony Patches
namespace GodsAndPantheons
{
    [HarmonyPatch(typeof(PowerLibrary), "spawnUnit")]
    public class MakeSummoned
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            CodeMatcher Matcher = new CodeMatcher(instructions);
            Matcher.MatchForward(false, new CodeMatch[]
			{
				new CodeMatch(new OpCode?(OpCodes.Callvirt), AccessTools.Method(typeof(ActorManager), nameof(ActorManager.spawnNewUnit)))
			});
            Matcher.Advance(1);
            Matcher.Insert(new CodeInstruction[]
            {
              new CodeInstruction(OpCodes.Dup),
              new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(MakeSummoned), nameof(MakeSummonedone)))
            });
            return Matcher.Instructions();
        }
        public static void MakeSummonedone(Actor a)
        {
            if (!Main.savedSettings.MakeSummoned)
            {
                return;
            }
            Actor Master = GetActorFromTile(a.currentTile, a);
            if(Master != null)
            {
                TurnActorIntoSummonedOne(a, Master);
            }
        }
        public static Actor GetActorFromTile(WorldTile pTile, Actor actortoexclude)
        {
            if (pTile == null)
            {
                return null;
            }
            Actor actor = null;
            float num = 0f;
            List<Actor> simpleList = World.world.units.getSimpleList();
            for (int i = 0; i < simpleList.Count; i++)
            {
                Actor actor2 = simpleList[i];
                if (actor2.isAlive() && actor2 != actortoexclude && !actor2.isInsideSomething() && !actor2.hasTrait("Summoned One"))
                {
                    float num2 = Toolbox.DistTile(actor2.currentTile, pTile);
                    if (num2 <= 3f && (actor == null || num2 < num))
                    {
                        actor = actor2;
                        num = num2;
                    }
                }
            }
            return actor;
        }
    }
    [HarmonyPatch(typeof(MapBox), "Update")]
    public class UpdateWorldStuff
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            CodeMatcher Matcher = new CodeMatcher(instructions);
            Matcher.MatchForward(false, new CodeMatch[]
            {
                new CodeMatch(new OpCode?(OpCodes.Call), AccessTools.Method(typeof(MapBox), nameof(MapBox.isPaused)))
            });
            Matcher.Advance(2);
            Matcher.Insert(new CodeInstruction[]
            {
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(UpdateWorldStuff), nameof(TryDivineMiracles)))
            });
            return Matcher.Instructions();
        }
        public static float Timer = 30;
        public static void TryDivineMiracles(MapBox instance)
        {
            if (blessingtime > 0)
            {
                blessingtime -= instance.elapsed;
                pb.flashPixel(LuckyOnee.currentTile);
                pb.divineLightFX(LuckyOnee.currentTile, null);
                pb.drawDivineLight(LuckyOnee.currentTile, null);
                SuperRegeneration(LuckyOnee, 100, 25);
            }
            else if (LuckyOnee != null)
            {
                blessingtime = 0;
                ActionLibrary.castShieldOnHimself(null, LuckyOnee, null);
                LuckyOnee = null;
            }
            if (!Main.savedSettings.DevineMiracles)
            {
                return;
            }
            Timer -= instance.elapsed;
            if (Timer > 0)
            {
                return;
            }
            Timer = 30;
            if (!Toolbox.randomChance(0.003f))
            {
                return;
            }
            List<Kingdom> list = World.world.kingdoms.list_civs;
            List<string> availabletraits = getavailblegodtraits(instance);
            if (availabletraits.Count == 0)
            {
                return;
            }
            availabletraits.Shuffle();
            list.Shuffle();
            foreach (Kingdom k in list)
            {
                if (!DoesKingdomHaveGod(k))
                {
                    List<Actor> units = k.units.getSimpleList();
                    units.Shuffle();
                    foreach (Actor a in units)
                    {
                        if (!a.hasTrait("infertile") && !a.hasTrait("Demi God") && !a.hasTrait("Lesser God"))
                        {
                            DivineMiracle(a, availabletraits[0]);
                            Timer = 300;
                            return;
                        }
                    }
                }
            }
        }
        static Actor LuckyOnee;
        static float blessingtime = 0;
        public static void DivineMiracle(Actor LuckyOne, string godtrait)
        {
            World.world.startShake(1f, 0.01f, 2f, true, true);
            LuckyOne.addTrait(godtrait);
            LuckyOnee = LuckyOne;
            blessingtime = 10;
            WorldLogMessage worldLogMessage = new WorldLogMessage($"A Divine Miracle Has Occurred in {LuckyOne.kingdom.name}!")
            {
                unit = LuckyOne,
                icon = "iconNightchild",
                location = LuckyOne.currentPosition,
                color_special1 = LuckyOne.kingdom.kingdomColor.getColorText(),
                color_special2 = LuckyOne.kingdom.kingdomColor.getColorText()
            };
            worldLogMessage.add();
        }
    }
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
            if (!Main.savedSettings.GodKings)
            {
                return null;
            }
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
            CodeMatcher Matcher = new CodeMatcher(instructions);
            Matcher.MatchForward(false, new CodeMatch[]
            {
                new CodeMatch(OpCodes.Ldnull)
            });
            Matcher.RemoveInstruction();
            Matcher.Insert(new CodeInstruction[] {
                new CodeInstruction(OpCodes.Ldarg_1),
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(findking), nameof(GetKing))) });
            return Matcher.Instructions();
        }
    }
    [HarmonyPatch(typeof(TooltipLibrary), "showTrait")]
    public class showdemistats
    {
        static BaseStats getdemistats(ActorTrait trait)
        {
            if((trait.id == "Demi God" || trait.id == "Lesser God") && Config.selectedUnit != null)
            {
                return GetDemiStats(Config.selectedUnit.data);
            }
            return trait.base_stats;
        }
        
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            CodeMatcher Matcher = new CodeMatcher(instructions);
            Matcher.MatchForward(false, new CodeMatch[]
            {
                new CodeMatch(OpCodes.Ldfld, AccessTools.Field(typeof(ActorTrait), "base_stats"))
            });
            Matcher.RemoveInstruction();
            Matcher.Insert(new CodeInstruction[] {
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(showdemistats), nameof(getdemistats))) });
            return Matcher.Instructions();
        }
    }
    [HarmonyPatch(typeof(BaseSimObject), "canAttackTarget")]
    public class DontAttack
    {
        static void Postfix(BaseSimObject __instance, BaseSimObject pTarget, ref bool __result)
        {
            if (Main.savedSettings.HunterAssasins)
            {
                if (pTarget.hasStatus("Invisible") || __instance.hasStatus("Invisible"))
                {
                    __result = false;
                }
                if (__instance.isActor() && __instance.a.hasTrait("God Hunter") && __instance.a.data.health < __instance.getMaxHealth() * (__instance.hasStatus("powerup") ? 0.7 : 0.35))
                {
                    __result = false;
                }
            }
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
            if (__instance.finished)
            {
                if (__instance.asset.id == "Invisible" || __instance.asset.id == "FireStorm")
                {
                    __instance._simObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                }
                if(__instance.asset.id == "Lassering" && __instance._simObject.transform.childCount > 0)
                {
                    __instance._simObject.transform.GetChild(0)?.gameObject.DestroyImmediateIfNotNull();
                }
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
                if(pID == "Invisible" || pID == "FireStorm")
                {
                    __instance.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                }
                if(pID == "Lassering" && __instance.transform.childCount > 0)
                {
                    __instance.transform.GetChild(0)?.gameObject.DestroyImmediateIfNotNull();
                }
            }
        }
    }
    [HarmonyPatch(typeof(BehMakeBaby), "makeBaby")]
    public class InheritGodTraitsFromNonCitizens
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            CodeMatcher Matcher = new CodeMatcher(instructions);
            Matcher.MatchForward(false, new CodeMatch[]
            {
                new CodeMatch(OpCodes.Stloc_2)
            });
            Matcher.Advance(1);
            Matcher.Insert(new CodeInstruction[]
            {
                new CodeInstruction(OpCodes.Ldloc_2),
                new CodeInstruction(OpCodes.Ldarg_1),
                new CodeInstruction(OpCodes.Ldarg_2),
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(InheritGodTraitsFromNonCitizens), nameof(MakeBaby)))
            });
            return Matcher.Instructions();
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
            CodeMatcher Matcher = new CodeMatcher(instructions);
            Matcher.MatchForward(false, new CodeMatch[]
            {
                new CodeMatch((ci) => ci.opcode == OpCodes.Stloc_S && ci.operand is LocalBuilder localBuilder && localBuilder.LocalIndex == 5)
            });
            Matcher.Advance(1);
            Matcher.Insert(new CodeInstruction[]
            {
                new CodeInstruction(OpCodes.Ldloc_S, 4),
                new CodeInstruction(OpCodes.Ldloc_0),
                new CodeInstruction(OpCodes.Ldloc_1),
                new CodeInstruction(OpCodes.Ldloc_S, 5),
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(InheritGodTraits), nameof(inheritgodtraits)))
            });
            return Matcher.Instructions();
        }
        public static void inheritgodtraits(ActorData child, Actor pParent1, Actor pParent2, Clan GreatClan)
        {
            int parents = pParent2 != null ? 2 : 1;
            float godparents = IsGod(pParent1) ? 1 : 0;
            float demiparents = pParent1.data.hasTrait("Demi God") ? 1 : 0;
            float lesserparents = pParent1.hasTrait("Lesser God") ? 1 : 0;
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
            if(godparents == 0 && demiparents == 0 && lesserparents == 0) {
                return; 
            }
            float chancemult = 0.5f;
            chancemult += godparents / 2;
            chancemult += lesserparents / 4;
            chancemult += demiparents / 8;
            float importantgenes = godparents + lesserparents;
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
            else if (importantgenes == 1 || demiparents == parents)
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
            CodeMatcher Matcher = new CodeMatcher(instructions);
            Matcher.MatchForward(false, new CodeMatch[]
            {
                new CodeMatch(OpCodes.Ldsfld),
                new CodeMatch(OpCodes.Stloc_S),
            });
            Matcher.Advance(2);
            Matcher.Insert(new CodeInstruction[]
            {
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(UseDemiStats), nameof(mergedemistats)))
            });
            return Matcher.Instructions();
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
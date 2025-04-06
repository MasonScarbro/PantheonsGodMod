using ai.behaviours;
using HarmonyLib;
using SleekRender;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using static GodsAndPantheons.Traits;
//Harmony Patches
namespace GodsAndPantheons.Patches
{
    [HarmonyPatch(typeof(Actor), nameof(Actor.ignoresBlocks))]
    public class EarthGodWalkOnMountains
    {
        static void Postfix(Actor __instance, ref bool __result)
        {
            if(__instance.hasTrait("Earth Walker"))
            {
                __result = true;
            }
        }
    }
    [HarmonyPatch(typeof(Actor), nameof(Actor.u6_checkFrozen))]
    public class DontMoveIfPetrified
    {
        static void Postfix(Actor __instance)
        {
            if (__instance.hasStatus("Petrified"))
            {
                __instance.skipUpdates();
            }
        }
    }
    [HarmonyPatch(typeof(MapBox), nameof(MapBox.applyForceOnTile))]
    public class RandomForce
    {
        [HarmonyReversePatch]
        //pForceOut is useless here! the max radius is also a lot larger
        public static void CreateRandomForce(object instance, WorldTile pTile, int pRad = 10, float pForceAmount = 1.5f, bool pForceOut = true, int pDamage = 0, string[] pIgnoreKingdoms = null, BaseSimObject pByWho = null, TerraformOptions pOptions = null, bool pChangeHappiness = false)
        {
            IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                CodeMatcher Matcher = new CodeMatcher(instructions);
                Matcher.MatchForward(false, new CodeMatch[] { new CodeMatch(OpCodes.Ldc_I4_1) });
                Matcher.RemoveInstruction();
                Matcher.Insert(new CodeInstruction[]
                {
                    new CodeInstruction(OpCodes.Ldc_I4, 16),
                });
                Matcher.MatchForward(false, new CodeMatch[]
                {
                    new CodeMatch(OpCodes.Ldarg_S, (byte)4)
                });
                List<Label> label = Matcher.Instruction.labels;
                Matcher.RemoveInstruction();
                Matcher.Insert(new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(Randy), nameof(Randy.randomBool))) {labels = label});
                return Matcher.Instructions();
            }
        }
    }
    /*[HarmonyPatch(typeof(Clan), nameof(Clan.newClanInit))]
    public class FixClanRaceError
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            CodeMatcher Matcher = new CodeMatcher(instructions);
            Matcher.MatchForward(false, new CodeMatch[]
            {
                new CodeMatch(new OpCode?(OpCodes.Ldfld), AccessTools.Field(typeof(Actor), nameof(Actor.race)))
            });
            Matcher.RemoveInstruction();
            Matcher.Insert(new CodeInstruction[]
            {
              new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(FixClanRaceError), nameof(GetTrueRace)))
            });
            return Matcher.Instructions();
        }
        public static race GetTrueRace(Actor actor)
        {
            if(actor.asset.race != SK.dragons)
            {
                return actor.race;
            }
            actor.data.get("oldself", out string oldself, SA.unit_human);
            return AssetManager.raceLibrary.get(AssetManager.actor_library.get(oldself).race);
        }
    }*/ //idk if this is neccessary anymore
    //[HarmonyPatch(typeof(PowerLibrary), "spawnUnit")]
    public class MakeSummoned
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            CodeMatcher Matcher = new CodeMatcher(instructions);
            Matcher.MatchForward(false, new CodeMatch[]
			{
				new CodeMatch(OpCodes.Callvirt, AccessTools.Method(typeof(ActorManager), nameof(ActorManager.spawnNewUnit)))
			});
            Debug.Log(Matcher.Operand);
            Matcher.Advance(2);
            Debug.Log(Matcher.Operand);
            Matcher.Insert(new CodeInstruction[]
            {
              new CodeInstruction(OpCodes.Ldloc_2),
              new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(MakeSummoned), nameof(MakeSummonedone)))
            });
            foreach(CodeInstruction instruction in instructions)
            {
                Debug.Log($"{instruction.opcode} with {instruction.operand}");
            }
            return Matcher.Instructions();
        }
        public static void MakeSummonedone(Actor a)
        {
            if (!Main.savedSettings.MakeSummoned)
            {
                return;
            }
            Actor Master = GetActorFromTile(a.current_tile, a);
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
                    float num2 = Toolbox.DistTile(actor2.current_tile, pTile);
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
    [HarmonyPatch(typeof(Actor), "nextJobActor")]
    public class jobapply
    {
        static void Postfix(ref string __result, Actor pActor)
        {
            if (pActor.hasStatus("Blinded"))
            {
                __result = "random_move";
                return;
            }
            if (pActor.hasTrait("Summoned One"))
            {
                __result = "SummonedJob";
            }
            if (pActor.hasStatus("BrainWashed"))
            {
                __result = "BrainWashedJob";
            }
        }
    }
    [HarmonyPatch(typeof(SuccessionTool), nameof(SuccessionTool.getKingFromLeaders))]
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
                list.Sort(new Comparison<Actor>(ListSorters.sortUnitByAgeOldFirst));
                return list[0];
            }
            return null;
        }
        static void Postfix(Kingdom pKingdom, ref Actor __result)
        {
            Actor GodKing = GetKing(pKingdom);
            if (GodKing != null)
            {
                __result = GodKing;
            }
        }
    }
    [HarmonyPatch(typeof(TooltipLibrary), "showTrait")]
    public class showdemistats
    {
        static BaseStats[] getdemistats(ActorTrait trait)
        {
            if((trait.id == "Demi God" || trait.id == "Lesser God") && SelectedUnit.unit != null)
            {
                return new BaseStats[] { GetDemiStats(SelectedUnit.unit.data) };
            }
            return null;
        }
        
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            CodeMatcher Matcher = new CodeMatcher(instructions);
            Matcher.MatchForward(false, new CodeMatch[]
            {
                new CodeMatch(OpCodes.Call, AccessTools.Field(typeof(Array), nameof(Array.Empty)))
            });
            Matcher.RemoveInstruction();
            Matcher.Insert(new CodeInstruction[] {
                new CodeInstruction(OpCodes.Ldloc_0),
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(showdemistats), nameof(getdemistats))) });
            return Matcher.Instructions();
        }
    }
    [HarmonyPatch(typeof(BaseSimObject), "canAttackTarget")]
    public class DontAttack
    {
        static void Postfix(BaseSimObject __instance, BaseSimObject pTarget, ref bool __result)
        {
            if (__instance.hasStatus("Blinded") || __instance.hasStatus("Petrified"))
            {
                __result = false;
                return;
            }
            if (Main.savedSettings.HunterAssasins)
            {
                if (pTarget.hasStatus("Invisible") || __instance.hasStatus("Invisible"))
                {
                    __result = false;
                    return;
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
            if(pDeadUnit == null) return;
            bool isgod = IsGod(pDeadUnit);
            if(__instance.hasTrait("NecroMancer") && !isgod)
            {
                Actor actor = CopyActor(pDeadUnit, pDeadUnit.asset.zombie_id);
                if (actor != null)
                {
                    TurnActorIntoSummonedOne(actor, __instance, 101);
                }
            }
            if (isgod)
            {
                __instance.addTrait("God Killer");
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
    [HarmonyPatch(typeof(Status), "update")]
    public class finishinvisibility
    {
        static void Postfix(Status __instance)
        {
            if (__instance._finished)
            {
                if (__instance.asset.id == "Invisible")
                {
                   // __instance._sim_object.a.avatar.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1); //stupid maxim
                }
                if(__instance.asset.id == "Lassering" && __instance._sim_object.a.avatar.transform.childCount > 0)
                {
                    //__instance._sim_object.a.avatar.transform.GetChild(0)?.gameObject.DestroyImmediateIfNotNull(); fix later
                }
                if(__instance.asset.id == "BrainWashed")
                {
                    FinishBrainWashing(__instance._sim_object.a);
                }
                if(__instance.asset.id == "Levitating")
                {
                    Actor actor = __instance._sim_object.a;
                    Actor Target = GetTargetToCrashLand(actor);
                    if (Target != null)
                    {
                        actor.velocity.z = 0;
                        PushActorTowardsTile(Target.current_tile.pos, actor, 0.1f);
                        Target.getHit(Target.getMaxHealth() * 0.1f, true, AttackType.Other, null, false);
                        actor.getHit(actor.getMaxHealth() * 0.4f, true, AttackType.Other, null, false);
                    }
                }
            }
        }
    }
    [HarmonyPatch(typeof(BaseSimObject), "finishStatusEffect")]
    public class finishinvisibility2
    {
        static void Postfix(string pID, BaseSimObject __instance)
        {
            if(__instance._active_status_dict == null) { return; }
            if (__instance._active_status_dict.ContainsKey(pID))
            {
                if(pID == "Invisible")
                {
                    //__instance.a.avatar.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1); nuh uh
                }
                if(pID == "Lassering" && __instance.a.avatar.transform.childCount > 0)
                {
                   // __instance.a.avatar.transform.GetChild(0)?.gameObject.DestroyImmediateIfNotNull(); fix later
                }
                if (pID == "BrainWashed")
                {
                    FinishBrainWashing(__instance.a);
                }
            }
        }
    }
    [HarmonyPatch(typeof(BabyHelper), "applyParentsMeta")]
    public class InheritGodTraits
    {
        public static void Postfix(Actor pParent1, Actor pParent2, Actor pBaby)
        {
            inheritgodtraits(pBaby, pParent1, pParent2, BabyHelper.checkGreatClan(pParent1, pParent2));
        }
        public static void inheritgodtraits(Actor child, Actor pParent1, Actor pParent2, Clan GreatClan)
        {
            int parents = pParent2 != null ? 2 : 1;
            float godparents = IsGod(pParent1) ? 1 : 0;
            float demiparents = pParent1.hasTrait("Demi God") ? 1 : 0;
            float lesserparents = pParent1.hasTrait("Lesser God") ? 1 : 0;
            using ListPool<string> godtraits = new ListPool<string>(GetGodTraits(pParent1));
            AddRange(godtraits, Getinheritedgodtraits(pParent1.data));
            if (parents == 2)
            {
                AddRange(godtraits, GetGodTraits(pParent2));
                AddRange(godtraits, Getinheritedgodtraits(pParent2.data));
                godparents += IsGod(pParent2) ? 1 : 0;
                demiparents += pParent2.hasTrait("Demi God") ? 1 : 0;
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
            AutoTrait(child, godtraits, true, chancemult);
            if (chief != null)
            {
                if (IsGod(chief))
                {
                    MakeLesserGod(godtraits, child, chancemult);
                    return;
                }
            }
            if (parents == importantgenes)
            {
                MakeLesserGod(godtraits, child, chancemult);
                return;
            }
            else if (importantgenes == 1 || demiparents == parents)
            {
                MakeDemiGod(godtraits, child, chancemult);
                return;
            }
        }
        static void AddRange(ListPool<string> list, List<string> range)
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
    [HarmonyPatch(typeof(Actor), nameof(Actor.precalcMovementSpeed))]
    public class ModifySpeed
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            CodeMatcher Matcher = new CodeMatcher(instructions);
            Matcher.MatchForward(false, new CodeMatch[]
            {
                new CodeMatch(OpCodes.Ldc_R4, 0.4f)
            });
            Matcher.Advance(1);
            Matcher.Insert(new CodeInstruction[]
            {
                new CodeInstruction(OpCodes.Mul),
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(ModifySpeed), nameof(GetCustomSpeedModifier)))
            });
            return Matcher.Instructions();
        }
        public static float GetCustomSpeedModifier(Actor actor)
        {
            float Speed = 1;
            if(actor.hasTrait("Earth Walker") && !actor.current_tile.Type.liquid)
            {
                Speed /= actor.current_tile.Type.walk_multiplier;
            }
            return Speed;
        }
    }
    [HarmonyPatch(typeof(Actor), "updateStats")]
    public class AddCustomStats
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            CodeMatcher Matcher = new CodeMatcher(instructions);
            Matcher.MatchForward(false, new CodeMatch[]
            {
                new CodeMatch(OpCodes.Callvirt, AccessTools.Method(typeof(BaseStats), nameof(BaseStats.clear)))
            });
            Matcher.Advance(1);
            Matcher.Insert(new CodeInstruction[]
            {
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(AddCustomStats), nameof(MergeCustomStats)))
            });
            return Matcher.Instructions();
        }
        static void MergeCustomStats(Actor __instance)
        {
            if (__instance.hasTrait("Demi God") || __instance.hasTrait("Lesser God"))
            {
                __instance.stats.mergeStats(GetDemiStats(__instance.data));
            }
        }
    }
    [HarmonyPatch(typeof(MapBox), "applyAttack")]
    public class KnowledgeGodEnemySwap
    {
        static bool Prefix(AttackData pData, ref BaseSimObject pTargetToCheck)
        {
            if (pTargetToCheck.isActor() && pData.initiator != null)
            {
                if (pTargetToCheck.a.hasTrait("God Of War"))
                {
                    if (Randy.randomChance(GetEnhancedChance("God Of War", "BlockAttack%")))
                    {
                        MusicBox.playSound(MB.HitSwordSword, pTargetToCheck.current_tile);
                        if (Toolbox.DistTile(pTargetToCheck.current_tile, pData.initiator.current_tile) < 3)
                        {
                            pTargetToCheck = pData.initiator;
                            return true;
                        }
                        return false;
                    }
                }
                if (pTargetToCheck.a.hasTrait("God Of Knowledge"))
                {
                    if (Randy.randomChance(GetEnhancedChance("God Of Knowledge", "EnemySwap%")))
                    {
                        WorldTile tile = pTargetToCheck.current_tile;
                        List<BaseSimObject> enemies = EnemiesFinder.findEnemiesFrom(tile, pTargetToCheck.kingdom, -1).list;
                        if(enemies == null)
                        {
                            return true;
                        }
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
                            EffectsLibrary.spawnAt("fx_teleport_blue", enemytoswap.current_tile.posV3, pTargetToCheck.stats[S.scale]);
                            pTargetToCheck.a.spawnOn(enemytoswap.current_tile);
                            enemytoswap.spawnOn(tile);
                            pTargetToCheck = enemytoswap;
                        }
                    }
                }
            }
            return true;
        }
    }
}
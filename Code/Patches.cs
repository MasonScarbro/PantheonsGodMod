using ai.behaviours;
using GodsAndPantheons.CustomEffects;
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
    public class LavaWalkers
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            CodeMatcher Matcher = new CodeMatcher(instructions);
            Matcher.MatchForward(false, new CodeMatch[]
            {
                new CodeMatch(OpCodes.Ldfld, AccessTools.Field(typeof(Actor), nameof(Actor.asset))),
                new CodeMatch(OpCodes.Ldfld, AccessTools.Field(typeof(ActorAsset), nameof(ActorAsset.die_in_lava)))
            });
            Matcher.RemoveInstructions(2);
            Matcher.Insert(new CodeInstruction[]
            {
              new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(Traits), nameof(Traits.CanDieInLava)))
            });
            return Matcher.Instructions();
        }
    }
    [HarmonyPatch(typeof(WorldAgeManager), nameof(WorldAgeManager.updateEffects))]
    public class BloodMoonDarkenWorld
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            CodeMatcher Matcher = new CodeMatcher(instructions);
            Matcher.MatchForward(false, new CodeMatch[]
            {
                new CodeMatch(OpCodes.Ldarg_0),
                new CodeMatch(OpCodes.Ldfld),
                new CodeMatch(OpCodes.Ldfld)
            });
            Matcher.Advance(1);
            Matcher.RemoveInstructions(2);
            Matcher.Insert(new CodeInstruction[]
            {
              new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(BloodMoonDarkenWorld), nameof(BloodMoonDarkenWorld.MakeWorldDark)))
            });
            return Matcher.Instructions();
        }
        public static bool MakeWorldDark(WorldAgeManager manager)
        {
            return manager._current_age.overlay_darkness || BloodMoon.BloodMoonPresent;
        }
    }
    [HarmonyPatch(typeof(Actor), nameof(Actor.isAffectedByLiquid))]
    public class LavaWalkerWalkOnLava
    {
        static void Postfix(Actor __instance, ref bool __result)
        {
            if(__instance.hasTrait("Lava Walker") && __instance.current_tile.Type.lava)
            {
                __result = false;
            }
        }
    }
    [HarmonyPatch(typeof(Actor), nameof(Actor.isWaterCreature))]
    public class LavaWalkerDontDrowinInlava
    {
        static void Postfix(Actor __instance, ref bool __result)
        {
            if (__instance.hasTrait("Lava Walker") && __instance.current_tile.Type.lava)
            {
                __result = true;
            }
        }
    }

    [HarmonyPatch(typeof(Dragon), nameof(Dragon.getHit))]
    public class FireGodExplodeEnemy
    {
        static void Prefix(BaseSimObject pSelf, BaseSimObject pAttackedBy)
        {
            if(pSelf.a.hasTrait("God Of Fire") && pAttackedBy != null)
            {
                if (CanUseAbility("God Of Fire", "MorphIntoDragon%", 25))
                {
                    CreateFireExplosion(pSelf, pAttackedBy);
                }
            }
        }
    }

    [HarmonyPatch(typeof(Actor), nameof(Actor.makeStunned))]
    public class GodsImmuneToStuns
    {
        static bool Prefix(Actor __instance)
        {
            return !__instance.IsGod();
        }
    }
    [HarmonyPatch(typeof(Actor), nameof(Actor.ignoresBlocks))]
    public class EarthGodNotAffectedByMountains
    {
        static void Postfix(Actor __instance, ref bool __result)
        {
            if(__instance.hasTrait("Earth Walker") || __instance.hasTrait("God Hunter") || __instance.hasStatus("At The Speed Of Light"))
            {
                __result = true;
            }
        }
    }
    [HarmonyPatch(typeof(Actor), nameof(Actor.goTo))]
    public class EarthGodWalkOnMountains
    {
        static void Prefix(Actor __instance, ref bool pWalkOnBlocks)
        {
            if (__instance.hasTrait("Earth Walker")) {
                pWalkOnBlocks = true;
            }
        }
    }
    [HarmonyPatch(typeof(KingdomBehCheckKing), nameof(KingdomBehCheckKing.checkClanCreation))]
    public class FixClanRaceError
    {
        static bool Prefix(Actor pActor)
        {
            if (pActor.HasMorphed())
            {
                return false;
            }
            return true;
        }
    }
    [HarmonyPatch(typeof(Kingdom), nameof(Kingdom.getActorAsset))]
    public class fixbannererror
    {
        static void Postfix(Kingdom __instance, ref ActorAsset __result)
        {
            if (__instance.hasKing())
            {
                __result = __instance.king.GetTrueAsset();
            }
        }
    }
    [HarmonyPatch(typeof(PowerLibrary), "spawnUnit")]
    public class MakeSummoned
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
            Actor Master = GetActorFromTile(a.current_tile, a);
            if(Master != null)
            {
                TurnActorIntoSummonedOne(a, Master);
            }
        }
        public static Actor GetActorFromTile(WorldTile pTile, Actor ActorToExclude)
        {
            if (pTile == null)
            {
                return null;
            }
            Actor Master = null;
            float MinDist = 3.1f;
            foreach (Actor Actor in World.world.units)
            {
                if (Actor.isAlive() && Actor != ActorToExclude && !Actor.isInsideSomething() && !Actor.hasTrait("Summoned One"))
                {
                    float Dist = Toolbox.DistTile(Actor.current_tile, pTile);
                    if (Dist < MinDist)
                    {
                        Master = Actor;
                        MinDist = Dist;
                    }
                }
            }
            return Master;
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
            if (pActor.hasStatus("At The Speed Of Light"))
            {
                __result = "AtTheSpeedOfLight";
                return;
            }
            if (pActor.hasStatus("BrainWashed"))
            {
                __result = "BrainWashedJob";
                return;
            }
            if (pActor.hasTrait("Summoned One"))
            {
                __result = "SummonedJob";
                return;
            }
            if(pActor.hasTrait("God Hunter"))
            {
                __result = "GodHunter";
                return;
            }
            if (pActor.HasMorphed() && pActor.asset.job.Length != 0)
            {
                __result = pActor.asset.job.GetRandom();
                return;
            }
        }
    }
    [HarmonyPatch(typeof(KingdomBehCheckKing), nameof(KingdomBehCheckKing.execute))]
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
                if (a.IsGod())
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
        static bool Prefix(Kingdom pKingdom)
        {
            if (pKingdom.hasKing() || pKingdom.data.timer_new_king > 0)
            {
                return true;
            }
            Actor pNewKing = GetKing(pKingdom);
            if(pNewKing != null)
            {
                if (pNewKing.hasCity() && pNewKing.isCityLeader())
                {
                    pNewKing.city.removeLeader();
                }
                if (pKingdom.hasCapital() && pNewKing.city != pKingdom.capital)
                {
                    pNewKing.setCity(pKingdom.capital);
                }
                pKingdom.setKing(pNewKing, true);
                WorldLog.logNewKing(pKingdom);
            }
            return true;
        }
    }
    [HarmonyPatch(typeof(Actor), nameof(Actor.getMaturationTimeSeconds))]
    public class GodsDevelopFaster
    {
        static void Postfix(Actor __instance, ref float __result)
        {
            __result *= GetMul(__instance) * GetMul(__instance.lover);
        }
        static float GetMul(Actor Actor)
        {
            if(Actor == null)
            {
                return 1;
            }
            if (Actor.IsGod())
            {
                return Randy.randomFloat(0.1f, 0.2f);
            }
            if (Actor.hasTrait("Lesser God"))
            {
                return Randy.randomFloat(0.3f, 0.5f);
            }
            if (Actor.hasTrait("Demi God"))
            {
                return Randy.randomFloat(0.6f, 0.9f);
            }
            return 1;
        }
    }
    [HarmonyPatch(typeof(TooltipLibrary), "showTrait")]
    public class showdemistats
    {
        static BaseStats[] getdemistats(ActorTrait trait)
        {
            if((trait.id == "Demi God" || trait.id == "Lesser God" || trait.id == "God Killer") && SelectedUnit.unit != null)
            {
                DemiGodData Data = SelectedUnit.unit.DemiData();
                if (Data != null)
                {
                    return new BaseStats[] { Data.BaseStats };
                }
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
    [HarmonyPatch(typeof(BaseSimObject), nameof(BaseSimObject.canAttackTarget))]
    public class DontAttack
    {
        static void Postfix(BaseSimObject __instance, BaseSimObject pTarget, ref bool __result)
        {
            if (__instance.hasStatus("Blinded") || __instance.hasStatus("At The Speed Of Light"))
            {
                __result = false;
                return;
            }
            if (pTarget.hasStatus("Invisible"))
            {
                __result = false;
                return;
            }
            if (__instance.isActor())
            {
                if(__instance.a.asset.unit_zombie && !__instance.kingdom.isCiv() && pTarget.isActor() && pTarget.a.hasTrait("God Of The Lich"))
                {
                    __result = false;
                }
                if(__instance.a.hasTrait("God Of Chaos") && pTarget.isBuilding() && pTarget.b.asset.id == "corrupted_brain")
                {
                    __result = false;
                }
                if(__instance.hasStatus("BrainWashed") || __instance.a.hasTrait("Summoned One"))
                {
                    Actor Master = __instance.a.FindMaster();
                    if (Master != null)
                    {
                        if (Master == pTarget)
                        {
                            __result = false;
                            return;
                        }
                        __result = Master.canAttackTarget(pTarget);
                        return;
                    }
                }
                if(__instance.a.hasTrait("God Hunter"))
                {
                    if (__instance.hasStatus("Invisible"))
                    {
                        __result = false;
                    }
                }
            }
            else
            {
                if(__instance.b.asset.id == "corrupted_brain" && pTarget.isActor() && pTarget.a.hasTrait("God Of Chaos"))
                {
                    __result = false;
                    return;
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
            bool isgod = pDeadUnit.IsGod();
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
                CreateGodKiller(__instance, pDeadUnit.GetGodTraits());
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
    [HarmonyPatch(typeof(BaseSimObject), "finishStatusEffect")]
    public class finishstatuseffects
    {
        static void Postfix(string pID, BaseSimObject __instance)
        {
            if(__instance._active_status_dict == null) { return; }
            if (__instance._active_status_dict.ContainsKey(pID))
            {
                if(pID == "Invisible")
                {
                    __instance.a.color = new Color(1, 1, 1, 1);
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
            float godparents = pParent1.IsGod() ? 1 : 0;
            float demiparents = pParent1.hasTrait("Demi God") ? 1 : 0;
            float lesserparents = pParent1.hasTrait("Lesser God") ? 1 : 0;
            using ListPool<string> godtraits = new ListPool<string>(pParent1.GetGodTraits());
            AddRange(godtraits, pParent1.Getinheritedgodtraits());
            if (parents == 2)
            {
                AddRange(godtraits, pParent2.GetGodTraits());
                AddRange(godtraits, pParent2.Getinheritedgodtraits());
                godparents += pParent2.IsGod() ? 1 : 0;
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
                if (chief.IsGod())
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
        static void AddRange(ListPool<string> list, IEnumerable<string> range)
        {
            if(range == null)
            {
                return;
            }
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
            if (actor.asset.ignore_tile_speed_multiplier)
            {
                return Speed;
            }
            if(actor.hasTrait("Earth Walker") && !actor.current_tile.Type.liquid)
            {
                Speed /= actor.current_tile.Type.walk_multiplier;
            }
            if(actor.hasTrait("Lava Swimmer") && actor.current_tile.Type.lava)
            {
                Speed /= actor.current_tile.Type.walk_multiplier;
            }
            return Speed;
        }
    }
    [HarmonyPatch(typeof(Actor), nameof(Actor.getHit))]
    public class GetHitMore
    {
        static void Prefix(Actor __instance, BaseSimObject pAttacker, AttackType pAttackType, ref float pDamage)
        {
            if(pAttackType != AttackType.Weapon || pAttacker == null || !pAttacker.isActor() || pAttacker.a.equipment == null)
            {
                return;
            }
            ItemAsset Weapon = pAttacker.a.getWeaponAsset();
            if (__instance.IsGod() && Weapon.id == "GodHuntersScythe")
            {
                pDamage *= 5;
            }
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
            if (__instance.hasTrait("Demi God") || __instance.hasTrait("Lesser God") || __instance.hasTrait("God Killer"))
            {
                __instance.stats.mergeStats(__instance.GetDemiStats());
            }
            if(__instance.hasTrait("God Of Fire") && __instance.asset.id == SA.dragon)
            {
                __instance.stats[S.multiplier_health] += 5;
            }
        }
    }
    [HarmonyPatch(typeof(MapBox), "applyAttack")]
    public class CustomAttacks
    {
        static bool Prefix(AttackData pData, ref BaseSimObject pTargetToCheck, ref AttackDataResult __result)
        {
            if (pTargetToCheck.hasStatus("At The Speed Of Light"))
            {
                __result = AttackDataResult.Miss;
                return false;
            }
            if (pData.initiator == null)
            {
                return true;
            }
            if(pData.is_projectile && pData.projectile_id == "Heart")
            {
                __result = AttackDataResult.Hit;
                return false;
            }
            if (pData.initiator.isActor() && pData.initiator.a.IsGod(false, true, true))
            {
                pData.initiator.a.data.set("AttackFromProjectile", pData.projectile_id);
            }
            if (!pTargetToCheck.isActor())
            {
                return true;
            }
            if (pTargetToCheck.a.hasTrait("God Of War") && CanUseAbility("God Of War", "BlockAttack%"))
            {
                MusicBox.playSound(MB.HitSwordSword, pTargetToCheck.current_tile);
                if (pData.is_projectile)
                {
                    CombatActionLibrary.combat_action_deflect.action_actor(pTargetToCheck.a, pData);
                    __result = new AttackDataResult(ApplyAttackState.Deflect, pTargetToCheck.getID());
                    return false;
                }
                if (Toolbox.DistTile(pTargetToCheck.current_tile, pData.initiator.current_tile) < 4)
                {
                    pTargetToCheck = pData.initiator;
                }
                AssetManager.combat_action_library.doBlockAction(pTargetToCheck.a, pData);
                __result = AttackDataResult.Block;
                return false;
            }
            if (pTargetToCheck.a.hasTrait("God Of Knowledge") && CanUseAbility("God Of Knowledge", "EnemySwap%"))
            {
                WorldTile tile = pTargetToCheck.current_tile;
                List<BaseSimObject> enemies = EnemiesFinder.findEnemiesFrom(tile, pTargetToCheck.kingdom, -1).list;
                if (enemies == null)
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
            return true;
        }
    }
}
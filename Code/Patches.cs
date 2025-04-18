﻿using ai.behaviours;
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
    [HarmonyPatch(typeof(Dragon), nameof(Dragon.getHit))]
    public class FireGodExplodeEnemy
    {
        static void Prefix(BaseSimObject pSelf, BaseSimObject pAttackedBy)
        {
            if(pSelf.a.hasTrait("God Of Fire") && pAttackedBy != null)
            {
                if (Randy.randomChance(Chance("God Of Fire", "MorphIntoDragon%", 25)))
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
            return !IsGod(__instance);
        }
    }
    [HarmonyPatch(typeof(Actor), nameof(Actor.ignoresBlocks))]
    public class EarthGodNotAffectedByMountains
    {
        static void Postfix(Actor __instance, ref bool __result)
        {
            if(__instance.hasTrait("Earth Walker") || __instance.hasTrait("God Hunter"))
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
            if (HasMorphed(pActor))
            {
                return false;
            }
            return true;
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
            if (HasMorphed(pActor) && pActor.asset.job.Length != 0)
            {
                __result = pActor.asset.job.GetRandom();
                return;
            }
            if (pActor.hasStatus("Blinded"))
            {
                __result = "random_move";
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
            if (IsGod(Actor))
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
    [HarmonyPatch(typeof(BaseSimObject), nameof(BaseSimObject.canAttackTarget))]
    public class DontAttack
    {
        static void Postfix(BaseSimObject __instance, BaseSimObject pTarget, ref bool __result)
        {
            if (__instance.hasStatus("Blinded"))
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
                    Actor Master = FindMaster(__instance.a);
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
                    if(pTarget.isActor() && pTarget.areFoes(__instance) && IsGod(pTarget.a))
                    {
                        __result = true;
                        return;
                    }
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
        static void AddRange(ListPool<string> list, IEnumerable<string> range)
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
    [HarmonyPatch(typeof(Actor), nameof(Actor.getHit))]
    public class GetHitMore
    {
        static void Prefix(Actor __instance, BaseSimObject pAttacker, ref float pDamage)
        {
            if(pAttacker == null || !pAttacker.isActor() || pAttacker.a.equipment == null || __instance == null)
            {
                return;
            }
            if(__instance?.asset == null)
            {
                return;
            }
            ItemAsset Weapon = pAttacker.a.getWeaponAsset();
            if (IsGod(__instance) && Weapon.id == "GodHuntersScythe")
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
            if (__instance.hasTrait("Demi God") || __instance.hasTrait("Lesser God"))
            {
                __instance.stats.mergeStats(GetDemiStats(__instance.data));
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
            if (pData.initiator == null)
            {
                return true;
            }
            if(pData.is_projectile && pData.projectile_id == "Heart")
            {
                __result = AttackDataResult.Hit;
                return false;
            }
            if (pData.initiator.isActor() && IsGod(pData.initiator.a))
            {
                pData.initiator.a.data.set("AttackFromProjectile", pData.is_projectile);
            }
            if (!pTargetToCheck.isActor())
            {
                return true;
            }
            if (pTargetToCheck.a.hasTrait("God Of War") && Randy.randomChance(Chance("God Of War", "BlockAttack%")))
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
            if (pTargetToCheck.a.hasTrait("God Of Knowledge") && Randy.randomChance(Chance("God Of Knowledge", "EnemySwap%")))
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
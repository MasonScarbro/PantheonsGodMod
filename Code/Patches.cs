using ai.behaviours;
using HarmonyLib;
using SleekRender;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using static GodsAndPantheons.Traits;
//Harmony Patches
namespace GodsAndPantheons
{
    [HarmonyPatch(typeof(Actor), nameof(Actor.updateParallelChecks))]
    public class DontMoveIfPetrified
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            CodeMatcher Matcher = new CodeMatcher(instructions);
            Matcher.MatchForward(false, new CodeMatch[] { new CodeMatch(OpCodes.Ldfld, AccessTools.Field(typeof(ActorBase), "has_status_frozen")) });
            Matcher.RemoveInstruction();
            Matcher.Insert(new CodeInstruction[]
            {
                    new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(DontMoveIfPetrified), nameof(CantMove)))
            });
            return Matcher.Instructions();
        }
        public static bool CantMove(Actor a)
        {
            return a.has_status_frozen || a.hasStatus("Petrified");
        }
    }
    [HarmonyPatch(typeof(MapBox), nameof(MapBox.applyForce))]
    public class RandomForce
    {
        [HarmonyReversePatch]
        //pforce out here is useless! the radius can also be infinite unlike in the game (max in-game is 32)
        public static void CreateRandomForce(object instance, WorldTile pTile, int pRad = 10, float pSpeedForce = 1.5f, bool pForceOut = true, bool useOnNature = false, int pDamage = 0, string[] pIgnoreKingdoms = null, BaseSimObject pByWho = null, TerraformOptions pOptions = null)
        {
            IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                CodeMatcher Matcher = new CodeMatcher(instructions);
                Matcher.MatchForward(false, new CodeMatch[] { new CodeMatch(OpCodes.Ldarg_1) });
                int Pos = Matcher.Pos;
                Matcher.RemoveInstructions(26);
                Matcher.Insert(new CodeInstruction[]
                {
                    new CodeInstruction(OpCodes.Ldarg_0),
                    new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(MapBox), "_force_temp_actor_list")),
                    new CodeInstruction(OpCodes.Ldarg_S, (byte)1),
                    new CodeInstruction(OpCodes.Ldarg_S, (byte)2),
                    new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(RandomForce), nameof(FillListWithAllActors)))
                });
                Matcher.MatchForward(false, new CodeMatch[]
                {
                    new CodeMatch(OpCodes.Ldarg_S, (byte)4)
                });
                List<Label> label = Matcher.Instruction.labels;
                Matcher.RemoveInstruction();
                Matcher.Insert(new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(Toolbox), nameof(Toolbox.randomBool))) {labels = label});
                return Matcher.Instructions();
            }
        }
        //less optimized but allows infinite radius
        public static void FillListWithAllActors(List<Actor> pList, WorldTile Tile, int MaxDistance = 64)
        {
            foreach(Actor a in World.world.units)
            {
                if(Toolbox.DistTile(Tile, a.currentTile) < MaxDistance)
                {
                    pList.Add(a);
                }
            }
        }
    }
    [HarmonyPatch(typeof(Clan), nameof(Clan.createClan))]
    public class FixClanRaceError
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            CodeMatcher Matcher = new CodeMatcher(instructions);
            Matcher.MatchForward(false, new CodeMatch[]
            {
                new CodeMatch(new OpCode?(OpCodes.Ldfld), AccessTools.Field(typeof(ActorBase), nameof(ActorBase.race)))
            });
            Matcher.RemoveInstruction();
            Matcher.Insert(new CodeInstruction[]
            {
              new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(FixClanRaceError), nameof(GetTrueRace)))
            });
            return Matcher.Instructions();
        }
        public static Race GetTrueRace(Actor actor)
        {
            if(actor.asset.race != SK.dragons)
            {
                return actor.race;
            }
            actor.data.get("oldself", out string oldself, SA.unit_human);
            return AssetManager.raceLibrary.get(AssetManager.actor_library.get(oldself).race);
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
            if (!Toolbox.randomChance(0.002f))
            {
                return;
            }
            DivineMiracle(instance);
        }
        public static bool DivineMiracle(MapBox instance)
        {
            List<Kingdom> list = World.world.kingdoms.list_civs;
            List<string> availabletraits = getavailblegodtraits(instance);
            if (availabletraits.Count == 0)
            {
                return false;
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
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        static Actor LuckyOnee;
        static float blessingtime = 0;
        public static void DivineMiracle(Actor LuckyOne, string godtrait)
        {
            World.world.startShake(1f, 0.01f, 2f, true, true);
            LuckyOne.addTrait(godtrait);
            LuckyOne.equipment?.weapon?.emptySlot();
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
    public class jobapply
    {
        static void Postfix(ref string __result, ActorBase pActor)
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
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            CodeMatcher Matcher = new CodeMatcher(instructions);
            Matcher.MatchForward(false, new CodeMatch[]
            {
                new CodeMatch(OpCodes.Ldfld, AccessTools.Field(typeof(ActorAsset), "skipFightLogic"))
            });
            Matcher.Advance(1);
            Label CodeLabel = (Label)Matcher.Operand;
            Matcher.Advance(1);
            Matcher.Insert(new CodeInstruction[] {
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(BaseSimObject), "a")),
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(DontAttack), nameof(CanUseFightLogic))),
                new CodeInstruction(OpCodes.Brtrue_S, CodeLabel)
            });
            return Matcher.Instructions();
        }
        //add anything you want here
        static bool CanUseFightLogic(Actor Actor)
        {
            return Actor.asset.id == SA.dragon && Actor.hasTrait("God Of Fire");
        }
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
            if(__instance.hasTrait("God Of The Lich") && !isgod)
            {
                Actor actor = CopyActor(pDeadUnit, pDeadUnit.asset.zombieID);
                if (actor != null)
                {
                    TurnActorIntoSummonedOne(actor, __instance, 100);
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
                if(__instance.asset.id == "BrainWashed")
                {
                    FinishBrainWashing(__instance._simObject.a);
                }
                if(__instance.asset.id == "Levitating")
                {
                    Actor actor = __instance._simObject.a;
                    Actor Target = GetTargetToCrashLand(actor);
                    if (Target != null)
                    {
                        actor.forceVector.z = 0;
                        PushActorTowardsTile(Target.currentTile.pos, actor, 0.1f);
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
                if (pID == "BrainWashed")
                {
                    FinishBrainWashing(__instance.a);
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
            child.setStatsDirty();
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
            using ListPool<string> godtraits = new ListPool<string>(GetGodTraits(pParent1));
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
            AutoTrait(child, godtraits, true, chancemult);
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
    [HarmonyPatch(typeof(ActorBase), "updateStats")]
    public class AddCustomStats
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
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(AddCustomStats), nameof(mergedemistats)))
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
        static bool Prefix(AttackData pData, ref BaseSimObject pTargetToCheck)
        {
            if (pTargetToCheck.isActor() && pData.initiator != null)
            {
                if (pTargetToCheck.a.hasTrait("God Of War"))
                {
                    if (Toolbox.randomChance(GetEnhancedChance("God Of War", "BlockAttack%")))
                    {
                        MusicBox.playSound(MB.HitSwordSword, pTargetToCheck.currentTile);
                        if (Toolbox.DistTile(pTargetToCheck.currentTile, pData.initiator.currentTile) < 3)
                        {
                            pTargetToCheck = pData.initiator;
                            return true;
                        }
                        return false;
                    }
                }
                if (pTargetToCheck.a.hasTrait("God Of Knowledge"))
                {
                    if (Toolbox.randomChance(GetEnhancedChance("God Of Knowledge", "EnemySwap%")))
                    {
                        WorldTile tile = pTargetToCheck.currentTile;
                        using ListPool<BaseSimObject> enemies = EnemiesFinder.findEnemiesFrom(tile, pTargetToCheck.kingdom, -1).list;
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
                            EffectsLibrary.spawnAt("fx_teleport_blue", enemytoswap.currentTile.posV3, pTargetToCheck.stats[S.scale]);
                            pTargetToCheck.a.spawnOn(enemytoswap.currentTile);
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
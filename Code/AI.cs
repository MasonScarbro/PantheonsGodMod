using ai.behaviours;
using NeoModLoader.General;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static GodsAndPantheons.BehFunctions;
namespace GodsAndPantheons
{
    public class GodHunterBeh : BehaviourActionActor
    {
        public static void init()
        {
            BehaviourTaskActor GodHunt = new BehaviourTaskActor();
            GodHunt.id = "GodHunt";
            GodHunt.locale_key = "Hunting Gods";
            GodHunt.addBeh(new GodHunterBeh());
            GodHunt.addBeh(new BehGoToTileTarget() { walkOnBlocks = true, walkOnWater = true });
            AssetManager.tasks_actor.add(GodHunt);
            ActorJob GodHunterJob = new ActorJob
            {
                id = "GodHunter",
            };
            GodHunterJob.addTask("GodHunt");
            LM.AddToCurrentLocale("Hunting Gods", "Hunting Gods......");
            AssetManager.job_actor.add(GodHunterJob);
        }
        public override BehResult execute(Actor pActor)
        {
            if (pActor.has_attack_target)
            {
                return FleeIfInDanger(pActor);
            }
            if (!Main.savedSettings.HunterAssasins)
            {
                getrandomtile(ref pActor);
                return FleeIfInDanger(pActor);
            }
            Actor GodToHunt = Toolbox.getClosestActor(Traits.FindGods(pActor).ToList(), pActor.current_tile);
            if (GodToHunt == null)
            {
                getrandomtile(ref pActor);
                return FleeIfInDanger(pActor);
            }
            pActor.beh_tile_target = GodToHunt.current_tile;
            return Attack(pActor, GodToHunt);
        }
        public BehResult Attack(Actor pActor, Actor pTarget)
        {
            if (pTarget == null || !pActor.hasStatus("Invisible"))
            {
                return FleeIfInDanger(pActor);
            }
            if (Vector2.Distance(pTarget.current_position, pActor.current_position) > 8)
            {
                return FleeIfInDanger(pActor);
            }
            if (getalliesofactor(Finder.getUnitsFromChunk(pActor.current_tile, 1, 4), pTarget) > 7)
            {
                return FleeIfInDanger(pActor);
            }
            pActor.finishStatusEffect("Invisible");
            pActor.data.set("invisiblecooldown", 10);
            if (!pTarget.is_inside_building)
            {
                pActor.setAttackTarget(pTarget);
            }
            else
            {
                pActor.setAttackTarget(pTarget.inside_building);
            }
            return FleeIfInDanger(pActor);
        }
        public BehResult FleeIfInDanger(Actor pActor)
        {
            if (!(!pActor.hasStatus("Invisible") && pActor.data.health < pActor.getMaxHealth() * (pActor.hasStatus("powerup") ? 0.7 : 0.35)) || pActor.isInLiquid())
            {
                return BehResult.Continue;
            }
            pActor.addStatusEffect("caffeinated", 3);
            pActor.clearAttackTarget();
            return forceTaskImmediate(pActor, "run_away", false, true);
        }
    }
    public class SummonedOneBeh : BehaviourActionActor
    {
        public static void init()
        {
            BehaviourTaskActor SummonedOne = new BehaviourTaskActor();
            SummonedOne.id = "SummonedOneTask";
            SummonedOne.locale_key = "Minion";
            SummonedOne.addBeh(new SummonedOneBeh());
            SummonedOne.addBeh(new BehGoToTileTarget() { walkOnBlocks = false, walkOnWater = true });
            AssetManager.tasks_actor.add(SummonedOne);
            ActorJob SummonedOneJob = new ActorJob
            {
                id = "SummonedJob",
            };
            SummonedOneJob.addTask("SummonedOneTask");
            LM.AddToCurrentLocale("Minion", "Obeying My Master");
            AssetManager.job_actor.add(SummonedOneJob);
        }
        public bool CheckStatus(Actor pActor, out Actor Master)
        {
            Master = Traits.FindMaster(pActor);
            if (pActor.hasTrait("madness"))
            {
                pActor.data.setName("Corrupted One");
                pActor.data.removeString("Master");
                pActor.data.removeInt("life");
                pActor.data.removeInt("lifespan");
                pActor.removeTrait("Summoned One");
                return false;
            }
            if (Master == null)
            {
                pActor.die();
                return false;
            }
            return true;
        }
        public override BehResult execute(Actor pActor)
        {
            if (!CheckStatus(pActor, out Actor Master))
            {
                pActor?.finishAllStatusEffects();
                pActor?.clearBeh();
                return BehResult.Stop;
            }
            return Minion(pActor, Master);
        }
    }
    //basically the same as summoned one but checkstatus is different
    public class CorruptedOneBeh : BehaviourActionActor
    {
        public static void init()
        {
            BehaviourTaskActor BrainWashed = new BehaviourTaskActor();
            BrainWashed.id = "BrainWashedTask";
            BrainWashed.locale_key = "Minion";
            BrainWashed.addBeh(new CorruptedOneBeh());
            BrainWashed.addBeh(new BehGoToTileTarget() { walkOnBlocks = true, walkOnWater = true });
            AssetManager.tasks_actor.add(BrainWashed);
            ActorJob BrainWashedJob = new ActorJob
            {
                id = "BrainWashedJob",
            };
            BrainWashedJob.addTask("BrainWashedTask");
            AssetManager.job_actor.add(BrainWashedJob);
        }
        public bool CheckStatus(Actor pActor, out Actor Master)
        {
            Master = Traits.FindMaster(pActor);
            if (pActor.hasTrait("madness") || Master == null)
            {
                return false;
            }
            return true;
        }
        public override BehResult execute(Actor pActor)
        {
            if (!CheckStatus(pActor, out Actor Master))
            {
                pActor.finishStatusEffect("BrainWashed");
                pActor.clearBeh();
                return BehResult.Stop;
            }
            return Minion(pActor, Master);
        }
    }
    public class BehFunctions
    {
        public static WorldTile GetTileWithinDistance(WorldTile tile, float mindistance = 5, int maxdistance = 10, int attempts = 20, bool ignoreMountains = false)
        {
            if (tile.Type.liquid)
            {
                return null;
            }
            WorldTile _tile = null;
            for (int i = 0; i < attempts; i++)
            {
                _tile = Toolbox.getRandomTileWithinDistance(tile, maxdistance);
                if (Vector2.Distance(tile.pos, _tile.pos) > mindistance && tile.isSameIsland(_tile) && (!_tile.Type.mountains || ignoreMountains))
                {
                    return _tile;
                }
            }
            return _tile;
        }
        public static void getrandomtile(ref Actor pActor)
        {
            MapRegion mapRegion = pActor.current_tile.region;
            if (Randy.randomChance(0.65f) && mapRegion.tiles.Count > 0)
            {
                pActor.beh_tile_target = mapRegion.tiles.GetRandom();
            }
            if (mapRegion.neighbours.Count > 0 && Randy.randomBool())
            {
                mapRegion = mapRegion.neighbours.GetRandom();
            }
            if (mapRegion.tiles.Count > 0)
            {
                pActor.beh_tile_target = mapRegion.tiles.GetRandom();
            }
        }
        public static BehResult Minion(Actor pActor, Actor Master)
        {
            if (Master.kingdom.id != pActor.kingdom.id)
            {
                pActor.setKingdom(Master.kingdom);
            }
            if (pActor.has_attack_target)
            {
                return BehResult.Continue;
            }
            if (Master.has_attack_target)
            {
                pActor.beh_tile_target = Master.attack_target.current_tile;
                return BehResult.Continue;
            }
            pActor.beh_tile_target = GetTileWithinDistance(Master.current_tile);
            return BehResult.Continue;
        }
        public static int getalliesofactor(IEnumerable<BaseSimObject> actors, BaseSimObject actor)
        {
            int count = 0;
            foreach (BaseSimObject a in actors)
            {
                if (!a.kingdom.isEnemy(actor.kingdom))
                {
                    count++;
                }
            }
            return count;
        }
    }
}

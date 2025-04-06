using ai.behaviours;
using System.Collections.Generic;
using UnityEngine;

namespace GodsAndPantheons
{
    public class SummonedOneBeh : BehaviourActionActor
    {
        public static void init()
        {
            BehaviourTaskActor SummonedOne = new BehaviourTaskActor();
            SummonedOne.id = "SummonedOneTask";
            SummonedOne.addBeh(new SummonedOneBeh());
            SummonedOne.addBeh(new BehGoToTileTarget() { walkOnBlocks = true, walkOnWater = true });
            AssetManager.tasks_actor.add(SummonedOne);
            ActorJob SummonedOneJob = new ActorJob
            {
                id = "SummonedJob",
            };
            SummonedOneJob.addTask("SummonedOneTask");
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
            return BehFunctions.Minion(pActor, Master);
        }
    }
    //basically the same as summoned one but checkstatus is different
    public class CorruptedOneBeh : BehaviourActionActor
    {
        public static void init()
        {
            BehaviourTaskActor BrainWashed = new BehaviourTaskActor();
            BrainWashed.id = "BrainWashedTask";
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
            Master = Traits.FindBrainWasher(pActor);
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
            return BehFunctions.Minion(pActor, Master);
        }
    }
    public class BehFunctions
    {
        public static WorldTile gettilewithindistance(WorldTile tile, float mindistance = 5, int maxdistance = 10, int attempts = 20, bool ignoreMountains = false)
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
            pActor.beh_tile_target = gettilewithindistance(Master.current_tile);
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

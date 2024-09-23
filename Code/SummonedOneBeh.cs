using ai.behaviours;
using UnityEngine;

namespace GodsAndPantheons
{
    public class SummonedOneBeh
    {
        public static void init()
        {
            BehaviourTaskActor SummonedOne = new BehaviourTaskActor();
            SummonedOne.id = "SummonedOneTask";
            SummonedOne.addBeh(new Patrol());
            SummonedOne.addBeh(new BehGoToTileTarget() { walkOnBlocks = true, walkOnWater = true });
            AssetManager.tasks_actor.add(SummonedOne);
            ActorJob SummonedOneJob = new ActorJob
            {
                id = "SummonedJob",
            };
            SummonedOneJob.addTask("SummonedOneTask");
            AssetManager.job_actor.add(SummonedOneJob);
        }
    }
    public class Patrol : BehaviourActionActor
    {
        public static Actor? Master;
        public override BehResult execute(Actor pActor)
        {
            Master = Traits.FindMaster(pActor);
            if (Master == null)
            {
                pActor.removeTrait("Summoned One");
                pActor.data.removeString("Master");
                return BehResult.Stop;
            }
            if(Master.kingdom.id != pActor.kingdom.id)
            {
                pActor.setKingdom(Master.kingdom);
            }
            if (pActor.has_attack_target)
            {
                return BehResult.Continue;
            }
            if (Master.has_attack_target)
            {
                pActor.beh_tile_target = Master.currentTile;
                return BehResult.Continue;
            }
            pActor.beh_tile_target = BehFunctions.gettilewithindistance(Master.currentTile);
            return BehResult.Continue;
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
            MapRegion mapRegion = pActor.currentTile.region;
            if (Toolbox.randomChance(0.65f) && mapRegion.tiles.Count > 0)
            {
                pActor.beh_tile_target = mapRegion.tiles.GetRandom();
            }
            if (mapRegion.neighbours.Count > 0 && Toolbox.randomBool())
            {
                mapRegion = mapRegion.neighbours.GetRandom();
            }
            if (mapRegion.tiles.Count > 0)
            {
                pActor.beh_tile_target = mapRegion.tiles.GetRandom();
            }
        }
    }
}

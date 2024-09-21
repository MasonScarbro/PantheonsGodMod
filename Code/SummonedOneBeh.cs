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
            SummonedOne.addBeh(new BehDefendMaster());
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
            if (pActor.has_attack_target || Master.has_attack_target)
            {
                return BehResult.Continue;
            }
            if (Master.currentTile.Type.liquid)
            {
                GodHunt.getrandomtile(ref pActor);
                return BehResult.Continue;
            }
            if(Vector2.Distance(Master.currentPosition, pActor.currentPosition) > 50)
            {
                pActor.data.set("patrolling", false);
                pActor.beh_tile_target = Master.currentTile;
                return BehResult.Continue;
            }
            pActor.data.get("patrolling", out bool patrolling);
            if (!patrolling)
            {
                WorldTile tiletopatrol = getrilewithindistance(Master.currentTile);
                if (tiletopatrol == null)
                {
                    GodHunt.getrandomtile(ref pActor);
                }
                else
                {
                    pActor.data.set("oldx", tiletopatrol.x);
                    pActor.data.set("oldy", tiletopatrol.y);
                    pActor.beh_tile_target = tiletopatrol;
                    pActor.data.set("patrolling", true);
                }
            }
            else
            {
                pActor.data.get("oldx", out int oldx);
                pActor.data.get("oldy", out int oldy);
                WorldTile tiletopatrol = World.world.GetTileSimple(oldx, oldy);
                if(tiletopatrol == null)
                {
                    pActor.data.set("patrolling", false);
                    return BehResult.Continue;
                }
                pActor.beh_tile_target = tiletopatrol;
                if (Vector2.Distance(tiletopatrol.pos, pActor.currentPosition) < 5)
                {
                    pActor.data.set("patrolling", false);
                }
            }
            return BehResult.Continue;
        }
        WorldTile getrilewithindistance(WorldTile tile, float mindistance = 5, int maxdistance = 10, int attempts = 20, bool ignoreMountains = false)
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
    }

    public class BehDefendMaster : BehaviourActionActor
    {
        public override BehResult execute(Actor pActor)
        {
            if (pActor.has_attack_target)
            {
                return BehResult.Continue;
            }
            if(Patrol.Master.attackTarget != null)
            {
                pActor.beh_tile_target = Patrol.Master.attackTarget.currentTile;
                pActor.data.set("patrolling", false);
            }
            return BehResult.Continue;
        }
    }
}

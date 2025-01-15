using ai.behaviours;
using System.Collections.Generic;
using UnityEngine;
using static GodsAndPantheons.BehFunctions;
namespace GodsAndPantheons
{
    internal class GodHunterBeh
    {
        public static void init()
        {
            BehaviourTaskActor GodHunt = new BehaviourTaskActor();
            GodHunt.id = "GodHunt";
            GodHunt.addBeh(new GodHunt());
            GodHunt.addBeh(new BehAttackGod());
            GodHunt.addBeh(new escape());
            GodHunt.addBeh(new BehGoToTileTarget() { walkOnBlocks = false, walkOnWater = true });
            AssetManager.tasks_actor.add(GodHunt);
            ActorJob GodHunterob = new ActorJob
            {
                id = "GodHunter",
            };
            GodHunterob.addTask("GodHunt");
            AssetManager.job_actor.add(GodHunterob);
        }
    }
    public class escape : BehaviourActionActor
    {
        public override void create()
        {
            base.create();
        }

        public override BehResult execute(Actor pActor)
        {
            if(!(!pActor.hasStatus("Invisible") && pActor.data.health < pActor.getMaxHealth() * (pActor.hasStatus("powerup") ? 0.7 : 0.35)) || pActor._isInLiquid)
            {
                return BehResult.Continue;
            }
            pActor.addStatusEffect("caffeinated", 3);
            pActor.clearAttackTarget();
            WorldTile tile = GetTileToEscapeToo(pActor);
            if(tile != null)
            {
                pActor.beh_tile_target = tile;
                return BehResult.Continue;
            }
            getrandomtile(ref pActor);
            return BehResult.Continue;
        }
        WorldTile GetTileToEscapeToo(Actor pActor)
        {
            WorldTile tiletoescapeto = null;
            int countofenemies = 9999;
            World.world.getObjectsInChunks(pActor.currentTile, 30, MapObjectType.Actor);
            for (int i = 0; i < 15; i++)
            {
                WorldTile tile = gettilewithindistance(pActor.currentTile, 5, 20);
                if(tile == null)
                {
                    continue;
                }
                int count = getnearbyoppsofactor(World.world.temp_map_objects, pActor, tile);
                if(count == 0)
                {
                    return tile;
                }
                if(count < countofenemies)
                {
                    tiletoescapeto = tile;
                    countofenemies = count;
                }
            }
            return tiletoescapeto;
        }
        static int getnearbyoppsofactor(List<BaseSimObject> actors, Actor actor, WorldTile currentile, int distance = 10)
        {
            int count = 0;
            foreach(BaseSimObject a in actors)
            {
                if (a.canAttackTarget(actor) && Toolbox.DistTile(a.currentTile, currentile) < distance)
                {
                    count++;
                }
            }
            return count;
        }
    }
    public class GodHunt : BehaviourActionActor
    {
        public static Actor? GodToHunt;
        public override BehResult execute(Actor pActor)
        {
            GodToHunt = null;
            if (pActor.has_attack_target)
            {
                return BehResult.Continue;
            }
            if(!Main.savedSettings.HunterAssasins)
            {
                getrandomtile(ref pActor);
                return BehResult.Continue;
            }
            GodToHunt = Toolbox.getClosestActor(Traits.FindGods(pActor), pActor.currentTile);
            if (GodToHunt == null)
            {
                getrandomtile(ref pActor);
                return BehResult.Continue;
            }
            pActor.beh_tile_target = GodToHunt.currentTile;
            return BehResult.Continue;
        }
    }

    public class BehAttackGod : BehaviourActionActor
    {
        public override BehResult execute(Actor pActor)
        {
            if(GodHunt.GodToHunt == null || !pActor.hasStatus("Invisible"))
            {
                return BehResult.Continue;
            }
            if(Vector2.Distance(GodHunt.GodToHunt.currentPosition, pActor.currentPosition) > 8)
            {
                return BehResult.Continue;
            }
            World.world.getObjectsInChunks(pActor.currentTile, 4, MapObjectType.Actor);
            if (getalliesofactor(World.world.temp_map_objects, GodHunt.GodToHunt) > 7)
            {
                return BehResult.Continue;
            }
            pActor.finishStatusEffect("Invisible");
            pActor.data.set("invisiblecooldown", 10);
            if (!GodHunt.GodToHunt.is_inside_building)
            {
                pActor.setAttackTarget(GodHunt.GodToHunt);
            }
            else
            {
                pActor.setAttackTarget(GodHunt.GodToHunt.insideBuilding);
            }
            return BehResult.Continue;
        }
        static int getalliesofactor(List<BaseSimObject> actors, BaseSimObject actor)
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

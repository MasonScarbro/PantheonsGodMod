﻿using ai.behaviours;
using System.Collections.Generic;
using UnityEngine;

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
            GodHunt.addBeh(new BehGoToTileTarget() { walkOnBlocks = true, walkOnWater = true });
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
            if(!(!pActor.hasStatus("Invisible") && pActor.data.health < pActor.getMaxHealth() * (pActor.hasStatus("powerup") ? 0.5 : 0.25)) || pActor._isInLiquid)
            {
                return BehResult.Continue;
            }
            pActor.clearAttackTarget();
            pActor.data.get("oldx", out int oldx, -1);
            pActor.data.get("oldy", out int oldy);
            WorldTile tiletoecapeto;
            if (oldx == -1)
            {
                GetTileToEscapeToo(ref pActor);
                pActor.data.get("oldx", out oldx);
                pActor.data.get("oldy", out oldy);
            }
            tiletoecapeto = World.world.GetTileSimple(oldx, oldy);
            if (!tiletoecapeto.isSameIsland(pActor.currentTile))
            {
                tiletoecapeto = GetTileToEscapeToo(ref pActor);
            }
            if (Vector2.Distance(tiletoecapeto.pos, pActor.currentPosition) < 5)
            {
                pActor.data.set("invisiblecooldown", 0);
            }
            pActor.beh_tile_target = tiletoecapeto;
            return BehResult.Continue;
        }
        WorldTile GetTileToEscapeToo(ref Actor pActor)
        {
            WorldTile tiletoescapeto = null;
            for (int i = 0; i < 15; i++)
            {
                tiletoescapeto = pActor.currentTile.region.island.getRandomTile();
                if (Vector2.Distance(tiletoescapeto.pos, pActor.currentPosition) > 20)
                {
                    break;
                }
            }
            pActor.data.set("oldx", tiletoescapeto.x);
            pActor.data.set("oldy", tiletoescapeto.y);
            return tiletoescapeto;
        }
    }
    public class GodHunt : BehaviourActionActor
    {
        public static Actor? GodToHunt;
        public override BehResult execute(Actor pActor)
        {
            GodToHunt = Toolbox.getClosestActor(Traits.FindGods(pActor), pActor.currentTile);
            if (GodToHunt == null || !pActor.hasStatus("Invisible") || !Main.savedSettings.HunterAssasins)
            {
                GodToHunt = null;
                getrandomtile(ref pActor);
                return BehResult.Continue;
            }
            pActor.beh_tile_target = GodToHunt.currentTile;
            return BehResult.Continue;
        }
        public static void getrandomtile(ref Actor pActor)
        {
            MapRegion mapRegion = pActor.currentTile.region;
            if (Toolbox.randomChance(0.65f) && mapRegion.tiles.Count > 0)
            {
               pActor.beh_tile_target  = mapRegion.tiles.GetRandom();
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

    public class BehAttackGod : BehaviourActionActor
    {
        public override BehResult execute(Actor pActor)
        {
            if(GodHunt.GodToHunt == null)
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

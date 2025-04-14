using ai.behaviours;
using NeoModLoader.General;
using System.Linq;
using UnityEngine;
using static GodsAndPantheons.BehFunctions;
namespace GodsAndPantheons
{
    internal class GodHunterBeh : BehaviourActionActor
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
}

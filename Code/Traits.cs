/* 
AUTHOR: MASON SCARBRO
VERSION: 1.0.0
*/
using System;
using System.Threading;
using NCMS;
using UnityEngine;
using ReflectionUtility;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ai;
using HarmonyLib;
namespace GodsAndPantheons
{
    class Traits
    { 
        public static void init()
        {
		// copy and paste this into your repo
	    ActorTrait godofgods = new ActorTrait();
            godofgods.id = "God Of gods";
            godofgods.path_icon = "ui/icons/IconDemi";
            godofgods.base_stats[S.damage] += 200f;
            godofgods.base_stats[S.health] += 1000f;
            godofgods.base_stats[S.attack_speed] += 100f;
            godofgods.base_stats[S.critical_chance] += 50f;
            godofgods.base_stats[S.intelligence] += 40f;
            godofgods.base_stats[S.range] += 30f;
            godofgods.base_stats[S.dodge] += 35f;
            godofgods.base_stats[S.accuracy] += 15f;
            godofgods.base_stats[S.speed] += 30f;
            godofgods.base_stats[S.armor] += 60f;
            godofgods.action_death = new WorldAction(ActionLibrary.deathNuke);
            godofgods.action_special_effect = (WorldAction)Delegate.Combine(godofgods.action_special_effect, new WorldAction(GodOfGodsAutoTrait));
            godofgods.base_stats[S.scale] = 1f;
            godofgods.action_attack_target += new AttackAction(GodOfGodsAttack);
            AssetManager.traits.add(godofgods);
            PlayerConfig.unlockTrait(godofgods.id);
            addTraitToLocalizedLibrary(godofgods.id, "The god who rules among all");

            ActorTrait SummonedOne = new ActorTrait();
            SummonedOne.id = "Summoned One";
            SummonedOne.path_icon = "ui/icons/iconBlessing";
            SummonedOne.base_stats[S.damage] += 10;
            SummonedOne.base_stats[S.health] += 30;
            SummonedOne.base_stats[S.armor] += 10;
            SummonedOne.base_stats[S.knockback_reduction] += 0.5f;
            SummonedOne.base_stats[S.max_age] = -20000f;
            SummonedOne.action_special_effect += new WorldAction(SummonedBeing);
            SummonedOne.group_id = TraitGroup.special;
            SummonedOne.can_be_given = false;
            AssetManager.traits.add(SummonedOne);
            addTraitToLocalizedLibrary(SummonedOne.id, "A creature summoned by God himself in order to aid them in battle, DO NOT MODIFY THE NAME OF THIS CREATURE!");
	    //harmony class at the bottom of the file, this is to make it so summoned ones do not attack the god and his allies and vise versa
            var harmony = new Harmony("com.Gods.Pantheons");
            harmony.PatchAll();
        }
        public static bool SummonedBeing(BaseSimObject pTarget, WorldTile pTile)
        {
            Actor a = (Actor)pTarget;
            int life;
            a.data.get("lifespan", out life);
            a.data.set("lifespan", life + 1);
            if (life + 1 > 21f)
            {
                a.killHimself(false, AttackType.Age, false, true, true);
            }
            return true;
        }
	    
        public static bool GodOfGodsAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
    {
    PowerLibrary pb = new PowerLibrary();
    Actor self = (Actor)pSelf;
    if (pTarget != null)
    {
        Actor a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;
        Vector2Int pos = pTile.pos; // Position of the Ptile as a Vector 2
        float pDist = Vector2.Distance(pTarget.currentPosition, pos); // the distance between the target and the pTile
        if (Toolbox.randomChance(0.06f))
        {
            ActionLibrary.addFrozenEffectOnTarget(null, pTarget, null); // freezezz the target
        }else
        if (Toolbox.randomChance(0.04f))
        {
            EffectsLibrary.spawn("fx_meteorite", pTarget.currentTile, "meteorite_disaster", null, 0f, -1f, -1f);    //spawn 1 meteorite
            pSelf.a.addStatusEffect("invincible", 1f);
        }
        else if (Toolbox.randomChance(0.09f))
        {
            ActionLibrary.castTornado(pSelf, pTarget, pTile);
        }
        if (Toolbox.randomChance(0.06f))
        {
            Summon(SA.demon, 1, self, pTile);
        }
        else if (Toolbox.randomChance(0.07f))
        {
            Summon(SA.evilMage, 1, self, pTile);
        }
        else if (Toolbox.randomChance(0.08f))
        {
            Summon(SA.skeleton, 3, self, pTile);
        }
        if(Toolbox.randomChance(0.1f))
        {
            ActionLibrary.castLightning(null, pTarget, null);
        }
        else
        if (Toolbox.randomChance(0.08f))
        {
            EffectsLibrary.spawn("fx_explosion_middle", pTarget.a.currentTile, null, null, 0f, -1f, -1f);
            pSelf.a.addStatusEffect("invincible", 1f);
        }else
        if (Toolbox.randomChance(0.2f))
        {
            // randomly spawns a flash of fire or acid on the tile 
            MapBox.instance.dropManager.spawn(pTile, "fire", 5f, -1f);
            MapBox.instance.dropManager.spawn(pTile, "acid", 5f, -1f);
            MapBox.instance.dropManager.spawn(pTile, "fire", 5f, -1f); // Drops fire from distance 5 with scale of one at current tile
        }else if (Toolbox.randomChance(0.03f))
        {
            Vector3 newPoint = Toolbox.getNewPoint(pSelf.currentPosition.x, pSelf.currentPosition.y, (float)pos.x, (float)pos.y, pDist, true); // the Point of the projectile launcher 
            Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pTarget.a.stats[S.size], true);
            EffectsLibrary.spawnProjectile("lightBallzProjectiles", newPoint, newPoint2, 0.0f);
        }else if (Toolbox.randomChance(0.03f))
        {
            Vector3 newPoint = Toolbox.getNewPoint(pSelf.currentPosition.x + 35f, pSelf.currentPosition.y + 95f, (float)pos.x + 1f, (float)pos.y + 1f, pDist, true); // the Point of the projectile launcher 
            Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pTarget.a.stats[S.size], true);
            EffectsLibrary.spawnProjectile("moonFall", newPoint, newPoint2, 0.0f);
            pSelf.a.addStatusEffect("invincible", 2f);
        }
        else if (Toolbox.randomChance(0.04f))
        {
            pb.spawnEarthquake(pTarget.a.currentTile, null);
        }


        return true;
    }
    return false;
 }
public static void Summon(string creature, int times, Actor self, WorldTile Ptile)
{
    for (int i = 0; i < times; i++)
    {
        Actor actor = World.world.units.spawnNewUnit(creature, Ptile, true, 3f);
        actor.data.name = $"Summoned by {self.getName()}";
        actor.addTrait("Summoned One");
        actor.addTrait("regeneration");
        actor.removeTrait("immortal");
        actor.data.set("lifespan", 0);

    }

}
         public static void addTraitToLocalizedLibrary(string id, string description)        
         {

            string language = Reflection.GetField(LocalizedTextManager.instance.GetType(), LocalizedTextManager.instance, "language") as string;
      		Dictionary<string, string> localizedText = Reflection.GetField(LocalizedTextManager.instance.GetType(), LocalizedTextManager.instance, "localizedText") as Dictionary<string, string>;
      		localizedText.Add("trait_" + id, id);
      		localizedText.Add("trait_" + id + "_info", description);

        }
    }
    [HarmonyPatch(typeof(BaseSimObject), "canAttackTarget")]
    public class UpdateAttacking
    {
        static bool Prefix(ref bool __result, BaseSimObject __instance, BaseSimObject pTarget)
        {
            if(__instance == pTarget)
            {
                __result = false;
                return false;
            }
            if (__instance.isActor() && pTarget.isActor())
            {
                Actor a = (Actor)__instance;
                Actor b = (Actor)pTarget;
                if (a.hasTrait("God Of gods"))
                {
                    if (b.getName().Equals($"Summoned by {a.getName()}"))
                    {
                        __result = false;
                        return false;
                    }
                }
                else if (a.hasTrait("Summoned One"))
                {
                    List<Actor> simpleList = World.world.units.getSimpleList();
                    foreach (Actor actor in simpleList)
                    {
                        if (a.getName().Equals($"Summoned by {actor.getName()}") && actor.hasTrait("God Of gods"))
                        {
                            if (!actor.canAttackTarget(b))
                            {
                                __result = false;
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
    }

}


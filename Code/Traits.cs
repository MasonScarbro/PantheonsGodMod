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
using NCMS.Utils;
using static UnityEngine.GraphicsBuffer;
using System.Reflection;


namespace GodsAndPantheons
{
    class Traits
    {

        //Knowledge Gods Chances
        public static float knowledgeGodPwrChance1 = 20f;
        public static float knowledgeGodPwrChance2 = 1f;
        public static float knowledgeGodPwrChance3 = 1f;
        public static float knowledgeGodPwrChance4 = 5f;
        public static float knowledgeGodPwrChance5 = 4f;
        public static float knowledgeGodPwrChance6 = 2f;
        public static float knowledgeGodPwrChance7 = 0.5f;
        public static float knowledgeGodPwrChance8 = 1f;
        public static float knowledgeGodPwrChance9 = 9f;
        // Moon Gods Power Chances
        public static float starGodPwrChance1 = 0.05f;
        public static float starGodPwrChance2 = 1f;
        public static float starGodPwrChance3 = 0.5f;
        // NightGods Power Chances
        public static float darkGodPwrChance1 = 0.01f;
        public static float darkGodPwrChance2 = 0.1f;
        public static float darkGodPwrChance3 = 4f;
        public static float darkGodPwrChance4 = 1f;
        // Sun God Chances
        public static float sunGodPwrChance1 = 0.01f;
        public static float sunGodPwrChance2 = 8f;
        public static float sunGodPwrChance3 = 2f;
        public static float sunGodPwrChance4 = 0.5f;
        public static float sunGodPwrChance5 = 0.01f;
        // War God Chances
        public static float warGodPwrChance1 = 1f;
        public static float warGodPwrChance2 = 3f;
        public static float warGodPwrChance3 = 2f;
        // Earth God Chances
        public static float earthGodPwrChance1 = 5f;
        public static float earthGodPwrChance2 = 1f;
        public static float earthGodPwrChance3 = 1f;
        // Lich God Chances
        public static float lichGodPwrChance1 = 1f;
        //god of Gods Chances
        public static float GodOfGodsPwrChance1 = 5f;
        public static float GodOfGodsPwrChance2 = 10f;
        public static float GodOfGodsPwrChance3 = 8f;


        public static void init()
        {

            /* The Clans could be the way to control inheritence and assign the lesser god or demi god trait */


            ActorTrait chaosGod = new ActorTrait();
            chaosGod.id = "God_Of_Chaos";
            chaosGod.path_icon = "ui/icons/chaosGod";
            chaosGod.base_stats[S.damage] += 30f;
            chaosGod.base_stats[S.health] += 800;
            chaosGod.base_stats[S.attack_speed] += 5f;
            chaosGod.base_stats[S.attack_speed] += 10f;
            chaosGod.base_stats[S.critical_chance] += 0.05f;
            chaosGod.base_stats[S.range] += 8f;
            chaosGod.action_special_effect = new WorldAction(GodWeaponManager.godGiveWeapon);
            chaosGod.action_attack_target = new AttackAction(ActionLibrary.addBurningEffectOnTarget);
            chaosGod.action_attack_target = new AttackAction(chaosGodAttack);
            //chaosGod.action_death = new WorldAction(ActionLibrary.turnIntoDemon);
            chaosGod.action_death = (WorldAction)Delegate.Combine(chaosGod.action_death, new WorldAction(chaosGodsTrick));
            chaosGod.base_stats[S.scale] = 0.08f;
            chaosGod.group_id = "GodTraits";
            AddTrait(chaosGod, "Tis's The God Of Chaos!");


            ActorTrait sunGod = new ActorTrait();
            sunGod.id = "God Of light";
            sunGod.path_icon = "ui/icons/lightGod";
            sunGod.base_stats[S.damage] += 20f;
            sunGod.base_stats[S.health] += 500;
            sunGod.base_stats[S.attack_speed] += 100f;
            sunGod.base_stats[S.critical_chance] += 0.05f;
            sunGod.base_stats[S.speed] += 90f;
            sunGod.base_stats[S.dodge] += 80f;
            sunGod.base_stats[S.accuracy] += 10f;
            sunGod.base_stats[S.range] += 5f;
            sunGod.action_special_effect = new WorldAction(GodWeaponManager.godGiveWeapon);
            sunGod.action_special_effect = (WorldAction)Delegate.Combine(sunGod.action_special_effect, new WorldAction(sunGodAutoTrait));
            sunGod.action_attack_target = new AttackAction(ActionLibrary.addBurningEffectOnTarget);
            sunGod.action_attack_target = new AttackAction(ActionLibrary.addSlowEffectOnTarget);
            sunGod.action_attack_target = new AttackAction(sunGodAttack);
            sunGod.action_death = (WorldAction)Delegate.Combine(sunGod.action_death, new WorldAction(sunGodsDeath));
            sunGod.action_special_effect = (WorldAction)Delegate.Combine(sunGod.action_special_effect, new WorldAction(sunGodEraStatus));
            sunGod.group_id = "GodTraits";
            AddTrait(sunGod, "The God Of light, controls the very light that shines and can pass through with great speed");


            ActorTrait darkGod = new ActorTrait();
            darkGod.id = "God Of the Night";
            darkGod.path_icon = "ui/icons/godDark";
            darkGod.base_stats[S.damage] += 20f;
            darkGod.base_stats[S.health] += 550;
            darkGod.base_stats[S.attack_speed] += 3f;
            darkGod.base_stats[S.critical_chance] += 0.25f;
            darkGod.base_stats[S.scale] = 0.02f;
            darkGod.base_stats[S.dodge] += 60f;
            darkGod.base_stats[S.range] += 6f;
            darkGod.action_special_effect = new WorldAction(GodWeaponManager.godGiveWeapon);
            darkGod.action_attack_target = new AttackAction(darkGodAttack);
            darkGod.action_death = (WorldAction)Delegate.Combine(darkGod.action_death, new WorldAction(darkGodsDeath));
            darkGod.action_special_effect = (WorldAction)Delegate.Combine(darkGod.action_special_effect, new WorldAction(darkGodEraStatus));
            darkGod.action_special_effect = (WorldAction)Delegate.Combine(darkGod.action_special_effect, new WorldAction(darkGodAutoTrait));
            darkGod.group_id = "GodTraits";
            AddTrait(darkGod, "The God Of darkness, thievery and the shadows of which is his domain ");

            ActorTrait knowledgeGod = new ActorTrait();
            knowledgeGod.id = "God Of Knowledge";
            knowledgeGod.path_icon = "ui/icons/knowledgeGod";
            knowledgeGod.base_stats[S.damage] += 20f;
            knowledgeGod.base_stats[S.health] += 600;
            knowledgeGod.base_stats[S.attack_speed] += 1f;
            knowledgeGod.base_stats[S.critical_chance] += 0.25f;
            knowledgeGod.base_stats[S.range] += 15f;
            knowledgeGod.base_stats[S.scale] = 0.04f;
            knowledgeGod.base_stats[S.intelligence] += 35f;
            knowledgeGod.base_stats[S.accuracy] += 10f;
            knowledgeGod.action_special_effect = new WorldAction(GodWeaponManager.godGiveWeapon);
            knowledgeGod.action_death = (WorldAction)Delegate.Combine(knowledgeGod.action_death, new WorldAction(genericGodsDeath));
            knowledgeGod.action_attack_target = new AttackAction(knowledgeGodAttack);
            knowledgeGod.action_special_effect = (WorldAction)Delegate.Combine(knowledgeGod.action_special_effect, new WorldAction(knowledgeGodEraStatus));
            knowledgeGod.action_special_effect = (WorldAction)Delegate.Combine(knowledgeGod.action_special_effect, new WorldAction(knowledgeGodAutoTrait));
            knowledgeGod.group_id = "GodTraits";
            AddTrait(knowledgeGod, "The God Of Knowledge, His mind excedes Time he knows all");

            ActorTrait starsGod = new ActorTrait();
            starsGod.id = "God Of the Stars";
            starsGod.path_icon = "ui/icons/starsGod";
            starsGod.base_stats[S.damage] += 25f;
            starsGod.base_stats[S.health] += 600;
            starsGod.base_stats[S.attack_speed] += 1f;
            starsGod.base_stats[S.critical_chance] += 0.05f;
            starsGod.base_stats[S.scale] = 0.02f;
            starsGod.base_stats[S.range] += 15f;
            starsGod.base_stats[S.intelligence] += 3f;
            starsGod.action_special_effect = new WorldAction(GodWeaponManager.godGiveWeapon);
            starsGod.action_death = (WorldAction)Delegate.Combine(starsGod.action_death, new WorldAction(starsGodsDeath));
            starsGod.action_attack_target = new AttackAction(ActionLibrary.addFrozenEffectOnTarget);
            starsGod.action_attack_target += new AttackAction(starsGodAttack);
            starsGod.action_special_effect = (WorldAction)Delegate.Combine(starsGod.action_special_effect, new WorldAction(starsGodEraStatus));
            starsGod.group_id = "GodTraits";
            starsGod.action_special_effect = (WorldAction)Delegate.Combine(starsGod.action_special_effect, new WorldAction(starsGodAutoTrait));
            AddTrait(starsGod, "Now Cometh the Age of stars, A Thousand Year Voyage under the wisdom of the moon");

            ActorTrait earthGod = new ActorTrait();
            earthGod.id = "God Of the Earth";
            earthGod.path_icon = "ui/icons/earthGod";
            earthGod.base_stats[S.damage] += 40f;
            earthGod.base_stats[S.health] += 1000;
            earthGod.base_stats[S.attack_speed] += 1f;
            earthGod.base_stats[S.armor] += 30f;
            earthGod.base_stats[S.scale] = 0.1f;
            earthGod.base_stats[S.range] += 10f;
            earthGod.base_stats[S.intelligence] += 3f;
            earthGod.action_attack_target = new AttackAction(earthGodAttack);
            earthGod.group_id = "GodTraits";
            earthGod.action_special_effect = new WorldAction(earthGodBuildWorld);
            earthGod.action_special_effect += new WorldAction(GodWeaponManager.godGiveWeapon);
            earthGod.action_special_effect = (WorldAction)Delegate.Combine(earthGod.action_special_effect, new WorldAction(earthGodAutoTrait));
            AddTrait(earthGod, "God of the Natural Enviornment, The titan of creation");

            ActorTrait subGod = new ActorTrait();
            subGod.id = "LesserGod";
            subGod.path_icon = "ui/icons/subGod";
            subGod.base_stats[S.damage] += 5f;
            subGod.base_stats[S.health] += 400;
            subGod.base_stats[S.attack_speed] += 1f;
            subGod.base_stats[S.scale] = 0.02f;
            subGod.base_stats[S.critical_chance] += 0.05f;
            subGod.group_id = "GodTraits";
            subGod.action_special_effect = (WorldAction)Delegate.Combine(subGod.action_special_effect, new WorldAction(godKillerAutoTrait));
            AddTrait(subGod, "These Are the gods that have smaller importance");

            ActorTrait warGod = new ActorTrait();
            warGod.id = "God Of War";
            warGod.path_icon = "ui/icons/warGod";
            warGod.base_stats[S.damage] += 100f;
            warGod.base_stats[S.health] += 700;
            warGod.base_stats[S.attack_speed] += 35f;
            warGod.base_stats[S.armor] += 20f;
            warGod.base_stats[S.knockback_reduction] += 0.5f;
            warGod.base_stats[S.scale] = 0.03f;
            warGod.base_stats[S.range] += 8f;
            warGod.base_stats[S.warfare] += 40f;
            warGod.action_death = (WorldAction)Delegate.Combine(warGod.action_death, new WorldAction(genericGodsDeath));
            warGod.action_attack_target = new AttackAction(warGodAttack);
            warGod.action_special_effect = new WorldAction(GodWeaponManager.godGiveWeapon);
            warGod.action_special_effect = (WorldAction)Delegate.Combine(warGod.action_special_effect, new WorldAction(warGodAutoTrait));
            warGod.action_special_effect = (WorldAction)Delegate.Combine(warGod.action_special_effect, new WorldAction(warGodSeeds));
            warGod.group_id = "GodTraits";
            AddTrait(warGod, "God of Conflict, Bravery, Ambition, Many spheres of domain lie with him");


            ActorTrait lichGod = new ActorTrait();
            lichGod.id = "God Of The Lich";
            lichGod.path_icon = "ui/icons/lichGod";
            lichGod.base_stats[S.damage] += 100f;
            lichGod.base_stats[S.health] += 700;
            lichGod.base_stats[S.attack_speed] += 35f;
            lichGod.base_stats[S.armor] += 20f;
            lichGod.base_stats[S.knockback_reduction] += 0.5f;
            lichGod.base_stats[S.scale] = 0.03f;
            lichGod.base_stats[S.range] += 8f;
            lichGod.base_stats[S.warfare] += 40f;
            lichGod.action_death = (WorldAction)Delegate.Combine(lichGod.action_death, new WorldAction(genericGodsDeath));
            lichGod.action_attack_target = new AttackAction(lichGodAttack);
            lichGod.action_special_effect = new WorldAction(GodWeaponManager.godGiveWeapon);
            lichGod.action_special_effect = (WorldAction)Delegate.Combine(lichGod.action_special_effect, new WorldAction(lichGodAutoTrait));
            lichGod.group_id = "GodTraits";
            
            AddTrait(lichGod, "God of Dead Souls, Corruption, and Rot, Many spheres of domain lie with him");

            ActorTrait godKiller = new ActorTrait();
            godKiller.id = "God Killer";
            godKiller.path_icon = "ui/icons/godKiller";
            godKiller.base_stats[S.damage] += 10f;
            godKiller.base_stats[S.health] += 100;
            godKiller.base_stats[S.attack_speed] += 15f;
            godKiller.base_stats[S.armor] += 5f;
            godKiller.base_stats[S.knockback_reduction] += 0.1f;
            godKiller.base_stats[S.scale] = 0.01f;
            godKiller.base_stats[S.range] += 4f;
            godKiller.base_stats[S.warfare] += 4f;
            godKiller.action_special_effect = (WorldAction)Delegate.Combine(godKiller.action_special_effect, new WorldAction(godKillerAutoTrait));
            godKiller.group_id = "GodTraits";
            AddTrait(godKiller, "To Kill a God is nearly to become one");


            ActorTrait godHunter = new ActorTrait();
            godHunter.id = "God Hunter";
            godHunter.path_icon = "ui/icons/godKiller";
            godHunter.base_stats[S.damage] += 0;
            godHunter.base_stats[S.health] += 0;
	        godHunter.action_special_effect = new WorldAction(SuperRegeneration);
            godHunter.action_special_effect = (WorldAction)Delegate.Combine(godHunter.action_special_effect, new WorldAction(GodWeaponManager.godGiveWeapon));
            godHunter.action_death = (WorldAction)Delegate.Combine(godHunter.action_death, new WorldAction(godHunterDeath));
            godHunter.action_special_effect = (WorldAction)Delegate.Combine(godHunter.action_special_effect, new WorldAction(godKillerAutoTrait));
            godHunter.action_special_effect = (WorldAction)Delegate.Combine(godHunter.action_special_effect, new WorldAction(ChaseGod));
            godHunter.group_id = "GodTraits";
            AddTrait(godHunter, "He will stop at NOTHING to kill a god");
            //my traits
            ActorTrait godofgods = new ActorTrait();
            godofgods.id = "God Of gods";
            godofgods.path_icon = "ui/icons/IconDemi";
            godofgods.base_stats[S.damage] += 200;
            godofgods.base_stats[S.health] += 1000;
            godofgods.base_stats[S.attack_speed] += 60f;
            godofgods.base_stats[S.critical_chance] += 0.5f;
            godofgods.base_stats[S.intelligence] += 40f;
            godofgods.base_stats[S.range] += 20f;
            godofgods.base_stats[S.dodge] += 35f;
            godofgods.base_stats[S.accuracy] += 15f;
            godofgods.base_stats[S.speed] += 30f;
            godofgods.base_stats[S.armor] += 50f;
            godofgods.action_death = new WorldAction(ActionLibrary.deathNuke);
	        godofgods.action_death = (WorldAction)Delegate.Combine(godofgods.action_death, new WorldAction(genericGodsDeath));
            godofgods.action_death = (WorldAction)Delegate.Combine(godofgods.action_death, new WorldAction(genericGodsDeath));
            godofgods.action_special_effect = (WorldAction)Delegate.Combine(godofgods.action_special_effect, new WorldAction(GodOfGodsAutoTrait));
            godofgods.action_special_effect = (WorldAction)Delegate.Combine(godofgods.action_special_effect, new WorldAction(BringMinions));
            godofgods.action_special_effect = (WorldAction)Delegate.Combine(godofgods.action_special_effect, new WorldAction(GodOfGodsEraStatus));
            godofgods.base_stats[S.scale] = 0.075f;
            godofgods.action_attack_target += new AttackAction(GodOfGodsAttack);
            godofgods.group_id = "GodTraits";
            AddTrait(godofgods, "The god who rules among all");
            ActorTrait SummonedOne = new ActorTrait();
            SummonedOne.id = "Summoned One";
            SummonedOne.path_icon = "ui/icons/iconBlessing";
            SummonedOne.base_stats[S.damage] += 10;
            SummonedOne.base_stats[S.health] += 20;
            SummonedOne.base_stats[S.armor] += 10;
            SummonedOne.base_stats[S.knockback_reduction] += 0.5f;
            SummonedOne.base_stats[S.max_age] = -20000f;
            SummonedOne.action_special_effect += new WorldAction(SummonedBeing);
            SummonedOne.group_id = TraitGroup.special;
            SummonedOne.can_be_given = false;
            SummonedOne.action_special_effect = (WorldAction)Delegate.Combine(SummonedOne.action_special_effect, new WorldAction(GodOfGodsEraStatus));
            AddTrait(SummonedOne, "A creature summoned by God himself in order to aid them in battle, DO NOT MODIFY THE NAME OF THIS CREATURE!");
            
            //this to make it so summoned ones dont fight their Master and his allies
            var harmony = new Harmony("com.Gods.Pantheons");
            harmony.PatchAll();
        }
        //to make summoned ones only live for like 30 secounds
        public static bool SummonedBeing(BaseSimObject pTarget, WorldTile pTile)
        {
            Actor a = (Actor)pTarget;
            int life;
            int lifespan;
            a.data.get("lifespan", out lifespan);
            a.data.get("life", out life);
            a.data.set("life", life + 1);
            if (life + 1 > lifespan)
            {
                a.killHimself(false, AttackType.Age, false, true, true);
            }
            return true;
        }
        //if god is too far away the god hunter will teleport to them
        public static bool ChaseGod(BaseSimObject pTarget, WorldTile pTile)
        {
            if (pTarget.isActor())
            {
                BaseSimObject? a = Reflection.GetField(typeof(ActorBase), pTarget, "attackTarget") as BaseSimObject;
                if (a != null)
                {
                    if (Traits.IsGod(a.a))
                    {
                        float pDist = Vector2.Distance(pTarget.currentPosition, a.currentPosition);
                        if (pDist > 30)
                        {
                            EffectsLibrary.spawnAt("fx_teleport_blue", a.currentPosition, pTarget.stats[S.scale]);
                            SuperRegeneration(pTarget, pTile);
                            pTarget.a.spawnOn(a.currentTile, 0f);
                        }
                    }
                }
            }
                    return true;
        }
        public static void AddTrait(ActorTrait Trait, string disc)
        {
            AssetManager.traits.add(Trait);
            PlayerConfig.unlockTrait(Trait.id);
            addTraitToLocalizedLibrary(Trait.id, disc);
        }
        public static bool BringMinions(BaseSimObject pTarget, WorldTile pTile)
        {
            Actor b = (Actor)pTarget;
            List<Actor> Minions = GetMinions(b);
            foreach(Actor a in Minions){
                float pDist = Vector2.Distance(pTarget.currentPosition, a.currentPosition);
                if(pDist > 50){
                    EffectsLibrary.spawnAt("fx_teleport_blue", pTarget.currentPosition, a.stats[S.scale]);
                    a.spawnOn(pTarget.currentTile, 0f);
                }
            }
            return true;
        }
	public static bool SuperRegeneration(BaseSimObject pTarget, WorldTile pTile)
        {
		if(Toolbox.randomChance(0.1f)){
			pTarget.a.restoreHealth((int)(pTarget.a.getMaxHealth() * 0.05f));
		}
		   return true;
	}
        public static List<Actor> GetMinions(Actor a){
            List<Actor> MyMinions = new List<Actor>();
            List<Actor> simpleList = World.world.units.getSimpleList();
                 foreach (Actor actor in simpleList)
                  {
                   if (actor.getName().Equals($"Summoned by {a.getName()}") && actor.hasTrait("Summoned One"))
                  {
                         MyMinions.Add(actor);
                    }
               }
              return MyMinions;
        }
            public static bool IsGod(Actor a){
            return a.hasTrait("God Of The Lich")
            || a.hasTrait("God Of The Stars")
            || a.hasTrait("God Of Knowledge")
            || a.hasTrait("God Of The Night")
            || a.hasTrait("God_Of_Chaos")
            || a.hasTrait("God Of War")
            || a.hasTrait("God Of the Earth")
            || a.hasTrait("God Of light")
            || a.hasTrait("God Of gods")
            || a.hasTrait("LesserGod");
         }
        //god of gods attack
        public static bool GodOfGodsAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            PowerLibrary pb = new PowerLibrary();
            Actor self = (Actor)pSelf;
            if (pTarget != null)
            {
                Vector2Int pos = pTile.pos; // Position of the Ptile as a Vector 2
                float pDist = Vector2.Distance(pTarget.currentPosition, pos); // the distance between the target and the pTile
                if(Toolbox.randomChance(GodOfGodsPwrChance1/100)){
			int decider = Toolbox.randomInt(1, 4);
		switch(decider){	
			case 1: ActionLibrary.castLightning(null, pTarget, null); break;
			case 2: EffectsLibrary.spawn("fx_meteorite", pTarget.currentTile, "meteorite_disaster", null, 0f, -1f, -1f); pSelf.a.addStatusEffect("invincible", 1f); break;
			case 3: ActionLibrary.castTornado(pSelf, pTarget, pTile); break;
			case 4: pb.spawnEarthquake(pTarget.a.currentTile, null); break;
                }
		}
                if(Toolbox.randomChance(GodOfGodsPwrChance2 / 100)){
			int decider = Toolbox.randomInt(1, 3);
                switch(decider){	
			case 1: Summon(SA.demon, 1, self, pTile); break;
			case 2: Summon(SA.evilMage, 1, self, pTile); break;
			case 3: Summon(SA.skeleton, 3, self, pTile); break;
                }
                }
                if(Toolbox.randomChance(GodOfGodsPwrChance3 / 100)){
                int decider = Toolbox.randomInt(1, 5);
                switch(decider){	
		    case 1: ActionLibrary.addFrozenEffectOnTarget(null, pTarget, null); break;
				
		    case 2: EffectsLibrary.spawn("fx_explosion_middle", pTarget.a.currentTile, null, null, 0f, -1f, -1f);
                    pSelf.a.addStatusEffect("invincible", 1f); break;

                    // randomly spawns a flash of fire or acid on the tile 
		    case 3: MapBox.instance.dropManager.spawn(pTile, "fire", 5f, -1f);
                    MapBox.instance.dropManager.spawn(pTile, "acid", 5f, -1f);
                    MapBox.instance.dropManager.spawn(pTile, "fire", 5f, -1f); break;

		    case 4: {Vector3 newPoint = Toolbox.getNewPoint(pSelf.currentPosition.x, pSelf.currentPosition.y, (float)pos.x, (float)pos.y, pDist, true); // the Point of the projectile launcher 
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pTarget.a.stats[S.size], true);
                    EffectsLibrary.spawnProjectile("lightBallzProjectiles", newPoint, newPoint2, 0.0f); break;}

		    case 5: {Vector3 newPoint = Toolbox.getNewPoint(pSelf.currentPosition.x + 35f, pSelf.currentPosition.y + 95f, (float)pos.x + 1f, (float)pos.y + 1f, pDist, true); // the Point of the projectile launcher 
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pTarget.a.stats[S.size], true);
                    EffectsLibrary.spawnProjectile("moonFall", newPoint, newPoint2, 0.0f);
                    pSelf.a.addStatusEffect("invincible", 2f); break;}
                }
                }

                return true;
            }
            return false;
         }
        //summon ability
        public static void Summon(string creature, int times, Actor self, WorldTile Ptile)
        {
            for (int i = 0; i < times; i++)
            {
                Actor actor = World.world.units.spawnNewUnit(creature, Ptile, true, 3f);
                actor.data.name = $"Summoned by {self.getName()}";
                actor.addTrait("Summoned One");
                actor.addTrait("regeneration");
                actor.addTrait("madness");
                actor.addTrait("fire_proof");
                actor.addTrait("acid_proof");
                actor.removeTrait("immortal");
                actor.data.set("life", 0);
                actor.data.set("lifespan", 31);
            }

        }
        public static bool GodOfGodsAutoTrait(BaseSimObject pTarget, WorldTile pTile)
        {
            if (pTarget.a != null)
            {
                if (pTarget.a.hasTrait("God Of gods"))
                {
                    pTarget.a.addTrait("blessed");
                    pTarget.a.addTrait("poison_immune");
                    pTarget.a.addTrait("fire_proof");
                    pTarget.a.addTrait("acid_Proof");
                    pTarget.a.addTrait("freeze_proof");
                    pTarget.a.addTrait("shiny");
                    pTarget.a.addTrait("energized");
                    pTarget.a.addTrait("immortal");
                    pTarget.a.addTrait("nightchild");
                    pTarget.a.addTrait("moonchild");
                    pTarget.a.addTrait("regeneration");
                }


            }
            return true;
        }
        
        public static bool chaosGodsTrick(BaseSimObject pSelf, WorldTile pTile = null)
        {
            Actor pActor = (Actor)pSelf;

            if(Main.savedSettings.deathera)
              World.world.eraManager.setEra(S.age_chaos, true);
            pActor.removeTrait("God_Of_Chaos");


            return true;

        }

        public static bool sunGodsDeath(BaseSimObject pTarget, WorldTile pTile = null)
        {

            BaseSimObject attackedBy = pTarget.a.attackedBy;
            if (!((BaseSimObject)attackedBy != null) || !attackedBy.isActor() || !attackedBy.isAlive())
            {
                return false;
            }
	    if(!IsGod(attackedBy.a))
              attackedBy.a.addTrait("God Killer");
            if(Main.savedSettings.deathera)
              World.world.eraManager.setEra(S.age_dark, true);
            return true;

        }

        public static bool starsGodsDeath(BaseSimObject pTarget, WorldTile pTile = null)
        {

            BaseSimObject attackedBy = pTarget.a.attackedBy;
            if (!((BaseSimObject)attackedBy != null) || !attackedBy.isActor() || !attackedBy.isAlive())
            {
                return false;
            }
	    if(!IsGod(attackedBy.a))
              attackedBy.a.addTrait("God Killer");
            if(Main.savedSettings.deathera)
              World.world.eraManager.setEra(S.age_moon, true);
            return true;

        }

        public static bool darkGodsDeath(BaseSimObject pTarget, WorldTile pTile = null)
        {
            BaseSimObject attackedBy = pTarget.a.attackedBy;
            if (!((BaseSimObject)attackedBy != null) || !attackedBy.isActor() || !attackedBy.isAlive())
            {
                return false;
            }
	    if(!IsGod(attackedBy.a))
              attackedBy.a.addTrait("God Killer");
            if(Main.savedSettings.deathera)
              World.world.eraManager.setEra(S.age_sun, true);


            return true;

        }

        public static bool godHunterDeath(BaseSimObject pTarget, WorldTile pTile = null)
        {
            BaseSimObject attackedBy = pTarget.a.attackedBy;
            if (!((BaseSimObject)attackedBy != null) || !attackedBy.isActor() || !attackedBy.isAlive())
            {
                return false;
            }
            if (!IsGod(attackedBy.a)){
                ItemData godHuntersScythe = new ItemData();
                godHuntersScythe.id = "GodHuntersScythe";
                godHuntersScythe.material = "base";
                attackedBy.a.equipment.getSlot(EquipmentType.Weapon).setItem(godHuntersScythe);
                attackedBy.a.setStatsDirty();

            }

            return true;

        }

        public static bool genericGodsDeath(BaseSimObject pTarget, WorldTile pTile = null)
        {
            BaseSimObject attackedBy = pTarget.a.attackedBy;
            if (!((BaseSimObject)attackedBy != null) || !attackedBy.isActor() || !attackedBy.isAlive())
            {
                return false;
            }
            attackedBy.a.addTrait("God Killer");
            return true;

        }

        public static bool chaosGodAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            var chaosGodPwr1Chance = Toolbox.randomChance(0.01f);
            var chaosGodPwr2Chance = Toolbox.randomChance(0.02f);

            if (pTarget != null)
            {
                if (chaosGodPwr1Chance)
                {
                    Vector2Int pos = pTile.pos; // Position of the Ptile as a Vector 2
                    float pDist = Vector2.Distance(pTarget.currentPosition, pos); // the distance between the target and the pTile
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.currentPosition.x, pSelf.currentPosition.y, (float)pos.x, (float)pos.y, pDist, true); // the Point of the projectile launcher 
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pTarget.a.stats[S.size], true);
                    EffectsLibrary.spawnProjectile("fireBallX", newPoint, newPoint2, 0.0f);

                }
                if (chaosGodPwr2Chance)
                {
                    Vector2Int pos = pTile.pos; // Position of the Ptile as a Vector 2
                    float pDist = Vector2.Distance(pTarget.currentPosition, pos); // the distance between the target and the pTile
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.currentPosition.x, pSelf.currentPosition.y, (float)pos.x, (float)pos.y, pDist, true); // the Point of the projectile launcher 
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pTarget.a.stats[S.size], true);
                    EffectsLibrary.spawnProjectile("boneFire", newPoint, newPoint2, 0.0f);


                }


                return true;
            }
            return false;
        }


        public static bool knowledgeGodAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {

            

            if (pTarget != null)
            {

                if (Toolbox.randomChance(knowledgeGodPwrChance1 / 100))
                {
                    // randomly spawns a flash of fire or acid on the tile 
                    MapBox.instance.dropManager.spawn(pTile, "fire", 5f, -1f);
                    MapBox.instance.dropManager.spawn(pTile, "acid", 5f, -1f);
                    MapBox.instance.dropManager.spawn(pTile, "fire", 5f, -1f); // Drops fire from distance 5 with scale of one at current tile
                }
                if (Toolbox.randomChance(knowledgeGodPwrChance2 / 100))
                {
                    ActionLibrary.castCurses(null, pTarget, null); // casts curses
                    ((Actor)pSelf).removeTrait("cursed");
                }
                if (Toolbox.randomChance(knowledgeGodPwrChance3 / 100))
                {
                    ActionLibrary.addFrozenEffectOnTarget(null, pTarget, null); // freezezz the target
                }
                if (Toolbox.randomChance(knowledgeGodPwrChance4 / 100))
                {
                    ActionLibrary.castShieldOnHimself(null, pSelf, null); // Casts a shield for himself !! hint: pSelf !!
                }
                if (Toolbox.randomChance(knowledgeGodPwrChance5/100))
                {
                    ActionLibrary.teleportRandom(null, pTarget, null); // teleports the target
                }

                if (Toolbox.randomChance(knowledgeGodPwrChance6 / 100))
                {
                    ActionLibrary.castLightning(null, pTarget, null); // Casts Lightning on the target
                }
                if (Toolbox.randomChance(knowledgeGodPwrChance7 / 100))
                {
                    EffectsLibrary.spawn("fx_meteorite", pTarget.currentTile, "meteorite_disaster", null, 0f, -1f, -1f);    //spawn 1 meteorite
                    pSelf.a.addStatusEffect("invincible", 5f);
                }
                if (Toolbox.randomChance(knowledgeGodPwrChance8/100))
                {
                    EffectsLibrary.spawn("fx_fireball_explosion", pTarget.a.currentTile, null, null, 0f, -1f, -1f);
                    MapAction.damageWorld(pSelf.currentTile, 2, AssetManager.terraform.get("grenade"), null);
                    pSelf.a.addStatusEffect("invincible", 1f);
                }
                if (Toolbox.randomChance(knowledgeGodPwrChance9 / 100))
                {
                    Vector2Int pos = pTile.pos; // Position of the Ptile as a Vector 2
                    float pDist = Vector2.Distance(pTarget.currentPosition, pos); // the distance between the target and the pTile
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.currentPosition.x, pSelf.currentPosition.y, (float)pos.x, (float)pos.y, pDist, true); // the Point of the projectile launcher 
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pTarget.a.stats[S.size], true);
                    EffectsLibrary.spawnProjectile("PagesOfKnowledge", newPoint, newPoint2, 0.0f);
                }

                return true;
            }
            return false;
        }


        public static bool darkGodAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {

            


            if (pTarget != null)
            {


                if (Toolbox.randomChance(darkGodPwrChance1 / 100))
                {
                    EffectsLibrary.spawn("fx_antimatter_effect", pTarget.a.currentTile, null, null, 0f, -1f, -1f);
                    pSelf.a.addStatusEffect("invincible", 5f);
                }
                if (Toolbox.randomChance(darkGodPwrChance2/100))
                {
                    Vector2Int pos = pTile.pos; // Position of the Ptile as a Vector 2
                    float pDist = Vector2.Distance(pTarget.currentPosition, pos); // the distance between the target and the pTile
                    Vector3 newPoint = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pDist, true); // the Point of the projectile launcher 
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pTarget.a.stats[S.size], true);
                    EffectsLibrary.spawnProjectile("BlackHoleProjectile1", newPoint, newPoint2, 0.0f);


                }
                if (Toolbox.randomChance(darkGodPwrChance3 / 100))
                {
                    Vector2Int pos = pTile.pos; // Position of the Ptile as a Vector 2
                    float pDist = Vector2.Distance(pTarget.currentPosition, pos); // the distance between the target and the pTile
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.currentPosition.x, pSelf.currentPosition.y, (float)pos.x, (float)pos.y, pDist, true); // the Point of the projectile launcher 
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pTarget.a.stats[S.size], true);
                    EffectsLibrary.spawnProjectile("DarkDaggersProjectiles", newPoint, newPoint2, 0.0f);


                }
                if (Toolbox.randomChance(darkGodPwrChance4/100))
                {
                    EffectsLibrary.spawnAtTile("fx_smokeFlash_dej", pTile, 0.1f);
                    MapAction.damageWorld(pTarget.currentTile, 5, AssetManager.terraform.get("lightning_power"), null);
                    MapAction.damageWorld(pTarget.currentTile, 8, AssetManager.terraform.get("smokeFlash"), null);
                    World.world.applyForce(pTarget.currentTile, 2, 0.4f, false, true, 20, null, pTarget, null);

                }


                return true;
            }
            return false;
        }


        public static bool starsGodAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            

            if (pTarget != null)
            {


                if (Toolbox.randomChance(starGodPwrChance1 / 100))
                {
                    Vector2Int pos = pTile.pos; // Position of the Ptile as a Vector 2
                    float pDist = Vector2.Distance(pTarget.currentPosition, pos); // the distance between the target and the pTile
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.currentPosition.x + 35f, pSelf.currentPosition.y + 95f, (float)pos.x + 1f, (float)pos.y + 1f, pDist, true); // the Point of the projectile launcher 
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pTarget.a.stats[S.size], true);
                    EffectsLibrary.spawnProjectile("moonFall", newPoint, newPoint2, 0.0f);
                    pSelf.a.addStatusEffect("invincible", 2f);
                }
                if (Toolbox.randomChance(starGodPwrChance2 / 100))
                {
                    EffectsLibrary.spawnAtTile("fx_cometAzureDown_dej", pTarget.a.currentTile, 0.1f);
                    MapAction.applyTileDamage(pTarget.currentTile, 8, AssetManager.terraform.get("cometAzureDownDamage"));
                    MapAction.damageWorld(pTarget.currentTile.neighbours[2], 8, AssetManager.terraform.get("cometAzureDownDamage"), null);
                    MapAction.damageWorld(pTarget.currentTile.neighbours[1], 8, AssetManager.terraform.get("cometAzureDownDamage"), null);
                    World.world.applyForce(pTarget.currentTile.neighbours[0], 4, 0.4f, false, true, 200, null, pTarget, null);
                    pSelf.a.addStatusEffect("invincible", 5f);
                }
                if (Toolbox.randomChance(starGodPwrChance3 / 100))
                {
                    EffectsLibrary.spawnAtTile("fx_cometShower_dej", pTarget.a.currentTile, 0.09f);
                    MapAction.applyTileDamage(pTarget.currentTile, 2f, AssetManager.terraform.get("cometRain"));
                    pSelf.a.addStatusEffect("invincible", 5f);
                    // The Rain Damage effect 
                    MapAction.applyTileDamage(pTarget.currentTile.neighbours[2].neighbours[2].neighbours[1].neighbours[1], 2f, AssetManager.terraform.get("cometRain"));
                    MapAction.applyTileDamage(pTarget.currentTile.neighbours[3].neighbours[3].neighbours[2].neighbours[2], 1f, AssetManager.terraform.get("cometRain"));
                    MapAction.applyTileDamage(pTarget.currentTile.neighbours[0].neighbours[0].neighbours[1].neighbours[2], 1f, AssetManager.terraform.get("cometRain"));
                    MapAction.applyTileDamage(pTarget.currentTile.neighbours[1].neighbours[2].neighbours[3].neighbours[0], 1f, AssetManager.terraform.get("cometRain"));
                    MapAction.applyTileDamage(pTarget.currentTile.neighbours[1].neighbours[1].neighbours[1].neighbours[1], 1f, AssetManager.terraform.get("cometRain"));
                    MapAction.applyTileDamage(pTarget.currentTile.neighbours[2].neighbours[2].neighbours[2].neighbours[2], 1f, AssetManager.terraform.get("cometRain"));
                    MapAction.applyTileDamage(pTarget.currentTile.neighbours[3].neighbours[3].neighbours[3].neighbours[3], 1f, AssetManager.terraform.get("cometRain"));
                    MapAction.applyTileDamage(pTarget.currentTile.neighbours[0].neighbours[1], 2f, AssetManager.terraform.get("cometRain"));
                    MapAction.applyTileDamage(pTarget.currentTile.neighbours[2].neighbours[3], 2f, AssetManager.terraform.get("cometRain"));
                    MapAction.applyTileDamage(pTarget.currentTile.neighbours[0].neighbours[0].neighbours[0], 2f, AssetManager.terraform.get("cometRain"));
                    World.world.applyForce(pTarget.currentTile.neighbours[3].neighbours[3].neighbours[2].neighbours[2], 2, 0.4f, false, true, 20, null, pTarget, null);
                    World.world.applyForce(pTarget.currentTile.neighbours[3].neighbours[3].neighbours[3].neighbours[3], 2, 0.4f, false, true, 20, null, pTarget, null);



                }



                return true;
            }
            return false;
        }



        public static bool sunGodAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {

            

            if (pTarget != null)
            {

                PowerLibrary pb = new PowerLibrary();

                if (Toolbox.randomChance(sunGodPwrChance1 / 100))
                {
                    pb.divineLightFX(pTarget.a.currentTile, null);
                    EffectsLibrary.spawn("fx_napalm_flash", pTarget.a.currentTile, null, null, 0f, -1f, -1f);
                    pSelf.a.addStatusEffect("invincible", 5f);
                }
                if (Toolbox.randomChance(sunGodPwrChance2/100))
                {
                    pb.divineLightFX(pTarget.a.currentTile, null);
                    pTarget.a.addStatusEffect("burning", 5f);

                }
                if (Toolbox.randomChance(sunGodPwrChance3/100))
                {
                    pb.divineLightFX(pTarget.a.currentTile, null);
                    EffectsLibrary.spawn("fx_thunder_flash", pSelf.a.currentTile, null, null, 0f, -1f, -1f);
                    pSelf.a.addStatusEffect("caffeinated", 10f);
                    pTarget.a.addStatusEffect("slowness", 10f);


                }
                if (Toolbox.randomChance(sunGodPwrChance4 / 100))
                {
                    Vector2Int pos = pTile.pos; // Position of the Ptile as a Vector 2
                    float pDist = Vector2.Distance(pTarget.currentPosition, pos); // the distance between the target and the pTile
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.currentPosition.x, pSelf.currentPosition.y, (float)pos.x, (float)pos.y, pDist, true); // the Point of the projectile launcher 
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pTarget.a.stats[S.size], true);
                    EffectsLibrary.spawnProjectile("lightBallzProjectiles", newPoint, newPoint2, 0.0f);
                }
                //EffectsLibrary.spawnAtTile("fx_multiFlash_dej", pTile, 0.01f);

                if (Toolbox.randomChance(sunGodPwrChance5 / 100))
                {
                    //TO BE USED AS IMACT ACTION FOR LIGHT PROJECILES LATER
                    int count = 0;
                    Vector2Int pos = pTile.pos; // Position of the Ptile as a Vector 2
                    float pDist = Vector2.Distance(pTarget.currentPosition, pos);
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pTarget.a.stats[S.size], true);

                    EffectsLibrary.spawn("fx_napalm_flash", pTarget.a.currentTile.neighbours[2].neighbours[2].neighbours[1].neighbours[1].neighbours[2], null, null, 0f, -1f, -1f);
                    count++;
                    if (count == 1)
                    {
                        EffectsLibrary.spawn("fx_napalm_flash", pTarget.a.currentTile.neighbours[1].neighbours[0].neighbours[0].neighbours[1].neighbours[0].neighbours[0].neighbours[1].neighbours[1].neighbours[0].neighbours[0].neighbours[0], null, null, 0f, -0.5f, -0.2f);
                        count++;
                    }
                    if (count == 2)
                    {
                        EffectsLibrary.spawn("fx_napalm_flash", pTarget.a.currentTile.neighbours[1].neighbours[3].neighbours[2].neighbours[1].neighbours[2].neighbours[3].neighbours[1].neighbours[1].neighbours[0].neighbours[0], null, null, 0f, 1f, -0.2f);
                        count++;
                    }
                    if (count == 3)
                    {
                        EffectsLibrary.spawn("fx_napalm_flash", pTarget.a.currentTile.neighbours[1].neighbours[3].neighbours[2].neighbours[1].neighbours[2].neighbours[3].neighbours[1].neighbours[1].neighbours[0].neighbours[0], null, null, 0f, -0.5f, -0.2f);
                        EffectsLibrary.spawn("fx_napalm_flash", pTarget.a.currentTile.neighbours[1].neighbours[3].neighbours[0].neighbours[1].neighbours[0].neighbours[0].neighbours[0].neighbours[1].neighbours[0].neighbours[0], null, null, 0f, -0.5f, -0.2f);

                    }




                    pSelf.a.addStatusEffect("invincible", 5f);
                }


                return true;
            }
            return false;
        }

        public static bool warGodAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            


            if (pTarget != null)
            {
                PowerLibrary pb = new PowerLibrary();

                if (Toolbox.randomChance(warGodPwrChance1 / 100))
                {
                    EffectsLibrary.spawnExplosionWave(pSelf.currentTile.posV3, 1f, 1f);
                    pSelf.a.addStatusEffect("WarGodsCry", 30f);
                    World.world.startShake(0.3f, 0.01f, 2f, true, true);
                    pSelf.a.addStatusEffect("invincible", 1f);
                    MapAction.damageWorld(pSelf.currentTile, 2, AssetManager.terraform.get("crab_step"), null);
                    pSelf.a.addStatusEffect("invincible", 1f);
                    World.world.applyForce(pSelf.currentTile, 4, 0.4f, false, true, 20, null, pTarget, null);

                }
                if (Toolbox.randomChance(warGodPwrChance2 / 100))
                {
                    Vector2Int pos = pTile.pos; // Position of the Ptile as a Vector 2
                    float pDist = Vector2.Distance(pTarget.currentPosition, pos); // the distance between the target and the pTile
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.currentPosition.x, pSelf.currentPosition.y, (float)pos.x, (float)pos.y, pDist, true); // the Point of the projectile launcher 
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pTarget.a.stats[S.size], true);
                    EffectsLibrary.spawnProjectile("WarAxeProjectile1", newPoint, newPoint2, 0.0f);

                }




                return true;
            }
            return false;
        }

        public static bool earthGodAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            

            if (pTarget != null)
            {
                PowerLibrary pb = new PowerLibrary();

                if (Toolbox.randomChance(earthGodPwrChance1/ 100))
                {
                    pb.spawnEarthquake(pTarget.a.currentTile, null);
                }
                if (Toolbox.randomChance(earthGodPwrChance2/ 100))
                {
                    pb.spawnCloudRain(pTarget.a.currentTile, null);
                    pb.spawnCloudSnow(pTarget.a.currentTile, null);
                }
                if (Toolbox.randomChance(earthGodPwrChance3/100))
                {
                    pb.spawnBoulder(pTarget.a.currentTile, null);
                }


                return true;
            }
            return false;
        }


        public static bool lichGodAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            

            if (pTarget != null)
            {

                if (Toolbox.randomChance(lichGodPwrChance1 / 100))
                {
                    Vector2Int pos = pTile.pos; // Position of the Ptile as a Vector 2
                    float pDist = Vector2.Distance(pTarget.currentPosition, pos); // the distance between the target and the pTile
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.currentPosition.x, pSelf.currentPosition.y, (float)pos.x, (float)pos.y, pDist, true); // the Point of the projectile launcher 
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pTarget.a.stats[S.size], true);
                    EffectsLibrary.spawnProjectile("waveOfMutilationProjectile", newPoint, newPoint2, 0.0f);
                }


                return true;
            }
            return false;
        }


        public static bool sunGodAutoTrait(BaseSimObject pTarget, WorldTile pTile)
        {

            if (pTarget.a != null)
            {
                if (pTarget.a.hasTrait("God Of light"))
                {
                    pTarget.a.addTrait("blessed");
                    pTarget.a.addTrait("shiny");
                    pTarget.a.addTrait("agile");
                    pTarget.a.addTrait("fire_proof");
                    pTarget.a.addTrait("fire_blood");
                    pTarget.a.addTrait("weightless");
                    pTarget.a.addTrait("fast");
                    pTarget.a.addTrait("energized");
                    pTarget.a.addTrait("light_lamp");
                    pTarget.a.addTrait("immortal");

                }


            }
            return true;
        }





        public static bool knowledgeGodAutoTrait(BaseSimObject pTarget, WorldTile pTile)
        {
            if (pTarget.a != null)
            {
                if (pTarget.a.hasTrait("God Of Knowledge"))
                {
                    pTarget.a.addTrait("blessed");
                    pTarget.a.addTrait("genius");
                    pTarget.a.addTrait("frost_proof");
                    pTarget.a.addTrait("fire_proof");
                    pTarget.a.addTrait("fire_blood");
                    pTarget.a.addTrait("tough");
                    pTarget.a.addTrait("strong_minded");
                    pTarget.a.addTrait("energized");
                    pTarget.a.addTrait("immortal");
                    pTarget.a.addTrait("wise");


                }


            }
            return true;
        }

        public static bool darkGodAutoTrait(BaseSimObject pTarget, WorldTile pTile)
        {
            if (pTarget.a != null)
            {
                if (pTarget.a.hasTrait("God Of the Night"))
                {
                    pTarget.a.addTrait("blessed");
                    pTarget.a.addTrait("bloodlust");
                    pTarget.a.addTrait("agile");
                    pTarget.a.addTrait("frost_proof");
                    pTarget.a.addTrait("weightless");
                    pTarget.a.addTrait("cold_aura");
                    pTarget.a.addTrait("energized");
                    pTarget.a.addTrait("immortal");
                    pTarget.a.addTrait("nightchild");
                    pTarget.a.addTrait("moonchild");

                }


            }
            return true;
        }

        public static bool starsGodAutoTrait(BaseSimObject pTarget, WorldTile pTile)
        {
            if (pTarget.a != null)
            {
                if (pTarget.a.hasTrait("God Of the Stars"))
                {
                    pTarget.a.addTrait("blessed");
                    pTarget.a.addTrait("agile");
                    pTarget.a.addTrait("frost_proof");
                    pTarget.a.addTrait("weightless");
                    pTarget.a.addTrait("shiny");
                    pTarget.a.addTrait("energized");
                    pTarget.a.addTrait("immortal");
                    pTarget.a.addTrait("nightchild");
                    pTarget.a.addTrait("moonchild");

                }


            }
            return true;
        }

        public static bool earthGodAutoTrait(BaseSimObject pTarget, WorldTile pTile)
        {

            if (pTarget.a != null)
            {
                if (pTarget.a.hasTrait("God Of the Earth"))
                {
                    pTarget.a.addTrait("blessed");
                    pTarget.a.addTrait("giant");
                    pTarget.a.addTrait("strong");
                    pTarget.a.addTrait("frost_proof");
                    pTarget.a.addTrait("fat");
                    pTarget.a.addTrait("tough");
                    pTarget.a.addTrait("immortal");

                }


            }
            return true;
        }

        public static bool warGodAutoTrait(BaseSimObject pTarget, WorldTile pTile)
        {

            if (pTarget.a != null)
            {
                if (pTarget.a.hasTrait("God Of War"))
                {
                    pTarget.a.addTrait("blessed");
                    pTarget.a.addTrait("strong");
                    pTarget.a.addTrait("frost_proof");
                    pTarget.a.addTrait("ambitious");
                    pTarget.a.addTrait("pyromaniac");
                    pTarget.a.addTrait("veteran");
                    pTarget.a.addTrait("tough");
                    pTarget.a.addTrait("immortal");

                }


            }
            return true;
        }

        public static bool lichGodAutoTrait(BaseSimObject pTarget, WorldTile pTile)
        {

            if (pTarget.a != null)
            {
                if (pTarget.a.hasTrait("God Of The Lich"))
                {
                    pTarget.a.addTrait("blessed");
                    pTarget.a.addTrait("strong");
                    pTarget.a.addTrait("frost_proof");
                    pTarget.a.addTrait("acid_touch");
                    pTarget.a.addTrait("acid_blood");
                    pTarget.a.addTrait("acid_proof");
                    pTarget.a.addTrait("regeneration");
                    pTarget.a.addTrait("tough");
                    pTarget.a.addTrait("immortal");

                }


            }
            return true;
        }

        public static bool godKillerAutoTrait(BaseSimObject pTarget, WorldTile pTile)
        {

            if (pTarget.a != null)
            {
                    pTarget.a.addTrait("blessed");
                    pTarget.a.addTrait("frost_proof");
                    pTarget.a.addTrait("fire_proof");
		            pTarget.a.addTrait("tough");


            }
            return true;
        }


        private static bool sunGodEraStatus(BaseSimObject pSelf, WorldTile pTile)
        {
            if (pSelf.a != null)
            {
                if (World.world_era.id == "age_sun")       //only in age of sun
                {
                    pSelf.a.addStatusEffect("Lights_Prevail"); // add the status I created

                }
                else
                {
                    if (pSelf.a.hasStatus("Lights_Prevail"))          //no other age can have this trait
                    {
                        pSelf.a.finishAllStatusEffects(); // remove the status
                    }
                }


            }
            return true;
        }
        private static bool GodOfGodsEraStatus(BaseSimObject pSelf, WorldTile pTile)
        {
            if (pSelf.a != null)
            {
                if (World.world_era.id == "age_hope")   {    //only in age of hope
                    if(!pSelf.a.hasStatus("God_Of_All")){
                {
                    pSelf.a.addStatusEffect("God_Of_All"); // add the status I created
                    pSelf.a.data.set("lifespan", 61);
                }
                    }
                }
                else
                {
                    if (pSelf.a.hasStatus("God_Of_All"))          //no other age can have this trait
                    {
                        pSelf.a.finishAllStatusEffects(); // remove the status
                        pSelf.a.data.set("lifespan", 31);
                    }
                }


            }
            return true;
        }

        private static bool warGodEraStatus(BaseSimObject pSelf, WorldTile pTile)
        {
            if (pSelf.a != null)
            {
                if (World.world_era.id == "age_ash")       //only in age of sun
                {
                    pSelf.a.addStatusEffect("War_Prevail"); // add the status I created

                }
                else
                {
                    if (pSelf.a.hasStatus("War_Prevail"))          //no other age can have this trait
                    {
                        pSelf.a.finishAllStatusEffects(); // remove the status
                    }
                }


            }
            return true;
        }

        private static bool starsGodEraStatus(BaseSimObject pSelf, WorldTile pTile)
        {
            if (pSelf.a != null)
            {
                if (World.world_era.id == "age_moon")       //only in age of moon
                {
                    pSelf.a.addStatusEffect("Stars_Prevail"); // add the status I created

                }
                else
                {
                    if (pSelf.a.hasStatus("Stars_Prevail"))          //no other age can have this trait
                    {
                        pSelf.a.finishAllStatusEffects(); // remove the status
                    }
                }


            }
            return true;
        }

        private static bool darkGodEraStatus(BaseSimObject pSelf, WorldTile pTile)
        {
            if (pSelf.a != null)
            {
                if (World.world_era.id == "age_dark")       //only in age of dark
                {
                    pSelf.a.addStatusEffect("Nights_Prevail"); // add the status I created

                }
                else
                {
                    if (pSelf.a.hasStatus("Nights_Prevail"))          //no other age can have this trait
                    {
                        pSelf.a.finishAllStatusEffects(); // remove the status
                    }
                }


            }
            return true;
        }

        private static bool knowledgeGodEraStatus(BaseSimObject pSelf, WorldTile pTile)
        {
            if (pSelf.a != null)
            {
                if (World.world_era.id == "age_wonders")       //only in age of wonder
                {
                    pSelf.a.addStatusEffect("Knowledge_Prevail"); // add the status I created

                }
                else
                {
                    if (pSelf.a.hasStatus("Knowledge_Prevail"))          //no other age can have this trait
                    {
                        pSelf.a.finishAllStatusEffects(); // remove the status
                    }
                }


            }
            return true;
        }

        public static bool warGodSeeds(BaseSimObject pTarget, WorldTile pTile)
        {

            if (pTarget.a != null)
            {

                WorldTile _tile = Toolbox.getRandomTileWithinDistance(pTile, 60);
                //WorldTile tile2 = Toolbox.getRandomTileWithinDistance(pTile, 40);
               // List<WorldTile> randTile = List.Of<WorldTile>(new WorldTile[] { tile1, tile2 });
               // WorldTile _tile = Toolbox.getRandomTileWithinDistance(randTile, pTile, 45, 120);
                if (Toolbox.randomChance(warGodPwrChance3/100))
                {
                    MapBox.instance.dropManager.spawn(_tile, SD.spite, 5f, -1f);
                }
                if (Toolbox.randomChance(warGodPwrChance3/100))
                {
                    MapBox.instance.dropManager.spawn(_tile, SD.discord, 5f, -1f);
                }
                if (Toolbox.randomChance(warGodPwrChance3 / 100))
                {
                    MapBox.instance.dropManager.spawn(_tile, SD.inspiration, 5f, -1f);
                }

            }
            return true;
        }

        public static bool earthGodBuildWorld(BaseSimObject pSelf, WorldTile pTile)
        {
            if (pSelf.a != null)
            {
                if (Toolbox.randomChance(earthGodPwrChance3/100))
                {
                    ActionLibrary.tryToGrowTree(pSelf);
                }
                if (Toolbox.randomChance(earthGodPwrChance3/100))
                {
                    ActionLibrary.tryToCreatePlants(pSelf);
                }
                if (Toolbox.randomChance(earthGodPwrChance3 / 100))
                {
                    BuildingActions.tryGrowMineralRandom(pSelf.a.currentTile);
                }
                if (Toolbox.randomChance(earthGodPwrChance3/100))
                {

                    buildMountain(pTile);
                    Debug.Log("IGNORE THIS ERROR AND KEEP PLAYING!");

                }

                return true;
            }
            return false;
        }


        public static void buildMountain(WorldTile pTile)
        {
            WorldTile tile1 = Toolbox.getRandomTileWithinDistance(pTile, 50);
            WorldTile tile2 = Toolbox.getRandomTileWithinDistance(pTile, 25);
            List<WorldTile> randTile = List.Of<WorldTile>(new WorldTile[] { tile1, tile2 });
            WorldTile _tile = Toolbox.getRandomTileWithinDistance(randTile, pTile, 30, 120);
            if (_tile != null)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        WorldTile tile = _tile.neighbours[i];
                        MapAction.terraformMain(tile, AssetManager.tiles.get("mountains"), TerraformLibrary.destroy);
                        MapAction.terraformMain(tile.neighbours[i], AssetManager.tiles.get("mountains"), TerraformLibrary.destroy);
                        MapAction.terraformMain(tile.neighbours[j], AssetManager.tiles.get("soil_high"), TerraformLibrary.destroy);
                        MapAction.terraformMain(tile.neighbours[j].neighbours[i], AssetManager.tiles.get("soil_high"), TerraformLibrary.destroy);
                        MapAction.terraformMain(tile.neighbours[j].neighbours[i], AssetManager.tiles.get("soil_low"), TerraformLibrary.destroy);
                        MapAction.terraformMain(tile.neighbours[i].neighbours[j], AssetManager.tiles.get("soil_low"), TerraformLibrary.destroy);
                        MapAction.terraformMain(tile.neighbours[i].neighbours[0], AssetManager.tiles.get("mountains"), TerraformLibrary.destroy);
                        MapAction.terraformMain(tile.neighbours[i].neighbours[j].neighbours[j], AssetManager.tiles.get("soil_low"), TerraformLibrary.destroy);
                        MapAction.terraformMain(tile.neighbours[j].neighbours[i].neighbours[i], AssetManager.tiles.get("soil_low"), TerraformLibrary.destroy);

                    }
                }
            }


        }
        public static void addTraitToLocalizedLibrary(string id, string description)
        {

            string language = Reflection.GetField(LocalizedTextManager.instance.GetType(), LocalizedTextManager.instance, "language") as string;
            Dictionary<string, string> localizedText = Reflection.GetField(LocalizedTextManager.instance.GetType(), LocalizedTextManager.instance, "localizedText") as Dictionary<string, string>;
            localizedText.Add("trait_" + id, id);
            localizedText.Add("trait_" + id + "_info", description);

        }

        public static Actor FindMaster(Actor summoned)
           {
              List<Actor> simpleList = World.world.units.getSimpleList();
                 foreach (Actor actor in simpleList)
                  {
                   if (summoned.getName().Equals($"Summoned by {actor.getName()}") && actor.hasTrait("God Of gods"))
                  {
                         return actor;
                    }
               }
              return summoned;
        }
    }
    [HarmonyPatch(typeof(BaseSimObject), "canAttackTarget")]
    public class UpdateAttacking
    {
        static void Postfix(ref bool __result, BaseSimObject __instance, BaseSimObject pTarget)
        {
            if (__instance == pTarget)
            {
                __result = false;
            }
            if (__instance.isActor())
            {
                Actor a = __instance.a;
                if (a.hasTrait("Summoned One"))
                {
                    Actor Master = Traits.FindMaster(a);
                    if (Master != a)
                    {
                        if (!Master.canAttackTarget(pTarget))
                        {
                            __result = false;
                            return;
                        }
                    }
                }
            }
             if (pTarget.isActor())
            {
                Actor b = pTarget.a;
                if (b.hasTrait("Summoned One"))
                {
                    Actor Master = Traits.FindMaster(b);
                    if (Master != b)
                    {
                        if (!__instance.canAttackTarget(Master))
                            __result = false;
                    }
                }
            }
            
       }
    }
    [HarmonyPatch(typeof(ActorBase), "clearAttackTarget")]
    public class KEEPATTACKING
    {
        static bool Prefix(ActorBase __instance)
        {
            if (__instance.hasTrait("God Hunter")){
                BaseSimObject? a = Reflection.GetField(typeof(ActorBase), __instance, "attackTarget") as BaseSimObject;
                if (a != null) {
                    if (Traits.IsGod(a.a) && a.isAlive()) { return false; }
                }
            }
            return true;
        }
    }
    [HarmonyPatch(typeof(MapBox), "applyAttack")]
    public class updateAttack
    {
      static void Postfix(AttackData pData, BaseSimObject pTargetToCheck)
       {
	      if (pData.initiator.isActor() && pTargetToCheck.isActor()){
	      if(pData.initiator.a.hasTrait("God Hunter") && !pTargetToCheck.isAlive() && Traits.IsGod(pTargetToCheck.a)){
              EffectsLibrary.spawnAt("fx_teleport_blue", pData.initiator.currentPosition, pData.initiator.stats[S.scale]);
		      pData.initiator.a.killHimself();
	      }
	      }
        }
 }
}

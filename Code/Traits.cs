/* 
AUTHOR: MASON SCARBRO
VERSION: 0.3.0
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

namespace GodsAndPantheons
{
    class Traits
    { 
        public static void init()
        {

            /*
            PLANNED ENCOMPOSING:
            All of the things that encompose all gods etc will be planned here 
            - Each god will be assigned an in game trait automatically that associated to their type (soon to be unique non base game traits)
            - Each god will have a stat boost with their associated age
            - There will be DemiGods which are the breeding of mortal and god
            - Check if God is Almost dead if he is increase chance he will activate special power else 
            - Add a special power to stars God
            */

            /*some notes pTarget typically reps the target that the actor is currently facing pSelf is the Actors self
            a typically stands for the actor a is a assembly reference to the actor however we can also declare an Actor object with a
            you will notice that s typically used in the spawning of things or usage of effects, as of now I cant find what reflection actually does or why its used
            from what I can tell this is typically used when using attacks that might want to be used for range to 
            locate the target and kill him, TESTED. I deleted the line and just used the base game a call it didnt seem to efffect anything? */

            
            ActorTrait chaosGod = new ActorTrait();
            chaosGod.id = "God Of Chaos";
            chaosGod.path_icon = "ui/icons/achievements/achievements_thedemon";
            chaosGod.base_stats[S.damage] += 15;
            chaosGod.base_stats[S.health] += 800;
            chaosGod.base_stats[S.attack_speed] += 5f;
            chaosGod.action_attack_target = new AttackAction(ActionLibrary.addBurningEffectOnTarget);
            chaosGod.action_death = new WorldAction(ActionLibrary.turnIntoDemon);
            chaosGod.action_death = (WorldAction)Delegate.Combine(chaosGod.action_death, new WorldAction(chaosGodsTrick));
            chaosGod.base_stats[S.scale] = 0.08f;
            AssetManager.traits.add(chaosGod);
            PlayerConfig.unlockTrait(chaosGod.id);
            addTraitToLocalizedLibrary(chaosGod.id, "Tis's The God Of Chaos!");

            
            ActorTrait sunGod = new ActorTrait();
            sunGod.id = "God Of light";
            sunGod.path_icon = "ui/icons/achievements/achievements_thedemon";
            sunGod.base_stats[S.damage] += 15f;
            sunGod.base_stats[S.health] += 500;
            sunGod.base_stats[S.attack_speed] += 25f;
            sunGod.base_stats[S.critical_chance] += 0.05f;
            sunGod.base_stats[S.speed] += 80f;
            sunGod.base_stats[S.dodge] += 30f;
            sunGod.base_stats[S.accuracy] += 10f;
            sunGod.base_stats[S.range] += 100f;
            sunGod.action_attack_target = new AttackAction(ActionLibrary.addBurningEffectOnTarget);
            sunGod.action_attack_target = new AttackAction(ActionLibrary.addSlowEffectOnTarget);
            sunGod.action_attack_target = new AttackAction(sunGodAttack);
            sunGod.action_death = (WorldAction)Delegate.Combine(sunGod.action_death, new WorldAction(sunGodsDeath));
            AssetManager.traits.add(sunGod);
            PlayerConfig.unlockTrait(sunGod.id);
            sunGod.action_special_effect = (WorldAction)Delegate.Combine(sunGod.action_special_effect, new WorldAction(sunGodAutoTrait));
            addTraitToLocalizedLibrary(sunGod.id, "Tis' The God Of light!");
            

            ActorTrait darkGod = new ActorTrait();
            darkGod.id = "God Of the Night";
            darkGod.path_icon = "ui/icons/achievements/achievements_thedemon";
            darkGod.base_stats[S.damage] += 15f;
            darkGod.base_stats[S.health] += 550;
            darkGod.base_stats[S.attack_speed] += 3f;
            darkGod.base_stats[S.critical_chance] += 0.25f;
            darkGod.base_stats[S.scale] = 0.02f;
            darkGod.base_stats[S.dodge] += 3f;
            darkGod.action_attack_target = new AttackAction(darkGodAttack);
            darkGod.action_death = (WorldAction)Delegate.Combine(darkGod.action_death, new WorldAction(darkGodsDeath));
            AssetManager.traits.add(darkGod);
            PlayerConfig.unlockTrait(darkGod.id);
            darkGod.action_special_effect = (WorldAction)Delegate.Combine(darkGod.action_special_effect, new WorldAction(darkGodAutoTrait));
            addTraitToLocalizedLibrary(darkGod.id, "Tis' The God Of darkness!");

            ActorTrait knowledgeGod = new ActorTrait();
            knowledgeGod.id = "God Of Knowledge";
            knowledgeGod.path_icon = "ui/icons/achievements/achievements_thedemon";
            knowledgeGod.base_stats[S.damage] += 15f;
            knowledgeGod.base_stats[S.health] += 600;
            knowledgeGod.base_stats[S.attack_speed] += 1f;
            knowledgeGod.base_stats[S.critical_chance] += 0.25f;
            knowledgeGod.base_stats[S.range] += 15f;
            knowledgeGod.base_stats[S.scale] = 0.06f;
            knowledgeGod.base_stats[S.intelligence] += 35f;
            knowledgeGod.base_stats[S.accuracy] += 10f;
            knowledgeGod.action_attack_target = new AttackAction(knowledgeGodAttack);
            AssetManager.traits.add(knowledgeGod);
            PlayerConfig.unlockTrait(knowledgeGod.id);
            knowledgeGod.action_special_effect = (WorldAction)Delegate.Combine(knowledgeGod.action_special_effect, new WorldAction(knowledgeGodAutoTrait));
            addTraitToLocalizedLibrary(knowledgeGod.id, "Tis' The God Of Knowledges!");

            ActorTrait starsGod = new ActorTrait();
            starsGod.id = "God Of the Stars";
            starsGod.path_icon = "ui/icons/achievements/achievements_thedemon";
            starsGod.base_stats[S.damage] += 15f;
            starsGod.base_stats[S.health] += 600;
            starsGod.base_stats[S.attack_speed] += 1f;
            starsGod.base_stats[S.critical_chance] += 0.05f;
            starsGod.base_stats[S.scale] = 0.02f;
            starsGod.base_stats[S.range] += 15f;
            starsGod.base_stats[S.intelligence] += 3f;
            starsGod.action_attack_target = new AttackAction(ActionLibrary.addFrozenEffectOnTarget);
            AssetManager.traits.add(starsGod);
            PlayerConfig.unlockTrait(starsGod.id);
            starsGod.action_special_effect = (WorldAction)Delegate.Combine(starsGod.action_special_effect, new WorldAction(starsGodAutoTrait));
            addTraitToLocalizedLibrary(starsGod.id, "Now Cometh the Age of stars, A Thousand Year Voyage under the wisdom of the moon");

            ActorTrait earthGod = new ActorTrait();
            earthGod.id = "God Of the Earth";
            earthGod.path_icon = "ui/icons/achievements/achievements_thedemon";
            earthGod.base_stats[S.damage] += 40f;
            earthGod.base_stats[S.health] += 1000;
            earthGod.base_stats[S.attack_speed] += 1f;
            earthGod.base_stats[S.armor] += 30f;
            earthGod.base_stats[S.scale] = 0.1f;
            earthGod.base_stats[S.range] += 10f;
            earthGod.base_stats[S.intelligence] += 3f;
            AssetManager.traits.add(earthGod);
            PlayerConfig.unlockTrait(earthGod.id);
            earthGod.action_special_effect = (WorldAction)Delegate.Combine(earthGod.action_special_effect, new WorldAction(earthGodAutoTrait));
            addTraitToLocalizedLibrary(earthGod.id, "God of the Natural Enviornment, The titan of creation");

            ActorTrait subGod = new ActorTrait();
            subGod.id = "LesserGod";
            subGod.path_icon = "ui/icons/achievements/achievements_thedemon";
            subGod.base_stats[S.damage] += 5f;
            subGod.base_stats[S.health] += 400;
            subGod.base_stats[S.attack_speed] += 1f;
            subGod.base_stats[S.scale] = 0.02f;
            subGod.base_stats[S.critical_chance] += 0.05f;
            AssetManager.traits.add(subGod);
            PlayerConfig.unlockTrait(subGod.id);
            addTraitToLocalizedLibrary(subGod.id, "These Are the gods that have smaller importance");


        }

        public static bool chaosGodsTrick(BaseSimObject pTarget, WorldTile pTile = null)
        {
        
            Actor pActor = (Actor)pTarget;
            pActor.removeTrait("chaosGod");
            World.world.eraManager.setEra(S.age_chaos, true);


            return true;

        }

        public static bool sunGodsDeath(BaseSimObject pTarget, WorldTile pTile = null)
        {
        

            World.world.eraManager.setEra(S.age_dark, true);


            return true;

        }

        public static bool darkGodsDeath(BaseSimObject pTarget, WorldTile pTile = null)
        {
        
            World.world.eraManager.setEra(S.age_sun, true);


            return true;

        }


        public static bool knowledgeGodAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {

            if (pTarget != null)
            {
                Actor a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;

                if (Toolbox.randomChance(0.2f))
                {
                    // randomly spawns a flash of fire or acid on the tile 
                    MapBox.instance.dropManager.spawn(pTile, "fire", 5f, -1f);
                    MapBox.instance.dropManager.spawn(pTile, "acid", 5f, -1f);
                    MapBox.instance.dropManager.spawn(pTile, "fire", 5f, -1f); // Drops fire from distance 5 with scale of one at current tile
                }
                if (Toolbox.randomChance(0.01f))
                {
                    ActionLibrary.castCurses(null, pTarget, null); // casts curses
                }
                if (Toolbox.randomChance(0.01f))
                {
                    ActionLibrary.addFrozenEffectOnTarget(null, pTarget, null); // freezezz the target
                }
                if (Toolbox.randomChance(0.05f))
                {
                    ActionLibrary.castShieldOnHimself(null, pSelf, null); // Casts a shield for himself !! hint: pSelf !!
                }
                if (Toolbox.randomChance(0.04f))
                {
                    ActionLibrary.teleportRandom(null, pTarget, null); // teleports the target
                }
                if (Toolbox.randomChance(0.1f))
                {
                    ActionLibrary.castLightning(null, pTarget, null); // Casts Lightning on the target
                }
                if (Toolbox.randomChance(0.05f))
                {
                    EffectsLibrary.spawn("fx_meteorite", pTarget.currentTile, "meteorite_disaster", null, 0f, -1f, -1f);    //spawn 1 meteorite
                    pSelf.a.addStatusEffect("invincible", 5f);
                }
                if (Toolbox.randomChance(0.1f))
                {
                    EffectsLibrary.spawn("fx_thunder_flash", pTarget.a.currentTile, null, null, 0f, -1f, -1f);
                    pSelf.a.addStatusEffect("invincible", 5f);
                }

                return true;
            }
            return false;
        }


        public static bool darkGodAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {

            if (pTarget != null)
            {
                Actor a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;

                
                if (Toolbox.randomChance(0.05f))
                {
                    EffectsLibrary.spawn("fx_antimatter_effect", pTarget.a.currentTile, null, null, 0f, -1f, -1f);
                    pSelf.a.addStatusEffect("invincible", 5f);
                }

                return true;
            }
            return false;
        }

        

        public static bool sunGodAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {

            if (pTarget != null)
            {
                
                Actor a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;
                
                if (Toolbox.randomChance(0.08f))
                {
                    EffectsLibrary.spawn("fx_napalm_flash", pTarget.a.currentTile, null, null, 0f, -1f, -1f);
                    pSelf.a.addStatusEffect("invincible", 5f);
                }
                if (Toolbox.randomChance(0.5f))
                {
                    EffectsLibrary.spawn("fx_slash", pTarget.a.currentTile, null, null, 0f, -1f, -1f);
                }


                return true;
            }
            return false;
        }
        
        public static bool earthGodAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {

            if (pTarget != null)
            {
                Actor a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;
                PowerLibrary pb = new PowerLibrary();
                
                if (Toolbox.randomChance(0.5f))
                {
                    pb.spawnEarthquake(pTarget.a.currentTile, null);
                }
                if (Toolbox.randomChance(0.1f))
                {
                    pb.spawnCloudRain(pTarget.a.currentTile, null);
                    pb.spawnCloudSnow(pTarget.a.currentTile, null);
                }
                if (Toolbox.randomChance(0.01f))
                {
                    pb.spawnBoulder(pTarget.a.currentTile, null);
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
            Actor a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;
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
            Actor a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;
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
            Actor a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;
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

         public static void addTraitToLocalizedLibrary(string id, string description)        
         {

            string language = Reflection.GetField(LocalizedTextManager.instance.GetType(), LocalizedTextManager.instance, "language") as string;
      		Dictionary<string, string> localizedText = Reflection.GetField(LocalizedTextManager.instance.GetType(), LocalizedTextManager.instance, "localizedText") as Dictionary<string, string>;
      		localizedText.Add("trait_" + id, id);
      		localizedText.Add("trait_" + id + "_info", description);

        }
        


    }

}

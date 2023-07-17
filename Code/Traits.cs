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
            - Each god will have a specific height/size change (maybe not all)
            - Each god will be assigned an in game trait automatically that associated to their type (soon to be unique non base game traits)
            - Each god will have a stat boost with their associated age
            - There will be DemiGods which are the breeding of mortal and god 
            - Fix git
            */

            
            ActorTrait chaosGod = new ActorTrait();
            chaosGod.id = "God Of Chaos";
            chaosGod.path_icon = "ui/icons/achievements/achievements_thedemon";
            chaosGod.base_stats[S.damage] += 15;
            chaosGod.base_stats[S.health] = 800;
            chaosGod.base_stats[S.attack_speed] += 5f;
            chaosGod.action_attack_target = new AttackAction(ActionLibrary.addBurningEffectOnTarget);
            chaosGod.action_death = new WorldAction(ActionLibrary.turnIntoDemon);
            chaosGod.action_death = (WorldAction)Delegate.Combine(chaosGod.action_death, new WorldAction(chaosGodsTrick));
            AssetManager.traits.add(chaosGod);
            PlayerConfig.unlockTrait(chaosGod.id);
            addTraitToLocalizedLibrary(chaosGod.id, "Tis's The God Of Chaos!");

            
            ActorTrait sunGod = new ActorTrait();
            sunGod.id = "God Of light";
            sunGod.path_icon = "ui/icons/achievements/achievements_thedemon";
            sunGod.base_stats[S.damage] += 10f;
            sunGod.base_stats[S.health] = 500;
            sunGod.base_stats[S.attack_speed] += 15f;
            sunGod.base_stats[S.critical_chance] += 0.05f;
            //INCREASE MOVEMENT SPEED PLANNED
            sunGod.action_attack_target = new AttackAction(ActionLibrary.addBurningEffectOnTarget);
            //NEW ACTION TO BE ADDED
            sunGod.action_death = (WorldAction)Delegate.Combine(sunGod.action_death, new WorldAction(sunGodsDeath));
            AssetManager.traits.add(sunGod);
            PlayerConfig.unlockTrait(sunGod.id);
            addTraitToLocalizedLibrary(sunGod.id, "Tis's The God Of light!");

            ActorTrait darkGod = new ActorTrait();
            darkGod.id = "God Of the Night";
            darkGod.path_icon = "ui/icons/achievements/achievements_thedemon";
            darkGod.base_stats[S.damage] += 10f;
            darkGod.base_stats[S.health] = 550;
            darkGod.base_stats[S.attack_speed] += 3f;
            darkGod.base_stats[S.critical_chance] += 0.25f;
            //SPAWN ANTIMATTER BOMB VISUAL ON BODY
            darkGod.action_death = (WorldAction)Delegate.Combine(darkGod.action_death, new WorldAction(darkGodsDeath));
            AssetManager.traits.add(darkGod);
            PlayerConfig.unlockTrait(darkGod.id);
            addTraitToLocalizedLibrary(darkGod.id, "Tis's The God Of darkness!");

            ActorTrait knowledgeGod = new ActorTrait();
            knowledgeGod.id = "God Of Knowledge";
            knowledgeGod.path_icon = "ui/icons/achievements/achievements_thedemon";
            knowledgeGod.base_stats[S.damage] += 10f;
            knowledgeGod.base_stats[S.health] = 600;
            knowledgeGod.base_stats[S.attack_speed] += 1f;
            knowledgeGod.base_stats[S.critical_chance] += 0.25f;
            knowledgeGod.base_stats[S.intelligence] += 35f;
            //KNOWLEDGE GOD IS INTENDED TO BE ON WIZARD SO IF ELSE GIVE ACTOR ABILITY TO USE ALL SPELLS INCLUDING NAPALM BOMBS VISUAL (NO PHYS BOMB)
            AssetManager.traits.add(knowledgeGod);
            PlayerConfig.unlockTrait(knowledgeGod.id);
            addTraitToLocalizedLibrary(knowledgeGod.id, "Tis's The God Of Knowledges!");

            ActorTrait starsGod = new ActorTrait();
            starsGod.id = "God Of the Stars";
            starsGod.path_icon = "ui/icons/achievements/achievements_thedemon";
            starsGod.base_stats[S.damage] += 10f;
            starsGod.base_stats[S.health] = 600;
            starsGod.base_stats[S.attack_speed] += 1f;
            starsGod.base_stats[S.critical_chance] += 0.05f;
            starsGod.base_stats[S.intelligence] += 3f;
            //GOD OF STARS IS THE AGE OF THE MOON THUS WILL BE ICE/COLD BASED, ICE ATTACKS AND FREEZING ABILITIES FROM THE WIZARD
            AssetManager.traits.add(starsGod);
            PlayerConfig.unlockTrait(starsGod.id);
            addTraitToLocalizedLibrary(starsGod.id, "Now Cometh the Age of stars, A Thousand Year Voyage under the wisdom of the moon");

            ActorTrait subGod = new ActorTrait();
            subGod.id = "God Of You Decide";
            subGod.path_icon = "ui/icons/achievements/achievements_thedemon";
            subGod.base_stats[S.damage] += 5f;
            subGod.base_stats[S.health] = 400;
            subGod.base_stats[S.attack_speed] += 1f;
            subGod.base_stats[S.critical_chance] += 0.05f;
            AssetManager.traits.add(subGod);
            PlayerConfig.unlockTrait(subGod.id);
            addTraitToLocalizedLibrary(subGod.id, "Now Cometh the Age of stars, A Thousand Year Voyage under the wisdom of the moon");


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
        
            Actor pActor = (Actor)pTarget;
            World.world.eraManager.setEra(S.age_dark, true);


            return true;

        }

        public static bool darkGodsDeath(BaseSimObject pTarget, WorldTile pTile = null)
        {
        
            Actor pActor = (Actor)pTarget;
            World.world.eraManager.setEra(S.age_sun, true);


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

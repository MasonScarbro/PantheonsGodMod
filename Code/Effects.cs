/* 
AUTHOR: MASON SCARBRO
VERSION: 1.0.0
*/
using System.Collections.Generic;
using ReflectionUtility;

namespace GodsAndPantheons
{
    class Effects
    { 
        public static void init()
        {

            StatusEffect sunGodEra = new StatusEffect();
            sunGodEra.id = "Lights_Prevail";
            sunGodEra.duration = 7000f;
            sunGodEra.base_stats[S.armor] += 25;
            sunGodEra.base_stats[S.health] += 500;
            sunGodEra.base_stats[S.speed] += 300;
            sunGodEra.base_stats[S.knockback_reduction] += 0.5f;
            sunGodEra.base_stats[S.knockback] += 1f;
            sunGodEra.base_stats[S.attack_speed] += 80f;
            sunGodEra.base_stats[S.dodge] += 80f;
            sunGodEra.path_icon = "ui/icons/lightGod";
            sunGodEra.description = "The World, Light up";
            sunGodEra.name = "status_title_Lights_Prevail";
            sunGodEra.action_interval = 2;
            sunGodEra.action = new WorldAction(Traits.SuperRegeneration);
            localizeStatus(sunGodEra.id, "Lights_Prevail", sunGodEra.description); // Localizes the status effect
            AssetManager.status.add(sunGodEra);


            StatusEffect darkGodEra = new StatusEffect();
            darkGodEra.id = "Nights_Prevail";
            darkGodEra.duration = 7000f;
            darkGodEra.base_stats[S.mod_armor] += 0.55f;
            darkGodEra.base_stats[S.health] += 500;
            darkGodEra.base_stats[S.speed] += 30;
            darkGodEra.base_stats[S.knockback_reduction] += 0.6f;
            darkGodEra.base_stats[S.knockback] += 2f;
            darkGodEra.base_stats[S.attack_speed] += 8f;
            darkGodEra.path_icon = "ui/icons/godDark";
            darkGodEra.description = "The world, shrouded in my domain";
            darkGodEra.name = "status_title_Nights_Prevail";
            darkGodEra.action_interval = 2;
            darkGodEra.action = new WorldAction(Traits.SuperRegeneration);
            localizeStatus(darkGodEra.id, "Nights_Prevail", darkGodEra.description); // Localizes the status effect
            AssetManager.status.add(darkGodEra);

	        StatusEffect GodOfGodsEra = new StatusEffect();
            GodOfGodsEra.id = "God_Of_All";
            GodOfGodsEra.duration = 7000f;
            GodOfGodsEra.base_stats[S.mod_armor] = 0.5f;
	        GodOfGodsEra.base_stats[S.mod_damage] = 0.5f;
	        GodOfGodsEra.base_stats[S.range] += 10;
            GodOfGodsEra.base_stats[S.mod_health] = 0.5f;
            GodOfGodsEra.base_stats[S.speed] += 70;
            GodOfGodsEra.base_stats[S.knockback_reduction] += 0.5f;
            GodOfGodsEra.base_stats[S.mod_crit] = 0.5f;
            GodOfGodsEra.base_stats[S.attack_speed] += 40f;
	        GodOfGodsEra.base_stats[S.scale] += 0.075f;
            GodOfGodsEra.base_stats[S.dodge] += 40f;
            GodOfGodsEra.path_icon = "ui/icons/GodofGods";
            GodOfGodsEra.description = "Now i have become death, destroyer of worlds";
            GodOfGodsEra.name = "God Of All";
            GodOfGodsEra.action_interval = 2;
            GodOfGodsEra.action = new WorldAction(Traits.SuperRegeneration);
            localizeStatus(GodOfGodsEra.id, "God_Of_All", GodOfGodsEra.description); // Localizes the status effect
            AssetManager.status.add(GodOfGodsEra);

            StatusEffect knowledgeGodEra = new StatusEffect();
            knowledgeGodEra.id = "Knowledge_Prevail";
            knowledgeGodEra.duration = 7000f;
            knowledgeGodEra.base_stats[S.mod_armor] = 0.3f;
            knowledgeGodEra.base_stats[S.health] += 500;
            knowledgeGodEra.base_stats[S.speed] += 20;
            knowledgeGodEra.base_stats[S.knockback_reduction] += 0.9f;
            knowledgeGodEra.base_stats[S.knockback] += 1f;
            knowledgeGodEra.base_stats[S.attack_speed] += 20f;
            knowledgeGodEra.base_stats[S.intelligence] += 200f;
            knowledgeGodEra.base_stats[S.stewardship] += 20f;
            knowledgeGodEra.base_stats[S.dodge] += 20f;
            knowledgeGodEra.base_stats[S.range] += 40f;
            knowledgeGodEra.base_stats[S.accuracy] += 20f;
            knowledgeGodEra.path_icon = "ui/icons/knowledgeGod";
            knowledgeGodEra.description = "The era of Knowledge has come to pass";
            knowledgeGodEra.name = "status_title_Knowledge_Prevail";
            knowledgeGodEra.action_interval = 2;
            knowledgeGodEra.action = new WorldAction(Traits.SuperRegeneration);
            localizeStatus(knowledgeGodEra.id, "Knowledge_Prevail", knowledgeGodEra.description); // Localizes the status effect
            AssetManager.status.add(knowledgeGodEra);

            StatusEffect starsGodEra = new StatusEffect();
            starsGodEra.id = "Stars_Prevail";
            starsGodEra.duration = 7000f;
            starsGodEra.base_stats[S.health] += 500;
            starsGodEra.base_stats[S.speed] += 30;
            starsGodEra.base_stats[S.knockback_reduction] += 0.8f;
            starsGodEra.base_stats[S.knockback] += 1f;
            starsGodEra.base_stats[S.mod_armor] += 0.2f;
            starsGodEra.base_stats[S.attack_speed] += 80f;
            starsGodEra.path_icon = "ui/icons/starsGod";
            starsGodEra.description = "The Age Of Stars is Apon Us";
            starsGodEra.name = "status_title_Stars_Prevail";
            starsGodEra.action_interval = 20;
            starsGodEra.action = new WorldAction(Traits.SuperRegeneration);
            localizeStatus(starsGodEra.id, "Stars_Prevail", starsGodEra.description); // Localizes the status effect
            AssetManager.status.add(starsGodEra);

            StatusEffect EarthGodEra = new StatusEffect();
            EarthGodEra.id = "Earth Prevails";
            EarthGodEra.duration = 7000f;
            EarthGodEra.base_stats[S.health] += 500;
            EarthGodEra.base_stats[S.mod_speed] += 0.25f;
            EarthGodEra.base_stats[S.knockback_reduction] += 0.8f;
            EarthGodEra.base_stats[S.knockback] += 1f;
            EarthGodEra.base_stats[S.mod_armor] += 0.4f;
            EarthGodEra.base_stats[S.attack_speed] += 80f;
            EarthGodEra.path_icon = "ui/icons/earthGod";
            EarthGodEra.description = "The World, shrowded in my home";
            EarthGodEra.name = "Earth Prevails";
            EarthGodEra.action_interval = 2;
            EarthGodEra.action = new WorldAction(Traits.SuperRegeneration);
            localizeStatus(EarthGodEra.id, "Earth Prevails", EarthGodEra.description); // Localizes the status effect
            AssetManager.status.add(EarthGodEra);

            StatusEffect warGodEra = new StatusEffect();
            warGodEra.id = "Despair Prevails";
            warGodEra.duration = 7000f;
            warGodEra.base_stats[S.mod_armor] += .5f;
            warGodEra.base_stats[S.health] += 500;
            warGodEra.base_stats[S.speed] += 30;
            warGodEra.base_stats[S.knockback_reduction] += 2f;
            warGodEra.base_stats[S.knockback] += 2f;
            warGodEra.base_stats[S.attack_speed] += 8f;
            warGodEra.base_stats[S.damage] += 30f;
            warGodEra.path_icon = "ui/icons/warGod";
            warGodEra.description = "There is Power in despair for some";
            warGodEra.name = "Despair Prevails";
            warGodEra.action_interval = 2;
            warGodEra.action = new WorldAction(Traits.SuperRegeneration);
            localizeStatus(warGodEra.id, "Despair Prevails", warGodEra.description); // Localizes the status effect
            AssetManager.status.add(warGodEra);

            StatusEffect lichgodera = new StatusEffect
            {
                id = "Sorrow Prevails",
                duration = 7000f,
            };
            lichgodera.base_stats[S.mod_armor] += 0.5f;
            lichgodera.base_stats[S.health] += 500;
            lichgodera.base_stats[S.speed] += 30;
            lichgodera.base_stats[S.knockback_reduction] += 2f;
            lichgodera.base_stats[S.knockback] += 2f;
            lichgodera.base_stats[S.attack_speed] += 8f;
            lichgodera.base_stats[S.mod_damage] += 0.3f;
            lichgodera.path_icon = "ui/icons/lichGod";
            lichgodera.description = "Sorrow is my Power";
            lichgodera.name = "Sorrow Prevails";
            lichgodera.action_interval = 2;
            lichgodera.action = new WorldAction(Traits.SuperRegeneration);
            localizeStatus(lichgodera.id, "Sorrow Prevails", lichgodera.description); // Localizes the status effect
            AssetManager.status.add(lichgodera);

            StatusEffect chaosgodsera = new StatusEffect();
            chaosgodsera.duration = 7000f;
            chaosgodsera.id = "Chaos Prevails";
            chaosgodsera.path_icon = "ui/icons/chaosGod";
            chaosgodsera.name = "Chaos Prevails";
            chaosgodsera.description = "Chaos Sooths My Soul";
            chaosgodsera.base_stats[S.mod_armor] += 0.5f;
            chaosgodsera.base_stats[S.health] += 500;
            chaosgodsera.base_stats[S.speed] += 30;
            chaosgodsera.base_stats[S.knockback_reduction] += 2f;
            chaosgodsera.base_stats[S.knockback] += 2f;
            chaosgodsera.base_stats[S.attack_speed] += 8f;
            chaosgodsera.base_stats[S.damage] += 30f;
            chaosgodsera.action_interval = 2;
            chaosgodsera.action = new WorldAction(Traits.SuperRegeneration);
            localizeStatus(chaosgodsera.id, "Chaos Prevails", chaosgodsera.description); // Localizes the status effect
            AssetManager.status.add(chaosgodsera);
            StatusEffect warGodsCry = new StatusEffect();
            warGodsCry.id = "WarGodsCry";
            warGodsCry.duration = 7000f;
            warGodsCry.base_stats[S.mod_armor] += 0.40f;
            warGodsCry.base_stats[S.health] += 60;
            warGodsCry.base_stats[S.speed] += 10;
            warGodsCry.base_stats[S.knockback_reduction] += 3f;
            warGodsCry.base_stats[S.knockback] += 2f;
            warGodsCry.base_stats[S.attack_speed] += 8f;
            warGodsCry.base_stats[S.damage] += 15f;
            warGodsCry.path_icon = "ui/icons/warGod";
            warGodsCry.description = "A Cry Of Anger and Rage";
            warGodsCry.name = "WarGodsCry";
            
            localizeStatus(warGodsCry.id, "WarGodsCry", warGodsCry.description); // Localizes the status effect
            AssetManager.status.add(warGodsCry);


            /*
            ProjectileAsset Test = new ProjectileAsset();
            Usoppball.id = "Test";
		    Usoppball.texture = "effects/projectiles/Test";
		    Usoppball.trailEffect_enabled = false;
		    Usoppball.look_at_target = true;
            Usoppball.parabolic = false;
            Usoppball.hitShake = true;
		    Usoppball.looped = false;
		    Usoppball.speed = 1f;
            Usoppball.startScale = 0.1f;
            Usoppball.targetScale = 0.1f;
            AssetManager.projectiles.add(Usoppball);


            

            */

        }

        public static void localizeStatus(string id, string name, string description)
      	{
      		Dictionary<string, string> localizedText = Reflection.GetField(LocalizedTextManager.instance.GetType(), LocalizedTextManager.instance, "localizedText") as Dictionary<string, string>;
            localizedText.Add(name, id);
            localizedText.Add(description, description);
        }
    }

}

using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using NCMS;
using NCMS.Utils;
using UnityEngine;
using ReflectionUtility;
using HarmonyLib;
using ai;

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
            sunGodEra.base_stats[S.knockback_reduction] += 10f;
            sunGodEra.base_stats[S.knockback] += 1f;
            sunGodEra.base_stats[S.attack_speed] += 80f;
            sunGodEra.base_stats[S.dodge] += 80f;
            sunGodEra.path_icon = "ui/icons/lightGod";
            sunGodEra.description = "status_description_Lights_Prevail";
            sunGodEra.name = "status_title_Lights_Prevail";
            localizeStatus(sunGodEra.id, "Lights_Prevail", sunGodEra.description); // Localizes the status effect
            AssetManager.status.add(sunGodEra);


            StatusEffect darkGodEra = new StatusEffect();
            darkGodEra.id = "Nights_Prevail";
            darkGodEra.duration = 7000f;
            darkGodEra.base_stats[S.armor] += 55;
            darkGodEra.base_stats[S.health] += 500;
            darkGodEra.base_stats[S.speed] += 30;
            darkGodEra.base_stats[S.knockback_reduction] += 10f;
            darkGodEra.base_stats[S.knockback] += 2f;
            darkGodEra.base_stats[S.armor] += 50f;
            darkGodEra.base_stats[S.attack_speed] += 8f;
            darkGodEra.path_icon = "ui/icons/godDark";
            darkGodEra.description = "status_description_Nights_Prevail";
            darkGodEra.name = "status_title_Nights_Prevail";
            localizeStatus(darkGodEra.id, "Nights_Prevail", darkGodEra.description); // Localizes the status effect
            AssetManager.status.add(darkGodEra);

            StatusEffect knowledgeGodEra = new StatusEffect();
            knowledgeGodEra.id = "Knowledge_Prevail";
            knowledgeGodEra.duration = 7000f;
            knowledgeGodEra.base_stats[S.armor] += 30;
            knowledgeGodEra.base_stats[S.health] += 500;
            knowledgeGodEra.base_stats[S.speed] += 20;
            knowledgeGodEra.base_stats[S.knockback_reduction] += 10f;
            knowledgeGodEra.base_stats[S.knockback] += 1f;
            knowledgeGodEra.base_stats[S.armor] += 50f;
            knowledgeGodEra.base_stats[S.attack_speed] += 20f;
            knowledgeGodEra.base_stats[S.intelligence] += 200f;
            knowledgeGodEra.base_stats[S.stewardship] += 20f;
            knowledgeGodEra.base_stats[S.dodge] += 20f;
            knowledgeGodEra.base_stats[S.range] += 40f;
            knowledgeGodEra.base_stats[S.accuracy] += 20f;
            knowledgeGodEra.path_icon = "ui/icons/knowledgeGod";
            knowledgeGodEra.description = "status_description_Knowledge_Prevail";
            knowledgeGodEra.name = "status_title_Knowledge_Prevail";
            localizeStatus(knowledgeGodEra.id, "Knowledge_Prevail", knowledgeGodEra.description); // Localizes the status effect
            AssetManager.status.add(knowledgeGodEra);

            StatusEffect starsGodEra = new StatusEffect();
            starsGodEra.id = "Stars_Prevail";
            starsGodEra.duration = 7000f;
            starsGodEra.base_stats[S.armor] += 50;
            starsGodEra.base_stats[S.health] += 500;
            starsGodEra.base_stats[S.speed] += 30;
            starsGodEra.base_stats[S.knockback_reduction] += 30f;
            starsGodEra.base_stats[S.knockback] += 1f;
            starsGodEra.base_stats[S.armor] += 50f;
            starsGodEra.base_stats[S.attack_speed] += 80f;
            starsGodEra.path_icon = "ui/icons/starsGod";
            starsGodEra.description = "status_description_Stars_Prevail";
            starsGodEra.name = "status_title_Stars_Prevail";
            localizeStatus(starsGodEra.id, "Stars_Prevail", starsGodEra.description); // Localizes the status effect
            AssetManager.status.add(starsGodEra);

            StatusEffect warGodEra = new StatusEffect();
            warGodEra.id = "War_Prevail";
            warGodEra.duration = 7000f;
            warGodEra.base_stats[S.armor] += 50;
            warGodEra.base_stats[S.health] += 500;
            warGodEra.base_stats[S.speed] += 30;
            warGodEra.base_stats[S.knockback_reduction] += 30f;
            warGodEra.base_stats[S.knockback] += 2f;
            warGodEra.base_stats[S.armor] += 50f;
            warGodEra.base_stats[S.attack_speed] += 8f;
            warGodEra.base_stats[S.damage] += 30f;
            warGodEra.path_icon = "ui/icons/starsGod";
            warGodEra.description = "status_description_Stars_Prevail";
            warGodEra.name = "status_title_Stars_Prevail";
            localizeStatus(warGodEra.id, "Stars_Prevail", warGodEra.description); // Localizes the status effect
            AssetManager.status.add(warGodEra);

        }

        public static void localizeStatus(string id, string name, string description)
      	{
      		Dictionary<string, string> localizedText = Reflection.GetField(LocalizedTextManager.instance.GetType(), LocalizedTextManager.instance, "localizedText") as Dictionary<string, string>;
      		localizedText.Add("status_title_" + id, name);
      		localizedText.Add("status_description_" + id, description);
        }
    }

}
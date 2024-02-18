using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NCMS;
using NCMS.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ReflectionUtility;
using HarmonyLib;

namespace GodsAndPantheons
{
    class Units
    {
        public static void init()
        {
            
            loadAssets();
        }

        public static void loadAssets()
        {
            


            var godHunter = AssetManager.actor_library.clone("GodHunter", "_mob");
            godHunter.nameLocale = "GodHunter";
            godHunter.nameTemplate = "human_name";
            godHunter.race = SK.human;
            godHunter.kingdom = SK.evil;
            godHunter.base_stats[S.max_age] = 1000;
            godHunter.base_stats[S.knockback_reduction] = 10f;
            godHunter.base_stats[S.critical_chance] = 0.1f;
            godHunter.base_stats[S.attack_speed] = 40f;
            godHunter.base_stats[S.knockback] = 1f;
            godHunter.base_stats[S.accuracy] = 8f;
            godHunter.base_stats[S.health] = 600;
            godHunter.base_stats[S.speed] = 30f;
            godHunter.base_stats[S.damage] = 50;
            godHunter.base_stats[S.targets] = 3;
            godHunter.base_stats[S.dodge] = 1f;
            godHunter.base_stats[S.armor] = 5f;
            godHunter.body_separate_part_hands = true;
            godHunter.canBeKilledByDivineLight = false;
            godHunter.canBeKilledByLifeEraser = true;
            godHunter.ignoredByInfinityCoin = false;
            godHunter.disableJumpAnimation = true;
            godHunter.canBeMovedByPowers = true;
            godHunter.canTurnIntoZombie = false;
            godHunter.canAttackBuildings = true;
            godHunter.hideFavoriteIcon = false;
            godHunter.can_edit_traits = true;
            godHunter.very_high_flyer = false;
            godHunter.damagedByOcean = false;
            godHunter.swampCreature = false;
            godHunter.damagedByRain = false;
            godHunter.oceanCreature = false;
            godHunter.landCreature = true;
            godHunter.dieOnGround = false;
            godHunter.take_items = false;
            godHunter.use_items = true;
            godHunter.diet_meat = false;
            godHunter.dieInLava = false;
            godHunter.needFood = false;
            godHunter.has_soul = true;
            godHunter.flying = false;
            godHunter.action_liquid = new WorldAction(ActionLibrary.swimToIsland);
            godHunter.defaultWeapons = List.Of<string>("DarkDagger");
            godHunter.defaultWeaponsMaterial = List.Of<string>(new string[] { "adamantine" });
            godHunter.animation_walk = "walk_0, walk_1, walk_2, walk_3";
            godHunter.animation_swim = "swim_0,swim_1";
            godHunter.texture_path = "GodHunter";
            godHunter.icon = "godKiller";
            godHunter.job = "attacker";
            AssetManager.actor_library.add(godHunter);
            AssetManager.actor_library.CallMethod("loadShadow", godHunter);;
            AssetManager.actor_library.CallMethod("addTrait", "immortal");
            AssetManager.actor_library.CallMethod("addTrait", "fire_proof");
            Localization.addLocalization(godHunter.nameLocale, godHunter.nameLocale);
        }
    }
}
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
            KingdomAsset ghk = new KingdomAsset();
            ghk.id = "GodHunters";
            ghk.mobs = true;
            ghk.addTag("GodHunters");
            ghk.addFriendlyTag("GodHunters");
            ghk.addFriendlyTag("SK.dragons");
            ghk.addFriendlyTag("SK.undead");
            ghk.addEnemyTag("SK.demons");
            AssetManager.kingdoms.add(ghk);
            MapBox.instance.kingdoms.CallMethod("newHiddenKingdom", ghk);

            NameGeneratorAsset ghn = new NameGeneratorAsset();
            ghn.id = "GodSkin_Fatty";
            ghn.part_groups.Add("GodSkin_Fatty");
            ghn.templates.Add("part_group");
            AssetManager.nameGenerator.add(ghn);

            loadAssets();
        }

        public static void loadAssets()
        {
            


            var godHunter = AssetManager.actor_library.clone("GodHunter", "_mob");
            godHunter.nameLocale = "GodHunter";
            godHunter.nameTemplate = "alien_name";
            godHunter.job = "move_mob";
            godHunter.race = "GodHunters";
            godHunter.defaultAttack = "GodHuntersScythe";
            godHunter.kingdom = "GodHunters";
            godHunter.skeletonID = "skeleton_cursed";
            godHunter.zombieID = "zombie";
            godHunter.icon = "walk_0";
            godHunter.animation_swim = "swim_0,swim_1,swim_2,swim_3";
            godHunter.animation_walk = "walk_0,walk_1,walk_2,walk_3";
            godHunter.texture_path = "GodHunter";
            godHunter.defaultAttack = "base";
            godHunter.run_to_water_when_on_fire = true;
            godHunter.canBeKilledByStuff = true;
            godHunter.canBeKilledByLifeEraser = true;
            godHunter.canAttackBuildings = false;
            godHunter.canBeMovedByPowers = true;
            godHunter.canBeHurtByPowers = true;
            godHunter.canTurnIntoZombie = false;
            godHunter.canBeInspected = true;
            godHunter.hideOnMinimap = false;
            godHunter.use_items = true;
            godHunter.take_items = true;
            godHunter.needFood = false;
            godHunter.diet_meat = false;
            godHunter.inspect_home = true;
            godHunter.disableJumpAnimation = true;
            godHunter.has_soul = true;
            godHunter.swampCreature = false;
            godHunter.oceanCreature = false;
            godHunter.landCreature = true;
            godHunter.can_turn_into_demon_in_age_of_chaos = false;
            godHunter.canTurnIntoIceOne = false;
            godHunter.canTurnIntoTumorMonster = false;
            godHunter.canTurnIntoMush = false;
            godHunter.dieInLava = true;
            godHunter.dieOnBlocks = false;
            godHunter.dieOnGround = false;
            godHunter.dieByLightning = true;
            godHunter.damagedByOcean = false;
            godHunter.damagedByRain = false;
            godHunter.flying = false;
            godHunter.very_high_flyer = false;
            godHunter.hideFavoriteIcon = false;
            godHunter.can_edit_traits = true;
            godHunter.canBeKilledByDivineLight = true;
            godHunter.ignoredByInfinityCoin = true;
            godHunter.actorSize = ActorSize.S13_Human;
            godHunter.action_liquid = new WorldAction(ActionLibrary.swimToIsland);
            godHunter.base_stats[S.max_age] = 10000f;
            godHunter.base_stats[S.health] = 500;
            godHunter.base_stats[S.damage] = 30;
            godHunter.base_stats[S.speed] = 80f;
            godHunter.base_stats[S.armor] = 1;
            godHunter.base_stats[S.attack_speed] = 100f;
            godHunter.base_stats[S.critical_chance] = 0.1f;
            godHunter.base_stats[S.knockback] = 0.1f;
            godHunter.base_stats[S.knockback_reduction] = 0.1f;
            godHunter.base_stats[S.accuracy] = 1f;
            godHunter.base_stats[S.range] = 3;
            godHunter.base_stats[S.targets] = 1f;
            godHunter.base_stats[S.dodge] = 1f;
            
            AssetManager.actor_library.add(godHunter);
            AssetManager.actor_library.CallMethod("loadShadow", godHunter);;
            AssetManager.actor_library.CallMethod("addTrait", "immortal");
            AssetManager.actor_library.CallMethod("addTrait", "God Hunter");
            Localization.addLocalization(godHunter.nameLocale, godHunter.nameLocale);
        }
    }
}

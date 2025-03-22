using NCMS.Utils;
using ReflectionUtility;

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
            godHunter.job = "GodHunter";
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
            godHunter.canAttackBuildings = true;
            godHunter.canBeMovedByPowers = true;
            godHunter.canBeHurtByPowers = true;
            godHunter.canTurnIntoZombie = false;
            godHunter.canBeInspected = true;
            godHunter.hideOnMinimap = false;
            godHunter.use_items = true;
            godHunter.take_items = false;
            godHunter.needFood = false;
            godHunter.diet_meat = false;
            godHunter.inspect_home = true;
            godHunter.disableJumpAnimation = true;
            godHunter.has_soul = true;
            godHunter.swampCreature = false;
            godHunter.oceanCreature = true;
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
            godHunter.ignoredByInfinityCoin = false;
            godHunter.actorSize = ActorSize.S13_Human;
            godHunter.action_liquid = new WorldAction(ActionLibrary.swimToIsland);
            godHunter.base_stats[S.max_age] = 10000f;
            godHunter.base_stats[S.health] = 200;
            godHunter.base_stats[S.damage] = 35f;
            godHunter.base_stats[S.speed] = 75f;
            godHunter.base_stats[S.attack_speed] = 55f;
            godHunter.base_stats[S.critical_chance] = 0.25f;
            godHunter.base_stats[S.knockback] = 0.1f;
            godHunter.base_stats[S.knockback_reduction] = 0.1f;
            godHunter.base_stats[S.accuracy] = 8f;
            godHunter.base_stats[S.range] = 1f;
            godHunter.base_stats[S.targets] = 1f;
            godHunter.base_stats[S.dodge] = 1f;
            
            AssetManager.actor_library.add(godHunter);
            AssetManager.actor_library.CallMethod("loadShadow", godHunter);
            AssetManager.actor_library.CallMethod("addTrait", "immortal");
            AssetManager.actor_library.CallMethod("addTrait", "God Hunter");
            Localization.addLocalization(godHunter.nameLocale, godHunter.nameLocale);


            var darkOne = AssetManager.actor_library.clone("DarkOne", "_mob");
            darkOne.nameLocale = "DarkOne";
            darkOne.nameTemplate = "alien_name";
            darkOne.job = "random_move";
            darkOne.race = SK.undead;
            darkOne.defaultAttack = "base";
            darkOne.kingdom = SK.undead;
            darkOne.skeletonID = "skeleton_cursed";
            darkOne.zombieID = "zombie";
            darkOne.icon = "walk_0";
            darkOne.animation_swim = "walk_0,walk_1,walk_2,walk_3";
            darkOne.animation_walk = "walk_0,walk_1,walk_2,walk_3";
            darkOne.texture_path = "DarkOne";
            darkOne.run_to_water_when_on_fire = true;
            darkOne.canBeKilledByStuff = true;
            darkOne.canBeKilledByLifeEraser = true;
            darkOne.canAttackBuildings = false;
            darkOne.canBeMovedByPowers = true;
            darkOne.canBeHurtByPowers = true;
            darkOne.canTurnIntoZombie = false;
            darkOne.canBeInspected = true;
            darkOne.hideOnMinimap = false;
            darkOne.use_items = false;
            darkOne.take_items = false; //doesn't have hands
            darkOne.needFood = false;
            darkOne.diet_meat = false;
            darkOne.inspect_home = true;
            darkOne.disableJumpAnimation = true;
            darkOne.has_soul = true;
            darkOne.swampCreature = false;
            darkOne.oceanCreature = false;
            darkOne.landCreature = true;
            darkOne.can_turn_into_demon_in_age_of_chaos = false;
            darkOne.canTurnIntoIceOne = false;
            darkOne.canTurnIntoTumorMonster = false;
            darkOne.canTurnIntoMush = false;
            darkOne.dieInLava = true;
            darkOne.dieOnBlocks = false;
            darkOne.dieOnGround = false;
            darkOne.dieByLightning = true;
            darkOne.damagedByOcean = false;
            darkOne.damagedByRain = false;
            darkOne.flying = false;
            darkOne.very_high_flyer = false;
            darkOne.hideFavoriteIcon = false;
            darkOne.can_edit_traits = true;
            darkOne.canBeKilledByDivineLight = false;
            darkOne.ignoredByInfinityCoin = false;
            darkOne.actorSize = ActorSize.S13_Human;
            darkOne.action_liquid = new WorldAction(ActionLibrary.swimToIsland);
            darkOne.base_stats[S.max_age] = 1f;
            darkOne.base_stats[S.health] = 100;
            darkOne.base_stats[S.damage] = 25f;
            darkOne.base_stats[S.speed] = 35f;
            darkOne.base_stats[S.armor] = 0f;
            darkOne.base_stats[S.attack_speed] = 50f;
            darkOne.base_stats[S.critical_chance] = 0.1f;
            darkOne.base_stats[S.knockback] = 0.1f;
            darkOne.base_stats[S.knockback_reduction] = 0.1f;
            darkOne.base_stats[S.accuracy] = 8f;
            darkOne.base_stats[S.range] = 1f;
            darkOne.base_stats[S.targets] = 1f;
            darkOne.base_stats[S.dodge] = 15f;

            AssetManager.actor_library.add(darkOne);
            AssetManager.actor_library.CallMethod("loadShadow", darkOne); ;
            Localization.addLocalization(darkOne.nameLocale, darkOne.nameLocale);
        }
    }
}

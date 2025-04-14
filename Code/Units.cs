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
            ghk.default_kingdom_color = new ColorAsset("#AAAAAA");
            ghk.addTag("GodHunters");
            ghk.addFriendlyTag("GodHunters");
            ghk.addFriendlyTag(SK.dragons);
            ghk.addFriendlyTag(SK.undead);
            AssetManager.kingdoms.add(ghk);
            World.world.kingdoms_wild.newWildKingdom(ghk);
            loadAssets();
        }

        public static void loadAssets()
        {
            var godHunter = AssetManager.actor_library.clone("GodHunter", "$mob$");
            godHunter.name_locale = "GodHunter";
            godHunter.use_phenotypes = false;
            godHunter.unit_other = true;
            godHunter.has_advanced_textures = false;
            godHunter.check_flip = delegate { return true; };
            godHunter.name_template_sets = new string[] { "alien_set" };
            godHunter.job = new string[] { "GodHunter" };
            godHunter.kingdom_id_wild = "GodHunters";
            godHunter.icon = "walk_0";
            godHunter.animation_swim = new string[] { "swim_0","swim_1","swim_2","swim_3" };
            godHunter.animation_walk = new string[] { "walk_0", "walk_1", "walk_2", "walk_3" };
            godHunter.texture_id = "GodHunter";
            godHunter.run_to_water_when_on_fire = true;
            godHunter.can_be_killed_by_stuff = true;
            godHunter.can_be_killed_by_life_eraser = true;
            godHunter.can_attack_buildings = true;
            godHunter.can_be_moved_by_powers = true;
            godHunter.can_be_hurt_by_powers = true;
            godHunter.can_turn_into_zombie = false;
            godHunter.can_be_inspected = true;
            godHunter.visible_on_minimap = true;
            godHunter.use_items = true;
            godHunter.take_items = false;
            godHunter.can_evolve_into_new_species = false;
            godHunter.can_have_subspecies = false;
            godHunter.can_talk_with = false;
            godHunter.control_can_talk = false;
            godHunter.can_have_subspecies = false;
            godHunter.inspect_home = false;
            godHunter.disable_jump_animation = true;
            godHunter.has_soul = true;
            godHunter.force_ocean_creature = true;
            godHunter.force_land_creature = true;
            godHunter.can_turn_into_demon_in_age_of_chaos = false;
            godHunter.can_turn_into_ice_one = false;
            godHunter.can_turn_into_tumor = false;
            godHunter.can_turn_into_mush = false;
            godHunter.die_in_lava = false;
            godHunter.die_on_blocks = false;
            godHunter.die_by_lightning = true;
            godHunter.damaged_by_ocean = false;
            godHunter.flying = false;
            godHunter.very_high_flyer = false;
            godHunter.hide_favorite_icon = false;
            godHunter.can_edit_traits = true;
            godHunter.can_be_killed_by_divine_light = true;
            godHunter.icon = "godKiller";
            godHunter.ignored_by_infinity_coin = false;
            godHunter.actor_size = ActorSize.S13_Human;
            godHunter.base_stats[S.lifespan] = 10000f;
            godHunter.base_stats[S.health] = 200;
            godHunter.base_stats[S.damage] = 35f;
            godHunter.base_stats[S.speed] = 75f;
            godHunter.base_stats[S.attack_speed] = 55f;
            godHunter.base_stats[S.mass] = 10;
            godHunter.base_stats[S.critical_chance] = 0.25f;
            godHunter.base_stats[S.knockback] = 0.1f;
            godHunter.base_stats[S.accuracy] = 8f;
            godHunter.base_stats[S.range] = 1f;
            godHunter.base_stats[S.targets] = 1f;
            AssetManager.actor_library.loadShadow(godHunter);
            AssetManager.actor_library.loadTexturesAndSprites(godHunter);
            godHunter.addTrait("immortal");
            godHunter.addTrait("God Hunter");
            godHunter.addTrait("blessed");
            godHunter.addTrait("tough");
            Localization.Add(godHunter.name_locale, godHunter.name_locale);


            var darkOne = AssetManager.actor_library.clone("DarkOne", "$mob$");
            darkOne.name_locale = "DarkOne";
            darkOne.use_phenotypes = false;
            darkOne.unit_other = true;
            darkOne.has_advanced_textures = false;
            darkOne.check_flip = delegate { return true; };
            darkOne.name_template_sets = new string[] { "alien_set" };
            darkOne.job = new string[] { "random_move" };
            darkOne.kingdom_id_wild = "neutral";
            darkOne.icon = "walk_0";
            darkOne.animation_swim = new string[] { "swim_0", "swim_1", "swim_2", "swim_3" };
            darkOne.animation_walk = new string[] { "walk_0", "walk_1", "walk_2", "walk_3" };
            darkOne.texture_id = "DarkOne";
            darkOne.run_to_water_when_on_fire = true;
            darkOne.can_be_killed_by_stuff = true;
            darkOne.can_be_killed_by_life_eraser = true;
            darkOne.can_attack_buildings = true;
            darkOne.can_be_moved_by_powers = true;
            darkOne.can_be_hurt_by_powers = true;
            darkOne.can_turn_into_zombie = false;
            darkOne.can_be_inspected = true;
            darkOne.visible_on_minimap = true;
            darkOne.use_items = true;
            darkOne.take_items = false;
            darkOne.can_evolve_into_new_species = false;
            darkOne.can_have_subspecies = false;
            darkOne.can_talk_with = false;
            darkOne.control_can_talk = false;
            darkOne.can_have_subspecies = false;
            darkOne.inspect_home = false;
            darkOne.disable_jump_animation = true;
            darkOne.has_soul = true;
            darkOne.force_ocean_creature = true;
            darkOne.force_land_creature = true;
            darkOne.can_turn_into_demon_in_age_of_chaos = false;
            darkOne.can_turn_into_ice_one = false;
            darkOne.can_turn_into_tumor = false;
            darkOne.can_turn_into_mush = false;
            darkOne.die_in_lava = false;
            darkOne.die_on_blocks = false;
            darkOne.die_by_lightning = true;
            darkOne.damaged_by_ocean = false;
            darkOne.flying = false;
            darkOne.very_high_flyer = false;
            darkOne.hide_favorite_icon = false;
            darkOne.can_edit_traits = true;
            darkOne.can_be_killed_by_divine_light = true;
            darkOne.ignored_by_infinity_coin = false;
            darkOne.actor_size = ActorSize.S13_Human;
            darkOne.base_stats[S.lifespan] = 1f;
            darkOne.base_stats[S.health] = 100;
            darkOne.base_stats[S.damage] = 25f;
            darkOne.base_stats[S.speed] = 35f;
            darkOne.base_stats[S.mass] = 20f;
            darkOne.base_stats[S.attack_speed] = 50f;
            darkOne.base_stats[S.critical_chance] = 0.1f;
            darkOne.base_stats[S.knockback] = 0.1f;
            darkOne.base_stats[S.accuracy] = 8f;
            darkOne.base_stats[S.range] = 1f;
            darkOne.base_stats[S.targets] = 1f;
            AssetManager.actor_library.loadTexturesAndSprites(darkOne);
            AssetManager.actor_library.loadShadow(darkOne);
            Localization.Add(darkOne.name_locale, darkOne.name_locale);
        }
    }
}

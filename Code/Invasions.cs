using NeoModLoader.General;

namespace GodsAndPantheons
{
    class Invasions
    {
        public static void init()
        {
            WorldLogAsset worldLog = AssetManager.world_log_library.add(new WorldLogAsset() { id = "GodHunters"});
            worldLog.text_replacer = GetText;
            worldLog.path_icon = "ui/icons/godKiller";
            DisasterAsset hunterInvasion = AssetManager.disasters.add(new DisasterAsset());
            hunterInvasion.id = "GodHunters";
            hunterInvasion.rate = 7;
            hunterInvasion.chance = 0.9f;
            hunterInvasion.min_world_cities = 9;
            hunterInvasion.max_existing_units = 2;
            hunterInvasion.world_log = "GodHunters";
            LM.AddToCurrentLocale("GodHunters", "GodHunters");
            hunterInvasion.min_world_population = 0;
            hunterInvasion.spawn_asset_unit = "GodHunter";
            hunterInvasion.units_min = 2;
            hunterInvasion.units_max = 5;
            hunterInvasion.ages_forbid.Add("age_hope");
            hunterInvasion.action = new DisasterAction(AssetManager.disasters.simpleUnitAssetSpawnUsingIslands);
        }
        public static void GetText(WorldLogMessage log, ref string s)
        {
            s = "A group of God Hunters have Come From Another World Seeking Gods";
        }
    }

}
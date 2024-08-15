namespace GodsAndPantheons
{
    class Invasions
    {
        public static void init()
        {
            DisasterAsset hunterInvasion = new DisasterAsset();
            hunterInvasion.id = "trollsv1";
            hunterInvasion.rate = 7;
            hunterInvasion.chance = 0.9f;
            hunterInvasion.min_world_cities = 9;
            hunterInvasion.max_existing_units = 2;
            hunterInvasion.world_log = "A group of God Hunters have Come From Another World Seeking Gods";
            hunterInvasion.world_log_icon = "godKiller";
            hunterInvasion.min_world_population = 0;
            hunterInvasion.spawn_asset_unit = "GodHunter";
            hunterInvasion.units_min = 2;
            hunterInvasion.units_max = 5;
            hunterInvasion.ages_forbid.Add(S.age_hope);
            hunterInvasion.action = new DisasterAction(AssetManager.disasters.simpleUnitAssetSpawnUsingIslands);
            AssetManager.disasters.add(hunterInvasion);
        }

    }

}
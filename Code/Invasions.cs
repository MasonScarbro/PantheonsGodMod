namespace GodsAndPantheons
{
    class Invasions
    {
        public static Actor nextInvader;
        public static void init()
        {
            DisasterAsset hunterInvasion = new DisasterAsset();
            hunterInvasion.id = "trollsv1";
            hunterInvasion.rate = 7;
            hunterInvasion.chance = 0.9f;
            hunterInvasion.min_world_cities = 9;
            hunterInvasion.max_existing_units = 2;
            hunterInvasion.world_log = "A God Hunter Comes From Another World Seeking Gods";
            hunterInvasion.world_log_icon = "godKiller";
            hunterInvasion.min_world_population = 0;
            hunterInvasion.spawn_asset_unit = "GodHunter";
            hunterInvasion.units_min = 1;
            hunterInvasion.units_max = 3;
            hunterInvasion.ages_forbid.Add(S.age_hope);
            hunterInvasion.action = new DisasterAction(AssetManager.disasters.simpleUnitAssetSpawnUsingIslands);
            AssetManager.disasters.add(hunterInvasion);
        }

    }

}
using NCMS.Utils;
using UnityEngine;
using ReflectionUtility;

namespace GodsAndPantheons
{
    class Buttons
    {
        public static void init()
        {
            
            Tab.createTab("Button Tab_GodsAndPantheons", "Tab_GodsAndPantheons", "GodsAndPantheons", "Gods Here", -150);
            loadButtons();
        }
        private static void loadButtons()
        {
            PowersTab tab = getPowersTab("GodsAndPantheons");

            

            var godHunterSpawn = new GodPower();
            godHunterSpawn.id = "GodHunter";
            godHunterSpawn.showSpawnEffect = true;
            godHunterSpawn.multiple_spawn_tip = true;
            godHunterSpawn.actorSpawnHeight = 3f;
            godHunterSpawn.name = "GodHunter";
            godHunterSpawn.spawnSound = "spawnelf";
            godHunterSpawn.actor_asset_id = "GodHunter";
            godHunterSpawn.click_action = new PowerActionWithID(callSpawnUnit);
            AssetManager.powers.add(godHunterSpawn);


            var darkOneSpawn = new GodPower();
            darkOneSpawn.id = "DarkOne";
            darkOneSpawn.showSpawnEffect = true;
            darkOneSpawn.multiple_spawn_tip = true;
            darkOneSpawn.actorSpawnHeight = 3f;
            darkOneSpawn.name = "DarkOne";
            darkOneSpawn.spawnSound = "spawnelf";
            darkOneSpawn.actor_asset_id = "DarkOne";
            darkOneSpawn.click_action = new PowerActionWithID(callSpawnUnit);
            AssetManager.powers.add(darkOneSpawn);

            PowerButtons.CreateButton(
              "GodHunter",
              Resources.Load<Sprite>("ui/Icons/godKiller"),
              "Godhunter Spawn",
              "Spawn a GodHunter",
              new Vector2(100, 18),
              ButtonType.GodPower,
              tab.transform,
              null
              
          );
            PowerButtons.CreateButton(
              "DarkOne",
              Resources.Load<Sprite>("ui/Icons/godKiller"),
              "DarkOne Spawn",
              "Spawn a GodHunter",
              new Vector2(100, -18),
              ButtonType.GodPower,
              tab.transform,
              null

          );
            PowerButtons.CreateButton(
                    "KnowledgeGodWindow",
                    Resources.Load<Sprite>("ui/Icons/knowledgeGod"),
                    "Chance Modfier",
                    "Manage The Gods Powers",
                    new Vector2(136, 18),
                    ButtonType.Click,
                    tab.transform,
                    WindowManager.windows["KnowledgeGodWindow"].openWindow
              );
            PowerButton button = PowerButtons.CreateButton(
                    "ToggleDeathEras",
                    Resources.Load<Sprite>("ui/Icons/ages/iconAgeHope"),
                    "Option modifier",
                    "should deaths change the era?",
                    new Vector2(388, 18),
                    ButtonType.Toggle,
                    tab.transform,
                    ToggleEra
              );
            if (Main.savedSettings.deathera)
                PowerButtons.ToggleButton(button.name);
            PowerButton button2 = PowerButtons.CreateButton(
                    "HunterAssassins",
                    Resources.Load<Sprite>("ui/Icons/godKiller"),
                    "option modifier",
                    "if enabled, god hunters will become assassins and will hunt down gods",
                    new Vector2(424, 18),
                    ButtonType.Toggle,
                    tab.transform,
                    ToggleAssassins
              );
            if (Main.savedSettings.HunterAssasins)
                PowerButtons.ToggleButton(button2.name);
            PowerButtons.CreateButton(
                    "GodOfGodsWindow",
                    Resources.Load<Sprite>("ui/Icons/GodofGods"),
                    "Chance Modfier",
                    "Manage The Gods Powers",
                    new Vector2(352, 18),
                    ButtonType.Click,
                    tab.transform,
                    WindowManager.windows["GodOfGodsWindow"].openWindow
              );
            PowerButtons.CreateButton(
                    "MoonGodWindow",
                    Resources.Load<Sprite>("ui/Icons/starsGod"),
                    "Chance Modfier",
                    "Manage The Gods Powers",
                    new Vector2(172, 18),
                    ButtonType.Click,
                    tab.transform,
                    WindowManager.windows["MoonGodWindow"].openWindow
              );
            PowerButtons.CreateButton(
                    "DarkGodWindow",
                    Resources.Load<Sprite>("ui/Icons/godDark"),
                    "Chance Modfier",
                    "Manage The Gods Powers",
                    new Vector2(208, 18),
                    ButtonType.Click,
                    tab.transform,
                    WindowManager.windows["DarkGodWindow"].openWindow
              );
            PowerButtons.CreateButton(
                    "SunGodWindow",
                    Resources.Load<Sprite>("ui/Icons/lightGod"),
                    "Chance Modfier",
                    "Manage The Gods Powers",
                    new Vector2(244, 18),
                    ButtonType.Click,
                    tab.transform,
                    WindowManager.windows["SunGodWindow"].openWindow
              );
            PowerButtons.CreateButton(
                    "WarGodWindow",
                    Resources.Load<Sprite>("ui/Icons/warGod"),
                    "Chance Modfier",
                    "Manage The Gods Powers",
                    new Vector2(280, 18),
                    ButtonType.Click,
                    tab.transform,
                    WindowManager.windows["WarGodWindow"].openWindow
              );
            PowerButtons.CreateButton(
                    "EarthGodWindow",
                    Resources.Load<Sprite>("ui/Icons/earthGod"),
                    "Chance Modfier",
                    "Manage The Gods Powers",
                    new Vector2(316, 18),
                    ButtonType.Click,
                    tab.transform,
                    WindowManager.windows["EarthGodWindow"].openWindow
              );
            PowerButtons.CreateButton(
                    "LichGodWindow",
                    Resources.Load<Sprite>("ui/Icons/lichGod"),
                    "Chance Modfier",
                    "Manage The Gods Powers",
                    new Vector2(460, 18),
                    ButtonType.Click,
                    tab.transform,
                    WindowManager.windows["LichGodWindow"].openWindow
              );
            PowerButtons.CreateButton(
                    "ChaosGodWindow",
                    Resources.Load<Sprite>("ui/Icons/chaosGod"),
                    "Chance Modfier",
                    "Manage The Gods Powers",
                    new Vector2(498, 18),
                    ButtonType.Click,
                    tab.transform,
                    WindowManager.windows["ChaosGodWindow"].openWindow
              );

        }

        private static PowersTab getPowersTab(string id)
        {
            GameObject gameObject = GameObjects.FindEvenInactive("Tab_" + id);
            return gameObject.GetComponent<PowersTab>();
        }
        public static bool callSpawnUnit(WorldTile pTile, string pPowerID)
        {
            AssetManager.powers.CallMethod("spawnUnit", pTile, pPowerID);
            return true;
        }
        public static void ToggleEra(){
                Main.savedSettings.deathera = !Main.savedSettings.deathera;
                Main.saveSettings();
        }
        public static void ToggleAssassins()
        {
            Main.savedSettings.HunterAssasins = !Main.savedSettings.HunterAssasins;
            Main.saveSettings();
        }
    }
}

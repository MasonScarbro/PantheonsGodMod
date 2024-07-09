using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using NCMS;
using NCMS.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ReflectionUtility;
using Assets.SimpleZip;


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
                    "KnowledgeGodWindow",
                    Resources.Load<Sprite>("ui/Icons/knowledgeGod"),
                    "Chance Modfier",
                    "Manage The Gods Powers",
                    new Vector2(136, 18),
                    ButtonType.Click,
                    tab.transform,
                    KnowledgeGodWindow.openWindow
              );
            PowerButtons.CreateButton(
                    "ToggleDeathEras",
                    Resources.Load<Sprite>("ui/Icons/ages/iconAgeHope"),
                    "Chance Modfier",
                    "should deaths change the era?",
                    new Vector2(388, 18),
                    ButtonType.Click,
                    tab.transform,
                    ToggleEra
              );
            PowerButtons.CreateButton(
                    "GodOfGodsWindow",
                    Resources.Load<Sprite>("ui/Icons/IconDemi"),
                    "Chance Modfier",
                    "Manage The Gods Powers",
                    new Vector2(352, 18),
                    ButtonType.Click,
                    tab.transform,
                    GodOfGodsWindow.openWindow
              );
            PowerButtons.CreateButton(
                    "MoonGodWindow",
                    Resources.Load<Sprite>("ui/Icons/starsGod"),
                    "Chance Modfier",
                    "Manage The Gods Powers",
                    new Vector2(172, 18),
                    ButtonType.Click,
                    tab.transform,
                    MoonGodWindow.openWindow
              );
            PowerButtons.CreateButton(
                    "DarkGodWindow",
                    Resources.Load<Sprite>("ui/Icons/godDark"),
                    "Chance Modfier",
                    "Manage The Gods Powers",
                    new Vector2(208, 18),
                    ButtonType.Click,
                    tab.transform,
                    DarkGodWindow.openWindow
              );
            PowerButtons.CreateButton(
                    "SunGodWindow",
                    Resources.Load<Sprite>("ui/Icons/lightGod"),
                    "Chance Modfier",
                    "Manage The Gods Powers",
                    new Vector2(244, 18),
                    ButtonType.Click,
                    tab.transform,
                    SunGodWindow.openWindow
              );
            PowerButtons.CreateButton(
                    "WarGodWindow",
                    Resources.Load<Sprite>("ui/Icons/warGod"),
                    "Chance Modfier",
                    "Manage The Gods Powers",
                    new Vector2(280, 18),
                    ButtonType.Click,
                    tab.transform,
                    WarGodWindow.openWindow
              );
            PowerButtons.CreateButton(
                    "EarthGodWindow",
                    Resources.Load<Sprite>("ui/Icons/earthGod"),
                    "Chance Modfier",
                    "Manage The Gods Powers",
                    new Vector2(316, 18),
                    ButtonType.Click,
                    tab.transform,
                    EarthGodWindow.openWindow
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
            if(Settings.deathera){
                Settings.deathera = false;
            }else{
                Settings.deathera = true;
            }
        }
    }
}

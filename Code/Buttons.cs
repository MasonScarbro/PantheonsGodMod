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
              new Vector2(254, 18),
              ButtonType.GodPower,
              tab.transform,
              null
              
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
    }
}
/* 
AUTHOR: MASON SCARBRO
VERSION: 1.0.0
*/
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using NCMS;
using UnityEngine;
using Newtonsoft.Json;
using HarmonyLib;
using NeoModLoader.constants;
using GodsAndPantheons.AI;
using GodsAndPantheons.Patches;
using ai.behaviours;
namespace GodsAndPantheons
{
    [ModEntry]
    class Main : MonoBehaviour
    {
        
        private const string correctSettingsVersion = "0.2.3";
        public static SavedSettings savedSettings = new SavedSettings();
        public static SavedSettings defaultSettings = new SavedSettings();
        static Harmony _harmony;
        void Awake()
        {
            //SETTINGS
            loadSettings();

            //UI
            WindowManager.init();
            Buttons.init();

            //EFFECTS
            Prefabs.Init();
            Effects.init();

            //CREATURES
            Units.init();
            Invasions.init();

            //GODS
            Traits.init();
            Items.init();

            //POWERS
            NewProjectiles.init();
            NewTerraformOptions.init();
            NewEffects.init();

            //ai
            GodHunterBeh.init();
            SummonedOneBeh.init();
            CorruptedOneBeh.init();
            AtTheSpeedOfLight.init();

            //WORLD
            NewOpinions.init();
            WorldBehaviours.init();

            //APPLY PATCHES
            Patch();
        }
        public static void Patch()
        {
            _harmony = new Harmony("Com.Pantheon.Gods");

             MethodInfo Transpiler = AccessTools.Method(typeof(LavaWalkers), nameof(LavaWalkers.Transpiler));
            _harmony.Patch(AccessTools.Method(typeof(Actor), nameof(Actor.findCurrentTile)), null, null, new HarmonyMethod(Transpiler));
            _harmony.Patch(AccessTools.Method(typeof(Actor), nameof(Actor.u5_curTileAction)), null, null, new HarmonyMethod(Transpiler));
            _harmony.Patch(AccessTools.Method(typeof(Actor), nameof(Actor.updatePathMovement)), null, null, new HarmonyMethod(Transpiler));
            _harmony.Patch(AccessTools.Method(typeof(Dragon), nameof(Dragon.canLand)), null, null, new HarmonyMethod(Transpiler));

            _harmony.PatchAll();
        }
        //??
        public static Dictionary<Actor, Actor> listOfTamedBeasts = new Dictionary<Actor, Actor>();
        public static void saveSettings(SavedSettings previousSettings = null)
        {
            if (previousSettings != null)
            {
                foreach (FieldInfo field in typeof(SavedSettings).GetFields())
                {
                    field.SetValue(savedSettings, field.GetValue(previousSettings));
                }
                savedSettings.settingVersion = correctSettingsVersion;
            }
            string json = JsonConvert.SerializeObject(savedSettings, Formatting.Indented);
            File.WriteAllText($"{Paths.ModsConfigPath}/GodChancesWindow.json", json);
        }
        public static bool loadSettings()
        {
            if (!File.Exists($"{Paths.ModsConfigPath}/GodChancesWindow.json"))
            {
                saveSettings();
                return false;
            }
            string data = File.ReadAllText($"{Paths.ModsConfigPath}/GodChancesWindow.json");
            SavedSettings loadedData;
            try
            {
                loadedData = JsonConvert.DeserializeObject<SavedSettings>(data);
            }
            catch
            {
                saveSettings();
                return false;
            }
            if (loadedData == null || loadedData.settingVersion != correctSettingsVersion)
            {
                saveSettings();
                return false;
            }
            savedSettings = loadedData;
            return true;
        }
        public static void modifyGodOption(string ID, string key, bool? active, int? value = null)
        {
            if (active != null)
            {
                PlayerConfig.dict[key].boolVal = (bool)active;
                PowerButtonSelector.instance.checkToggleIcons();
            }
            savedSettings[ID][key].Set(value ?? savedSettings[ID][key].value, active ?? savedSettings[ID][key].active);
            saveSettings();
        }
    }
}

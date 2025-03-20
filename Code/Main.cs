/* 
AUTHOR: MASON SCARBRO
VERSION: 1.0.0
*/
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NCMS;
using NCMS.Utils;
using UnityEngine;
using ReflectionUtility;
using Newtonsoft.Json;
using HarmonyLib;
namespace GodsAndPantheons
{
    [ModEntry]
    class Main : MonoBehaviour
    {
        
        private const string correctSettingsVersion = "0.2.0";
        public static SavedSettings savedSettings = new SavedSettings();
        public static SavedSettings defaultSettings = new SavedSettings();
        public static Main instance;
        static Harmony _harmony;
        void Awake()
        {
            loadSettings();
            WindowManager.init();
            Buttons.init();

            Effects.init();
            Group.init();
            Traits.init();
            NewProjectiles.init();
            NewTerraformOptions.init();
            NewEffects.init();
            Items.init();
            NewOpinions.init();

            //ai
            GodHunterBeh.init();
            SummonedOneBeh.init();
            CorruptedOneBeh.init();

            Units.init();
            Tab.init();
            Invasions.init();
            instance = this;
            //APPLY PATCHES
            _harmony = new Harmony("Com.Pantheon.Gods");
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
            File.WriteAllText($"{Core.NCMSModsPath}/GodChancesWindow.json", json);
        }
        public static bool loadSettings()
        {
            if (!File.Exists($"{Core.NCMSModsPath}/GodChancesWindow.json"))
            {
                saveSettings();
                return false;
            }
            string data = File.ReadAllText($"{Core.NCMSModsPath}/GodChancesWindow.json");
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
            if (loadedData.settingVersion != correctSettingsVersion)
            {
                saveSettings();
                return false;
            }
            savedSettings = loadedData;
            return true;
        }
        public static void modifyGodOption(string ID, string key, bool? active, float? value = null)
        {
            savedSettings.Chances[ID][key].Set(value ?? savedSettings.Chances[ID][key].value, active ?? savedSettings.Chances[ID][key].active);
            saveSettings();
        }
    }
}

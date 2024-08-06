/* 
AUTHOR: MASON SCARBRO
VERSION: 1.0.0
*/
using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using NCMS;
using NCMS.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ReflectionUtility;
using HarmonyLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
namespace GodsAndPantheons
{
    [ModEntry]
    class Main : MonoBehaviour
    {
        
        private static string correctSettingsVersion = "0.0.2";
        public static SavedSettings savedSettings = new SavedSettings();
        internal static Dictionary<string, UnityEngine.Object> modsResources;
        public static Main instance;
        void Awake()
        {
            loadSettings();
            Dictionary<string, ScrollWindow> allWindows = (Dictionary<string, ScrollWindow>)Reflection.GetField(typeof(ScrollWindow), null, "allWindows");
            Reflection.CallStaticMethod(typeof(ScrollWindow), "checkWindowExist", "inspect_unit");
            allWindows["inspect_unit"].gameObject.SetActive(false);
            Reflection.CallStaticMethod(typeof(ScrollWindow), "checkWindowExist", "village");
            allWindows["village"].gameObject.SetActive(false);
            Reflection.CallStaticMethod(typeof(ScrollWindow), "checkWindowExist", "debug");
            allWindows["debug"].gameObject.SetActive(false);
            Reflection.CallStaticMethod(typeof(ScrollWindow), "checkWindowExist", "kingdom");
            allWindows["kingdom"].gameObject.SetActive(false);
            
            modsResources = Reflection.GetField(typeof(ResourcesPatch), null, "modsResources") as Dictionary<string, UnityEngine.Object>;
            Effects.init();
            Group.init();
            Traits.init();
            NewProjectiles.init();
            NewTerraformOptions.init();
            NewEffects.init();
            Items.init();
            Units.init();
            Tab.init();
            Invasions.init();
            
            instance = this;
        }
        IEnumerator Start()
        {
            loadSettings();
            Dictionary<string, ScrollWindow> allWindows = (Dictionary<string, ScrollWindow>)Reflection.GetField(typeof(ScrollWindow), null, "allWindows");
            Reflection.CallStaticMethod(typeof(ScrollWindow), "checkWindowExist", "inspect_unit");
            allWindows["inspect_unit"].gameObject.SetActive(false);
            Reflection.CallStaticMethod(typeof(ScrollWindow), "checkWindowExist", "village");
            allWindows["village"].gameObject.SetActive(false);
            Reflection.CallStaticMethod(typeof(ScrollWindow), "checkWindowExist", "debug");
            allWindows["debug"].gameObject.SetActive(false);
            Reflection.CallStaticMethod(typeof(ScrollWindow), "checkWindowExist", "kingdom");
            allWindows["kingdom"].gameObject.SetActive(false);
            yield return new WaitForSeconds(1f);
            WindowManager.init();
            Buttons.init();
        }
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
            SavedSettings loadedData = null;
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
                saveSettings(loadedData);
                return false;
            }
            savedSettings = loadedData;
            return true;
        }
        public static void modifyGodOption(string ID, string key, string value, bool active)
        {
            savedSettings.Chances[ID][key] = new InputOption { active = active, value = value };
            saveSettings();
        }
    }
}

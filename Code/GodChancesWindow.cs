using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NCMS;
using NCMS.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ReflectionUtility;



namespace GodsAndPantheons
{
    

    class KnowledgeGodWindow : MonoBehaviour
    {
        private static GameObject contents;
        private static GameObject scrollView;
        private static Vector2 originalSize;
        public static KnowledgeGodWindow instance;


        public static void init()
        {

            contents = WindowManager.windowContents["KnowledgeGodWindow"];
            instance = new GameObject("KnowledgeGodWindowInstance").AddComponent<KnowledgeGodWindow>();
            scrollView = GameObject.Find($"/Canvas Container Main/Canvas - Windows/windows/KnowledgeGodWindow/Background/Scroll View");
            originalSize = contents.GetComponent<RectTransform>().sizeDelta;
            VerticalLayoutGroup layoutGroup = contents.AddComponent<VerticalLayoutGroup>();
            layoutGroup.childControlHeight = false;
            layoutGroup.childControlWidth = false;
            layoutGroup.childForceExpandHeight = false;
            layoutGroup.childForceExpandWidth = false;
            layoutGroup.childScaleHeight = true;
            layoutGroup.childScaleWidth = true;
            layoutGroup.childAlignment = TextAnchor.UpperCenter;
            layoutGroup.spacing = 50;
            loadSettingOptions();
        }

        private static void loadSettingOptions()
        {
            loadInputOptions();


        }

        public static void openWindow()
        {
            Windows.ShowWindow("KnowledgeGodWindow");
        }

        private static void loadInputOptions()
        {
            contents.GetComponent<RectTransform>().sizeDelta += new Vector2(0, ((Main.savedSettings.knowledgeGodChances.Count)) * 250);
            foreach (KeyValuePair<string, InputOption> kv in Main.savedSettings.knowledgeGodChances)
            {

                UnityAction call = null;
                switch (kv.Key)
                {
                    case "KnowledgeGodPwr1%":
                        call = delegate {
                            if (Main.savedSettings.knowledgeGodChances["KnowledgeGodPwr1%"].active)
                            {
                                Traits.knowledgeGodPwrChance1 = int.Parse(Main.savedSettings.knowledgeGodChances["KnowledgeGodPwr1%"].value) / 100;
                            }

                        };
                        break;
                    case "KnowledgeGodPwr2%":
                        call = delegate {
                            if (Main.savedSettings.knowledgeGodChances["KnowledgeGodPwr2%"].active)
                            {
                                Traits.knowledgeGodPwrChance2 = int.Parse(Main.savedSettings.knowledgeGodChances["KnowledgeGodPwr2%"].value) / 100;
                            }

                        };
                        break;
                    case "KnowledgeGodPwr3%":
                        call = delegate {
                            if (Main.savedSettings.knowledgeGodChances["KnowledgeGodPwr3%"].active)
                            {
                                Traits.knowledgeGodPwrChance3 = int.Parse(Main.savedSettings.knowledgeGodChances["KnowledgeGodPwr3%"].value) / 100;
                            }

                        };
                        break;
                    case "KnowledgeGodPwr4%":
                        call = delegate {
                            if (Main.savedSettings.knowledgeGodChances["KnowledgeGodPwr4%"].active)
                            {
                                Traits.knowledgeGodPwrChance4 = int.Parse(Main.savedSettings.knowledgeGodChances["KnowledgeGodPwr4%"].value) / 100;
                            }

                        };
                        break;
                    case "KnowledgeGodPwr5%":
                        call = delegate {
                            if (Main.savedSettings.knowledgeGodChances["KnowledgeGodPwr5%"].active)
                            {
                                Traits.knowledgeGodPwrChance5 = int.Parse(Main.savedSettings.knowledgeGodChances["KnowledgeGodPwr5%"].value) / 100;
                            }

                        };
                        break;
                    case "SummonLightning%":
                        call = delegate {
                            if (Main.savedSettings.knowledgeGodChances["SummonLightning%"].active)
                            {
                                Traits.knowledgeGodPwrChance6 = int.Parse(Main.savedSettings.knowledgeGodChances["SummonLightning%"].value) / 100;
                            }
                            else
                            {
                                Traits.knowledgeGodPwrChance6 = 0;
                            }

                        };
                        break;
                    case "SummonMeteor%":
                        call = delegate {
                            if (Main.savedSettings.knowledgeGodChances["SummonMeteor%"].active)
                            {
                                Traits.knowledgeGodPwrChance7 = int.Parse(Main.savedSettings.knowledgeGodChances["SummonMeteor%"].value) / 100;
                            }
                            else
                            {
                                Traits.knowledgeGodPwrChance7 = 0;
                            }

                        };
                        break;
                    case "PagesOfKnowledge%":
                        call = delegate {
                            if (Main.savedSettings.knowledgeGodChances["PagesOfKnowledge%"].active)
                            {
                                Traits.knowledgeGodPwrChance9 = int.Parse(Main.savedSettings.knowledgeGodChances["PagesOfKnowledge%"].value) / 100;
                            }
                            else
                            {
                                Traits.knowledgeGodPwrChance9 = 0;
                            }
                        };
                        break;
                }
                if (call != null)
                {
                    call.Invoke();
                }

                NameInput input = NewUI.createInputOption(
                    "KnowledgeGodWindow",
                    $"{kv.Key}_setting",
                    kv.Key,
                    "Modify The Value Of This Setting",
                    0,
                    contents,
                    kv.Value.value
                );
                input.inputField.characterValidation = InputField.CharacterValidation.Integer;
                input.inputField.onValueChanged.AddListener(delegate {
                    string pValue = NewUI.checkStatInput(input);
                    Main.modifyGodOption(kv.Key, pValue, PowerButtons.GetToggleValue($"{kv.Key}Button"), call);
                    input.setText(pValue);
                });

                PowerButton activeButton = PowerButtons.CreateButton(
                    $"{kv.Key}Button",
                    Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.units.icon.png"),
                    "Activate Setting",
                    "",
                    new Vector2(200, 0),
                    ButtonType.Toggle,
                    input.transform.parent.transform,
                    delegate {
                        string pValue = NewUI.checkStatInput(input);
                        Main.modifyGodOption(kv.Key, pValue, PowerButtons.GetToggleValue($"{kv.Key}Button"), call);
                        input.setText(pValue);
                    }
                );
                if (kv.Value.active)
                {
                    PowerButtons.ToggleButton($"{kv.Key}Button");
                }
                activeButton.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(64, 64);
            }
        }

    }

    class MoonGodWindow : MonoBehaviour
    {
        private static GameObject contents;
        private static GameObject scrollView;
        private static Vector2 originalSize;
        public static MoonGodWindow instance;


        public static void init()
        {

            contents = WindowManager.windowContents["MoonGodWindow"];
            instance = new GameObject("MoonGodWindowInstance").AddComponent<MoonGodWindow>();
            scrollView = GameObject.Find($"/Canvas Container Main/Canvas - Windows/windows/MoonGodWindow/Background/Scroll View");
            originalSize = contents.GetComponent<RectTransform>().sizeDelta;
            VerticalLayoutGroup layoutGroup = contents.AddComponent<VerticalLayoutGroup>();
            layoutGroup.childControlHeight = false;
            layoutGroup.childControlWidth = false;
            layoutGroup.childForceExpandHeight = false;
            layoutGroup.childForceExpandWidth = false;
            layoutGroup.childScaleHeight = true;
            layoutGroup.childScaleWidth = true;
            layoutGroup.childAlignment = TextAnchor.UpperCenter;
            layoutGroup.spacing = 50;
            loadSettingOptions();
        }

        private static void loadSettingOptions()
        {
            loadInputOptions();


        }

        public static void openWindow()
        {
            Windows.ShowWindow("MoonGodWindow");
        }

        private static void loadInputOptions()
        {
            contents.GetComponent<RectTransform>().sizeDelta += new Vector2(0, ((Main.savedSettings.moonGodChances.Count)) * 250);
            foreach (KeyValuePair<string, InputOption> kv in Main.savedSettings.moonGodChances)
            {

                UnityAction call = null;
                switch (kv.Key)
                {
                    case "summonMoonChunk%":
                        call = delegate {
                            if (Main.savedSettings.moonGodChances["summonMoonChunk%"].active)
                            {
                                Traits.starGodPwrChance1 = float.Parse(Main.savedSettings.moonGodChances["summonMoonChunk%"].value) / 100;

                            }
                            else
                            {
                                Traits.starGodPwrChance1 = 0;
                            }

                        };
                        break;
                    case "cometAzure%":
                        call = delegate {
                            if (Main.savedSettings.moonGodChances["cometAzure%"].active)
                            {
                                Traits.starGodPwrChance2 = int.Parse(Main.savedSettings.moonGodChances["cometAzure%"].value) / 100;
                            }
                            else
                            {
                                Traits.starGodPwrChance2 = 0;
                            }

                        };
                        break;
                    case "cometShower%":
                        call = delegate {
                            if (Main.savedSettings.moonGodChances["cometShower%"].active)
                            {
                                Traits.starGodPwrChance3 = int.Parse(Main.savedSettings.moonGodChances["cometShower%"].value) / 100;
                            }
                            else
                            {
                                Traits.starGodPwrChance3 = 0;
                            }

                        };
                        break;
                    
                }
                if (call != null)
                {
                    call.Invoke();
                }

                NameInput input = NewUI.createInputOption(
                    "MoonGodWindow",
                    $"{kv.Key}_setting",
                    kv.Key,
                    "Modify The Value Of This Setting",
                    0,
                    contents,
                    kv.Value.value
                );
                input.inputField.characterValidation = InputField.CharacterValidation.Integer;
                input.inputField.onValueChanged.AddListener(delegate {
                    string pValue = NewUI.checkStatInput(input);
                    Main.modifyGodOption(kv.Key, pValue, PowerButtons.GetToggleValue($"{kv.Key}Button"), call);
                    input.setText(pValue);
                });

                PowerButton activeButton = PowerButtons.CreateButton(
                    $"{kv.Key}Button",
                    Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.units.icon.png"),
                    "Activate Setting",
                    "",
                    new Vector2(200, 0),
                    ButtonType.Toggle,
                    input.transform.parent.transform,
                    delegate {
                        string pValue = NewUI.checkStatInput(input);
                        Main.modifyGodOption(kv.Key, pValue, PowerButtons.GetToggleValue($"{kv.Key}Button"), call);
                        input.setText(pValue);
                    }
                );
                if (kv.Value.active)
                {
                    PowerButtons.ToggleButton($"{kv.Key}Button");
                }
                activeButton.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(64, 64);
            }
        }

    }

    class DarkGodWindow : MonoBehaviour
    {
        private static GameObject contents;
        private static GameObject scrollView;
        private static Vector2 originalSize;
        public static DarkGodWindow instance;


        public static void init()
        {

            contents = WindowManager.windowContents["DarkGodWindow"];
            instance = new GameObject("DarkGodWindowInstance").AddComponent<DarkGodWindow>();
            scrollView = GameObject.Find($"/Canvas Container Main/Canvas - Windows/windows/DarkGodWindow/Background/Scroll View");
            originalSize = contents.GetComponent<RectTransform>().sizeDelta;
            VerticalLayoutGroup layoutGroup = contents.AddComponent<VerticalLayoutGroup>();
            layoutGroup.childControlHeight = false;
            layoutGroup.childControlWidth = false;
            layoutGroup.childForceExpandHeight = false;
            layoutGroup.childForceExpandWidth = false;
            layoutGroup.childScaleHeight = true;
            layoutGroup.childScaleWidth = true;
            layoutGroup.childAlignment = TextAnchor.UpperCenter;
            layoutGroup.spacing = 50;
            loadSettingOptions();
        }

        private static void loadSettingOptions()
        {
            loadInputOptions();


        }

        public static void openWindow()
        {
            Windows.ShowWindow("DarkGodWindow");
        }

        private static void loadInputOptions()
        {
            contents.GetComponent<RectTransform>().sizeDelta += new Vector2(0, ((Main.savedSettings.darkGodChances.Count)) * 250);
            foreach (KeyValuePair<string, InputOption> kv in Main.savedSettings.darkGodChances)
            {

                UnityAction call = null;
                switch (kv.Key)
                {
                    case "cloudOfDarkness%":
                        call = delegate {
                            if (Main.savedSettings.darkGodChances["cloudOfDarkness%"].active)
                            {
                                Traits.darkGodPwrChance1 = float.Parse(Main.savedSettings.darkGodChances["cloudOfDarkness%"].value) / 100;

                            }
                            else
                            {
                                Traits.darkGodPwrChance1 = 0;
                            }

                        };
                        break;
                    case "blackHole%":
                        call = delegate {
                            if (Main.savedSettings.darkGodChances["blackHole%"].active)
                            {
                                Traits.darkGodPwrChance2 = float.Parse(Main.savedSettings.darkGodChances["blackHole%"].value) / 100;
                            }
                            else
                            {
                                Traits.darkGodPwrChance2 = 0;
                            }

                        };
                        break;
                    case "darkDaggers%":
                        call = delegate {
                            if (Main.savedSettings.darkGodChances["darkDaggers%"].active)
                            {
                                Traits.darkGodPwrChance3 = int.Parse(Main.savedSettings.darkGodChances["darkDaggers%"].value) / 100;
                            }
                            else
                            {
                                Traits.darkGodPwrChance3 = 0;
                            }

                        };
                        break;
                    case "smokeFlash%":
                        call = delegate {
                            if (Main.savedSettings.darkGodChances["smokeFlash%"].active)
                            {
                                Traits.darkGodPwrChance4 = int.Parse(Main.savedSettings.darkGodChances["smokeFlash%"].value) / 100;
                            }
                            else
                            {
                                Traits.darkGodPwrChance4 = 0;
                            }
                            

                        };
                        break;

                }
                if (call != null)
                {
                    call.Invoke();
                }

                NameInput input = NewUI.createInputOption(
                    "DarkGodWindow",
                    $"{kv.Key}_setting",
                    kv.Key,
                    "Modify The Value Of This Setting",
                    0,
                    contents,
                    kv.Value.value
                );
                input.inputField.characterValidation = InputField.CharacterValidation.Integer;
                input.inputField.onValueChanged.AddListener(delegate {
                    string pValue = NewUI.checkStatInput(input);
                    Main.modifyGodOption(kv.Key, pValue, PowerButtons.GetToggleValue($"{kv.Key}Button"), call);
                    input.setText(pValue);
                });

                PowerButton activeButton = PowerButtons.CreateButton(
                    $"{kv.Key}Button",
                    Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.units.icon.png"),
                    "Activate Setting",
                    "",
                    new Vector2(200, 0),
                    ButtonType.Toggle,
                    input.transform.parent.transform,
                    delegate {
                        string pValue = NewUI.checkStatInput(input);
                        Main.modifyGodOption(kv.Key, pValue, PowerButtons.GetToggleValue($"{kv.Key}Button"), call);
                        input.setText(pValue);
                    }
                );
                if (kv.Value.active)
                {
                    PowerButtons.ToggleButton($"{kv.Key}Button");
                }
                activeButton.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(64, 64);
            }
        }

    }

    class SunGodWindow : MonoBehaviour
    {
        private static GameObject contents;
        private static GameObject scrollView;
        private static Vector2 originalSize;
        public static SunGodWindow instance;


        public static void init()
        {

            contents = WindowManager.windowContents["SunGodWindow"];
            instance = new GameObject("SunGodWindowInstance").AddComponent<SunGodWindow>();
            scrollView = GameObject.Find($"/Canvas Container Main/Canvas - Windows/windows/SunGodWindow/Background/Scroll View");
            originalSize = contents.GetComponent<RectTransform>().sizeDelta;
            VerticalLayoutGroup layoutGroup = contents.AddComponent<VerticalLayoutGroup>();
            layoutGroup.childControlHeight = false;
            layoutGroup.childControlWidth = false;
            layoutGroup.childForceExpandHeight = false;
            layoutGroup.childForceExpandWidth = false;
            layoutGroup.childScaleHeight = true;
            layoutGroup.childScaleWidth = true;
            layoutGroup.childAlignment = TextAnchor.UpperCenter;
            layoutGroup.spacing = 50;
            loadSettingOptions();
        }

        private static void loadSettingOptions()
        {
            loadInputOptions();


        }

        public static void openWindow()
        {
            Windows.ShowWindow("SunGodWindow");
        }

        private static void loadInputOptions()
        {
            contents.GetComponent<RectTransform>().sizeDelta += new Vector2(0, ((Main.savedSettings.sunGodChances.Count)) * 250);
            foreach (KeyValuePair<string, InputOption> kv in Main.savedSettings.sunGodChances)
            {

                UnityAction call = null;
                switch (kv.Key)
                {
                    case "flashOfLight%":
                        call = delegate {
                            if (Main.savedSettings.sunGodChances["flashOfLight%"].active)
                            {
                                Traits.sunGodPwrChance1 = float.Parse(Main.savedSettings.sunGodChances["flashOfLight%"].value) / 100;

                            }
                            else
                            {
                                Traits.sunGodPwrChance1 = 0;
                            }

                        };
                        break;
                    case "beamOfLight%":
                        call = delegate {
                            if (Main.savedSettings.sunGodChances["beamOfLight%"].active)
                            {
                                Traits.sunGodPwrChance2 = float.Parse(Main.savedSettings.sunGodChances["beamOfLight%"].value) / 100;
                            }
                            else
                            {
                                Traits.sunGodPwrChance2 = 0;
                            }

                        };
                        break;
                    case "speedOfLight%":
                        call = delegate {
                            if (Main.savedSettings.sunGodChances["speedOfLight%"].active)
                            {
                                Traits.sunGodPwrChance3 = float.Parse(Main.savedSettings.sunGodChances["speedOfLight%"].value) / 100;
                            }
                            else
                            {
                                Traits.sunGodPwrChance3 = 0;
                            }

                        };
                        break;
                    case "lightBallz%":
                        call = delegate {
                            if (Main.savedSettings.sunGodChances["lightBallz%"].active)
                            {
                                Traits.sunGodPwrChance4 = float.Parse(Main.savedSettings.sunGodChances["lightBallz%"].value) / 100;
                            }
                            else
                            {
                                Traits.sunGodPwrChance4 = 0;
                            }


                        };
                        break;

                }
                if (call != null)
                {
                    call.Invoke();
                }

                NameInput input = NewUI.createInputOption(
                    "SunGodWindow",
                    $"{kv.Key}_setting",
                    kv.Key,
                    "Modify The Value Of This Setting",
                    0,
                    contents,
                    kv.Value.value
                );
                input.inputField.characterValidation = InputField.CharacterValidation.Integer;
                input.inputField.onValueChanged.AddListener(delegate {
                    string pValue = NewUI.checkStatInput(input);
                    Main.modifyGodOption(kv.Key, pValue, PowerButtons.GetToggleValue($"{kv.Key}Button"), call);
                    input.setText(pValue);
                });

                PowerButton activeButton = PowerButtons.CreateButton(
                    $"{kv.Key}Button",
                    Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.units.icon.png"),
                    "Activate Setting",
                    "",
                    new Vector2(200, 0),
                    ButtonType.Toggle,
                    input.transform.parent.transform,
                    delegate {
                        string pValue = NewUI.checkStatInput(input);
                        Main.modifyGodOption(kv.Key, pValue, PowerButtons.GetToggleValue($"{kv.Key}Button"), call);
                        input.setText(pValue);
                    }
                );
                if (kv.Value.active)
                {
                    PowerButtons.ToggleButton($"{kv.Key}Button");
                }
                activeButton.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(64, 64);
            }
        }

    }

    class WarGodWindow : MonoBehaviour
    {
        private static GameObject contents;
        private static GameObject scrollView;
        private static Vector2 originalSize;
        public static WarGodWindow instance;


        public static void init()
        {

            contents = WindowManager.windowContents["WarGodWindow"];
            instance = new GameObject("WarGodWindowInstance").AddComponent<WarGodWindow>();
            scrollView = GameObject.Find($"/Canvas Container Main/Canvas - Windows/windows/WarGodWindow/Background/Scroll View");
            originalSize = contents.GetComponent<RectTransform>().sizeDelta;
            VerticalLayoutGroup layoutGroup = contents.AddComponent<VerticalLayoutGroup>();
            layoutGroup.childControlHeight = false;
            layoutGroup.childControlWidth = false;
            layoutGroup.childForceExpandHeight = false;
            layoutGroup.childForceExpandWidth = false;
            layoutGroup.childScaleHeight = true;
            layoutGroup.childScaleWidth = true;
            layoutGroup.childAlignment = TextAnchor.UpperCenter;
            layoutGroup.spacing = 50;
            loadSettingOptions();
        }

        private static void loadSettingOptions()
        {
            loadInputOptions();


        }

        public static void openWindow()
        {
            Windows.ShowWindow("WarGodWindow");
        }

        private static void loadInputOptions()
        {
            contents.GetComponent<RectTransform>().sizeDelta += new Vector2(0, ((Main.savedSettings.warGodChances.Count)) * 250);
            foreach (KeyValuePair<string, InputOption> kv in Main.savedSettings.warGodChances)
            {

                UnityAction call = null;
                switch (kv.Key)
                {
                    case "warGodsCry%":
                        call = delegate {
                            if (Main.savedSettings.warGodChances["warGodsCry%"].active)
                            {
                                Traits.warGodPwrChance1 = float.Parse(Main.savedSettings.warGodChances["warGodsCry%"].value) / 100;

                            }
                            else
                            {
                                Traits.warGodPwrChance1 = 0;
                            }

                        };
                        break;
                    case "axeThrow%":
                        call = delegate {
                            if (Main.savedSettings.warGodChances["axeThrow%"].active)
                            {
                                Traits.warGodPwrChance2 = float.Parse(Main.savedSettings.warGodChances["axeThrow%"].value) / 100;
                            }
                            else
                            {
                                Traits.warGodPwrChance2 = 0;
                            }

                        };
                        break;
                    case "seedsOfWar%":
                        call = delegate {
                            if (Main.savedSettings.warGodChances["seedsOfWar%"].active)
                            {
                                Traits.warGodPwrChance3 = float.Parse(Main.savedSettings.warGodChances["seedsOfWar%"].value) / 100;
                            }
                            else
                            {
                                Traits.warGodPwrChance3 = 0;
                            }


                        };
                        break;

                }
                if (call != null)
                {
                    call.Invoke();
                }

                NameInput input = NewUI.createInputOption(
                    "WarGodWindow",
                    $"{kv.Key}_setting",
                    kv.Key,
                    "Modify The Value Of This Setting",
                    0,
                    contents,
                    kv.Value.value
                );
                input.inputField.characterValidation = InputField.CharacterValidation.Integer;
                input.inputField.onValueChanged.AddListener(delegate {
                    string pValue = NewUI.checkStatInput(input);
                    Main.modifyGodOption(kv.Key, pValue, PowerButtons.GetToggleValue($"{kv.Key}Button"), call);
                    input.setText(pValue);
                });

                PowerButton activeButton = PowerButtons.CreateButton(
                    $"{kv.Key}Button",
                    Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.units.icon.png"),
                    "Activate Setting",
                    "",
                    new Vector2(200, 0),
                    ButtonType.Toggle,
                    input.transform.parent.transform,
                    delegate {
                        string pValue = NewUI.checkStatInput(input);
                        Main.modifyGodOption(kv.Key, pValue, PowerButtons.GetToggleValue($"{kv.Key}Button"), call);
                        input.setText(pValue);
                    }
                );
                if (kv.Value.active)
                {
                    PowerButtons.ToggleButton($"{kv.Key}Button");
                }
                activeButton.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(64, 64);
            }
        }

    }

    class EarthGodWindow : MonoBehaviour
    {
        private static GameObject contents;
        private static GameObject scrollView;
        private static Vector2 originalSize;
        public static EarthGodWindow instance;


        public static void init()
        {

            contents = WindowManager.windowContents["EarthGodWindow"];
            instance = new GameObject("EarthGodWindowInstance").AddComponent<EarthGodWindow>();
            scrollView = GameObject.Find($"/Canvas Container Main/Canvas - Windows/windows/EarthGodWindow/Background/Scroll View");
            originalSize = contents.GetComponent<RectTransform>().sizeDelta;
            VerticalLayoutGroup layoutGroup = contents.AddComponent<VerticalLayoutGroup>();
            layoutGroup.childControlHeight = false;
            layoutGroup.childControlWidth = false;
            layoutGroup.childForceExpandHeight = false;
            layoutGroup.childForceExpandWidth = false;
            layoutGroup.childScaleHeight = true;
            layoutGroup.childScaleWidth = true;
            layoutGroup.childAlignment = TextAnchor.UpperCenter;
            layoutGroup.spacing = 50;
            loadSettingOptions();
        }

        private static void loadSettingOptions()
        {
            loadInputOptions();


        }

        public static void openWindow()
        {
            Windows.ShowWindow("EarthGodWindow");
        }

        private static void loadInputOptions()
        {
            contents.GetComponent<RectTransform>().sizeDelta += new Vector2(0, ((Main.savedSettings.earthGodChances.Count)) * 250);
            foreach (KeyValuePair<string, InputOption> kv in Main.savedSettings.earthGodChances)
            {

                UnityAction call = null;
                switch (kv.Key)
                {
                    case "earthquake%":
                        call = delegate {
                            if (Main.savedSettings.earthGodChances["earthquake%"].active)
                            {
                                Traits.earthGodPwrChance1 = float.Parse(Main.savedSettings.earthGodChances["earthquake%"].value) / 100;

                            }
                            else
                            {
                                Traits.earthGodPwrChance1 = 0;
                            }

                        };
                        break;
                    case "makeItRain%":
                        call = delegate {
                            if (Main.savedSettings.earthGodChances["makeItRain%"].active)
                            {
                                Traits.earthGodPwrChance2 = float.Parse(Main.savedSettings.earthGodChances["makeItRain%"].value) / 100;
                            }
                            else
                            {
                                Traits.earthGodPwrChance2 = 0;
                            }

                        };
                        break;
                    case "buildWorld%":
                        call = delegate {
                            if (Main.savedSettings.earthGodChances["buildWorld%"].active)
                            {
                                Traits.earthGodPwrChance3 = float.Parse(Main.savedSettings.earthGodChances["buildWorld%"].value) / 100;
                            }
                            else
                            {
                                Traits.earthGodPwrChance3 = 0;
                            }


                        };
                        break;

                }
                if (call != null)
                {
                    call.Invoke();
                }

                NameInput input = NewUI.createInputOption(
                    "EarthGodWindow",
                    $"{kv.Key}_setting",
                    kv.Key,
                    "Modify The Value Of This Setting",
                    0,
                    contents,
                    kv.Value.value
                );
                input.inputField.characterValidation = InputField.CharacterValidation.Integer;
                input.inputField.onValueChanged.AddListener(delegate {
                    string pValue = NewUI.checkStatInput(input);
                    Main.modifyGodOption(kv.Key, pValue, PowerButtons.GetToggleValue($"{kv.Key}Button"), call);
                    input.setText(pValue);
                });

                PowerButton activeButton = PowerButtons.CreateButton(
                    $"{kv.Key}Button",
                    Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.units.icon.png"),
                    "Activate Setting",
                    "",
                    new Vector2(200, 0),
                    ButtonType.Toggle,
                    input.transform.parent.transform,
                    delegate {
                        string pValue = NewUI.checkStatInput(input);
                        Main.modifyGodOption(kv.Key, pValue, PowerButtons.GetToggleValue($"{kv.Key}Button"), call);
                        input.setText(pValue);
                    }
                );
                if (kv.Value.active)
                {
                    PowerButtons.ToggleButton($"{kv.Key}Button");
                }
                activeButton.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(64, 64);
            }
        }

    }

}
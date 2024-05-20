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
            contents.GetComponent<RectTransform>().sizeDelta += new Vector2(0, ((Main.savedSettings.knowledgeGodChances.Count) / 4) * 250);
            foreach (KeyValuePair<string, InputOption> kv in Main.savedSettings.knowledgeGodChances)
            {

                UnityAction call = null;
                switch (kv.Key)
                {
                    case "KnowledgeGodPwr1%":
                        call = delegate {
                            if (Main.savedSettings.knowledgeGodChances["KnowledgeGodPwr1%"].active)
                            {
                                Traits.knowledgeGodPwrChance1 = int.Parse(Main.savedSettings.knowledgeGodChances["KnowledgeGodPwr1%"].value);
                            }

                        };
                        break;
                    case "KnowledgeGodPwr2%":
                        call = delegate {
                            if (Main.savedSettings.knowledgeGodChances["KnowledgeGodPwr2%"].active)
                            {
                                Traits.knowledgeGodPwrChance1 = int.Parse(Main.savedSettings.knowledgeGodChances["KnowledgeGodPwr2%"].value);
                            }

                        };
                        break;
                    case "KnowledgeGodPwr3%":
                        call = delegate {
                            if (Main.savedSettings.knowledgeGodChances["KnowledgeGodPwr3%"].active)
                            {
                                Traits.knowledgeGodPwrChance1 = int.Parse(Main.savedSettings.knowledgeGodChances["KnowledgeGodPwr3%"].value);
                            }

                        };
                        break;
                    case "KnowledgeGodPwr4%":
                        call = delegate {
                            if (Main.savedSettings.knowledgeGodChances["KnowledgeGodPwr4%"].active)
                            {
                                Traits.knowledgeGodPwrChance1 = int.Parse(Main.savedSettings.knowledgeGodChances["KnowledgeGodPwr4%"].value);
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

}
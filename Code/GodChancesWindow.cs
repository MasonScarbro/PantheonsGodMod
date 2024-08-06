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
using UnityEngine.UIElements;



namespace GodsAndPantheons
{
    class GodWindow : MonoBehaviour
    {
        private  GameObject contents;
        string ID;
        public void init(string id)
        {
            ID = id;
            contents = WindowManager.windowContents[ID];
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

        private void loadSettingOptions()
        {
            loadInputOptions();
        }

        public void openWindow()
        {
            Windows.ShowWindow(ID);
        }

        private void loadInputOptions()
        {
            Dictionary<string, InputOption> options = Main.savedSettings.Chances[ID];
            contents.GetComponent<RectTransform>().sizeDelta += new Vector2(0, (options.Count) * 250);
            foreach (KeyValuePair<string, InputOption> kv in options)
            {
                
                if (options.ContainsKey(kv.Key))
                {
                    NameInput input = NewUI.createInputOption(
                        ID,
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
                        Main.modifyGodOption(ID,kv.Key, pValue, PowerButtons.GetToggleValue($"{kv.Key}Button"));
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
                            Main.modifyGodOption(ID,kv.Key, pValue, PowerButtons.GetToggleValue($"{kv.Key}Button"));
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
}
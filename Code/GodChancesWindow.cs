using System.Collections.Generic;
using NCMS.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace GodsAndPantheons
{
    class GodWindow : MonoBehaviour
    {
        private GameObject contents;
        string ID;
        public void init(string id, GameObject content)
        {
            ID = id;
            contents = content;
            VerticalLayoutGroup layoutGroup = contents.AddComponent<VerticalLayoutGroup>();
            layoutGroup.childControlHeight = false;
            layoutGroup.childControlWidth = false;
            layoutGroup.childForceExpandHeight = false;
            layoutGroup.childForceExpandWidth = false;
            layoutGroup.childScaleHeight = true;
            layoutGroup.childScaleWidth = true;
            layoutGroup.childAlignment = TextAnchor.UpperCenter;
            layoutGroup.spacing = 50;
            LoadSettingOptions();
        }

        private void LoadSettingOptions()
        {
            LoadInputOptions();
        }

        public void openWindow()
        {
            Windows.ShowWindow(ID);
        }

        private void LoadInputOptions()
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
                    input.inputField.characterValidation = InputField.CharacterValidation.Decimal;
                    input.inputField.onValueChanged.AddListener(delegate {
                        string pValue = NewUI.checkStatInput(input);
                        Main.modifyGodOption(ID, kv.Key, PowerButtons.GetToggleValue($"{kv.Key}Button"), pValue);
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
                        delegate { Main.modifyGodOption(ID, kv.Key, PowerButtons.GetToggleValue($"{kv.Key}Button")); }
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
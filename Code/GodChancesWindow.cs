using System.Collections.Generic;
using NCMS.Utils;
using NeoModLoader.General;
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
        public void Reset()
        {
            foreach(Transform Child in contents.transform)
            {
                if(Child.GetChild(0)?.GetComponent<NameInput>() != null)
                {
                    Child.GetChild(0).GetComponent<NameInput>().setText($"{Main.defaultSettings.Chances[ID][Child.name].value}");
                    Main.modifyGodOption(ID, Child.name, Main.defaultSettings.Chances[ID][Child.name].active, Main.defaultSettings.Chances[ID][Child.name].value);
                }
            }
        }

        private void LoadInputOptions()
        {
            Dictionary<string, InputOption> options = Main.savedSettings.Chances[ID];
            contents.GetComponent<RectTransform>().sizeDelta += new Vector2(0, (options.Count) * 250);
            PowerButtons.CreateButton(
                        $"Reset {ID} Settings",
                        Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.units.icon.png"),
                        "Reset the settings of this god",
                        "",
                        new Vector2(100, 0),
                        ButtonType.Click,
                        contents.transform,
                        Reset
            );
            foreach (KeyValuePair<string, InputOption> kv in options)
            {
                if (options.ContainsKey(kv.Key))
                {
                    NameInput input = NewUI.createInputOption(
                        ID,
                        kv.Key,
                        kv.Key,
                        Main.defaultSettings.Chances[ID][kv.Key].Description,
                        0,
                        contents,
                        $"{kv.Value.value}"
                    );
                    input.inputField.characterValidation = InputField.CharacterValidation.Decimal;
                    input.inputField.onValueChanged.AddListener(delegate {
                        int pValue = NewUI.checkStatInput(input);
                        Main.modifyGodOption(ID, kv.Key, null, pValue);
                        input.setText($"{pValue}");
                    });
                    GodPower power = AssetManager.powers.add(new GodPower()
                    {
                        id = kv.Key,
                        name = kv.Key,
                        toggle_name = kv.Key,
                        toggle_action = delegate
                        {
                            Main.modifyGodOption(ID, kv.Key, !Main.savedSettings.Chances[ID][kv.Key].active);
                        }
                    });
                    LM.AddToCurrentLocale(power.name, power.name);
                    LM.AddToCurrentLocale(power.name + " Description", "Toggle the option");
                    PlayerConfig.dict.Add(kv.Key, new PlayerOptionData(kv.Key));
                    PowerButton activeButton = PowerButtonCreator.CreateToggleButton(
                        $"{kv.Key}",
                        Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.units.icon.png"),
                        input.transform.parent.transform,
                        new Vector2(200, 0),
                        true
                    );
                    if (!kv.Value.active)
                    {
                        PlayerConfig.dict[kv.Key].boolVal = false;
                    }
                    activeButton.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(64, 64);
                }
            }
            PowerButtonSelector.instance.checkToggleIcons();
        }
    }
}
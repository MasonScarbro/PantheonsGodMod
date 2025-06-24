using System.Globalization;
using NCMS.Utils;
using ReflectionUtility;
using System.Reflection;
using System.Collections.Generic;
using HarmonyLib;
using SleekRender;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GodsAndPantheons
{
    class UI : MonoBehaviour
    {
        public static void createTab(string buttonID, string tabID, string name, string desc, int xPos)
        {
            GameObject OtherTabButton = GameObjects.FindEvenInactive("Button_Other");
            if (OtherTabButton != null)
            {
                Localization.AddOrSet(buttonID, name);
                Localization.AddOrSet($"{buttonID} Description", desc);
                Localization.AddOrSet("M.S_mod_creator", "Made By M.S");
                Localization.AddOrSet(tabID, name);


                GameObject newTabButton = GameObject.Instantiate(OtherTabButton);
                newTabButton.GetComponent<GraphicRaycaster>().DestroyImmediateIfNotNull();
                newTabButton.GetComponentAtIndex(9).DestroyImmediateIfNotNull();
                newTabButton.transform.SetParent(OtherTabButton.transform.parent);
                Button buttonComponent = newTabButton.GetComponent<Button>();
                TipButton tipButton = buttonComponent.gameObject.GetComponent<TipButton>();
                tipButton.textOnClick = buttonID;
                tipButton.textOnClickDescription = $"{buttonID} Description";
                tipButton.text_description_2 = "M.S_mod_creator";



                newTabButton.transform.localPosition = new Vector3(150f, 49.57f);
                newTabButton.transform.localScale = new Vector3(1f, 1f);
                newTabButton.name = buttonID;

                var spriteForTab = Resources.Load<Sprite>("ui/Icons/subgod");
                newTabButton.transform.Find("Icon").GetComponent<Image>().sprite = spriteForTab;


                GameObject OtherTab = FindAllGameObjectsCreditToNikonForThisFunctionBTW("other");
                if (OtherTab == null)
                {
                    Debug.LogError("Tab_Other not found, cannot create new tab.");
                    return;
                }
                else
                {
                    foreach (Transform child in OtherTab.transform)
                    {
                        child.gameObject.SetActive(false);
                    }

                    GameObject additionalPowersTab = GameObject.Instantiate(OtherTab);

                    foreach (Transform child in additionalPowersTab.transform)
                    {
                        if (child.gameObject.name == "tabBackButton" || child.gameObject.name == "-space")
                        {
                            child.gameObject.SetActive(true);
                            continue;
                        }

                        GameObject.Destroy(child.gameObject);
                    }

                    foreach (Transform child in OtherTab.transform)
                    {
                        child.gameObject.SetActive(true);
                    }
                    additionalPowersTab.transform.SetParent(OtherTab.transform.parent);
                    PowersTab powersTabComponent = additionalPowersTab.GetComponent<PowersTab>();
                    powersTabComponent.powerButton = buttonComponent;
                    Reflection.SetField<List<PowerButton>>(powersTabComponent, "_power_buttons", new List<PowerButton>());



                    additionalPowersTab.name = tabID;
                    powersTabComponent.powerButton.onClick = new Button.ButtonClickedEvent();
                    powersTabComponent.powerButton.onClick.AddListener(() => tabOnClick(tabID));
                    Reflection.SetField<GameObject>(powersTabComponent, "parentObj", OtherTab.transform.parent.parent.gameObject);

                    additionalPowersTab.SetActive(true);
                    powersTabComponent.powerButton.gameObject.SetActive(true);
                    var asset = new PowerTabAsset
                    {
                        id = tabID,
                        locale_key = tabID,
                        tab_type_main = true,
                        get_power_tab = () => powersTabComponent
                    };
                    AssetManager.power_tab_library.add(asset);
                    powersTabComponent._asset = asset;
                }
                

                
            }
        }

        public static GameObject FindAllGameObjectsCreditToNikonForThisFunctionBTW(string Name)
        {
            GameObject[] objectsOfTypeAll = Resources.FindObjectsOfTypeAll<GameObject>();
            for (int index = 0; index < objectsOfTypeAll.Length; ++index)
            {
                if (objectsOfTypeAll[index].gameObject.gameObject.name == Name)
                    return objectsOfTypeAll[index];
            }
            return (GameObject)null;
        }
        public static void tabOnClick(string tabID)
        {
            GameObject AdditionalTab = GameObjects.FindEvenInactive(tabID);
            PowersTab AdditionalPowersTab = AdditionalTab.GetComponent<PowersTab>();

            AdditionalPowersTab.showTab(AdditionalPowersTab.powerButton);
        }
        private static GameObject textRef;
        public static Text addText(string window, string textString, GameObject parent, int sizeFont, Vector3 pos, Vector2 addSize = default(Vector2))
        {
            textRef = GameObject.Find($"/Canvas Container Main/Canvas - Windows/windows/" + window + "/Background/Title");
            GameObject textGo = Instantiate(textRef, parent.transform);
            textGo.SetActive(true);

            var textComp = textGo.GetComponent<Text>();
            textComp.fontSize = sizeFont;
            textComp.resizeTextMaxSize = sizeFont;
            var textRect = textGo.GetComponent<RectTransform>();
            textRect.position = new Vector3(0, 0, 0);
            textRect.localPosition = pos + new Vector3(0, -50, 0);
            textRect.sizeDelta = new Vector2(100, 100) + addSize;
            textGo.AddComponent<GraphicRaycaster>();
            textComp.text = textString;

            return textComp;
        }
        public static NameInput createInputOption(string window, string objName, string title, string desc, int posY, GameObject parent, string textValue = "-1")
        {
            GameObject inputRef = GameObjects.FindEvenInactive("NameInputElement");
            GameObject statHolder = new GameObject(objName);
            statHolder.transform.SetParent(parent.transform);
            Image statImage = statHolder.AddComponent<Image>();
            statImage.sprite = Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.UI.windowInnerSliced1.png");
            RectTransform statHolderRect = statHolder.GetComponent<RectTransform>();
            statHolderRect.localPosition = new Vector3(130, posY, 0);
            statHolderRect.sizeDelta = new Vector2(400, 150);
            GameObject inputField = Instantiate(inputRef, statHolder.transform);

            Text statText = addText(window, title, statHolder, 20, new Vector3(0, 110, 0), new Vector2(100, 0));
            RectTransform statTextRect = statText.gameObject.GetComponent<RectTransform>();
            statTextRect.sizeDelta = new Vector2(statTextRect.sizeDelta.x + 50, 80);

            Text descText = addText(window, desc, statHolder, Mathf.Min(1300/desc.Length, 16), new Vector3(0, 80, 0), new Vector2(300, 0));
            RectTransform descTextRect = descText.gameObject.GetComponent<RectTransform>();
            descTextRect.sizeDelta = new Vector2(descTextRect.sizeDelta.x, 80);

            NameInput nameInputComp = inputField.GetComponent<NameInput>();
            nameInputComp.setText(textValue);
            RectTransform inputRect = inputField.GetComponent<RectTransform>();
            inputRect.localPosition = new Vector3(0, -40, 0);
            inputRect.sizeDelta += new Vector2(120, 40);

            GameObject inputChild = inputField.transform.Find("InputField").gameObject;
            RectTransform inputChildRect = inputChild.GetComponent<RectTransform>();
            inputChildRect.sizeDelta *= 2;
            Text inputChildText = inputChild.GetComponent<Text>();
            inputChildText.resizeTextMaxSize = 20;
            return nameInputComp;
        }
        public static Button createBGWindowButton(GameObject parent, int posY, string iconName, string buttonName, string buttonTitle,
        string buttonDesc, UnityAction call)
        {
            PowerButton button = PowerButtons.CreateButton(
                buttonName,
                Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.units.icon.png"),
                buttonTitle,
                buttonDesc,
                new Vector2(118, posY),
                ButtonType.Click,
                parent.transform,
                call
            );

            Image buttonBG = button.gameObject.GetComponent<Image>();
            buttonBG.sprite = Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.UI.backgroundTabButton1");
            Button buttonButton = button.gameObject.GetComponent<Button>();
            buttonBG.rectTransform.localScale = Vector3.one;

            return buttonButton;
        }
        public static int checkStatInput(NameInput pInput = null, string pText = null)
        {
            string text = pText;
            if (pInput != null)
            {
                text = pInput.inputField.text;
            }
            int num;
            if (!int.TryParse(text, out num))
            {
                return 0;
            }
            if (num > 1000)
            {
                return 1000;
            }
            if (num < -100)
            {
                return -100;
            }
            return num;
        }
    }
}

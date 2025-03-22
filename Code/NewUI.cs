using System.Globalization;
using NCMS.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GodsAndPantheons
{
    class NewUI : MonoBehaviour
    {
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
        public static float checkStatInput(NameInput pInput = null, string pText = null)
        {
            string text = pText;
            if (pInput != null)
            {
                text = pInput.inputField.text;
            }
            float num;
            if (!float.TryParse(text, out num))
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

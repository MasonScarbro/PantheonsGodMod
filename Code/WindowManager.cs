using System.Collections.Generic;
using NCMS.Utils;
using UnityEngine;

namespace GodsAndPantheons
{
    class WindowManager
    {
        public static Dictionary<string, GodWindow> windows = new Dictionary<string, GodWindow>();
        public static void init()
        {
            newWindow("KnowledgeGodWindow", "Knowledge Chance Modfier");
            newWindow("MoonGodWindow", "Ranni Chance Modifier");
            newWindow("DarkGodWindow", "Dark Chance Modifier");
            newWindow("SunGodWindow", "Sun Chance Modifier");
            newWindow("WarGodWindow", "War Chance Modifier");
            newWindow("EarthGodWindow", "Earth Chance Modifier");
            newWindow("LichGodWindow", "Lich Chance Modifier");
            newWindow("GodOfGodsWindow", "Godly Chance Modifier");
            newWindow("ChaosGodWindow", "Chaotic Chance Modifier");
        }

        private static void newWindow(string id, string title)
        {
            ScrollWindow window;
            GameObject content;
            window = Windows.CreateNewWindow(id, title);

            GameObject scrollView = GameObject.Find($"/Canvas Container Main/Canvas - Windows/windows/{window.name}/Background/Scroll View");
            content = GameObject.Find($"/Canvas Container Main/Canvas - Windows/windows/{window.name}/Background/Scroll View/Viewport/Content");
            if (content != null)
            {
                windows.Add(id, scrollView.AddComponent<GodWindow>());
                scrollView.GetComponent<GodWindow>().init(id, content);
                scrollView.gameObject.SetActive(true);
            }
        }
    }
}

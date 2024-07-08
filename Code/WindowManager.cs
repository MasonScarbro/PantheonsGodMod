using System;
using System.Collections;
using System.Collections.Generic;
using NCMS;
using NCMS.Utils;
using UnityEngine;
using ReflectionUtility;

namespace GodsAndPantheons
{
    class WindowManager
    {
        public static Dictionary<string, GameObject> windowContents = new Dictionary<string, GameObject>();
        public static Dictionary<string, ScrollWindow> createdWindows = new Dictionary<string, ScrollWindow>();

        public static void init()
        {
            
            
            newWindow("KnowledgeGodWindow", "Knowledge Chance Modfier");
            newWindow("MoonGodWindow", "Ranni Chance Modifier");
            newWindow("DarkGodWindow", "Dark Chance Modifier");
            newWindow("SunGodWindow", "Sun Chance Modifier");
            newWindow("WarGodWindow", "Sun Chance Modifier");
            newWindow("EarthGodWindow", "Earth Chance Modifier");
            newWindow("GodOfGodsWindow", "Godly Chance Modifier");
            KnowledgeGodWindow.init();
            MoonGodWindow.init();
            DarkGodWindow.init();
            SunGodWindow.init();
            WarGodWindow.init();
            EarthGodWindow.init();
            //GodChancesWindow.init();
        }

        private static void newWindow(string id, string title)
        {
            ScrollWindow window;
            GameObject content;
            window = Windows.CreateNewWindow(id, title);
            createdWindows.Add(id, window);

            GameObject scrollView = GameObject.Find($"/Canvas Container Main/Canvas - Windows/windows/{window.name}/Background/Scroll View");
            scrollView.gameObject.SetActive(true);

            content = GameObject.Find($"/Canvas Container Main/Canvas - Windows/windows/{window.name}/Background/Scroll View/Viewport/Content");
            if (content != null)
            {
                windowContents.Add(id, content);
            }
        }
    }
}

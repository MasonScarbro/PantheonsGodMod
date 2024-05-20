using System;
using System.Collections;
using System.Collections.Generic;
using NCMS;
using NCMS.Utils;
using UnityEngine;
using UnityEngine.UI;
using ReflectionUtility;

namespace GodsAndPantheons
{
    [Serializable]
    public class SavedSettings
    {
        public string settingVersion = "0.0.2";
        
        public Dictionary<string, InputOption> knowledgeGodChances = new Dictionary<string, InputOption>
        {
            {"KnowledgeGodPwr1%", new InputOption{active = true, value = "3" }},
            {"KnowledgeGodPwr2%", new InputOption{active = true, value = "35" }},
            {"KnowledgeGodPwr3%", new InputOption{active = true, value = "300" }},
            {"KnowledgeGodPwr4%", new InputOption{active = true, value = "5" }},
        };


    }
    public class InputOption
    {
        public bool active = true;
        public string value;
    }
}
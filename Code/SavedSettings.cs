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
            {"KnowledgeGodPwr1%", new InputOption{active = true, value = "20" }},
            {"KnowledgeGodPwr2%", new InputOption{active = true, value = "1" }},
            {"KnowledgeGodPwr3%", new InputOption{active = true, value = "1" }},
            {"KnowledgeGodPwr4%", new InputOption{active = true, value = "5" }},
            {"KnowledgeGodPwr5%", new InputOption{active = true, value = "4" }},
            {"SummonLightning%", new InputOption{active = true, value = "2" }},
            {"SummonMeteor%", new InputOption{active = true, value = "1" }},
            {"PagesOfKnowledge%", new InputOption{active = true, value = "1" }},
        };
        public Dictionary<string, InputOption> GodOfGodsChances = new Dictionary<string, InputOption>
        {
            {"Terrain bending%", new InputOption{active = true, value = "20" }},
            {"Summoning%", new InputOption{active = true, value = "1" }},
            {"Magic%", new InputOption{active = true, value = "1" }},
        };
        public Dictionary<string, InputOption> moonGodChances = new Dictionary<string, InputOption>
        {
            {"summonMoonChunk%", new InputOption{active = true, value = "0.05" }},
            {"cometAzure%", new InputOption{active = true, value = "1" }},
            {"cometShower%", new InputOption{active = true, value = "1" }},
            {"summonWolf%", new InputOption{active = true, value = "5" }}

        };
        public Dictionary<string, InputOption> darkGodChances = new Dictionary<string, InputOption>
        {
            {"cloudOfDarkness%", new InputOption{active = true, value = "0.01" }},
            {"blackHole%", new InputOption{active = true, value = "0.1" }},
            {"darkDaggers%", new InputOption{active = true, value = "4" }},
            {"smokeFlash%", new InputOption{active = true, value = "1" }},
            {"summonDarkOne%", new InputOption{active = true, value = "4" }},
        };
        public Dictionary<string, InputOption> sunGodChances = new Dictionary<string, InputOption>
        {
            {"flashOfLight%", new InputOption{active = true, value = "1" }},
            {"beamOfLight%", new InputOption{active = true, value = "8" }},
            {"speedOfLight%", new InputOption{active = true, value = "2" }},
            {"lightBallz%", new InputOption{active = true, value = "0.5" }}
        };
        public Dictionary<string, InputOption> warGodChances = new Dictionary<string, InputOption>
        {
            {"warGodsCry%", new InputOption{active = true, value = "1" }},
            {"axeThrow%", new InputOption{active = true, value = "3" }},
            {"seedsOfWar%", new InputOption{active = true, value = "2" }},
            
        };
        public Dictionary<string, InputOption> earthGodChances = new Dictionary<string, InputOption>
        {
            {"earthquake%", new InputOption{active = true, value = "5" }},
            {"makeItRain%", new InputOption{active = true, value = "10" }},
            {"buildWorld%", new InputOption{active = true, value = "1" }},

        };
        public Dictionary<string, InputOption> lichGodChances = new Dictionary<string, InputOption>
        {
            {"waveOfMutilation%", new InputOption{active = true, value = "20" }},
            {"summonSkele%", new InputOption{active = true, value = "2" }},
            {"summonDead%", new InputOption{active = true, value = "3" }},
            {"rigorMortisHand%", new InputOption{active = true, value = "5" }},

        };
        public bool deathera = true;
        public bool HunterAssasins = true;


    }
    public class InputOption
    {
        public bool active = true;
        public string value;
    }
}

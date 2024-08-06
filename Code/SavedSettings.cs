using System;
using System.Collections.Generic;

namespace GodsAndPantheons
{
    [Serializable]
    public class SavedSettings
    {
        public string settingVersion = "0.1.0";
        public Dictionary<string, Dictionary<string, InputOption>> Chances = new Dictionary<string, Dictionary<string, InputOption>>
        {
            {"KnowledgeGodWindow",
              new Dictionary<string, InputOption>
             {
              {"KnowledgeGodPwr1%", new InputOption{active = true, value = "20" }},
              {"KnowledgeGodPwr2%", new InputOption{active = true, value = "1" }},
              {"KnowledgeGodPwr3%", new InputOption{active = true, value = "1" }},
              {"KnowledgeGodPwr4%", new InputOption{active = true, value = "5" }},
              {"KnowledgeGodPwr5%", new InputOption{active = true, value = "4" }},
              {"SummonLightning%", new InputOption{active = true, value = "2" }},
              {"SummonMeteor%", new InputOption{active = true, value = "1" }},
              {"PagesOfKnowledge%", new InputOption{active = true, value = "1" }}
             }   
            },
            {"LichGodWindow", 
                new Dictionary<string, InputOption>
              {
                {"waveOfMutilation%", new InputOption{active = true, value = "20" }},
                {"summonSkele%", new InputOption{active = true, value = "2" }},
                {"summonDead%", new InputOption{active = true, value = "3" }},
                {"rigorMortisHand%", new InputOption{active = true, value = "5" }}
              } 
            },
            {"GodOfGodsWindow",
                new Dictionary<string, InputOption>
              {
                 {"Terrain bending%", new InputOption{active = true, value = "20" }},
                 {"Summoning%", new InputOption{active = true, value = "1" }},
                 {"Magic%", new InputOption{active = true, value = "1" }}
              }
            },
            {"MoonGodWindow",
                new Dictionary<string, InputOption>
              {
                 {"summonMoonChunk%", new InputOption{active = true, value = "0.05" }},
                 {"cometAzure%", new InputOption{active = true, value = "1" }},
                 {"cometShower%", new InputOption{active = true, value = "1" }},
                 {"summonWolf%", new InputOption{active = true, value = "5" }}
              }
            },
            {"DarkGodWindow",
                new Dictionary<string, InputOption>
              {
                  {"cloudOfDarkness%", new InputOption{active = true, value = "0.01" }},
                  {"blackHole%", new InputOption{active = true, value = "0.1" }},
                  {"darkDaggers%", new InputOption{active = true, value = "4" }},
                  {"smokeFlash%", new InputOption{active = true, value = "1" }},
                  {"summonDarkOne%", new InputOption{active = true, value = "4" }}
              }
            },
            {"SunGodWindow",
                new Dictionary<string, InputOption>
              {
                  {"flashOfLight%", new InputOption{active = true, value = "1" }},
                  {"beamOfLight%", new InputOption{active = true, value = "8" }},
                  {"speedOfLight%", new InputOption{active = true, value = "2" }},
                  {"lightBallz%", new InputOption{active = true, value = "0.5" }}
              }
            },
            {"WarGodWindow",
                new Dictionary<string, InputOption>
              {
                  {"warGodsCry%", new InputOption{active = true, value = "1" }},
                  {"axeThrow%", new InputOption{active = true, value = "3" }},
                  {"seedsOfWar%", new InputOption{active = true, value = "2" }}
              }
            },
            {"EarthGodWindow",
                new Dictionary<string, InputOption>
              {
                  {"earthquake%", new InputOption{active = true, value = "5" }},
                  {"makeItRain%", new InputOption{active = true, value = "10" }},
                  {"buildWorld%", new InputOption{active = true, value = "1" }},
                  {"SummonDruid%", new InputOption{active = true, value = "10" }}
              }
            }
        };
        public bool deathera = true;
        public bool HunterAssasins = true;
    }
    [Serializable]
    public struct InputOption
    {
        public bool active;
        public string value;
    }
}

using System;
using System.Collections.Generic;

namespace GodsAndPantheons
{
    [Serializable]
    public class SavedSettings
    {
        public string settingVersion = "0.1.2";
        //inheritance of the god traits is stored here, if the baby has two parents and both of them are god's or has one parent who is a god, it use the inherit for the chance
        //if it has a god parent and normal parent it will use the inherit for the chance of changing the stats of the demi god trait for the baby
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
              {"PagesOfKnowledge%", new InputOption{active = true, value = "1" }},
              {"inherit%", new InputOption{active = true, value = "35" }}
             }   
            },
            {"LichGodWindow", 
                new Dictionary<string, InputOption>
              {
                {"waveOfMutilation%", new InputOption{active = true, value = "20" }},
                {"summonSkele%", new InputOption{active = true, value = "2" }},
                {"summonDead%", new InputOption{active = true, value = "3" }},
                {"rigorMortisHand%", new InputOption{active = true, value = "5" }},
                {"inherit%", new InputOption{active = true, value = "25" }}
              } 
            },
            {"GodOfGodsWindow",
                new Dictionary<string, InputOption>
              {
                 {"Terrain bending%", new InputOption{active = true, value = "20" }},
                 {"Summoning%", new InputOption{active = true, value = "1" }},
                 {"Magic%", new InputOption{active = true, value = "1" }},
                 {"inherit%", new InputOption{active = true, value = "10" }}
              }
            },
            {"MoonGodWindow",
                new Dictionary<string, InputOption>
              {
                 {"summonMoonChunk%", new InputOption{active = true, value = "0.05" }},
                 {"cometAzure%", new InputOption{active = true, value = "1" }},
                 {"cometShower%", new InputOption{active = true, value = "1" }},
                 {"summonWolf%", new InputOption{active = true, value = "5" }},
                 {"inherit%", new InputOption{active = true, value = "43" }}
              }
            },
            {"DarkGodWindow",
                new Dictionary<string, InputOption>
              {
                  {"cloudOfDarkness%", new InputOption{active = true, value = "0.01" }},
                  {"blackHole%", new InputOption{active = true, value = "0.1" }},
                  {"darkDaggers%", new InputOption{active = true, value = "4" }},
                  {"smokeFlash%", new InputOption{active = true, value = "1" }},
                  {"summonDarkOne%", new InputOption{active = true, value = "4" }},
                  {"inherit%", new InputOption{active = true, value = "40" }}
              }
            },
            {"SunGodWindow",
                new Dictionary<string, InputOption>
              {
                  {"flashOfLight%", new InputOption{active = true, value = "1" }},
                  {"beamOfLight%", new InputOption{active = true, value = "8" }},
                  {"speedOfLight%", new InputOption{active = true, value = "2" }},
                  {"lightBallz%", new InputOption{active = true, value = "0.5" }},
                  {"inherit%", new InputOption{active = true, value = "45" }}
              }
            },
            {"WarGodWindow",
                new Dictionary<string, InputOption>
              {
                  {"warGodsCry%", new InputOption{active = true, value = "1" }},
                  {"axeThrow%", new InputOption{active = true, value = "3" }},
                  {"seedsOfWar%", new InputOption{active = true, value = "2" }},
                  {"inherit%", new InputOption{active = true, value = "15" }}
              }
            },
            {"EarthGodWindow",
                new Dictionary<string, InputOption>
              {
                  {"earthquake%", new InputOption{active = true, value = "5" }},
                  {"makeItRain%", new InputOption{active = true, value = "10" }},
                  {"buildWorld%", new InputOption{active = true, value = "1" }},
                  {"SummonDruid%", new InputOption{active = true, value = "10" }},
                  {"inherit%", new InputOption{active = true, value = "20" }}
              }
            },
            {"ChaosGodWindow",
                new Dictionary<string, InputOption>
              {
                  {"Power1%", new InputOption{active = true, value = "1" }},
                  {"Power2%", new InputOption{active = true, value = "5" }},
                  {"Power3%", new InputOption{active = true, value = "1" }},
                  {"Power4%", new InputOption{active = true, value = "10" }},
                  {"inherit%", new InputOption{active = true, value = "30" }}
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

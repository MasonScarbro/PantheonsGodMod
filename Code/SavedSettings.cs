using System;
using System.Collections.Generic;

namespace GodsAndPantheons
{
    [Serializable]
    public class SavedSettings
    {
        public string settingVersion = "0.1.3";
        //inheritance of the god traits is stored here, if the baby has two parents and both of them are god's or has one parent who is a god, it use the inherit for the chance
        //if it has a god parent and normal parent it will use the inherit for the chance of changing the stats of the demi god trait for the baby
        public Dictionary<string, Dictionary<string, InputOption>> Chances = new Dictionary<string, Dictionary<string, InputOption>>
        {
            {"KnowledgeGodWindow",
              new Dictionary<string, InputOption>
             {
              {"KnowledgeGodPwr1%", new InputOption{active = true, value = 20 }},
              {"KnowledgeGodPwr2%", new InputOption{active = true, value = 5 }},
              {"KnowledgeGodPwr3%", new InputOption{active = true, value = 20 }},
              {"KnowledgeGodPwr4%", new InputOption{active = true, value = 1 }},
              {"KnowledgeGodPwr5%", new InputOption{active = true, value = 5 }},
              {"SummonLightning%", new InputOption{active = true, value = 9 }},
              {"SummonMeteor%", new InputOption{active = true, value = 3 }},
              {"PagesOfKnowledge%", new InputOption{active = true, value = 3 }},
              {"EnemySwap%", new InputOption{active = true, value = 20 }},
              {"God Of Knowledgeinherit%", new InputOption{active = true, value = 35 }}
             }   
            },
            {"LichGodWindow", 
                new Dictionary<string, InputOption>
              {
                {"waveOfMutilation%", new InputOption{active = true, value = 20 }},
                {"summonSkele%", new InputOption{active = true, value = 8 }},
                {"summonDead%", new InputOption{active = true, value = 9 }},
                {"rigorMortisHand%", new InputOption{active = true, value = 10 }},
                {"God Of The Lichinherit%", new InputOption{active = true, value = 25 }}
              } 
            },
            {"GodOfGodsWindow",
                new Dictionary<string, InputOption>
              {
                 {"Terrain bending%", new InputOption{active = true, value = 10 }},
                 {"Summoning%", new InputOption{active = true, value = 15 }},
                 {"Magic%", new InputOption{active = true, value = 20 }},
                 {"God Of godsinherit%", new InputOption{active = true, value = 10 }}
              }
            },
            {"MoonGodWindow",
                new Dictionary<string, InputOption>
              {
                 {"summonMoonChunk%", new InputOption{active = true, value = 4 }},
                 {"cometAzure%", new InputOption{active = true, value = 7 }},
                 {"cometShower%", new InputOption{active = true, value = 10 }},
                 {"summonWolf%", new InputOption{active = true, value = 17 }},
                 {"God Of the Starsinherit%", new InputOption{active = true, value = 43 }}
              }
            },
            {"DarkGodWindow",
                new Dictionary<string, InputOption>
              {
                  {"cloudOfDarkness%", new InputOption{active = true, value = 1 }},
                  {"blackHole%", new InputOption{active = true, value = 3 }},
                  {"darkDaggers%", new InputOption{active = true, value = 10 }},
                  {"smokeFlash%", new InputOption{active = true, value = 15 }},
                  {"summonDarkOne%", new InputOption{active = true, value = 15 }},
                  {"God Of the Nightinherit%", new InputOption{active = true, value = 45 }}
              }
            },
            {"SunGodWindow",
                new Dictionary<string, InputOption>
              {
                  {"flashOfLight%", new InputOption{active = true, value = 15 }},
                  {"beamOfLight%", new InputOption{active = true, value = 8 }},
                  {"speedOfLight%", new InputOption{active = true, value = 7 }},
                  {"lightBallz%", new InputOption{active = true, value = 5 }},
                  {"God Of lightinherit%", new InputOption{active = true, value = 40 }}
              }
            },
            {"WarGodWindow",
                new Dictionary<string, InputOption>
              {
                  {"warGodsCry%", new InputOption{active = true, value = 1 }},
                  {"axeThrow%", new InputOption{active = true, value = 29 }},
                  {"seedsOfWar%", new InputOption{active = true, value = 2 }},
                  {"God Of Warinherit%", new InputOption{active = true, value = 15 }}
              }
            },
            {"EarthGodWindow",
                new Dictionary<string, InputOption>
              {
                  {"earthquake%", new InputOption{active = true, value = 4 }},
                  {"makeItRain%", new InputOption{active = true, value = 20 }},
                  {"buildWorld%", new InputOption{active = true, value = 1 }},
                  {"SummonDruid%", new InputOption{active = true, value = 15 }},
                  {"God Of the Earthinherit%", new InputOption{active = true, value = 20 }}
              }
            },
            {"ChaosGodWindow",
                new Dictionary<string, InputOption>
              {
                  {"Power1%", new InputOption{active = true, value = 7 }},
                  {"Power2%", new InputOption{active = true, value = 20 }},
                  {"Power3%", new InputOption{active = true, value = 5 }},
                  {"Power4%", new InputOption{active = true, value = 15 }},
                  {"God Of Chaosinherit%", new InputOption{active = true, value = 30 }}
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
        public float value;
    }
}

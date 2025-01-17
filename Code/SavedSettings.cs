using Amazon.Runtime.Internal.Transform;
using System;
using System.Collections.Generic;

namespace GodsAndPantheons
{
    [Serializable]
    public class SavedSettings
    {
        public string settingVersion = "0.2.0";
        //inheritance of the god traits is stored here
        //if it has a god parent and normal parent it will use the inherit for the chance of changing the stats of the demi god trait for the baby
        public Dictionary<string, Dictionary<string, InputOption>> Chances = new Dictionary<string, Dictionary<string, InputOption>>
        {
            {"KnowledgeGodWindow",
              new Dictionary<string, InputOption>
             {
              {"UnleashFireAndAcid%", new InputOption{active = true, value = 20 }},
              {"CastCurses%", new InputOption{active = true, value = 5 }},
              {"Freeze%", new InputOption{active = true, value = 20 }},
              {"CreateShield%", new InputOption{active = true, value = 1 }},
              {"TeleprtTarget%", new InputOption{active = true, value = 1.8f }},
              {"SummonLightning%", new InputOption{active = true, value = 9 }},
              {"SummonMeteor%", new InputOption{active = true, value = 3 }},
              {"PagesOfKnowledge%", new InputOption{active = true, value = 3 }},
              {"EnemySwap%", new InputOption{active = true, value = 20 }},
              {"CorruptEnemy%", new InputOption{active = true, value = 15 }},
              {"God Of Knowledgeinherit%", new InputOption{active = true, value = 31 }}
             }   
            },
            {"LichGodWindow", 
                new Dictionary<string, InputOption>
              {
                {"waveOfMutilation%", new InputOption{active = true, value = 20 }},
                {"summonSkele%", new InputOption{active = true, value = 8 }},
                {"summonDead%", new InputOption{active = true, value = 9 }},
                {"rigorMortisHand%", new InputOption{active = true, value = 10 }},
                {"God Of The Lichinherit%", new InputOption{active = true, value = 36 }}
              } 
            },
            {"GodOfFireWindow",
                new Dictionary<string, InputOption>
              {
                 {"FireStorm%", new InputOption{active = true, value = 0.5f }},
                 {"MorphIntoDragon%", new InputOption{active = true, value = 15 }},
                 {"Summoning%", new InputOption{active = true, value = 1.2f }},
                 {"Magic%", new InputOption{active = true, value = 2 }},
                 {"ChaosLaser%", new InputOption{active = true, value = 1 }},
                 {"God Of Fireinherit%", new InputOption{active = true, value = 50 }}
              }
            },
            {"MoonGodWindow",
                new Dictionary<string, InputOption>
              {
                 {"summonMoonChunk%", new InputOption{active = true, value = 4 }},
                 {"cometAzure%", new InputOption{active = true, value = 2 }},
                 {"cometShower%", new InputOption{active = true, value = 4 }},
                 {"summonWolf%", new InputOption{active = true, value = 10 }},
                 {"God Of the Starsinherit%", new InputOption{active = true, value = 22 }}
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
                  {"God Of the Nightinherit%", new InputOption{active = true, value = 44 }}
              }
            },
            {"SunGodWindow",
                new Dictionary<string, InputOption>
              {
                  {"flashOfLight%", new InputOption{active = true, value = 15 }},
                  {"beamOfLight%", new InputOption{active = true, value = 8 }},
                  {"speedOfLight%", new InputOption{active = true, value = 7 }},
                  {"lightBallz%", new InputOption{active = true, value = 5 }},
                  {"God Of lightinherit%", new InputOption{active = true, value = 46 }}
              }
            },
            {"WarGodWindow",
                new Dictionary<string, InputOption>
              {
                  {"warGodsCry%", new InputOption{active = true, value = 1 }},
                  {"axeThrow%", new InputOption{active = true, value = 29 }},
                  {"seedsOfWar%", new InputOption{active = true, value = 0.2f }},
                  {"God Of Warinherit%", new InputOption{active = true, value = 17 }}
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
                  {"FireBall%", new InputOption{active = true, value = 28 }},
                  {"UnleashChaos%", new InputOption{active = true, value = 40 }},
                  {"ChaosBoulder%", new InputOption{active = true, value = 15 }},
                  {"BoneFire%", new InputOption{active = true, value = 30 }},
                  {"WorldOfChaos%", new InputOption{active = true, value = 5 }},
                  {"God Of Chaosinherit%", new InputOption{active = true, value = 28 }}
              }
            },
            {"LoveGodWindow",
                new Dictionary<string, InputOption>
              {
                  {"Poisoning%", new InputOption{active = true, value = 50 }},
                  {"healAllies%", new InputOption{active = true, value = 40}},
                  {"blessAllies%", new InputOption{active = true, value = 10 }},
                  {"CastShields%", new InputOption{active = true, value = 15 }},
                  {"CorruptEnemys%", new InputOption{active = true, value = 20 }},
                  {"God Of Loveinherit%", new InputOption{active = true, value = 55 }}
              }
            }
        };
        public bool deathera = true;
        public bool HunterAssasins = true;
        public bool DevineMiracles = false;
        public bool GodKings = true;
        public bool MakeSummoned = false;
        public bool DiplomacyChanges = true;
    }
    [Serializable]
    public struct InputOption
    {
        public bool active;
        public float value;
    }
}

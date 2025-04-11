using Newtonsoft.Json;
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
              {"UnleashFireAndAcid%", new InputOption (true, 20, "unleashes fire and acid")},
              {"CastCurses%", new InputOption(true, 5, "casts curses on non-gods")},
              {"Freeze%", new InputOption(true, 20, "freezes the enemy")},
              {"CreateShield%", new InputOption(true, 3, "creates a temporary shield")},
              {"TeleprtTarget%", new InputOption(true, 5, "teleports the enemy if the god is low on health")},
              {"SummonLightning%", new InputOption(true, 9, "casts lightning on the enemy")},
              {"SummonMeteor%", new InputOption(true, 3, "Summons a meteor on the enemy")},
              {"PagesOfKnowledge%", new InputOption(true, 3, "shoots special projectiles at the enemy")},
              {"UseForce%", new InputOption(true, 10, "[Special] allows the god to use telekinesis to pull enemies into the air and fling them around")},
              {"EnemySwap%", new InputOption(true, 20, "[Special] allows the god to predict when he is attacked, and swap places with a nearby enemy so they get hit instead")},
              {"CorruptEnemy%", new InputOption(true, 15, "[ITEM] allows the holder of the staff of knowledge to corrupt enemy's to make them their minions for a short time")},
              {"God Of Knowledgeinherit%", new InputOption(true, 31, "the chance of a child of a god of knowledge to inherit a stat / ability / trait")}
             }   
            },
            {"LichGodWindow", 
                new Dictionary<string, InputOption>
              {
                {"waveOfMutilation%", new InputOption(true, 20, "[ITEM] shoots a very powerfull wave of mutilation on the enemy")},
                {"summonSkele%", new InputOption(true, 8, "summons skeletons")},
                {"summonDead%", new InputOption(true, 9, "summons zombies")},
                {"UndeadArmy%", new InputOption(true, 5, "[Special] Converts nearby enemies into zombies")},
                {"rigorMortisHand%", new InputOption(true, 10, "pulls an undead hand from the ground to grab the enemy")},
                {"God Of The Lichinherit%", new InputOption(true, 36, "the chance of a child of a god of Lich to inherit a stat / ability / trait")}
              } 
            },
            //his children are terribly weak without his staff, so they need a high chance of inheriting
            {"GodOfFireWindow",
                new Dictionary<string, InputOption>
              {
                 {"FireStorm%", new InputOption(true, 4, "Can create a cloud of ash, fire tornados, or the FIRE STORM")},
                 {"MorphIntoDragon%", new InputOption(true, 30, "[SPECIAL] the god of fire can morph into a dragon when sorrounded by enemies")},
                 {"Summoning%", new InputOption(true, 3, "Summons mages, fire skeletons and demons")},
                 {"Magic%", new InputOption(true, 4, "creates explosions, lava and fire")},
                 {"ChaosLaser%", new InputOption(true, 2, "[ITEM] allows the holder of the staff of fire to cast a super-deadly laster to annihilate the enemy")},
                 {"God Of Fireinherit%", new InputOption(true, 55, "the chance of a child of a god of fire to inherit a stat / ability / trait")}
              }
            },
            {"MoonGodWindow",
                new Dictionary<string, InputOption>
              {
                 {"summonMoonChunk%", new InputOption(true, 10, "[ITEM] Summons a moon chunk out of the sky onto \n the enemy")},
                 {"MoonOrbit%", new InputOption(true, 10, "[Special] Creates orbiting Moon chunks around the \n god")},
                 {"cometAzure%", new InputOption(true, 6, "Summons a Powerfull comet on the enemy")},
                 {"cometShower%", new InputOption(true, 8, "Summons a comet shower on the enemy")},
                 {"summonWolf%", new InputOption(true, 10, "Summons wolfs")},
                 {"God Of the Starsinherit%", new InputOption(true, 24, "the chance of a child of a god of stars to inherit a stat / ability / trait")}
              }
            },
            {"DarkGodWindow",
                new Dictionary<string, InputOption>
              {
                  {"DarkDash%", new InputOption(true, 30, "[ITEM] allows the holder of the dark dagger to dash into enemies and deal damage")},
                  {"cloudOfDarkness%", new InputOption(true, 1, "Creates a massive cloud of darkness that shrouds the area in darkness")},
                  {"blackHole%", new InputOption(true, 6, "Creates a black hole that sucks in enemies dealing alot of damage over time")},
                  {"darkDaggers%", new InputOption(true, 10, "throws dark daggers at enemies")},
                  {"smokeFlash%", new InputOption(true, 15, "creates a flash of smoke")},
                  {"summonDarkOne%", new InputOption(true, 15, "summons dark ones, minions of the gods of dark")},
                  {"God Of the Nightinherit%", new InputOption(true, 44, "the chance of a child of a god of night to inherit a stat / ability / trait")}
              }
            },
            {"SunGodWindow",
                new Dictionary<string, InputOption>
              {
                  {"flashOfLight%", new InputOption(true, 15, "creates a flash of light that blinds the enemy")},
                  {"beamOfLight%", new InputOption(true, 8, "Creates a beam of light that lights the enemy \n on fire")},
                  {"speedOfLight%", new InputOption(true, 7, "makes the god of light go incredibly fast!")},
                  {"lightBallz%", new InputOption(true, 5, "shoots balls of light at the enemy")},
                  {"God Of lightinherit%", new InputOption(true, 46, "the chance of a child of a god of sun to inherit a stat / ability / trait")}
              }
            },
            //god of war is the strongest, but his strength comes from his stats and not his abilities, so he needs a high inheritence chance for his children to be strong
            {"WarGodWindow",
                new Dictionary<string, InputOption>
              {
                  {"warGodsCry%", new InputOption(true, 5, "A cry of anger and rage that inspires allies and pushes the enemy")},
                  {"axeThrow%", new InputOption(true, 29, "throws a axe")},
                  {"seedsOfWar%", new InputOption(true, 1, "allows the god of war to make political decisions while also making enemies mad")},
                  {"StunEnemy%", new InputOption(true, 20, "stuns an enemy")},
                  {"axemaelstrom%", new InputOption(true, 8, "[ITEM] allows the holder of the Axe of fury to create a axe maelstrom, throwing many, many axes at enemies")},
                  {"BlockAttack%", new InputOption(true, 16, "[Special] lets the god of war block an attack")},
                  {"War Gods Leap%", new InputOption(true, 20, "[Special] allows the god of war to leap to the skies and crash down in a big explosion")},
                  {"God Of Warinherit%", new InputOption(true, 40, "the chance of a child of a god of war to inherit a stat / ability / trait")}
              }
            },
            {"EarthGodWindow",
                new Dictionary<string, InputOption>
              {
                  {"earthquake%", new InputOption(true, 10, "[Special] Creates a earthquake")},
                  {"makeItRain%", new InputOption(true, 20, "makes rain, and snow clouds")},
                  {"buildWorld%", new InputOption(true, 20, "[Special] creates mountains and resources")},
                  {"StalagmitePath%", new InputOption(true, 20, "[ITEM] Pulls spikes out of the ground to impale the enemy")},
                  {"SendMountain%", new InputOption(true, 10, "Sends a Mountain path towards the enemy")},
                  {"LiftRocks%", new InputOption(true, 15, "Lifts rocks out of the ground and shoots them at the enemy")},
                  {"SummonDruid%", new InputOption(true, 15, "summons druids")},
                  {"God Of the Earthinherit%", new InputOption(true, 20, "the chance of a child of a earth god to inherit a stat / ability / trait")}
              }
            },
            {"ChaosGodWindow",
                new Dictionary<string, InputOption>
              {
                  {"FireBall%", new InputOption(true, 28, "shoots a fire ball")},
                  {"UnleashChaos%", new InputOption(true, 40, "creates a cloud of rage and makes all nearby creatures mad, including allies")},
                  {"ChaosBoulder%", new InputOption(true, 15, "summons a boulder on the enemy")},
                  {"BoneFire%", new InputOption(true, 30, "[ITEM] creates a bone fire that has orbs that create corrupted brains")},
                  {"WorldOfChaos%", new InputOption(true, 5, "Shakes the area, flinging everyone across the \n map")},
                  {"God Of Chaosinherit%", new InputOption(true, 28, "the chance of a child of a god of chaos to inherit a stat / ability / trait")}
              }
            },
            {"LoveGodWindow",
                new Dictionary<string, InputOption>
              {
                  {"Poisoning%", new InputOption(true, 13, "Poisons, and slows the enemy")},
                  {"healAllies%", new InputOption(true, 40, "heals allies, [SPECIAL] the god of love can shoot hearts to heal allies")},
                  {"blessAllies%", new InputOption(true, 5, "blesses allies")},
                  {"CastShields%", new InputOption(true, 15, "casts shields on nearby allies")},
                  {"CorruptEnemys%", new InputOption(true, 10, "blinds enemies")},
                  {"Petrification%", new InputOption(true, 20, "[ITEM] petrifies the enemy, making them forever unable to move or attack")},
                  {"God Of Loveinherit%", new InputOption(true, 40, "the chance of a child of a god of love to inherit a stat / ability / trait")}
              }
            }
        };
        public bool deathera = true;
        public bool HunterAssasins = true;
        public bool DevineMiracles = false;
        public bool GodKings = true;
        public bool MakeSummoned = false;
        public bool DiplomacyChanges = true;
        public bool AutoTraits = true;
    }
    [Serializable]
    public class InputOption
    {
        public bool active;
        public int value;
        public InputOption(bool active, int value, string Description)
        {
            this.value = value;
            this.active = active;
            this.Description = Description;
        }
        public void Set(int value, bool active)
        {
            this.value = value;
            this.active = active;
        }
        [JsonIgnore] public readonly string Description;
    }
}

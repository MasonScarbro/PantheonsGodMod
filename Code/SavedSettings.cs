using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Core.Tokens;

namespace GodsAndPantheons
{
    [Serializable]
    public class SavedSettings
    {
        public string settingVersion = "0.2.3";
        public Dictionary<string, InputOption> this[string ID] => Chances[ID];

        public Dictionary<string, Dictionary<string, InputOption>> Chances = new Dictionary<string, Dictionary<string, InputOption>>
        {
            {"KnowledgeGodWindow",
              new Dictionary<string, InputOption>
             {
              {"UnleashFireAndAcid%", new InputOption (20, "unleashes fire and acid")},
              {"CastCurses%", new InputOption(5, "casts curses on non-gods")},
              {"Freeze%", new InputOption(20, "freezes the enemy")},
              {"CreateShield%", new InputOption(3, "creates a temporary shield")},
              {"TeleprtTarget%", new InputOption(5, "teleports the enemy if the god is low on health")},
              {"SummonLightning%", new InputOption(9, "casts lightning on the enemy")},
              {"SummonMeteor%", new InputOption(3, "Summons a meteor on the enemy")},
              {"PagesOfKnowledge%", new InputOption(3, "shoots special projectiles at the enemy")},
              {"UseForce%", new InputOption(10, "[Special] allows the god to use telekinesis to pull enemies into the air and fling them around")},
              {"EnemySwap%", new InputOption(20, "[Special] allows the god to predict when he is attacked, and swap places with a nearby enemy so they get hit instead")},
              {"CorruptEnemy%", new InputOption(15, "[ITEM] allows the holder of the staff of knowledge to corrupt enemy's to make them their minions for a short time")},
              {"God Of Knowledgeinherit%", new InputOption(31, "the chance of a child of a god of knowledge to inherit a stat / ability / trait")}
             }
            },
            {"LichGodWindow",
                new Dictionary<string, InputOption>
              {
                {"waveOfMutilation%", new InputOption(20, "[ITEM] shoots a very powerfull wave of mutilation on the enemy")},
                {"summonSkele%", new InputOption(8, "summons skeletons")},
                {"summonDead%", new InputOption(9, "summons zombies")},
                {"UndeadArmy%", new InputOption(5, "[Special] Converts nearby enemies into zombies to serve as his minions")},
                {"rigorMortisHand%", new InputOption(10, "pulls an undead hand from the ground to grab the enemy")},
                {"God Of The Lichinherit%", new InputOption(36, "the chance of a child of a god of Lich to inherit a stat / ability / trait")}
              }
            },
            //his children are terribly weak without his staff, so they need a high chance of inheriting
            {"GodOfFireWindow",
                new Dictionary<string, InputOption>
              {
                 {"FireStorm%", new InputOption(4, "Can create a cloud of ash, fire tornados, or the FIRE STORM")},
                 {"FireBreath%", new InputOption(15, "Creates a fire breath") },
                 {"MorphIntoDragon%", new InputOption(30, "[SPECIAL] the god of fire can morph into a dragon when sorrounded by enemies")},
                 {"Summoning%", new InputOption(3, "Summons mages, fire skeletons and demons")},
                 {"Explosions%", new InputOption(4, "creates explosions, lava and fire")},
                 {"ChaosLaser%", new InputOption(2, "[ITEM] allows the holder of the staff of fire to cast a super-deadly laser to annihilate the enemy")},
                 {"God Of Fireinherit%", new InputOption(44, "the chance of a child of a god of fire to inherit a stat / ability / trait")}
              }
            },
            {"MoonGodWindow",
                new Dictionary<string, InputOption>
              {
                 {"summonMoonChunk%", new InputOption(10, "Summons a moon chunk out of the sky onto \n the enemy")},
                 {"MoonOrbit%", new InputOption(10, "[ITEM] Creates orbiting Moon chunks around the \n god")},
                 {"NightOfTheBloodMoon%", new InputOption(3, "[Special] creates a blood moon that wrecks havic on the enviornment")},
                 {"cometAzure%", new InputOption(6, "Summons a Powerfull comet on the enemy")},
                 {"cometShower%", new InputOption(8, "Summons a comet shower on the enemy")},
                 {"summonWolf%", new InputOption(10, "Summons wolfs")},
                 {"God Of the Starsinherit%", new InputOption(30, "the chance of a child of a god of stars to inherit a stat / ability / trait")}
              }
            },
            {"DarkGodWindow",
                new Dictionary<string, InputOption>
              {
                  {"DarkDash%", new InputOption(30, "[ITEM] allows the holder of the dark dagger to dash into enemies and deal damage")},
                  {"cloudOfDarkness%", new InputOption(1, "Creates a massive cloud of darkness that shrouds the area in darkness")},
                  {"blackHole%", new InputOption(6, "Creates a black hole that sucks in enemies dealing alot of damage over time")},
                  {"darkDaggers%", new InputOption(10, "throws dark daggers at enemies")},
                  {"GodOfTheShadows%", new InputOption(8, "[SPECIAL] turns the god of dark invisible")},
                  {"smokeFlash%", new InputOption(15, "creates a flash of smoke")},
                  {"summonDarkOne%", new InputOption(15, "summons dark ones, minions of the gods of dark")},
                  {"God Of the Nightinherit%", new InputOption(40, "the chance of a child of a god of night to inherit a stat / ability / trait")}
              }
            },
            {"SunGodWindow",
                new Dictionary<string, InputOption>
              {
                  {"flashOfLight%", new InputOption(15, "creates a flash of light that blinds the enemy")},
                  {"beamOfLight%", new InputOption(8, "Creates a beam of light that lights the enemy \n on fire")},
                  {"speedOfLight%", new InputOption(7, "makes the god of light go incredibly fast!")},
                  {"lightBallz%", new InputOption(8, "shoots balls of light at the enemy")},
                  {"AtTheSpeedOfLight%", new InputOption(10, "[SPECIAL] the god of light can move at such high speeds that anything he touches is annihilated....") },
                  {"LightOrbs%", new InputOption(5, "[ITEM] creates light orbs")},
                  {"SunGodsSlashes%", new InputOption(20, "Creates Light Slashes") },
                  {"God Of lightinherit%", new InputOption(42, "the chance of a child of a god of sun to inherit a stat / ability / trait")}
              }
            },
            //god of war is the strongest, but his strength comes from his stats and not his abilities, so he needs a high inheritence chance for his children to be strong
            {"WarGodWindow",
                new Dictionary<string, InputOption>
              {
                  {"warGodsCry%", new InputOption(5, "A cry of anger and rage that inspires allies and pushes the enemy")},
                  {"axeThrow%", new InputOption(29, "throws a axe")},
                  {"seedsOfWar%", new InputOption(8, "allows the god of war to make political decisions while also making enemies mad")},
                  {"StunEnemy%", new InputOption(20, "stuns an enemy")},
                  {"axemaelstrom%", new InputOption(1, "[ITEM] allows the holder of the Axe of fury to create a axe maelstrom, throwing many, many axes at enemies")},
                  {"BlockAttack%", new InputOption(16, "[Special] lets the god of war block an attack")},
                  {"War Gods Leap%", new InputOption(20, "[Special] allows the god of war to leap to the skies and crash down in a big explosion")},
                  {"God Of Warinherit%", new InputOption(35, "the chance of a child of a god of war to inherit a stat / ability / trait")}
              }
            },
            {"EarthGodWindow",
                new Dictionary<string, InputOption>
              {
                  {"earthquake%", new InputOption(10, "[Special] Creates a earthquake")},
                  {"makeItRain%", new InputOption(20, "makes rain, and snow clouds")},
                  {"buildWorld%", new InputOption(20, "[Special] creates mountains and resources")},
                  {"StalagmitePath%", new InputOption(20, "[ITEM] Pulls spikes out of the ground to impale the enemy")},
                  {"SendMountain%", new InputOption(10, "Sends a Mountain path towards the enemy")},
                  {"LiftRocks%", new InputOption(15, "Lifts rocks out of the ground and shoots them at the enemy")},
                  {"SummonDruid%", new InputOption(15, "summons druids")},
                  {"God Of the Earthinherit%", new InputOption(26, "the chance of a child of a earth god to inherit a stat / ability / trait")}
              }
            },
            {"ChaosGodWindow",
                new Dictionary<string, InputOption>
              {
                  {"FireBall%", new InputOption(28, "shoots a fire ball")},
                  {"UnleashChaos%", new InputOption(40, "creates a cloud of rage and makes all nearby creatures mad, including allies")},
                  {"ChaosBoulder%", new InputOption(15, "summons a boulder on the enemy")},
                  {"BoneFire%", new InputOption(3, "[ITEM] creates a bone fire that has orbs that create corrupted brains")},
                  {"LordOfMadness%", new InputOption(4, "[Special] The god of chaos brings madness wherever he goes...")},
                  {"WorldOfChaos%", new InputOption(7, "Shakes the area, flinging everyone across the \n map")},
                  {"God Of Chaosinherit%", new InputOption(36, "the chance of a child of a god of chaos to inherit a stat / ability / trait")}
              }
            },
            {"LoveGodWindow",
                new Dictionary<string, InputOption>
              {
                  {"Poisoning%", new InputOption(13, "Poisons, and slows the enemy")},
                  {"healAllies%", new InputOption(40, "heals allies, [SPECIAL] the god of love can shoot hearts to heal allies")},
                  {"blessAllies%", new InputOption(5, "blesses allies")},
                  {"CastShields%", new InputOption(15, "casts shields on nearby allies")},
                  {"CorruptEnemys%", new InputOption(10, "blinds enemies")},
                  {"Petrification%", new InputOption(20, "[ITEM] petrifies the enemy, making them forever unable to move or attack")},
                  {"God Of Loveinherit%", new InputOption(40, "the chance of a child of a god of love to inherit a stat / ability / trait")}
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
        public InputOption(int value, string Description)
        {
            this.value = value;
            this.active = true;
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

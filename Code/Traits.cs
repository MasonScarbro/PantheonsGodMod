/* 
AUTHOR: MASON SCARBRO
VERSION: 1.0.0
*/
using System;
using System.Collections.Generic;
using UnityEngine;
namespace GodsAndPantheons
{
    //Contains the traits and their abilities & Stats
    partial class Traits
    {

        #region TraitStats Dict

        public static Dictionary<string, Dictionary<string, float>> TraitStats = new Dictionary<string, Dictionary<string, float>>()
        {
            {"God Of Chaos", new Dictionary<string, float>(){
                {S.damage, 40f},
                {S.health, 500},
                {S.mana, 300 },
                {S.attack_speed, 15f},
                {S.accuracy, 30},
                {S.armor, 20 },
                {S.critical_chance, 0.05f},
                {S.range, 8f},
                {S.scale, 0.08f},
                {S.max_nutrition, 60},
                {S.offspring, 60}
             }
            },
            {"God Of light", new Dictionary<string, float>(){
                {S.damage, 20f},
                {S.mana, 300 },
                {S.health, 500f},
                {S.accuracy, 30},
                {S.attack_speed, 100f},
                {S.critical_chance, 0.05f},
                {S.range, 5f},
                {S.speed, 90f},
                {S.max_nutrition, 30},
                {S.offspring, 80},
             }
            },
            {"God Of the Night", new Dictionary<string, float>(){
                {S.damage, 20f},
                {S.health, 550f},
                {S.mana, 300 },
                {S.accuracy, 30},
                {S.attack_speed, 3f},
                {S.critical_chance, 0.25f},
                {S.range, 6f},
                {S.scale, 0.02f},
                {S.max_nutrition, 30},
                {S.offspring, 60},
             }
            },
            {"God Of Knowledge", new Dictionary<string, float>(){
                {S.damage, 5f},
                {S.health, 300},
                {S.mana, 300 },
                {S.accuracy, 30},
                {S.attack_speed, 1f},
                {S.critical_chance, 0.25f},
                {S.range, 15f},
                {S.scale, 0.01f},
                {S.intelligence, 35f},
                {S.max_nutrition, 20},
                {S.offspring, 60}
             }
            },
            {"God Of the Stars", new Dictionary<string, float>(){
                {S.damage, 40},
                {S.mana, 300 },
                {S.health, 500f},
                {S.accuracy, 30},
                {S.attack_speed, 1f},
                {S.critical_chance, 0.05f},
                {S.range, 15f},
                {S.scale, 0.02f},
                {S.intelligence, 3f},
                {S.max_nutrition, 20},
                {S.offspring, 70}
             }
            },
            {"God Of the Earth", new Dictionary<string, float>(){
                {S.damage, 60f},
                {S.health, 800f},
                {S.attack_speed, 1f},
                {S.mana, 300 },
                {S.accuracy, 30},
                {S.armor, 50f},
                {S.mass, 30f },
                {S.scale, 0.1f},
                {S.range, 10f},
                {S.intelligence, 3f},
                {S.max_nutrition, 30},
                {S.offspring, 80},
             }
            },
            {"God Of War", new Dictionary<string, float>(){
                {S.damage, 100f},
                {S.health, 700f},
                {S.attack_speed, 35f},
                {S.armor, 35f},
                {S.mass, 0.05f},
                {S.mana, 300 },
                {S.accuracy, 30},
                {S.scale, 0.03f},
                {S.range, 8f},
                {S.warfare, 40f},
                {S.max_nutrition, 30},
                {S.offspring, 50},
             }
            },
            {"God Of The Lich", new Dictionary<string, float>(){
                {S.damage, 100f},
                {S.health, 750f},
                {S.attack_speed, 35f},
                {S.armor, 25f},
                {S.mass, 0.05f},
                {S.scale, 0.03f},
                {S.accuracy, 30},
                {S.mana, 300 },
                {S.range, 8f},
                {S.warfare, 40f},
                {S.max_nutrition, 20},
                {S.offspring, 60},
             }
            },
            //NEW_GOD_TRAIT_STATS
            {"God Killer", new Dictionary<string, float>(){
                {S.damage, 10f},
                {S.health, 100f},
                {S.attack_speed, 15f},
                {S.mana, 100 },
                {S.mass, 0.1f},
                {S.accuracy, 10},
                {S.scale, 0.01f},
                {S.range, 4f},
                {S.warfare, 4f},
                {S.max_nutrition, 20}
             }
            },
            {"God Of Fire", new Dictionary<string, float>(){
                {S.damage, 25},
                {S.health, 700f},
                {S.attack_speed, 12f},
                {S.critical_chance, 0.5f},
                {S.mana, 300 },
                {S.armor, 30f},
                {S.accuracy, 30},
                {S.scale, 0.075f},
                {S.range, 20f},
                {S.speed, 30f},
                {S.max_nutrition, 30},
                {S.offspring, 60},
             }
            },
            {"God Of Love", new Dictionary<string, float>(){
                {S.health, 700},
                {S.intelligence, 10f },
                {S.speed, 15f},
                {S.mass, 0.05f},
                {S.mana, 300 },
                {S.accuracy, 30},
                {S.offspring, 100},
                {S.loyalty_traits, 10f },
                {S.max_nutrition, 60},
                {S.diplomacy, 15 }
             }
            },
            {"Summoned One", new Dictionary<string, float>(){
                {S.damage, 10f},
                {S.health, 20f},
                {S.armor, 10f},
                {S.speed, 15},
                {S.mass, 1},
             }
            }
        };

        #endregion

        #region AutoTraits Dict
        // *********************************************************************************************** //
        public static Dictionary<string, List<string>> AutoTraits = new Dictionary<string, List<string>>()
        {
            {"God Of Fire", new List<string>()
             {
                "evil",
                "poison_immune",
                "fire_proof",
                "acid_proof",
                "shiny",
                "fire_blood",
                "immortal",
                "regeneration",
                "immune",
                "strong_minded"
             }
            },
            {"God Of Love", new List<string>()
             {
                "blessed",
                "poison_immune",
                "shiny",
                "lustful",
                "fertile",
                "fast",
                "immortal",
                "regeneration",
                "thorns",
                "immune",
                "strong_minded"
             }
            },
            {"God Of light", new List<string>()
             {
                "blessed",
                "shiny",
                "agile",
                "fire_proof",
                "fire_blood",
                "weightless",
                "fast",
                "energized",
                "light_lamp",
                "immortal",
                "immune",
                "strong_minded"
             }
            },
            {"God Of Knowledge", new List<string>()
             {
                "blessed",
                "genius",
                "fire_proof",
                "freeze_proof",
                "tough",
                "energized",
                "immortal",
                "strong_minded",
                "wise",
                "immune"
             }
            },
            {"God Of the Night", new List<string>()
             {
                "blessed",
                "bloodlust",
                "agile",
                "cold_aura",
                "freeze_proof",
                "nightchild",
                "energized",
                "immortal",
                "strong_minded",
                "moonchild",
                "immune"
             }
            },
            {"God Of the Stars", new List<string>()
             {
                "blessed",
                "bloodlust",
                "weightless",
                "shiny",
                "freeze_proof",
                "nightchild",
                "energized",
                "immortal",
                "strong_minded",
                "moonchild",
                "immune"
             }
            },
            {"God Of the Earth", new List<string>()
             {
                "blessed",
                "poison_immune",
                "giant",
                "strong",
                "fat",
                "fire_proof",
                "freeze_proof",
                "tough",
                "immortal",
                "immune",
                "strong_minded",
                "Earth Walker"
             }
            },
            {"God Of Chaos", new List<string>()
             {
                "blessed",
                "giant",
                "strong",
                "fat",
                "fire_proof",
                "freeze_proof",
                "tough",
                "immortal",
                "immune",
                "strong_minded"
             }
            },
            {"God Of War", new List<string>()
             {
                "blessed",
                "strong",
                "ambitious",
                "freeze_proof",
                "fire_proof",
                "pyromaniac",
                "veteran",
                "immortal",
                "tough",
                "immune",
                "strong_minded"
             }
            },
            {"God Of The Lich", new List<string>()
             {
                "blessed",
                "strong",
                "acid_touch",
                "acid_blood",
                "acid_proof",
                "regeneration",
                "immortal",
                "tough",
                "immune",
                "strong_minded",
                "NecroMancer"
             }
            },
            {"God Killer", new List<string>()
             {
                "blessed",
                "strong",
                "tough",
             }
            }
        };
        // *********************************************************************************************** //
        #endregion

        #region TraitEras
        public static Dictionary<string, KeyValuePair<string, string>> TraitEras = new Dictionary<string, KeyValuePair<string, string>>()
        {
            {"God Of light", new KeyValuePair<string, string>("age_sun", "Lights_Prevail") },
            {"God Of Fire", new KeyValuePair<string, string>("age_ash", "God_Of_All") },
            {"God Of the Stars", new KeyValuePair<string, string>("age_moon", "Stars_Prevail") },
            {"God Of the Night", new KeyValuePair<string, string>("age_dark", "Nights_Prevail") },
            {"God Of Knowledge", new KeyValuePair<string, string>("age_wonders", "Knowledge_Prevail") },
            {"God Of Chaos", new KeyValuePair<string, string>(S.age_chaos, "Chaos Prevails") },
            {"God Of The Lich", new KeyValuePair<string, string>(S.age_tears, "Sorrow Prevails") },
            {"God Of War", new KeyValuePair<string, string>(S.age_despair, "Despair Prevails") },
            {"God Of the Earth", new KeyValuePair<string, string>(S.age_ash, "Earth Prevails") },
            {"God Of Love", new KeyValuePair<string, string>(S.age_hope, "Love Prevails") },
        };
        #endregion

        #region GodAbilities Dict
        //any abilities here can be inherited
        public static Dictionary<string, List<AttackAction>> GodAbilities = new Dictionary<string, List<AttackAction>>()
        {
            {"God Of Fire", new List<AttackAction>(){
                new AttackAction(FireStorm),
                new AttackAction(Summoning),
                new AttackAction(Magic),
                new AttackAction(FireBreath)
             }
            },
            {"God Of Chaos", new List<AttackAction>(){
                new AttackAction(ChaosBall),
                new AttackAction(UnleachChaos),
                new AttackAction(ChaosHell),
                new AttackAction(ChaosBoulder)
             }
            },
            {"God Of the Stars", new List<AttackAction>(){
                new AttackAction(CometShower),
                new AttackAction(cometAzure),
                new AttackAction(SummonWolf)
             }
            },
            {"God Of Knowledge", new List<AttackAction>(){
                new AttackAction(CreateElements),
                new AttackAction(trydefendself),
                new AttackAction(SummonMeteor),
                new AttackAction(PagesOfKnowledge)
             }
            },
            {"God Of the Night", new List<AttackAction>(){
                new AttackAction(CloudOfDarkness),
                new AttackAction(darkdaggers),
                new AttackAction(smokeflash),
                new AttackAction(summondarkones)
             }
            },
            {"God Of light", new List<AttackAction>(){
                new AttackAction(LightBallz),
                new AttackAction(FlashOfLight),
                new AttackAction(SpeedOfLight),
                new AttackAction(BeamOfLight)
             }
            },
            {"God Of War", new List<AttackAction>(){
                new AttackAction(wargodscry),
                new AttackAction(SeedsOfWar),
                new AttackAction(WarGodThrow),
                new AttackAction(StunEnemy)
             }
            },
            {"God Of the Earth", new List<AttackAction>(){
                new AttackAction(SummonDruids),
                new AttackAction(MakeItRain),
                new AttackAction(buildMountainPath),
                new AttackAction(PullRocks)
             }
            },
            {"God Of The Lich", new List<AttackAction>(){
                new AttackAction(SummonHand),
                new AttackAction(summonskele),
                new AttackAction(SummonDead)
             }
            },
            {"God Of Love", new List<AttackAction>(){
                new AttackAction(HealAllies),
                new AttackAction(BlessAllies),
                new AttackAction(CastShields),
                new AttackAction(CorruptEnemy),
                new AttackAction(PoisonEnemys),
             }
            }
        };
        #endregion
        #region God Relations
        public static Dictionary<string, List<string>> GodEnemies = new Dictionary<string, List<string>>()
        {
            {"God Of light", new List<string>(){ "God Of the Stars", "God Of the Night" } },
            { "God Of the Stars", new List<string>(){ "God Of light" } },
            { "God Of the Night", new List<string>(){ "God Of light" } },
            { "God Of Chaos", new List<string>(){ "God Of Knowledge" } },
            { "God Of War", new List<string>(){ "God Of Knowledge", "God Of Love" } },
            { "God Of Love", new List<string>(){ "God Of The Lich", "God Of War" } },
            { "God Of The Lich", new List<string>(){ "God Of Love" } },
            { "God Of Knowledge", new List<string>(){ "God Of Chaos", "God Of War" } }
        };
        #endregion
        public static void init()
        {
            static bool godGiveWeapon(NanoObject pTarget, BaseAugmentationAsset asset)
            {
                static string GetId(string ID)
                => ID switch
                {
                    "God Of War" => "AxeOfFury",
                    "God Of the Night" => "DarkDagger",
                    "God Of light" => "SpearOfLight",
                    "God Of Knowledge" => "StaffOfKnowledge",
                    "God Of Chaos" => "MaceOfDestruction",
                    "God Of the Stars" => "CometScepter",
                    "God Of The Lich" => "LichGodsGreatSword",
                    "God Of the Earth" => "HammerOfCreation",
                    "God Of Love" => "StaffOfLove",
                    "God Of Fire" => "HellStaff",
                    "God Hunter" => "GodHuntersScythe",
                    _ => "Sword",
                };
                Actor pActor = (Actor)pTarget;
                if (pActor.asset.use_items)
                {
                    pActor.equipment.weapon?.throwOutItem();
                    Item Item = World.world.items.generateItem(AssetManager.items.get(GetId(asset.id)), pActor.kingdom, pActor.name, 1, pActor);
                    Item.data.name = Item.asset.translation_key;
                    Item.addMod("divine_rune");
                    pActor.equipment.setItem(Item, pActor);
                }
                return true;
            }
            //TRAIT GROUPS
            ActorTraitGroupAsset MTtraits = new ActorTraitGroupAsset();
            MTtraits.id = "GodTraits";
            MTtraits.name = "The Traits Of Gods";
            MTtraits.color = "000000";
            AssetManager.trait_groups.add(MTtraits);
            addTraitGroupToLocalizedLibrary(MTtraits.name, "The Traits Of Gods");

            ActorTraitGroupAsset NTraits = new ActorTraitGroupAsset();
            NTraits.id = "NonGodTraits";
            NTraits.name = "Non God Traits But Still Special";
            NTraits.color = "FFFFFF";
            AssetManager.trait_groups.add(NTraits);
            addTraitGroupToLocalizedLibrary(NTraits.name, "Non God Traits But Still Special");

            ActorTrait chaosGod = new ActorTrait();
            chaosGod.id = "God Of Chaos";
            chaosGod.path_icon = "ui/icons/chaosGod";
            chaosGod.action_on_add = (WorldActionTrait)Delegate.Combine(new WorldActionTrait(AutoTrait), new WorldActionTrait(godGiveWeapon));
            chaosGod.action_attack_target = new AttackAction(ActionLibrary.addBurningEffectOnTarget);
            chaosGod.action_attack_target = new AttackAction(chaosGodAttack);
            //chaosGod.action_death = new WorldAction(ActionLibrary.turnIntoDemon);
            chaosGod.action_death = (WorldAction)Delegate.Combine(chaosGod.action_death, new WorldAction(chaosGodsTrick));
            chaosGod.action_special_effect = (WorldAction)Delegate.Combine(chaosGod.action_special_effect, new WorldAction(chaosgoderastatus));
            chaosGod.group_id = "GodTraits";
            chaosGod.rarity = Rarity.R3_Legendary;
            AddTrait(chaosGod, "Tis's The God Of Chaos!");
            
            ActorTrait sunGod = new ActorTrait();
            sunGod.id = "God Of light";
            sunGod.path_icon = "ui/icons/lightGod";
            sunGod.action_on_add = (WorldActionTrait)Delegate.Combine(new WorldActionTrait(AutoTrait), new WorldActionTrait(godGiveWeapon));
            sunGod.action_attack_target = new AttackAction(ActionLibrary.addBurningEffectOnTarget);
            sunGod.action_attack_target = new AttackAction(sunGodAttack);
            sunGod.action_death = new WorldAction(sunGodsDeath);
            sunGod.action_special_effect = new WorldAction(sungoderastatus);
            sunGod.group_id = "GodTraits";
            sunGod.rarity = Rarity.R3_Legendary;
            AddTrait(sunGod, "The God Of light, controls the very light that shines and can pass through with great speed");

            ActorTrait darkGod = new ActorTrait();
            darkGod.id = "God Of the Night";
            darkGod.path_icon = "ui/icons/godDark";
            darkGod.action_attack_target = new AttackAction(darkGodAttack);
            darkGod.action_death = (WorldAction)Delegate.Combine(darkGod.action_death, new WorldAction(darkGodsDeath));
            darkGod.action_special_effect = (WorldAction)Delegate.Combine(darkGod.action_special_effect, new WorldAction(nightgoderastatus));
            darkGod.action_on_add = (WorldActionTrait)Delegate.Combine(new WorldActionTrait(AutoTrait), new WorldActionTrait(godGiveWeapon));
            darkGod.group_id = "GodTraits";
            darkGod.rarity = Rarity.R3_Legendary;
            AddTrait(darkGod, "The God Of darkness, thievery and the shadows of which is his domain ");

            ActorTrait knowledgeGod = new ActorTrait();
            knowledgeGod.id = "God Of Knowledge";
            knowledgeGod.path_icon = "ui/icons/knowledgeGod";
            knowledgeGod.action_special_effect = (WorldAction)Delegate.Combine((WorldAction)KnowledgeGodUseForce, (WorldAction)knowledgegoderastatus);
            knowledgeGod.action_attack_target = new AttackAction(knowledgeGodAttack);
            knowledgeGod.action_on_add = (WorldActionTrait)Delegate.Combine(new WorldActionTrait(AutoTrait), new WorldActionTrait(godGiveWeapon));
            knowledgeGod.group_id = "GodTraits";
            knowledgeGod.rarity = Rarity.R3_Legendary;
            AddTrait(knowledgeGod, "The God Of Knowledge, His mind excedes Time, he knows all");

            ActorTrait starsGod = new ActorTrait();
            starsGod.id = "God Of the Stars";
            starsGod.path_icon = "ui/icons/starsGod";
            starsGod.action_death = (WorldAction)Delegate.Combine(starsGod.action_death, new WorldAction(starsGodsDeath));
            starsGod.action_attack_target = (AttackAction)Delegate.Combine(new AttackAction(starsGodAttack), new AttackAction(CreateMoonOrbit));
            starsGod.action_special_effect = (WorldAction)Delegate.Combine(starsGod.action_special_effect, new WorldAction(stargoderastatus));
            starsGod.group_id = "GodTraits";
            starsGod.rarity = Rarity.R3_Legendary;
            starsGod.action_on_add = (WorldActionTrait)Delegate.Combine(new WorldActionTrait(AutoTrait), new WorldActionTrait(godGiveWeapon));
            AddTrait(starsGod, "Now Cometh the Age of stars, A Thousand Year Voyage under the wisdom of the moon");

            ActorTrait earthGod = new ActorTrait();
            earthGod.id = "God Of the Earth";
            earthGod.path_icon = "ui/icons/earthGod";
            earthGod.group_id = "GodTraits";
            earthGod.rarity = Rarity.R3_Legendary;
            earthGod.action_attack_target = (AttackAction)Delegate.Combine(new AttackAction(EarthGodAttack), new AttackAction(EarthQuake));
            earthGod.action_special_effect = (WorldAction)Delegate.Combine(new WorldAction(earthGodBuildWorld));
            earthGod.action_on_add = (WorldActionTrait)Delegate.Combine(new WorldActionTrait(AutoTrait), new WorldActionTrait(godGiveWeapon));
            earthGod.action_special_effect = (WorldAction)Delegate.Combine(earthGod.action_special_effect, new WorldAction(earthgoderastatus));
            AddTrait(earthGod, "God of the Natural Enviornment, The titan of creation");

            ActorTrait subGod = new ActorTrait();
            subGod.id = "Lesser God";
            subGod.rarity = Rarity.R3_Legendary;
            subGod.path_icon = "ui/icons/subGod";
            subGod.group_id = S_TraitGroup.special;
            subGod.can_be_given = false;
            subGod.action_attack_target = new AttackAction(LesserAttack);
            subGod.action_special_effect = new WorldAction(LesserEraStatus);
            AddTrait(subGod, "like the demigod, but can also inherit abilities! all children in a gods clan are lessergods");

            ActorTrait warGod = new ActorTrait();
            warGod.id = "God Of War";
            warGod.rarity = Rarity.R3_Legendary;
            warGod.path_icon = "ui/icons/warGod";
            warGod.action_attack_target = (AttackAction)Delegate.Combine(new AttackAction(warGodAttack), new AttackAction(SlamDunk));
            warGod.action_on_add = (WorldActionTrait)Delegate.Combine(new WorldActionTrait(AutoTrait), new WorldActionTrait(godGiveWeapon));
            warGod.action_special_effect = (WorldAction)Delegate.Combine(warGod.action_special_effect, new WorldAction(warGodSeeds));
            warGod.action_special_effect = (WorldAction)Delegate.Combine(warGod.action_special_effect, new WorldAction(wargoderastatus));
            warGod.group_id = "GodTraits";
            AddTrait(warGod, "God of Conflict, Bravery, Ambition, Many spheres of domain lie with him");

            ActorTrait lichGod = new ActorTrait();
            lichGod.id = "God Of The Lich";
            lichGod.rarity = Rarity.R3_Legendary;
            lichGod.path_icon = "ui/icons/lichGod";
            lichGod.action_attack_target = new AttackAction(lichGodAttack);
            lichGod.action_special_effect = (WorldAction)Delegate.Combine(new WorldAction(lichGodsUndeadArmy));
            lichGod.action_on_add = (WorldActionTrait)Delegate.Combine(new WorldActionTrait(AutoTrait), new WorldActionTrait(godGiveWeapon));
            lichGod.action_special_effect = (WorldAction)Delegate.Combine(lichGod.action_special_effect, new WorldAction(lichgoderastatus));
            lichGod.group_id = "GodTraits";
            AddTrait(lichGod, "God of Dead Souls, Corruption, and Rot, Many spheres of domain lie with him");

            //NEW_GOD_INIT

            ActorTrait GodOfLove = new ActorTrait();
            GodOfLove.id = "God Of Love";
            GodOfLove.rarity = Rarity.R3_Legendary;
            GodOfLove.path_icon = "ui/icons/GodOfLove";
            GodOfLove.group_id = "GodTraits";
            GodOfLove.action_death = new WorldAction(GodOfLoveDeath);
            GodOfLove.action_on_add = (WorldActionTrait)Delegate.Combine(new WorldActionTrait(AutoTrait), new WorldActionTrait(godGiveWeapon));
            GodOfLove.action_special_effect = (WorldAction)Delegate.Combine(GodOfLove.action_special_effect, new WorldAction(GodOfLoveerastatus));
            GodOfLove.action_special_effect = (WorldAction)Delegate.Combine(GodOfLove.action_special_effect, new WorldAction(HealAllies));
            GodOfLove.special_effect_interval = 0.0001f;
            GodOfLove.action_attack_target = new AttackAction(GodOfLoveAttack);
            AddTrait(GodOfLove, "The God of Hope, love and compassion");

            ActorTrait godoffire = new ActorTrait();
            godoffire.id = "God Of Fire";
            godoffire.rarity = Rarity.R3_Legendary;
            godoffire.path_icon = "ui/icons/GodOfFire";
            godoffire.action_death = new WorldAction(ActionLibrary.deathBomb);
            godoffire.action_on_add = (WorldActionTrait)Delegate.Combine(new WorldActionTrait(AutoTrait), new WorldActionTrait(godGiveWeapon));
            godoffire.action_special_effect = (WorldAction)Delegate.Combine(godoffire.action_special_effect, new WorldAction(godoffireerastatus));
            godoffire.action_special_effect = (WorldAction)Delegate.Combine(godoffire.action_special_effect, new WorldAction(MorphIntoDragon));
            godoffire.action_death = new WorldAction(GodOfFireDeath);
            godoffire.special_effect_interval = 0.5f;
            godoffire.action_attack_target += new AttackAction(GodOfFireAttack);
            godoffire.group_id = "GodTraits";
            AddTrait(godoffire, "The Most Powerfull of the gods, the god of fire, many spheres of domain lie with him");

            ActorTrait SummonedOne = new ActorTrait();
            SummonedOne.id = "Summoned One";
            SummonedOne.path_icon = "ui/icons/SummonedOne";
            SummonedOne.group_id = S_TraitGroup.special;
            SummonedOne.default_for_actor_assets = new List<ActorAsset>() { AssetManager.actor_library.get("DarkOne") };
            SummonedOne.can_be_given = false;
            SummonedOne.action_special_effect = (WorldAction)Delegate.Combine(new WorldAction(SummonedBeing), new WorldAction(SummonedOneEraStatus));
            AddTrait(SummonedOne, "A creature summoned by God himself in order to aid them in battle");

            ActorTrait DemiGod = new ActorTrait();
            DemiGod.id = "Demi God";
            DemiGod.path_icon = "ui/icons/IconDemi";
            DemiGod.rarity = Rarity.R2_Epic;
            DemiGod.group_id = S_TraitGroup.special;
            DemiGod.can_be_given = false;
            AddTrait(DemiGod, "The Demi God, offspring of Gods and Mortals, the stat's of this trait are determined by the stats of his parents");

            //NON GOD TRAITS
            
            ActorTrait EarthWalker = new ActorTrait();
            EarthWalker.id = "Earth Walker";
            EarthWalker.path_icon = "ui/icons/earthGod";
            EarthWalker.group_id = "NonGodTraits";
            EarthWalker.rarity = Rarity.R2_Epic;
            EarthWalker.can_be_given = true;
            AddTrait(EarthWalker, "He can walk on mountains", 20);
            
            ActorTrait NecroMancer = new ActorTrait();
            NecroMancer.id = "NecroMancer";
            NecroMancer.rarity = Rarity.R2_Epic;
            NecroMancer.path_icon = "ui/icons/lichGod";
            NecroMancer.group_id = "NonGodTraits";
            NecroMancer.can_be_given = true;
            AddTrait(NecroMancer, "When he kills a mortal, they transform into his undead minion", 2);

            ActorTrait godKiller = new ActorTrait();
            godKiller.id = "God Killer";
            godKiller.rarity = Rarity.R2_Epic;
            godKiller.path_icon = "ui/icons/godKiller";
            godKiller.action_on_add = new WorldActionTrait(AutoTrait);
            godKiller.group_id = "NonGodTraits";
            AddTrait(godKiller, "To Kill a God is nearly to become one");

            ActorTrait godHunter = new ActorTrait();
            godHunter.id = "God Hunter";
            godHunter.default_for_actor_assets = new List<ActorAsset>() { AssetManager.actor_library.get("GodHunter") };
            godHunter.path_icon = "ui/icons/godKiller";
            godHunter.action_special_effect = new WorldAction(SuperRegeneration);
            godHunter.action_on_add = (WorldActionTrait)Delegate.Combine(new WorldActionTrait(godGiveWeapon));
            godHunter.action_special_effect = (WorldAction)Delegate.Combine(godHunter.action_special_effect, new WorldAction(InvisibleCooldown));
            godHunter.action_attack_target = new AttackAction(GodHunterAttack);
            godHunter.group_id = "NonGodTraits";
            AddTrait(godHunter, "He will stop at NOTHING to kill a god");
        }

        //****************************** MAIN ATTACK STUFF ******************************//
        private static bool GodHunterAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (pSelf.isActor())
            {
                pSelf.a.data.set("invisiblecooldown", 10);
            }
            return true;
        }
        //to make summoned ones only live for like 60 secounds
        public static bool SummonedBeing(BaseSimObject pTarget, WorldTile pTile)
        {
            Actor a = (Actor)pTarget;
            if (a.hasTrait("immortal"))
            {
                return false;
            }
            a.data.get("lifespanincreased", out bool increased);
            a.data.get("lifespan", out int lifespan);
            a.data.get("life", out int life);
            a.data.set("life", life + 1);
            if (life + 1 > (increased ? lifespan * 2 : lifespan))
            {
                a.die(false, AttackType.Age, false, true, true);
            }
            return true;
        }
        public static bool InvisibleCooldown(BaseSimObject pTarget, WorldTile pTile)
        {
            if (Main.savedSettings.HunterAssasins)
            {
                pTarget.a.data.get("invisiblecooldown", out int invisiblecooldown);
                pTarget.a.data.set("invisiblecooldown", invisiblecooldown > 0 ? invisiblecooldown - 1 : 0);
                if (invisiblecooldown == 0)
                {
                    pTarget.addStatusEffect("Invisible", 12);
                }
            }
            return true;
        }
        public static void CastCurse(Actor pTarget)
        {
            if(IsGod(pTarget) || pTarget.hasTrait("blessed"))
            {
                return;
            }
            pTarget.addStatusEffect("cursed", 100);
        }
        public static bool SuperRegeneration(BaseSimObject pTarget, WorldTile pTile) => SuperRegeneration(pTarget, 15, 5);
        #region attackthings
        // The Main Attack Function to set up a new attack
        public static bool GodAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile, string godid)
        {
            if (GodAbilities.ContainsKey(godid) && pTarget != null)
            {
                foreach (AttackAction ability in GodAbilities[godid])
                {
                    ability(pSelf, pTarget, pTile);
                }
                return true;
            }
            return false;
        }
        public static bool LesserAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            foreach (string godtrait in Getinheritedgodtraits(pSelf.a.data))
            {
                foreach (AttackAction ability in GodAbilities[godtrait])
                {
                    pSelf.a.data.get("Lesser" + ability.Method.Name, out bool inherited);
                    if (inherited)
                    {
                        ability(pSelf, pTarget, pTile);
                    }
                }
            }
            return true;
        }
        #endregion

        #region ChaosGodsAttack
        public static bool ChaosHell(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (!Randy.randomChance(Chance("God Of Chaos", "WorldOfChaos%")))
            {
                return false;
            }
            ShakeEveryone(pTile, 192);
            World.world.startShake(1, 0.02f, 1.5f);
            return true;
        }
        public static void ShakeEveryone(WorldTile pTile, int Radius = 128)
        {
            static City GetClosestCity(Actor a)
            {
                float Num = 128;
                City currentcity = null;
                foreach (City City in a.kingdom.cities)
                {
                    float Dist = Vector3.Distance(City.city_center, a.current_position);
                    if (Dist < Num)
                    {
                        Num = Dist;
                        currentcity = City;
                    }
                }
                return currentcity;
            }
            static City GetTargetCity(ListPool<Kingdom> kingdoms, Actor a)
            {
                float Num = 192;
                City currentcity = null;
                foreach (Kingdom kingdom in kingdoms)
                {
                    foreach (City City in kingdom.cities)
                    {
                        float Dist = Vector3.Distance(City.city_center, a.current_position);
                        if (Dist < Num)
                        {
                            Num = Dist;
                            currentcity = City;
                        }
                    }
                }
                return currentcity;
            }
            foreach (Actor a in Finder.getUnitsFromChunk(pTile, (Radius / 16) + 1, Radius))
            {
                if (IsGod(a))
                {
                    continue;
                }
                if (a.kingdom.isCiv())
                {
                    if (Randy.randomChance(0.7f))
                    {
                        ListPool<Kingdom> kingdoms = a.kingdom.getEnemiesKingdoms();
                        if (kingdoms == null || kingdoms.Count == 0)
                        {
                            PushActorRandomly(a);
                            continue;
                        }
                        City city = GetTargetCity(kingdoms, a);
                        if (city == null)
                        {
                            PushActorRandomly(a);
                            continue;
                        }
                        PushActorTowardsTile(city.zones.GetRandom().tiles.GetRandom().pos, a, Randy.randomFloat(0.6f, 1.2f));
                        continue;
                    }
                    City city2 = GetClosestCity(a);
                    if (city2 == null)
                    {
                        PushActorRandomly(a);
                        continue;
                    }
                    PushActorTowardsTile(city2.zones.GetRandom().tiles.GetRandom().pos, a, Randy.randomFloat(0.6f, 1.2f));
                }
                else
                {
                    PushActorRandomly(a);
                }
            }
        }
        public static void PushActorRandomly(Actor pActor, int Dist = -1)
        {
            if (Dist == -1)
            {
                Dist = Randy.randomInt(20, 40);
            }
            PushActorTowardsTile(Toolbox.getRandomTileWithinDistance(pActor.current_tile, Dist).pos, pActor, Randy.randomFloat(0.6f, 1.2f));
        }
        public static bool ChaosBall(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Randy.randomChance(Chance("God Of Chaos", "FireBall%")))
            {
                ShootCustomProjectile(pSelf, pTarget, "fireBallX");
            }
            return true;
        }

        public static bool UnleachChaos(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Randy.randomChance(Chance("God Of Chaos", "UnleashChaos%")))
            {
                foreach (Actor a in Finder.getUnitsFromChunk(pTile, 1, 16))
                {
                    if (a != pSelf.a && !IsGod(a))
                    {
                        a.addTrait("madness");
                    }
                }
                pb.spawnCloudRage(pTile, null);
            }
            return true;
        }

        public static bool ChaosBoulder(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Randy.randomChance(Chance("God Of Chaos", "ChaosBoulder%")))
            {
                //pb.prepareBoulder(pTarget.current_tile, null);
            }
            return true;
        }

        public static bool chaosGodAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile) => GodAttack(pSelf, pTarget, pTile, "God Of Chaos");


        #endregion

        #region KnowledgeGodsAttack
        public static bool KnowledgeGodUseForce(BaseSimObject pActor, WorldTile pTile)
        {
            if (!Randy.randomChance(Chance("God Of Knowledge", "UseForce%")))
            {
                return true;
            }
            var Enemies = EnemiesFinder.findEnemiesFrom(pTile, pActor.kingdom, 3).list;
            if(Enemies == null)
            {
                return true;
            }
            if(Enemies.Count < 6)
            {
                return true;
            }
            SpawnCustomWave(pTile.posV3, 0.025f, 0.05f, 2);
            MusicBox.playSound("event:/SFX/EXPLOSIONS/ExplosionForce", pTile, false, false);
            foreach (BaseSimObject enemy in Enemies)
            {
                if(Randy.randomBool() && enemy.isActor() && !enemy.hasStatus("Levitating") && enemy != pActor.a && !IsGod(enemy.a))
                {
                    enemy.a.addForce(0, 0, 0.5f);
                    enemy.addStatusEffect("Levitating", Randy.randomInt(4, 10));
                }
            }
            return true;
        }
        public static bool CreateElements(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Randy.randomChance(Chance("God Of Knowledge", "UnleashFireAndAcid%")))
            {
                // randomly spawns a flash of fire or acid on the tile 
                MapBox.instance.drop_manager.spawn(pTile, "fire", 5f, -1f);
                MapBox.instance.drop_manager.spawn(pTile, "acid", 5f, -1f);
                MapBox.instance.drop_manager.spawn(pTile, "fire", 5f, -1f); // Drops fire from distance 5 with scale of one at current tile
            }
            if (Randy.randomChance(Chance("God Of Knowledge", "Freeze%")))
            {
                ActionLibrary.addFrozenEffectOnTarget(null, pTarget, null); // freezezz the target
            }
            if (Randy.randomChance(Chance("God Of Knowledge", "SummonLightning%")))
            {
                ActionLibrary.castLightning(pSelf, pTarget, null); // Casts Lightning on the target
            }
            return true;
        }

        public static bool trydefendself(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Randy.randomChance(Chance("God Of Knowledge", "CreateShield%")))
            {
                pSelf.addStatusEffect("shield", 10);
            }
            if (!pTarget.isActor())
            {
                return false;
            }
            if (Randy.randomChance(Chance("God Of Knowledge", "TeleprtTarget%")))
            {
                if (pSelf.a.data.health < pSelf.a.getMaxHealth() * 0.3f)
                {
                    ActionLibrary.teleportRandom(null, pTarget, null); // flee
                }
            }
            if (Randy.randomChance(Chance("God Of Knowledge", "CastCurses%")))
            {
                CastCurse(pTarget.a); // casts curses
            }
            return true;
        }
        public static bool SummonMeteor(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Randy.randomChance(Chance("God Of Knowledge", "SummonMeteor%")))
            {
                EffectsLibrary.spawn("fx_meteorite", pTarget.current_tile, "meteorite_disaster", null, 0f, -1f, -1f);    //spawn 1 meteorite
                pSelf.a.addStatusEffect("invincible", 1f);
            }
            return true;
        }

        public static bool PagesOfKnowledge(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Randy.randomChance(Chance("God Of Knowledge", "PagesOfKnowledge%")))
            {
                ShootCustomProjectile(pSelf, pTarget, "PagesOfKnowledge");
            }
            return true;
        }
        public static bool knowledgeGodAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile) => GodAttack(pSelf, pTarget, pTile, "God Of Knowledge");
        #endregion

        #region DarkGodsAttack

        public static void CloudOfDark(Storm s)
        {
            if (Randy.randomChance(0.2f))
            {
                pb.spawnLightning(Toolbox.getRandomTileWithinDistance(s.tile, 60), null);
            }
            if (Randy.randomChance(0.8f))
            {
                ActionLibrary.castCurses(s.ByWho, null, Toolbox.getRandomTileWithinDistance(s.tile, 60));
            }
            if (Randy.randomChance(0.5f))
            {
                pb.spawnForce(Toolbox.getRandomTileWithinDistance(s.tile, 60), null);
            }
            if (Randy.randomChance(0.3f))
            {
                CreateBlindess(Toolbox.getRandomTileWithinDistance(s.tile, 60), 5, 30, s.ByWho?.kingdom);
            }
        }
        public static bool CloudOfDarkness(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Randy.randomChance(Chance("God Of the Night", "cloudOfDarkness%")))
            {
                CreateStorm(pTile, 30f, 0.4f, CloudOfDark, new Color(1, 1, 1, 0.75f), 4f, pSelf);
            }
            if (Randy.randomChance(Chance("God Of the Night", "blackHole%")))
            {
                Vector2Int pos = pTile.pos; // Position of the Ptile as a Vector 2
                float pDist = Vector2.Distance(pTarget.current_position, pos); // the distance between the target and the pTile
                Vector3 newPoint = Toolbox.getNewPoint(pTarget.current_position.x, pTarget.current_position.y, (float)pos.x, (float)pos.y, pDist, true); // the Point of the projectile launcher 
                Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.current_position.x, pTarget.current_position.y, (float)pos.x, (float)pos.y, pTarget.stats[S.size], true);
                EffectsLibrary.spawn("fx_BlackHole", pTile)?.gameObject.GetComponent<BlackHoleFlash>().Init(pSelf, pTile);
            }
            return true;
        }
        public static bool darkdaggers(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Randy.randomChance(Chance("God Of the Night", "darkDaggers%")))
            {
                ShootCustomProjectile(pSelf, pTarget, "SunGodsSlashes");
            }
            return true;
        }
        public static bool smokeflash(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Randy.randomChance(Chance("God Of the Night", "smokeFlash%")))
            {
                EffectsLibrary.spawnAtTile("fx_smokeFlash_dej", pTile, 0.1f);
                MapAction.damageWorld(pTarget.current_tile, 5, AssetManager.terraform.get("lightning_power"), null);
                MapAction.damageWorld(pTarget.current_tile, 8, AssetManager.terraform.get("smokeFlash"), null);
                World.world.applyForceOnTile(pTarget.current_tile, 2, 0.4f, true, 20, null, pSelf);
            }
            return true;
        }
        public static bool summondarkones(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Randy.randomChance(Chance("God Of the Night", "summonDarkOne%")))
            {
                Summon("DarkOne", 4, pSelf, pTile);
            }
            return true;
        }
        public static bool darkGodAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile) => GodAttack(pSelf, pTarget, pTile, "God Of the Night");
        #endregion
        #region LightGodsAttack
        public static bool FlashOfLight(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Randy.randomChance(Chance("God Of light", "flashOfLight%")))
            {
                pb.divineLightFX(pTarget.current_tile, null);
                (EffectsLibrary.spawn("fx_napalm_flash", pTarget.current_tile, null, null, 0f, -1f, -1f) as NapalmFlash).bombSpawned = true;
                CreateBlindess(pTile, 10, 5f, pSelf.kingdom);
            }
            if (Randy.randomChance(0 / 100))
            {
                //EffectsLibrary.spawnAtTile("fx_multiFlash_dej", pTile, 0.01f);
                //not finushed?
                //TO BE USED AS IMACT ACTION FOR LIGHT PROJECILES LATER
                int count = 0;
                Vector2Int pos = pTile.pos; // Position of the Ptile as a Vector 2
                float pDist = Vector2.Distance(pTarget.current_position, pos);
                Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.current_position.x, pTarget.current_position.y, (float)pos.x, (float)pos.y, pTarget.stats[S.size], true);

                EffectsLibrary.spawn("fx_napalm_flash", pTarget.current_tile.neighbours[2].neighbours[2].neighbours[1].neighbours[1].neighbours[2], null, null, 0f, -1f, -1f);
                count++;
                //???????????????????????????????????????????????????????????????????????????
                if (count == 1)
                {
                    EffectsLibrary.spawn("fx_napalm_flash", pTarget.current_tile.neighbours[1].neighbours[0].neighbours[0].neighbours[1].neighbours[0].neighbours[0].neighbours[1].neighbours[1].neighbours[0].neighbours[0].neighbours[0], null, null, 0f, -0.5f, -0.2f);
                    count++;
                }
                if (count == 2)
                {
                    EffectsLibrary.spawn("fx_napalm_flash", pTarget.current_tile.neighbours[1].neighbours[3].neighbours[2].neighbours[1].neighbours[2].neighbours[3].neighbours[1].neighbours[1].neighbours[0].neighbours[0], null, null, 0f, 1f, -0.2f);
                    count++;
                }
                if (count == 3)
                {
                    EffectsLibrary.spawn("fx_napalm_flash", pTarget.current_tile.neighbours[1].neighbours[3].neighbours[2].neighbours[1].neighbours[2].neighbours[3].neighbours[1].neighbours[1].neighbours[0].neighbours[0], null, null, 0f, -0.5f, -0.2f);
                    EffectsLibrary.spawn("fx_napalm_flash", pTarget.current_tile.neighbours[1].neighbours[3].neighbours[0].neighbours[1].neighbours[0].neighbours[0].neighbours[0].neighbours[1].neighbours[0].neighbours[0], null, null, 0f, -0.5f, -0.2f);

                }
                pSelf.a.addStatusEffect("invincible", 1f);
            }
            return true;
        }
        public static void CreateBlindess(WorldTile pTile, int radius, float length, Kingdom kingdom = null)
        {
            foreach (Actor victim in Finder.getUnitsFromChunk(pTile, 2, radius))
            {
                if ((kingdom != null && victim.kingdom == kingdom) || IsGod(victim)) continue;
                victim.addStatusEffect("Blinded", length);
            }
        }
        public static bool BeamOfLight(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Randy.randomChance(Chance("God Of light", "beamOfLight%")))
            {
                pb.divineLightFX(pTarget.current_tile, null);
                pTarget.addStatusEffect("burning", 1f);
            }
            return true;
        }
        public static bool LightBallz(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Randy.randomChance(Chance("God Of light", "lightBallz%")))
            {
                ShootCustomProjectile(pSelf, pTarget, "lightBallzProjectiles");
            }
            return true;
        }
        public static bool SpeedOfLight(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Randy.randomChance(Chance("God Of light", "speedOfLight%")))
            {
                pb.divineLightFX(pTarget.current_tile, null);
                if (IsGod(pSelf.a))
                {
                    EffectsLibrary.spawn("fx_thunder_flash", pSelf.current_tile, null, null, 0f, -1f, -1f);
                }
                pSelf.a.addStatusEffect("caffeinated", 10f);
                pTarget.addStatusEffect("slowness", 10f);
            }
            return true;
        }
        public static bool sunGodAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile) => GodAttack(pSelf, pTarget, pTile, "God Of light");
        #endregion

        #region MoonGodsAttack
        public static bool cometAzure(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Randy.randomChance(Chance("God Of the Stars", "cometAzure%")))
            {
                EffectsLibrary.spawnAtTile("fx_cometAzureDown_dej", pTarget.current_tile, 0.1f);
                MapAction.applyTileDamage(pTarget.current_tile, 8, AssetManager.terraform.get("cometAzureDownDamage"));
                MapAction.damageWorld(pTarget.current_tile.neighbours[2], 8, AssetManager.terraform.get("cometAzureDownDamage"), null);
                MapAction.damageWorld(pTarget.current_tile.neighbours[1], 8, AssetManager.terraform.get("cometAzureDownDamage"), null);
                World.world.applyForceOnTile(pTarget.current_tile.neighbours[0], 4, 0.4f, true, 200, null, pSelf);
                pSelf.a.addStatusEffect("invincible", 1f);
            }
            return true;
        }
        private static bool CreateMoonOrbit(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Randy.randomChance(Chance("God Of the Stars", "MoonOrbit%")))
            {
                (EffectsLibrary.spawn("fx_Moon_Orbit", pTile, null, null, 0f, -1f, -1f) as MoonOrbit)?.Init(pSelf.a, pSelf.current_tile, Randy.randomFloat(5, 11));
                (EffectsLibrary.spawn("fx_Moon_Orbit", pTile, null, null, 0f, -1f, -1f) as MoonOrbit)?.Init(pSelf.a, pSelf.current_tile, Randy.randomFloat(5, 11), 70);
                (EffectsLibrary.spawn("fx_Moon_Orbit", pTile, null, null, 0f, -1f, -1f) as MoonOrbit)?.Init(pSelf.a, pSelf.current_tile, Randy.randomFloat(5, 11), 140);
            }
            return true;
        }
        public static bool CometShower(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Randy.randomChance(Chance("God Of the Stars", "cometShower%")))
            {
                EffectsLibrary.spawnAtTile("fx_cometShower_dej", pTarget.current_tile, 0.09f);
                MapAction.applyTileDamage(pTarget.current_tile, 2f, AssetManager.terraform.get("cometRain"));
                pSelf.addStatusEffect("invincible", 1f);
                // The Rain Damage effect ?????????/
                MapAction.applyTileDamage(pTarget.current_tile.neighbours[2].neighbours[2].neighbours[1].neighbours[1], 2f, AssetManager.terraform.get("cometRain"));
                MapAction.applyTileDamage(pTarget.current_tile.neighbours[3].neighbours[3].neighbours[2].neighbours[2], 1f, AssetManager.terraform.get("cometRain"));
                MapAction.applyTileDamage(pTarget.current_tile.neighbours[0].neighbours[0].neighbours[1].neighbours[2], 1f, AssetManager.terraform.get("cometRain"));
                MapAction.applyTileDamage(pTarget.current_tile.neighbours[1].neighbours[2].neighbours[3].neighbours[0], 1f, AssetManager.terraform.get("cometRain"));
                MapAction.applyTileDamage(pTarget.current_tile.neighbours[1].neighbours[1].neighbours[1].neighbours[1], 1f, AssetManager.terraform.get("cometRain"));
                MapAction.applyTileDamage(pTarget.current_tile.neighbours[2].neighbours[2].neighbours[2].neighbours[2], 1f, AssetManager.terraform.get("cometRain"));
                MapAction.applyTileDamage(pTarget.current_tile.neighbours[3].neighbours[3].neighbours[3].neighbours[3], 1f, AssetManager.terraform.get("cometRain"));
                MapAction.applyTileDamage(pTarget.current_tile.neighbours[0].neighbours[1], 2f, AssetManager.terraform.get("cometRain"));
                MapAction.applyTileDamage(pTarget.current_tile.neighbours[2].neighbours[3], 2f, AssetManager.terraform.get("cometRain"));
                MapAction.applyTileDamage(pTarget.current_tile.neighbours[0].neighbours[0].neighbours[0], 2f, AssetManager.terraform.get("cometRain"));
                World.world.applyForceOnTile(pTarget.current_tile.neighbours[3].neighbours[3].neighbours[2].neighbours[2], 2, 0.4f, true, 20, null, pSelf);
                World.world.applyForceOnTile(pTarget.current_tile.neighbours[3].neighbours[3].neighbours[3].neighbours[3], 2, 0.4f, true, 20, null, pSelf);
            }
            return true;
        }

        public static bool SummonWolf(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Randy.randomChance(Chance("God Of the Stars", "summonWolf%")))
            {
                Summon(SA.wolf, 3, pSelf, pTile);
            }
            return true;
        }

        public static bool starsGodAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile) => GodAttack(pSelf, pTarget, pTile, "God Of the Stars");
        #endregion

        #region GodOfWarsAttack
        public static bool wargodscry(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Randy.randomChance(Chance("God Of War", "warGodsCry%")))
            {
                EffectsLibrary.spawnExplosionWave(pSelf.current_tile.posV3, 1f, 1f);
                foreach (Actor a in Finder.getUnitsFromChunk(pSelf.current_tile, 1, 10))
                {
                    if (a.kingdom == pSelf.kingdom)
                    {
                        a.addStatusEffect("WarGodsCry", 10f);
                    }
                    else
                    {
                        PushActor(a, pSelf.current_tile.pos, 0.7f, 0.1f, true);
                    }
                }
                pSelf.addStatusEffect("WarGodsCry", 30f);
                if (IsGod(pSelf.a))
                {
                    World.world.startShake(0.3f, 0.01f, 2f, true, true);
                }
                MapAction.damageWorld(pSelf.current_tile, 2, AssetManager.terraform.get("crab_step"), pSelf);
            }
            return true;
        }

        public static bool SlamDunk(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if(Randy.randomChance(Chance("God Of War", "War Gods Leap%")) && !pSelf.hasStatus("War Gods Leap"))
            {
                if(BehFunctions.getalliesofactor(Finder.getUnitsFromChunk(pTile, 1, 6), pTarget) > 6)
                {
                    if (pSelf.addStatusEffect("War Gods Slam"))
                    {
                        PushActorTowardsTile(pTarget.current_tile.pos, pSelf.a, 3);
                    }
                }
            }
            return true;
        }
        public static bool WarGodThrow(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Randy.randomChance(Chance("God Of War", "axeThrow%")))
            {
                ShootCustomProjectile(pSelf.a, pTarget, "WarAxeProjectile1", 1);

            }
            return true;

        }
        public static bool StunEnemy(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (pTarget.isActor() && Randy.randomChance(Chance("God Of War", "StunEnemy%")))
            {
                MusicBox.playSound(MB.ExplosionLightningStrike, pTile);
                pTarget.a.addStatusEffect("Blinded", Randy.randomFloat(1f, 3f));
            }
            return true;
        }
        public static bool SeedsOfWar(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Randy.randomChance(Chance("God Of War", "seedsOfWar%")))
            {
                MapBox.instance.drop_manager.spawn(pTarget.current_tile, S_Drop.madness, 5f, -1f);
                MapBox.instance.drop_manager.spawn(Toolbox.getRandomTileWithinDistance(pTarget.current_tile, 5), S_Drop.madness, 5f, -1f);
                MapBox.instance.drop_manager.spawn(Toolbox.getRandomTileWithinDistance(pTarget.current_tile, 5), S_Drop.madness, 5f, -1f);
            }
            return true;
        }
        public static bool warGodAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile) => GodAttack(pSelf, pTarget, pTile, "God Of War");
        #endregion

        #region GodOfEarthsAttack
        public static bool EarthQuake(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Randy.randomChance(Chance("God Of the Earth", "earthquake%")))
            {
                pb.spawnEarthquake(pTarget.current_tile, null);
            }
            return true;
        }
        public static bool buildMountainPath(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Randy.randomChance(Chance("God Of the Earth", "SendMountain%")))
            {
                (EffectsLibrary.spawn("fx_Build_Path", pTile) as TerraformPath)?.Init(pSelf.current_tile, pTarget.current_tile, true, 0.1f, 1, true, pSelf, true);
            }
            return true;
        }

        public static bool MakeItRain(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Randy.randomChance(Chance("God Of the Earth", "makeItRain%")))
            {
                pb.spawnCloudRain(pTarget.current_tile, null);
            }
            if (Randy.randomChance(Chance("God Of the Earth", "makeItRain%")))
            {
                pb.spawnCloudSnow(pTarget.current_tile, null);
            }
            return true;
        }
        private static bool PullRocks(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Randy.randomChance(Chance("God Of the Earth", "LiftRocks%")))
            {
                (EffectsLibrary.spawn("fx_Pull_Rock", pTile) as PulledRock)?.Init(Toolbox.getRandomTileWithinDistance(pTile, 6), pSelf.a);
            }

            return true;
        }
        public static readonly List<string> earthgodminionautotraits = new List<string>() { "fire_proof", "freeze_proof", "regeneration" };

        public static bool SummonDruids(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Randy.randomChance(Chance("God Of the Earth", "SummonDruid%")))
            {
                Summon(SA.druid, 2, pSelf, pTile, 61, earthgodminionautotraits);
            }
            return true;
        }
        public static bool EarthGodAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile) => GodAttack(pSelf, pTarget, pTile, "God Of the Earth");
        #endregion

        #region LichGodsAttack
        public static bool lichGodAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile) => GodAttack(pSelf, pTarget, pTile, "God Of The Lich");
        public static bool lichGodsUndeadArmy(BaseSimObject pTarget, WorldTile pTile)
        {
            if(!Randy.randomChance(Chance("God Of The Lich", "UndeadArmy%"))){
                return true;
            }
            List<Actor> Enemies = GetEnemiesOfActor(Finder.getUnitsFromChunk(pTile, 1, 6), pTarget);
            if(Enemies.Count < 5)
            {
                return true;
            }
            MusicBox.playSound("event:/SFX/EXPLOSIONS/ExplosionForce", pTile, false, false);
            SpawnCustomWave(pTile.posV3, 0.025f, -0.05f, 2);
            foreach(Actor a in Enemies)
            {
                if (IsGod(a))
                {
                    continue;
                }
                Actor b = Morph(a, a.asset.getZombieID(), false, false);
                if (b != null)
                {
                    TurnActorIntoSummonedOne(b, pTarget.a, 31);
                }
                EffectsLibrary.spawnAtTile("fx_handgrab_dej", a.current_tile, 0.1f);
            }
            return true;
        }
        public static bool summonskele(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Randy.randomChance(Chance("God Of The Lich", "summonSkele%")))
            {
                //Lich God Summons Skeletons
                Summon(SA.skeleton, 8, pSelf, pTile);
            }
            return true;
        }

        public static bool SummonDead(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Randy.randomChance(Chance("God Of The Lich", "summonDead%")))
            {
                //Lich God Summons Zombie
                Summon(SA.zombie, 2, pSelf, pTile);
            }
            return true;
        }

        public static bool SummonHand(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Randy.randomChance(Chance("God Of The Lich", "rigorMortisHand%")))
            {
                //Lich God Summons RigorMortis Hand
                EffectsLibrary.spawnAtTile("fx_handgrab_dej", pTarget.current_tile, 0.1f);
                if (pTarget.isActor())
                {
                    pTarget.addStatusEffect("slowness");
                    pTarget.addStatusEffect("poisoned");
                }
            }
            return true;
        }

        #endregion

        //NEW_GOD_ATTACK_FUNC
        #region GodOfLoveStuff
        public static bool GodOfLoveDeath(BaseSimObject pSelf, WorldTile pTile)
        {
            if (Main.savedSettings.deathera)
            {
                World.world.era_manager.setCurrentAge(AssetManager.era_library.get(S.age_despair));
            }
            return true;
        }
        public static bool HealAllies(BaseSimObject pSelf, WorldTile pTile)
        {
            if (Randy.randomChance(Chance("God Of Love", "healAllies%")))
            {
                foreach(Actor a in Finder.getUnitsFromChunk(pTile, 1, 16))
                {
                    if(a.kingdom == pSelf.kingdom && a.data.health < a.getMaxHealth())
                    {
                        ShootCustomProjectile(pSelf.a, a, "Heart", 2);
                    }
                }
            }
            return true;
        }
        public static void CreateHeartExplosion(WorldTile pTile, float pRadius, bool corrupted = false, float pSpeed = 1f)
        {
            BaseEffect baseEffect = EffectsLibrary.spawn("fx_Heart" + (corrupted ? "_Corrupted" : ""), pTile, null, null, 0f, -1f, -1f);
            if (baseEffect == null)
            {
                return;
            }
            baseEffect.GetComponent<ExplosionFlash>().start(pTile.posV, pRadius, pSpeed);
        }
        public static bool BlessAllies(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Randy.randomChance(Chance("God Of Love", "blessAllies%")))
            {
                CreateHeartExplosion(pSelf.current_tile, 60f);
                foreach (Actor a in Finder.getUnitsFromChunk(pSelf.current_tile, 1, 6))
                {
                    if (a.kingdom == pSelf.kingdom)
                    {
                        a.a.addTrait("blessed");
                    }
                    else if(a.kingdom.isEnemy(pSelf.kingdom)) {
                        CastCurse(a);
                    }
                }
            }
            return true;
        }
        public static bool HealAllies(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Randy.randomChance(Chance("God Of Love", "healAllies%")))
            {
                CreateHeartExplosion(pSelf.current_tile, 100f);
                foreach (Actor a in Finder.getUnitsFromChunk(pSelf.current_tile, 1, 16))
                {
                    if (a.kingdom == pSelf.kingdom)
                    {
                        SuperRegeneration(a, 100f, 20f);
                    }
                }
            }
            return true;
        }
        public static bool CastShields(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Randy.randomChance(Chance("God Of Love", "CastShields%")))
            {
                CreateHeartExplosion(pSelf.current_tile, 80f);
                foreach (Actor a in Finder.getUnitsFromChunk(pSelf.current_tile, 1, 16))
                {
                    if (a.kingdom == pSelf.kingdom)
                    {
                        a.addStatusEffect("shield", 15f);
                    }
                }
            }
            return true;
        }
        public static bool CorruptEnemy(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Randy.randomChance(Chance("God Of Love", "CorruptEnemys%")))
            {
                if (!pTarget.isActor())
                {
                    return false;
                }
                CreateHeartExplosion(pTile, 80f, true);
                CreateBlindess(pTile, 8, 15f, pSelf.kingdom);
            }
            return true;
        }
        private static bool PoisonEnemys(BaseSimObject pSelf, BaseSimObject useless, WorldTile pTile)
        {
            foreach (BaseSimObject pTarget in Finder.getUnitsFromChunk(pTile, 1, 3))
            {
                if (pTarget.kingdom.isEnemy(pSelf.kingdom))
                {
                    if (Randy.randomChance(Traits.Chance("God Of Love", "Poisoning%")))
                    {
                        pTarget.addStatusEffect("slowness", 15);
                    }
                    if (Randy.randomChance(Traits.Chance("God Of Love", "Poisoning%")))
                    {
                        pTarget.addStatusEffect("poisoned", 30);
                    }
                    if (Randy.randomChance(Traits.Chance("God Of Love", "Poisoning%")))
                    {
                        pTarget.addStatusEffect("cough", 60);
                    }
                }
            }
            return true;
        }
        public static bool GodOfLoveAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile) => GodAttack(pSelf, pTarget, pTile, "God Of Love");
        #endregion

        #region GodOfFireStuff
        public static bool GodOfFireAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile) => GodAttack(pSelf, pTarget, pTile, "God Of Fire");

        public static void FireStorm(Storm s)
        {
            if (Toolbox.DistTile(s.tile, s.TileToGo) < 10 || (s.ByWho != null && Toolbox.DistTile(s.tile, s.ByWho.current_tile) < 20))
            {
               s.TileToGo = Toolbox.getRandomTileWithinDistance(s.tile, 120);
            }
            if (Randy.randomChance(0.1f))
            {
                s.UsingLaser = !s.UsingLaser;
            }
            if (Randy.randomChance(0.6f))
            {
                for (int i = 0; i < Randy.randomInt(3, 15); i++)
                {
                    LavaHelper.addLava(Toolbox.getRandomTileWithinDistance(s.tile, 10), "lava3");
                }
            }
            if (Randy.randomChance(0.8f)){
                MapAction.damageWorld(s.tile, 10, AssetManager.terraform.get("LesserCrabLaser"), s.ByWho);
            }
            if (Randy.randomChance(0.3f))
            {
                DropsLibrary.action_napalm_bomb(s.tile);
            }
            if (Randy.randomChance(0.8f))
            {
                World.world.applyForceOnTile(s.tile, 40, 1.5f, false, 5, null, s.ByWho);
            }
            if (Randy.randomChance(0.4f))
            {
                for(int i = 0; i < Randy.randomInt(5, 10); i++)
                    World.world.drop_manager.spawnParabolicDrop(s.tile, "lava", 0, 2, 100, 20, 90, 1.5f);
            }
        }
        public static bool FireStorm(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Randy.randomChance(Chance("God Of Fire", "FireStorm%")))
            {
                switch (Randy.randomInt(1, 10))
                {
                    case 1: case 2: case 3: case 4: case 5:  pb.spawnCloudAsh(pTile, null); break;
                    //FIRE TORNADOS
                    case 6: case 7: case 8:
                        {
                            for (int i = 0; i < Randy.randomInt(3, 6); i++)
                            {
                                EffectsLibrary.spawnAtTile("FireTornado", pTile, 0.35f);
                            }
                            break;
                        }
                    //FIRESTORM
                    case 9:
                        {
                            CreateStorm(pTile, 30f, 0.8f, FireStorm, Color.red, 0.8f).GetComponent<Storm>().TileToGo = Toolbox.getRandomTileWithinDistance(pTile, 100);
                            World.world.startShake(0.3f, 0.1f, 1);
                            break;
                        }
                }
            }
            return true;
        }
        public static bool Summoning(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Randy.randomChance(Chance("God Of Fire", "Summoning%")))
            {
                switch (Randy.randomInt(1, 4))
                {
                    case 1: Summon(SA.demon, 2, pSelf, pTile); break;
                    case 2: Summon(SA.evil_mage, 1, pSelf, pTile); break;
                    case 3: Summon(SA.fire_skull, 3, pSelf, pTile); break;
                }
            }
            return true;
        }

        public static bool MorphIntoDragon(BaseSimObject pSelf, WorldTile pTile)
        {
            bool IsDragon = pSelf.a.asset.id == SA.dragon;
            if (Randy.randomChance(Chance("God Of Fire", "MorphIntoDragon%")) || IsDragon)
            {
                List<BaseSimObject> enemies = EnemiesFinder.findEnemiesFrom(pTile, pSelf.kingdom, 3).list;
                if (!IsDragon)
                {
                    if (enemies?.Count > 10)
                    {
                        Morph(pSelf.a, SA.dragon);
                    }
                }
                else {
                    bool AnyEnemies = enemies != null && enemies.Count > 0;
                    if (AnyEnemies)
                    {
                        foreach (BaseSimObject enemy in enemies)
                        {
                            if (enemy.isActor())
                            {
                                pSelf.a.avatar.GetComponent<Dragon>().aggroTargets.Add(enemy.a);
                            }
                            if (Randy.randomChance(Chance("God Of Fire", "MorphIntoDragon%", 50)))
                            {
                                CreateFireExplosion(pSelf.a, enemy);
                            }
                        }
                    }
                    else
                    {
                        pSelf.a.data.get("oldself", out string oldself, SA.dragon);
                        Morph(pSelf.a, oldself);
                    }
                }
            }
            return true;
        }
        public static void CreateFireExplosion(BaseSimObject ByWho, BaseSimObject pTarget)
        {
            EffectsLibrary.spawnAtTile("FireGodsExplsion", pTarget.current_tile, Randy.randomFloat(0.1f, 0.3f));
            GodAttack(ByWho, pTarget, pTarget.current_tile, "God Of Fire");
            MapAction.damageWorld(pTarget.current_tile, 4, AssetManager.terraform.get("bomb"), ByWho);
        }
        public static void ShootCustomProjectile(BaseSimObject pSelf, BaseSimObject pTarget, string projectile, int amount = 1, float pZ = 0.25f, Vector2 Pos = default)
        {
            if(pSelf.kingdom == null)
            {
                return;
            }
            Vector3 Start = Pos == default ? pSelf.current_position : Pos;
            float tZ = 0f;
            if (pTarget.isInAir())
            {
                tZ = pTarget.getHeight();
            }
            for (int i = 0; i < amount; i++)
            {
                Vector3 tTargetPos = pTarget.current_tile.posV3;
                tTargetPos.x += Randy.randomFloat(-(pTarget.stats["size"] + 1f), pTarget.stats["size"] + 1f);
                tTargetPos.y += Randy.randomFloat(-pTarget.stats["size"], pTarget.stats["size"]);
                World.world.projectiles.spawn(pSelf, pTarget, projectile, Start, tTargetPos, tZ, pZ);
            }
        }
        public static bool FireBreath(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if(Toolbox.DistTile(pTarget.current_tile, pSelf.current_tile) > 10)
            {
                return true;
            }
            if (Randy.randomChance(Chance("God Of Fire", "FireBreath%")))
            {
                EffectsLibrary.spawn("FireBreath", pTile, null, null, 0, pSelf.current_position.x, pSelf.current_position.y, pSelf.a);
            }
            MapAction.damageWorld(pTile, 5, AssetManager.terraform.get("dragon_attack"), pSelf);
            return true;
        }
        public static bool Magic(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Randy.randomChance(Chance("God Of Fire", "Explosions%")))
            {
                switch (Randy.randomInt(1, 4))
                {
                    case 1: EffectsLibrary.spawn("fx_explosion_middle", pTarget.current_tile, null, null, 0f, -1f, -1f); break;

                    case 2:
                        EffectsLibrary.spawn("fx_napalm_flash", pTarget.current_tile, null, null, 0f, -1f, -1f); break;

                    case 3: for (int i = 0; i < Randy.randomInt(5, 21); i++){ World.world.drop_manager.spawnParabolicDrop(pTile, "lava", 0, 0.15f, 1.5f, 1, 5); } break;
                }
            }
            return true;
        }
        #endregion

        #region OnDeathActions
        public static bool GodOfFireDeath(BaseSimObject pself, WorldTile pTile)
        {
            if (Main.savedSettings.deathera)
            {
               World.world.era_manager.setCurrentAge(AssetManager.era_library.get(S.age_ice), true);
            }
            EffectsLibrary.spawn("fx_napalm_flash", pself.current_tile, null, null, 0f, -1f, -1f);
            for (int i = 0; i < Randy.randomInt(5, 10); i++)
                World.world.drop_manager.spawnParabolicDrop(pTile, "lava", 0, 2, 200, 20, 110, 1.5f);
            return true;
        }
        public static bool chaosGodsTrick(BaseSimObject pSelf, WorldTile pTile = null)
        {
            Actor pActor = (Actor)pSelf;
            if (Main.savedSettings.deathera)
                World.world.era_manager.setCurrentAge(AssetManager.era_library.get(S.age_chaos), true);
            pActor.removeTrait("God Of Chaos");
            return true;
        }


        public static bool sunGodsDeath(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (Main.savedSettings.deathera)
                World.world.era_manager.setCurrentAge(AssetManager.era_library.get(S.age_dark), true);
            return true;

        }

        public static bool starsGodsDeath(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (Main.savedSettings.deathera)
                World.world.era_manager.setCurrentAge(AssetManager.era_library.get(S.age_moon), true);
            return true;
        }

        public static bool darkGodsDeath(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (Main.savedSettings.deathera)
                World.world.era_manager.setCurrentAge(AssetManager.era_library.get(S.age_sun), true);
            return true;
        }
        #endregion

        #region AutoTraits&EraStatus
        public static bool AutoTrait(NanoObject pSelf, BaseAugmentationAsset asset) => AutoTrait((Actor)pSelf, asset.id);
        public static bool sungoderastatus(BaseSimObject pSelf, WorldTile pTile) => EraStatus(pSelf.a, "God Of light");
        public static bool godoffireerastatus(BaseSimObject pSelf, WorldTile pTile) => EraStatus(pSelf.a, "God Of Fire");
        public static bool knowledgegoderastatus(BaseSimObject pSelf, WorldTile pTile) => EraStatus(pSelf.a, "God Of Knowledge");
        private static bool GodOfLoveerastatus(BaseSimObject pTarget, WorldTile pTile) => EraStatus(pTarget.a, "God Of Love");
        public static bool nightgoderastatus(BaseSimObject pSelf, WorldTile pTile) => EraStatus(pSelf.a, "God Of the Night");
        public static bool stargoderastatus(BaseSimObject pSelf, WorldTile pTile) => EraStatus(pSelf.a, "God Of the Stars");
        public static bool earthgoderastatus(BaseSimObject pSelf, WorldTile pTile) => EraStatus(pSelf.a, "God Of the Earth");
        public static bool chaosgoderastatus(BaseSimObject pSelf, WorldTile pTile) => EraStatus(pSelf.a, "God Of Chaos");
        public static bool wargoderastatus(BaseSimObject pSelf, WorldTile pTile) => EraStatus(pSelf.a, "God Of War");
        public static bool lichgoderastatus(BaseSimObject pSelf, WorldTile pTile) => EraStatus(pSelf.a, "God Of The Lich");
        public static bool AutoTrait(Actor target, string trait)
        {
            target.setStatsDirty();
            target.event_full_stats = true;
            if (!Main.savedSettings.AutoTraits)
            {
                return true;
            }
            AddAutoTraits(target, trait);
            return true;
        }
        #endregion

        #region NonAttackPowers
        
        public static bool LesserEraStatus(BaseSimObject pSelf, WorldTile pTile)
        {
            if (!pSelf.isActor())
            {
                return false;
            }
            foreach(string trait in Getinheritedgodtraits(pSelf.a.data))
            {
                EraStatus(pSelf.a, trait);
            }
            return true;
        }
        public static bool EraStatus(Actor pSelf, string era)
        {
            if (World.world_era.id == TraitEras[era].Key)
            {
                if (!pSelf.hasStatus(TraitEras[era].Value))
                {
                    pSelf.addStatusEffect(TraitEras[era].Value);
                    pSelf.a.event_full_stats = true;
                }
            }
            else if (pSelf.hasStatus(TraitEras[era].Value))
            {
                pSelf.finishStatusEffect(TraitEras[era].Value);
            }
            return true;
        }
        private static bool SummonedOneEraStatus(BaseSimObject pSelf, WorldTile pTile)
        {
            if (pSelf.a != null)
            {
                Actor master = FindMaster(pSelf.a);
                if(master != null)
                {
                    if(EraStatus(master, pSelf.a))
                    {
                        pSelf.a.data.set("lifespanincreased", true);
                    }
                    else
                    {
                        pSelf.a.data.set("lifespanincreased", false);
                    }
                }
            }
            return true;
        }

        public static bool warGodSeeds(BaseSimObject pTarget, WorldTile pTile)
        {
            if (!WorldLawLibrary.world_law_diplomacy.getOption().boolVal)
            {
                return false;
            }
            if (!pTarget.kingdom.isCiv())
            {
                return false;
            }
            //WorldTile tile2 = Toolbox.getRandomTileWithinDistance(pTile, 40);
            // List<WorldTile> randTile = List.Of<WorldTile>(new WorldTile[] { tile1, tile2 });
            // WorldTile _tile = Toolbox.getRandomTileWithinDistance(randTile, pTile, 45, 120);
            if (pTarget.kingdom.king != null && Randy.randomChance(Chance("God Of War", "seedsOfWar%")))
            {
                MapBox.instance.plots.tryStartPlot(pTarget.kingdom.king, AssetManager.plots_library.get("new_war"));
            }
            if (pTarget.hasCity() && !pTarget.getCity().isCapitalCity() && Randy.randomChance(Chance("God Of War", "seedsOfWar%")))
            {
                MapBox.instance.plots.tryStartPlot(pTarget.a, AssetManager.plots_library.get("cause_rebellion"));
            }
            if (pTarget.kingdom.king != null && Randy.randomChance(Chance("God Of War", "seedsOfWar%")))
            {
                MapBox.instance.plots.tryStartPlot(pTarget.kingdom.king, AssetManager.plots_library.get("alliance_destroy"));
            }
            return true;
        }

        public static bool earthGodBuildWorld(BaseSimObject pSelf, WorldTile pTile)
        {
            if (pSelf.a != null)
            {
                if (Randy.randomChance(Chance("God Of the Earth", "buildWorld%")))
                {
                    ActionLibrary.tryToGrowTree(pSelf, pTile);
                }
                if (Randy.randomChance(Chance("God Of the Earth", "buildWorld%")))
                {
                    ActionLibrary.tryToCreatePlants(pSelf, pTile);
                }
                if (Randy.randomChance(Chance("God Of the Earth", "buildWorld%")))
                {
                    BuildingActions.tryGrowMineralRandom(pSelf.a.current_tile);
                }
                if (Randy.randomChance(Chance("God Of the Earth", "buildWorld%", 75)))
                {
                    CreateMountainWalls(pSelf.a);
                }

                return true;
            }
            return false;
        }
        public static void StealSouls(Actor a, Actor b)
        {
            ///not finushed
        }
        public static void CreateMountainWalls(Actor pActor)
        {
            static int GetPath(TileDirection direction, TileZone Zone, Kingdom kingdom, out int End)
            {
                bool UpCity = Zone.zone_up?.city?.kingdom == kingdom;
                bool DownCity = Zone.zone_down?.city?.kingdom == kingdom;
                bool LeftCity = Zone.zone_left?.city?.kingdom == kingdom;
                bool RightCity = Zone.zone_right?.city?.kingdom == kingdom;
                if (direction == TileDirection.Up)
                {
                    End = LeftCity ? 21 : 5;
                    return RightCity ? 45 : 61;
                }
                if (direction == TileDirection.Down)
                {
                    End = LeftCity ? 18 : 2;
                    return RightCity ? 42 : 58;
                }
                if (direction == TileDirection.Left)
                {
                    End = UpCity ? 21 : 23;
                    return DownCity ? 18 : 16;
                }
                if (direction == TileDirection.Right)
                {
                    End = UpCity ? 45 : 47;
                    return DownCity ? 42 : 40;
                }
                End = 0;
                return 0;
            }
            static TileDirection GetDirectionToCity(TileZone zone, City city)
            {
                if (city.zones.Contains(zone.zone_left))
                {
                    return TileDirection.Left;
                }
                if (city.zones.Contains(zone.zone_down))
                {
                    return TileDirection.Down;
                }
                if (city.zones.Contains(zone.zone_up))
                {
                    return TileDirection.Up;
                }
                if (city.zones.Contains(zone.zone_right))
                {
                    return TileDirection.Right;
                }
                return TileDirection.Null;
            }
            City city = pActor.city;
            if (city == null)
            {
                return;
            }
            bool AtWar = DiplomacyHelpers.wars.hasWars(pActor.kingdom);
            if (!pActor.current_tile.zone.isSameCityHere(city))
            {
                return;
            }
            int count = Randy.randomInt(3, 6);
            foreach (TileZone zone in city.neighbour_zones)
            {
                if(zone.city != null && zone.city.kingdom == pActor.kingdom)
                {
                    continue;
                }
                TileDirection direction = GetDirectionToCity(zone, city);
                if(direction == TileDirection.Null)
                {
                    continue;
                }
                int Start = GetPath(direction, zone, pActor.kingdom, out int End);
                bool hasmountains = zone.getTilesOfType(TileLibrary.mountains)?.Count >= 40;
                if (AtWar ? !hasmountains : hasmountains)
                {
                    (EffectsLibrary.spawn("fx_Build_Path", zone.centerTile) as TerraformPath)?.Init(zone.tiles[Start], zone.tiles[End], AtWar, 0.15f, 2, false);
                    count--;
                    if(count == 0)
                    {
                        return;
                    }
                }
            }
        }
        
        #endregion

        public static void addTraitToLocalizedLibrary(string id, string description)
        {
            Dictionary<string, string> localizedText = LocalizedTextManager.instance._localized_text;
            localizedText.Add("trait_" + id, id);
            localizedText.Add("trait_" + id + "_info", description);
        }
        private static void addTraitGroupToLocalizedLibrary(string id, string name)
        {
            Dictionary<string, string> localizedText = LocalizedTextManager.instance._localized_text;
            localizedText.Add(id, name);
        }
    }
}

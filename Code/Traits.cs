/* 
AUTHOR: MASON SCARBRO
VERSION: 1.0.0
*/
using System;
using UnityEngine;
using ReflectionUtility;
using System.Collections.Generic;
using static UnityEngine.GraphicsBuffer;
using Amazon.Runtime.Internal.Transform;
using UnityEngine.Tilemaps;
using System.Linq;

namespace GodsAndPantheons
{
    //Contains the traits and their abilities & Stats
    partial class Traits
    {

        #region TraitStats Dict

        public static Dictionary<string, Dictionary<string, float>> TraitStats = new Dictionary<string, Dictionary<string, float>>()
        {
            {"God Of Chaos", new Dictionary<string, float>(){
                {S.damage, 30f},
                {S.health, 500},
                {S.attack_speed, 15f},
                {S.armor, 10f },
                {S.critical_chance, 0.05f},
                {S.range, 8f},
                {S.scale, 0.08f},
                {S.max_children, 3},
                {S.fertility, 0.6f},
             }
            },
            {"God Of light", new Dictionary<string, float>(){
                {S.damage, 20f},
                {S.health, 500f},
                {S.attack_speed, 100f},
                {S.critical_chance, 0.05f},
                {S.range, 5f},
                {S.speed, 90f},
                {S.dodge, 80f},
                {S.accuracy, 10f},
                {S.max_children, 3},
                {S.fertility, 0.8f},
             }
            },
            {"God Of the Night", new Dictionary<string, float>(){
                {S.damage, 20f},
                {S.health, 550f},
                {S.attack_speed, 3f},
                {S.critical_chance, 0.25f},
                {S.range, 6f},
                {S.scale, 0.02f},
                {S.dodge, 60f},
                {S.max_children, 3},
                {S.fertility, 0.6f},
             }
            },
            {"God Of Knowledge", new Dictionary<string, float>(){
                {S.damage, 5f},
                {S.health, 300},
                {S.attack_speed, 1f},
                {S.critical_chance, 0.25f},
                {S.range, 15f},
                {S.scale, 0.01f},
                {S.intelligence, 35f},
                {S.accuracy, 10f},
                {S.max_children, 2},
                {S.fertility, 0.65f}
             }
            },
            {"God Of the Stars", new Dictionary<string, float>(){
                {S.damage, 25f},
                {S.health, 500f},
                {S.attack_speed, 1f},
                {S.critical_chance, 0.05f},
                {S.range, 15f},
                {S.scale, 0.02f},
                {S.intelligence, 3f},
                {S.max_children, 2},
                {S.fertility, 0.7f}
             }
            },
            {"God Of the Earth", new Dictionary<string, float>(){
                {S.damage, 60f},
                {S.health, 800f},
                {S.attack_speed, 1f},
                {S.armor, 50f},
                {S.scale, 0.1f},
                {S.range, 10f},
                {S.intelligence, 3f},
                {S.max_children, 3},
                {S.fertility, 0.8f},
             }
            },
            {"Lesser God", new Dictionary<string, float>(){
             }
            },
            {"God Of War", new Dictionary<string, float>(){
                {S.damage, 100f},
                {S.health, 700f},
                {S.attack_speed, 35f},
                {S.armor, 35f},
                {S.knockback_reduction, 0.05f},
                {S.scale, 0.03f},
                {S.range, 8f},
                {S.warfare, 40f},
                {S.max_children, 3},
                {S.fertility, 0.5f},
             }
            },
            {"God Of The Lich", new Dictionary<string, float>(){
                {S.damage, 100f},
                {S.health, 750f},
                {S.attack_speed, 35f},
                {S.armor, 25f},
                {S.knockback_reduction, 0.05f},
                {S.scale, 0.03f},
                {S.range, 8f},
                {S.warfare, 40f},
                {S.max_children, 2},
                {S.fertility, 0.6f},
             }
            },
            //NEW_GOD_TRAIT_STATS
            {"God Killer", new Dictionary<string, float>(){
                {S.damage, 10f},
                {S.health, 100f},
                {S.attack_speed, 15f},
                {S.knockback_reduction, 0.1f},
                {S.scale, 0.01f},
                {S.range, 4f},
                {S.warfare, 4f},
                {S.max_children, 2}
             }
            },
            {"God Hunter", new Dictionary<string, float>(){
             }
            },
            {"God Of Fire", new Dictionary<string, float>(){
                {S.damage, 25},
                {S.health, 700f},
                {S.attack_speed, 12f},
                {S.critical_chance, 0.5f},
                {S.intelligence, 30f},
                {S.armor, 30f},
                {S.scale, 0.075f},
                {S.range, 20f},
                {S.dodge, 35f},
                {S.accuracy, 15f},
                {S.speed, 30f},
                {S.max_children, 3},
                {S.fertility, 0.6f},
             }
            },
            {"Summoned One", new Dictionary<string, float>(){
                {S.damage, 10f},
                {S.health, 20f},
                {S.armor, 10f},
                {S.knockback_reduction, 0.5f},
                {S.max_age, -20000f},
             }
            },
            {"Demi God", new Dictionary<string, float>(){
             }
            },
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
                "burning_feet",
                "shiny",
                "fire_blood",
                "immortal",
                "regeneration"
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
                "tough"
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
             }
            },
            {"God Killer", new List<string>()
             {
                "blessed",
                "fire_proof",
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
            {"God Of the Earth", new KeyValuePair<string, string>(S.age_ash, "Earth Prevails") }
        };
        #endregion

        #region GodAbilities Dict
        public static Dictionary<string, List<AttackAction>> GodAbilities = new Dictionary<string, List<AttackAction>>()
        {
            {"God Of Fire", new List<AttackAction>(){
                new AttackAction(FireStorm),
                new AttackAction(Summoning),
                new AttackAction(Magic)
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
                new AttackAction(SeedsOfWar)
             }
            },
            {"God Of the Earth", new List<AttackAction>(){
                new AttackAction(SummonDruids),
                new AttackAction(MakeItRain),
                new AttackAction(EarthQuake)
             }
            },
            //NEW_GOD_ABILITY_DICT
            {"God Of The Lich", new List<AttackAction>(){
                new AttackAction(SummonHand),
                new AttackAction(summonskele),
                new AttackAction(SummonDead)
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
            { "God Of War", new List<string>(){ "God Of Knowledge" } },
            { "God Of Knowledge", new List<string>(){ "God Of Chaos", "God Of War" } }
        };
        #endregion
        public static void init()
        {

            /* Nuh uh*/

            ActorTrait chaosGod = new ActorTrait();
            chaosGod.id = "God Of Chaos";
            chaosGod.path_icon = "ui/icons/chaosGod";
            chaosGod.action_special_effect = new WorldAction(GodWeaponManager.godGiveWeapon);
            chaosGod.action_special_effect = (WorldAction)Delegate.Combine(chaosGod.action_special_effect, new WorldAction(chaosgodautotrait));
            chaosGod.action_attack_target = new AttackAction(ActionLibrary.addBurningEffectOnTarget);
            chaosGod.action_attack_target = new AttackAction(chaosGodAttack);
            //chaosGod.action_death = new WorldAction(ActionLibrary.turnIntoDemon);
            chaosGod.action_death = (WorldAction)Delegate.Combine(chaosGod.action_death, new WorldAction(chaosGodsTrick));
            chaosGod.action_special_effect = (WorldAction)Delegate.Combine(chaosGod.action_special_effect, new WorldAction(chaosgoderastatus));
            chaosGod.group_id = "GodTraits";
            AddTrait(chaosGod, "Tis's The God Of Chaos!");

            ActorTrait sunGod = new ActorTrait();
            sunGod.id = "God Of light";
            sunGod.path_icon = "ui/icons/lightGod";
            sunGod.action_special_effect = new WorldAction(GodWeaponManager.godGiveWeapon);
            sunGod.action_special_effect = (WorldAction)Delegate.Combine(sunGod.action_special_effect, new WorldAction(sungodatutotrait));
            sunGod.action_attack_target = new AttackAction(ActionLibrary.addBurningEffectOnTarget);
            sunGod.action_attack_target = new AttackAction(ActionLibrary.addSlowEffectOnTarget);
            sunGod.action_attack_target = new AttackAction(sunGodAttack);
            sunGod.action_death = (WorldAction)Delegate.Combine(sunGod.action_death, new WorldAction(sunGodsDeath));
            sunGod.action_special_effect = (WorldAction)Delegate.Combine(sunGod.action_special_effect, new WorldAction(sungoderastatus));
            sunGod.group_id = "GodTraits";
            AddTrait(sunGod, "The God Of light, controls the very light that shines and can pass through with great speed");

            ActorTrait darkGod = new ActorTrait();
            darkGod.id = "God Of the Night";
            darkGod.path_icon = "ui/icons/godDark";
            darkGod.action_special_effect = new WorldAction(GodWeaponManager.godGiveWeapon);
            darkGod.action_attack_target = new AttackAction(darkGodAttack);
            darkGod.action_death = (WorldAction)Delegate.Combine(darkGod.action_death, new WorldAction(darkGodsDeath));
            darkGod.action_special_effect = (WorldAction)Delegate.Combine(darkGod.action_special_effect, new WorldAction(nightgoderastatus));
            darkGod.action_special_effect = (WorldAction)Delegate.Combine(darkGod.action_special_effect, new WorldAction(nightgodautotrait));
            darkGod.group_id = "GodTraits";
            AddTrait(darkGod, "The God Of darkness, thievery and the shadows of which is his domain ");

            ActorTrait knowledgeGod = new ActorTrait();
            knowledgeGod.id = "God Of Knowledge";
            knowledgeGod.path_icon = "ui/icons/knowledgeGod";
            knowledgeGod.action_special_effect = new WorldAction(GodWeaponManager.godGiveWeapon);
            knowledgeGod.action_death = (WorldAction)Delegate.Combine(knowledgeGod.action_death, new WorldAction(genericGodsDeath));
            knowledgeGod.action_attack_target = new AttackAction(knowledgeGodAttack);
            knowledgeGod.action_special_effect = (WorldAction)Delegate.Combine(knowledgeGod.action_special_effect, new WorldAction(knowledgegoderastatus));
            knowledgeGod.action_special_effect = (WorldAction)Delegate.Combine(knowledgeGod.action_special_effect, new WorldAction(knowledgegodatuotrait));
            knowledgeGod.group_id = "GodTraits";
            AddTrait(knowledgeGod, "The God Of Knowledge, His mind excedes Time, he knows all");

            ActorTrait starsGod = new ActorTrait();
            starsGod.id = "God Of the Stars";
            starsGod.path_icon = "ui/icons/starsGod";
            starsGod.action_special_effect = new WorldAction(GodWeaponManager.godGiveWeapon);
            starsGod.action_death = (WorldAction)Delegate.Combine(starsGod.action_death, new WorldAction(starsGodsDeath));
            starsGod.action_attack_target += new AttackAction(starsGodAttack);
            starsGod.action_special_effect = (WorldAction)Delegate.Combine(starsGod.action_special_effect, new WorldAction(stargoderastatus));
            starsGod.group_id = "GodTraits";
            starsGod.action_special_effect = (WorldAction)Delegate.Combine(starsGod.action_special_effect, new WorldAction(stargodautotrait));
            AddTrait(starsGod, "Now Cometh the Age of stars, A Thousand Year Voyage under the wisdom of the moon");

            ActorTrait earthGod = new ActorTrait();
            earthGod.id = "God Of the Earth";
            earthGod.path_icon = "ui/icons/earthGod";
            earthGod.group_id = "GodTraits";
            earthGod.action_attack_target = new AttackAction(EarthGodAttack);
            earthGod.action_special_effect = new WorldAction(earthGodBuildWorld);
            earthGod.action_special_effect += new WorldAction(GodWeaponManager.godGiveWeapon);
            earthGod.action_special_effect = (WorldAction)Delegate.Combine(earthGod.action_special_effect, new WorldAction(earthgodautotrait));
            earthGod.action_special_effect = (WorldAction)Delegate.Combine(earthGod.action_special_effect, new WorldAction(earthgoderastatus));
            AddTrait(earthGod, "God of the Natural Enviornment, The titan of creation");

            ActorTrait subGod = new ActorTrait();
            subGod.id = "Lesser God";
            subGod.path_icon = "ui/icons/subGod";
            subGod.group_id = TraitGroup.special;
            subGod.can_be_given = false;
            subGod.action_attack_target = new AttackAction(LesserAttack);
            subGod.action_special_effect = new WorldAction(LesserEraStatus);
            AddTrait(subGod, "like the demigod, but can also inherit abilities! all children in a gods clan are lessergods");

            ActorTrait warGod = new ActorTrait();
            warGod.id = "God Of War";
            warGod.path_icon = "ui/icons/warGod";
            warGod.action_attack_target = new AttackAction(warGodAttack);
            warGod.action_special_effect = new WorldAction(GodWeaponManager.godGiveWeapon);
            warGod.action_special_effect = (WorldAction)Delegate.Combine(warGod.action_special_effect, new WorldAction(wargodautotrait));
            warGod.action_special_effect = (WorldAction)Delegate.Combine(warGod.action_special_effect, new WorldAction(warGodSeeds));
            warGod.action_special_effect = (WorldAction)Delegate.Combine(warGod.action_special_effect, new WorldAction(wargoderastatus));
            warGod.group_id = "GodTraits";
            AddTrait(warGod, "God of Conflict, Bravery, Ambition, Many spheres of domain lie with him");

            ActorTrait lichGod = new ActorTrait();
            lichGod.id = "God Of The Lich";
            lichGod.path_icon = "ui/icons/lichGod";
            lichGod.action_death = (WorldAction)Delegate.Combine(lichGod.action_death, new WorldAction(genericGodsDeath));
            lichGod.action_attack_target = new AttackAction(lichGodAttack);
            lichGod.action_special_effect = new WorldAction(GodWeaponManager.godGiveWeapon);
            lichGod.action_special_effect = (WorldAction)Delegate.Combine(lichGod.action_special_effect, new WorldAction(lichgodautotrait));
            lichGod.action_special_effect = (WorldAction)Delegate.Combine(lichGod.action_special_effect, new WorldAction(lichgoderastatus));
            lichGod.group_id = "GodTraits";
            AddTrait(lichGod, "God of Dead Souls, Corruption, and Rot, Many spheres of domain lie with him");

            //NEW_GOD_INIT

            ActorTrait godKiller = new ActorTrait();
            godKiller.id = "God Killer";
            godKiller.path_icon = "ui/icons/godKiller";
            godKiller.action_special_effect = (WorldAction)Delegate.Combine(godKiller.action_special_effect, new WorldAction(godkillerautotrait));
            godKiller.group_id = "GodTraits";
            AddTrait(godKiller, "To Kill a God is nearly to become one");

            ActorTrait godHunter = new ActorTrait();
            godHunter.id = "God Hunter";
            godHunter.path_icon = "ui/icons/godKiller";
            godHunter.action_special_effect = new WorldAction(SuperRegeneration);
            godHunter.action_special_effect = (WorldAction)Delegate.Combine(godHunter.action_special_effect, new WorldAction(GodWeaponManager.godGiveWeapon));
            godHunter.action_special_effect = (WorldAction)Delegate.Combine(godHunter.action_special_effect, new WorldAction(godkillerautotrait));
            godHunter.action_special_effect = (WorldAction)Delegate.Combine(godHunter.action_special_effect, new WorldAction(InvisibleCooldown));
            godHunter.action_attack_target = new AttackAction(GodHunterAttack);
            godHunter.group_id = "GodTraits";
            godHunter.can_be_given = true;
            AddTrait(godHunter, "He will stop at NOTHING to kill a god");

            //my traits
            ActorTrait godoffire = new ActorTrait();
            godoffire.id = "God Of Fire";
            godoffire.path_icon = "ui/icons/GodOfFire";
            godoffire.action_death = new WorldAction(ActionLibrary.deathBomb);
            godoffire.action_special_effect = (WorldAction)Delegate.Combine(new WorldAction(GodWeaponManager.godGiveWeapon), new WorldAction(godoffireautotrait));
            godoffire.action_special_effect = (WorldAction)Delegate.Combine(godoffire.action_special_effect, new WorldAction(godoffireerastatus));
            godoffire.action_special_effect = (WorldAction)Delegate.Combine(godoffire.action_special_effect, new WorldAction(MorphIntoDragon));
            godoffire.action_death = new WorldAction(GodOfFireDeath);
            godoffire.action_attack_target += new AttackAction(GodOfFireAttack);
            godoffire.group_id = "GodTraits";
            AddTrait(godoffire, "The Most Powerfull of the gods, the god of fire, many spheres of domain lie with him");

            ActorTrait SummonedOne = new ActorTrait();
            SummonedOne.id = "Summoned One";
            SummonedOne.path_icon = "ui/icons/iconBlessing";
            SummonedOne.action_special_effect += new WorldAction(SummonedBeing);
            SummonedOne.group_id = TraitGroup.special;
            SummonedOne.can_be_given = false;
            SummonedOne.action_special_effect = (WorldAction)Delegate.Combine(SummonedOne.action_special_effect, new WorldAction(SummonedOneEraStatus));
            AddTrait(SummonedOne, "A creature summoned by God himself in order to aid them in battle");

            ActorTrait DemiGod = new ActorTrait();
            DemiGod.id = "Demi God";
            DemiGod.path_icon = "ui/icons/IconDemi";
            DemiGod.group_id = TraitGroup.special;
            DemiGod.can_be_given = false;
            AddTrait(DemiGod, "The Demi God, offspring of Gods and Mortals, the stat's of this trait are determined by the stats of his parent's");
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
                a.killHimself(false, AttackType.Age, false, true, true);
            }
            return true;
        }
        //if god is too far away the god hunter will teleport to them
        public static bool InvisibleCooldown(BaseSimObject pTarget, WorldTile pTile)
        {
            if (pTarget.isActor() && Main.savedSettings.HunterAssasins && pTarget.a.asset.id == "GodHunter")
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
                    pSelf.a.data.get("Demi" + nameof(ability), out bool inherited);
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
            if(!Toolbox.randomChance(GetEnhancedChance("God Of Chaos", "WorldOfChaos%")))
            {
                return false;
            }
            RandomForce.CreateRandomForce(World.world, pTile, 32, 3);
            World.world.startShake(1, 0.02f, 1.5f);
            return true;
        }
        public static bool ChaosBall(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Toolbox.randomChance(GetEnhancedChance("God Of Chaos", "FireBall%")))
            {
                Vector2Int pos = pTile.pos; // Position of the Ptile as a Vector 2
                float pDist = Vector2.Distance(pTarget.currentPosition, pos); // the distance between the target and the pTile
                Vector3 newPoint = Toolbox.getNewPoint(pSelf.currentPosition.x, pSelf.currentPosition.y, (float)pos.x, (float)pos.y, pDist, true); // the Point of the projectile launcher 
                Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pTarget.stats[S.size], true);
                EffectsLibrary.spawnProjectile("fireBallX", newPoint, newPoint2, 0.0f);
            }
            return true;
        }

        public static bool UnleachChaos(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Toolbox.randomChance(GetEnhancedChance("God Of Chaos", "UnleashChaos%")))
            {
                World.world.getObjectsInChunks(pTile, 8, MapObjectType.Actor);
                foreach(Actor a in World.world.temp_map_objects)
                {
                    if (a != pSelf.a)
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
            if (Toolbox.randomChance(GetEnhancedChance("God Of Chaos", "ChaosBoulder%")))
            {
                pb.spawnBoulder(pTarget.currentTile, null);
            }
            return true;
        }

        public static bool chaosGodAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile) => GodAttack(pSelf, pTarget, pTile, "God Of Chaos");


        #endregion

        #region KnowledgeGodsAttack
        public static bool CreateElements(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Toolbox.randomChance(GetEnhancedChance("God Of Knowledge", "UnleashFireAndAcid%") / 100))
            {
                // randomly spawns a flash of fire or acid on the tile 
                MapBox.instance.dropManager.spawn(pTile, "fire", 5f, -1f);
                MapBox.instance.dropManager.spawn(pTile, "acid", 5f, -1f);
                MapBox.instance.dropManager.spawn(pTile, "fire", 5f, -1f); // Drops fire from distance 5 with scale of one at current tile
            }
            if (Toolbox.randomChance(GetEnhancedChance("God Of Knowledge", "Freeze%")))
            {
                ActionLibrary.addFrozenEffectOnTarget(null, pTarget, null); // freezezz the target
            }
            if (Toolbox.randomChance(GetEnhancedChance("God Of Knowledge", "SummonLightning%")))
            {
                ActionLibrary.castLightning(null, pTarget, null); // Casts Lightning on the target
            }
            return true;
        }

        public static bool trydefendself(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Toolbox.randomChance(GetEnhancedChance("God Of Knowledge", "CreateShield%")))
            {
                pSelf.addStatusEffect("shield", 10);
            }
            if (!pTarget.isActor())
            {
                return false;
            }
            if (Toolbox.randomChance(GetEnhancedChance("God Of Knowledge", "TeleprtTarget%")))
            {
                ActionLibrary.teleportRandom(null, pTarget, null); // flee
            }
            if (Toolbox.randomChance(GetEnhancedChance("God Of Knowledge", "CastCurses%")))
            {
                ActionLibrary.castCurses(null, pTarget, null); // casts curses
                ((Actor)pSelf).removeTrait("cursed");
            }
            return true;
        }
        public static bool SummonMeteor(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Toolbox.randomChance(GetEnhancedChance("God Of Knowledge", "SummonMeteor%")))
            {
                EffectsLibrary.spawn("fx_meteorite", pTarget.currentTile, "meteorite_disaster", null, 0f, -1f, -1f);    //spawn 1 meteorite
                pSelf.a.addStatusEffect("invincible", 1f);
            }
            return true;
        }

        public static bool PagesOfKnowledge(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Toolbox.randomChance(GetEnhancedChance("God Of Knowledge", "PagesOfKnowledge%")))
            {
                Vector2Int pos = pTile.pos; // Position of the Ptile as a Vector 2
                float pDist = Vector2.Distance(pTarget.currentPosition, pos); // the distance between the target and the pTile
                Vector3 newPoint = Toolbox.getNewPoint(pSelf.currentPosition.x, pSelf.currentPosition.y, (float)pos.x, (float)pos.y, pDist, true); // the Point of the projectile launcher 
                Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pTarget.stats[S.size], true);
                EffectsLibrary.spawnProjectile("PagesOfKnowledge", newPoint, newPoint2, 0.0f);
            }
            return true;
        }
        public static bool knowledgeGodAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile) => GodAttack(pSelf, pTarget, pTile, "God Of Knowledge");
        #endregion

        #region DarkGodsAttack

        public static void CloudOfDark(Storm s)
        {
            if (Toolbox.randomChance(0.2f))
            {
                pb.spawnLightning(Toolbox.getRandomTileWithinDistance(s.pTile, 60), null);
            }
            if (Toolbox.randomChance(0.8f))
            {
                ActionLibrary.castCurses(null, null, Toolbox.getRandomTileWithinDistance(s.pTile, 60));
            }
            if (Toolbox.randomChance(0.5f))
            {
                pb.spawnForce(Toolbox.getRandomTileWithinDistance(s.pTile, 60), null);
            }
        }
        public static bool CloudOfDarkness(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Toolbox.randomChance(GetEnhancedChance("God Of the Night", "cloudOfDarkness%")))
            {
                CreateStorm(pTile, 30f, 0.4f, CloudOfDark, new Color(1, 1, 1, 0.75f), 4f);
            }
            if (Toolbox.randomChance(GetEnhancedChance("God Of the Night", "blackHole%")))
            {
                Vector2Int pos = pTile.pos; // Position of the Ptile as a Vector 2
                float pDist = Vector2.Distance(pTarget.currentPosition, pos); // the distance between the target and the pTile
                Vector3 newPoint = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pDist, true); // the Point of the projectile launcher 
                Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pTarget.stats[S.size], true);
                EffectsLibrary.spawnProjectile("BlackHoleProjectile1", newPoint, newPoint2, 0.0f).byWho = pSelf;
            }
            return true;
        }
        public static bool darkdaggers(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Toolbox.randomChance(GetEnhancedChance("God Of the Night", "darkDaggers%")))
            {
                Vector2Int pos = pTile.pos; // Position of the Ptile as a Vector 2
                float pDist = Vector2.Distance(pTarget.currentPosition, pos); // the distance between the target and the pTile
                Vector3 newPoint = Toolbox.getNewPoint(pSelf.currentPosition.x, pSelf.currentPosition.y, (float)pos.x, (float)pos.y, pDist, true); // the Point of the projectile launcher 
                Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pTarget.stats[S.size], true);
                EffectsLibrary.spawnProjectile("DarkDaggersProjectiles", newPoint, newPoint2, 0.0f);
            }
            return true;
        }
        public static bool smokeflash(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Toolbox.randomChance(GetEnhancedChance("God Of the Night", "smokeFlash%")))
            {
                EffectsLibrary.spawnAtTile("fx_smokeFlash_dej", pTile, 0.1f);
                MapAction.damageWorld(pTarget.currentTile, 5, AssetManager.terraform.get("lightning_power"), null);
                MapAction.damageWorld(pTarget.currentTile, 8, AssetManager.terraform.get("smokeFlash"), null);
                World.world.applyForce(pTarget.currentTile, 2, 0.4f, false, true, 20, null, pTarget, null);

            }
            return true;
        }
        public static bool summondarkones(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Toolbox.randomChance(GetEnhancedChance("God Of the Night", "summonDarkOne%")))
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
            if (Toolbox.randomChance(GetEnhancedChance("God Of light", "flashOfLight%")))
            {
                pb.divineLightFX(pTarget.currentTile, null);
                (EffectsLibrary.spawn("fx_napalm_flash", pTarget.currentTile, null, null, 0f, -1f, -1f) as NapalmFlash).bombSpawned = true;
                CreateBlindess(pTile, 10, 5f, pSelf);
            }
            if (Toolbox.randomChance(0 / 100))
            {
                //EffectsLibrary.spawnAtTile("fx_multiFlash_dej", pTile, 0.01f);
                //not finushed?
                //TO BE USED AS IMACT ACTION FOR LIGHT PROJECILES LATER
                int count = 0;
                Vector2Int pos = pTile.pos; // Position of the Ptile as a Vector 2
                float pDist = Vector2.Distance(pTarget.currentPosition, pos);
                Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pTarget.stats[S.size], true);

                EffectsLibrary.spawn("fx_napalm_flash", pTarget.currentTile.neighbours[2].neighbours[2].neighbours[1].neighbours[1].neighbours[2], null, null, 0f, -1f, -1f);
                count++;
                //???????????????????????????????????????????????????????????????????????????
                if (count == 1)
                {
                    EffectsLibrary.spawn("fx_napalm_flash", pTarget.currentTile.neighbours[1].neighbours[0].neighbours[0].neighbours[1].neighbours[0].neighbours[0].neighbours[1].neighbours[1].neighbours[0].neighbours[0].neighbours[0], null, null, 0f, -0.5f, -0.2f);
                    count++;
                }
                if (count == 2)
                {
                    EffectsLibrary.spawn("fx_napalm_flash", pTarget.currentTile.neighbours[1].neighbours[3].neighbours[2].neighbours[1].neighbours[2].neighbours[3].neighbours[1].neighbours[1].neighbours[0].neighbours[0], null, null, 0f, 1f, -0.2f);
                    count++;
                }
                if (count == 3)
                {
                    EffectsLibrary.spawn("fx_napalm_flash", pTarget.currentTile.neighbours[1].neighbours[3].neighbours[2].neighbours[1].neighbours[2].neighbours[3].neighbours[1].neighbours[1].neighbours[0].neighbours[0], null, null, 0f, -0.5f, -0.2f);
                    EffectsLibrary.spawn("fx_napalm_flash", pTarget.currentTile.neighbours[1].neighbours[3].neighbours[0].neighbours[1].neighbours[0].neighbours[0].neighbours[0].neighbours[1].neighbours[0].neighbours[0], null, null, 0f, -0.5f, -0.2f);

                }
                pSelf.a.addStatusEffect("invincible", 1f);
            }
            return true;
        }
        public static void CreateBlindess(WorldTile pTile, int radius, float length, BaseSimObject ByWho)
        {
            World.world.getObjectsInChunks(pTile, radius, MapObjectType.Actor);
            foreach (Actor victim in World.world.temp_map_objects)
            {
                if(victim == ByWho) continue;
                victim.addStatusEffect("Blinded", length);
            }
        }
        public static bool BeamOfLight(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Toolbox.randomChance(GetEnhancedChance("God Of light", "beamOfLight%")))
            {
                pb.divineLightFX(pTarget.currentTile, null);
                pTarget.addStatusEffect("burning", 1f);
            }
            return true;
        }
        public static bool LightBallz(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Toolbox.randomChance(GetEnhancedChance("God Of light", "lightBallz%")))
            {
                Vector2Int pos = pTile.pos; // Position of the Ptile as a Vector 2
                float pDist = Vector2.Distance(pTarget.currentPosition, pos); // the distance between the target and the pTile
                Vector3 newPoint = Toolbox.getNewPoint(pSelf.currentPosition.x, pSelf.currentPosition.y, (float)pos.x, (float)pos.y, pDist, true); // the Point of the projectile launcher 
                Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pTarget.stats[S.size], true);
                EffectsLibrary.spawnProjectile("lightBallzProjectiles", newPoint, newPoint2, 0.0f);
            }
            return true;
        }
        public static bool SpeedOfLight(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Toolbox.randomChance(GetEnhancedChance("God Of light", "speedOfLight%")))
            {
                pb.divineLightFX(pTarget.currentTile, null);
                EffectsLibrary.spawn("fx_thunder_flash", pSelf.currentTile, null, null, 0f, -1f, -1f);
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
            if (Toolbox.randomChance(GetEnhancedChance("God Of the Stars", "cometAzure%")))
            {
                EffectsLibrary.spawnAtTile("fx_cometAzureDown_dej", pTarget.currentTile, 0.1f);
                MapAction.applyTileDamage(pTarget.currentTile, 8, AssetManager.terraform.get("cometAzureDownDamage"));
                MapAction.damageWorld(pTarget.currentTile.neighbours[2], 8, AssetManager.terraform.get("cometAzureDownDamage"), null);
                MapAction.damageWorld(pTarget.currentTile.neighbours[1], 8, AssetManager.terraform.get("cometAzureDownDamage"), null);
                World.world.applyForce(pTarget.currentTile.neighbours[0], 4, 0.4f, false, true, 200, null, pSelf, null);
                pSelf.a.addStatusEffect("invincible", 1f);
            }
            return true;
        }
        public static bool CometShower(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Toolbox.randomChance(GetEnhancedChance("God Of the Stars", "cometShower%")))
            {
                EffectsLibrary.spawnAtTile("fx_cometShower_dej", pTarget.currentTile, 0.09f);
                MapAction.applyTileDamage(pTarget.currentTile, 2f, AssetManager.terraform.get("cometRain"));
                pSelf.addStatusEffect("invincible", 1f);
                // The Rain Damage effect 
                MapAction.applyTileDamage(pTarget.currentTile.neighbours[2].neighbours[2].neighbours[1].neighbours[1], 2f, AssetManager.terraform.get("cometRain"));
                MapAction.applyTileDamage(pTarget.currentTile.neighbours[3].neighbours[3].neighbours[2].neighbours[2], 1f, AssetManager.terraform.get("cometRain"));
                MapAction.applyTileDamage(pTarget.currentTile.neighbours[0].neighbours[0].neighbours[1].neighbours[2], 1f, AssetManager.terraform.get("cometRain"));
                MapAction.applyTileDamage(pTarget.currentTile.neighbours[1].neighbours[2].neighbours[3].neighbours[0], 1f, AssetManager.terraform.get("cometRain"));
                MapAction.applyTileDamage(pTarget.currentTile.neighbours[1].neighbours[1].neighbours[1].neighbours[1], 1f, AssetManager.terraform.get("cometRain"));
                MapAction.applyTileDamage(pTarget.currentTile.neighbours[2].neighbours[2].neighbours[2].neighbours[2], 1f, AssetManager.terraform.get("cometRain"));
                MapAction.applyTileDamage(pTarget.currentTile.neighbours[3].neighbours[3].neighbours[3].neighbours[3], 1f, AssetManager.terraform.get("cometRain"));
                MapAction.applyTileDamage(pTarget.currentTile.neighbours[0].neighbours[1], 2f, AssetManager.terraform.get("cometRain"));
                MapAction.applyTileDamage(pTarget.currentTile.neighbours[2].neighbours[3], 2f, AssetManager.terraform.get("cometRain"));
                MapAction.applyTileDamage(pTarget.currentTile.neighbours[0].neighbours[0].neighbours[0], 2f, AssetManager.terraform.get("cometRain"));
                World.world.applyForce(pTarget.currentTile.neighbours[3].neighbours[3].neighbours[2].neighbours[2], 2, 0.4f, false, true, 20, null, pSelf, null);
                World.world.applyForce(pTarget.currentTile.neighbours[3].neighbours[3].neighbours[3].neighbours[3], 2, 0.4f, false, true, 20, null, pSelf, null);
            }
            return true;
        }

        public static bool SummonWolf(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Toolbox.randomChance(GetEnhancedChance("God Of the Stars", "summonWolf%")))
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
            if (Toolbox.randomChance(GetEnhancedChance("God Of War", "warGodsCry%")))
            {
                EffectsLibrary.spawnExplosionWave(pSelf.currentTile.posV3, 1f, 1f);
                pSelf.a.addStatusEffect("WarGodsCry", 30f);
                World.world.startShake(0.3f, 0.01f, 2f, true, true);
                pSelf.a.addStatusEffect("invincible", 1f);
                MapAction.damageWorld(pSelf.currentTile, 2, AssetManager.terraform.get("crab_step"), null);
                pSelf.a.addStatusEffect("invincible", 1f);
                World.world.applyForce(pSelf.currentTile, 4, 0.4f, false, true, 20, null, pTarget, null);

            }
            return true;
        }
        public static bool SeedsOfWar(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Toolbox.randomChance(GetEnhancedChance("God Of War", "seedsOfWar%")))
            {
                WorldTile _tile = Toolbox.getRandomTileWithinDistance(pTarget.currentTile, 5);

                //Gods arent effected by this and dispel his attack
                if (!pTarget.isActor())
                {
                    return true;
                }
                bool hasmadness = pSelf.a.hasTrait("madness");
                if (!IsGod(pTarget.a))
                {
                    MapBox.instance.dropManager.spawn(pTarget.a.currentTile, SD.madness, 5f, -1f);
                    MapBox.instance.dropManager.spawn(_tile, SD.madness, 5f, -1f);
                    MapBox.instance.dropManager.spawn(_tile.neighbours[0], SD.madness, 5f, -1f);
                }
                if (!hasmadness) pSelf.a.removeTrait("madness");
            }
            return true;
        }
        public static bool warGodAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile) => GodAttack(pSelf, pTarget, pTile, "God Of War");
        #endregion

        #region GodOfEarthsAttack
        public static bool EarthQuake(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Toolbox.randomChance(GetEnhancedChance("God Of the Earth", "earthquake%")))
            {
                pb.spawnEarthquake(pTarget.currentTile, null);
            }
            return true;
        }

        public static bool MakeItRain(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Toolbox.randomChance(GetEnhancedChance("God Of the Earth", "makeItRain%")))
            {
                pb.spawnCloudRain(pTarget.currentTile, null);
                pb.spawnCloudSnow(pTarget.currentTile, null);
            }
            return true;
        }
        public static readonly List<string> earthgodminionautotraits = new List<string>() { "fire_proof", "freeze_proof", "regeneration"};
        public static bool SummonDruids(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Toolbox.randomChance(GetEnhancedChance("God Of the Earth", "SummonDruid%")))
            {
                Summon(SA.druid, 2, pSelf, pTile, 61, earthgodminionautotraits);
            }
            return true;
        }
        public static bool EarthGodAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile) => GodAttack(pSelf, pTarget, pTile, "God Of the Earth");
        #endregion

        #region LichGodsAttack
        public static bool lichGodAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile) => GodAttack(pSelf, pTarget, pTile, "God Of The Lich");
        public static bool summonskele(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Toolbox.randomChance(GetEnhancedChance("God Of The Lich", "summonSkele%")))
            {
                //Lich God Summons Skeletons
                Summon(SA.skeleton, 8, pSelf, pTile);
            }
            return true;
        }

        public static bool SummonDead(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Toolbox.randomChance(GetEnhancedChance("God Of The Lich", "summonDead%")))
            {
                //Lich God Summons Zombie
                Summon(SA.walker, 2, pSelf, pTile);
            }
            return true;
        }

        public static bool SummonHand(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Toolbox.randomChance(GetEnhancedChance("God Of The Lich", "rigorMortisHand%")))
            {
                //Lich God Summons RigorMortis Hand
                EffectsLibrary.spawnAtTile("fx_handgrab_dej", pTarget.currentTile, 0.1f);
                pTarget.addStatusEffect("slowness");
                pTarget.addStatusEffect("poisoned");
            }
            return true;
        }

        #endregion

        //NEW_GOD_ATTACK_FUNC

        #region GodOfFireStuff
        public static bool GodOfFireAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile) => GodAttack(pSelf, pTarget, pTile, "God Of Fire");

        public static void FireStorm(Storm s)
        {
            if (Vector3.Distance(s.transform.position, s.TileToGo.posV) < 10)
            {
                s.TileToGo = Toolbox.getRandomTileWithinDistance(s.pTile, 120);
            }
            if (Toolbox.randomChance(0.1f))
            {
                s.UsingLaser = !s.UsingLaser;
            }
            if (Toolbox.randomChance(0.6f))
            {
                LavaHelper.addLava(Toolbox.getRandomTileWithinDistance(s.pTile, 30), "lava3");
            }
            if (Toolbox.randomChance(0.3f))
            {
                EffectsLibrary.spawn("fx_napalm_flash", s.pTile, null, null, 0f, -1f, -1f);
                EffectsLibrary.spawnAtTileRandomScale("fx_explosion_tiny", s.pTile, 0.15f, 0.3f);
            }
            if (Toolbox.randomChance(0.8f))
            {
                World.world.applyForce(s.pTile, 40, 1.5f, false, true, 5);
            }
            if (Toolbox.randomChance(0.4f))
            {
                for(int i = 0; i < Toolbox.randomInt(5, 10); i++)
                    World.world.dropManager.spawnParabolicDrop(s.pTile, "lava", 0, 2, 200, 20, 110, 1.5f);
            }
        }
        public static bool FireStorm(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Toolbox.randomChance(GetEnhancedChance("God Of Fire", "FireStorm%")))
            {
                switch (Toolbox.randomInt(1, 4))
                {
                    case 1: pb.spawnCloudAsh(pTile, null); break;
                    //FIRE TORNADOS
                    case 2:
                        {
                            for (int i = 0; i < Toolbox.randomInt(3, 6); i++)
                            {
                                Tornado component = World.world.units.createNewUnit(SA.tornado, pTile, 0f).GetComponent<Tornado>();
                                component.forceScaleTo(Tornado.TORNADO_SCALE_DEFAULT / 6f);
                                component.resizeTornado(Tornado.TORNADO_SCALE_DEFAULT / 3f);
                                component.actor.addStatusEffect("FireStorm");
                                Effects.FireStormEefect(component.actor, pTile);
                            }
                            break;
                        }
                    //FIRESTORM
                    case 3:
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
            if (Toolbox.randomChance(GetEnhancedChance("God Of Fire", "Summoning%")))
            {
                switch (Toolbox.randomInt(1, 4))
                {
                    case 1: Summon(SA.demon, 2, pSelf, pTile); break;
                    case 2: Summon(SA.evilMage, 1, pSelf, pTile); break;
                    case 3: Summon(SA.fire_skull, 3, pSelf, pTile); break;
                }
            }
            return true;
        }

        public static bool MorphIntoDragon(BaseSimObject pSelf, WorldTile pTile)
        {
            bool IsDragon = pSelf.a.asset.id == SA.dragon;
            if (Toolbox.randomChance(GetEnhancedChance("God Of Fire", "MorphIntoDragon%")) || IsDragon)
            {
                ListPool<BaseSimObject> enemies = EnemiesFinder.findEnemiesFrom(pTile, pSelf.kingdom, 2).list;
                if (!IsDragon)
                {
                    if (enemies?.Count > 10)
                    {
                        Morph(pSelf.a, SA.dragon);
                    }
                }
                else {
                    bool AnyEnemies = enemies != null;
                    if (AnyEnemies)
                    {
                        foreach (BaseSimObject enemy in enemies)
                        {
                            ShootCustomProjectile(pSelf.a, enemy, "red_orb", 5);
                        }
                    }
                    if (!AnyEnemies || pSelf.a.data.health < pSelf.getMaxHealth() * 0.1f)
                    {
                        pSelf.a.data.get("oldself", out string oldself, SA.dragon);
                        Morph(pSelf.a, oldself);
                    }
                }
            }
            return true;
        }
        public static Vector2 getAttackPosition(BaseSimObject target)
        {
            float num = target.stats[S.size];
            Vector2 vector = new Vector2(target.currentPosition.x, target.currentPosition.y);
            if (target.isActor() && target.a.is_moving && target.isFlying())
            {
                vector = Vector2.MoveTowards(vector, target.a.nextStepPosition, num * 3f);
            }
            return vector;
        }
        public static void ShootCustomProjectile(Actor Self, BaseSimObject Target, string projectile, int amount)
        {
            Vector2 attackPosition = getAttackPosition(Target);
            attackPosition.y += 0.1f;
            float TargetSize = Target.stats[S.size];
            float Size = Self.stats[S.size];
            Vector2 vector = new Vector2(attackPosition.x, attackPosition.y);
            vector.x += Toolbox.randomFloat(-(TargetSize + 1f), TargetSize + 1f);
            vector.y += Toolbox.randomFloat(-TargetSize, TargetSize);
            Vector3 newPoint = Toolbox.getNewPoint(Self.currentPosition.x, Self.currentPosition.y, vector.x, vector.y, Size, true);
            newPoint.y += 0.5f;
            float num5 = 0f;
            if (Target.isInAir())
            {
                num5 = Target.getZ();
            }
            for (int i = 0; i < amount; i++)
            {
                Projectile Projectile = EffectsLibrary.spawnProjectile(projectile, newPoint, vector, num5);
                if (Projectile != null)
                {
                    Projectile.byWho = Self;
                    Projectile.setStats(Self.stats);
                    Projectile.targetObject = Target;
                }
            }
        }
        public static bool Magic(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Toolbox.randomChance(GetEnhancedChance("God Of Fire", "Magic%")))
            {
                switch (Toolbox.randomInt(1, 4))
                {
                    case 1: EffectsLibrary.spawn("fx_explosion_middle", pTarget.currentTile, null, null, 0f, -1f, -1f); break;

                    case 2:
                        EffectsLibrary.spawn("fx_napalm_flash", pTarget.currentTile, null, null, 0f, -1f, -1f); break;

                    case 3: World.world.dropManager.spawnParabolicDrop(pTile, "lava", 0, 0.15f, 113, 1, 80, -1); break;
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
                if (Main.savedSettings.deathera)
                    World.world.eraManager.setEra(S.age_hope, true);
            }
            return true;
        }
        public static bool chaosGodsTrick(BaseSimObject pSelf, WorldTile pTile = null)
        {
            Actor pActor = (Actor)pSelf;
            if (Main.savedSettings.deathera)
                World.world.eraManager.setEra(S.age_chaos, true);
            pActor.removeTrait("God Of Chaos");
            return true;
        }

        public static bool sunGodsDeath(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (Main.savedSettings.deathera)
                World.world.eraManager.setEra(S.age_dark, true);
            return true;

        }

        public static bool starsGodsDeath(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (Main.savedSettings.deathera)
                World.world.eraManager.setEra(S.age_moon, true);
            return true;

        }

        public static bool darkGodsDeath(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (Main.savedSettings.deathera)
                World.world.eraManager.setEra(S.age_sun, true);
            return true;
        }

        public static bool genericGodsDeath(BaseSimObject pTarget, WorldTile pTile = null)
        {
            return true;

        }
        #endregion

        #region AutoTraits
        public static bool sungodatutotrait(BaseSimObject pSelf, WorldTile pTile) => AutoTrait(pSelf.a, "God Of light");
        public static bool godoffireautotrait(BaseSimObject pSelf, WorldTile pTile) => AutoTrait(pSelf.a, "God Of Fire");
        public static bool knowledgegodatuotrait(BaseSimObject pSelf, WorldTile pTile) => AutoTrait(pSelf.a, "God Of Knowledge");
        public static bool nightgodautotrait(BaseSimObject pSelf, WorldTile pTile) => AutoTrait(pSelf.a, "God Of the Night");
        public static bool stargodautotrait(BaseSimObject pSelf, WorldTile pTile) => AutoTrait(pSelf.a, "God Of the Stars");
        public static bool earthgodautotrait(BaseSimObject pSelf, WorldTile pTile) => AutoTrait(pSelf.a, "God Of the Earth");
        public static bool chaosgodautotrait(BaseSimObject pSelf, WorldTile pTile) => AutoTrait(pSelf.a, "God Of Chaos");
        public static bool wargodautotrait(BaseSimObject pSelf, WorldTile pTile) => AutoTrait(pSelf.a, "God Of War");
        public static bool lichgodautotrait(BaseSimObject pSelf, WorldTile pTile) => AutoTrait(pSelf.a, "God Of The Lich");
        public static bool godkillerautotrait(BaseSimObject pSelf, WorldTile pTile) => AutoTrait(pSelf.a, "God Killer");
        public static bool sungoderastatus(BaseSimObject pSelf, WorldTile pTile) => EraStatus(pSelf.a, "God Of light");
        public static bool godoffireerastatus(BaseSimObject pSelf, WorldTile pTile) => EraStatus(pSelf.a, "God Of Fire");
        public static bool knowledgegoderastatus(BaseSimObject pSelf, WorldTile pTile) => EraStatus(pSelf.a, "God Of Knowledge");
        public static bool nightgoderastatus(BaseSimObject pSelf, WorldTile pTile) => EraStatus(pSelf.a, "God Of the Night");
        public static bool stargoderastatus(BaseSimObject pSelf, WorldTile pTile) => EraStatus(pSelf.a, "God Of the Stars");
        public static bool earthgoderastatus(BaseSimObject pSelf, WorldTile pTile) => EraStatus(pSelf.a, "God Of the Earth");
        public static bool chaosgoderastatus(BaseSimObject pSelf, WorldTile pTile) => EraStatus(pSelf.a, "God Of Chaos");
        public static bool wargoderastatus(BaseSimObject pSelf, WorldTile pTile) => EraStatus(pSelf.a, "God Of War");
        public static bool lichgoderastatus(BaseSimObject pSelf, WorldTile pTile) => EraStatus(pSelf.a, "God Of The Lich");
        public static bool AutoTrait(Actor target, string trait)
        {
            if(AddAutoTraits(target, trait))
            {
                target.setStatsDirty();
                target.event_full_heal = true;
            }
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
                    pSelf.a.event_full_heal = true;
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
            if (!World.world.worldLaws.world_law_diplomacy.boolVal)
            {
                return false;
            }
            if (pTarget.a != null)
            {

                WorldTile _tile = Toolbox.getRandomTileWithinDistance(pTile, 60);
                //WorldTile tile2 = Toolbox.getRandomTileWithinDistance(pTile, 40);
                // List<WorldTile> randTile = List.Of<WorldTile>(new WorldTile[] { tile1, tile2 });
                // WorldTile _tile = Toolbox.getRandomTileWithinDistance(randTile, pTile, 45, 120);
                if (Toolbox.randomChance(GetEnhancedChance("God Of War", "seedsOfWar%")))
                {
                    MapBox.instance.dropManager.spawn(_tile, SD.spite, 5f, -1f);
                }
                if (Toolbox.randomChance(GetEnhancedChance("God Of War", "seedsOfWar%")))
                {
                    MapBox.instance.dropManager.spawn(_tile, SD.discord, 5f, -1f);
                }
                if (Toolbox.randomChance(GetEnhancedChance("God Of War", "seedsOfWar%")))
                {
                    MapBox.instance.dropManager.spawn(_tile, SD.inspiration, 5f, -1f);
                }

            }
            return true;
        }

        public static bool earthGodBuildWorld(BaseSimObject pSelf, WorldTile pTile)
        {
            if (pSelf.a != null)
            {
                if (Toolbox.randomChance(GetEnhancedChance("God Of the Earth", "buildWorld%")))
                {
                    ActionLibrary.tryToGrowTree(pSelf, pTile);
                }
                if (Toolbox.randomChance(GetEnhancedChance("God Of the Earth", "buildWorld%")))
                {
                    ActionLibrary.tryToCreatePlants(pSelf, pTile);
                }
                if (Toolbox.randomChance(GetEnhancedChance("God Of the Earth", "buildWorld%")))
                {
                    BuildingActions.tryGrowMineralRandom(pSelf.a.currentTile);
                }
                if (Toolbox.randomChance(GetEnhancedChance("God Of the Earth", "buildWorld%")))
                {
                    buildMountain(pTile);
                }

                return true;
            }
            return false;
        }
        public static void StealSouls(Actor a, Actor b)
        {
            ///not finushed
        }

        public static void buildMountain(WorldTile pTile)
        {
            WorldTile _tile = Toolbox.getRandomTileWithinDistance(pTile, 80);
            if (_tile != null)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int i = 0; i < _tile.neighbours.Length; i++)
                    {
                        //errror here!!!
                        WorldTile tile = _tile.neighbours[i];   
                        MapAction.terraformMain(tile, AssetManager.tiles.get("mountains"), TerraformLibrary.destroy);
                        MapAction.terraformMain(tile.neighbours[i], AssetManager.tiles.get("mountains"), TerraformLibrary.destroy);
                        MapAction.terraformMain(tile.neighbours[j], AssetManager.tiles.get("soil_high"), TerraformLibrary.destroy);
                        MapAction.terraformMain(tile.neighbours[j].neighbours[i], AssetManager.tiles.get("soil_high"), TerraformLibrary.destroy);
                        MapAction.terraformMain(tile.neighbours[j].neighbours[i], AssetManager.tiles.get("soil_low"), TerraformLibrary.destroy);
                        MapAction.terraformMain(tile.neighbours[i].neighbours[j], AssetManager.tiles.get("soil_low"), TerraformLibrary.destroy);
                        MapAction.terraformMain(tile.neighbours[i].neighbours[0], AssetManager.tiles.get("mountains"), TerraformLibrary.destroy);
                        MapAction.terraformMain(tile.neighbours[i].neighbours[j].neighbours[j], AssetManager.tiles.get("soil_low"), TerraformLibrary.destroy);
                        MapAction.terraformMain(tile.neighbours[j].neighbours[i].neighbours[i], AssetManager.tiles.get("soil_low"), TerraformLibrary.destroy);

                    }
                }
            }


        }
        #endregion

        public static void addTraitToLocalizedLibrary(string id, string description)
        {

            string language = Reflection.GetField(LocalizedTextManager.instance.GetType(), LocalizedTextManager.instance, "language") as string;
            Dictionary<string, string> localizedText = Reflection.GetField(LocalizedTextManager.instance.GetType(), LocalizedTextManager.instance, "localizedText") as Dictionary<string, string>;
            localizedText.Add("trait_" + id, id);
            localizedText.Add("trait_" + id + "_info", description);

        }
    }
}

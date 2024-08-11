/* 
AUTHOR: MASON SCARBRO
VERSION: 1.0.0
*/
using System;
using UnityEngine;
using ReflectionUtility;
using System.Collections.Generic;
using HarmonyLib;
using Amazon.Runtime.Internal.Transform;


namespace GodsAndPantheons
{
    //Contains the traits and their abilities & Stats
    partial class Traits
    {

        public static Dictionary<string, Dictionary<string, float>> TraitStats = new Dictionary<string, Dictionary<string, float>>()
        {
            {"God Of Chaos", new Dictionary<string, float>(){
                {S.damage, 30f},
                {S.health, 800},
                {S.attack_speed, 15f},
                {S.critical_chance, 0.05f},
                {S.range, 8f},
                {S.scale, 0.08f},
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
             }
            },
            {"God Of the Night", new Dictionary<string, float>(){
                {S.damage, 20f},
                {S.health, 550f},
                {S.attack_speed, 3f},
                {S.critical_chance, 0.25f},
                {S.range, 6f},
                {S.scale, 0.02f},
                {S.dodge, 60f}
             }
            },
            {"God Of Knowledge", new Dictionary<string, float>(){
                {S.damage, 20f},
                {S.health, 600f},
                {S.attack_speed, 1f},
                {S.critical_chance, 0.25f},
                {S.range, 15f},
                {S.scale, 0.04f},
                {S.intelligence, 35f},
                {S.accuracy, 10f}
             }
            },
            {"God Of the Stars", new Dictionary<string, float>(){
                {S.damage, 25f},
                {S.health, 600f},
                {S.attack_speed, 1f},
                {S.critical_chance, 0.05f},
                {S.range, 15f},
                {S.scale, 0.02f},
                {S.intelligence, 3f},
             }
            },
            {"God Of the Earth", new Dictionary<string, float>(){
                {S.damage, 40f},
                {S.health, 1000f},
                {S.attack_speed, 1f},
                {S.armor, 30f},
                {S.scale, 0.1f},
                {S.range, 10f},
                {S.intelligence, 3f}
             }
            },
            {"LesserGod", new Dictionary<string, float>(){
                {S.damage, 5f},
                {S.health, 400f},
                {S.attack_speed, 1f},
                {S.critical_chance, 0.05f},
                {S.scale, 0.02f},
             }
            },
            {"God Of War", new Dictionary<string, float>(){
                {S.damage, 100f},
                {S.health, 700f},
                {S.attack_speed, 35f},
                {S.armor, 20f},
                {S.knockback_reduction, 0.05f},
                {S.scale, 0.03f},
                {S.range, 8f},
                {S.warfare, 40f},
             }
            },
            {"God Of The Lich", new Dictionary<string, float>(){
                {S.damage, 100f},
                {S.health, 700f},
                {S.attack_speed, 35f},
                {S.armor, 20f},
                {S.knockback_reduction, 0.05f},
                {S.scale, 0.03f},
                {S.range, 8f},
                {S.warfare, 40f},
             }
            },
            {"God Killer", new Dictionary<string, float>(){
                {S.damage, 10f},
                {S.health, 100f},
                {S.attack_speed, 15f},
                {S.armor, 5f},
                {S.knockback_reduction, 0.1f},
                {S.scale, 0.01f},
                {S.range, 4f},
                {S.warfare, 4f},
             }
            },
            {"God Hunter", new Dictionary<string, float>(){
             }
            },
            {"God Of gods", new Dictionary<string, float>(){
                {S.damage, 200f},
                {S.health, 1000f},
                {S.attack_speed, 60f},
                {S.critical_chance, 0.5f},
                {S.intelligence, 30f},
                {S.armor, 50f},
                {S.scale, 0.075f},
                {S.range, 20f},
                {S.dodge, 35f},
                {S.accuracy, 15f},
                {S.speed, 30f}
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
            {"Failed God", new Dictionary<string, float>(){
                {S.damage, 10f},
                {S.health, 10f},
                {S.armor, 10f},
                {S.knockback_reduction, 0.5f},
             }
            }
        };
        public static Dictionary<string, List<string>> AutoTraits = new Dictionary<string, List<string>>()
        {
            {"God Of gods", new List<string>()
             {
                "blessed", 
                "poison_immune",
                "fire_proof",
                "acid_Proof",
                "freeze_proof",
                "shiny",
                "energized",
                "immortal",
                "nightchild",
                "moonchild",
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
                "fire_blood",
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
                "giant",
                "strong",
                "fat",
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
                "freeze_proof",
                "acid_proof",
                "regeneration",
                "immortal",
                "tough",
             }
            },
            {"LesserGod", new List<string>()
             {
                "blessed",
                "fire_proof",
                "freeze_proof",
                "tough",
             }
            },
            {"God Killer", new List<string>()
             {
                "blessed",
                "fire_proof",
                "freeze_proof",
                "tough",
             }
            },
            {"God Hunter", new List<string>()
             {
                "blessed",
                "fire_proof",
                "freeze_proof",
                "tough",
             }
            }
        };
        
        static PowerLibrary pb;
        public static void init()
        {

            /* Nuh uh*/

            ActorTrait chaosGod = new ActorTrait();
            chaosGod.id = "God Of Chaos";
            chaosGod.path_icon = "ui/icons/chaosGod";
            chaosGod.action_special_effect = new WorldAction(GodWeaponManager.godGiveWeapon);
            chaosGod.action_special_effect = (WorldAction)Delegate.Combine(chaosGod.action_special_effect, new WorldAction(AutoTrait));
            chaosGod.action_attack_target = new AttackAction(ActionLibrary.addBurningEffectOnTarget);
            chaosGod.action_attack_target = new AttackAction(chaosGodAttack);
            //chaosGod.action_death = new WorldAction(ActionLibrary.turnIntoDemon);
            chaosGod.action_death = (WorldAction)Delegate.Combine(chaosGod.action_death, new WorldAction(chaosGodsTrick));
            chaosGod.group_id = "GodTraits";
            AddTrait(chaosGod, "Tis's The God Of Chaos!");

            ActorTrait sunGod = new ActorTrait();
            sunGod.id = "God Of light";
            sunGod.path_icon = "ui/icons/lightGod";
            sunGod.action_special_effect = new WorldAction(GodWeaponManager.godGiveWeapon);
            sunGod.action_special_effect = (WorldAction)Delegate.Combine(sunGod.action_special_effect, new WorldAction(AutoTrait));
            sunGod.action_attack_target = new AttackAction(ActionLibrary.addBurningEffectOnTarget);
            sunGod.action_attack_target = new AttackAction(ActionLibrary.addSlowEffectOnTarget);
            sunGod.action_attack_target = new AttackAction(sunGodAttack);
            sunGod.action_death = (WorldAction)Delegate.Combine(sunGod.action_death, new WorldAction(sunGodsDeath));
            sunGod.action_special_effect = (WorldAction)Delegate.Combine(sunGod.action_special_effect, new WorldAction(sunGodEraStatus));
            sunGod.group_id = "GodTraits";
            AddTrait(sunGod, "The God Of light, controls the very light that shines and can pass through with great speed");

            ActorTrait darkGod = new ActorTrait();
            darkGod.id = "God Of the Night";
            darkGod.path_icon = "ui/icons/godDark";
            darkGod.action_special_effect = new WorldAction(GodWeaponManager.godGiveWeapon);
            darkGod.action_attack_target = new AttackAction(darkGodAttack);
            darkGod.action_death = (WorldAction)Delegate.Combine(darkGod.action_death, new WorldAction(darkGodsDeath));
            darkGod.action_special_effect = (WorldAction)Delegate.Combine(darkGod.action_special_effect, new WorldAction(BringMinions));
            darkGod.action_special_effect = (WorldAction)Delegate.Combine(darkGod.action_special_effect, new WorldAction(darkGodEraStatus));
            darkGod.action_special_effect = (WorldAction)Delegate.Combine(darkGod.action_special_effect, new WorldAction(AutoTrait));
            darkGod.group_id = "GodTraits";
            AddTrait(darkGod, "The God Of darkness, thievery and the shadows of which is his domain ");

            ActorTrait knowledgeGod = new ActorTrait();
            knowledgeGod.id = "God Of Knowledge";
            knowledgeGod.path_icon = "ui/icons/knowledgeGod";
            knowledgeGod.action_special_effect = new WorldAction(GodWeaponManager.godGiveWeapon);
            knowledgeGod.action_death = (WorldAction)Delegate.Combine(knowledgeGod.action_death, new WorldAction(genericGodsDeath));
            knowledgeGod.action_attack_target = new AttackAction(knowledgeGodAttack);
            knowledgeGod.action_special_effect = (WorldAction)Delegate.Combine(knowledgeGod.action_special_effect, new WorldAction(knowledgeGodEraStatus));
            knowledgeGod.action_special_effect = (WorldAction)Delegate.Combine(knowledgeGod.action_special_effect, new WorldAction(AutoTrait));
            knowledgeGod.group_id = "GodTraits";
            AddTrait(knowledgeGod, "The God Of Knowledge, His mind excedes Time, he knows all");

            ActorTrait starsGod = new ActorTrait();
            starsGod.id = "God Of the Stars";
            starsGod.path_icon = "ui/icons/starsGod";
            starsGod.action_special_effect = new WorldAction(GodWeaponManager.godGiveWeapon);
            starsGod.action_death = (WorldAction)Delegate.Combine(starsGod.action_death, new WorldAction(starsGodsDeath));
            starsGod.action_attack_target = new AttackAction(ActionLibrary.addFrozenEffectOnTarget);
            starsGod.action_attack_target += new AttackAction(starsGodAttack);
            starsGod.action_special_effect = (WorldAction)Delegate.Combine(starsGod.action_special_effect, new WorldAction(starsGodEraStatus));
            starsGod.action_special_effect = (WorldAction)Delegate.Combine(starsGod.action_special_effect, new WorldAction(BringMinions));
            starsGod.group_id = "GodTraits";
            starsGod.action_special_effect = (WorldAction)Delegate.Combine(starsGod.action_special_effect, new WorldAction(AutoTrait));
            AddTrait(starsGod, "Now Cometh the Age of stars, A Thousand Year Voyage under the wisdom of the moon");

            ActorTrait earthGod = new ActorTrait();
            earthGod.id = "God Of the Earth";
            earthGod.path_icon = "ui/icons/earthGod";
            earthGod.action_attack_target = new AttackAction(earthGodAttack);
            earthGod.group_id = "GodTraits";
            earthGod.action_special_effect = (WorldAction)Delegate.Combine(earthGod.action_special_effect, new WorldAction(BringMinions));
            earthGod.action_special_effect = new WorldAction(earthGodBuildWorld);
            earthGod.action_special_effect += new WorldAction(GodWeaponManager.godGiveWeapon);
            earthGod.action_special_effect = (WorldAction)Delegate.Combine(earthGod.action_special_effect, new WorldAction(AutoTrait));
            AddTrait(earthGod, "God of the Natural Enviornment, The titan of creation");

            ActorTrait subGod = new ActorTrait();
            subGod.id = "LesserGod";
            subGod.path_icon = "ui/icons/subGod";
            subGod.group_id = "GodTraits";
            subGod.action_special_effect = (WorldAction)Delegate.Combine(subGod.action_special_effect, new WorldAction(AutoTrait));
            AddTrait(subGod, "These Are the gods that have smaller importance");

            ActorTrait warGod = new ActorTrait();
            warGod.id = "God Of War";
            warGod.path_icon = "ui/icons/warGod";
            warGod.action_attack_target = new AttackAction(warGodAttack);
            warGod.action_special_effect = new WorldAction(GodWeaponManager.godGiveWeapon);
            warGod.action_special_effect = (WorldAction)Delegate.Combine(warGod.action_special_effect, new WorldAction(AutoTrait));
            warGod.action_special_effect = (WorldAction)Delegate.Combine(warGod.action_special_effect, new WorldAction(warGodSeeds));
            warGod.group_id = "GodTraits";
            AddTrait(warGod, "God of Conflict, Bravery, Ambition, Many spheres of domain lie with him");


            ActorTrait lichGod = new ActorTrait();
            lichGod.id = "God Of The Lich";
            lichGod.path_icon = "ui/icons/lichGod";
            lichGod.action_death = (WorldAction)Delegate.Combine(lichGod.action_death, new WorldAction(genericGodsDeath));
            lichGod.action_attack_target = new AttackAction(lichGodAttack);
            lichGod.action_special_effect = new WorldAction(GodWeaponManager.godGiveWeapon);
            lichGod.action_special_effect = (WorldAction)Delegate.Combine(lichGod.action_special_effect, new WorldAction(BringMinions));
            lichGod.action_special_effect = (WorldAction)Delegate.Combine(lichGod.action_special_effect, new WorldAction(AutoTrait));
            lichGod.group_id = "GodTraits";
            AddTrait(lichGod, "God of Dead Souls, Corruption, and Rot, Many spheres of domain lie with him");

            ActorTrait godKiller = new ActorTrait();
            godKiller.id = "God Killer";
            godKiller.path_icon = "ui/icons/godKiller";
            godKiller.action_special_effect = (WorldAction)Delegate.Combine(godKiller.action_special_effect, new WorldAction(AutoTrait));
            godKiller.group_id = "GodTraits";
            AddTrait(godKiller, "To Kill a God is nearly to become one");


            ActorTrait godHunter = new ActorTrait();
            godHunter.id = "God Hunter";
            godHunter.path_icon = "ui/icons/godKiller";
            godHunter.action_special_effect = new WorldAction(SuperRegeneration);
            godHunter.action_special_effect = (WorldAction)Delegate.Combine(godHunter.action_special_effect, new WorldAction(GodWeaponManager.godGiveWeapon));
            godHunter.action_special_effect = (WorldAction)Delegate.Combine(godHunter.action_special_effect, new WorldAction(AutoTrait));
            godHunter.action_special_effect = (WorldAction)Delegate.Combine(godHunter.action_special_effect, new WorldAction(ChaseGod));
            godHunter.group_id = "GodTraits";
            AddTrait(godHunter, "He will stop at NOTHING to kill a god");

            //my traits
            ActorTrait godofgods = new ActorTrait();
            godofgods.id = "God Of gods";
            godofgods.path_icon = "ui/icons/GodofGods";
            godofgods.action_death = new WorldAction(ActionLibrary.deathNuke);
            godofgods.action_special_effect = (WorldAction)Delegate.Combine(godofgods.action_special_effect, new WorldAction(AutoTrait));
            godofgods.action_special_effect = (WorldAction)Delegate.Combine(godofgods.action_special_effect, new WorldAction(BringMinions));
            godofgods.action_special_effect = (WorldAction)Delegate.Combine(godofgods.action_special_effect, new WorldAction(GodOfGodsEraStatus));
            godofgods.action_attack_target += new AttackAction(GodOfGodsAttack);
            godofgods.group_id = "GodTraits";
            AddTrait(godofgods, "The god who rules among all");

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
            AddTrait(DemiGod, "The Demi God, offspring of Gods and Mortals");
            pb = new PowerLibrary();

            ActorTrait FailedGod = new ActorTrait();
            FailedGod.id = "Failed God";
            FailedGod.path_icon = "ui/icons/iconCurse";
            FailedGod.group_id = TraitGroup.special;
            FailedGod.can_be_given = false;
            AddTrait(FailedGod, "his Genes were recessive");
            pb = new PowerLibrary();
        }
        //to make summoned ones only live for like 30 secounds
        public static bool SummonedBeing(BaseSimObject pTarget, WorldTile pTile)
        {
            Actor a = (Actor)pTarget;
            int life;
            int lifespan;
            a.data.get("lifespan", out lifespan);
            a.data.get("life", out life);
            a.data.set("life", life + 1);
            if (life + 1 > lifespan)
            {
                a.killHimself(false, AttackType.Age, false, true, true);
            }
            return true;
        }
        //if god is too far away the god hunter will teleport to them
        public static bool ChaseGod(BaseSimObject pTarget, WorldTile pTile)
        {
            if (pTarget.isActor() && Main.savedSettings.HunterAssasins)
            {
                BaseSimObject? a = Reflection.GetField(typeof(ActorBase), pTarget, "attackTarget") as BaseSimObject;
                if (a != null)
                {
                    if (IsGod(a.a))
                    {
                        if (TeleportNearActor(pTarget.a, a, 30, false, true)) SuperRegeneration(pTarget, 0.5f, 10);
                    }
                }
                else if (Toolbox.randomChance(0.5f))
                {
                    if (TeleportNearActor(pTarget.a, Toolbox.getClosestActor(FindGods(true, pTarget.a), pTarget.currentTile), 60, false, true)) SuperRegeneration(pTarget, 0.5f, 25);
                }

            }
            return true;
        }
        
        public static bool SuperRegeneration(BaseSimObject pTarget, WorldTile pTile)
        {
            if (Toolbox.randomChance(0.1f))
            {
                pTarget.a.restoreHealth((int)(pTarget.a.getMaxHealth() * 0.05f));
                return true;
            }
            return false;
        }
        //god of gods attack
        public static bool GodOfGodsAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            Actor self = (Actor)pSelf;
            if (pTarget != null)
            {
                Vector2Int pos = pTile.pos; // Position of the Ptile as a Vector 2
                float pDist = Vector2.Distance(pTarget.currentPosition, pos); // the distance between the target and the pTile
                if (Toolbox.randomChance(GetChance("GodOfGodsWindow", "Terrain bending%") / 100))
                {
                    int decider = Toolbox.randomInt(1, 4);
                    switch (decider)
                    {
                        case 1: ActionLibrary.castLightning(null, pTarget, null); break;
                        case 2: EffectsLibrary.spawn("fx_meteorite", pTarget.currentTile, "meteorite_disaster", null, 0f, -1f, -1f); pSelf.a.addStatusEffect("invincible", 1f); break;
                        case 3: ActionLibrary.castTornado(pSelf, pTarget, pTile); break;
                        case 4: pb.spawnEarthquake(pTarget.a.currentTile, null); break;
                    }
                }
                if (Toolbox.randomChance(GetChance("GodOfGodsWindow", "Summoning%") / 100))
                {
                    int decider = Toolbox.randomInt(1, 3);
                    switch (decider)
                    {
                        case 1: Summon(SA.demon, 1, pSelf, pTile); break;
                        case 2: Summon(SA.evilMage, 1, pSelf, pTile); break;
                        case 3: Summon(SA.skeleton, 3, pSelf, pTile); break;
                    }
                }
                if (Toolbox.randomChance(GetChance("GodOfGodsWindow", "Magic%") / 100))
                {
                    int decider = Toolbox.randomInt(1, 5);
                    switch (decider)
                    {
                        case 1: ActionLibrary.addFrozenEffectOnTarget(null, pTarget, null); break;

                        case 2:
                            EffectsLibrary.spawn("fx_explosion_middle", pTarget.a.currentTile, null, null, 0f, -1f, -1f);
                            pSelf.a.addStatusEffect("invincible", 1f); break;

                        // randomly spawns a flash of fire or acid on the tile 
                        case 3:
                            MapBox.instance.dropManager.spawn(pTile, "fire", 5f, -1f);
                            MapBox.instance.dropManager.spawn(pTile, "acid", 5f, -1f);
                            MapBox.instance.dropManager.spawn(pTile, "fire", 5f, -1f); break;

                        case 4:
                            {
                                Vector3 newPoint = Toolbox.getNewPoint(pSelf.currentPosition.x, pSelf.currentPosition.y, (float)pos.x, (float)pos.y, pDist, true); // the Point of the projectile launcher 
                                Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pTarget.a.stats[S.size], true);
                                EffectsLibrary.spawnProjectile("lightBallzProjectiles", newPoint, newPoint2, 0.0f); break;
                            }

                        case 5:
                            {
                                Vector3 newPoint = Toolbox.getNewPoint(pSelf.currentPosition.x + 35f, pSelf.currentPosition.y + 95f, (float)pos.x + 1f, (float)pos.y + 1f, pDist, true); // the Point of the projectile launcher 
                                Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pTarget.a.stats[S.size], true);
                                EffectsLibrary.spawnProjectile("moonFall", newPoint, newPoint2, 0.0f);
                                pSelf.a.addStatusEffect("invincible", 2f); break;
                            }
                    }
                }

                return true;
            }
            return false;
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

            BaseSimObject attackedBy = pTarget.a.attackedBy;
            if (!((BaseSimObject)attackedBy != null) || !attackedBy.isActor() || !attackedBy.isAlive())
            {
                return false;
            }
            if (Main.savedSettings.deathera)
                World.world.eraManager.setEra(S.age_dark, true);
            return true;

        }

        public static bool starsGodsDeath(BaseSimObject pTarget, WorldTile pTile = null)
        {

            BaseSimObject attackedBy = pTarget.a.attackedBy;
            if (!((BaseSimObject)attackedBy != null) || !attackedBy.isActor() || !attackedBy.isAlive())
            {
                return false;
            }
            if (Main.savedSettings.deathera)
                World.world.eraManager.setEra(S.age_moon, true);
            return true;

        }

        public static bool darkGodsDeath(BaseSimObject pTarget, WorldTile pTile = null)
        {
            BaseSimObject attackedBy = pTarget.a.attackedBy;
            if (!((BaseSimObject)attackedBy != null) || !attackedBy.isActor() || !attackedBy.isAlive())
            {
                return false;
            }
            if (Main.savedSettings.deathera)
                World.world.eraManager.setEra(S.age_sun, true);


            return true;

        }

        public static bool genericGodsDeath(BaseSimObject pTarget, WorldTile pTile = null)
        {
            BaseSimObject attackedBy = pTarget.a.attackedBy;
            if (!(attackedBy != null) || !attackedBy.isActor() || !attackedBy.isAlive())
            {
                return false;
            }
            return true;

        }

        public static bool chaosGodAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (pTarget != null && pSelf.isActor())
            {
                if (Toolbox.randomChance(GetChance("ChaosGodWindow", "Power1%") /100))
                {
                    Vector2Int pos = pTile.pos; // Position of the Ptile as a Vector 2
                    float pDist = Vector2.Distance(pTarget.currentPosition, pos); // the distance between the target and the pTile
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.currentPosition.x, pSelf.currentPosition.y, (float)pos.x, (float)pos.y, pDist, true); // the Point of the projectile launcher 
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pTarget.a.stats[S.size], true);
                    EffectsLibrary.spawnProjectile("fireBallX", newPoint, newPoint2, 0.0f);

                }
                //new ability: unleach chaos
                if (Toolbox.randomChance(GetChance("ChaosGodWindow", "Power2%") / 100))
                {
                    bool hasmadness = pSelf.a.hasTrait("madness");
                    DropsLibrary.action_madness(pTile);
                    if (!hasmadness) pSelf.a.removeTrait("madness");

                    World.world.getObjectsInChunks(pTile, 5, MapObjectType.Actor);
                    foreach (Actor Actor in World.world.temp_map_objects)
                    {
                        if (Actor.hasTrait("Summoned One"))
                        {
                            CorruptSummonedOne(Actor);
                        }
                    }
                }
                if (Toolbox.randomChance(GetChance("ChaosGodWindow", "Power3%") / 100))
                {
                    pb.spawnBoulder(pTarget.a.currentTile, null);
                }


                return true;
            }
            return false;
        }


        public static bool knowledgeGodAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (pTarget != null)
            {

                if (Toolbox.randomChance(GetChance("KnowledgeGodWindow", "KnowledgeGodPwr1%") / 100))
                {
                    // randomly spawns a flash of fire or acid on the tile 
                    MapBox.instance.dropManager.spawn(pTile, "fire", 5f, -1f);
                    MapBox.instance.dropManager.spawn(pTile, "acid", 5f, -1f);
                    MapBox.instance.dropManager.spawn(pTile, "fire", 5f, -1f); // Drops fire from distance 5 with scale of one at current tile
                }
                if (Toolbox.randomChance(GetChance("KnowledgeGodWindow", "KnowledgeGodPwr2%") / 100))
                {
                    ActionLibrary.castCurses(null, pTarget, null); // casts curses
                    ((Actor)pSelf).removeTrait("cursed");
                }
                if (Toolbox.randomChance(GetChance("KnowledgeGodWindow", "KnowledgeGodPwr3%") / 100))
                {
                    ActionLibrary.addFrozenEffectOnTarget(null, pTarget, null); // freezezz the target
                }
                if (Toolbox.randomChance(GetChance("KnowledgeGodWindow", "KnowledgeGodPwr4%") / 100))
                {
                    ActionLibrary.castShieldOnHimself(null, pSelf, null); // Casts a shield for himself !! hint: pSelf !!
                }
                if (Toolbox.randomChance(GetChance("KnowledgeGodWindow", "KnowledgeGodPwr5%") / 100))
                {
                    ActionLibrary.teleportRandom(null, pTarget, null); // flee
                }
                if (Toolbox.randomChance(GetChance("KnowledgeGodWindow", "SummonLightning%") / 100))
                {
                    ActionLibrary.castLightning(null, pTarget, null); // Casts Lightning on the target
                }
                if (Toolbox.randomChance(GetChance("KnowledgeGodWindow", "SummonMeteor%") / 100))
                {
                    EffectsLibrary.spawn("fx_meteorite", pTarget.currentTile, "meteorite_disaster", null, 0f, -1f, -1f);    //spawn 1 meteorite
                    pSelf.a.addStatusEffect("invincible", 5f);
                }
                if (Toolbox.randomChance(GetChance("KnowledgeGodWindow", "PagesOfKnowledge%") / 100))
                {
                    Vector2Int pos = pTile.pos; // Position of the Ptile as a Vector 2
                    float pDist = Vector2.Distance(pTarget.currentPosition, pos); // the distance between the target and the pTile
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.currentPosition.x, pSelf.currentPosition.y, (float)pos.x, (float)pos.y, pDist, true); // the Point of the projectile launcher 
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pTarget.a.stats[S.size], true);
                    EffectsLibrary.spawnProjectile("PagesOfKnowledge", newPoint, newPoint2, 0.0f);
                }

                return true;
            }
            return false;
        }


        public static bool darkGodAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (pTarget != null)
            {


                if (Toolbox.randomChance(GetChance("DarkGodWindow", "cloudOfDarkness%") / 100))
                {
                    EffectsLibrary.spawn("fx_antimatter_effect", pTarget.a.currentTile, null, null, 0f, -1f, -1f);
                    pSelf.a.addStatusEffect("invincible", 5f);
                }
                if (Toolbox.randomChance(GetChance("DarkGodWindow", "blackHole%") / 100))
                {
                    Vector2Int pos = pTile.pos; // Position of the Ptile as a Vector 2
                    float pDist = Vector2.Distance(pTarget.currentPosition, pos); // the distance between the target and the pTile
                    Vector3 newPoint = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pDist, true); // the Point of the projectile launcher 
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pTarget.a.stats[S.size], true);
                    EffectsLibrary.spawnProjectile("BlackHoleProjectile1", newPoint, newPoint2, 0.0f);


                }
                if (Toolbox.randomChance(GetChance("DarkGodWindow", "darkDaggers%") / 100))
                {
                    Vector2Int pos = pTile.pos; // Position of the Ptile as a Vector 2
                    float pDist = Vector2.Distance(pTarget.currentPosition, pos); // the distance between the target and the pTile
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.currentPosition.x, pSelf.currentPosition.y, (float)pos.x, (float)pos.y, pDist, true); // the Point of the projectile launcher 
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pTarget.a.stats[S.size], true);
                    EffectsLibrary.spawnProjectile("DarkDaggersProjectiles", newPoint, newPoint2, 0.0f);


                }
                if (Toolbox.randomChance(GetChance("DarkGodWindow", "smokeFlash%") / 100))
                {
                    EffectsLibrary.spawnAtTile("fx_smokeFlash_dej", pTile, 0.1f);
                    MapAction.damageWorld(pTarget.currentTile, 5, AssetManager.terraform.get("lightning_power"), null);
                    MapAction.damageWorld(pTarget.currentTile, 8, AssetManager.terraform.get("smokeFlash"), null);
                    World.world.applyForce(pTarget.currentTile, 2, 0.4f, false, true, 20, null, pTarget, null);

                }
                if (Toolbox.randomChance(GetChance("DarkGodWindow", "summonDarkOne%") / 100))
                {
                    Summon("DarkOne", 4, pSelf, pTile);
                }


                return true;
            }
            return false;
        }


        public static bool starsGodAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {


            if (pTarget != null)
            {
                if (Toolbox.randomChance(GetChance("MoonGodWindow", "cometAzure%") / 100))
                {
                    EffectsLibrary.spawnAtTile("fx_cometAzureDown_dej", pTarget.a.currentTile, 0.1f);
                    MapAction.applyTileDamage(pTarget.currentTile, 8, AssetManager.terraform.get("cometAzureDownDamage"));
                    MapAction.damageWorld(pTarget.currentTile.neighbours[2], 8, AssetManager.terraform.get("cometAzureDownDamage"), null);
                    MapAction.damageWorld(pTarget.currentTile.neighbours[1], 8, AssetManager.terraform.get("cometAzureDownDamage"), null);
                    World.world.applyForce(pTarget.currentTile.neighbours[0], 4, 0.4f, false, true, 200, null, pTarget, null);
                    pSelf.a.addStatusEffect("invincible", 5f);
                }
                if (Toolbox.randomChance(GetChance("MoonGodWindow", "cometShower%") / 100))
                {
                    EffectsLibrary.spawnAtTile("fx_cometShower_dej", pTarget.a.currentTile, 0.09f);
                    MapAction.applyTileDamage(pTarget.currentTile, 2f, AssetManager.terraform.get("cometRain"));
                    pSelf.a.addStatusEffect("invincible", 5f);
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
                    World.world.applyForce(pTarget.currentTile.neighbours[3].neighbours[3].neighbours[2].neighbours[2], 2, 0.4f, false, true, 20, null, pTarget, null);
                    World.world.applyForce(pTarget.currentTile.neighbours[3].neighbours[3].neighbours[3].neighbours[3], 2, 0.4f, false, true, 20, null, pTarget, null);



                }
                if (Toolbox.randomChance(GetChance("MoonGodWindow", "summonWolf%") / 100))
                {
                    Summon(SA.wolf, 3, pSelf, pTile);
                }



                return true;
            }
            return false;
        }

        public static bool sunGodAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {



            if (pTarget != null)
            {


                if (Toolbox.randomChance(GetChance("SunGodWindow", "flashOfLight%") / 100))
                {
                    pb.divineLightFX(pTarget.a.currentTile, null);
                    EffectsLibrary.spawn("fx_napalm_flash", pTarget.a.currentTile, null, null, 0f, -1f, -1f);
                    pSelf.a.addStatusEffect("invincible", 5f);
                }
                if (Toolbox.randomChance(GetChance("SunGodWindow", "beamOfLight%") / 100))
                {
                    pb.divineLightFX(pTarget.a.currentTile, null);
                    pTarget.a.addStatusEffect("burning", 5f);

                }
                if (Toolbox.randomChance(GetChance("SunGodWindow", "speedOfLight%") / 100))
                {
                    pb.divineLightFX(pTarget.a.currentTile, null);
                    EffectsLibrary.spawn("fx_thunder_flash", pSelf.a.currentTile, null, null, 0f, -1f, -1f);
                    pSelf.a.addStatusEffect("caffeinated", 10f);
                    pTarget.a.addStatusEffect("slowness", 10f);


                }
                if (Toolbox.randomChance(GetChance("SunGodWindow", "lightBallz%") / 100))
                {
                    Vector2Int pos = pTile.pos; // Position of the Ptile as a Vector 2
                    float pDist = Vector2.Distance(pTarget.currentPosition, pos); // the distance between the target and the pTile
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.currentPosition.x, pSelf.currentPosition.y, (float)pos.x, (float)pos.y, pDist, true); // the Point of the projectile launcher 
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pTarget.a.stats[S.size], true);
                    EffectsLibrary.spawnProjectile("lightBallzProjectiles", newPoint, newPoint2, 0.0f);
                }
                //EffectsLibrary.spawnAtTile("fx_multiFlash_dej", pTile, 0.01f);
                //not finushed?
                if (Toolbox.randomChance(0 / 100))
                {
                    //TO BE USED AS IMACT ACTION FOR LIGHT PROJECILES LATER
                    int count = 0;
                    Vector2Int pos = pTile.pos; // Position of the Ptile as a Vector 2
                    float pDist = Vector2.Distance(pTarget.currentPosition, pos);
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pTarget.a.stats[S.size], true);

                    EffectsLibrary.spawn("fx_napalm_flash", pTarget.a.currentTile.neighbours[2].neighbours[2].neighbours[1].neighbours[1].neighbours[2], null, null, 0f, -1f, -1f);
                    count++;
                    if (count == 1)
                    {
                        EffectsLibrary.spawn("fx_napalm_flash", pTarget.a.currentTile.neighbours[1].neighbours[0].neighbours[0].neighbours[1].neighbours[0].neighbours[0].neighbours[1].neighbours[1].neighbours[0].neighbours[0].neighbours[0], null, null, 0f, -0.5f, -0.2f);
                        count++;
                    }
                    if (count == 2)
                    {
                        EffectsLibrary.spawn("fx_napalm_flash", pTarget.a.currentTile.neighbours[1].neighbours[3].neighbours[2].neighbours[1].neighbours[2].neighbours[3].neighbours[1].neighbours[1].neighbours[0].neighbours[0], null, null, 0f, 1f, -0.2f);
                        count++;
                    }
                    if (count == 3)
                    {
                        EffectsLibrary.spawn("fx_napalm_flash", pTarget.a.currentTile.neighbours[1].neighbours[3].neighbours[2].neighbours[1].neighbours[2].neighbours[3].neighbours[1].neighbours[1].neighbours[0].neighbours[0], null, null, 0f, -0.5f, -0.2f);
                        EffectsLibrary.spawn("fx_napalm_flash", pTarget.a.currentTile.neighbours[1].neighbours[3].neighbours[0].neighbours[1].neighbours[0].neighbours[0].neighbours[0].neighbours[1].neighbours[0].neighbours[0], null, null, 0f, -0.5f, -0.2f);

                    }




                    pSelf.a.addStatusEffect("invincible", 5f);
                }


                return true;
            }
            return false;
        }

        public static bool warGodAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {



            if (pTarget != null)
            {

                if (Toolbox.randomChance(GetChance("WarGodWindow", "warGodsCry%") / 100))
                {
                    EffectsLibrary.spawnExplosionWave(pSelf.currentTile.posV3, 1f, 1f);
                    pSelf.a.addStatusEffect("WarGodsCry", 30f);
                    World.world.startShake(0.3f, 0.01f, 2f, true, true);
                    pSelf.a.addStatusEffect("invincible", 1f);
                    MapAction.damageWorld(pSelf.currentTile, 2, AssetManager.terraform.get("crab_step"), null);
                    pSelf.a.addStatusEffect("invincible", 1f);
                    World.world.applyForce(pSelf.currentTile, 4, 0.4f, false, true, 20, null, pTarget, null);

                }
                if (Toolbox.randomChance(GetChance("WarGodWindow", "seedsOfWar%") / 100))
                {
                    WorldTile _tile = Toolbox.getRandomTileWithinDistance(pTarget.a.currentTile, 5);
                    // Spawns Rage Cloud above enemy
                    pb.spawnCloudRage(pTarget.a.currentTile, null);
                    
                    //Gods arent effected by this and dispel his attack
                    if (!IsGod(pTarget.a))
                    {
                        MapBox.instance.dropManager.spawn(pTarget.a.currentTile, SD.madness, 5f, -1f);
                        MapBox.instance.dropManager.spawn(_tile, SD.madness, 5f, -1f);
                        MapBox.instance.dropManager.spawn(_tile.neighbours[0], SD.madness, 5f, -1f);
                    }
                    // randomly drops madness around enemies
                    

                    //if he accidentally gives it to himself
                    if (pSelf.a.hasTrait("madness")) pSelf.a.removeTrait("madness");

                }
                return true;
            }
            return false;
        }

        public static bool earthGodAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {


            if (pTarget != null)
            {
                
                

                if (Toolbox.randomChance(GetChance("EarthGodWindow", "earthquake%") / 100))
                {
                    pb.spawnEarthquake(pTarget.a.currentTile, null);
                }
                if (Toolbox.randomChance(GetChance("EarthGodWindow", "makeItRain%") / 100))
                {
                    pb.spawnCloudRain(pTarget.a.currentTile, null);
                    pb.spawnCloudSnow(pTarget.a.currentTile, null);
                }
                if (Toolbox.randomChance(GetChance("EarthGodWindow", "SummonDruid%") / 100))
                {
                    Summon(SA.druid, 1, pSelf, pTile);
                }
                return true;
            }
            return false;
        }


        public static bool lichGodAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            Actor self = (Actor)pSelf;
            if (Toolbox.randomChance(GetChance("LichGodWindow", "summonSkele%") / 100))
            {
                //Lich God Summons Skeletons
                Summon(SA.skeleton, 8, pSelf, pTile);
            }
            if (Toolbox.randomChance(GetChance("LichGodWindow", "summonDead%") / 100))
            {
                //Lich God Summons Zombie
                Summon(SA.walker, 2, pSelf, pTile);
            }
            if (Toolbox.randomChance(GetChance("LichGodWindow", "rigorMortisHand%") / 100))
            {
                //Lich God Summons RigorMortis Hand
                EffectsLibrary.spawnAtTile("fx_handgrab_dej", pTarget.a.currentTile, 0.1f);
                pTarget.a.addStatusEffect("slowness");
                pTarget.a.addStatusEffect("poisoned");
            }
            return true;
        }
        public static bool AutoTrait(BaseSimObject pTarget, WorldTile pTile)
        {
            if (pTarget.isActor()) {
                foreach (string trait in AutoTraits.Keys)
                {
                    if (pTarget.a.hasTrait(trait))
                    {
                        AddAutoTraits(pTarget.a.data, trait);
                    }
                }
            }
            return true;
        }
        private static bool sunGodEraStatus(BaseSimObject pSelf, WorldTile pTile)
        {
            if (pSelf.a != null)
            {
                if (World.world_era.id == "age_sun")       //only in age of sun
                {
                    pSelf.a.addStatusEffect("Lights_Prevail"); // add the status I created

                }
                else
                {
                    if (pSelf.a.hasStatus("Lights_Prevail"))          //no other age can have this trait
                    {
                        pSelf.a.finishAllStatusEffects(); // remove the status
                    }
                }


            }
            return true;
        }
        private static bool SummonedOneEraStatus(BaseSimObject pSelf, WorldTile pTile)
        {
            if (pSelf.a != null)
            {
                bool InEra = false;
                Actor master = FindMaster(pSelf.a);
                if(master != null)
                {
                    if (master.hasStatus("God_Of_All"))
                    {
                        pSelf.a.addStatusEffect("God_Of_All"); 
                        InEra = true;
                    }
                    if (master.hasStatus("Nights_Prevail"))
                    {
                        pSelf.a.addStatusEffect("Nights_Prevail"); 
                        InEra = true;
                    }
                    if (master.hasStatus("Stars_Prevail"))
                    {
                        pSelf.a.addStatusEffect("Stars_Prevail"); 
                        InEra = true;
                    }
                }
                if (InEra)
                {
                    pSelf.a.data.set("lifespan", 61);
                    SuperRegeneration(pSelf, pTile);
                }
                else
                {
                    pSelf.a.finishAllStatusEffects(); // remove the status
                    pSelf.a.data.set("lifespan", 31);
                }
            }
            return true;
        }
        private static bool GodOfGodsEraStatus(BaseSimObject pSelf, WorldTile pTile)
        {
            if (pSelf.a != null)
            {
                if (World.world_era.id == "age_hope")
                {    //only in age of hope
                    if (!pSelf.a.hasStatus("God_Of_All"))
                    {
                        {
                            pSelf.a.addStatusEffect("God_Of_All"); // add the status I created
                           
                        }
                    }
                }
                else
                {
                    if (pSelf.a.hasStatus("God_Of_All"))          //no other age can have this trait
                    {
                        pSelf.a.finishAllStatusEffects(); // remove the status
                        pSelf.a.data.set("lifespan", 31);
                    }
                }
            }
            return true;
        }

        private static bool warGodEraStatus(BaseSimObject pSelf, WorldTile pTile)
        {
            if (pSelf.a != null)
            {
                if (World.world_era.id == "age_ash")       //only in age of sun
                {
                    pSelf.a.addStatusEffect("War_Prevail"); // add the status I created

                }
                else
                {
                    if (pSelf.a.hasStatus("War_Prevail"))          //no other age can have this trait
                    {
                        pSelf.a.finishAllStatusEffects(); // remove the status
                    }
                }


            }
            return true;
        }

        private static bool starsGodEraStatus(BaseSimObject pSelf, WorldTile pTile)
        {
            if (pSelf.a != null)
            {
                if (World.world_era.id == "age_moon")       //only in age of moon
                {
                    pSelf.a.addStatusEffect("Stars_Prevail"); // add the status I created

                }
                else
                {
                    if (pSelf.a.hasStatus("Stars_Prevail"))          //no other age can have this trait
                    {
                        pSelf.a.finishAllStatusEffects(); // remove the status
                    }
                }


            }
            return true;
        }

        private static bool darkGodEraStatus(BaseSimObject pSelf, WorldTile pTile)
        {
            if (pSelf.a != null)
            {
                if (World.world_era.id == "age_dark")       //only in age of dark
                {
                    pSelf.a.addStatusEffect("Nights_Prevail"); // add the status I created

                }
                else
                {
                    if (pSelf.a.hasStatus("Nights_Prevail"))          //no other age can have this trait
                    {
                        pSelf.a.finishAllStatusEffects(); // remove the status
                    }
                }


            }
            return true;
        }

        private static bool knowledgeGodEraStatus(BaseSimObject pSelf, WorldTile pTile)
        {
            if (pSelf.a != null)
            {
                if (World.world_era.id == "age_wonders")       //only in age of wonder
                {
                    pSelf.a.addStatusEffect("Knowledge_Prevail"); // add the status I created

                }
                else
                {
                    if (pSelf.a.hasStatus("Knowledge_Prevail"))          //no other age can have this trait
                    {
                        pSelf.a.finishAllStatusEffects(); // remove the status
                    }
                }


            }
            return true;
        }

        public static bool warGodSeeds(BaseSimObject pTarget, WorldTile pTile)
        {

            if (pTarget.a != null)
            {

                WorldTile _tile = Toolbox.getRandomTileWithinDistance(pTile, 60);
                //WorldTile tile2 = Toolbox.getRandomTileWithinDistance(pTile, 40);
                // List<WorldTile> randTile = List.Of<WorldTile>(new WorldTile[] { tile1, tile2 });
                // WorldTile _tile = Toolbox.getRandomTileWithinDistance(randTile, pTile, 45, 120);
                if (Toolbox.randomChance(GetChance("WarGodWindow", "seedsOfWar%") / 100))
                {
                    MapBox.instance.dropManager.spawn(_tile, SD.spite, 5f, -1f);
                }
                if (Toolbox.randomChance(GetChance("WarGodWindow", "seedsOfWar%") / 100))
                {
                    MapBox.instance.dropManager.spawn(_tile, SD.discord, 5f, -1f);
                }
                if (Toolbox.randomChance(GetChance("WarGodWindow", "seedsOfWar%") / 100))
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
                if (Toolbox.randomChance(GetChance("EarthGodWindow", "buildWorld%") / 100))
                {
                    ActionLibrary.tryToGrowTree(pSelf, pTile);
                }
                if (Toolbox.randomChance(GetChance("EarthGodWindow", "buildWorld%") / 100))
                {
                    ActionLibrary.tryToCreatePlants(pSelf, pTile);
                }
                if (Toolbox.randomChance(GetChance("EarthGodWindow", "buildWorld%") / 100))
                {
                    BuildingActions.tryGrowMineralRandom(pSelf.a.currentTile);
                }
                if (Toolbox.randomChance(GetChance("EarthGodWindow", "buildWorld%") / 100))
                {

                    buildMountain(pTile);
                    Debug.Log("IGNORE THIS ERROR AND KEEP PLAYING!");

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
            WorldTile tile1 = Toolbox.getRandomTileWithinDistance(pTile, 50);
            WorldTile tile2 = Toolbox.getRandomTileWithinDistance(pTile, 25);
            List<WorldTile> randTile = List.Of<WorldTile>(new WorldTile[] { tile1, tile2 });
            WorldTile _tile = Toolbox.getRandomTileWithinDistance(randTile, pTile, 30, 120);
            if (_tile != null)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int i = 0; i < 4; i++)
                    {
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
        public static void addTraitToLocalizedLibrary(string id, string description)
        {

            string language = Reflection.GetField(LocalizedTextManager.instance.GetType(), LocalizedTextManager.instance, "language") as string;
            Dictionary<string, string> localizedText = Reflection.GetField(LocalizedTextManager.instance.GetType(), LocalizedTextManager.instance, "localizedText") as Dictionary<string, string>;
            localizedText.Add("trait_" + id, id);
            localizedText.Add("trait_" + id + "_info", description);

        }
    }
}

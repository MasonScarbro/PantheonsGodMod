/* 
AUTHOR: MASON SCARBRO
VERSION: 1.0.0
*/
using System.Collections.Generic;
using UnityEngine;

namespace GodsAndPantheons
{
    class Effects
    {
        public static void init()
        {

            StatusAsset sunGodEra = new StatusAsset();
            sunGodEra.id = "Lights_Prevail";
            sunGodEra.duration = 7000f;
            sunGodEra.base_stats[S.armor] += 13;
            sunGodEra.base_stats[S.health] += 500;
            sunGodEra.base_stats[S.speed] += 300;
            sunGodEra.base_stats[S.mass] += 0.5f;
            sunGodEra.base_stats[S.knockback] += 1f;
            sunGodEra.base_stats[S.attack_speed] += 80f;
            sunGodEra.path_icon = "ui/icons/lightGod";
            sunGodEra.locale_description = "The World, Light up";
            sunGodEra.locale_id = "Lights Prevail";
            sunGodEra.action_interval = 2;
            sunGodEra.action = new WorldAction(Traits.SuperRegeneration);
            localizeStatus(sunGodEra.id, "Lights Prevail", sunGodEra.locale_description); // Localizes the status effect
            AssetManager.status.add(sunGodEra);


            StatusAsset darkGodEra = new StatusAsset();
            darkGodEra.id = "Nights_Prevail";
            darkGodEra.duration = 7000f;
            darkGodEra.base_stats[S.armor] += 25f;
            darkGodEra.base_stats[S.health] += 500;
            darkGodEra.base_stats[S.speed] += 30;
            darkGodEra.base_stats[S.mass] += 0.6f;
            darkGodEra.base_stats[S.knockback] += 2f;
            darkGodEra.base_stats[S.attack_speed] += 8f;
            darkGodEra.path_icon = "ui/icons/godDark";
            darkGodEra.locale_description = "The world, shrouded in my domain";
            darkGodEra.locale_id = "Nights Prevail";
            darkGodEra.action_interval = 2;
            darkGodEra.action = new WorldAction(Traits.SuperRegeneration);
            localizeStatus(darkGodEra.id, "Nights Prevail", darkGodEra.locale_description); // Localizes the status effect
            AssetManager.status.add(darkGodEra);

            StatusAsset FireGodEra = new StatusAsset();
            FireGodEra.id = "God_Of_All";
            FireGodEra.duration = 7000f;
            FireGodEra.base_stats[S.armor] += 8;
            FireGodEra.base_stats[S.multiplier_health] = 0.55f;
            FireGodEra.base_stats[S.speed] += 70;
            FireGodEra.base_stats[S.mass] += 0.5f;
            FireGodEra.base_stats[S.multiplier_crit] = 0.5f;
            FireGodEra.base_stats[S.attack_speed] += 40f;
            FireGodEra.base_stats[S.scale] += 0.075f;
            FireGodEra.path_icon = "ui/icons/GodOfFire";
            FireGodEra.locale_description = "Now i have become death, destroyer of worlds";
            FireGodEra.locale_id = "God Of All";
            FireGodEra.action_interval = 2;
            FireGodEra.action = new WorldAction(Traits.SuperRegeneration);
            localizeStatus(FireGodEra.id, "God Of All", FireGodEra.locale_description); // Localizes the status effect
            AssetManager.status.add(FireGodEra);
            
            StatusAsset knowledgeGodEra = new StatusAsset();
            knowledgeGodEra.id = "Knowledge_Prevail";
            knowledgeGodEra.duration = 7000f;
            knowledgeGodEra.base_stats[S.armor] = 15;
            knowledgeGodEra.base_stats[S.health] += 500;
            knowledgeGodEra.base_stats[S.speed] += 20;
            knowledgeGodEra.base_stats[S.mass] += 0.9f;
            knowledgeGodEra.base_stats[S.knockback] += 1f;
            knowledgeGodEra.base_stats[S.attack_speed] += 20f;
            knowledgeGodEra.base_stats[S.intelligence] += 200f;
            knowledgeGodEra.base_stats[S.stewardship] += 20f;
            knowledgeGodEra.base_stats[S.range] += 40f;
            knowledgeGodEra.base_stats[S.accuracy] += 20f;
            knowledgeGodEra.path_icon = "ui/icons/knowledgeGod";
            knowledgeGodEra.locale_description = "The era of Knowledge has come to pass";
            knowledgeGodEra.locale_id = "Knowledge Prevails";
            knowledgeGodEra.action_interval = 2;
            knowledgeGodEra.action = new WorldAction(Traits.SuperRegeneration);
            localizeStatus(knowledgeGodEra.id, "Knowledge Prevails", knowledgeGodEra.locale_description); // Localizes the status effect
            AssetManager.status.add(knowledgeGodEra);
            
            StatusAsset LoveGodEra = new StatusAsset();
            LoveGodEra.id = "Love Prevails";
            LoveGodEra.duration = 7000f;
            LoveGodEra.base_stats[S.armor] = 8;
            LoveGodEra.base_stats[S.health] += 500;
            LoveGodEra.base_stats[S.speed] += 20;
            LoveGodEra.base_stats[S.mass] += 0.9f;
            LoveGodEra.base_stats[S.knockback] += 1f;
            LoveGodEra.base_stats[S.attack_speed] += 20f;
            LoveGodEra.base_stats[S.stewardship] += 20f;
            LoveGodEra.base_stats[S.range] += 10f;
            LoveGodEra.base_stats[S.accuracy] += 5;
            LoveGodEra.path_icon = "ui/icons/GodOfLove";
            LoveGodEra.locale_description = "The era of Love is here???";
            LoveGodEra.locale_id = "Love Prevails";
            LoveGodEra.action_interval = 2;
            LoveGodEra.action = new WorldAction(Traits.SuperRegeneration);
            localizeStatus(LoveGodEra.id, "Love Prevails", LoveGodEra.locale_description); // Localizes the status effect
            AssetManager.status.add(LoveGodEra);

            StatusAsset starsGodEra = new StatusAsset();
            starsGodEra.id = "Stars_Prevail";
            starsGodEra.duration = 7000f;
            starsGodEra.base_stats[S.health] += 500;
            starsGodEra.base_stats[S.multiplier_health] = 0.5f;
            starsGodEra.base_stats[S.speed] += 30;
            starsGodEra.base_stats[S.mass] += 0.8f;
            starsGodEra.base_stats[S.armor] += 10;
            starsGodEra.base_stats[S.attack_speed] += 80f;
            starsGodEra.path_icon = "ui/icons/starsGod";
            starsGodEra.locale_description = "The Age Of Stars is Apon Us";
            starsGodEra.locale_id = "Stars Prevail";
            starsGodEra.action_interval = 20;
            starsGodEra.action = new WorldAction(Traits.SuperRegeneration);
            localizeStatus(starsGodEra.id, "Stars Prevail", starsGodEra.locale_description); // Localizes the status effect
            AssetManager.status.add(starsGodEra);

            StatusAsset EarthGodEra = new StatusAsset();
            EarthGodEra.id = "Earth Prevails";
            EarthGodEra.duration = 7000f;
            EarthGodEra.base_stats[S.multiplier_health] += 0.8f;
            EarthGodEra.base_stats[S.multiplier_speed] += 0.25f;
            EarthGodEra.base_stats[S.mass] += 0.8f;
            EarthGodEra.base_stats[S.multiplier_damage] += 0.3f;
            EarthGodEra.base_stats[S.armor] += 15;
            EarthGodEra.base_stats[S.attack_speed] += 80f;
            EarthGodEra.path_icon = "ui/icons/earthGod";
            EarthGodEra.locale_description = "The World, shrowded in my home";
            EarthGodEra.locale_id = "Earth Prevails";
            EarthGodEra.action_interval = 2;
            EarthGodEra.action = new WorldAction(Traits.SuperRegeneration);
            localizeStatus(EarthGodEra.id, "Earth Prevails", EarthGodEra.locale_description); // Localizes the status effect
            AssetManager.status.add(EarthGodEra);

            StatusAsset warGodEra = new StatusAsset();
            warGodEra.id = "Despair Prevails";
            warGodEra.duration = 7000f;
            warGodEra.base_stats[S.armor] += 25;
            warGodEra.base_stats[S.health] += 500;
            warGodEra.base_stats[S.speed] += 30;
            warGodEra.base_stats[S.mass] += 0.2f;
            warGodEra.base_stats[S.knockback] += 2f;
            warGodEra.base_stats[S.attack_speed] += 8f;
            warGodEra.base_stats[S.damage] += 30f;
            warGodEra.path_icon = "ui/icons/warGod";
            warGodEra.locale_description = "There is Power in despair for some";
            warGodEra.locale_id = "Despair Prevails";
            warGodEra.action_interval = 2;
            warGodEra.action = new WorldAction(Traits.SuperRegeneration);
            localizeStatus(warGodEra.id, "Despair Prevails", warGodEra.locale_description); // Localizes the status effect
            AssetManager.status.add(warGodEra);

            StatusAsset lichgodera = new StatusAsset
            {
                id = "Sorrow Prevails",
                duration = 7000f,
            };
            lichgodera.base_stats[S.armor] += 25;
            lichgodera.base_stats[S.health] += 500;
            lichgodera.base_stats[S.speed] += 30;
            lichgodera.base_stats[S.mass] += 0.2f;
            lichgodera.base_stats[S.knockback] += 2f;
            lichgodera.base_stats[S.attack_speed] += 8f;
            lichgodera.base_stats[S.multiplier_damage] += 0.3f;
            lichgodera.path_icon = "ui/icons/lichGod";
            lichgodera.locale_description = "Sorrow is my Power";
            lichgodera.locale_id = "Sorrow Prevails";
            lichgodera.action_interval = 2;
            lichgodera.action = new WorldAction(Traits.SuperRegeneration);
            localizeStatus(lichgodera.id, "Sorrow Prevails", lichgodera.locale_description); // Localizes the status effect
            AssetManager.status.add(lichgodera);

            StatusAsset Invisible = new StatusAsset();
            Invisible.duration = 7000f;
            Invisible.id = "Invisible";
            Invisible.path_icon = "Actors/species/other/Godhunter/heads_male/walk_0";
            Invisible.locale_id = "Invisible";
            Invisible.locale_description = "you cant see me";
            Invisible.base_stats[S.speed] += 60;
            Invisible.base_stats[S.mass] -= 5;
            Invisible.action_interval = 0.5f;
            Invisible.action = new WorldAction(InvisibleEffect);
            Invisible.action_finish = FinishInvisibility;
            localizeStatus(Invisible.id, "Invisible", Invisible.locale_description); // Localizes the status effect
            AssetManager.status.add(Invisible);

            StatusAsset Lassering = new StatusAsset();
            Lassering.duration = 10;
            Lassering.id = "Lassering";
            Lassering.path_icon = "ui/icons/GodOfFire";
            Lassering.locale_id = "Lassering";
            Lassering.locale_description = "Unlimited power!!!!";
            Lassering.base_stats[S.multiplier_speed] = -0.75f;
            Lassering.action_interval = 0.01f;
            Lassering.action_on_receive = CreateLaserForActor;
            localizeStatus(Lassering.id, "Lassering", Lassering.locale_description); // Localizes the status effect
            AssetManager.status.add(Lassering);

            StatusAsset chaosgodsera = new StatusAsset();
            chaosgodsera.duration = 7000f;
            chaosgodsera.id = "Chaos Prevails";
            chaosgodsera.path_icon = "ui/icons/chaosGod";
            chaosgodsera.locale_id = "Chaos Prevails";
            chaosgodsera.locale_description = "Chaos Sooths My Soul";
            chaosgodsera.base_stats[S.armor] += 25;
            chaosgodsera.base_stats[S.health] += 500;
            chaosgodsera.base_stats[S.speed] += 30;
            chaosgodsera.base_stats[S.mass] += 0.2f;
            chaosgodsera.base_stats[S.knockback] += 2f;
            chaosgodsera.base_stats[S.attack_speed] += 8f;
            chaosgodsera.base_stats[S.damage] += 30f;
            chaosgodsera.base_stats[S.armor] = 10f;
            chaosgodsera.base_stats[S.multiplier_health] = 0.3f;
            chaosgodsera.action_interval = 2;
            chaosgodsera.action = new WorldAction(Traits.SuperRegeneration);
            AssetManager.status.add(chaosgodsera);
            localizeStatus(chaosgodsera.id, "Chaos Prevails", chaosgodsera.locale_description); // Localizes the status effect

            StatusAsset warGodsCry = new StatusAsset();
            warGodsCry.id = "WarGodsCry";
            warGodsCry.duration = 7000f;
            warGodsCry.base_stats[S.armor] += 25;
            warGodsCry.base_stats[S.multiplier_health] = 0.20f;
            warGodsCry.base_stats[S.speed] += 20;
            warGodsCry.base_stats[S.mass] += 0.8f;
            warGodsCry.base_stats[S.knockback] += 2f;
            warGodsCry.base_stats[S.multiplier_attack_speed] = 0.5f;
            warGodsCry.base_stats[S.damage] += 15f;
            warGodsCry.path_icon = "ui/icons/warGod";
            warGodsCry.locale_description = "A Cry Of Anger and Rage";
            warGodsCry.locale_id = "WarGodsCry";
            localizeStatus(warGodsCry.id, "WarGodsCry", warGodsCry.locale_description); // Localizes the status effect
            AssetManager.status.add(warGodsCry);

            AssetManager.status.add(chaosgodsera);
            StatusAsset SlamDunk = new StatusAsset();
            SlamDunk.id = "War Gods Slam";
            SlamDunk.duration = 7000f;
            SlamDunk.action_interval = 0.0001f;
            SlamDunk.action = new WorldAction(WarGodsSlam);
            SlamDunk.path_icon = "ui/icons/warGod";
            SlamDunk.locale_description = "its a bird! its a plane! its the GOD OF WAR!";
            SlamDunk.locale_id = "War Gods Slam";
            localizeStatus(SlamDunk.id, "War Gods Slam", SlamDunk.locale_description); // Localizes the status effect
            AssetManager.status.add(SlamDunk);

            StatusAsset ICANTSEE = new StatusAsset();
            ICANTSEE.id = "Blinded";
            ICANTSEE.duration = 7000f;
            ICANTSEE.base_stats[S.range] -= 10000;
            ICANTSEE.base_stats[S.critical_chance] = -1;
            ICANTSEE.base_stats[S.speed] -= 15;
            ICANTSEE.base_stats[S.mass] -= 0.6f;
            ICANTSEE.base_stats[S.damage] -= 30f;
            ICANTSEE.path_icon = "ui/icons/actor_traits/iconMadness";
            ICANTSEE.locale_description = "I CANNOT SEE AHHHHHHH!";
            ICANTSEE.locale_id = "Blinded";
            localizeStatus(ICANTSEE.id, "Blinded", ICANTSEE.locale_description); // Localizes the status effect
            AssetManager.status.add(ICANTSEE);
            
            StatusAsset Petrified = new StatusAsset();
            Petrified.id = "Petrified";
            Petrified.duration = 7000f;
            Petrified.base_stats[S.range] -= 10000;
            Petrified.base_stats[S.speed] -= 10000;
            Petrified.base_stats[S.mass] += 99999;
            Petrified.base_stats[S.damage] -= 99999;
            Petrified.path_icon = "ui/icons/iconResStone";
            Petrified.locale_description = "................";
            Petrified.action_interval = 0.5f;
            Petrified.base_stats.addTag("immovable");
            Petrified.base_stats.addTag("frozen_ai");
            Petrified.base_stats.addTag("stop_idle_animation");
            Petrified.action = new WorldAction(PetrifiedEffect);
            Petrified.locale_id = "Petrified";
            localizeStatus(Petrified.id, "Petrified", Petrified.locale_description); // Localizes the status effect
            AssetManager.status.add(Petrified);
            
            StatusAsset BrainWashed = new StatusAsset();
            BrainWashed.id = "BrainWashed";
            BrainWashed.duration = 20f;
            BrainWashed.render_priority = 8;
            BrainWashed.base_stats[S.speed] += 3;
            BrainWashed.base_stats[S.mass] -= 0.6f;
            BrainWashed.base_stats[S.attack_speed] += 3;
            BrainWashed.base_stats[S.damage] += 5f;
            BrainWashed.action_finish = FinishBrainWashing;
            BrainWashed.path_icon = "ui/icons/CorruptedOne";
            BrainWashed.locale_description = "Must.... Obey!";
            BrainWashed.locale_id = "BrainWashed";
            BrainWashed.animated = true;
            BrainWashed.texture = "projectiles/wordsOfKnowledgeProjectile";
            BrainWashed.sprite_list = Resources.LoadAll<Sprite>("effects/" + BrainWashed.texture);
            BrainWashed.random_frame = true;
            localizeStatus(BrainWashed.id, "BrainWashed", BrainWashed.locale_description); // Localizes the status effect
            AssetManager.status.add(BrainWashed);
            
            StatusAsset Levitating = new StatusAsset();
            Levitating.id = "Levitating";
            Levitating.duration = 5;
            Levitating.path_icon = "ui/icons/iconForce";
            Levitating.locale_description = "AHHHHHHHHHHHH!";
            Levitating.action_finish = LaunchToGround;
            Levitating.locale_id = "Levitating";
            Levitating.action_interval = 0.0000001f;
            Levitating.action = new WorldAction(LevitateEffect);
            localizeStatus(Levitating.id, "Levitating", Levitating.locale_description); // Localizes the status effect
            AssetManager.status.add(Levitating);


            /*
            ProjectileAsset Test = new ProjectileAsset();
            Usoppball.id = "Test";
		    Usoppball.texture = "effects/projectiles/Test";
		    Usoppball.trailEffect_enabled = false;
		    Usoppball.look_at_target = true;
            Usoppball.parabolic = false;
            Usoppball.hitShake = true;
		    Usoppball.looped = false;
		    Usoppball.speed = 1f;
            Usoppball.startScale = 0.1f;
            Usoppball.targetScale = 0.1f;
            AssetManager.projectiles.add(Usoppball);


            

            */

        }
        public static bool CreateLaserForActor(BaseSimObject baseSimObject, WorldTile Tile)
        {
            Actor pSelf = baseSimObject.a;
            if (!pSelf.has_attack_target)
            {
                pSelf.finishStatusEffect("Lassering");
                return false;
            }
            (EffectsLibrary.spawn("ChaosLaser") as ChaosLaser)?.Init(pSelf);
            return true;
        }
        public static bool LaunchToGround(BaseSimObject pTarget, WorldTile pTile)
        {
            Actor actor = pTarget.a;
            Actor Target = Traits.GetTargetToCrashLand(actor);
            if (Target != null)
            {
                actor.velocity.z = 0;
                Traits.PushActorTowardsTile(Target.current_tile.pos, actor, 0.3f);
                Target.getHit(80, true, AttackType.Other, null, false);
                actor.getHit(160, true, AttackType.Other, null, false);
            }
            return true;
        }
        public static bool FinishBrainWashing(BaseSimObject pself, WorldTile ptile)
        {
            Traits.FinishBrainWashing(pself.a);
            return true;
        }

        public static bool LevitateEffect(BaseSimObject pTarget, WorldTile pTile)
        {
            if(pTarget.position_height < 10)
            {
                pTarget.a.velocity.z = Traits.GetDampConstant(pTarget.a)/10;
                pTarget.a.velocity_speed = 3;
                pTarget.a.velocity.x = Randy.randomFloat(-3, 3);
                pTarget.a.velocity.y = Randy.randomFloat(-3, 3);
            }
            pTarget.a.velocity.x -= pTarget.a.velocity.x * 0.1f;
            pTarget.a.velocity.y -= pTarget.a.velocity.y * 0.1f;

            return true;
        }

        public static bool WarGodsSlam (BaseSimObject pTarget, WorldTile pTile)
        {
            if(pTarget.position_height == 0)
            {
                Traits.SpawnCustomWave(pTile.pos, 0.1f, 0.1f, 0.1f);
                MusicBox.playSound("event:/SFX/EXPLOSIONS/ExplosionBowlingBall", pTile.x, pTile.y);
                MapAction.damageWorld(pTile, 7, AssetManager.terraform.get("grenade"), pTarget);
                pTarget.a.restoreHealth(50);
                pTarget.finishStatusEffect("War Gods Slam");
            }
            return true;
        }
        public static void localizeStatus(string id, string name, string description)
        {
            Dictionary<string, string> localizedText = LocalizedTextManager.instance._localized_text;
            localizedText.Add(name, id);
            localizedText.Add(description, description);
        }
        public static bool FinishInvisibility(BaseSimObject pTarget, WorldTile pTile)
        {
            pTarget.a.color = new Color(1, 1, 1, 1);
            return true;
        }
        public static bool InvisibleEffect(BaseSimObject pTarget, WorldTile pTile)
        {
            Color mycolor = pTarget.a.color;
                if (mycolor.r != 0.8)
                {
                    pTarget.a.restoreHealth(pTarget.a.getMaxHealth());
                    pTarget.a.color = new Color(0.8f, mycolor.g, mycolor.b, 0.4f);
                }
            return true;
        }
        public static bool PetrifiedEffect(BaseSimObject pTarget, WorldTile pTile)
        {
            Color mycolor = pTarget.a.color;
            if (mycolor.r != 0.2 || mycolor.g == 0.4f)
            {
                pTarget.a.color = new Color(0.2f, 0.4f, 0.4f, mycolor.a);
            }
            if (Randy.randomChance(0.25f))
            {
                pTarget.getHit(pTarget.getMaxHealth() * .1f, true, AttackType.Other, null, false);
            }
            if (Randy.randomChance(0.1f))
            {
                DropsLibrary.action_spawn_building(pTile, "stone");
            }
            return true;
        }
    }

}

/* 
AUTHOR: MASON SCARBRO
VERSION: 1.0.0
*/
using System.Collections.Generic;
using UnityEngine;

namespace GodsAndPantheons 
{
    class NewProjectiles : MonoBehaviour
    {
        private static List<BaseSimObject> enemyObjectsList = new List<BaseSimObject>();

        public static void init()
        {
            loadProjectiles();
        }

        private static void loadProjectiles()
        {
            AssetManager.projectiles.add(new ProjectileAsset
            {
                id = "WarAxeProjectile1",
                speed = 7f,
                texture = "warAxeProjectile",
                trailEffect_enabled = false,
                texture_shadow = "shadow_ball",
                endEffect = string.Empty,
                terraformRange = 1,
                draw_light_area = true,
                draw_light_size = 0.1f,
                parabolic = true,
                sound_launch = "event:/SFX/WEAPONS/WeaponFireballStart",
                // sound_impact = "event:/SFX/WEAPONS/WeaponFireballLand",
                startScale = 0.1f,
                targetScale = 0.1f,
                
                /*impact_actions = new AttackAction(delegate(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
                {
                    return fireAttributeImpact(pSelf, pTarget, pTile, 0.15f, 0, 0, 2);
                })*/
            });

            AssetManager.projectiles.add(new ProjectileAsset
            {
                id = "CorruptedHeart",
                speed = 4f,
                animation_speed = 0.2f,
                texture = "CorruptedHeart",
                trailEffect_enabled = false,
                texture_shadow = "shadow_ball",
                endEffect = string.Empty,
                draw_light_area = true,
                draw_light_size = 0.1f,
                parabolic = false,
                sound_launch = "event:/SFX/WEAPONS/WeaponFireballStart",
                sound_impact = "event:/SFX/WEAPONS/WeaponRedOrbLand",
                startScale = 0.1f,
                targetScale = 0.1f,
                impact_actions = new AttackAction(delegate (BaseSimObject pSelf, BaseSimObject useless, WorldTile pTile)
                {
                    if(pSelf == null)
                    {
                        return false;
                    }
                    World.world.getObjectsInChunks(pTile, 3, MapObjectType.Actor);
                    foreach (BaseSimObject pTarget in World.world.temp_map_objects)
                    {
                        if (pTarget.kingdom.isEnemy(pSelf.kingdom))
                        {
                            if (Toolbox.randomChance(Traits.GetEnhancedChance("God Of Love", "Poisoning%", 0, 1, 25)))
                            {
                                pTarget.addStatusEffect("ash_fever", 15);
                            }
                            if (Toolbox.randomChance(Traits.GetEnhancedChance("God Of Love", "Poisoning%", 0, 1, 25)))
                            {
                                pTarget.addStatusEffect("poisoned", 30);
                            }
                            if (Toolbox.randomChance(Traits.GetEnhancedChance("God Of Love", "Poisoning%", 0, 1, 25)))
                            {
                                pTarget.addStatusEffect("cough", 60);
                            }
                            if(pTarget.isActor() && !Traits.IsGod(pTarget.a) && Toolbox.randomChance(Traits.GetEnhancedChance("God Of Love", "Petrification%")))
                            {
                                MusicBox.playSound("event:/SFX/DROPS/DropStone");
                                pTarget.addStatusEffect("Petrified");
                                Effects.PetrifiedEffect(pTarget, pTarget.currentTile);
                            }
                        }
                    }
                    return true;
                })
            });
            AssetManager.projectiles.add(new ProjectileAsset
            {
                id = "Heart",
                speed = 6f,
                animation_speed = 0.2f,
                texture = "Heart",
                trailEffect_enabled = false,
                texture_shadow = "shadow_ball",
                endEffect = string.Empty,
                draw_light_area = true,
                draw_light_size = 0.1f,
                parabolic = false,
                sound_launch = "event:/SFX/WEAPONS/WeaponFireballStart",
                sound_impact = "event:/SFX/WEAPONS/WeaponRedOrbLand",
                startScale = 0.05f,
                targetScale = 0.05f,
                impact_actions = new AttackAction(delegate (BaseSimObject pSelf, BaseSimObject useless, WorldTile pTile)
                {
                    if (pSelf == null)
                    {
                        return false;
                    }
                    World.world.getObjectsInChunks(pTile, 3, MapObjectType.Actor);
                    foreach (BaseSimObject pTarget in World.world.temp_map_objects)
                    {
                        if (pTarget.kingdom == pSelf.kingdom)
                        {
                            Traits.SuperRegeneration(pTarget, 100f, 10f);
                        }
                    }
                    return true;
                })
            });

            AssetManager.projectiles.add(new ProjectileAsset
            {
                id = "DarkDaggersProjectiles",
                speed = 10f,
                texture = "darkDaggersProjectile",
                trailEffect_enabled = false,
                texture_shadow = "shadow_ball",
                endEffect = string.Empty,
                terraformOption = "darkDaggers",
                terraformRange = 1,
                draw_light_area = true,
                looped = true,
                draw_light_size = 0.1f,
                look_at_target = true,
                parabolic = false,
                sound_launch = "event:/SFX/WEAPONS/WeaponFireballStart",
                // sound_impact = "event:/SFX/WEAPONS/WeaponFireballLand",
                startScale = 0.01f,
                targetScale = 0.01f,
                animation_speed = 0.08f,
                /*
                impact_actions = new AttackAction(delegate(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
                {
                    return Impact(pSelf, pTarget, pTile);
                })*/
            });

            AssetManager.projectiles.add(new ProjectileAsset
            {
                id = "boneFire",
                speed = 10f,
                texture = "chaosProjectile",
                trailEffect_enabled = false,
                texture_shadow = "shadow_ball",
                endEffect = string.Empty,
                terraformOption = "chaosBoneFire",
                terraformRange = 1,
                draw_light_area = true,
                looped = true,
                draw_light_size = 0.1f,
                parabolic = false,
                sound_launch = "event:/SFX/WEAPONS/WeaponFireballStart",
                // sound_impact = "event:/SFX/WEAPONS/WeaponFireballLand",
                startScale = 0.08f,
                targetScale = 0.1f,
                /*
                impact_actions = new AttackAction(delegate(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
                {
                    return Impact(pSelf, pTarget, pTile);
                })*/
            });

            AssetManager.projectiles.add(new ProjectileAsset
            {
                id = "fireBallX",
                speed = 10f,
                texture = "fireBallProjectile",
                trailEffect_enabled = false,
                texture_shadow = "shadow_ball",
                endEffect = string.Empty,
                terraformOption = "chaosBoneFire",
                terraformRange = 1,
                draw_light_area = true,
                looped = true,
                draw_light_size = 0.1f,
                parabolic = false,
                sound_launch = "event:/SFX/WEAPONS/WeaponFireballStart",
                // sound_impact = "event:/SFX/WEAPONS/WeaponFireballLand",
                startScale = 0.08f,
                targetScale = 0.1f,
                /*
                impact_actions = new AttackAction(delegate(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
                {
                    return Impact(pSelf, pTarget, pTile);
                })*/
            });

            
            AssetManager.projectiles.add(new ProjectileAsset
            {
                id = "moonFall",
                speed = 30f,
                texture = "moonProjectile",
                trailEffect_enabled = false,
                texture_shadow = "shadow_ball",
                endEffect = string.Empty,
                terraformOption = "moonFalling",
                terraformRange = 5,
                draw_light_area = true,
                looped = true,
                draw_light_size = 0.1f,
                parabolic = false,
                sound_launch = "event:/SFX/WEAPONS/WeaponFireballStart",
                sound_impact = "event:/SFX/WEAPONS/WeaponFireballLand",
                startScale = 0.1f,
                targetScale = 0.1f,
                /*
                impact_actions = new AttackAction(delegate(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
                {
                    return Impact(pSelf, pTarget, pTile);
                })*/
            });
            AssetManager.projectiles.clone("moonFallSlow", "moonFall").speed = 10;

            AssetManager.projectiles.add(new ProjectileAsset
            {
                id = "PagesOfKnowledge",
                speed = 10f,
                texture = "pagesProjectile",
                trailEffect_enabled = false,
                texture_shadow = "shadow_ball",
                endEffect = string.Empty,
                terraformOption = "darkDaggers",
                terraformRange = 1,
                draw_light_area = true,
                looped = true,
                draw_light_size = 0.1f,
                parabolic = false,
                sound_launch = "event:/SFX/WEAPONS/WeaponFireballStart",
                // sound_impact = "event:/SFX/WEAPONS/WeaponFireballLand",
                startScale = 0.04f,
                targetScale = 0.04f,
                /*
                impact_actions = new AttackAction(delegate(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
                {
                    return Impact(pSelf, pTarget, pTile);
                })*/
            });

            AssetManager.projectiles.add(new ProjectileAsset
            {
                id = "lightBallzProjectiles",
                speed = 5f,
                texture = "lightBallzProjectile",
                trailEffect_enabled = false,
                texture_shadow = "shadow_ball",
                endEffect = string.Empty,
                terraformOption = "lightBallz",
                terraformRange = 4,
                draw_light_area = true,
                draw_light_size = 0.1f,
                looped = false,
                parabolic = false,
                sound_launch = "event:/SFX/WEAPONS/WeaponFireballStart",
                // sound_impact = "event:/SFX/WEAPONS/WeaponFireballLand",
                startScale = 0.03f,
                targetScale = 0.03f,
                /*
                impact_actions = new AttackAction(delegate(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
                {
                    return Impact(pSelf, pTarget, pTile);
                })*/
            });

            AssetManager.projectiles.add(new ProjectileAsset
            {
                id = "lightSlashesProjectile",
                animation_speed = 0.09f,
                speed = 3f,
                texture = "lightSlashesProjectile",
                trailEffect_enabled = false,
                texture_shadow = "shadow_ball",
                endEffect = string.Empty,
                terraformOption = string.Empty,
                terraformRange = 1,
                draw_light_area = true,
                draw_light_size = 0.1f,
                looped = false,
                parabolic = false,
                sound_launch = "event:/SFX/WEAPONS/WeaponFireballStart",
                // sound_impact = "event:/SFX/WEAPONS/WeaponFireballLand",
                startScale = 0.1f,
                targetScale = 0.1f,
                look_at_target = true,

                /*
                impact_actions = new AttackAction(delegate(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
                {
                    return Impact(pSelf, pTarget, pTile);
                })*/
            });


            AssetManager.projectiles.add(new ProjectileAsset
            {
                id = "starShowerProjectile",
                animation_speed = 0.1f,
                speed = 5f,
                texture = "starShowerProjectile",
                trailEffect_enabled = false,
                texture_shadow = "shadow_ball",
                endEffect = string.Empty,
                terraformOption = string.Empty,
                terraformRange = 1,
                draw_light_area = true,
                draw_light_size = 0.1f,
                looped = false,
                parabolic = false,
                sound_launch = "event:/SFX/WEAPONS/WeaponFireballStart",
                // sound_impact = "event:/SFX/WEAPONS/WeaponFireballLand",
                startScale = 0.1f,
                targetScale = 0.1f,
                look_at_target = true,

                /*
                impact_actions = new AttackAction(delegate(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
                {
                    return Impact(pSelf, pTarget, pTile);
                })*/
            });


            AssetManager.projectiles.add(new ProjectileAsset
            {
                id = "wordsOfKnowledgeProjectile",
                animation_speed = 0.2f,
                speed = 4f,
                texture = "wordsOfKnowledgeProjectile",
                trailEffect_enabled = false,
                texture_shadow = "shadow_ball",
                endEffect = string.Empty,
                terraformOption = string.Empty,
                terraformRange = 1,
                draw_light_area = true,
                draw_light_size = 0.1f,
                looped = true,
                parabolic = false,
                sound_launch = "event:/SFX/WEAPONS/WeaponFireballStart",
                // sound_impact = "event:/SFX/WEAPONS/WeaponFireballLand",
                startScale = 0.1f,
                targetScale = 0.1f,
                look_at_target = true,

                /*
                impact_actions = new AttackAction(delegate(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
                {
                    return Impact(pSelf, pTarget, pTile);
                })*/
            });

            AssetManager.projectiles.add(new ProjectileAsset
            {
                id = "waveOfMutilationProjectile",
                animation_speed = 0.3f,
                speed = 5f,
                texture = "waveOfMutilationProjectile",
                trailEffect_enabled = true,
                texture_shadow = "shadow_ball",
                endEffect = string.Empty,
                terraformOption = "waveTerra",
                terraformRange = 2,
                draw_light_area = true,
                draw_light_size = 0.1f,
                looped = true,
                parabolic = false,
                sound_launch = "event:/SFX/WEAPONS/WeaponFireballStart",
                // sound_impact = "event:/SFX/WEAPONS/WeaponFireballLand",
                startScale = 0.08f,
                targetScale = 0.08f,
                look_at_target = true,

                /*
                impact_actions = new AttackAction(delegate(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
                {
                    return Impact(pSelf, pTarget, pTile);
                })*/
            });

            /*private static bool Impact(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {

            if (pTarget != null)
            { 
                
                    EffectsLibrary.spawn("fx_napalm_flash", pTarget.a.currentTile.neighbours[2].neighbours[2].neighbours[1].neighbours[1].neighbours[2], null, null, 0f, -1f, -1f);
                    pSelf.a.addStatusEffect("invincible", 5f);
                return true;
            }
            return false;

        }*/

        }

        
    }
}
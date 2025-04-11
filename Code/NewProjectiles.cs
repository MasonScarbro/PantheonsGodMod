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
                speed = 21f,
                texture = "warAxeProjectile",
                trail_effect_enabled = false,
                texture_shadow = "shadows/projectiles/shadow_arrow",
                end_effect = string.Empty,
                terraform_range = 1,
                draw_light_area = true,
                draw_light_size = 0.1f,
                sound_launch = "event:/SFX/WEAPONS/WeaponFireballStart",
                // sound_impact = "event:/SFX/WEAPONS/WeaponFireballLand",
                scale_start = 0.1f,
                scale_target = 0.1f,
                
                /*impact_actions = new AttackAction(delegate(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
                {
                    return fireAttributeImpact(pSelf, pTarget, pTile, 0.15f, 0, 0, 2);
                })*/
            });

            AssetManager.projectiles.add(new ProjectileAsset
            {
                id = "CorruptedHeart",
                speed = 12f,
                animation_speed = 0.2f,
                texture = "CorruptedHeart",
                trail_effect_enabled = false,
                texture_shadow = "shadows/projectiles/shadow_snowball",
                end_effect = string.Empty,
                draw_light_area = true,
                draw_light_size = 0.1f,
                sound_launch = "event:/SFX/WEAPONS/WeaponFireballStart",
                sound_impact = "event:/SFX/WEAPONS/WeaponRedOrbLand",
                scale_start = 0.1f,
                scale_target = 0.1f,
                look_at_target = true,
                impact_actions = new AttackAction(delegate (BaseSimObject pSelf, BaseSimObject useless, WorldTile pTile)
                {
                    if(pSelf == null)
                    {
                        return false;
                    }
                    foreach (BaseSimObject pTarget in Finder.getUnitsFromChunk(pTile, 1, 3))
                    {
                        if (pTarget.kingdom.isEnemy(pSelf.kingdom))
                        {
                            if (Randy.randomChance(Traits.GetEnhancedChance("God Of Love", "Poisoning%", 25)))
                            {
                                pTarget.addStatusEffect("ash_fever", 15);
                            }
                            if (Randy.randomChance(Traits.GetEnhancedChance("God Of Love", "Poisoning%", 25)))
                            {
                                pTarget.addStatusEffect("poisoned", 30);
                            }
                            if (Randy.randomChance(Traits.GetEnhancedChance("God Of Love", "Poisoning%", 25)))
                            {
                                pTarget.addStatusEffect("cough", 60);
                            }
                            if(pTarget.isActor() && !Traits.IsGod(pTarget.a) && Randy.randomChance(Traits.GetEnhancedChance("God Of Love", "Petrification%")))
                            {
                                MusicBox.playSound("event:/SFX/DROPS/DropStone");
                                pTarget.addStatusEffect("Petrified");
                                Effects.PetrifiedEffect(pTarget, pTarget.current_tile);
                            }
                        }
                    }
                    return true;
                })
            });
            AssetManager.projectiles.add(new ProjectileAsset
            {
                id = "Heart",
                speed = 18f,
                animation_speed = 0.2f,
                texture = "Heart",
                trail_effect_enabled = false,
                texture_shadow = "shadows/projectiles/shadow_snowball",
                end_effect = string.Empty,
                draw_light_area = true,
                draw_light_size = 0.1f,
                sound_launch = "event:/SFX/WEAPONS/WeaponFireballStart",
                sound_impact = "event:/SFX/WEAPONS/WeaponRedOrbLand",
                scale_start = 0.05f,
                look_at_target = true,
                scale_target = 0.05f,
                impact_actions = new AttackAction(delegate (BaseSimObject pSelf, BaseSimObject useless, WorldTile pTile)
                {
                    if (pSelf == null)
                    {
                        return false;
                    }
                    foreach (BaseSimObject pTarget in Finder.getUnitsFromChunk(pTile, 1, 3))
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
                speed = 30f,
                texture = "darkDaggersProjectile",
                trail_effect_enabled = false,
                texture_shadow = "shadows/projectiles/shadow_arrow",
                end_effect = string.Empty,
                terraform_option = "darkDaggers",
                terraform_range = 1,
                draw_light_area = true,
                draw_light_size = 0.1f,
                look_at_target = true,
                sound_launch = "event:/SFX/WEAPONS/WeaponFireballStart",
                // sound_impact = "event:/SFX/WEAPONS/WeaponFireballLand",
                scale_start = 0.01f,
                scale_target = 0.01f,
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
                speed = 30f,
                texture = "chaosProjectile",
                trail_effect_enabled = false,
                texture_shadow = "shadows/projectiles/shadow_ball",
                end_effect = string.Empty,
                terraform_option = "chaosBoneFire",
                terraform_range = 1,
                draw_light_area = true,
                draw_light_size = 0.1f,
                sound_launch = "event:/SFX/WEAPONS/WeaponFireballStart",
                // sound_impact = "event:/SFX/WEAPONS/WeaponFireballLand",
                scale_start = 0.08f,
                scale_target = 0.1f,
                /*
                impact_actions = new AttackAction(delegate(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
                {
                    return Impact(pSelf, pTarget, pTile);
                })*/
            });

            AssetManager.projectiles.add(new ProjectileAsset
            {
                id = "fireBallX",
                speed = 30f,
                texture = "fireBallProjectile",
                trail_effect_enabled = false,
                texture_shadow = "shadows/projectiles/shadow_ball",
                end_effect = string.Empty,
                terraform_option = "chaosBoneFire",
                terraform_range = 1,
                draw_light_area = true,
                draw_light_size = 0.1f,
                sound_launch = "event:/SFX/WEAPONS/WeaponFireballStart",
                // sound_impact = "event:/SFX/WEAPONS/WeaponFireballLand",
                scale_start = 0.08f,
                scale_target = 0.1f,
                /*
                impact_actions = new AttackAction(delegate(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
                {
                    return Impact(pSelf, pTarget, pTile);
                })*/
            });

            
            AssetManager.projectiles.add(new ProjectileAsset
            {
                id = "moonFall",
                speed = 90f,
                texture = "moonProjectile",
                trail_effect_enabled = false,
                texture_shadow = "shadows/projectiles/shadow_snowball",
                end_effect = string.Empty,
                terraform_option = "moonFalling",
                terraform_range = 5,
                draw_light_area = true,
                draw_light_size = 0.1f,
                sound_launch = "event:/SFX/WEAPONS/WeaponFireballStart",
                sound_impact = "event:/SFX/WEAPONS/WeaponFireballLand",
                scale_start = 0.1f,
                scale_target = 0.1f,
                /*
                impact_actions = new AttackAction(delegate(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
                {
                    return Impact(pSelf, pTarget, pTile);
                })*/
            });
            AssetManager.projectiles.clone("moonFallSlow", "moonFall").speed = 30f;

            AssetManager.projectiles.add(new ProjectileAsset
            {
                id = "PagesOfKnowledge",
                speed = 30f,
                texture = "pagesProjectile",
                trail_effect_enabled = false,
                texture_shadow = "shadows/projectiles/shadow_arrow",
                end_effect = string.Empty,
                terraform_option = "darkDaggers",
                terraform_range = 1,
                draw_light_area = true,
                draw_light_size = 0.1f,
                sound_launch = "event:/SFX/WEAPONS/WeaponFireballStart",
                // sound_impact = "event:/SFX/WEAPONS/WeaponFireballLand",
                scale_start = 0.04f,
                scale_target = 0.04f,
                /*
                impact_actions = new AttackAction(delegate(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
                {
                    return Impact(pSelf, pTarget, pTile);
                })*/
            });

            AssetManager.projectiles.add(new ProjectileAsset
            {
                id = "lightBallzProjectiles",
                speed = 20f,
                texture = "lightBallzProjectile",
                trail_effect_enabled = false,
                texture_shadow = "shadows/projectiles/shadow_arrow",
                end_effect = string.Empty,
                terraform_option = "lightBallz",
                terraform_range = 4,
                draw_light_area = true,
                draw_light_size = 0.1f,
                sound_launch = "event:/SFX/WEAPONS/WeaponFireballStart",
                // sound_impact = "event:/SFX/WEAPONS/WeaponFireballLand",
                scale_start = 0.03f,
                scale_target = 0.03f,
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
                speed = 9f,
                texture = "lightSlashesProjectile",
                trail_effect_enabled = false,
                texture_shadow = "shadows/projectiles/shadow_arrow",
                end_effect = string.Empty,
                terraform_option = string.Empty,
                terraform_range = 1,
                draw_light_area = true,
                draw_light_size = 0.1f,
                sound_launch = "event:/SFX/WEAPONS/WeaponFireballStart",
                // sound_impact = "event:/SFX/WEAPONS/WeaponFireballLand",
                scale_start = 0.1f,
                scale_target = 0.1f,
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
                speed = 15f,
                texture = "starShowerProjectile",
                trail_effect_enabled = false,
                texture_shadow = "shadows/projectiles/shadow_ball",
                end_effect = string.Empty,
                terraform_option = string.Empty,
                terraform_range = 1,
                draw_light_area = true,
                draw_light_size = 0.1f,
                sound_launch = "event:/SFX/WEAPONS/WeaponFireballStart",
                // sound_impact = "event:/SFX/WEAPONS/WeaponFireballLand",
                scale_start = 0.1f,
                scale_target = 0.1f,
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
                speed = 12f,
                texture = "wordsOfKnowledgeProjectile",
                trail_effect_enabled = false,
                texture_shadow = "shadows/projectiles/shadow_ball",
                end_effect = string.Empty,
                terraform_option = string.Empty,
                terraform_range = 1,
                draw_light_area = true,
                draw_light_size = 0.1f,
                sound_launch = "event:/SFX/WEAPONS/WeaponFireballStart",
                // sound_impact = "event:/SFX/WEAPONS/WeaponFireballLand",
                scale_start = 0.1f,
                scale_target = 0.1f,
                look_at_target = true,

                /*
                impact_actions = new AttackAction(delegate(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
                {
                    return Impact(pSelf, pTarget, pTile);
                })*/
            });

            AssetManager.projectiles.add(new ProjectileAsset
            {
                id = "EarthShardProjectile",
                speed = 24f,
                texture = "EarthShardProjectile",
                trail_effect_enabled = false,
                texture_shadow = "shadows/projectiles/shadow_arrow",
                end_effect = string.Empty,
                terraform_option = string.Empty,
                terraform_range = 1,
                draw_light_area = true,
                draw_light_size = 0.1f,
                sound_launch = "event:/SFX/WEAPONS/WeaponFireballStart",
                // sound_impact = "event:/SFX/WEAPONS/WeaponFireballLand",
                scale_start = 0.1f,
                scale_target = 0.1f,
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
                speed = 15f,
                texture = "waveOfMutilationProjectile",
                trail_effect_enabled = true,
                texture_shadow = "shadows/projectiles/shadow_ball",
                end_effect = string.Empty,
                terraform_option = "waveTerra",
                terraform_range = 2,
                draw_light_area = true,
                draw_light_size = 0.1f,
                sound_launch = "event:/SFX/WEAPONS/WeaponFireballStart",
                // sound_impact = "event:/SFX/WEAPONS/WeaponFireballLand",
                scale_start = 0.08f,
                scale_target = 0.08f,
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
                
                    EffectsLibrary.spawn("fx_napalm_flash", pTarget.a.current_tile.neighbours[2].neighbours[2].neighbours[1].neighbours[1].neighbours[2], null, null, 0f, -1f, -1f);
                    pSelf.a.addStatusEffect("invincible", 5f);
                return true;
            }
            return false;

        }*/

        }

        
    }
}
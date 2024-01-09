/* 
AUTHOR: MASON SCARBRO
VERSION: 1.0.0
*/
using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NCMS;
using NCMS.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ReflectionUtility;

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
                id = "BlackHoleProjectile1",
                speed = 0.5f,
                texture = "blackHoleProjectile",
                trailEffect_enabled = false,
                texture_shadow = "shadow_ball",
                endEffect = string.Empty,
                terraformOption = "BlackHole",
                terraformRange = 4,
                draw_light_area = true,
                draw_light_size = 0.1f,
                parabolic = false,
                sound_launch = "event:/SFX/WEAPONS/WeaponFireballStart",
                // sound_impact = "event:/SFX/WEAPONS/WeaponFireballLand",
                startScale = 0.2f,
                targetScale = 0.2f,
                /*
                impact_actions = new AttackAction(delegate(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
                {
                    return Impact(pSelf, pTarget, pTile);
                })*/
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
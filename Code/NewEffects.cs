/* 
AUTHOR: MASON SCARBRO
VERSION: 1.0.0
*/
using GodsAndPantheons.CustomEffects;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
namespace GodsAndPantheons
{
    class NewEffects : MonoBehaviour
    {
        public static void init()
        {
            loadEffects();
        }

        private static void loadEffects()
        {
            /*var effect1 = AssetManager.effects_library.add(new EffectAsset
            {
                id = "fx_multiFlash_dej",
                use_basic_prefab = true,
                sorting_layer_id = "EffectsBack",
                sprite_path = "effects/fx_multiFlash_dej",
                show_on_mini_map = true,
                use_basic_prefab = true,
                limit = 100,
                draw_light_area = true,
                draw_light_size = 2f,
                draw_light_area_offset_y = 0f
            });*/
            //World.world.stackEffects.CallMethod("add", effect1);
            AssetManager.effects_library.add(new EffectAsset
            {
                id = "fx_smokeFlash_dej",
                use_basic_prefab = true,
                sorting_layer_id = "EffectsBack",
                sprite_path = "effects/fx_smokeFlash_dej",
                show_on_mini_map = true,
                limit = 100,
                draw_light_area = true,
                draw_light_size = 2f,
                draw_light_area_offset_y = 0f,
                time_between_frames = 0.08f
            });
            AssetManager.effects_library.add(new EffectAsset
            {
                id = "fx_BlackHole",
                use_basic_prefab = false,
                prefab_id = "effects/prefabs/BlackHole",
                show_on_mini_map  = true,
                draw_light_area = true,
                draw_light_size = 2f,
                draw_light_area_offset_y = 0f,
                sound_launch = "event:/SFX/WEAPONS/WeaponFireballStart",
            });

            AssetManager.effects_library.add(new EffectAsset
            {
                id = "fx_CloudOfDarkness",
                use_basic_prefab = false,
                prefab_id = "effects/prefabs/CloudOfDarkness",
                show_on_mini_map = true,
                draw_light_area = true,
                draw_light_size = 2f,
                draw_light_area_offset_y = 0f,
                sound_launch = "event:/SFX/WEAPONS/WeaponFireballStart",
            });

            AssetManager.effects_library.add(new EffectAsset
            {
                id = "fx_custom_explosion_wave",
                use_basic_prefab = false,
                prefab_id = "effects/prefabs/CustomExplosionWave",
                show_on_mini_map = true
            });

            AssetManager.effects_library.add(new EffectAsset
            {
                id = "fx_cometAzureDown_dej",
                use_basic_prefab = true,
                sorting_layer_id = "EffectsBack",
                sprite_path = "effects/fx_cometAzureDown_dej",
                show_on_mini_map = true,
                limit = 100,
                draw_light_area = true,
                draw_light_size = 2f,
                draw_light_area_offset_y = 0f,
                sound_launch = "event:/SFX/EXPLOSIONS/ExplosionAntimatterBomb",
                time_between_frames = 0.08f,
            });

            AssetManager.effects_library.add(new EffectAsset
            {
                id = "fx_cometShower_dej",
                use_basic_prefab = true,
                sorting_layer_id = "EffectsBack",
                sprite_path = "effects/fx_cometShower_dej",
                show_on_mini_map = true,
                limit = 100,
                draw_light_area = true,
                draw_light_size = 2f,
                draw_light_area_offset_y = 0f,
                time_between_frames = 0.09f
            });

            AssetManager.effects_library.add(new EffectAsset
            {
                id = "fx_teleportStart_dej",
                use_basic_prefab = true,
                sorting_layer_id = "EffectsBack",
                sprite_path = "effects/fx_teleportStart_dej",
                show_on_mini_map = true,
                limit = 100,
                draw_light_area = true,
                draw_light_size = 2f,
                draw_light_area_offset_y = 0f,
                time_between_frames = 0.06f
            });

            AssetManager.effects_library.add(new EffectAsset
            {
                id = "fx_teleportEnd_dej",
                use_basic_prefab = true,
                sorting_layer_id = "EffectsBack",
                sprite_path = "effects/fx_teleportEnd_dej",
                show_on_mini_map = true,
                limit = 100,
                draw_light_area = true,
                draw_light_size = 2f,
                draw_light_area_offset_y = 0f,
                time_between_frames = 0.08f
            });

            AssetManager.effects_library.add(new EffectAsset
            {
                id = "fx_handgrab_dej",
                use_basic_prefab = true,
                sorting_layer_id = "EffectsBack",
                sprite_path = "effects/fx_handgrab_dej",
                show_on_mini_map = true,
                limit = 100,
                draw_light_area = true,
                draw_light_size = 2f,
                draw_light_area_offset_y = 0f,
                time_between_frames = 0.1f
            });

            AssetManager.effects_library.add(new EffectAsset
            {
                id = "fx_Heart",
                use_basic_prefab = false,
                prefab_id = "effects/prefabs/Heart",
                sound_launch = "event:/SFX/WEAPONS/WeaponFireballStart",
                show_on_mini_map = true,
                draw_light_area = true,
                draw_light_size = 2f,
                draw_light_area_offset_y = 0f,
            });
            AssetManager.effects_library.clone("fx_Heart_Corrupted", "fx_Heart").prefab_id = "effects/prefabs/CorruptedHeart";
            
            AssetManager.effects_library.add(new EffectAsset
            {
                id = "fx_Moon_Orbit",
                use_basic_prefab = false,
                prefab_id = "effects/prefabs/Moon",
                show_on_mini_map = true,
                draw_light_area = true,
                draw_light_size = 2f,
                draw_light_area_offset_y = 0f,
            });
            
            AssetManager.effects_library.add(new EffectAsset
            {
                id = "fx_Build_Path",
                use_basic_prefab = false,
                prefab_id = "effects/prefabs/TerraformPath",
                show_on_mini_map = true,
                draw_light_area = true,
                draw_light_size = 2f,
                draw_light_area_offset_y = 0f,
            });

            AssetManager.effects_library.add(new EffectAsset
            {
                id = "fx_Stalagmite_path",
                use_basic_prefab = false,
                prefab_id = "effects/prefabs/StalagmitePath",
                show_on_mini_map = true,
                draw_light_area = true,
                draw_light_size = 2f,
                draw_light_area_offset_y = 0f,
            });
            AssetManager.effects_library.add(new EffectAsset
            {
                id = "Stalagmite",
                use_basic_prefab = true,
                sorting_layer_id = "EffectsTop",
                sprite_path = "effects/fx_Stalagmite",
                time_between_frames = 0.05f,
                spawn_action = (BaseEffect pEffect, WorldTile pTile, string _, string __, float ___, Actor ____) =>
                {
                    pEffect.GetComponent<SpriteRenderer>().color = pTile.Type.color;
                    return null;
                }
            });

            AssetManager.effects_library.add(new EffectAsset
            {
                id = "fx_Pull_Rock",
                use_basic_prefab = false,
                prefab_id = "effects/prefabs/PulledRock",
                show_on_mini_map = true,
                draw_light_area = true,
                draw_light_size = 2f,
                draw_light_area_offset_y = 0f,
            });

            AssetManager.effects_library.add(new EffectAsset
            {
                id = "ChaosLaser",
                use_basic_prefab = false,
                prefab_id = "effects/prefabs/ChaosLaser",
                limit = 0
            });

            AssetManager.effects_library.clone("FireTornado", "fx_tornado").prefab_id = "effects/prefabs/FireTornado";

            EffectAsset FireBomb = AssetManager.effects_library.clone("FireGodsExplsion", "fx_firebomb_explosion");
            FireBomb.sprite_path = "effects/FireExplosion";
            FireBomb.time_between_frames = 0.1f;

            AssetManager.effects_library.add(new EffectAsset
            {
                id = "FireBreath",
                use_basic_prefab = true,
                sprite_path = "effects/fx_FireBreath",
                sorting_layer_id = "EffectsTop",
                draw_light_area = true,
                draw_light_size = 4,
                spawn_action = (BaseEffect pEffect, WorldTile pTile, string _, string __, float ___, Actor pActor) =>
                {
                    if(pActor == null)
                    {
                        return EffectsLibrary.spawn("fx_napalm_flash", pTile);
                    }
                    float Angle = Mathf.Atan2(pTile.y - pActor.current_position.y, pTile.x - pActor.current_position.x) * Mathf.Rad2Deg;
                    bool flip = Angle <= -90 || Angle >= 90;
                    pEffect.transform.localScale = new Vector2(0.1f, flip ? -0.1f : 0.1f);
                    pEffect.transform.rotation = Quaternion.AngleAxis(Angle, Vector3.forward);
                    return null;
                },
                sound_launch = "event:/SFX/UNITS/dragon/fire_breath"
            });

            AssetManager.effects_library.add(new EffectAsset
            {
                id = "SunGodsOrb",
                use_basic_prefab = false,
                prefab_id = "effects/prefabs/SunGodsOrb",
                show_on_mini_map = true,
                draw_light_area = true,
                draw_light_size = 10,
                spawn_action = (BaseEffect pEffect, WorldTile pTile, string _, string __, float pFloatParam1, Actor pActor) =>
                {
                    if (pActor == null)
                    {
                        return EffectsLibrary.spawn("fx_napalm_flash", pTile);
                    }
                    ((LightGodsOrb)pEffect).Init(pActor, pTile, pFloatParam1);
                    return null;
                }
            }
            );
        }
    }
}
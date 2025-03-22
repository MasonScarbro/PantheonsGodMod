/* 
AUTHOR: MASON SCARBRO
VERSION: 1.0.0
*/
using UnityEngine;
using NeoModLoader.utils;

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

            GameObject BlackHolePrefab = Instantiate(Resources.Load<GameObject>("effects/prefabs/PrefabEffectBasic"));
            BlackHolePrefab.transform.position = new Vector3(-99999, -99999, 0);
            DestroyImmediate(BlackHolePrefab.GetComponent<BaseEffect>());
            SpriteAnimation component = BlackHolePrefab.GetComponent<SpriteAnimation>();
            component.timeBetweenFrames = 0.08f;
            component.returnToPool = false;
            component.frames = Resources.LoadAll<Sprite>("effects/projectiles/blackHoleProjectile");
            component.spriteRenderer.sortingLayerName = "EffectsBack";
            BlackHolePrefab.AddComponent<BlackHoleFlash>();
            ResourcesPatch.PatchResource("effects/prefabs/BlackHole", BlackHolePrefab);
            AssetManager.effects_library.add(new EffectAsset
            {
                id = "fx_BlackHole",
                use_basic_prefab = false,
                prefab_id = "effects/prefabs/BlackHole",
                show_on_mini_map = false,
                draw_light_area = true,
                draw_light_size = 2f,
                draw_light_area_offset_y = 0f,
                sound_launch = "event:/SFX/WEAPONS/WeaponFireballStart",
            });

            GameObject CloudOfDarkness = Instantiate(Resources.Load<GameObject>("effects/prefabs/PrefabAntimatterEffect"));
            CloudOfDarkness.transform.position = new Vector3(-99999, -99999, 0);
            DestroyImmediate(CloudOfDarkness.GetComponent<AntimatterBombEffect>());
            CloudOfDarkness.AddComponent<Storm>();
            ResourcesPatch.PatchResource("effects/prefabs/CloudOfDarkness", CloudOfDarkness);
            AssetManager.effects_library.add(new EffectAsset
            {
                id = "fx_CloudOfDarkness",
                use_basic_prefab = false,
                prefab_id = "effects/prefabs/CloudOfDarkness",
                show_on_mini_map = false,
                limit = 100,
                draw_light_area = true,
                draw_light_size = 2f,
                draw_light_area_offset_y = 0f,
                sound_launch = "event:/SFX/WEAPONS/WeaponFireballStart",
            });

            GameObject CustomWave = Instantiate(Resources.Load<GameObject>("effects/prefabs/PrefabExplosionWave"));
            CustomWave.transform.position = new Vector3(-99999, -99999, 0);
            DestroyImmediate(CustomWave.GetComponent<ExplosionFlash>());
            CustomWave.AddComponent<CustomExplosionFlash>();
            ResourcesPatch.PatchResource("effects/prefabs/CustomExplosionWave", CustomWave);
            AssetManager.effects_library.add(new EffectAsset
            {
                id = "fx_custom_explosion_wave",
                use_basic_prefab = false,
                prefab_id = "effects/prefabs/CustomExplosionWave",
                show_on_mini_map = true
            });

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

            GameObject HeartPrefab = Instantiate(Resources.Load<GameObject>("effects/prefabs/PrefabEffectBasic"));
            HeartPrefab.transform.position = new Vector3(-99999, -99999, 0);
            DestroyImmediate(HeartPrefab.GetComponent<BaseEffect>());
            SpriteAnimation com = HeartPrefab.GetComponent<SpriteAnimation>();
            com.timeBetweenFrames = 0.1f;
            com.returnToPool = false;
            com.frames = Resources.LoadAll<Sprite>("effects/projectiles/Heart");
            com.spriteRenderer.sortingLayerName = "EffectsBack";
            HeartPrefab.AddComponent<ExplosionFlash>();
            GameObject CorruptedHeartPrefab = Instantiate(HeartPrefab);
            CorruptedHeartPrefab.GetComponent<SpriteAnimation>().frames = Resources.LoadAll<Sprite>("effects/projectiles/CorruptedHeart");
            ResourcesPatch.PatchResource("effects/prefabs/CorruptedHeart", CorruptedHeartPrefab);
            ResourcesPatch.PatchResource("effects/prefabs/Heart", HeartPrefab);
            AssetManager.effects_library.add(new EffectAsset
            {
                id = "fx_Heart",
                use_basic_prefab = false,
                prefab_id = "effects/prefabs/Heart",
                sound_launch = "event:/SFX/WEAPONS/WeaponFireballStart",
                show_on_mini_map = true,
                limit = 100,
                draw_light_area = true,
                draw_light_size = 2f,
                draw_light_area_offset_y = 0f,
            });
            AssetManager.effects_library.clone("fx_Heart_Corrupted", "fx_Heart").prefab_id = "effects/prefabs/CorruptedHeart";

            GameObject Moonprefab = Instantiate(Resources.Load<GameObject>("effects/prefabs/PrefabEffectBasic"));
            Moonprefab.transform.position = new Vector3(-99999, -99999, 0);
            DestroyImmediate(Moonprefab.GetComponent<BaseEffect>());
            SpriteAnimation moon = Moonprefab.GetComponent<SpriteAnimation>();
            moon.timeBetweenFrames = 0.1f;
            moon.returnToPool = false;
            moon.frames = Resources.LoadAll<Sprite>("effects/projectiles/moonProjectile");
            moon.spriteRenderer.sortingLayerName = "EffectsTop";
            Moonprefab.AddComponent<MoonOrbit>();
            ResourcesPatch.PatchResource("effects/prefabs/Moon", Moonprefab);
            AssetManager.effects_library.add(new EffectAsset
            {
                id = "fx_Moon_Orbit",
                use_basic_prefab = false,
                prefab_id = "effects/prefabs/Moon",
                show_on_mini_map = true,
                limit = 300,
                draw_light_area = true,
                draw_light_size = 2f,
                draw_light_area_offset_y = 0f,
            });
        }
    }
}
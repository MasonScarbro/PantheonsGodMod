using NeoModLoader.utils;
using SleekRender;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using GodsAndPantheons.CustomEffects;
namespace GodsAndPantheons
{
    public class Prefabs : MonoBehaviour
    {
        public static void Init()
        {
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

            GameObject CloudOfDarkness = Instantiate(Resources.Load<GameObject>("effects/prefabs/PrefabAntimatterEffect"));
            CloudOfDarkness.transform.position = new Vector3(-99999, -99999, 0);
            DestroyImmediate(CloudOfDarkness.GetComponent<AntimatterBombEffect>());
            CloudOfDarkness.AddComponent<Storm>();
            ResourcesPatch.PatchResource("effects/prefabs/CloudOfDarkness", CloudOfDarkness);

            GameObject CustomWave = Instantiate(Resources.Load<GameObject>("effects/prefabs/PrefabExplosionWave"));
            CustomWave.transform.position = new Vector3(-99999, -99999, 0);
            DestroyImmediate(CustomWave.GetComponent<ExplosionFlash>());
            CustomWave.AddComponent<CustomExplosionFlash>();
            ResourcesPatch.PatchResource("effects/prefabs/CustomExplosionWave", CustomWave);

            GameObject HeartPrefab = Instantiate(Resources.Load<GameObject>("effects/prefabs/PrefabEffectBasic"));
            HeartPrefab.transform.position = new Vector3(-99999, -99999, 0);
            HeartPrefab.transform.rotation = Quaternion.Euler(0, 0, -90);
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

            GameObject MountainPath = Instantiate(Resources.Load<GameObject>("effects/prefabs/PrefabEffectBasic"));
            MountainPath.transform.position = new Vector3(-99999, -99999, 0);
            DestroyImmediate(MountainPath.GetComponent<BaseEffect>());
            {
                SpriteAnimation Texture = MountainPath.GetComponent<SpriteAnimation>();
                Texture.timeBetweenFrames = 0.05f;
                Texture.returnToPool = false;
                Texture.looped = false;
                Texture.frames = Resources.LoadAll<Sprite>("effects/fx_dustexplosion");
                Texture.spriteRenderer.sortingLayerName = "EffectsTop";
            }
            MountainPath.AddComponent<TerraformPath>();
            ResourcesPatch.PatchResource("effects/prefabs/TerraformPath", MountainPath);

            GameObject StalagmitePath = Instantiate(MountainPath);
            DestroyImmediate(StalagmitePath.GetComponent<TerraformPath>());
            StalagmitePath.AddComponent<StalagmitePath>();
            ResourcesPatch.PatchResource("effects/prefabs/StalagmitePath", StalagmitePath);

            GameObject PulledRock = Instantiate(Resources.Load<GameObject>("effects/prefabs/PrefabEffectBasic"));
            PulledRock.transform.position = new Vector3(-99999, -99999, 0);
            DestroyImmediate(PulledRock.GetComponent<BaseEffect>());
            {
                SpriteAnimation Texture = PulledRock.GetComponent<SpriteAnimation>();
                Texture.timeBetweenFrames = 0.05f;
                Texture.returnToPool = false;
                Texture.looped = false;
                Texture.frames = Resources.LoadAll<Sprite>("effects/fx_PulledRock");
                Texture.spriteRenderer.sortingLayerName = "EffectsBack";
            }
            PulledRock.AddComponent<PulledRock>();
            ResourcesPatch.PatchResource("effects/prefabs/PulledRock", PulledRock);

            GameObject FireTornado = Instantiate(Resources.Load<GameObject>("effects/prefabs/PrefabTornado"));
            FireTornado.transform.position = new Vector3(-999999, -999999, 0);
            FireTornado.GetComponent<SpriteRenderer>().color = new Color(1, 0.15f, 0.2f, 1);
            FireTornado.GetComponent<TornadoEffect>().DestroyImmediateIfNotNull();
            FireTornado.AddComponent<FireTornado>();
            ResourcesPatch.PatchResource("effects/prefabs/FireTornado", FireTornado);

            GameObject crablaser = Instantiate(Resources.Load<GameObject>("actors/p_crabzilla").transform.GetChild(0).GetChild(2).gameObject);
            crablaser.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).parent = crablaser.transform;
            crablaser.transform.GetChild(0).gameObject.DestroyImmediateIfNotNull();
            crablaser.transform.GetChild(0).localPosition = new Vector3(0, 0, 0);
            crablaser.transform.GetChild(0).localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            crablaser.transform.GetChild(0).localScale = new Vector3(1, 1, 1);
            CrabArm arm = crablaser.transform.GetComponent<CrabArm>();
            arm.laser.color = new Color(1, 0, 0, 1);
            crablaser.transform.GetChild(0).GetChild(0).localPosition = new Vector3(140, 0, 0);
            GameObject Prefab = new GameObject("ChaosLaser");
            crablaser.transform.parent = Prefab.transform;
            crablaser.transform.localPosition = new Vector3(-3f, 12f);
            crablaser.transform.localScale = new Vector3(0.3f, 0.25f);
            InitPrefab(Prefab.AddComponent<ChaosLaser>(), arm.laser, arm.laserPoint, arm.transform);
            static void InitPrefab(ChaosLaser Laser, SpriteRenderer lasersprite, Transform laserPoint, Transform laser)
            {
                Laser.LaserSprite = lasersprite;
                Laser.LaserPoint = laserPoint;
                Laser.Laser = laser;
            }
            Traits.LaserSprites = new List<Sprite>(arm.laserSprites);
            arm.DestroyImmediateIfNotNull();
            ResourcesPatch.PatchResource("effects/prefabs/ChaosLaser", Prefab);
        }
    }
}

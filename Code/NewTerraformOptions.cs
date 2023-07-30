/* 
AUTHOR: MASON SCARBRO
VERSION: 1.0.0
*/
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
    class NewTerraformOptions : MonoBehaviour
    {
        public static void init()
        {
            loadTerraformOptions();
        
        }
        private static void loadTerraformOptions()
        {
            AssetManager.terraform.add(new TerraformOptions
                {
                    id = "BlackHole",
                    flash = true,
                    explode_tile = true,
                    applyForce = true,
                    removeFire = true,
                    destroyBuildings = true,
                    removeTreesFully = true,
                    force_power = 1.5f,
                    explode_strength = 2,
                    explode_and_set_random_fire = true,
                    explosion_pixel_effect = true,
                    attackType = AttackType.Eaten,
                    damage = 10,


                });

                 AssetManager.terraform.add(new TerraformOptions
                {
                    id = "darkDaggers",

                    applyForce = true,
                    force_power = .0001f,
                    attackType = AttackType.Poison,
                    damage = 10,


                });

                AssetManager.terraform.add(new TerraformOptions
                {
                    id = "moonFalling",
                    addBurned = true,
                    applyForce = true,
                    force_power = .01f,
                    explode_tile = true,
                    explode_and_set_random_fire = true,
                    explode_strength = 10,
                    damageBuildings = true,
                    setFire = true,
                    damage = 20,
                    shake = true,
                    shake_intensity = 1f,



                });

                AssetManager.terraform.add(new TerraformOptions
                {
                    id = "lightBallz",
                    flash = true,
                    explode_tile = true,
                    destroyBuildings = true,
                    removeTreesFully = true,
                    addBurned = true,
                    force_power = .1f,
                    explode_strength = 3,
                    explode_and_set_random_fire = true,
                    explosion_pixel_effect = true,
                    attackType = AttackType.Fire,
                    damage = 15,


                });

                AssetManager.terraform.add(new TerraformOptions
                {
                    id = "chaosBoneFire",
                    flash = true,
                    explode_tile = true,
                    destroyBuildings = true,
                    removeTreesFully = true,
                    addBurned = true,
                    explode_strength = 3,
                    explode_and_set_random_fire = true,
                    explosion_pixel_effect = true,
                    attackType = AttackType.Fire,
                    damage = 15,


                });

                AssetManager.terraform.add(new TerraformOptions
                {
                    id = "cometRain",
                    flash = true,
                    explode_tile = true,
                    destroyBuildings = true,
                    removeTreesFully = true,
                    addBurned = true,
                    explode_strength = 1,
                    explosion_pixel_effect = true,
                    attackType = AttackType.Acid,
                    damage = 15,
                });
                AssetManager.terraform.add(new TerraformOptions
                {
                    id = "cometAzureDownDamage",
                    flash = true,
                    explode_tile = true,
                    destroyBuildings = true,
                    removeTreesFully = true,
                    explode_strength = 100,
                    explosion_pixel_effect = true,
                    damage = 500,
                });

                AssetManager.terraform.add(new TerraformOptions
                {
                    id = "smokeFlash",
                    flash = true,
                    explode_tile = true,
                    destroyBuildings = true,
                    removeTreesFully = true,
                    explode_strength = 10,
                    explosion_pixel_effect = true,
                    damage = 500,
                });
        }

    }

}
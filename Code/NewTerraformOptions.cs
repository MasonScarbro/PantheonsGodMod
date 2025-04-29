/* 
AUTHOR: MASON SCARBRO
VERSION: 1.0.0
*/

using UnityEngine;

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
                    apply_force = false,
                    remove_fire = true,
                    destroy_buildings = true,
                    remove_trees_fully = true,
                    explode_strength = 2,
                    explode_and_set_random_fire = true,
                    explosion_pixel_effect = true,
                    attack_type = AttackType.Eaten,
                    damage = 60,


                });
            TerraformOptions lessercrablaser = AssetManager.terraform.clone("LesserCrabLaser", "crab_laser");
            lessercrablaser.damage = 80;
            lessercrablaser.shake_intensity = 0.2f;
            lessercrablaser.apply_force = false;

            AssetManager.terraform.add(new TerraformOptions
                {
                    id = "darkDaggers",

                    apply_force = true,
                    force_power = .0001f,
                    attack_type = AttackType.Poison,
                    damage = 10,


                });

                AssetManager.terraform.add(new TerraformOptions
                {
                    id = "moonFalling",
                    add_burned = true,
                    apply_force = true,
                    force_power = .01f,
                    explode_tile = true,
                    explode_and_set_random_fire = true,
                    explode_strength = 10,
                    damage_buildings = true,
                    setFire = true,
                    damage = 20,
                    shake = true,
                    shake_intensity = 1f,



                });

            AssetManager.terraform.add(new TerraformOptions
            {
                id = "MoonFallSlow",
                add_burned = true,
                apply_force = true,
                force_power = .3f,
                damage_buildings = true,
                setFire = true,
                damage = 30
            });

            AssetManager.terraform.add(new TerraformOptions
                {
                    id = "lightBallz",
                    flash = true,
                    explode_tile = true,
                    destroy_buildings = true,
                    remove_trees_fully = true,
                    add_burned = true,
                    force_power = .1f,
                    explode_strength = 3,
                    explode_and_set_random_fire = true,
                    explosion_pixel_effect = true,
                    attack_type = AttackType.Fire,
                    damage = 15,


                });

                AssetManager.terraform.add(new TerraformOptions
                {
                    id = "chaosBoneFire",
                    flash = true,
                    explode_tile = true,
                    destroy_buildings = true,
                    remove_trees_fully = true,
                    add_burned = true,
                    explode_strength = 3,
                    explode_and_set_random_fire = true,
                    explosion_pixel_effect = true,
                    attack_type = AttackType.Fire,
                    damage = 15,


                });

                AssetManager.terraform.add(new TerraformOptions
                {
                    id = "cometRain",
                    flash = true,
                    explode_tile = true,
                    destroy_buildings = true,
                    remove_trees_fully = true,
                    add_burned = true,
                    explode_strength = 1,
                    explosion_pixel_effect = true,
                    attack_type = AttackType.Acid,
                    damage = 15,
                });
                AssetManager.terraform.add(new TerraformOptions
                {
                    id = "cometAzureDownDamage",
                    flash = true,
                    explode_tile = true,
                    destroy_buildings = true,
                    remove_trees_fully = true,
                    explode_strength = 100,
                    explosion_pixel_effect = true,
                    damage = 50,
                });

                AssetManager.terraform.add(new TerraformOptions
                {
                    id = "smokeFlash",
                    flash = true,
                    explode_tile = true,
                    destroy_buildings = true,
                    remove_trees_fully = true,
                    explode_strength = 10,
                    explosion_pixel_effect = true,
                    damage = 500,
                });

                AssetManager.terraform.add(new TerraformOptions
                {
                    id = "waveTerra",
                    flash = false,
                    explode_tile = false,
                    transform_to_wasteland = true,
                    destroy_buildings = true,
                    remove_trees_fully = true,
                    explode_strength = 5,
                    explosion_pixel_effect = true,
                    apply_force = true,
                    force_power = .0001f,
                    attack_type = AttackType.Poison,
                    damage = 50,
                });
            AssetManager.terraform.add(new TerraformOptions
            {
                id = "PassiveDamage",
                flash = true,
                explode_tile = false,
                transform_to_wasteland = false,
                destroy_buildings = true,
                remove_trees_fully = true,
                shake = false,
                apply_force = true,
                force_power = .0001f,
                attack_type = AttackType.Other,
                damage = 60,
            });
        }

    }

}
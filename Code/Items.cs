/* 
AUTHOR: MASON SCARBRO
VERSION: 1.0.0
*/
using System;
using UnityEngine;
using ReflectionUtility;
using System.Collections.Generic;
using HarmonyLib;
using NCMS.Utils;
using static GodsAndPantheons.Traits;
using System.Runtime.Serialization;

namespace GodsAndPantheons
{
    class Items
    {
        public static void init()
        {


            ItemAsset spearOfLight = AssetManager.items.clone("SpearOfLight", "_melee");
            spearOfLight.id = "SpearOfLight";
            spearOfLight.name_templates = List.Of<string>(new string[] { "Spear Of Divine Light" });
            spearOfLight.materials = List.Of<string>(new string[] { "adamantine" });
            spearOfLight.base_stats[S.fertility] = 0.0f;
            spearOfLight.base_stats[S.max_children] = 0f;
            spearOfLight.base_stats[S.max_age] += 100f;
            spearOfLight.base_stats[S.attack_speed] = 15f;
            spearOfLight.base_stats[S.damage] += 15f;
            spearOfLight.base_stats[S.speed] += 10f;
            spearOfLight.base_stats[S.health] += 10;
            spearOfLight.base_stats[S.accuracy] += 5f;
            spearOfLight.base_stats[S.range] = 1;
            spearOfLight.base_stats[S.armor] = 3f;
            spearOfLight.base_stats[S.scale] = 0.0f;
            spearOfLight.base_stats[S.dodge] = 10f;
            spearOfLight.base_stats[S.targets] = 3f;
            spearOfLight.base_stats[S.critical_chance] = 0.0f;
            spearOfLight.base_stats[S.knockback] = 0.0f;
            spearOfLight.base_stats[S.knockback_reduction] = 0.0f;
            spearOfLight.base_stats[S.intelligence] = 0;
            spearOfLight.base_stats[S.warfare] = 0;
            spearOfLight.base_stats[S.diplomacy] = 0;
            spearOfLight.base_stats[S.stewardship] = 10;
            spearOfLight.base_stats[S.opinion] = 0f;
            spearOfLight.base_stats[S.loyalty_traits] = 0f;
            spearOfLight.base_stats[S.cities] = 0;
            spearOfLight.base_stats[S.zone_range] = 0.1f;
            spearOfLight.equipment_value = 3000;
            spearOfLight.special_effect_interval = 0.1f;
            spearOfLight.path_slash_animation = "effects/slashes/slash_spear";
            spearOfLight.tech_needed = "weapon_spear";
            spearOfLight.quality = ItemQuality.Legendary;
            spearOfLight.equipmentType = EquipmentType.Weapon;
            spearOfLight.name_class = "item_class_weapon";
            spearOfLight.path_icon = "ui/weapon_icons/icon_SpearOfLight_adamantine";
            // For Ranged Weapons use "_range"
            //.base_stats[S.projectiles] = 12f;
            //.base_stats[S.damage_range] = 0.9f;
            //.projectile = "rock" 
            spearOfLight.action_attack_target = new AttackAction(sunGodFuryStrikesAttack);
            AssetManager.items.list.AddItem(spearOfLight);
            Localization.Add("item_SpearOfLight", "Spear Of Divine Light");
            addWeaponsSprite(spearOfLight.id, spearOfLight.materials[0]);


            ItemAsset axeOfFury = AssetManager.items.clone("AxeOfFury", "_melee");
            axeOfFury.id = "AxeOfFury";
            axeOfFury.name_templates = List.Of<string>(new string[] { "Spear Of Divine Light" });
            axeOfFury.materials = List.Of<string>(new string[] { "adamantine" });
            axeOfFury.base_stats[S.fertility] = 0.0f;
            axeOfFury.base_stats[S.max_children] = 0f;
            axeOfFury.base_stats[S.max_age] += 100f;
            axeOfFury.base_stats[S.attack_speed] = 1f;
            axeOfFury.base_stats[S.damage] += 25f;
            axeOfFury.base_stats[S.speed] = 1f;
            axeOfFury.base_stats[S.health] += 5f;
            axeOfFury.base_stats[S.accuracy] = 1f;
            axeOfFury.base_stats[S.range] = 1;
            axeOfFury.base_stats[S.armor] = 1;
            axeOfFury.base_stats[S.scale] = 0.01f;
            axeOfFury.base_stats[S.dodge] = 5f;
            axeOfFury.base_stats[S.targets] = 3f;
            axeOfFury.base_stats[S.critical_chance] = 0.05f;
            axeOfFury.base_stats[S.knockback] = 0.05f;
            axeOfFury.base_stats[S.knockback_reduction] = 0.0f;
            axeOfFury.base_stats[S.intelligence] = 0;
            axeOfFury.base_stats[S.warfare] = 10;
            axeOfFury.base_stats[S.diplomacy] = 0;
            axeOfFury.base_stats[S.stewardship] = 0;
            axeOfFury.base_stats[S.opinion] = 0f;
            axeOfFury.base_stats[S.loyalty_traits] = 0f;
            axeOfFury.base_stats[S.cities] = 0;
            axeOfFury.base_stats[S.zone_range] = 0.1f;
            axeOfFury.equipment_value = 3000;
            axeOfFury.path_slash_animation = "effects/slashes/slash_spear";
            axeOfFury.tech_needed = "weapon_axe";
            axeOfFury.action_attack_target = new AttackAction(WarGodThrow);
            axeOfFury.quality = ItemQuality.Legendary;
            axeOfFury.equipmentType = EquipmentType.Weapon;
            axeOfFury.name_class = "item_class_weapon";
            axeOfFury.path_icon = "ui/weapon_icons/icon_AxeOfFury_adamantine";
            // For Ranged Weapons use "_range"
            //.base_stats[S.projectiles] = 12f;
            //.base_stats[S.damage_range] = 0.9f;
            //.projectile = "rock" 

            AssetManager.items.list.AddItem(axeOfFury);
            Localization.Add("item_AxeOfFury", "Axe Of War");
            addWeaponsSprite(axeOfFury.id, axeOfFury.materials[0]);


            ItemAsset darkDagger = AssetManager.items.clone("DarkDagger", "_melee");
            darkDagger.id = "DarkDagger";
            darkDagger.name_templates = List.Of<string>(new string[] { "Dagger Of Darkness" });
            darkDagger.materials = List.Of<string>(new string[] { "adamantine" });
            darkDagger.base_stats[S.fertility] = 0.0f;
            darkDagger.base_stats[S.max_children] = 0f;
            darkDagger.base_stats[S.max_age] += 100f;
            darkDagger.base_stats[S.attack_speed] = 3f;
            darkDagger.base_stats[S.damage] += 15f;
            darkDagger.base_stats[S.speed] = 1f;
            darkDagger.base_stats[S.health] = 1;
            darkDagger.base_stats[S.accuracy] = 1f;
            darkDagger.base_stats[S.range] = 1;
            darkDagger.base_stats[S.armor] = 1;
            darkDagger.base_stats[S.scale] = 0.0f;
            darkDagger.base_stats[S.dodge] = 15f;
            darkDagger.base_stats[S.targets] = 3f;
            darkDagger.base_stats[S.critical_chance] = 0.0f;
            darkDagger.base_stats[S.knockback] = 0.0f;
            darkDagger.base_stats[S.knockback_reduction] = 0.0f;
            darkDagger.base_stats[S.intelligence] = 0;
            darkDagger.base_stats[S.warfare] = 0;
            darkDagger.base_stats[S.diplomacy] = 0;
            darkDagger.base_stats[S.stewardship] = 5;
            darkDagger.base_stats[S.opinion] = 5f;
            darkDagger.base_stats[S.loyalty_traits] = 0f;
            darkDagger.base_stats[S.cities] = 0;
            darkDagger.base_stats[S.zone_range] = 0.1f;
            darkDagger.equipment_value = 3000;
            darkDagger.path_slash_animation = "effects/slashes/darkSlash";
            darkDagger.tech_needed = String.Empty;
            darkDagger.quality = ItemQuality.Legendary;
            darkDagger.equipmentType = EquipmentType.Weapon;
            darkDagger.name_class = "item_class_weapon";
            darkDagger.path_icon = "ui/weapon_icons/icon_DarkDagger_adamantine";
            darkDagger.action_attack_target = new AttackAction(darkGodTeleportAttack);
            // For Ranged Weapons use "_range"
            //.base_stats[S.projectiles] = 12f;
            //.base_stats[S.damage_range] = 0.9f;
            //.projectile = "rock" 

            AssetManager.items.list.AddItem(darkDagger);
            Localization.Add("item_DarkDagger", "Dagger Of Darkness");
            addWeaponsSprite(darkDagger.id, darkDagger.materials[0]);


            ItemAsset maceOfDestruction = AssetManager.items.clone("MaceOfDestruction", "_melee");
            maceOfDestruction.id = "MaceOfDestruction";
            maceOfDestruction.name_templates = List.Of<string>(new string[] { "Dagger Of Darkness" });
            maceOfDestruction.materials = List.Of<string>(new string[] { "adamantine" });
            maceOfDestruction.base_stats[S.fertility] = 0.0f;
            maceOfDestruction.base_stats[S.max_children] = 0f;
            maceOfDestruction.base_stats[S.max_age] += 100f;
            maceOfDestruction.base_stats[S.attack_speed] = 3f;
            maceOfDestruction.base_stats[S.damage] += 30;
            maceOfDestruction.base_stats[S.speed] += 1f;
            maceOfDestruction.base_stats[S.health] = 1;
            maceOfDestruction.base_stats[S.accuracy] = 1f;
            maceOfDestruction.base_stats[S.range] = 1;
            maceOfDestruction.base_stats[S.armor] = 1;
            maceOfDestruction.base_stats[S.scale] = 0.0f;
            maceOfDestruction.base_stats[S.dodge] = 4f;
            maceOfDestruction.base_stats[S.targets] = 3f;
            maceOfDestruction.base_stats[S.critical_chance] = 0.0f;
            maceOfDestruction.base_stats[S.knockback] = 0.0f;
            maceOfDestruction.base_stats[S.knockback_reduction] = 0.1f;
            maceOfDestruction.base_stats[S.intelligence] = 0;
            maceOfDestruction.base_stats[S.warfare] = 0;
            maceOfDestruction.base_stats[S.diplomacy] = 0;
            maceOfDestruction.base_stats[S.stewardship] = 0;
            maceOfDestruction.base_stats[S.opinion] = 0f;
            maceOfDestruction.base_stats[S.loyalty_traits] = 0f;
            maceOfDestruction.base_stats[S.cities] = 0;
            maceOfDestruction.base_stats[S.zone_range] = 0.1f;
            maceOfDestruction.equipment_value = 3000;
            maceOfDestruction.path_slash_animation = "effects/slashes/slash_spear";
            maceOfDestruction.tech_needed = String.Empty;
            maceOfDestruction.quality = ItemQuality.Legendary;
            maceOfDestruction.equipmentType = EquipmentType.Weapon;
            maceOfDestruction.action_attack_target = new AttackAction(UnleashFire);
            maceOfDestruction.name_class = "item_class_weapon";
            maceOfDestruction.path_icon = "ui/weapon_icons/icon_MaceOfDestruction_adamantine";
            // For Ranged Weapons use "_range"
            //.base_stats[S.projectiles] = 12f;
            //.base_stats[S.damage_range] = 0.9f;
            //.projectile = "rock" 

            AssetManager.items.list.AddItem(maceOfDestruction);
            Localization.Add("item_MaceOfDestruction", "Mace Of Destruction");
            addWeaponsSprite(maceOfDestruction.id, maceOfDestruction.materials[0]);
            
            ItemAsset staffOfKnowledge = AssetManager.items.clone("StaffOfKnowledge", "_range");
            staffOfKnowledge.id = "StaffOfKnowledge";
            staffOfKnowledge.name_templates = List.Of<string>(new string[] { "Staff Of Knowledge" });
            staffOfKnowledge.materials = List.Of<string>(new string[] { "base" });
            staffOfKnowledge.base_stats[S.fertility] = 0.0f;
            staffOfKnowledge.base_stats[S.max_children] = 0f;
            staffOfKnowledge.base_stats[S.max_age] += 100f;
            staffOfKnowledge.base_stats[S.attack_speed] = 1f;
            staffOfKnowledge.base_stats[S.speed] += 1f;
            staffOfKnowledge.base_stats[S.health] = 1;
            staffOfKnowledge.base_stats[S.accuracy] = 1f;
            staffOfKnowledge.base_stats[S.range] = 5;
            staffOfKnowledge.base_stats[S.armor] = 1;
            staffOfKnowledge.base_stats[S.scale] = 0.0f;
            staffOfKnowledge.base_stats[S.dodge] = 1f;
            staffOfKnowledge.base_stats[S.targets] = 3f;
            staffOfKnowledge.base_stats[S.critical_chance] = 0.0f;
            staffOfKnowledge.base_stats[S.knockback] = 0.0f;
            staffOfKnowledge.base_stats[S.knockback_reduction] = 0.1f;
            staffOfKnowledge.base_stats[S.intelligence] = 15f;
            staffOfKnowledge.base_stats[S.warfare] = 0;
            staffOfKnowledge.base_stats[S.diplomacy] = 0;
            staffOfKnowledge.base_stats[S.stewardship] = 0;
            staffOfKnowledge.base_stats[S.opinion] = 0f;
            staffOfKnowledge.base_stats[S.loyalty_traits] = 0f;
            staffOfKnowledge.base_stats[S.cities] = 0;
            staffOfKnowledge.base_stats[S.zone_range] = 0.1f;
            staffOfKnowledge.equipment_value = 3000;
            staffOfKnowledge.path_slash_animation = "effects/slashes/darkSlash";
            staffOfKnowledge.tech_needed = String.Empty;
            staffOfKnowledge.quality = ItemQuality.Legendary;
            staffOfKnowledge.equipmentType = EquipmentType.Weapon;
            staffOfKnowledge.attackType = WeaponType.Range;
            staffOfKnowledge.name_class = "item_class_weapon";
            staffOfKnowledge.path_icon = "ui/weapon_icons/icon_StaffOfKnowledge_base";
            staffOfKnowledge.action_attack_target = new AttackAction(CorruptEnemy);
            // For Ranged Weapons use "_range"
            staffOfKnowledge.base_stats[S.projectiles] = 12f;
            staffOfKnowledge.base_stats[S.damage_range] = 0.9f;
            staffOfKnowledge.projectile = "wordsOfKnowledgeProjectile";

            AssetManager.items.list.AddItem(staffOfKnowledge);
            Localization.Add("item_StaffOfKnowledge", "Staff Of Knowledge");
            addWeaponsSprite(staffOfKnowledge.id, staffOfKnowledge.materials[0]);
            
            ItemAsset StaffOfLove = AssetManager.items.clone("StaffOfLove", "_range");
            StaffOfLove.id = "StaffOfLove";
            StaffOfLove.name_templates = List.Of<string>(new string[] { "Staff Of Love" });
            StaffOfLove.materials = List.Of<string>(new string[] { "base" });
            StaffOfLove.base_stats[S.max_age] += 100f;
            StaffOfLove.base_stats[S.attack_speed] = 1f;
            StaffOfLove.base_stats[S.speed] += 1f;
            StaffOfLove.base_stats[S.health] = 10;
            StaffOfLove.base_stats[S.accuracy] = 1f;
            StaffOfLove.base_stats[S.range] = 20;
            StaffOfLove.base_stats[S.armor] = 1;
            StaffOfLove.base_stats[S.scale] = 0.0f;
            StaffOfLove.base_stats[S.dodge] = 1f;
            StaffOfLove.base_stats[S.damage] = 10;
            StaffOfLove.base_stats[S.targets] = 3f;
            StaffOfLove.base_stats[S.critical_chance] = 0.0f;
            StaffOfLove.base_stats[S.knockback] = 0.0f;
            StaffOfLove.base_stats[S.knockback_reduction] = 0.1f;
            StaffOfLove.base_stats[S.diplomacy] = 5;
            StaffOfLove.base_stats[S.stewardship] = 10;
            StaffOfLove.base_stats[S.damage_range] = 0.5f;
            StaffOfLove.equipment_value = 3000;
            StaffOfLove.path_slash_animation = "effects/slashes/darkSlash";
            StaffOfLove.tech_needed = String.Empty;
            StaffOfLove.quality = ItemQuality.Legendary;
            StaffOfLove.equipmentType = EquipmentType.Weapon;
            StaffOfLove.attackType = WeaponType.Range;
            StaffOfLove.name_class = "item_class_weapon";
            StaffOfLove.path_icon = "ui/weapon_icons/icon_StaffOfLove_base";
            // For Ranged Weapons use "_range"
            StaffOfLove.base_stats[S.projectiles] = 1;
            StaffOfLove.base_stats[S.damage_range] = 0.9f;
            StaffOfLove.projectile = "CorruptedHeart";

            AssetManager.items.list.AddItem(StaffOfLove);
            Localization.Add("item_StaffOfLove", "Staff Of Love");
            addWeaponsSprite(StaffOfLove.id, StaffOfLove.materials[0]);

            ItemAsset FireGodStaff = AssetManager.items.clone("HellStaff", "evil_staff");
            FireGodStaff.id = "HellStaff";
            FireGodStaff.name_templates = List.Of<string>(new string[] { "The Staff of fire" });
            FireGodStaff.materials = List.Of(new string[] { "base" });
            FireGodStaff.base_stats[S.fertility] = 0.0f;
            FireGodStaff.base_stats[S.max_children] = 0f;
            FireGodStaff.base_stats[S.max_age] += 100f;
            FireGodStaff.base_stats[S.attack_speed] = 1f;
            FireGodStaff.base_stats[S.speed] += 1f;
            FireGodStaff.base_stats[S.damage] = 15;
            FireGodStaff.base_stats[S.range] = 1;
            FireGodStaff.base_stats[S.armor] = 1;
            FireGodStaff.base_stats[S.scale] = 0.0f;
            FireGodStaff.base_stats[S.dodge] += 3f;
            FireGodStaff.base_stats[S.targets] = 3f;
            FireGodStaff.base_stats[S.critical_chance] += 0.1f;
            FireGodStaff.base_stats[S.knockback] = 0.0f;
            FireGodStaff.base_stats[S.knockback_reduction] = 0.1f;
            FireGodStaff.base_stats[S.intelligence] += 15f;
            FireGodStaff.base_stats[S.warfare] = 0;
            FireGodStaff.base_stats[S.diplomacy] = 0;
            FireGodStaff.base_stats[S.stewardship] = 0;
            FireGodStaff.base_stats[S.opinion] = 0f;
            FireGodStaff.base_stats[S.loyalty_traits] = 0f;
            FireGodStaff.base_stats[S.cities] = 0;
            FireGodStaff.base_stats[S.zone_range] = 0.1f;
            FireGodStaff.base_stats[S.projectiles] = 10;
            FireGodStaff.equipment_value = 3300;
            FireGodStaff.tech_needed = String.Empty;
            FireGodStaff.quality = ItemQuality.Legendary;
            FireGodStaff.name_class = "item_class_weapon";
            FireGodStaff.path_icon = "ui/weapon_icons/icon_HellStaf";
            FireGodStaff.action_attack_target = new AttackAction(UnleashHell);

            AssetManager.items.list.AddItem(FireGodStaff);
            Localization.Add("item_HellStaff", "the Staff of fire");
            addWeaponsSprite(FireGodStaff.id, FireGodStaff.materials[0]);

            ItemAsset cometScepter = AssetManager.items.clone("CometScepter", "_range");
            cometScepter.id = "CometScepter";
            cometScepter.name_templates = List.Of<string>(new string[] { "Scepter Of The Stars" });
            cometScepter.materials = List.Of<string>(new string[] { "base" });
            cometScepter.base_stats[S.fertility] = 0.0f;
            cometScepter.base_stats[S.max_children] = 0f;
            cometScepter.base_stats[S.max_age] += 100f;
            cometScepter.base_stats[S.attack_speed] = 1f;
            cometScepter.base_stats[S.damage] += 10;
            cometScepter.base_stats[S.speed] += 1f;
            cometScepter.base_stats[S.health] = 1;
            cometScepter.base_stats[S.accuracy] = 1f;
            cometScepter.base_stats[S.range] = 1;
            cometScepter.base_stats[S.armor] = 1;
            cometScepter.base_stats[S.scale] = 0.0f;
            cometScepter.base_stats[S.dodge] += 3f;
            cometScepter.base_stats[S.targets] = 3f;
            cometScepter.base_stats[S.critical_chance] += 0.1f;
            cometScepter.base_stats[S.knockback] = 0.0f;
            cometScepter.base_stats[S.knockback_reduction] = 0.1f;
            cometScepter.base_stats[S.intelligence] += 15f;
            cometScepter.base_stats[S.warfare] = 0;
            cometScepter.base_stats[S.diplomacy] = 0;
            cometScepter.base_stats[S.stewardship] = 0;
            cometScepter.base_stats[S.opinion] = 0f;
            cometScepter.base_stats[S.loyalty_traits] = 0f;
            cometScepter.base_stats[S.cities] = 0;
            cometScepter.base_stats[S.zone_range] = 0.1f;
            cometScepter.equipment_value = 3000;
            cometScepter.path_slash_animation = "effects/slashes/slash_punch";
            cometScepter.tech_needed = String.Empty;
            cometScepter.quality = ItemQuality.Legendary;
            cometScepter.equipmentType = EquipmentType.Weapon;
            cometScepter.attackType = WeaponType.Range;
            cometScepter.name_class = "item_class_weapon";
            cometScepter.path_icon = "ui/weapon_icons/icon_CometScepter_base";
            cometScepter.action_attack_target = new AttackAction(UnleashMoonFall);
            // For Ranged Weapons use "_range"
            cometScepter.base_stats[S.projectiles] = 4f;
            cometScepter.base_stats[S.damage_range] = 0.9f;
            cometScepter.projectile = "starShowerProjectile";

            AssetManager.items.list.AddItem(cometScepter);
            Localization.Add("item_CometScepter", "Scepter Of The Stars");
            addWeaponsSprite(cometScepter.id, cometScepter.materials[0]);

            ItemAsset hammerOfCreation = AssetManager.items.clone("HammerOfCreation", "_melee");
            hammerOfCreation.id = "HammerOfCreation";
            hammerOfCreation.name_templates = List.Of<string>(new string[] { "Hammer Of Creation" });
            hammerOfCreation.materials = List.Of<string>(new string[] { "base" });
            hammerOfCreation.base_stats[S.fertility] = 0.0f;
            hammerOfCreation.base_stats[S.max_children] = 0f;
            hammerOfCreation.base_stats[S.max_age] += 100f;
            hammerOfCreation.base_stats[S.attack_speed] = 1f;
            hammerOfCreation.base_stats[S.damage] += 15;
            hammerOfCreation.base_stats[S.speed] += 1f;
            hammerOfCreation.base_stats[S.health] = 1;
            hammerOfCreation.base_stats[S.accuracy] = 1f;
            hammerOfCreation.base_stats[S.range] = 2;
            hammerOfCreation.base_stats[S.armor] = 3;
            hammerOfCreation.base_stats[S.scale] = 0.0f;
            hammerOfCreation.base_stats[S.dodge] += 1f;
            hammerOfCreation.base_stats[S.targets] = 3f;
            hammerOfCreation.base_stats[S.critical_chance] += 0.1f;
            hammerOfCreation.base_stats[S.knockback] = 0.2f;
            hammerOfCreation.base_stats[S.knockback_reduction] = 0.1f;
            hammerOfCreation.base_stats[S.intelligence] += 0f;
            hammerOfCreation.base_stats[S.warfare] = 0;
            hammerOfCreation.base_stats[S.diplomacy] = 0;
            hammerOfCreation.base_stats[S.stewardship] = 0;
            hammerOfCreation.base_stats[S.opinion] = 0f;
            hammerOfCreation.base_stats[S.loyalty_traits] = 0f;
            hammerOfCreation.base_stats[S.cities] = 0;
            hammerOfCreation.base_stats[S.zone_range] = 0.1f;
            hammerOfCreation.equipment_value = 3000;
            hammerOfCreation.path_slash_animation = "effects/slashes/slash_axe";
            hammerOfCreation.tech_needed = String.Empty;
            hammerOfCreation.quality = ItemQuality.Legendary;
            hammerOfCreation.equipmentType = EquipmentType.Weapon;
            hammerOfCreation.name_class = "item_class_weapon";
            hammerOfCreation.path_icon = "ui/weapon_icons/icon_HammerOfCreation_base";
            hammerOfCreation.action_attack_target = (AttackAction)Delegate.Combine(hammerOfCreation.action_attack_target, new AttackAction(earthGodSendMountain));
            // For Ranged Weapons use "_range"
            AssetManager.items.list.AddItem(hammerOfCreation);
            Localization.Add("item_HammerOfCreation", "Hammer Of Creation");
            addWeaponsSprite(hammerOfCreation.id, hammerOfCreation.materials[0]);

            ItemAsset lichGodsGreatSword = AssetManager.items.clone("LichGodsGreatSword", "_melee");
            lichGodsGreatSword.id = "LichGodsGreatSword";
            lichGodsGreatSword.name_templates = List.Of<string>(new string[] { "GreatSword Of The Lich" });
            lichGodsGreatSword.materials = List.Of<string>(new string[] { "base" });
            lichGodsGreatSword.base_stats[S.fertility] = 0.0f;
            lichGodsGreatSword.base_stats[S.max_children] = 0f;
            lichGodsGreatSword.base_stats[S.max_age] += 100f;
            lichGodsGreatSword.base_stats[S.attack_speed] = 1f;
            lichGodsGreatSword.base_stats[S.damage] += 10;
            lichGodsGreatSword.base_stats[S.speed] += 1f;
            lichGodsGreatSword.base_stats[S.health] = 1;
            lichGodsGreatSword.base_stats[S.accuracy] = 1f;
            lichGodsGreatSword.base_stats[S.range] = 2;
            lichGodsGreatSword.base_stats[S.armor] = 3;
            lichGodsGreatSword.base_stats[S.scale] = 0.0f;
            lichGodsGreatSword.base_stats[S.dodge] += 1f;
            lichGodsGreatSword.base_stats[S.targets] = 3f;
            lichGodsGreatSword.base_stats[S.critical_chance] += 0.1f;
            lichGodsGreatSword.base_stats[S.knockback] = 0.2f;
            lichGodsGreatSword.base_stats[S.knockback_reduction] = 0.1f;
            lichGodsGreatSword.base_stats[S.intelligence] += 0f;
            lichGodsGreatSword.base_stats[S.warfare] = 0;
            lichGodsGreatSword.base_stats[S.diplomacy] = 0;
            lichGodsGreatSword.base_stats[S.stewardship] = 0;
            lichGodsGreatSword.base_stats[S.opinion] = 0f;
            lichGodsGreatSword.base_stats[S.loyalty_traits] = 0f;
            lichGodsGreatSword.base_stats[S.cities] = 0;
            lichGodsGreatSword.base_stats[S.zone_range] = 0.1f;
            lichGodsGreatSword.equipment_value = 3000;
            lichGodsGreatSword.path_slash_animation = "effects/slashes/slash_sword";
            lichGodsGreatSword.tech_needed = String.Empty;
            lichGodsGreatSword.quality = ItemQuality.Legendary;
            lichGodsGreatSword.equipmentType = EquipmentType.Weapon;
            lichGodsGreatSword.action_attack_target = new AttackAction(LichGodAttack);
            lichGodsGreatSword.name_class = "item_class_weapon";
            lichGodsGreatSword.path_icon = "ui/weapon_icons/icon_LichGodsGreatSword_base";
            
            // For Ranged Weapons use "_range"
            AssetManager.items.list.AddItem(lichGodsGreatSword);
            Localization.Add("item_LichGodsGreatSword", "GreatSword Of The Lich");
            addWeaponsSprite(lichGodsGreatSword.id, lichGodsGreatSword.materials[0]);

            ItemAsset godHuntersScythe = AssetManager.items.clone("GodHuntersScythe", "_melee");
            godHuntersScythe.id = "GodHuntersScythe";
            godHuntersScythe.name_templates = List.Of<string>(new string[] { "God Hunters Scythe" });
            godHuntersScythe.materials = List.Of<string>(new string[] { "base" });
            godHuntersScythe.base_stats[S.attack_speed] += 18f;
            godHuntersScythe.base_stats[S.damage] += 26;
            godHuntersScythe.base_stats[S.speed] += 3f;
            godHuntersScythe.base_stats[S.health] = 1;
            godHuntersScythe.base_stats[S.accuracy] = -2f;
            godHuntersScythe.base_stats[S.range] = 3;
            godHuntersScythe.base_stats[S.armor] = 2;
            godHuntersScythe.base_stats[S.dodge] += 4f;
            godHuntersScythe.base_stats[S.targets] = 3f;
            godHuntersScythe.base_stats[S.critical_chance] += 0.1f;
            godHuntersScythe.base_stats[S.knockback] = 0.2f;
            godHuntersScythe.base_stats[S.knockback_reduction] = 0.1f;
            godHuntersScythe.base_stats[S.intelligence] += 4f;
            godHuntersScythe.base_stats[S.opinion] = 20f;
            godHuntersScythe.base_stats[S.zone_range] = 0.1f;
            godHuntersScythe.equipment_value = 1800;
            godHuntersScythe.path_slash_animation = "effects/slashes/slash_sword";
            godHuntersScythe.tech_needed = String.Empty;
            godHuntersScythe.quality = ItemQuality.Legendary;
            godHuntersScythe.equipmentType = EquipmentType.Weapon;
            godHuntersScythe.name_class = "item_class_weapon";
            godHuntersScythe.path_icon = "ui/weapon_icons/icon_GodHuntersScythe_base";
            godHuntersScythe.action_attack_target = new AttackAction(GodHunterAttack);

            AssetManager.items.list.AddItem(godHuntersScythe);
            Localization.Add("item_GodHuntersScythe", "The Weapon Chosen by the God Hunters, Deals 5x more damage to Gods");
            addWeaponsSprite(godHuntersScythe.id, godHuntersScythe.materials[0]);

        }

        static bool Flame(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null)
            {
                if (Toolbox.randomChance(80.0f))
                {
                    pTarget.CallMethod("addStatusEffect", "burning", 15f);
                }
            }
            return false;
        }


        static bool NoneAttackSomeoneAction(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            return false;
            
        }
        //doesnt work on gods, that would be too overpowered
        public static bool CorruptEnemy(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Toolbox.randomChance(GetEnhancedChance("God Of Knowledge", "CorruptEnemy%")) && pTarget.isActor() && !IsGod(pTarget.a))
            {
                World.world.getObjectsInChunks(pTile, 8, MapObjectType.Actor);
                foreach (Actor a in World.world.temp_map_objects)
                {
                    if(IsGod(a) || a.kingdom == pSelf.kingdom) continue;
                    CorruptActor(a, pSelf.a);
                }
                MusicBox.playSound("event:/SFX/EXPLOSIONS/ExplosionForce", pTile, false, false);
                EffectsLibrary.spawnExplosionWave(pTile.posV3, 5f, 0.5f);
            }
            return true;
        }
        static bool UnleashMoonFall(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Toolbox.randomChance(GetEnhancedChance("God Of the Stars", "summonMoonChunk%")))
            {
                Vector2Int pos = pTile.pos; // Position of the Ptile as a Vector 2
                float pDist = Vector2.Distance(pTarget.currentPosition, pos); // the distance between the target and the pTile
                Vector3 newPoint = Toolbox.getNewPoint(pSelf.currentPosition.x + 35f, pSelf.currentPosition.y + 95f, (float)pos.x + 1f, (float)pos.y + 1f, pDist, true); // the Point of the projectile launcher 
                Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pTarget.a.stats[S.size], true);
                EffectsLibrary.spawnProjectile("moonFall", newPoint, newPoint2, 0.0f);
                pSelf.a.addStatusEffect("invincible", 2f);
            }
            return true;
        }
        static bool UnleashFire(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Toolbox.randomChance(GetEnhancedChance("God Of Chaos", "BoneFire%")))
            {
                Vector2Int pos = pTile.pos; // Position of the Ptile as a Vector 2
                float pDist = Vector2.Distance(pTarget.currentPosition, pos); // the distance between the target and the pTile
                Vector3 newPoint = Toolbox.getNewPoint(pSelf.currentPosition.x, pSelf.currentPosition.y, (float)pos.x, (float)pos.y, pDist, true); // the Point of the projectile launcher 
                Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pTarget.stats[S.size], true);
                EffectsLibrary.spawnProjectile("boneFire", newPoint, newPoint2, 0.0f);
            }
            return true;
        }
        static bool WarGodThrow(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Toolbox.randomChance(GetEnhancedChance("God Of War", "axeThrow%")))
            {
                Vector2Int pos = pTile.pos; // Position of the Ptile as a Vector 2
                float pDist = Vector2.Distance(pTarget.currentPosition, pos); // the distance between the target and the pTile
                Vector3 newPoint = Toolbox.getNewPoint(pSelf.currentPosition.x, pSelf.currentPosition.y, (float)pos.x, (float)pos.y, pDist, true); // the Point of the projectile launcher 
                Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pTarget.a.stats[S.size], true);
                EffectsLibrary.spawnProjectile("WarAxeProjectile1", newPoint, newPoint2, 0.0f);

            }
            return true;

        }
        static bool NoneRegularAction(BaseSimObject pTarget, WorldTile pTile = null)
        {

            return false;

        }
        static bool NoneGetAttackedAction(BaseSimObject pSelf, BaseSimObject pAttackedBy = null, WorldTile pTile = null)
        {

            return false;

        }

        public static bool sunGodFuryStrikesAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (pTarget != null)
            {
                if (Toolbox.randomChance(0.9f))
                {

                    Vector2Int pos = pTile.pos; // Position of the Ptile as a Vector 2
                    float pDist = Vector2.Distance(pTarget.currentPosition, pos); // the distance between the target and the pTile
                    Vector3 newPoint = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pDist, true); // the Point of the projectile launcher 
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pTarget.stats[S.size], true);
                    EffectsLibrary.spawnProjectile("lightSlashesProjectile", newPoint, newPoint2, 0.0f);

                }
                return true;
            }
            return false;
        }

        public static bool darkGodTeleportAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (pTarget != null)
            {
                if (Toolbox.randomChance(GetEnhancedChance("God Of the Night", "DarkDash%")))
                {
                    PushActor(pSelf.a, pTarget.currentTile.pos, 4);
                    World.world.getObjectsInChunks(pTile, 4, MapObjectType.All);
                    foreach(BaseSimObject enemy in World.world.temp_map_objects){
                        if(enemy.kingdom != pSelf.kingdom)
                        {
                            enemy.getHit(80, true, AttackType.Weapon, pSelf);
                        }
                    }
                    return true;
                }
                return true;
            }
            return false;
        }
        public static bool GodHunterAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile){
            if(pTarget != null && pTarget.isActor()){
                pTarget.addStatusEffect("ash_fever", 5);
                if (IsGod(pTarget.a)){
                    pTarget.getHit(pSelf.stats[S.damage] * 2f, true, AttackType.Weapon, pSelf, false, true);
                }
            }
            return true;
                
        }

        public static bool UnleashHell(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (!pSelf.hasStatus("Lassering") && Toolbox.randomChance(GetEnhancedChance("God Of Fire", "ChaosLaser%")))
            {
                CreateLaserForActor(pSelf.a);
            }
            return true;
        }

        

        public static bool LichGodAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (pTarget != null)
            {

                if (Toolbox.randomChance(GetEnhancedChance("God Of The Lich", "waveOfMutilation%")))
                {
                    Vector2Int pos = pTile.pos; // Position of the Ptile as a Vector 2
                    float pDist = Vector2.Distance(pTarget.currentPosition, pos); // the distance between the target and the pTile
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.currentPosition.x, pSelf.currentPosition.y, (float)pos.x, (float)pos.y, pDist, true); // the Point of the projectile launcher 
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pTarget.a.stats[S.size], true);
                    EffectsLibrary.spawnProjectile("waveOfMutilationProjectile", newPoint, newPoint2, 0.0f);
                }


            }
            return true;
        }

            public static bool earthGodSendMountain(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (pSelf.a != null)
            {
                
                if (Toolbox.randomChance(GetEnhancedChance("God Of the Earth", "buildWorld%")))
                {

                    buildMountainPath(pTile, pSelf, pTarget);
                }

                return true;
            }
            return false;
        }

        

        public static void buildMountainPath(WorldTile pTile, BaseSimObject pSelf, BaseSimObject pTarget)
        {
            
            List<WorldTile> selfTiles = pSelf.a.current_path;
            List<WorldTile> targetTiles = pTarget.a.current_path;
            if (selfTiles != null)
            {
                int length = selfTiles.Count;
                for (int i = 0; i < selfTiles.Count; i++)
                {
                    
                    WorldTile tileP = selfTiles[i];
                    MapAction.terraformMain(tileP, AssetManager.tiles.get("mountains"), TerraformLibrary.destroy);
                    


                }
                

            }
            if (targetTiles != null)
            {
                if (selfTiles.Count == 0)
                {
                    for (int i = 0; i < targetTiles.Count; i++)
                    {
                        WorldTile tileP = targetTiles[i];
                        MapAction.terraformMain(tileP, AssetManager.tiles.get("mountains"), TerraformLibrary.destroy);
                       

                    }
                   
                }
            }
            


        }


        static void addWeaponsSprite(string id, string material)
        {
            var dictItems = Reflection.GetField(typeof(ActorAnimationLoader), null, "dictItems") as Dictionary<string, Sprite>;
            var sprite = Resources.Load<Sprite>("weapons/w_" + id + "_" + material);
            dictItems.Add(sprite.name, sprite);
        }



    }
}

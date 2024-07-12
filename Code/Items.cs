/* 
AUTHOR: MASON SCARBRO
VERSION: 1.0.0
*/
using System;
using System.Threading;
using NCMS;
using UnityEngine;
using ReflectionUtility;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using NCMS.Utils;
using System.Collections;

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
            Localization.addLocalization("item_SpearOfLight", "Spear Of Divine Light");
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
            axeOfFury.quality = ItemQuality.Legendary;
            axeOfFury.equipmentType = EquipmentType.Weapon;
            axeOfFury.name_class = "item_class_weapon";
            axeOfFury.path_icon = "ui/weapon_icons/icon_AxeOfFury_adamantine";
            // For Ranged Weapons use "_range"
            //.base_stats[S.projectiles] = 12f;
            //.base_stats[S.damage_range] = 0.9f;
            //.projectile = "rock" 

            AssetManager.items.list.AddItem(axeOfFury);
            Localization.addLocalization("item_AxeOfFury", "Axe Of War");
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
            Localization.addLocalization("item_DarkDagger", "Dagger Of Darkness");
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
            maceOfDestruction.name_class = "item_class_weapon";
            maceOfDestruction.path_icon = "ui/weapon_icons/icon_MaceOfDestruction_adamantine";
            // For Ranged Weapons use "_range"
            //.base_stats[S.projectiles] = 12f;
            //.base_stats[S.damage_range] = 0.9f;
            //.projectile = "rock" 

            AssetManager.items.list.AddItem(maceOfDestruction);
            Localization.addLocalization("item_MaceOfDestruction", "Mace Of Destruction");
            addWeaponsSprite(maceOfDestruction.id, maceOfDestruction.materials[0]);

            ItemAsset staffOfKnowledge = AssetManager.items.clone("StaffOfKnowledge", "_range");
            staffOfKnowledge.id = "StaffOfKnowledge";
            staffOfKnowledge.name_templates = List.Of<string>(new string[] { "Staff Of Knowledge" });
            staffOfKnowledge.materials = List.Of<string>(new string[] { "base" });
            staffOfKnowledge.base_stats[S.fertility] = 0.0f;
            staffOfKnowledge.base_stats[S.max_children] = 0f;
            staffOfKnowledge.base_stats[S.max_age] += 100f;
            staffOfKnowledge.base_stats[S.attack_speed] = 1f;
            staffOfKnowledge.base_stats[S.damage] += 8;
            staffOfKnowledge.base_stats[S.speed] += 1f;
            staffOfKnowledge.base_stats[S.health] = 1;
            staffOfKnowledge.base_stats[S.accuracy] = 1f;
            staffOfKnowledge.base_stats[S.range] = 1;
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
            // For Ranged Weapons use "_range"
            staffOfKnowledge.base_stats[S.projectiles] = 12f;
            staffOfKnowledge.base_stats[S.damage_range] = 0.9f;
            staffOfKnowledge.projectile = "wordsOfKnowledgeProjectile";

            AssetManager.items.list.AddItem(staffOfKnowledge);
            Localization.addLocalization("item_StaffOfKnowledge", "Staff Of Knowledge");
            addWeaponsSprite(staffOfKnowledge.id, staffOfKnowledge.materials[0]);

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
            // For Ranged Weapons use "_range"
            cometScepter.base_stats[S.projectiles] = 4f;
            cometScepter.base_stats[S.damage_range] = 0.9f;
            cometScepter.projectile = "starShowerProjectile";

            AssetManager.items.list.AddItem(cometScepter);
            Localization.addLocalization("item_CometScepter", "Scepter Of The Stars");
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
            Localization.addLocalization("item_HammerOfCreation", "Hammer Of Creation");
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
            lichGodsGreatSword.name_class = "item_class_weapon";
            lichGodsGreatSword.path_icon = "ui/weapon_icons/icon_LichGodsGreatSword_base";
            
            // For Ranged Weapons use "_range"
            AssetManager.items.list.AddItem(lichGodsGreatSword);
            Localization.addLocalization("item_LichGodsGreatSword", "GreatSword Of The Lich");
            addWeaponsSprite(lichGodsGreatSword.id, lichGodsGreatSword.materials[0]);


            ItemAsset godHuntersScytheB = AssetManager.items.clone("GodHuntersScytheBlank", "_melee");
            godHuntersScytheB.id = "GodHuntersScytheBlank";
            godHuntersScytheB.name_templates = List.Of<string>(new string[] { "God Hunters Scythe" });
            godHuntersScytheB.materials = List.Of<string>(new string[] { "base" });
            godHuntersScytheB.base_stats[S.fertility] = 0.0f;
            godHuntersScytheB.base_stats[S.max_children] = 0f;
            godHuntersScytheB.base_stats[S.attack_speed] += 5f;
            godHuntersScytheB.base_stats[S.damage] += 20;
            godHuntersScytheB.base_stats[S.speed] += 1f;
            godHuntersScytheB.base_stats[S.health] = 1;
            godHuntersScytheB.base_stats[S.accuracy] = 1f;
            godHuntersScytheB.base_stats[S.range] = 2;
            godHuntersScytheB.base_stats[S.armor] = 3;
            godHuntersScytheB.base_stats[S.scale] = 0.0f;
            godHuntersScytheB.base_stats[S.dodge] += 4f;
            godHuntersScytheB.base_stats[S.targets] = 3f;
            godHuntersScytheB.base_stats[S.critical_chance] += 0.1f;
            godHuntersScytheB.base_stats[S.knockback] = 0.2f;
            godHuntersScytheB.base_stats[S.knockback_reduction] = 0.1f;
            godHuntersScytheB.base_stats[S.intelligence] += 0f;
            godHuntersScytheB.base_stats[S.warfare] = 0;
            godHuntersScytheB.base_stats[S.diplomacy] = 0;
            godHuntersScytheB.base_stats[S.stewardship] = 0;
            godHuntersScytheB.base_stats[S.opinion] = 0f;
            godHuntersScytheB.base_stats[S.loyalty_traits] = 0f;
            godHuntersScytheB.base_stats[S.cities] = 0;
            godHuntersScytheB.base_stats[S.zone_range] = 0.1f;
            godHuntersScytheB.equipment_value = 3000;
            godHuntersScytheB.path_slash_animation = "effects/slashes/slash_sword";
            godHuntersScytheB.tech_needed = String.Empty;
            godHuntersScytheB.quality = ItemQuality.Legendary;
            godHuntersScytheB.equipmentType = EquipmentType.Weapon;
            godHuntersScytheB.name_class = "item_class_weapon";
            godHuntersScytheB.path_icon = "ui/weapon_icons/icon_GodHuntersScythe_base";
            godHuntersScytheB.action_attack_target = new AttackAction(GodHunterAttack);

            // For Ranged Weapons use "_range"
            AssetManager.items.list.AddItem(godHuntersScytheB);
            Localization.addLocalization("item_GodHuntersScytheBlank", "The Weapon Chosen by the God Hunters, Deals 5x more damage to Gods");
            addWeaponsSprite(godHuntersScytheB.id, godHuntersScytheB.materials[0]);


            // for humans
            ItemAsset godHuntersScythe = AssetManager.items.clone("GodHuntersScythe", "_melee");
            godHuntersScythe.id = "GodHuntersScythe";
            godHuntersScythe.name_templates = List.Of<string>(new string[] { "God Hunters Scythe" });
            godHuntersScythe.materials = List.Of<string>(new string[] { "base" });
            godHuntersScythe.base_stats[S.fertility] = 0.0f;
            godHuntersScythe.base_stats[S.max_children] = 0f;
            godHuntersScythe.base_stats[S.attack_speed] += 10f;
            godHuntersScythe.base_stats[S.damage] += 25;
            godHuntersScythe.base_stats[S.speed] += 3f;
            godHuntersScythe.base_stats[S.health] = 1;
            godHuntersScythe.base_stats[S.accuracy] = 1f;
            godHuntersScythe.base_stats[S.range] = 2;
            godHuntersScythe.base_stats[S.armor] = 3;
            godHuntersScythe.base_stats[S.scale] = 0.0f;
            godHuntersScythe.base_stats[S.dodge] += 4f;
            godHuntersScythe.base_stats[S.targets] = 3f;
            godHuntersScythe.base_stats[S.critical_chance] += 0.1f;
            godHuntersScythe.base_stats[S.knockback] = 0.2f;
            godHuntersScythe.base_stats[S.knockback_reduction] = 0.1f;
            godHuntersScythe.base_stats[S.intelligence] += 4f;
            godHuntersScythe.base_stats[S.warfare] = 0;
            godHuntersScythe.base_stats[S.diplomacy] = 0;
            godHuntersScythe.base_stats[S.stewardship] = 0;
            godHuntersScythe.base_stats[S.opinion] = 20f;
            godHuntersScythe.base_stats[S.loyalty_traits] = 0f;
            godHuntersScythe.base_stats[S.cities] = 0;
            godHuntersScythe.base_stats[S.zone_range] = 0.1f;
            godHuntersScythe.equipment_value = 3000;
            godHuntersScythe.path_slash_animation = "effects/slashes/slash_sword";
            godHuntersScythe.tech_needed = String.Empty;
            godHuntersScythe.quality = ItemQuality.Legendary;
            godHuntersScythe.equipmentType = EquipmentType.Weapon;
            godHuntersScythe.name_class = "item_class_weapon";
            godHuntersScythe.path_icon = "ui/weapon_icons/icon_GodHuntersScythe_base";
            godHuntersScythe.action_attack_target = new AttackAction(GodHunterAttack);

            AssetManager.items.list.AddItem(godHuntersScythe);
            Localization.addLocalization("item_GodHuntersScythe", "The Weapon Chosen by the God Hunters, Deals 5x more damage to Gods");
            addWeaponsSprite(godHuntersScythe.id, godHuntersScythe.materials[0]);

        }

        static bool Flame(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null)
            {
                Actor a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;
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
                Actor a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;
                if (Toolbox.randomChance(0.9f))
                {

                    Vector2Int pos = pTile.pos; // Position of the Ptile as a Vector 2
                    float pDist = Vector2.Distance(pTarget.currentPosition, pos); // the distance between the target and the pTile
                    Vector3 newPoint = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pDist, true); // the Point of the projectile launcher 
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pTarget.a.stats[S.size], true);
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
                
                if (Toolbox.randomChance(0.6f))
                {
                    EffectsLibrary.spawnAt("fx_teleportStart_dej", pSelf.currentPosition, pSelf.a.stats[S.scale]);
                    BaseEffect baseEffect = EffectsLibrary.spawnAt("fx_teleportEnd_dej", pTarget.a.currentTile.posV3, pSelf.a.stats[S.scale]);
                    if (baseEffect != null)
                    {
                        baseEffect.spriteAnimation.setFrameIndex(9);
                    }
                    pSelf.a.cancelAllBeh(null);
                    pSelf.a.spawnOn(pTarget.a.currentTile, 0f);
                    return true;
                }
                return true;
            }
            return false;
        }
        public static bool GodHunterAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile){
            if(pTarget != null && pTarget.isActor()){
                if(Traits.IsGod(pTarget.a)){
                    pTarget.getHit(pSelf.stats[S.damage] * 4f, true, AttackType.Weapon, pSelf, false, true);
                }
            }
            return true;
                
        }

        public static bool earthGodSendMountain(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (pSelf.a != null)
            {
                
                if (Toolbox.randomChance(0.1f))
                {

                    buildMountainPath(pTile, pSelf, pTarget);
                    //Debug.Log("IGNORE THIS ERROR AND KEEP PLAYING!");

                }

                return true;
            }
            return false;
        }

        

        public static void buildMountainPath(WorldTile pTile, BaseSimObject pSelf, BaseSimObject pTarget)
        {
            
            List<WorldTile> selfTiles = pSelf.a.current_path;
            List<WorldTile> targetTiles = pTarget.a.current_path;

            //Debug.Log(selfTiles);
            if (selfTiles != null)
            {
                int length = selfTiles.Count;
                Debug.Log("Self: " + selfTiles.Count);
                for (int i = 0; i < selfTiles.Count; i++)
                {
                    
                    WorldTile tileP = selfTiles[i];
                    MapAction.terraformMain(tileP, AssetManager.tiles.get("mountains"), TerraformLibrary.destroy);
                    


                }
                

            }
            if (targetTiles != null)
            {
                Debug.Log("Target: " + targetTiles.Count);
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

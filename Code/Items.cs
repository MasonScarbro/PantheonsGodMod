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

namespace GodsAndPantheons
{
    class Items
    {
        public static void init()
        {


            ItemAsset spearOfLight = AssetManager.items.clone("SpearOfLight", "$melee");
            spearOfLight.id = "SpearOfLight";
            spearOfLight.translation_key = "Spear Of Divine Light";
            spearOfLight.name_templates = List.Of<string>(new string[] { "sword_name" });
            spearOfLight.base_stats[S.lifespan] += 100f;
            spearOfLight.base_stats[S.attack_speed] = 15f;
            spearOfLight.base_stats[S.damage] += 15f;
            spearOfLight.base_stats[S.speed] += 10f;
            spearOfLight.base_stats[S.health] += 10;
            spearOfLight.base_stats[S.accuracy] += 5f;
            spearOfLight.base_stats[S.range] = 1;
            spearOfLight.base_stats[S.armor] = 3f;
            spearOfLight.base_stats[S.scale] = 0.0f;
            spearOfLight.base_stats[S.targets] = 3f;
            spearOfLight.base_stats[S.critical_chance] = 0.0f;
            spearOfLight.base_stats[S.knockback] = 0.0f;
            spearOfLight.base_stats[S.mass] = 0.0f;
            spearOfLight.base_stats[S.intelligence] = 0;
            spearOfLight.base_stats[S.warfare] = 0;
            spearOfLight.base_stats[S.diplomacy] = 0;
            spearOfLight.base_stats[S.stewardship] = 10;
            spearOfLight.base_stats[S.opinion] = 0f;
            spearOfLight.base_stats[S.loyalty_traits] = 0f;
            spearOfLight.base_stats[S.cities] = 0;
            spearOfLight.equipment_value = 3000;
            spearOfLight.special_effect_interval = 0.1f;
            spearOfLight.path_slash_animation = "effects/slashes/slash_spear";
            spearOfLight.quality = Rarity.R3_Legendary;
            spearOfLight.equipment_type = EquipmentType.Weapon;
            spearOfLight.name_class = "item_class_weapon";
            spearOfLight.path_icon = "ui/weapon_icons/icon_SpearOfLight";
            spearOfLight.action_attack_target = new AttackAction(sunGodFuryStrikesAttack);
            AssetManager.items.list.AddItem(spearOfLight);
            Localization.Add("Spear Of Divine Light", "Spear Of Divine Light");
            Localization.Add("SpearOfLight_description", "The Light of the Sun Flows through it!");
            addWeaponsSprite(spearOfLight.id);


            ItemAsset axeOfFury = AssetManager.items.clone("AxeOfFury", "$melee");
            axeOfFury.id = "AxeOfFury";
            axeOfFury.translation_key = "Axe Of War";
            axeOfFury.name_templates = List.Of<string>(new string[] { "sword_name" });
            axeOfFury.base_stats[S.lifespan] += 100f;
            axeOfFury.base_stats[S.attack_speed] = 1f;
            axeOfFury.base_stats[S.damage] += 25f;
            axeOfFury.base_stats[S.speed] = 1f;
            axeOfFury.base_stats[S.health] += 5f;
            axeOfFury.base_stats[S.accuracy] = 1f;
            axeOfFury.base_stats[S.range] = 1;
            axeOfFury.base_stats[S.armor] = 1;
            axeOfFury.base_stats[S.scale] = 0.01f;
            axeOfFury.base_stats[S.targets] = 3f;
            axeOfFury.base_stats[S.critical_chance] = 0.05f;
            axeOfFury.base_stats[S.knockback] = 0.05f;
            axeOfFury.base_stats[S.mass] = 0.0f;
            axeOfFury.base_stats[S.intelligence] = 0;
            axeOfFury.base_stats[S.warfare] = 10;
            axeOfFury.base_stats[S.diplomacy] = 0;
            axeOfFury.base_stats[S.stewardship] = 0;
            axeOfFury.base_stats[S.opinion] = 0f;
            axeOfFury.base_stats[S.loyalty_traits] = 0f;
            axeOfFury.base_stats[S.cities] = 0;
            axeOfFury.equipment_value = 3000;
            axeOfFury.path_slash_animation = "effects/slashes/slash_spear";
            axeOfFury.action_attack_target = new AttackAction(AxeMaestro);
            axeOfFury.quality = Rarity.R3_Legendary;
            axeOfFury.equipment_type = EquipmentType.Weapon;
            axeOfFury.name_class = "item_class_weapon";
            axeOfFury.path_icon = "ui/weapon_icons/icon_AxeOfFury";

            Localization.Add("Axe Of War", "Axe Of War");
            Localization.Add("AxeOfFury_description", "The Sight of my blade trembles my enemies! (they dont care)"); ///idk
            addWeaponsSprite(axeOfFury.id);

            ItemAsset darkDagger = AssetManager.items.clone("DarkDagger", "$melee");
            darkDagger.id = "DarkDagger";
            darkDagger.translation_key = "the Dark Dagger";
            darkDagger.name_templates = List.Of<string>(new string[] { "sword_name" });
            darkDagger.base_stats[S.lifespan] += 100f;
            darkDagger.base_stats[S.attack_speed] = 3f;
            darkDagger.base_stats[S.damage] += 15f;
            darkDagger.base_stats[S.speed] = 1f;
            darkDagger.base_stats[S.health] = 1;
            darkDagger.base_stats[S.accuracy] = 1f;
            darkDagger.base_stats[S.range] = 1;
            darkDagger.base_stats[S.armor] = 1;
            darkDagger.base_stats[S.targets] = 3f;
            darkDagger.base_stats[S.knockback] = 0.0f;
            darkDagger.base_stats[S.stewardship] = 5;
            darkDagger.base_stats[S.opinion] = 5f;
            darkDagger.base_stats[S.loyalty_traits] = 0f;
            darkDagger.equipment_value = 3000;
            darkDagger.path_slash_animation = "effects/slashes/darkSlash";
            darkDagger.quality = Rarity.R3_Legendary;
            darkDagger.equipment_type = EquipmentType.Weapon;
            darkDagger.name_class = "item_class_weapon";
            darkDagger.path_icon = "ui/weapon_icons/icon_DarkDagger";
            darkDagger.action_attack_target = new AttackAction(darkGodTeleportAttack);
            Localization.Add("DarkDagger_description", "The Daggers of darkness");
            Localization.Add("the Dark Dagger", "Dagger Of Darkness");
            addWeaponsSprite(darkDagger.id);


            ItemAsset maceOfDestruction = AssetManager.items.clone("MaceOfDestruction", "$range");
            maceOfDestruction.id = "MaceOfDestruction";
            maceOfDestruction.translation_key = "Mace Of Destruction";
            maceOfDestruction.name_templates = List.Of<string>(new string[] { "sword_name" });
            maceOfDestruction.base_stats[S.lifespan] += 100f;
            maceOfDestruction.base_stats[S.attack_speed] = 3f;
            maceOfDestruction.base_stats[S.damage] += 30;
            maceOfDestruction.base_stats[S.speed] += 1f;
            maceOfDestruction.base_stats[S.health] = 1;
            maceOfDestruction.base_stats[S.accuracy] = 1f;
            maceOfDestruction.base_stats[S.range] = 5;
            maceOfDestruction.base_stats[S.armor] = 1;
            maceOfDestruction.base_stats[S.targets] = 3f;
            maceOfDestruction.base_stats[S.mass] = 0.1f;
            maceOfDestruction.equipment_value = 3000;
            maceOfDestruction.path_slash_animation = "effects/slashes/slash_spear";
            maceOfDestruction.quality = Rarity.R3_Legendary;
            maceOfDestruction.equipment_type = EquipmentType.Weapon;
            maceOfDestruction.attack_type = WeaponType.Range;
            maceOfDestruction.base_stats[S.projectiles] = 1;
            maceOfDestruction.base_stats[S.damage_range] = 0.9f;
            maceOfDestruction.projectile = "boneFire";
            maceOfDestruction.name_class = "item_class_weapon";
            maceOfDestruction.path_icon = "ui/weapon_icons/icon_MaceOfDestruction";
            Localization.Add("Mace Of Destruction", "Mace Of Destruction");
            Localization.Add("MaceOfDestruction_description", "Here Comes the Chaos!");
            addWeaponsSprite(maceOfDestruction.id);
            
            ItemAsset staffOfKnowledge = AssetManager.items.clone("StaffOfKnowledge", "$range");
            staffOfKnowledge.id = "StaffOfKnowledge";
            staffOfKnowledge.translation_key = "Staff Of Knowledge";
            staffOfKnowledge.name_templates = List.Of<string>(new string[] { "sword_name" });
            staffOfKnowledge.base_stats[S.lifespan] += 100f;
            staffOfKnowledge.base_stats[S.attack_speed] = 1f;
            staffOfKnowledge.base_stats[S.speed] += 1f;
            staffOfKnowledge.base_stats[S.health] = 1;
            staffOfKnowledge.base_stats[S.accuracy] = 1f;
            staffOfKnowledge.base_stats[S.range] = 5;
            staffOfKnowledge.base_stats[S.armor] = 1;
            staffOfKnowledge.base_stats[S.targets] = 3f;
            staffOfKnowledge.base_stats[S.mass] = 0.1f;
            staffOfKnowledge.base_stats[S.intelligence] = 15f;
            staffOfKnowledge.equipment_value = 3000;
            staffOfKnowledge.path_slash_animation = "effects/slashes/darkSlash";
            staffOfKnowledge.quality = Rarity.R3_Legendary;
            staffOfKnowledge.equipment_type = EquipmentType.Weapon;
            staffOfKnowledge.attack_type = WeaponType.Range;
            staffOfKnowledge.name_class = "item_class_weapon";
            staffOfKnowledge.path_icon = "ui/weapon_icons/icon_StaffOfKnowledge";
            staffOfKnowledge.action_attack_target = new AttackAction(CorruptEnemy);
            staffOfKnowledge.base_stats[S.projectiles] = 12f;
            staffOfKnowledge.base_stats[S.damage_range] = 0.9f;
            staffOfKnowledge.projectile = "wordsOfKnowledgeProjectile";
            Localization.Add("StaffOfKnowledge_description", "Knowledge is the true source of power -guy who is holding it");
            Localization.Add("Staff Of Knowledge", "Staff Of Knowledge");
            addWeaponsSprite(staffOfKnowledge.id);
            
            ItemAsset StaffOfLove = AssetManager.items.clone("StaffOfLove", "$range");
            StaffOfLove.id = "StaffOfLove";
            StaffOfLove.translation_key = "Staff Of Love";
            StaffOfLove.name_templates = List.Of<string>(new string[] { "sword_name" });
            StaffOfLove.base_stats[S.lifespan] += 100f;
            StaffOfLove.base_stats[S.attack_speed] = 1f;
            StaffOfLove.base_stats[S.speed] += 1f;
            StaffOfLove.base_stats[S.health] = 10;
            StaffOfLove.base_stats[S.accuracy] = 1f;
            StaffOfLove.base_stats[S.range] = 20;
            StaffOfLove.base_stats[S.armor] = 1;
            StaffOfLove.base_stats[S.damage] = 10;
            StaffOfLove.base_stats[S.targets] = 3f;
            StaffOfLove.base_stats[S.critical_chance] = 0.0f;
            StaffOfLove.base_stats[S.knockback] = 0.0f;
            StaffOfLove.base_stats[S.mass] = 0.1f;
            StaffOfLove.base_stats[S.diplomacy] = 5;
            StaffOfLove.base_stats[S.stewardship] = 10;
            StaffOfLove.base_stats[S.damage_range] = 0.5f;
            StaffOfLove.equipment_value = 3000;
            StaffOfLove.path_slash_animation = "effects/slashes/darkSlash";
            StaffOfLove.quality = Rarity.R3_Legendary;
            StaffOfLove.equipment_type = EquipmentType.Weapon;
            StaffOfLove.attack_type = WeaponType.Range;
            StaffOfLove.name_class = "item_class_weapon";
            StaffOfLove.path_icon = "ui/weapon_icons/icon_StaffOfLove";
            StaffOfLove.base_stats[S.projectiles] = 1;
            StaffOfLove.base_stats[S.damage_range] = 0.9f;
            StaffOfLove.projectile = "CorruptedHeart";
            Localization.Add("StaffOfLove_description", "You get some love, you get love, everyone gets loved! except me ):");
            Localization.Add("Staff Of Love", "Staff Of Love");
            addWeaponsSprite(StaffOfLove.id);

            ItemAsset FireGodStaff = AssetManager.items.clone("HellStaff", "evil_staff");
            FireGodStaff.id = "HellStaff";
            FireGodStaff.translation_key = "The Staff Of Fire";
            FireGodStaff.name_templates = List.Of<string>(new string[] { "sword_name" });
            FireGodStaff.base_stats[S.lifespan] += 100f;
            FireGodStaff.base_stats[S.attack_speed] = 1f;
            FireGodStaff.base_stats[S.speed] += 1f;
            FireGodStaff.base_stats[S.damage] = 15;
            FireGodStaff.base_stats[S.range] = 1;
            FireGodStaff.base_stats[S.armor] = 1;
            FireGodStaff.base_stats[S.targets] = 3f;
            FireGodStaff.base_stats[S.critical_chance] += 0.1f;
            FireGodStaff.base_stats[S.knockback] = 0.0f;
            FireGodStaff.base_stats[S.mass] = 0.1f;
            FireGodStaff.base_stats[S.intelligence] += 15f;
            FireGodStaff.base_stats[S.projectiles] = 10;
            FireGodStaff.equipment_value = 3300;
            FireGodStaff.quality = Rarity.R3_Legendary;
            FireGodStaff.name_class = "item_class_weapon";
            FireGodStaff.path_icon = "ui/weapon_icons/icon_HellStaf";
            FireGodStaff.action_attack_target = new AttackAction(UnleashHell);
            AssetManager.items.list.AddItem(FireGodStaff);
            Localization.Add("The Staff Of Fire", "the Staff of fire");
            Localization.Add("HellStaff_description", "You will taste the true power of the fire side?");
            addWeaponsSprite(FireGodStaff.id);

            ItemAsset cometScepter = AssetManager.items.clone("CometScepter", "$range");
            cometScepter.id = "CometScepter";
            cometScepter.translation_key = "Scepter Of The Stars";
            cometScepter.name_templates = List.Of(new string[] { "sword_name" });
            cometScepter.base_stats[S.offspring] += 100f;
            cometScepter.base_stats[S.attack_speed] = 1f;
            cometScepter.base_stats[S.damage] += 10;
            cometScepter.base_stats[S.speed] += 1f;
            cometScepter.base_stats[S.health] = 1;
            cometScepter.base_stats[S.accuracy] = 1f;
            cometScepter.base_stats[S.range] = 1;
            cometScepter.base_stats[S.armor] = 1;
            cometScepter.base_stats[S.targets] = 3f;
            cometScepter.base_stats[S.critical_chance] += 0.1f;
            cometScepter.base_stats[S.mass] = 0.1f;
            cometScepter.base_stats[S.intelligence] += 15f;
            cometScepter.equipment_value = 3000;
            cometScepter.path_slash_animation = "effects/slashes/slash_punch";
            cometScepter.quality = Rarity.R3_Legendary;
            cometScepter.equipment_type = EquipmentType.Weapon;
            cometScepter.attack_type = WeaponType.Range;
            cometScepter.name_class = "item_class_weapon";
            cometScepter.path_icon = "ui/weapon_icons/icon_CometScepter";
            cometScepter.action_attack_target = new AttackAction(UnleashMoonFall);
            cometScepter.base_stats[S.projectiles] = 4f;
            cometScepter.base_stats[S.damage_range] = 0.9f;
            cometScepter.projectile = "starShowerProjectile";

            AssetManager.items.list.AddItem(cometScepter);
            Localization.Add("CometScepter_description", "The power of the moon is incredible! right?");
            Localization.Add("Scepter Of The Stars", "Scepter Of The Stars");
            addWeaponsSprite(cometScepter.id);

            ItemAsset hammerOfCreation = AssetManager.items.clone("HammerOfCreation", "$melee");
            hammerOfCreation.id = "HammerOfCreation";
            hammerOfCreation.translation_key = "Hammer Of Creation";
            hammerOfCreation.name_templates = List.Of<string>(new string[] { "sword_name" });
            hammerOfCreation.base_stats[S.lifespan] += 100f;
            hammerOfCreation.base_stats[S.attack_speed] = 1f;
            hammerOfCreation.base_stats[S.damage] += 15;
            hammerOfCreation.base_stats[S.speed] += 1f;
            hammerOfCreation.base_stats[S.health] = 1;
            hammerOfCreation.base_stats[S.accuracy] = 1f;
            hammerOfCreation.base_stats[S.range] = 2;
            hammerOfCreation.base_stats[S.armor] = 3;
            hammerOfCreation.base_stats[S.targets] = 3f;
            hammerOfCreation.base_stats[S.critical_chance] += 0.1f;
            hammerOfCreation.base_stats[S.knockback] = 0.2f;
            hammerOfCreation.base_stats[S.mass] = 0.1f;
            hammerOfCreation.equipment_value = 3000;
            hammerOfCreation.path_slash_animation = "effects/slashes/slash_axe";
            hammerOfCreation.quality = Rarity.R3_Legendary;
            hammerOfCreation.equipment_type = EquipmentType.Weapon;
            hammerOfCreation.name_class = "item_class_weapon";
            hammerOfCreation.path_icon = "ui/weapon_icons/icon_HammerOfCreation";
            hammerOfCreation.action_attack_target = (AttackAction)Delegate.Combine(hammerOfCreation.action_attack_target, new AttackAction(earthGodImpaleEnemy));
            AssetManager.items.list.AddItem(hammerOfCreation);
            Localization.Add("HammerOfCreation_description", "With this Hammer i hereby cast COOL SPIKES OUT OF GROUND!");
            Localization.Add("Hammer Of Creation", "Hammer Of Creation");
            addWeaponsSprite(hammerOfCreation.id);

            ItemAsset lichGodsGreatSword = AssetManager.items.clone("LichGodsGreatSword", "$melee");
            lichGodsGreatSword.id = "LichGodsGreatSword";
            lichGodsGreatSword.translation_key = "Great Sword Of The Lich";
            lichGodsGreatSword.name_templates = List.Of<string>(new string[] { "sword_name" });
            lichGodsGreatSword.base_stats[S.lifespan] += 100f;
            lichGodsGreatSword.base_stats[S.attack_speed] = 1f;
            lichGodsGreatSword.base_stats[S.damage] += 10;
            lichGodsGreatSword.base_stats[S.speed] += 1f;
            lichGodsGreatSword.base_stats[S.health] = 1;
            lichGodsGreatSword.base_stats[S.accuracy] = 1f;
            lichGodsGreatSword.base_stats[S.range] = 2;
            lichGodsGreatSword.base_stats[S.armor] = 3;
            lichGodsGreatSword.base_stats[S.targets] = 3f;
            lichGodsGreatSword.base_stats[S.critical_chance] += 0.1f;
            lichGodsGreatSword.base_stats[S.knockback] = 0.2f;
            lichGodsGreatSword.base_stats[S.mass] = 0.1f;
            lichGodsGreatSword.equipment_value = 3000;
            lichGodsGreatSword.path_slash_animation = "effects/slashes/slash_sword";
            lichGodsGreatSword.quality = Rarity.R3_Legendary;
            lichGodsGreatSword.equipment_type = EquipmentType.Weapon;
            lichGodsGreatSword.action_attack_target = new AttackAction(LichGodAttack);
            lichGodsGreatSword.name_class = "item_class_weapon";
            lichGodsGreatSword.path_icon = "ui/weapon_icons/icon_LichGodsGreatSword";
            
            AssetManager.items.list.AddItem(lichGodsGreatSword);
            Localization.Add("LichGodsGreatSword_description", "The Power of the Undead lies in this Great Sword!");
            Localization.Add("Great Sword Of The Lich", "Great Sword Of The Lich");
            addWeaponsSprite(lichGodsGreatSword.id);

            ItemAsset godHuntersScythe = AssetManager.items.clone("GodHuntersScythe", "$melee");
            godHuntersScythe.id = "GodHuntersScythe";
            godHuntersScythe.translation_key = "God Hunters Scythe";
            godHuntersScythe.name_templates = List.Of<string>(new string[] { "sword_name" });
            godHuntersScythe.base_stats[S.attack_speed] += 18f;
            godHuntersScythe.base_stats[S.damage] += 26;
            godHuntersScythe.base_stats[S.speed] += 3f;
            godHuntersScythe.base_stats[S.health] = 1;
            godHuntersScythe.base_stats[S.accuracy] = -2f;
            godHuntersScythe.base_stats[S.range] = 3;
            godHuntersScythe.base_stats[S.armor] = 2;
            godHuntersScythe.base_stats[S.targets] = 3f;
            godHuntersScythe.base_stats[S.critical_chance] += 0.1f;
            godHuntersScythe.base_stats[S.knockback] = 0.2f;
            godHuntersScythe.base_stats[S.mass] = 0.1f;
            godHuntersScythe.base_stats[S.intelligence] += 4f;
            godHuntersScythe.base_stats[S.opinion] = 20f;
            godHuntersScythe.equipment_value = 1800;
            godHuntersScythe.path_slash_animation = "effects/slashes/slash_sword";
            godHuntersScythe.quality = Rarity.R2_Epic;
            godHuntersScythe.equipment_type = EquipmentType.Weapon;
            godHuntersScythe.name_class = "item_class_weapon";
            godHuntersScythe.path_icon = "ui/weapon_icons/icon_GodHuntersScythe";
            godHuntersScythe.action_attack_target = new AttackAction(GodHunterAttack);

            AssetManager.items.list.AddItem(godHuntersScythe);
            Localization.Add("GodHuntersScythe_description", "Why did the god hunters choose this weapon? it Deals 5x more damage to Gods AND it only costs 10.99!!!!!!");
            Localization.Add("God Hunters Scythe", "The God Hunters Scythe");
            addWeaponsSprite(godHuntersScythe.id);
        }

        static bool Flame(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null)
            {
                if (Randy.randomChance(80.0f))
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
            if (Randy.randomChance(GetEnhancedChance("God Of Knowledge", "CorruptEnemy%")))
            {
                foreach (Actor a in Finder.getUnitsFromChunk(pTile, 1, 8))
                {
                    if(IsGod(a) || a.kingdom == pSelf.kingdom) continue;
                    CorruptActor(a, pSelf.a);
                }
                MusicBox.playSound("event:/SFX/EXPLOSIONS/ExplosionForce", pTile, false, false);
                SpawnCustomWave(pTile.posV3, 0.025f, -0.05f, 2);
            }
            return true;
        }
        static bool UnleashMoonFall(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Randy.randomChance(GetEnhancedChance("God Of the Stars", "summonMoonChunk%")))
            {
                Vector2Int pos = pTile.pos; // Position of the Ptile as a Vector 2
                float pDist = Vector2.Distance(pTarget.current_position, pos); // the distance between the target and the pTile
                Vector3 newPoint = Toolbox.getNewPoint(pSelf.current_position.x + 35f, pSelf.current_position.y + 95f, (float)pos.x + 1f, (float)pos.y + 1f, pDist, true); // the Point of the projectile launcher 
                Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.current_position.x, pTarget.current_position.y, (float)pos.x, (float)pos.y, pTarget.a.stats[S.size], true);
                World.world.projectiles.spawn(pSelf, pTarget, "moonFall", newPoint, newPoint2, 0.0f);
                pSelf.a.addStatusEffect("invincible", 2f);
            }
            return true;
        }
        static bool AxeMaestro(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            pSelf.a.data.get("MaelStrom", out bool MaelStorm, false);
            if (!MaelStorm && Randy.randomChance(GetEnhancedChance("God Of War", "axemaelstrom%")))
            {
                List<Actor> Targets = GetAlliesOfActor(Finder.getUnitsFromChunk(pSelf.current_tile, 1, 12), pTarget);
                if(Targets.Count == 0)
                {
                    return false;
                }
                for (int i = 0; i < Randy.randomInt(10, 21); i++)
                {
                    ShootCustomProjectile(pSelf, Targets[i % Targets.Count], "WarAxeProjectile1", 1);
                }
            }
            return true;
        }

        public static bool sunGodFuryStrikesAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (pTarget != null)
            {
                if (Randy.randomChance(0.9f))
                {

                    Vector2Int pos = pTile.pos; // Position of the Ptile as a Vector 2
                    float pDist = Vector2.Distance(pTarget.current_position, pos); // the distance between the target and the pTile
                    Vector3 newPoint = Toolbox.getNewPoint(pTarget.current_position.x, pTarget.current_position.y, (float)pos.x, (float)pos.y, pDist, true); // the Point of the projectile launcher 
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.current_position.x, pTarget.current_position.y, (float)pos.x, (float)pos.y, pTarget.stats[S.size], true);
                    World.world.projectiles.spawn(pSelf, pTarget, "lightSlashesProjectile", newPoint, newPoint2, 0.0f);
                }
                return true;
            }
            return false;
        }

        public static bool darkGodTeleportAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (pTarget != null)
            {
                if (Randy.randomChance(GetEnhancedChance("God Of the Night", "DarkDash%")))
                {
                    PushActor(pSelf.a, pTarget.current_tile.pos, 4, 0.1f);
                    foreach(BaseSimObject enemy in Finder.getUnitsFromChunk(pTile, 1, 4)){
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
            }
            return true;
        }

        public static bool UnleashHell(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (!pSelf.hasStatus("Lassering") && Randy.randomChance(GetEnhancedChance("God Of Fire", "ChaosLaser%")))
            {
                pSelf.addStatusEffect("Lassering", 10);
            }
            return true;
        }

        

        public static bool LichGodAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (pTarget != null)
            {

                if (Randy.randomChance(GetEnhancedChance("God Of The Lich", "waveOfMutilation%")))
                {
                    Vector2Int pos = pTile.pos; // Position of the Ptile as a Vector 2
                    float pDist = Vector2.Distance(pTarget.current_position, pos); // the distance between the target and the pTile
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.current_position.x, pSelf.current_position.y, (float)pos.x, (float)pos.y, pDist, true); // the Point of the projectile launcher 
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.current_position.x, pTarget.current_position.y, (float)pos.x, (float)pos.y, pTarget.a.stats[S.size], true);
                    World.world.projectiles.spawn(pSelf, pTarget, "waveOfMutilationProjectile", newPoint, newPoint2, 0.0f);
                }


            }
            return true;
        }

            public static bool earthGodImpaleEnemy(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
            {   
                if (Randy.randomChance(GetEnhancedChance("God Of the Earth", "StalagmitePath%")))
                {
                    CreateStalagmites(pTile, pSelf, pTarget);
                }
                return true;
            }

        

        public static void CreateStalagmites(WorldTile pTile, BaseSimObject pSelf, BaseSimObject pTarget)
        {
            (EffectsLibrary.spawn("fx_Stalagmite_path", pTile) as StalagmitePath)?.Init(pSelf.current_tile, pTarget.current_tile, 0.15f, pSelf);
        }


        static void addWeaponsSprite(string id)
        {
            var dictItems = ActorAnimationLoader._dict_items;
            var sprite = Resources.Load<Sprite>("weapons/"+id);
            dictItems.Add("w_"+id, new List<Sprite>() { sprite });
        }
    }
}

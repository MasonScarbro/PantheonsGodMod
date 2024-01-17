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
            spearOfLight.base_stats[S.attack_speed] = 10f;
            spearOfLight.base_stats[S.damage] = 2;
            spearOfLight.base_stats[S.speed] += 8f;
            spearOfLight.base_stats[S.health] = 1;
            spearOfLight.base_stats[S.accuracy] = 1f;
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
            spearOfLight.equipment_value = 30;
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
            axeOfFury.base_stats[S.max_age] = 0f;
            axeOfFury.base_stats[S.attack_speed] = 10f;
            axeOfFury.base_stats[S.damage] = 2;
            axeOfFury.base_stats[S.speed] = 8f;
            axeOfFury.base_stats[S.health] = 1;
            axeOfFury.base_stats[S.accuracy] = 1f;
            axeOfFury.base_stats[S.range] = 1;
            axeOfFury.base_stats[S.armor] = 3;
            axeOfFury.base_stats[S.scale] = 0.0f;
            axeOfFury.base_stats[S.dodge] = 10f;
            axeOfFury.base_stats[S.targets] = 3f;
            axeOfFury.base_stats[S.critical_chance] = 0.0f;
            axeOfFury.base_stats[S.knockback] = 0.0f;
            axeOfFury.base_stats[S.knockback_reduction] = 0.0f;
            axeOfFury.base_stats[S.intelligence] = 0;
            axeOfFury.base_stats[S.warfare] = 0;
            axeOfFury.base_stats[S.diplomacy] = 0;
            axeOfFury.base_stats[S.stewardship] = 0;
            axeOfFury.base_stats[S.opinion] = 0f;
            axeOfFury.base_stats[S.loyalty_traits] = 0f;
            axeOfFury.base_stats[S.cities] = 0;
            axeOfFury.base_stats[S.zone_range] = 0.1f;
            axeOfFury.equipment_value = 30;
            axeOfFury.path_slash_animation = "effects/slashes/slash_spear";
            axeOfFury.tech_needed = "weapon_axe";
            axeOfFury.quality = ItemQuality.Legendary;
            axeOfFury.equipmentType = EquipmentType.Weapon;
            axeOfFury.name_class = "item_class_weapon";
            axeOfFury.path_icon = "ui/weapon_icons/icon_axeOfFury_adamantine";
            // For Ranged Weapons use "_range"
            //.base_stats[S.projectiles] = 12f;
            //.base_stats[S.damage_range] = 0.9f;
            //.projectile = "rock" 
            axeOfFury.action_attack_target = new AttackAction(sunGodFuryStrikesAttack);
            AssetManager.items.list.AddItem(axeOfFury);
            Localization.addLocalization("item_axeOfFury", "Spear Of Divine Light");
            addWeaponsSprite(axeOfFury.id, axeOfFury.materials[0]);


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
                    if (Toolbox.randomChance(0.7f))
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

            static void addWeaponsSprite(string id, string material)
            {
                var dictItems = Reflection.GetField(typeof(ActorAnimationLoader), null, "dictItems") as Dictionary<string, Sprite>;
                var sprite = Resources.Load<Sprite>("weapons/w_" + id + "_" + material);
                dictItems.Add(sprite.name, sprite);
            }

            public static void addWeaponToLocalizedLibrary(string id, string description)
            {

                string language = Reflection.GetField(LocalizedTextManager.instance.GetType(), LocalizedTextManager.instance, "language") as string;
                Dictionary<string, string> localizedText = Reflection.GetField(LocalizedTextManager.instance.GetType(), LocalizedTextManager.instance, "localizedText") as Dictionary<string, string>;
                localizedText.Add("item_" + id, id);
                localizedText.Add("trait_" + id + "_info", description);

            }
        
    }
}
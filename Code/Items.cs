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


            ItemAsset spearOfLight = AssetManager.items.clone("SpearOfLight", "spear");
            spearOfLight.id = "SpearOfLight";
            spearOfLight.name_templates = Toolbox.splitStringIntoList(new string[]
            {
              "spear_name#30",
              "spear_name_king#3",
              "weapon_name_city",
              "weapon_name_kingdom",
              "weapon_name_culture",
              "weapon_name_enemy_king",
              "weapon_name_enemy_kingdom"
            });
            spearOfLight.materials = List.Of<string>(new string[] { "adamantine" });
            spearOfLight.base_stats[S.fertility] = 0.0f;
            spearOfLight.base_stats[S.max_children] = 0f;
            spearOfLight.base_stats[S.max_age] = 0f;
            spearOfLight.base_stats[S.attack_speed] = 10f;
            spearOfLight.base_stats[S.damage] = 2;
            spearOfLight.base_stats[S.speed] = 8f;
            spearOfLight.base_stats[S.health] = 1;
            spearOfLight.base_stats[S.accuracy] = 1f;
            spearOfLight.base_stats[S.range] = 1;
            spearOfLight.base_stats[S.armor] = 3;
            spearOfLight.base_stats[S.scale] = 0.0f;
            spearOfLight.base_stats[S.dodge] = 10f;
            spearOfLight.base_stats[S.targets] = 3f;
            spearOfLight.base_stats[S.critical_chance] = 0.0f;
            spearOfLight.base_stats[S.knockback] = 0.0f;
            spearOfLight.base_stats[S.knockback_reduction] = 0.0f;
            spearOfLight.base_stats[S.intelligence] = 0;
            spearOfLight.base_stats[S.warfare] = 0;
            spearOfLight.base_stats[S.diplomacy] = 0;
            spearOfLight.base_stats[S.stewardship] = 0;
            spearOfLight.base_stats[S.opinion] = 0f;
            spearOfLight.base_stats[S.loyalty_traits] = 0f;
            spearOfLight.base_stats[S.cities] = 0;
            spearOfLight.base_stats[S.zone_range] = 0.1f;
            spearOfLight.equipment_value = 20;
            spearOfLight.path_slash_animation = "effects/slashes/slash_spear";
            spearOfLight.tech_needed = "weapon_spear";
            spearOfLight.equipmentType = EquipmentType.Weapon;
            spearOfLight.name_class = "item_class_weapon";
            spearOfLight.action_special_effect = new WorldAction(NoneRegularAction);
            spearOfLight.action_attack_target = new AttackAction(Flame);
            AssetManager.items.list.AddItem(spearOfLight);
            addWeaponToLocalizedLibrary("SpearOfLight", "A radiant spear forged from pure sunlight. Its blade gleams with divine energy, capable of slicing through darkness and incinerating foes.");
            addWeaponsSprite(spearOfLight.id, spearOfLight.materials[0]);


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
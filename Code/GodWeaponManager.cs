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
using ai;
using HarmonyLib;
using NCMS.Utils;

public static class GodWeaponManager
{
    private static bool hasHappend = false;

    public static bool godGiveWeapon(BaseSimObject pTarget, WorldTile pTile)
    {
        //Debug.Log(hasGodGivenWeapon); 
        if (hasGodGivenWeapon == true)
        {
            return false;
        }

        if (pTarget.a != null)
        {

            if (pTarget.a.hasTrait("God Of War"))
            {
                ItemData axeOfFuryD = ItemGenerator.generateItem(AssetManager.items.get("AxeOfFury"), "adamantine", 0, null, null, 1, pTarget.a);
                pTarget.a.equipment.getSlot(EquipmentType.Weapon).setItem(axeOfFuryD);

            }

            if (pTarget.a.hasTrait("God Of the Night"))
            {
                ItemData darkDaggerD = ItemGenerator.generateItem(AssetManager.items.get("DarkDagger"), "adamantine", 0, null, null, 1, pTarget.a);
                pTarget.a.equipment.getSlot(EquipmentType.Weapon).setItem(darkDaggerD);

            }

            if (pTarget.a.hasTrait("God Of light"))
            {
                ItemData spearOfLightD = ItemGenerator.generateItem(AssetManager.items.get("SpearOfLight"), "adamantine", 0, null, null, 1, pTarget.a);
                pTarget.a.equipment.getSlot(EquipmentType.Weapon).setItem(spearOfLightD);

            }
            hasHappend = true;
            pTarget.a.updateStats();
        }
        
        return false;
    }
}
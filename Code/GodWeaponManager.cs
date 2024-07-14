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
    public static int count = 0;

    public static bool godGiveWeapon(BaseSimObject pTarget, WorldTile pTile)
    {

       /* if (count == 1)
        {
            Debug.Log("IGNORE THIS ERROR AND KEEP PLAYING!");
        }*/ //why is this here??? just uses more cpu

        if (pTarget.a != null)
        {
            bool gottenweapon = false;
            pTarget.a.data.get("gottenweapon", out gottenweapon);
            if (pTarget.a.asset.use_items && !gottenweapon)
            {
            if (pTarget.a.hasTrait("God Of War"))
            {
                ItemData axeOfFuryD = new ItemData();
                axeOfFuryD.id = "AxeOfFury";
                axeOfFuryD.material = "adamantine";
                pTarget.a.equipment.getSlot(EquipmentType.Weapon).setItem(axeOfFuryD);
                pTarget.a.setStatsDirty();
            }

            if (pTarget.a.hasTrait("God Of the Night"))
            {
                ItemData darkDaggerD = new ItemData();
                darkDaggerD.id = "DarkDagger";
                darkDaggerD.material = "adamantine";
                var weaponSlot = pTarget.a.equipment.getSlot(EquipmentType.Weapon);
                if (weaponSlot != null)
                {
                    weaponSlot.setItem(darkDaggerD);
                }

                pTarget.a.setStatsDirty();
            }

            if (pTarget.a.hasTrait("God Of light"))
            {
                ItemData spearOfLightD = new ItemData();
                spearOfLightD.id = "SpearOfLight";
                spearOfLightD.material = "adamantine";

                var weaponSlot = pTarget.a.equipment.getSlot(EquipmentType.Weapon);
                if (weaponSlot != null)
                {
                    weaponSlot.setItem(spearOfLightD);
                }


                pTarget.a.setStatsDirty();

            }

            if (pTarget.a.hasTrait("God Of Knowledge"))
            {
                ItemData staffOfKnowledgeD = new ItemData();
                staffOfKnowledgeD.id = "StaffOfKnowledge";
                staffOfKnowledgeD.material = "base";

                var weaponSlot = pTarget.a.equipment.getSlot(EquipmentType.Weapon);
                if (weaponSlot != null)
                {
                    weaponSlot.setItem(staffOfKnowledgeD);
                }
                pTarget.a.setStatsDirty();

            }

            if (pTarget.a.hasTrait("God_Of_Chaos"))
            {
                ItemData maceOfDestructionD = new ItemData();
                maceOfDestructionD.id = "MaceOfDestruction";
                maceOfDestructionD.material = "adamantine";
                pTarget.a.equipment.getSlot(EquipmentType.Weapon).setItem(maceOfDestructionD);
                pTarget.a.setStatsDirty();

            }

            if (pTarget.a.hasTrait("God Of the Stars"))
            {
                ItemData cometScepterD = new ItemData();
                cometScepterD.id = "CometScepter";
                cometScepterD.material = "base";
                pTarget.a.equipment.getSlot(EquipmentType.Weapon).setItem(cometScepterD);
                pTarget.a.setStatsDirty();

            }

            if (pTarget.a.hasTrait("God Of The Lich"))
            {
                ItemData lichGodsGreatSwordD = new ItemData();
                lichGodsGreatSwordD.id = "LichGodsGreatSword";
                lichGodsGreatSwordD.material = "base";
                pTarget.a.equipment.getSlot(EquipmentType.Weapon).setItem(lichGodsGreatSwordD);
                pTarget.a.setStatsDirty();

            }


            if (pTarget.a.hasTrait("God Of the Earth"))
            {
                //Debug.Log("Why No Activate!?!?!?");
                ItemData hammerOfCreationD = new ItemData();
                hammerOfCreationD.id = "HammerOfCreation";
                hammerOfCreationD.material = "base";
                pTarget.a.equipment.getSlot(EquipmentType.Weapon).setItem(hammerOfCreationD);
                pTarget.a.setStatsDirty();

            }


            if (pTarget.a.hasTrait("God Hunter"))
            {
                //Debug.Log("Why No Activate!?!?!?");
                ItemData godHuntersScythe = new ItemData();
                godHuntersScythe.id = "GodHuntersScytheBlank";
                godHuntersScythe.material = "base";
                pTarget.a.equipment.getSlot(EquipmentType.Weapon).setItem(godHuntersScythe);
                pTarget.a.setStatsDirty();

            }
            pTarget.a.updateStats();
            count++;
            pTarget.a.data.set("gottenweapon", true);
            return true;
            }
        }


        return false;
    }

}


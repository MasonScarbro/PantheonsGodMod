using System;
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
    class Units
    {
        public static void init()
        {
            //this is bat
            var godHunter = AssetManager.actor_library.clone("GodHunter", "_mob");
            godHunter.id = "GodHunter";
            godHunter.nameLocale = "God Hunter";
            //godHunter.nameTemplate = "unit_human";
            godHunter.race = SK.undead;
            godHunter.kingdom = SK.evil;
            godHunter.base_stats[S.health] = 600;
            godHunter.base_stats[S.speed] = 10f;
            godHunter.base_stats[S.attack_speed] = 20f;
            godHunter.base_stats[S.dodge] = 10f;
            //godHunter.inspectAvatarScale = 1f;
            godHunter.icon = "iconButterfly";
            godHunter.job = "move_mob";
            //godHunter.flying = true;
            //godHunter.has_soul = true; //no idea what this do?
            //godHunter.traits.Add("GokuAttk");
            godHunter.disablePunchAttackAnimation = false;
            godHunter.disableJumpAnimation = false;
            godHunter.canTurnIntoZombie = false;
            godHunter.canBeKilledByDivineLight = true;
            godHunter.canBeMovedByPowers = true;
            godHunter.canBeHurtByPowers = true;
            godHunter.dieByLightning = true;
            godHunter.dieInLava = true;
            godHunter.canBeKilledByLifeEraser = true;
            godHunter.canBeKilledByStuff = true;
            godHunter.canReceiveTraits = true;
            godHunter.canAttackBuildings = true;
            godHunter.needFood = false;
            godHunter.base_stats[S.max_age] = 1000;
            //godHunter.base_stats[S.scale] = 0.03f;
            godHunter.fmod_spawn = "event:/SFX/UNITS/Butterfly/ButterflySpawn";
            godHunter.fmod_attack = "event:/SFX/UNITS/Butterfly/ButterflyAttack";
            godHunter.fmod_death = "event:/SFX/UNITS/Butterfly/ButterflyDeath";
            godHunter.oceanCreature = false; ;
            godHunter.landCreature = true;
            godHunter.swampCreature = false;
            godHunter.animation_walk = "walk_0,walk_1,walk_2,walk_3";
            godHunter.animation_swim = "swim_0,swim_1,swim_2,swim_3";
            godHunter.texture_path = "GodHunter";
            AssetManager.actor_library.CallMethod("addTrait", "eagle_eyed");
            AssetManager.actor_library.CallMethod("addTrait", "strong");
            AssetManager.actor_library.add(godHunter);
            Localization.addLocalization(godHunter.nameLocale, godHunter.nameLocale);
        }
    }
}
using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NCMS;
using NCMS.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ReflectionUtility;
using HarmonyLib;

namespace GodsAndPantheons
{
    class Units
    {
        public static void init()
        {
            
            loadAssets();
        }

        public static void loadAssets()
        {
            var godHunter = AssetManager.actor_library.clone("GodHunter", "_mob");
            godHunter.id = "GodHunter";
            godHunter.nameLocale = "God Hunter";
            godHunter.nameTemplate = "unit_human";
            godHunter.race = "human";
            godHunter.kingdom = SK.evil;
            godHunter.base_stats[S.health] = 600;
            godHunter.base_stats[S.speed] = 10f;
            godHunter.base_stats[S.attack_speed] = 20f;
            godHunter.base_stats[S.dodge] = 10f;
            //godHunter.inspectAvatarScale = 1f;
            godHunter.icon = "godKiller";
            godHunter.job = "attacker";
            godHunter.flying = false;
            godHunter.inspect_experience = true;
            godHunter.canBeCitizen = false;
            godHunter.inspect_kills = true;
            godHunter.hideOnMinimap = false;
            godHunter.use_items = false;
            godHunter.defaultAttack = "base";
            
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
            //godHunter.fmod_spawn = "event:/SFX/UNITS/Butterfly/ButterflySpawn";
            //godHunter.fmod_attack = "event:/SFX/UNITS/Butterfly/ButterflyAttack";
            //godHunter.fmod_death = "event:/SFX/UNITS/Butterfly/ButterflyDeath";
            godHunter.oceanCreature = false; ;
            godHunter.landCreature = true;
            godHunter.swampCreature = false;
            godHunter.animation_walk = "walk_0,walk_1,walk_2,walk_3";
            godHunter.animation_idle = "walk_0,walk_1,walk_2,walk_3";
            godHunter.animation_swim = "swim_0,swim_1,swim_2,swim_3";
            godHunter.texture_path = "GodHunter";
            AssetManager.actor_library.CallMethod("addTrait", "eagle_eyed");
            AssetManager.actor_library.CallMethod("addTrait", "strong");
            AssetManager.actor_library.add(godHunter);
            Localization.addLocalization(godHunter.nameLocale, godHunter.nameLocale);
        }
    }
}
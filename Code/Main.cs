/* 
AUTHOR: MASON SCARBRO
VERSION: 1.0.0
*/
using System;
using NCMS;
using UnityEngine;
using ReflectionUtility;
using HarmonyLib;
using NCMS.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace GodsAndPantheons
{
    [ModEntry]
    class Main : MonoBehaviour
    {
        void Awake()
        {
            Effects.init();
            Traits.init();
            NewProjectiles.init();
            NewTerraformOptions.init();
            NewEffects.init();
            Items.init();
            Units.init();
            Tab.init();
            Buttons.init();
        }
        public static Dictionary<Actor, Actor> listOfTamedBeasts = new Dictionary<Actor, Actor>();
    }
}
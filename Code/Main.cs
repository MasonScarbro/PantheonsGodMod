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
        }
        
    }
}
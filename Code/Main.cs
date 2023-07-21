/* 
AUTHOR: MASON SCARBRO
VERSION: 0.4.1
*/
using System;
using NCMS;
using UnityEngine;
using ReflectionUtility;


namespace GodsAndPantheons
{
    [ModEntry]
    class Main : MonoBehaviour
    {
        void Awake()
        {
            Effects.init();
            Traits.init();
        }
        
    }
}
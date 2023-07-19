/* 
AUTHOR: MASON SCARBRO
VERSION: 0.3.0
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
            Traits.init();
        }
        
    }
}
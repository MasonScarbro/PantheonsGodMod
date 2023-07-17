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
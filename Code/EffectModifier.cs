using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GodsAndPantheons
{
    public class EffectModifier : MonoBehaviour
    {
        public void RemoveModifier()
        {
            transform.localScale = Vector3.one;
            transform.GetComponent<SpriteRenderer>().color = Color.white;
            DestroyImmediate(this);
        }
    }
}

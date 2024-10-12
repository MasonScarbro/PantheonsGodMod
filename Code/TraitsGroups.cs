using System.Collections.Generic;
using ReflectionUtility;
using UnityEngine;

namespace GodsAndPantheons
{
    class Group
    {
        public static void init()
        {
 
            ActorTraitGroupAsset MTtraits = new ActorTraitGroupAsset();
            MTtraits.id = "GodTraits";
            MTtraits.name = "The Traits Of Gods";
            MTtraits.color = Color.black;
            AssetManager.trait_groups.add(MTtraits);
            addTraitGroupToLocalizedLibrary(MTtraits.id, "Godly Traits");
 
 
        }
        private static void addTraitGroupToLocalizedLibrary(string id, string name)
        {
            Dictionary<string, string> localizedText = LocalizedTextManager.instance.localizedText;
            localizedText.Add("trait_group_" + id, name);
        }
    }
}

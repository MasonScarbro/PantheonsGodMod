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
            MTtraits.color = "000000";
            AssetManager.trait_groups.add(MTtraits);
            addTraitGroupToLocalizedLibrary(MTtraits.name, "Godly Traits");

            ActorTraitGroupAsset NTraits = new ActorTraitGroupAsset();
            NTraits.id = "NonGodTraits";
            NTraits.name = "Non God Traits But Still Special";
            NTraits.color = "FFFFFF";
            AssetManager.trait_groups.add(NTraits);
            addTraitGroupToLocalizedLibrary(NTraits.name, "Non Godly Traits");
        }
        private static void addTraitGroupToLocalizedLibrary(string id, string name)
        {
            Dictionary<string, string> localizedText = LocalizedTextManager.instance._localized_text;
            localizedText.Add(id, name);
        }
    }
}

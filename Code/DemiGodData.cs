using System;
using System.Collections.Generic;

namespace GodsAndPantheons
{
    [Serializable]
    public class DemiGodData
    {
        public BaseStats BaseStats = new BaseStats();
        public void AddBaseStat(string stat, float Num)
        {
            BaseStats[stat] += Num;
        }
        public void InheritTrait(string trait)
        {
            GodsAndAbilities.TryAdd(trait, new List<int>());
        }
        public void AddAbility(string God, int Index)
        {
            GodsAndAbilities[God].Add(Index);
        }
        public IReadOnlyCollection<string> InheritedGodTraits
        {
            get
            {
                return GodsAndAbilities.Keys;
            }
        }
        public Dictionary<string, List<int>> GodsAndAbilities = new Dictionary<string, List<int>>();
    }
}

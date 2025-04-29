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
        public bool InheritTrait(string trait)
        {
            return GodsAndAbilities.TryAdd(trait, new HashSet<int>());
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
        public Dictionary<string, HashSet<int>> GodsAndAbilities = new Dictionary<string, HashSet<int>>();
    }
}

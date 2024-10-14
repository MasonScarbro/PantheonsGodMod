using System;
using System.Collections.Generic;
using System.Text;
using static GodsAndPantheons.Traits;
namespace GodsAndPantheons
{
    internal class NewOpinions
    {
        public static void init()
        {
            OpinionAsset EnemyGods = new OpinionAsset();
            EnemyGods.id = "Enemy_Gods";
            EnemyGods.translation_key = "Enemy Gods";
            EnemyGods.calc = EnemyGodKings;
            AssetManager.opinion_library.add(EnemyGods);

            OpinionAsset NoRespect = new OpinionAsset();
            NoRespect.id = "No Respect";
            NoRespect.translation_key = "The Other King Isnt A God!";
            NoRespect.calc = NoRespectForKings;
            AssetManager.opinion_library.add(NoRespect);

            OpinionAsset ThereCanOnlyBeOne = new OpinionAsset();
            ThereCanOnlyBeOne.id = "There Can Only Be One";
            ThereCanOnlyBeOne.translation_key = "There Can Only Be One God!";
            ThereCanOnlyBeOne.calc = ThereCanOnlyBeOneGod;
            AssetManager.opinion_library.add(ThereCanOnlyBeOne);

            OpinionAsset MyAgeHasBegun = new OpinionAsset();
            MyAgeHasBegun.id = "My Age Has Begun";
            MyAgeHasBegun.translation_key = "My Age Has Begun!";
            MyAgeHasBegun.calc = MyAgeHasBegunn;
            AssetManager.opinion_library.add(MyAgeHasBegun);
        }
        public static int MyAgeHasBegunn(Kingdom Self, Kingdom Target)
        {
            if (!Main.savedSettings.DiplomacyChanges)
            {
                return 0;
            }
            if(Self.king == null)
            {
                return 0;
            }
            Actor myking = Self.king;
            if (!IsGod(myking))
            {
                return 0;
            }
            foreach(string trait in GetGodTraits(myking))
            {
                if (World.world_era.id == TraitEras[trait].Key)
                {
                    return -20;
                }
            }
            return 0;
        }
        public static int ThereCanOnlyBeOneGod(Kingdom Self, Kingdom Target)
        {
            if (!Main.savedSettings.DiplomacyChanges)
            {
                return 0;
            }
            if (Self.king == null || Target.king == null)
            {
                return 0;
            }
            Actor myking = Self.king;
            Actor enemyking = Target.king;
            if (!IsGod(myking) || !IsGod(enemyking))
            {
                return 0;
            }
            foreach(string trait in GetGodTraits(myking))
            {
                if (enemyking.hasTrait(trait))
                {
                    return -30;
                }
            }
            return 0;
        }
        public static int EnemyGodKings(Kingdom Self, Kingdom Target)
        {
            if (!Main.savedSettings.DiplomacyChanges)
            {
                return 0;
            }
            if (Self.king == null || Target.king == null)
            {
                return 0;
            }
            Actor myking = Self.king;
            Actor enemyking = Target.king;
            if (!IsGod(myking) || !IsGod(enemyking))
            {
                return 0;
            }
            foreach(string trait in GodEnemies.Keys)
            {
                if (myking.hasTrait(trait))
                {
                    foreach(string enemytrait in GodEnemies[trait])
                    {
                        if (enemyking.hasTrait(enemytrait))
                        {
                            return -15;
                        }
                    }
                }
            }
            return 0;
        }
        public static int NoRespectForKings(Kingdom Self, Kingdom Target)
        {
            if (!Main.savedSettings.DiplomacyChanges)
            {
                return 0;
            }
            if (Self.king == null || Target.king == null)
            {
                return 0;
            }
            if(IsGod(Self.king) && !IsGod(Target.king))
            {
                return -5;
            }
            return 0;
        }
    }
}

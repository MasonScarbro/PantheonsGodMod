using NeoModLoader.General;
using static GodsAndPantheons.Traits;
namespace GodsAndPantheons
{
    public class NewOpinions
    {
        public static void init()
        {
            OpinionAsset EnemyGods = new OpinionAsset();
            EnemyGods.id = "Enemy_Gods";
            EnemyGods.translation_key = "Enemy Gods";
            EnemyGods.calc = EnemyGodKings;
            AssetManager.opinion_library.add(EnemyGods);
            LM.AddToCurrentLocale("Enemy Gods", "Enemy Gods");

            OpinionAsset NoRespect = new OpinionAsset();
            NoRespect.id = "No Respect";
            NoRespect.translation_key = "The Other King Isnt A God!";
            NoRespect.calc = NoRespectForKings;
            AssetManager.opinion_library.add(NoRespect);
            LM.AddToCurrentLocale("The Other King Isnt A God!", "The Other King Isnt A God!");

            OpinionAsset ThereCanOnlyBeOne = new OpinionAsset();
            ThereCanOnlyBeOne.id = "There Can Only Be One";
            ThereCanOnlyBeOne.translation_key = "There Can Only Be One God!";
            ThereCanOnlyBeOne.calc = ThereCanOnlyBeOneGod;
            AssetManager.opinion_library.add(ThereCanOnlyBeOne);
            LM.AddToCurrentLocale("There Can Only Be One God!", "There Can Only Be One God!");

            OpinionAsset MyAgeHasBegun = new OpinionAsset();
            MyAgeHasBegun.id = "My Age Has Begun";
            MyAgeHasBegun.translation_key = "My Age Has Begun!";
            MyAgeHasBegun.calc = MyAgeHasBegunn;
            AssetManager.opinion_library.add(MyAgeHasBegun);
            LM.AddToCurrentLocale("My Age Has Begun!", "My Age Has Begun!");
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
            if (!myking.IsGod())
            {
                return 0;
            }
            foreach(string trait in myking.GetGodTraits())
            {
                if (World.world_era.id == TraitEras[trait].Key)
                {
                    return -80;
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
            foreach(string trait in Self.king.GetGodTraits())
            {
                if (Target.king.hasTrait(trait))
                {
                    return -120;
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
            if (!myking.IsGod() || !enemyking.IsGod())
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
                            return -60;
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
            if(Self.king.IsGod() && !Target.king.IsGod())
            {
                return -20;
            }
            return 0;
        }
    }
}

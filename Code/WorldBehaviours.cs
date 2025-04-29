using System;
using System.Collections.Generic;
using System.Text;
using static GodsAndPantheons.Traits;
namespace GodsAndPantheons
{
    public class WorldBehaviours
    {
        static WorldLogAsset DevineMiracle;
        public static void init()
        {
            WorldBehaviourAsset Behaviour = AssetManager.world_behaviours.add(new WorldBehaviourAsset());
            Behaviour.id = "Devine Miracles";
            Behaviour.interval_random = 0;
            Behaviour.interval = 0;
            Behaviour.stop_when_world_on_pause = true;
            Behaviour.action = TryDivineMiracles;
            Behaviour.manager = new WorldBehaviour(Behaviour);
            DevineMiracle = AssetManager.world_log_library.add(new WorldLogAsset() { id = "Devine Miracles" });
            DevineMiracle.text_replacer = GetText;
            DevineMiracle.path_icon = "ui/icons/actor_traits/iconNightchild";
        }
        public static float Timer = 30;
        public static void TryDivineMiracles()
        {
            MapBox instance = World.world;
            if (blessingtime > 0)
            {
                blessingtime -= instance.elapsed;
                pb.flashPixel(LuckyOnee.current_tile);
                pb.divineLightFX(LuckyOnee.current_tile, null);
                pb.drawDivineLight(LuckyOnee.current_tile, null);
                SuperRegeneration(LuckyOnee, 100, 25);
            }
            else if (LuckyOnee != null)
            {
                blessingtime = 0;
                ActionLibrary.castShieldOnHimself(null, LuckyOnee, null);
                LuckyOnee = null;
            }
            if (!Main.savedSettings.DevineMiracles)
            {
                return;
            }
            Timer -= instance.elapsed;
            if (Timer > 0)
            {
                return;
            }
            Timer = 30;
            if (!Randy.randomChance(0.002f))
            {
                return;
            }
            DivineMiracle();
        }
        public static bool DivineMiracle()
        {
            List<Kingdom> list = World.world.kingdoms.list;
            List<string> availabletraits = getavailblegodtraits();
            if (availabletraits.Count == 0)
            {
                return false;
            }
            availabletraits.Shuffle();
            list.Shuffle();
            foreach (Kingdom k in list)
            {
                if (!DoesKingdomHaveGod(k))
                {
                    List<Actor> units = new List<Actor>(k.units);
                    units.Shuffle();
                    foreach (Actor a in units)
                    {
                        if (!a.hasTrait("infertile") && !a.hasTrait("Demi God") && !a.hasTrait("Lesser God"))
                        {
                            DivineMiracle(a, availabletraits[0]);
                            Timer = 300;
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        static Actor LuckyOnee;
        static float blessingtime = 0;
        public static void DivineMiracle(Actor LuckyOne, string godtrait)
        {
            World.world.startShake(1f, 0.01f, 2f, true, true);
            LuckyOne.addTrait(godtrait);
            LuckyOnee = LuckyOne;
            blessingtime = 10;
            new WorldLogMessage(DevineMiracle, LuckyOne.kingdom.name)
            {
                unit = LuckyOne,
                location = LuckyOne.current_position,
                color_special1 = LuckyOne.kingdom.kingdomColor.getColorText(),
                color_special2 = LuckyOne.kingdom.kingdomColor.getColor32Main()
            }.add();
        }
        public static void GetText(WorldLogMessage log, ref string s)
        {
            s = $"A Divine Miracle Has Occurred in {log.special1}!";
        }
    }
}

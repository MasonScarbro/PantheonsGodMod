
using System.Collections.Generic;
using UnityEngine;

namespace GodsAndPantheons
{
    //contains tools for working with the traits
    partial class Traits
    {
        //returns the summoned if unable to find master
        public static Actor FindMaster(Actor summoned)
        {
            List<Actor> simpleList = World.world.units.getSimpleList();
            foreach (Actor actor in simpleList)
            {
                if (summoned.getName().Equals($"Summoned by {actor.getName()}"))
                {
                    summoned.data.get("Master", out string master, "");
                    if (actor.data.id.Equals(master))
                    {
                        return actor;
                    }
                }
            }
            return summoned;
        }
        public static void CorruptSummonedOne(Actor SummonedOne)
        {
            SummonedOne.data.setName("Corrupted One");
            SummonedOne.data.removeString("Master");
        }
        //summon ability
        public static void Summon(string creature, int times, BaseSimObject pSelf, WorldTile Ptile, int lifespan = 31)
        {
            Actor self = (Actor)pSelf;
            for (int i = 0; i < times; i++)
            {
                Actor actor = World.world.units.spawnNewUnit(creature, Ptile, true, 3f);
                actor.data.name = $"Summoned by {self.getName()}";
                actor.data.set("Master", self.data.id);
                actor.addTrait("Summoned One");
                actor.addTrait("regeneration");
                actor.addTrait("madness");
                actor.addTrait("fire_proof");
                actor.addTrait("acid_proof");
                actor.removeTrait("immortal");
                actor.data.set("life", 0);
                actor.data.set("lifespan", lifespan);
            }
        }
        public static List<Actor> GetMinions(Actor a)
        {
            List<Actor> MyMinions = new List<Actor>();
            List<Actor> simpleList = World.world.units.getSimpleList();
            foreach (Actor actor in simpleList)
            {
                if (actor.getName().Equals($"Summoned by {a.getName()}") && actor.hasTrait("Summoned One"))
                {
                    actor.data.get("Master", out string master, "");
                    if (a.data.id.Equals(master))
                    {
                        MyMinions.Add(actor);
                    }
                }
            }
            return MyMinions;
        }
        public static bool IsGod(Actor a)
            => GetGodTraits(a).Count > 0
            || a.asset.id == SA.crabzilla; //crabzilla is obviously a god, duhh

        public static bool IsGodTrait(string a)
             => a.Equals("God Of The Lich")
            || a.Equals("God Of The Stars")
            || a.Equals("God Of Knowledge")
            || a.Equals("God Of the Night")
            || a.Equals("God_Of_Chaos")
            || a.Equals("God Of War")
            || a.Equals("God Of the Earth")
            || a.Equals("God Of light")
            || a.Equals("God Of gods")
            || a.Equals("LesserGod");

        public static List<string> GetGodTraits(Actor a) => GetGodTraits(a.data.traits);
        public static List<string> GetGodTraits(List<string> pTraits, bool includedemigods = false)
        {
            List<string> list = new List<string>();
            foreach (string trait in pTraits)
            {
                if (IsGodTrait(trait) || (trait.Equals("Demi God") && includedemigods))
                {
                    list.Add(trait);
                }
            }
            return list;
        }
        public static bool SuperRegeneration(BaseSimObject pTarget, float chance, int percent)
        {
            if (Toolbox.randomChance(chance))
            {
                pTarget.a.restoreHealth(pTarget.a.getMaxHealth() * (percent / 100));
                return true;
            }
            return false;
        }
        public static bool BringMinions(BaseSimObject pTarget, WorldTile pTile)
        {
            Actor b = (Actor)pTarget;
            List<Actor> Minions = GetMinions(b);
            foreach (Actor a in Minions)
            {
                float pDist = Vector2.Distance(pTarget.currentPosition, a.currentPosition);
                if (pDist > 50)
                {
                    EffectsLibrary.spawnAt("fx_teleport_blue", pTarget.currentPosition, a.stats[S.scale]);
                    a.cancelAllBeh();
                    a.spawnOn(pTarget.currentTile, 0f);
                }
            }
            return true;
        }
        public static List<Actor> FindGods(bool CanAttack, Actor a)
        {
            List<Actor> Gods = new List<Actor>();
            List<Actor> simpleList = World.world.units.getSimpleList();
            foreach (Actor actor in simpleList)
            {
                if (IsGod(actor) && (!CanAttack || a.canAttackTarget(actor)))
                {
                    Gods.Add(actor);
                }
            }
            return Gods;
        }
        //returns true if teleported
        public static bool TeleportNearActor(Actor Actor, BaseSimObject Target, int distance, bool AllTiles = false, bool MustBeFar = false, byte Attempts = 10)
        {
            if (Target != null)
            {
                if (!MustBeFar || (Vector2.Distance(Target.currentPosition, Actor.currentPosition) > distance))
                {
                    byte attempts = 0;
                    while (attempts < Attempts)
                    {
                        WorldTile _tile = Toolbox.getRandomTileWithinDistance(Target.currentTile, distance);
                        attempts++;
                        if (AllTiles || (_tile.Type.ground && !_tile.Type.block && _tile.isSameIsland(Target.currentTile)))
                        {
                            Actor.cancelAllBeh();
                            EffectsLibrary.spawnAt("fx_teleport_blue", _tile.posV3, Actor.stats[S.scale]);
                            Actor.spawnOn(_tile, 0f);
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}

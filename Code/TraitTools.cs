
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GodsAndPantheons
{
    //contains tools for working with the traits
    partial class Traits
    {
        //returns null if unable to find master
        public static Actor FindMaster(Actor summoned)
        {
            summoned.data.get("Master", out string master, "");
            Actor Mymaster = World.world.units.get(master);
            return Mymaster != default(Actor) ? Mymaster : null;
        }
        public static void AddTrait(ActorTrait Trait, string disc)
        {
            foreach (KeyValuePair<string, float> kvp in TraitStats[Trait.id])
            {
                Trait.base_stats[kvp.Key] = kvp.Value;
            }
            Trait.inherit = -99999999999999;
            AssetManager.traits.add(Trait);
            PlayerConfig.unlockTrait(Trait.id);
            addTraitToLocalizedLibrary(Trait.id, disc);
        }
        public static void CorruptSummonedOne(Actor SummonedOne)
        {
            SummonedOne.data.setName("Corrupted One");
            SummonedOne.data.removeString("Master");
        }
        public static void AddAutoTraits(ActorData a, string trait, bool mustbeinherited = false)
        {
            foreach (string autotrait in AutoTraits[trait])
            {
                if (Toolbox.randomChance(GetEnhancedChance(trait, trait+"inherit%")) || !mustbeinherited)
                {
                    a.addTrait(autotrait);
                }
            }
        }
        public static bool AutoTrait(ActorData pTarget, List<string> traits, bool MustBeInherited = false)
        {
            foreach (string trait in AutoTraits.Keys)
             {
               if (traits.Contains(trait))
               {
                  AddAutoTraits(pTarget, trait, MustBeInherited);
               }
             }
            return true;
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
            || a.Equals("God Of Chaos")
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
        //returns the raw chance
        public static float GetChance(string ID, string Chance, float Default = 0) => Main.savedSettings.Chances.ContainsKey(ID) ? Main.savedSettings.Chances[ID].ContainsKey(Chance) ? Main.savedSettings.Chances[ID][Chance].active ? float.Parse(Main.savedSettings.Chances[ID][Chance].value) : 0 : Default : Default;
        //returns 2 if the trait's era is one, 1 if it is not, Default if the trait is not found
        public static float GetEraMultiplier(string trait, float Default = 1) => TraitEras.Keys.Contains(trait) ? World.world_era.id == TraitEras[trait].Key ? 2 : 1f : Default;
        //returns the chance of the trait multiplied by its era multiplier devided by the devisor
        public static float GetEnhancedChance(string trait, string chance, float DefaultChance = 0, float DefaultMult = 1, float devisor = 100) => GetChance(TraitToWindow(trait), chance, DefaultChance) * GetEraMultiplier(trait, DefaultMult) / devisor;
        public static string TraitToWindow(string Trait) => Trait switch
            {
                "God Of Chaos" => "ChaosGodWindow",
                "God Of light" => "SunGodWindow",
                "God Of the Night" => "DarkGodWindow",
                "God Of Knowledge" => "KnowledgeGodWindow",
                "God Of the Stars" => "MoonGodWindow",
                "God Of the Earth" => "EarthGodWindow",
                "God Of War" => "WarGodWindow",
                "God Of The Lich" => "LichGodWindow",
                "God Of gods" => "GodOfGodsWindow",
                _ => "",
            };

        //kill me
        public static List<KeyValuePair<string, float>> GetDemiStats(ActorData pData)
        {
            List<KeyValuePair<string, float>> stats = new List<KeyValuePair<string, float>>();
            pData.get("Demi" + S.speed, out float speed);
            pData.get("Demi" + S.health, out float health);
            pData.get("Demi" + S.critical_chance, out float crit);
            pData.get("Demi" + S.damage, out float damage);
            pData.get("Demi" + S.armor, out float armor);
            pData.get("Demi" + S.attack_speed, out float attackSpeed);
            pData.get("Demi" + S.accuracy, out float accuracy);
            pData.get("Demi" + S.range, out float range);
            pData.get("Demi" + S.scale, out float scale);
            pData.get("Demi" + S.intelligence, out float intell);
            pData.get("Demi" + S.knockback_reduction, out float knockback_reduction);
            pData.get("Demi" + S.warfare, out float warfare);
            pData.get("Demi" + S.fertility, out float fertility);
            pData.get("Demi" + S.max_children, out float maxchildren);
            stats.Add(new KeyValuePair<string, float>(S.speed, speed));
            stats.Add(new KeyValuePair<string, float>(S.critical_chance, crit));
            stats.Add(new KeyValuePair<string, float>(S.health, health));
            stats.Add(new KeyValuePair<string, float>(S.damage, damage));
            stats.Add(new KeyValuePair<string, float>(S.armor, armor));
            stats.Add(new KeyValuePair<string, float>(S.attack_speed, attackSpeed));
            stats.Add(new KeyValuePair<string, float>(S.accuracy, accuracy));
            stats.Add(new KeyValuePair<string, float>(S.range, range));
            stats.Add(new KeyValuePair<string, float>(S.scale, scale));
            stats.Add(new KeyValuePair<string, float>(S.intelligence, intell));
            stats.Add(new KeyValuePair<string, float>(S.knockback_reduction, knockback_reduction));
            stats.Add(new KeyValuePair<string, float>(S.warfare, warfare));
            stats.Add(new KeyValuePair<string, float>(S.fertility, fertility));
            stats.Add(new KeyValuePair<string, float>(S.max_children, maxchildren));
            return stats;
        }
        public static void Inheritgodtraits(List<string> godtraits, ref ActorData God)
        {
            foreach (string trait in godtraits)
            {
                if (Toolbox.randomChance(GetEnhancedChance(trait, trait+"inherit%", 50)))
                {
                  God.addTrait(trait);
                }
                else
                {
                    God.addTrait("Failed God");
                    God.set("Demi" + trait, true);
                }
            }
        }
        public static void MakeDemiGod(List<string> godtraits, ref ActorData DemiGod)
        {
            DemiGod.addTrait("Demi God");
            foreach (string trait in godtraits)
            {
                DemiGod.set("Demi" + trait, true);
                foreach (KeyValuePair<string, float> kvp in TraitStats[trait])
                {
                  if (Toolbox.randomChance(GetEnhancedChance(trait, trait + "inherit%", 50, 1, 75)))
                  {
                    DemiGod.get("Demi" + kvp.Key, out float value);
                    DemiGod.set("Demi" + kvp.Key, (kvp.Value / 2) + Random.Range(-(kvp.Value / 2.5f), kvp.Value / 2.5f) + value);
                  }
                }
            }
        }
        public static List<string> Getinheritedgodtraits(ActorData pData)
        {
            List<string> traits = new List<string>();
            foreach (string key in TraitStats.Keys)
            {
                pData.get("Demi" + key, out bool inherited);
                if (inherited)
                {
                    traits.Add(key);
                }
            }
            return traits;
        }
    }
}
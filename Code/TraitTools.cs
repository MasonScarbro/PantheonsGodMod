﻿
using ai;
using ReflectionUtility;
using SleekRender;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GodsAndPantheons
{
    //contains tools for working with the traits
    partial class Traits
    {
        public static PowerLibrary pb = AssetManager.powers;

        //returns null if unable to find master
        public static Actor FindMaster(Actor Minion)
        {
            if (Minion.hasStatus("BrainWashed"))
            {
                Minion.data.get("BrainWasher", out long BrainWasher, -1);
                return World.world.units.get(BrainWasher);
            }
            Minion.data.get("Master", out long master, -1);
            return World.world.units.get(master);
        }
        public static List<Sprite> LaserSprites;
        
        public static GameObject CreateStorm(WorldTile pTile, float time, float TimeCooldown, StormAction? Action, Color? StormColor, float Size)
        {
            World.world.startShake(0.3f, 0.01f, 0.03f, false, true);
            GameObject Storm = EffectsLibrary.spawn("fx_CloudOfDarkness", pTile, null, null, 0f, -1f, -1f).gameObject;
            Storm.GetComponent<Storm>().spawnOnTile(pTile);
            Storm.GetComponent<Storm>().Init(time, TimeCooldown, Action);
            Storm.transform.localScale = new Vector3(Size, Size, 1);
            Storm.GetComponent<SpriteRenderer>().color = StormColor ?? Color.white;
            return Storm;
        }
        public static void CreateLaserForActor(Actor pSelf, float time = 10)
        {
            if (!pSelf.has_attack_target)
            {
                return;
            }
            (EffectsLibrary.spawn("ChaosLaser") as ChaosLaser).Init(pSelf);
            pSelf.addStatusEffect("Lassering", time);
        }
        public static void AddTrait(ActorTrait Trait, string disc, int InheritRate = 0)
        {
            if (TraitStats.ContainsKey(Trait.id))
            {
                Trait.base_stats = new BaseStats();
                foreach (KeyValuePair<string, float> Stat in TraitStats[Trait.id])
                {
                    Trait.base_stats[Stat.Key] = Stat.Value;
                }
            }
            Trait.rate_inherit = InheritRate;
            AssetManager.traits.add(Trait);
            Trait.unlock();
            addTraitToLocalizedLibrary(Trait.id, disc);
        }
        //returns true if a trait is added
        public static void AddAutoTraits(Actor a, string trait, bool mustbeinherited = false, float chancemult = 1)
        {
            foreach (string autotrait in AutoTraits[trait])
            {
                if (!mustbeinherited || Randy.randomChance(GetEnhancedChance(trait, trait + "inherit%") * chancemult))
                {
                   a.addTrait(autotrait);
                }
            }
        }
        public static void AutoTrait(Actor pTarget, ListPool<string> traits, bool MustBeInherited = false, float chancemult = 1)
        {
            if (!Main.savedSettings.AutoTraits)
            {
                return;
            }
            foreach (string trait in AutoTraits.Keys)
             {
               if (traits.Contains(trait))
               {
                  AddAutoTraits(pTarget, trait, MustBeInherited, chancemult);
               }
             }
            return;
        }
        static readonly List<string> summonedoneautotraits = new List<string>() { "regeneration", "fire_proof", "acid_proof"};

        //summon ability
        public static void Summon(string creature, int times, BaseSimObject pSelf, WorldTile Ptile, int lifespan = 61, List<string>? autotraits = null)
        {
            Actor self = (Actor)pSelf;
            for (int i = 0; i < times; i++)
            {
                Actor actor = World.world.units.spawnNewUnit(creature, Ptile, false, true);
                TurnActorIntoSummonedOne(actor, self, lifespan);
                foreach (string trait in autotraits ?? summonedoneautotraits)
                {
                    actor.addTrait(trait);
                }
            }
        }
        static List<Actor> temp_minion_list = new List<Actor>();
        public static List<Actor> GetMinions(Actor a)
        {
            temp_minion_list.Clear();
            foreach (Actor actor in World.world.units)
            {
               actor.data.get("Master", out string master, "");
               if (a.data.id.Equals(master))
               {
                 temp_minion_list.Add(actor);
               }
            }
            return temp_minion_list;
        }
        public static bool IsGod(Actor a)
            => GetGodTraits(a).Count > 0
            || a.asset.id == SA.crabzilla //crabzilla is obviously a god, duhh
            || a.asset.id == SA.god_finger; //its in the name

        public static bool IsGodTrait(ActorTrait a) => GodAbilities.Keys.Contains(a.id);
        public static List<string> GetGodTraits(Actor a) => GetGodTraits(a.traits);
        public static List<string> GetGodTraits(HashSet<ActorTrait> pTraits, bool includedemigods = false, bool includesubgods = false)
        {
            List<string> list = new List<string>();
            foreach (ActorTrait trait in pTraits)
            {
                if (IsGodTrait(trait) || (trait.Equals("Demi God") && includedemigods) || (trait.Equals("Lesser God") && includesubgods))
                {
                    list.Add(trait.id);
                }
            }
            return list;
        }
        static List<Actor> temp_gods_list = new List<Actor>();
        public static List<Actor> FindGods(Actor a, bool CanAttack = false, bool includeself = false)
        {
            temp_gods_list.Clear();
            foreach (BaseSimObject actor in World.world.units)
            {
                    if (actor.a != a || includeself)
                    {
                        if (IsGod(actor.a) && (!CanAttack || a.canAttackTarget(actor)))
                        {
                            temp_gods_list.Add(actor.a);
                        }
                    }
            }
            return temp_gods_list;
        }
        //returns true if teleported
        public static bool TeleportNearActor(Actor Actor, BaseSimObject Target, int distance, bool AllTiles = false, bool MustBeFar = true, byte Attempts = 20)
        {
            if (Target != null)
            {
                if (!MustBeFar || Vector2.Distance(Target.current_position, Actor.current_position) > distance || !Actor.current_tile.isSameIsland(Target.current_tile))
                {
                    for(byte attempts = 0; attempts < Attempts; attempts++)
                    {
                        WorldTile _tile = Toolbox.getRandomTileWithinDistance(Target.current_tile, distance);
                        if (AllTiles || (_tile.Type.ground && !_tile.Type.block && _tile.isSameIsland(Target.current_tile)))
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
        public static bool SuperRegeneration(BaseSimObject pTarget, float chance, float percent)
        {
            if (Randy.randomChance(chance / 100))
            {
                pTarget.a.restoreHealth((int)(pTarget.a.getMaxHealth() * (percent / 100)));
            }
            return true;
        }
        //returns the raw chance
        public static float GetChance(string ID, string Chance, float Default = 0) => Main.savedSettings.Chances.ContainsKey(ID) && Main.savedSettings.Chances[ID].ContainsKey(Chance) ? Main.savedSettings.Chances[ID][Chance].active ? Main.savedSettings.Chances[ID][Chance].value : 0 : Default;
        //returns 2 if the trait's era is one, 1 if it is not, Default if the trait is not found
        public static float GetEraMultiplier(string trait, float Default = 1) => TraitEras.Keys.Contains(trait) ? World.world_era.id == TraitEras[trait].Key ? 2 : 1f : Default;
        //returns the chance of the trait multiplied by its era multiplier devided by the devisor
        public static float GetEnhancedChance(string trait, string chance, float devisor = 100, float DefaultChance = 0, float DefaultMult = 1) => GetChance(TraitToWindow(trait), chance, DefaultChance) * GetEraMultiplier(trait, DefaultMult) / devisor;
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
                "God Of Fire" => "GodOfFireWindow",
                "God Of Love" => "LoveGodWindow",
                _ => "",
            };

        public static BaseStats temp_base_stats = new BaseStats();
        public static BaseStats GetDemiStats(ActorData pData)
        {
            pData.get("Demi" + S.speed, out float speed);
            pData.get("Demi" + S.health, out float health);
            pData.get("Demi" + S.critical_chance, out float crit);
            pData.get("Demi" + S.damage, out float damage);
            pData.get("Demi" + S.armor, out float armor);
            pData.get("Demi" + S.attack_speed, out float attackSpeed);
            pData.get("Demi" + S.accuracy, out float accuracy);
            pData.get("Demi" + S.mana, out float Mana);
            pData.get("Demi" + S.range, out float range);
            pData.get("Demi" + S.scale, out float scale);
            pData.get("Demi" + S.intelligence, out float intell);
            pData.get("Demi" + S.mass, out float mass);
            pData.get("Demi" + S.warfare, out float warfare);
            pData.get("Demi" + S.offspring, out float offpsring);
            pData.get("Demi" + S.max_nutrition, out float maxNutrition);
            pData.get("Demi" + S.diplomacy, out float diplomacy);
            pData.get("Demi" + S.loyalty_traits, out float loyalty);
            pData.get("Demi" + S.lifespan, out int lifespan); //special
            temp_base_stats.clear();
            temp_base_stats[S.speed] = speed;
            temp_base_stats[S.mana] = Mana;
            temp_base_stats[S.critical_chance] = crit;
            temp_base_stats[S.health] = health;
            temp_base_stats[S.damage] = damage;      
            temp_base_stats[S.armor] = armor;
            temp_base_stats[S.attack_speed] = attackSpeed;
            temp_base_stats[S.accuracy] = accuracy;
            temp_base_stats[S.range] = range;
            temp_base_stats[S.scale] = scale;
            temp_base_stats[S.intelligence] = intell;
            temp_base_stats[S.mass] = mass;
            temp_base_stats[S.warfare] = warfare;
            temp_base_stats[S.offspring] = offpsring;
            temp_base_stats[S.diplomacy] = diplomacy;
            temp_base_stats[S.loyalty_traits] = loyalty;
            temp_base_stats[S.max_nutrition] = (int)maxNutrition;
            //special
            temp_base_stats[S.lifespan] = lifespan;
            return temp_base_stats;
        }
        public static Actor GetTargetToCrashLand(Actor actor)
        {
            using ListPool<Actor> actors = new ListPool<Actor>();
            foreach(Actor a in GetAlliesOfActor(Finder.getUnitsFromChunk(actor.current_tile, 1, 6), actor))
            {
                if (!a.hasStatus("Levitating") && a != actor)
                {
                    actors.Add(a);
                }
            }
            if(actors.Count == 0)
            {
                return null;
            }
            return actors.GetRandom();
        }
        public static List<Actor> GetAlliesOfActor(IEnumerable<BaseSimObject> actors, BaseSimObject actor)
        {
            List<Actor> allies = new List<Actor>();
            foreach (Actor a in actors)
            {
                if (a.kingdom == actor.kingdom)
                {
                    allies.Add(a);
                }
            }
            return allies;
        }
        public static List<Actor> GetEnemiesOfActor(IEnumerable<BaseSimObject> actors, BaseSimObject actor)
        {
            List<Actor> allies = new List<Actor>();
            foreach (Actor a in actors)
            {
                if (a.kingdom != actor.kingdom)
                {
                    allies.Add(a);
                }
            }
            return allies;
        }
        public static Actor CopyActor(Actor pActor, string ActorID)
        {
            Actor actor = World.world.units.createNewUnit(ActorID, pActor.current_tile, true);
            if(actor == null)
            {
                return null;
            }
            actor.traits.Clear();
            actor.data.custom_data_bool = pActor.data.custom_data_bool;
            actor.data.custom_data_float = pActor.data.custom_data_float;
            actor.data.custom_data_int = pActor.data.custom_data_int;
            actor.data.custom_data_string = pActor.data.custom_data_string;
            actor.data.custom_data_flags = pActor.data.custom_data_flags;
            ActorTool.copyUnitToOtherUnit(pActor, actor, true);
            return actor;
        }
        public static Actor Morph(Actor pActor, string morphid, bool Log = true, bool destroyWeapon = true)
        {
            if (pActor == null)
            {
                return null;
            }
            if (!pActor.inMapBorder())
            {
                return null;
            }
            if (pActor.asset.id == morphid)
            {
                return null;
            }
            if (destroyWeapon)
            {
                pActor.equipment?.weapon?.throwOutItem();
            }
            Actor actor = CopyActor(pActor, morphid);
            if (actor == null)
            {
                return null;
            }
            if (!actor.asset.use_items && pActor.asset.use_items)
            {
                pActor.city?.takeAllItemsFromActorOnDeath(pActor);
            }
            actor.setKingdom(pActor.kingdom);
            actor.setCity(pActor.city);
            pActor.traits.Clear();
            if (Log)
            {
                actor.data.set("morphedinto", morphid);
                actor.data.set("oldself", pActor.asset.id);
            }
            foreach (Actor minion in GetMinions(pActor))
            {
                minion.data.set("Master", actor.data.id);
            }
            if (SelectedUnit._unit_main == pActor)
            {
                SelectedUnit.makeMainSelected(actor);
            }
            Clan clan = pActor.clan;
            ActionLibrary.removeUnit(pActor);
            if (clan != null) { 
            actor.setClan(clan);
            }
            EffectsLibrary.spawn("fx_spawn", actor.current_tile, null, null, 0f, -1f, -1f);
            return actor;
        }
        public static void MakeDemiGod(ListPool<string> godtraits, Actor DemiGod, float chancemmult = 1)
        {
            DemiGod.addTrait("Demi God");
            foreach (string trait in godtraits)
            {
                DemiGod.data.set("Demi" + trait, true);
                foreach (KeyValuePair<string, float> kvp in TraitStats[trait])
                {
                  if (Randy.randomChance(GetEnhancedChance(trait, trait + "inherit%") * chancemmult))
                  {
                    DemiGod.data.get("Demi" + kvp.Key, out float value);
                    DemiGod.data.set("Demi" + kvp.Key, Random.Range(kvp.Value * 0.5f, kvp.Value * 0.75f) + value);
                  }
                }
            }
            DemiGod.data.set("Demi"+S.lifespan, Random.Range(100, 200));
        }
        public static void MakeLesserGod(ListPool<string> godtraits, Actor LesserGod, float chancemult = 1)
        {
            LesserGod.addTrait("Lesser God");
            LesserGod.addTrait("immortal"); // lesser gods immortal or very long lived? havent decided yet
            foreach (string trait in godtraits)
            {
                LesserGod.data.set("Demi" + trait, true);
                foreach (KeyValuePair<string, float> kvp in TraitStats[trait])
                {
                    if (Randy.randomChance(GetEnhancedChance(trait, trait + "inherit%") * chancemult))
                    {
                        LesserGod.data.get("Demi" + kvp.Key, out float value);
                        LesserGod.data.set("Demi" + kvp.Key, Random.Range(kvp.Value * 0.75f, kvp.Value) + value);
                    }
                }
                foreach (AttackAction ability in GodAbilities[trait])
                {
                    if (Randy.randomChance(GetEnhancedChance(trait, trait + "inherit%") * chancemult))
                    {
                        LesserGod.data.set("Demi" + ability.Method.Name, true);
                    }
                }
            }
        }
        //gets god traits which are stored in the data of lesser gods / demigods
        public static List<string> Getinheritedgodtraits(ActorData pData)
        {
            List<string> traits = new List<string>();
            foreach (string key in GodAbilities.Keys)
            {
                pData.get("Demi" + key, out bool inherited);
                if (inherited)
                {
                    traits.Add(key);
                }
            }
            return traits;
        }
        public static bool InheritedTrait(Actor a, string trait)
        {
            a.data.get("Demi" + trait, out bool inherited);
            return inherited;
        }
        public static bool EraStatus(Actor Master, Actor Me)
        {
            bool added = false;
            foreach (string era in TraitEras.Keys)
            {
                if (Master.hasTrait(era) || InheritedTrait(Master, era))
                {
                    if (World.world_era.id == TraitEras[era].Key)
                    {
                        Me.addStatusEffect(TraitEras[era].Value);
                        added = true;
                    }
                    else if (Me.hasStatus(TraitEras[era].Value))
                    {
                        Me.finishStatusEffect(TraitEras[era].Value);
                    }
                }
            }
            return added;
        }
        public static bool DoesKingdomHaveGod(Kingdom pKingdom)
        {
            foreach (Actor a in pKingdom.units)
            {
                if (IsGod(a))
                {
                    return true;
                }
            }
            return false;
        }
        //returns god traits which no one in the world have
        public static List<string> getavailblegodtraits(MapBox instance)
        {
            List<string> list = new List<string>(GodAbilities.Keys);
            foreach (Actor a in instance.units)
            {
                foreach (string godtrait in GetGodTraits(a))
                {
                    list.Remove(godtrait);
                }
            }
            return list;
        }
        public static void TurnActorIntoSummonedOne(Actor minion, Actor Master, int lifespan = 61)
        {
            minion.data.name = $"Summoned by {Master.getName()}";
            minion.data.set("Master", Master.data.id);
            minion.addTrait("Summoned One");
            minion.setKingdom(Master.kingdom);
            minion.removeTrait("immortal");
            minion.data.set("life", 0);
            minion.data.set("lifespan", lifespan);
        }
        public static void CorruptActor(Actor minion, Actor Master, float duration = 31)
        {
            minion.data.set("BrainWasher", Master.data.id);
            minion.addStatusEffect("BrainWashed", duration);
            minion.data.set("PreviousKingdom", minion.kingdom.id);
            minion.setKingdom(Master.kingdom);
        }
        //pushes the actor directly to the target
        public static void PushActorTowardsTile(Vector2Int point, Actor pActor, float time = 6f/90f)
        {
            float pX = (point.x-pActor.current_position.x)/(24f*time);
            float pY = (point.y-pActor.current_position.y)/(24f*time);
            float pZ = (1.5f * time) - (pActor.position_height/24);
            //forces the actor add the force even if he is in the air
            pActor.velocity.x += pX * 0.6f;
            pActor.velocity.y += pY * 0.6f;
            pActor.velocity.z += pZ * 2f;
            pActor.under_forces = true;
            return;
        }
        public static void PushActor(Actor pActor, Vector2Int point, float strength = 1, float Zstrength = 0.1f, bool Outward = false)
        {
            if (!Outward) {
                pActor.calculateForce(pActor.current_position.x, pActor.current_position.y, point.x, point.y, strength, Zstrength);
            }
            else
            {
                pActor.calculateForce(point.x, point.y, pActor.current_position.x, pActor.current_position.y, strength, Zstrength);
            }
        }
        //call this by using finishStatusEffect("BrainWashed")
        public static void FinishBrainWashing(Actor pActor)
        {
            pActor.data.get("PreviousKingdom", out long PreviousKingdom);
            Kingdom kingdom = World.world.kingdoms.get(PreviousKingdom);
            if (kingdom != null)
            {
                pActor.setKingdom(kingdom);
            }
            else
            {
                pActor.setDefaultKingdom();
            }
            pActor.data.removeString("PreviousKingdom");
            pActor.data.removeString("BrainWasher");
        }
        //formula so it dissapears as soon as it reaches target size: pAlphaSpeed = 1/((ABS(pRadius-TargetRadius) / pSpeed))
        public static void SpawnCustomWave(Vector2 pVector, float pRadius, float pSpeed = 1f, float pAlphaSpeed = 1f)
        {
            BaseEffect baseEffect = EffectsLibrary.spawn("fx_custom_explosion_wave");
            if (baseEffect == null)
            {
                return;
            }
            ((CustomExplosionFlash)baseEffect).start(pVector, pRadius, pSpeed, pAlphaSpeed);
        }
    }
}
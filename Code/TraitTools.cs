using ai;
using GodsAndPantheons.CustomEffects;
using HarmonyLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

namespace GodsAndPantheons
{
    //contains tools for working with the traits
    static partial class Traits
    {
        public static PowerLibrary pb = AssetManager.powers;
        public static ConcurrentDictionary<Actor, DemiGodData> CachedDemiGodData = new ConcurrentDictionary<Actor, DemiGodData>();
        public static bool CanDieInLava(this Actor actor)
        {
            return actor.asset.die_in_lava && !actor.hasTrait("Lava Walker");
        }
        //returns null if unable to find master
        public static Actor FindMaster(this Actor Minion)
        {
            if (Minion.hasStatus("BrainWashed"))
            {
                Minion.data.get("BrainWasher", out long BrainWasher, -1);
                return World.world.units.get(BrainWasher);
            }
            Minion.data.get("Master", out long master, -1);
            return World.world.units.get(master);
        }
        public static IEnumerable<City> GetAllCitiesWithinRadius(WorldTile tile, int radius)
        {
            foreach (City city in World.world.cities)
            {
                if (Vector3.Distance(city.city_center, tile.posV) <= radius)
                {
                    yield return city;
                }
            }
        }
        public static Storm CreateStorm(WorldTile pTile, float time, float TimeCooldown, StormAction Action, Color? StormColor, float Size, BaseSimObject ByWho = null)
        {
            World.world.startShake(0.3f, 0.01f, 0.03f, false, true);
            GameObject Storm = EffectsLibrary.spawn("fx_CloudOfDarkness", pTile).gameObject;
            Storm.GetComponent<Storm>().spawnOnTile(pTile);
            Storm.GetComponent<Storm>().Init(time, TimeCooldown, ByWho, Action);
            Storm.transform.localScale = new Vector3(Size, Size, 1);
            Storm.GetComponent<SpriteRenderer>().color = StormColor ?? Color.white;
            return Storm.GetComponent<Storm>();
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
                if (!mustbeinherited || CanUseAbility(trait, trait + "inherit%", 100/chancemult))
                {
                   a.addTrait(autotrait);
                }
            }
        }
        public static void AutoTrait(Actor pTarget, IEnumerable<string> traits, bool MustBeInherited = false, float chancemult = 1)
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
        public static void ShootCustomProjectile(BaseSimObject pSelf, BaseSimObject pTarget, string projectile, int amount = 1, float pZ = 0.25f, Vector2 Pos = default)
        {
            if (pSelf.kingdom == null)
            {
                return;
            }
            Vector3 Start = Pos == default ? pSelf.current_position : Pos;
            float tZ = 0f;
            if (pTarget.isInAir())
            {
                tZ = pTarget.getHeight();
            }
            for (int i = 0; i < amount; i++)
            {
                Vector3 tTargetPos = pTarget.current_tile.posV3;
                tTargetPos.x += Randy.randomFloat(-(pTarget.stats["size"] + 1f), pTarget.stats["size"] + 1f);
                tTargetPos.y += Randy.randomFloat(-pTarget.stats["size"], pTarget.stats["size"]);
                World.world.projectiles.spawn(pSelf, pTarget, projectile, Start, tTargetPos, tZ, pZ);
            }
        }
        public static IEnumerable<Actor> GetMinions(this Actor a)
        {
            foreach (Actor actor in World.world.units)
            {
               if(!(actor.hasStatus("BrainWashed") || actor.hasTrait("Summoned One")))
               {
                    continue;
               }
               actor.data.get("Master", out long master, -1);
               actor.data.get("BrainWasher", out long brainwasher, -1);
               if (a.data.id.Equals(master) || a.data.id.Equals(brainwasher))
               {
                    yield return actor;
               }
            }
        }
        public static bool IsGod(this Actor a, bool IncludeDemigods = false, bool IncludeLesserGods = false, bool IncludeGodKillers = false)
            => GetGodTraits(a.traits, IncludeDemigods, IncludeLesserGods, IncludeGodKillers).ToList().Count > 0
            || a.asset.id == SA.crabzilla //crabzilla is obviously a god, duhh
            || a.asset.id == SA.god_finger; //its in the name

        public static bool IsGodTrait(this ActorTrait a) => GodAbilities.Keys.Contains(a.id);
        public static IEnumerable<string> GetGodTraits(this Actor a) => GetGodTraits(a.traits);
        public static IEnumerable<string> GetGodTraits(HashSet<ActorTrait> pTraits, bool includedemigods = false, bool includesubgods = false, bool IncludeGodKillers = false)
        {
            foreach (ActorTrait trait in pTraits)
            {
                if (IsGodTrait(trait) || (trait.Equals("Demi God") && includedemigods) || (trait.Equals("Lesser God") && includesubgods) || (trait.Equals("God Killer") && IncludeGodKillers))
                {
                    yield return trait.id;
                }
            }
        }
        public static IEnumerable<Actor> FindGods(Actor a, bool CanAttack = true, bool includeself = false)
        {
            foreach (BaseSimObject actor in World.world.units)
            {
                if (actor.a != a || includeself)
                {
                    if (IsGod(actor.a) && (!CanAttack || a.canAttackTarget(actor)))
                    {
                        yield return actor.a;
                    }
                }
            }
        }
        public static bool SuperRegeneration(BaseSimObject pTarget, float chance, float percent)
        {
            if (Randy.randomChance(chance / 100))
            {
                pTarget.a.restoreHealth((int)(pTarget.a.getMaxHealth() * (percent / 100)));
            }
            return true;
        }
        //returns the raw chance, if the trait is not found an exception WILL occur
        public static float GetRawChance(string ID, string Key) => Main.savedSettings[ID][Key].active ? Main.savedSettings[ID][Key].value : 0;
        //returns 2 if the trait's era is one, 1 if it is not
        public static float GetEraMultiplier(string trait) => World.world_era.id == TraitEras[trait].Key ? 2 : 1f;
        //returns the chance of the trait multiplied by its era multiplier devided by the devisor
        public static float Chance(string trait, string chance, float devisor = 100) => GetRawChance(TraitToWindow(trait), chance) * GetEraMultiplier(trait) / devisor;
        public static bool CanUseAbility(string trait, string chance, float devisor = 100) => Randy.randomChance(Chance(trait, chance, devisor));
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
        public static BaseStats GetDemiStats(this Actor pActor)
        {
            return pActor.DemiData().BaseStats;
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
        public static List<Actor> GetAlliesOfActor(IEnumerable<BaseSimObject> actors, BaseSimObject actor, bool shuffle = true)
        {
            List<Actor> allies = new List<Actor>();
            foreach (Actor a in actors)
            {
                if (!a.areFoes(actor))
                {
                    allies.Add(a);
                }
            }
            if (shuffle)
            {
                allies.Shuffle();
            }
            return allies;
        }
        public static IEnumerable<BaseSimObject> GetEnemiesOfKingdom(IEnumerable<BaseSimObject> Objects, Kingdom kingdom)
        {
            foreach(BaseSimObject Object in Objects)
            {
                if (Object.kingdom.isEnemy(kingdom)){
                    yield return Object;
                }
            }
        }
        public static void CreateBlindess(WorldTile pTile, int radius, float length, Kingdom kingdom = null)
        {
            foreach (Actor victim in Finder.getUnitsFromChunk(pTile, 2, radius))
            {
                if ((kingdom != null && victim.kingdom == kingdom) || IsGod(victim)) continue;
                victim.addStatusEffect("Blinded", length);
            }
        }
        public static List<Actor> GetEnemiesOfActor(IEnumerable<Actor> actors, BaseSimObject actor)
        {
            List<Actor> enemies = new List<Actor>();
            foreach (Actor a in actors)
            {
                if (!a.isAlive())
                {
                    continue;
                }
                if (a.areFoes(actor))
                {
                    enemies.Add(a);
                }
            }
            return enemies;
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
        public static bool HasMorphed(this Actor pActor)
        {
            pActor.data.get("morphedinto", out string morphed, "");
            pActor.data.get("oldself", out string oldself, "");
            return morphed != oldself;
        }
        public static ActorAsset GetTrueAsset(this Actor pActor)
        {
            pActor.data.get("morphedinto", out string morphed, "");
            pActor.data.get("oldself", out string oldself, "");
            if (morphed == oldself)
            {
                return pActor.asset;
            }
            return AssetManager.actor_library.get(oldself);
        }
        public static Actor Morph(this Actor pActor, string morphid, bool Log = true, bool destroyWeapon = true)
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
                return pActor;
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
            foreach (Actor minion in pActor.GetMinions())
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
        public static void MakeDemiGod(ListPool<string> godtraits, Actor DemiGod, float chancemult = 1)
        {
            DemiGod.addTrait("Demi God");
            DemiGodData Data = new DemiGodData();
            foreach (string trait in godtraits)
            {
                Data.InheritTrait(trait);
                InheritStats(Data, trait, chancemult, 0.5f, 0.75f);
            }
            Data.AddBaseStat(S.lifespan, Random.Range(100, 201));
            DemiGod.SetDemiData(Data);
        }
        public static void SetDemiData(this Actor DemiGod, DemiGodData data)
        {
            DemiGod.data.set("DemiData", JsonConvert.SerializeObject(JObject.FromObject(data)));
            if(CachedDemiGodData.ContainsKey(DemiGod)){
                CachedDemiGodData[DemiGod] = data;
            }
        }
        public static DemiGodData DemiData(this Actor DemiGod) {
            if (CachedDemiGodData.TryGetValue(DemiGod, out DemiGodData data))
            {
                return data;
            }
            DemiGod.data.get("DemiData", out string DemiData);
            if(DemiData == null)
            {
                return null;
            }
            DemiGodData Data = JsonConvert.DeserializeObject<JObject>(DemiData).ToObject<DemiGodData>();
            if(Data == null)
            {
                return null;
            }
            CachedDemiGodData.TryAdd(DemiGod, Data);
            return Data;
        }
        public static void InheritStats(this DemiGodData data, string trait, float chancemult, float MinRange = 0.75f, float MaxRange = 1)
        {
            foreach (KeyValuePair<string, float> kvp in TraitStats[trait])
            {
                if(CanUseAbility(trait, trait + "inherit%", 50/chancemult))
                {
                    data.AddBaseStat(kvp.Key, Random.Range(kvp.Value * MinRange, kvp.Value * MaxRange));
                }
            }
        }
        public static void InheritAbilities(this DemiGodData data, string trait, float chancemult)
        {
            for (int i = 0; i < GodAbilities[trait].Count; i++)
            {
                if (CanUseAbility(trait, trait + "inherit%", 100 / chancemult))
                {
                    data.AddAbility(trait, i);
                }
            }
        }
        public static void MakeLesserGod(ListPool<string> godtraits, Actor LesserGod, float chancemult = 1)
        {
            LesserGod.addTrait("Lesser God");
            LesserGod.addTrait("immortal"); // lesser gods immortal or very long lived? havent decided yet
            DemiGodData demiGodData = new DemiGodData();
            foreach (string trait in godtraits)
            {
                demiGodData.InheritTrait(trait);
                InheritStats(demiGodData, trait, chancemult);
                InheritAbilities(demiGodData, trait, chancemult);
            }
            LesserGod.SetDemiData(demiGodData);
        }
        public static void CreateGodKiller(Actor GodKiller, IEnumerable<string> godtraits)
        {
            GodKiller.addTrait("God Killer");
            GodKiller.addTrait("immortal");
            AutoTrait(GodKiller, godtraits, true, 2);
            DemiGodData GodKillerData = new DemiGodData();
            foreach (string trait in godtraits)
            {
                GodKillerData.InheritTrait(trait);
                GodKillerData.InheritStats(trait, 2);
                GodKillerData.InheritAbilities(trait, 2);
            }
            GodKiller.SetDemiData(GodKillerData);
        }
        //gets god traits which are stored in the data of lesser gods / demigods
        public static IEnumerable<string> Getinheritedgodtraits(this Actor pActor)
        {
            return pActor.DemiData()?.InheritedGodTraits;
        }
        public static bool InheritedTrait(this Actor a, string trait)
        {
            return a.DemiData()?.InheritedGodTraits.Contains(trait) ?? false;
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
                if (a.IsGod())
                {
                    return true;
                }
            }
            return false;
        }
        //returns god traits which no one in the world have
        public static List<string> getavailblegodtraits()
        {
            List<string> list = new List<string>(GodAbilities.Keys);
            foreach (Actor a in World.world.units)
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
        //pushes the actor just enough so it lands exactly at the target
        public static void PushActorTowardsTile(Vector2Int point, Actor pActor, float time = 6f/90f, float VelocitySpeed = 1)
        {
            float pX = (point.x-pActor.current_position.x)/(time*VelocitySpeed);
            float pY = (point.y-pActor.current_position.y)/(time*VelocitySpeed);
            float pZ = (0.15f * time * GetDampConstant(pActor)) - (pActor.position_height / (time * GetDampConstant(pActor)));
            //forces the actor add the force even if he is in the air
            pActor.velocity_speed = VelocitySpeed;
            pActor.velocity.x = pX;
            pActor.velocity.y = pY;
            pActor.velocity.z = pZ;
            pActor.under_forces = true;
            return;
        }
        public static float GetDampConstant(Actor pActor)
        {
            float tDampMod = pActor.stats["mass"] * SimGlobals.m.gravity;
            return Mathf.Min(tDampMod, 20f);
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
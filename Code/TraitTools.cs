
using ai;
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
        public static Actor FindMaster(Actor summoned)
        {
            summoned.data.get("Master", out string master, "");
            return World.world.units.get(master);
        }
        public static Actor FindBrainWasher(Actor brainwashed)
        {
            brainwashed.data.get("BrainWasher", out string master, "");
            return World.world.units.get(master);
        }
        public static List<Sprite> LaserSprites;
        public static GameObject LoadCrabZillaLaser(out List<Sprite> sprites)
        {
            GameObject crablaser = Object.Instantiate(Resources.Load<Actor>("actors/p_crabzilla").transform.GetChild(0).GetChild(2).gameObject);
            crablaser.GetComponent<CrabArm>().giantzilla = null;
            crablaser.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).parent = crablaser.transform;
            crablaser.transform.GetChild(0).gameObject.DestroyImmediateIfNotNull();
            crablaser.transform.GetChild(0).localPosition = new Vector3(0, 0, 0);
            crablaser.transform.GetChild(0).localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            crablaser.transform.GetChild(0).localScale = new Vector3(1, 1, 1);
            crablaser.transform.GetComponent<CrabArm>().laser.color = new Color(1, 0, 0, 1);
            crablaser.transform.GetChild(0).GetChild(0).localPosition = new Vector3(140, 0, 0);
            sprites = new List<Sprite>(crablaser.GetComponent<CrabArm>().laserSprites);
            crablaser.GetComponent<CrabArm>().laserSprites = null;
            return crablaser;
        }
        public static GameObject CreateStorm(WorldTile pTile, float time, float TimeCooldown, StormAction? Action, Color? StormColor, float Size)
        {
            World.world.startShake(0.3f, 0.01f, 0.03f, false, true);
            GameObject Storm = EffectsLibrary.spawn("fx_antimatter_effect", pTile, null, null, 0f, -1f, -1f).gameObject;
            Storm.GetComponent<SpriteRenderer>().color = StormColor ?? Color.white;
            Storm.transform.localScale = new Vector3(Size, Size, 1);
            Storm.AddComponent<Storm>().Init(time,TimeCooldown, Action);
            return Storm;
        }
        public static GameObject Laserr = LoadCrabZillaLaser(out LaserSprites);
        public static void CreateLaserForActor(Actor pSelf, float time = 10)
        {
            GameObject Laser = Object.Instantiate(Laserr);
            Laser.transform.position = pSelf.currentPosition;
            Laser.transform.parent = pSelf.transform;
            Laser.transform.localPosition = new Vector3(-3, 12, 0);
            Laser.transform.localScale = new Vector3(0.3f, 0.25f, 1);
            pSelf.addStatusEffect("Lassering", time);
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
        //returns true if a trait is added
        public static bool AddAutoTraits(ActorData a, string trait, bool mustbeinherited = false, float chancemult = 1)
        {
            bool addednew = false;
            foreach (string autotrait in AutoTraits[trait])
            {
                if (!mustbeinherited || Toolbox.randomChance(GetEnhancedChance(trait, trait + "inherit%") * chancemult))
                {
                    if (!a.traits.Contains(autotrait)) addednew = true;
                    a.addTrait(autotrait);
                }
            }
            return addednew;
        }
        public static bool AddAutoTraits(Actor a, string trait) => AddAutoTraits(a.data, trait);
        public static bool AutoTrait(ActorData pTarget, ListPool<string> traits, bool MustBeInherited = false, float chancemult = 1)
        {
            foreach (string trait in AutoTraits.Keys)
             {
               if (traits.Contains(trait))
               {
                  AddAutoTraits(pTarget, trait, MustBeInherited, chancemult);
               }
             }
            return true;
        }
        static readonly List<string> summonedoneautotraits = new List<string>() { "regeneration", "fire_proof", "acid_proof"};
        private static readonly object a;

        //summon ability
        public static void Summon(string creature, int times, BaseSimObject pSelf, WorldTile Ptile, int lifespan = 61, List<string>? autotraits = null)
        {
            Actor self = (Actor)pSelf;
            for (int i = 0; i < times; i++)
            {
                Actor actor = World.world.units.spawnNewUnit(creature, Ptile, true, 3f);
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
            || a.asset.id == SA.godFinger; //its in the name

        public static bool IsGodTrait(string a)
             => a.Equals("God Of The Lich")
            || a.Equals("God Of the Stars")
            || a.Equals("God Of Knowledge")
            || a.Equals("God Of the Night")
            || a.Equals("God Of Chaos")
            || a.Equals("God Of War")
            || a.Equals("God Of the Earth")
            || a.Equals("God Of light")
            || a.Equals("God Of Fire");

        public static List<string> GetGodTraits(Actor a) => GetGodTraits(a.data.traits);
        public static List<string> GetGodTraits(List<string> pTraits, bool includedemigods = false, bool includesubgods = false)
        {
            List<string> list = new List<string>();
            foreach (string trait in pTraits)
            {
                if (IsGodTrait(trait) || (trait.Equals("Demi God") && includedemigods) || (trait.Equals("Lesser God") && includesubgods))
                {
                    list.Add(trait);
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
                if (!MustBeFar || Vector2.Distance(Target.currentPosition, Actor.currentPosition) > distance || !Actor.currentTile.isSameIsland(Target.currentTile))
                {
                    for(byte attempts = 0; attempts < Attempts; attempts++)
                    {
                        WorldTile _tile = Toolbox.getRandomTileWithinDistance(Target.currentTile, distance);
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
        public static bool SuperRegeneration(BaseSimObject pTarget, float chance, float percent)
        {
            if (Toolbox.randomChance(chance / 100))
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
                "God Of Fire" => "GodOfFireWindow",
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
            pData.get("Demi" + S.range, out float range);
            pData.get("Demi" + S.scale, out float scale);
            pData.get("Demi" + S.intelligence, out float intell);
            pData.get("Demi" + S.knockback_reduction, out float knockback_reduction);
            pData.get("Demi" + S.warfare, out float warfare);
            pData.get("Demi" + S.fertility, out float fertility);
            pData.get("Demi" + S.max_children, out float maxchildren);
            pData.get("Demi" + S.max_age, out int maxage);
            temp_base_stats.clear();
            temp_base_stats[S.speed] = speed;
            temp_base_stats[S.critical_chance] = crit;
            temp_base_stats[S.health] = health;
            temp_base_stats[S.damage] = damage;      
            temp_base_stats[S.armor] = armor;
            temp_base_stats[S.attack_speed] = attackSpeed;
            temp_base_stats[S.accuracy] = accuracy;
            temp_base_stats[S.range] = range;
            temp_base_stats[S.scale] = scale;
            temp_base_stats[S.intelligence] = intell;
            temp_base_stats[S.knockback_reduction] = knockback_reduction;
            temp_base_stats[S.warfare] = warfare;
            temp_base_stats[S.fertility] = fertility;
            temp_base_stats[S.max_children] = (int)maxchildren;
            //special
            temp_base_stats[S.max_age] = maxage;
            return temp_base_stats;
        }
        public static bool Morph(Actor pActor, string morphid, bool savedata = true, bool destroyWeapon = true)
        {
            if (pActor == null)
            {
                return false;
            }
            if (!pActor.inMapBorder())
            {
                return false;
            }
            if(pActor.asset.id == morphid)
            {
                return false;
            }
            if (destroyWeapon)
            {
                pActor.equipment?.weapon?.emptySlot();
            }
            Actor actor = World.world.units.createNewUnit(morphid, pActor.currentTile, 0f);
            actor.data.traits.Clear();
            actor.setKingdom(pActor.kingdom);
            actor.setCity(pActor.city);
            if (savedata)
            {
                actor.data.custom_data_bool = pActor.data.custom_data_bool;
                actor.data.custom_data_float = pActor.data.custom_data_float;
                actor.data.custom_data_int = pActor.data.custom_data_int;
                actor.data.custom_data_string = pActor.data.custom_data_string;
                actor.data.custom_data_flags = pActor.data.custom_data_flags;
                actor.data.set("morphedinto", morphid);
                actor.data.set("oldself", pActor.asset.id);
                
                if (!actor.asset.use_items && pActor.asset.use_items)
                {
                    pActor.city?.takeAllItemsFromActor(pActor);
                }
                ActorTool.copyUnitToOtherUnit(pActor, actor);
            }
            pActor.data.traits.Clear();
            foreach(Actor minion in GetMinions(pActor))
            {
                minion.data.set("Master", actor.data.id);
            }
            if(Config.selectedUnit == pActor)
            {
                Config.selectedUnit = actor;
            }
            Clan clan = pActor.getClan();
            ActionLibrary.removeUnit(pActor);
            clan?.addUnit(actor);
            EffectsLibrary.spawn("fx_spawn", actor.currentTile, null, null, 0f, -1f, -1f);
            return true;
        }
        public static void MakeDemiGod(ListPool<string> godtraits, ref ActorData DemiGod, float chancemmult = 1)
        {
            DemiGod.addTrait("Demi God");
            foreach (string trait in godtraits)
            {
                DemiGod.set("Demi" + trait, true);
                foreach (KeyValuePair<string, float> kvp in TraitStats[trait])
                {
                  if (Toolbox.randomChance(GetEnhancedChance(trait, trait + "inherit%") * chancemmult))
                  {
                    DemiGod.get("Demi" + kvp.Key, out float value);
                    DemiGod.set("Demi" + kvp.Key, Random.Range(kvp.Value * 0.5f, kvp.Value * 0.75f) + value);
                  }
                }
            }
            DemiGod.set("Demi"+S.max_age, Random.Range(10, 30));
        }
        public static void MakeLesserGod(ListPool<string> godtraits, ref ActorData LesserGod, float chancemult = 1)
        {
            LesserGod.addTrait("Lesser God");
            foreach (string trait in godtraits)
            {
                LesserGod.set("Demi" + trait, true);
                foreach (KeyValuePair<string, float> kvp in TraitStats[trait])
                {
                    if (Toolbox.randomChance(GetEnhancedChance(trait, trait + "inherit%") * chancemult))
                    {
                        LesserGod.get("Demi" + kvp.Key, out float value);
                        LesserGod.set("Demi" + kvp.Key, Random.Range(kvp.Value * 0.75f, kvp.Value) + value);
                    }
                }
                foreach (AttackAction ability in GodAbilities[trait])
                {
                    if (Toolbox.randomChance(GetEnhancedChance(trait, trait + "inherit%") * chancemult))
                    {
                        LesserGod.set("Demi" + nameof(ability), true);
                    }
                }
            }
            LesserGod.set("Demi" + S.max_age, Random.Range(30, 50));
        }
        //gets god traits which are stored in the data of failed gods / demigods
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
        public static bool EraStatus(Actor Master, Actor Me)
        {
            bool added = false;
            foreach (string era in TraitEras.Keys)
            {
                if (Master.a.hasTrait(era))
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
        public static void CorruptActor(Actor minion, Actor Master, int duration = 31)
        {
            minion.data.set("BrainWasher", Master.data.id);
            minion.addStatusEffect("BrainWashed", duration);
            minion.data.set("PreviousKingdom", minion.kingdom.id);
            minion.setKingdom(Master.kingdom);
        }
        public static void PushActor(Actor pActor, Vector2Int point, float strength = 1, float Zstrength = 0.1f, bool Outward = false)
        {
            float angle = Toolbox.getAngle(pActor.currentTile.x, pActor.currentTile.y, point.x, point.y);
            float TrueStrength = strength - strength * pActor.stats[S.knockback_reduction];
            float num4 = Mathf.Cos(angle) * TrueStrength * (Outward ? -1 : 1);
            float num5 = Mathf.Sin(angle) * TrueStrength * (Outward ? -1 : 1);
            pActor.addForce(num4, num5, Zstrength);
        }
        //call this by using finishStatusEffect("BrainWashed")
        public static void FinishBrainWashing(Actor pActor)
        {
            pActor.data.get("PreviousKingdom", out string PreviousKingdom);
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
    }
}
using ai;
using ai.behaviours;
using HarmonyLib;
using NeoModLoader.General;
using ReflectionUtility;
using System.Collections.Generic;
using UnityEngine;
//Harmony Patches
namespace GodsAndPantheons
{
    [HarmonyPatch(typeof(BaseSimObject), "canAttackTarget")]
    public class UpdateAttacking
    {
        static void Postfix(ref bool __result, BaseSimObject __instance, BaseSimObject pTarget)
        {
            if (__instance == pTarget)
            {
                __result = false;
            }
            if (__instance.isActor())
            {
                Actor a = __instance.a;
                if (a.hasTrait("Summoned One"))
                {
                    Actor Master = Traits.FindMaster(a);
                    if (Master != a)
                    {
                        if (!Master.canAttackTarget(pTarget))
                        {
                            __result = false;
                            return;
                        }
                    }
                }
            }
            if (pTarget.isActor())
            {
                Actor b = pTarget.a;
                if (b.hasTrait("Summoned One"))
                {
                    Actor Master = Traits.FindMaster(b);
                    if (Master != b)
                    {
                        if (!__instance.canAttackTarget(Master))
                            __result = false;
                    }
                }
            }

        }
    }
    [HarmonyPatch(typeof(ActorBase), "clearAttackTarget")]
    public class KEEPATTACKING
    {
        static bool Prefix(ActorBase __instance)
        {
            if (__instance.hasTrait("God Hunter") && Main.savedSettings.HunterAssasins)
            {
                BaseSimObject? a = Reflection.GetField(typeof(ActorBase), __instance, "attackTarget") as BaseSimObject;
                if (a != null)
                {
                    if (Traits.IsGod(a.a) && a.isAlive()) { return false; }
                }
            }
            return true;
        }
    }
    [HarmonyPatch(typeof(Actor), "newKillAction")]
    public class isgodkiller
    {
        static void Prefix(Actor __instance, Actor pDeadUnit)
        {
            if (Traits.IsGod(pDeadUnit))
            {
                __instance.addTrait("God Killer");
            }
        }
    }
    [HarmonyPatch(typeof(CityBehProduceUnit), "checkGreatClan")]
    public class InheritGodTraits
    {
        static void Postfix(Actor pParent1, Actor pParent2)
        {
            if (HavingChild)
            {
                int parents = pParent2 != null ? 2 : 1;
                int godparents = Traits.IsGod(pParent1) ? 1 : 0;
                int demiparents = pParent1.data.traits.Contains("Demi God") ? 1 : 0;
                List<string> parentdata = new List<string>(getinheritedgodtraits(pParent1.data));
                List<string> godtraits = new List<string>(Traits.GetGodTraits(pParent1));
                if (parents == 2)
                {
                    godtraits.AddRange(Traits.GetGodTraits(pParent2));
                    parentdata.AddRange(getinheritedgodtraits(pParent2.data));
                    godparents += Traits.IsGod(pParent2) ? 1 : 0;
                    demiparents += pParent2.data.traits.Contains("Demi God") ? 1 : 0;
                }
                if (godparents > 0)
                {
                    if (parents == godparents)
                    {
                        inheritgodtraits(godtraits, ref Child);
                    }
                    else
                    {
                        if (demiparents > 0 && Toolbox.randomChance(0.5f))
                        {
                            inheritgodtraits(godtraits, ref Child);
                        }
                        else
                        {
                            MakeDemiGod(godtraits, ref Child);
                        }
                    }
                }
                else if (demiparents > 0)
                {
                    if (parents == demiparents)
                    {
                        if (Toolbox.randomChance(0.25f))
                        {
                            inheritgodtraits(parentdata, ref Child);
                        }
                        else
                        {
                            MakeDemiGod(parentdata, ref Child);
                        }
                    }
                    else
                    {
                        if (Toolbox.randomChance(0.3f)) Child.addTrait("blessed");
                        if (Toolbox.randomChance(0.4f)) Child.addTrait("giant");
                        if (Toolbox.randomChance(0.5f)) Child.addTrait("strong");
                        if (Toolbox.randomChance(0.6f)) Child.addTrait("frost_proof");
                        if (Toolbox.randomChance(0.7f)) Child.addTrait("tough");
                        if (Toolbox.randomChance(0.2f)) Child.addTrait("immortal");
                    }
                }
                HavingChild = false;
            }
        }
        static void inheritgodtraits(List<string> godtraits, ref ActorData __instance)
        {
            foreach (string trait in godtraits)
            {
                string window = Traits.TraitToWindow(trait);
                if (window != null)
                {
                    if (Toolbox.randomChance(Traits.GetChance(window, Traits.TraitToInherit(trait)) / 100))
                    {
                        __instance.addTrait(trait);
                    }
                }
            }
        }
        static void MakeDemiGod(List<string> godtraits, ref ActorData __instance)
        {
            __instance.addTrait("Demi God");
            foreach (string trait in godtraits)
            {
                __instance.set("Demi" + trait, true);
                string window = Traits.TraitToWindow(trait);
                if (window != null)
                {
                    foreach (KeyValuePair<string, float> kvp in Traits.TraitStats[trait])
                    {
                        if (Toolbox.randomChance(Traits.GetChance(window, Traits.TraitToInherit(trait)) / 75))
                        {
                            __instance.get("Demi" + kvp.Key, out float value);
                            __instance.set("Demi" + kvp.Key, (kvp.Value / 2) + Random.Range(-(kvp.Value / 2.5f), kvp.Value / 2.5f) + value);
                        }
                    }
                }
            }
        }
        static List<string> getinheritedgodtraits(ActorData pData)
        {
            List<string> traits = new List<string>();
            foreach (string key in Traits.TraitStats.Keys)
            {
                pData.get("Demi"+key, out bool value);
                if (value)
                {
                    traits.Add(key);
                }
            }
            return traits;
        }
        public static bool HavingChild = false;
        public static ActorData Child;
    }
    [HarmonyPatch(typeof(ActorData), "inheritTraits")]
    public class ChildData
    {
        static void Postfix(ActorData __instance, List<string> pTraits)
        {
            if (!InheritGodTraits.HavingChild && Traits.GetGodTraits(pTraits, true).Count > 0) { }
            {
                InheritGodTraits.Child = __instance;
                InheritGodTraits.HavingChild = true;
            }
        }
    }
    [HarmonyPatch(typeof(ActorBase), "calculateFertility")]
    public class UseDemiStats
    {
        static void Postfix(ActorBase __instance) {
            if (__instance.hasTrait("Demi God"))
            {
                mergeStats(GetStats(__instance.data), ref __instance.stats);
            }
        }
        static void mergeStats(List<KeyValuePair<string, float>> pStats, ref BaseStats __instance)
        {
            for (int i = 0; i < pStats.Count; i++)
            {
                __instance[pStats[i].Key] += pStats[i].Value;
            }
        }
        //kill me
        static List<KeyValuePair<string, float>> GetStats(ActorData pData)
        {
            List<KeyValuePair<string, float>> stats = new List<KeyValuePair<string, float>>();
            pData.get("Demi"+S.speed, out float speed);
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
            return stats;
        }
    }
}

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
    [HarmonyPatch(typeof(ActorData), "inheritTraits")]
    public class InheritGodTraits
    {
        static void Postfix(ActorData __instance, List<string> pTraits)
        {
            __instance.get("parents", out int parents);
            __instance.get("godparents", out int godparents);
            parents++;
            List<string> godtraits = Traits.GetGodTraits(pTraits);
            if (godtraits.Count > 0)
            {
                foreach(string trait in godtraits)
                {
                    __instance.set(trait, true);
                }
                godparents++;
            }
            __instance.set("godparents", godparents);
            __instance.set("parents", parents);
        }
    }
    [HarmonyPatch(typeof(City), "spawnPopPoint")]
    public class CreateDemiGods
    {
        static void Prefix(ActorData pData)
        {
            pData.get("parents", out int parents);
            pData.get("godparents", out int godparents);
            if (godparents > 0)
            {
                List<string> data = getinheritedgodtraits(pData);
                  if ((parents == 1) || (parents == 2 && godparents == 2))
                  {
                     inheritgodtraits(data, pData);
                  }
                  else
                  {
                      MakeDemiGod(data, pData);
                  }
                  foreach(string trait in data)
                  {
                    pData.removeBool(trait);
                  }
            }
            pData.removeInt("parents");
            pData.removeInt("godparents");

        }
        static List<string> getinheritedgodtraits(ActorData pData)
        {
            List<string> traits = new List<string>();
            foreach(string key in Traits.TraitStats.Keys)
            {
                pData.get(key, out bool value);
                if (value)
                {
                    traits.Add(key);
                }
            }
            return traits;
        }
        static void inheritgodtraits(List<string> godtraits, ActorData __instance)
        {
            foreach (string trait in godtraits)
            {
                string window = Traits.TraitToWindow(trait);
                if (window != null)
                {
                    if (Toolbox.randomChance(Traits.GetChance(window, Traits.TraitToInherit(trait))/100))
                    {
                        __instance.addTrait(trait);
                    }
                }
            }
        }
        static void MakeDemiGod(List<string> godtraits, ActorData __instance)
        {
            __instance.traits.Add("Demi God");
            foreach (string trait in godtraits)
            {
                string window = Traits.TraitToWindow(trait);
                if (window != null)
                {
                    if (Toolbox.randomChance(Traits.GetChance(window, Traits.TraitToInherit(trait))/75))
                    {
                        foreach (KeyValuePair<string, float> kvp in Traits.TraitStats[trait])
                        {
                            __instance.get("Demi" + kvp.Key, out float value);
                            __instance.set("Demi"+kvp.Key, (kvp.Value/2) + Random.Range(-(kvp.Value/2.5f), kvp.Value/2.5f)+value);
                        }
                    }
                }
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

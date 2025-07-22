using NCMS.Utils;
using UnityEngine;
using UnityEngine.Events;
using NeoModLoader.General;
using System.Collections.Generic;

namespace GodsAndPantheons
{
    class Buttons
    {
        public static void init()
        {
            
            UI.createTab("Button Tab_GodsAndPantheons", "Tab_GodsAndPantheons", "GodsAndPantheons", "Gods Here", -150);
            loadButtons();
        }
        static PowersTab tab;
        private static void loadButtons()
        {
            tab = getPowersTab("GodsAndPantheons");

            var godHunterSpawn = new GodPower();
            godHunterSpawn.id = "GodHunter";
            godHunterSpawn.show_spawn_effect = true;
            godHunterSpawn.multiple_spawn_tip = true;
            godHunterSpawn.actor_spawn_height = 3f;
            godHunterSpawn.name = "GodHunter";
            godHunterSpawn.sound_event = "spawnelf";
            godHunterSpawn.actor_asset_id = "GodHunter";
            godHunterSpawn.click_action = new PowerActionWithID(callSpawnUnit);
            AssetManager.powers.add(godHunterSpawn);


            var darkOneSpawn = new GodPower();
            darkOneSpawn.id = "DarkOne";
            darkOneSpawn.show_spawn_effect = true;
            darkOneSpawn.multiple_spawn_tip = true;
            darkOneSpawn.actor_spawn_height = 3f;
            darkOneSpawn.name = "DarkOne";
            darkOneSpawn.sound_event = "spawnelf";
            darkOneSpawn.actor_asset_id = "DarkOne";
            darkOneSpawn.click_action = new PowerActionWithID(callSpawnUnit);
            AssetManager.powers.add(darkOneSpawn);

            var InspectInheritence = new GodPower();
            InspectInheritence.id = "Inspect God Traits Inherited";
            InspectInheritence.name = "Inspect God Traits Inherited";
            InspectInheritence.click_action = new PowerActionWithID(callInspectInheritence);
            AssetManager.powers.add(InspectInheritence);

            var InspectAbilities = new GodPower();
            InspectAbilities.id = "Inspect Abilities Inherited";
            InspectAbilities.name = "Inspect Abilities Inherited";
            InspectAbilities.click_action = new PowerActionWithID(callInspectAbilities);
            AssetManager.powers.add(InspectAbilities);

            PowerButtons.CreateButton(
              "GodHunter",
              Resources.Load<Sprite>("ui/Icons/godKiller"),
              "Godhunter Spawn",
              "The God Hunter, Assassin Of Gods, they are usually successfull in groups",
              new Vector2(100, 18),
              ButtonType.GodPower,
              tab.transform,
              null
              
          );
            PowerButtons.CreateButton(
              "DarkOne",
              Resources.Load<Sprite>("Actors/species/other/DarkOne/heads_male/walk_0"),
              "DarkOne Spawn",
              "The Minion of the gods of darkness",
              new Vector2(100, -18),
              ButtonType.GodPower,
              tab.transform,
              null
          );
            PowerButtons.CreateButton(
                    "KnowledgeGodWindow",
                    Resources.Load<Sprite>("ui/Icons/knowledgeGod"),
                    "Chance Modfier",
                    "Manage The Gods Powers",
                    new Vector2(136, 18),
                    ButtonType.Click,
                    tab.transform,
                    WindowManager.windows["KnowledgeGodWindow"].openWindow
              );
            CreateToggleButton(
                    "ToggleDiplomacyModifiers",
                    Resources.Load<Sprite>("ui/Icons/iconDiplomacy"),
                    "Diplomacy Modifiers",
                    "if enabled, kingdoms will change their opinions on other kingdoms based on if their king's are gods",
                    new Vector2(388, -18),
                    ToggleDiplomacy,
                    Main.savedSettings.DiplomacyChanges
              );
            CreateToggleButton(
                    "ToggleDeathEras",
                    Resources.Load<Sprite>("ui/Icons/ages/iconAgeHope"),
                    "Death Eras",
                    "should deaths change the era?",
                    new Vector2(388, 18),
                    ToggleEra,
                    Main.savedSettings.deathera
              );
            CreateToggleButton(
                    "HunterAssassins",
                    Resources.Load<Sprite>("ui/Icons/godKiller"),
                    "Hunter Assassins",
                    "if enabled, god hunters will become assassins and will hunt down gods",
                    new Vector2(460, 18),
                    ToggleAssassins,
                    Main.savedSettings.HunterAssasins
              );
            CreateToggleButton(
                    "DevineMiracles",
                    Resources.Load<Sprite>("ui/Icons/actor_traits/iconNightchild"),
                    "Devine Miracles",
                    "allows for a very, very rare chance that a mortal in a kingdom will become a god",
                    new Vector2(424, 18),
                    ToggleDevineMiracles,
                    Main.savedSettings.DevineMiracles
              );
            PowerButtons.CreateButton(
                    "Generate God",
                    Resources.Load<Sprite>("ui/Icons/actor_traits/iconNightchild"),
                    "Create a God",
                    "Press this to generate a god",
                    new Vector2(244, -18),
                    ButtonType.Click,
                    tab.transform,
                    CreateGeneratedGod
              );
              PowerButtons.CreateButton(
                    "Create a Divine Miracle",
                    Resources.Load<Sprite>("ui/Icons/actor_traits/iconNightchild"),
                    "Create a Divine Miracle",
                    "press this to make a divine miracle manually",
                    new Vector2(424, -18),
                    ButtonType.Click,
                    tab.transform,
                    CreateDivineMiracle
              );
            CreateToggleButton(
                    "GodKings",
                    Resources.Load<Sprite>("ui/Icons/iconKings"),
                    "God Kings",
                    "if enabled, gods will always take higher priority when electing a new king",
                    new Vector2(352, 18),
                    ToggleGodKings,
                    Main.savedSettings.GodKings
              );
            CreateToggleButton(
                    "MakeSummonedOne",
                    Resources.Load<Sprite>("ui/Icons/SummonedOne"),
                    "Spawn Summoned Ones",
                    "if enabled, if you were to spawn a creature on a tile that already has a creature on it, the new creature will be a summoned one of that creature, note that summoned ones have their own AI and only live for 60 or 120s (unless they are immortal)",
                    new Vector2(460, -18),
                    ToggleSummmoned,
                    Main.savedSettings.MakeSummoned
              );
            PowerButtons.CreateButton(
                    "GodOfFireWindow",
                    Resources.Load<Sprite>("ui/Icons/GodOfFire"),
                    "Chance Modfier",
                    "Manage The Gods Powers",
                    new Vector2(316, -18),
                    ButtonType.Click,
                    tab.transform,
                    WindowManager.windows["GodOfFireWindow"].openWindow
              );
            CreateToggleButton(
                    "ToggleAutoTraits",
                    Resources.Load<Sprite>("ui/Icons/actor_traits/iconBlessing"),
                    "ToggleAutoTraits",
                    "if enabled, gods will automatically get some traits related to their god trait (all gods get the immortal trait, god of fire gets fireproof)",
                    new Vector2(352, -18),
                    ToggleAutoTraits,
                    Main.savedSettings.AutoTraits
              );
            PowerButtons.CreateButton(
                    "MoonGodWindow",
                    Resources.Load<Sprite>("ui/Icons/starsGod"),
                    "Chance Modfier",
                    "Manage The Gods Powers",
                    new Vector2(172, 18),
                    ButtonType.Click,
                    tab.transform,
                    WindowManager.windows["MoonGodWindow"].openWindow
              );
            PowerButtons.CreateButton(
                    "DarkGodWindow",
                    Resources.Load<Sprite>("ui/Icons/godDark"),
                    "Chance Modfier",
                    "Manage The Gods Powers",
                    new Vector2(208, 18),
                    ButtonType.Click,
                    tab.transform,
                    WindowManager.windows["DarkGodWindow"].openWindow
              );
            PowerButtons.CreateButton(
                    "LoveGodWindow",
                    Resources.Load<Sprite>("ui/Icons/GodOfLove"),
                    "Chance Modfier",
                    "Manage The Gods Powers",
                    new Vector2(208, -18),
                    ButtonType.Click,
                    tab.transform,
                    WindowManager.windows["LoveGodWindow"].openWindow
              );
            PowerButtons.CreateButton(
                    "SunGodWindow",
                    Resources.Load<Sprite>("ui/Icons/lightGod"),
                    "Chance Modfier",
                    "Manage The Gods Powers",
                    new Vector2(244, 18),
                    ButtonType.Click,
                    tab.transform,
                    WindowManager.windows["SunGodWindow"].openWindow
              );
            PowerButtons.CreateButton(
                    "WarGodWindow",
                    Resources.Load<Sprite>("ui/Icons/warGod"),
                    "Chance Modfier",
                    "Manage The Gods Powers",
                    new Vector2(280, 18),
                    ButtonType.Click,
                    tab.transform,
                    WindowManager.windows["WarGodWindow"].openWindow
              );
              PowerButtons.CreateButton(
                    "RemoveGeneratedGods",
                    Resources.Load<Sprite>("ui/Icons/iconNightchild"),
                    "Chance Modfier",
                    "Remove All Generated Gods",
                    new Vector2(532, -18),
                    ButtonType.Click,
                    tab.transform,
                    RemoveGeneratedGods
              );
            PowerButtons.CreateButton(
                    "EarthGodWindow",
                    Resources.Load<Sprite>("ui/Icons/earthGod"),
                    "Chance Modfier",
                    "Remove Generated Gods",
                    new Vector2(316, 18),
                    ButtonType.Click,
                    tab.transform,
                    WindowManager.windows["EarthGodWindow"].openWindow
              );
            PowerButtons.CreateButton(
                    "LichGodWindow",
                    Resources.Load<Sprite>("ui/Icons/lichGod"),
                    "Chance Modfier",
                    "Manage The Gods Powers",
                    new Vector2(172, -18),
                    ButtonType.Click,
                    tab.transform,
                    WindowManager.windows["LichGodWindow"].openWindow
              );
            PowerButtons.CreateButton(
                    "ChaosGodWindow",
                    Resources.Load<Sprite>("ui/Icons/chaosGod"),
                    "Chance Modfier",
                    "Manage The Gods Powers",
                    new Vector2(136, -18),
                    ButtonType.Click,
                    tab.transform,
                    WindowManager.windows["ChaosGodWindow"].openWindow
              );
            PowerButtons.CreateButton(
                    "Inspect God Traits Inherited",
                    Resources.Load<Sprite>("ui/Icons/IconDemi"),
                    "Inspect God Traits Inherited",
                    "use this to inspect which god traits a lessergod/demi god/ god killer inherited",
                    new Vector2(496, 18),
                    ButtonType.GodPower,
                    tab.transform,
                    null
              );
            PowerButtons.CreateButton(
                   "Inspect Abilities Inherited",
                   Resources.Load<Sprite>("ui/Icons/subGod"),
                   "Inspect Abilities Inherited",
                   "use this to inspect which god abiltiies a lessergod/god killer inherited",
                   new Vector2(496, -18),
                   ButtonType.GodPower,
                   tab.transform,
                   null
             );
        }
        public static string GetGodTraitsInherited(WorldTile pTile)
        {
            Actor pActor = null;
            foreach (Actor a in Finder.getUnitsFromChunk(pTile, 1, 2))
            {
                if (a.hasTrait("Lesser God") || a.hasTrait("Demi God") || a.hasTrait("God Killer"))
                {
                    pActor = a;
                    break;
                }
            }
            if (pActor == null)
            {
               return "no Lesser God, Demi God or God Killer found!";
            }
            string message = "";
            foreach (string trait in pActor.Getinheritedgodtraits())
            {
                message += (message == "" ? "" : ", ") + trait;
            }
            return message;
        }
        public static string GetAbilitiesInherited(WorldTile pTile)
        {
            Actor pActor = null;
            foreach (Actor a in Finder.getUnitsFromChunk(pTile, 1, 2))
            {
                if (a.hasTrait("Lesser God") || a.hasTrait("God Killer"))
                {
                    pActor = a;
                    break;
                }
            }
            if (pActor == null)
            {
                return "no Lesser God or God Killer found!";
            }
            string message = "";
            foreach(KeyValuePair<string, HashSet<int>> Ability in pActor.DemiData().GodsAndAbilities)
            {
                foreach (int i in Ability.Value)
                {
                    message += (message == "" ? "" : ", ") + Traits.GodAbilities[Ability.Key][i].Method.Name;
                }
            }
            if(message == "")
            {
                message = "This Lesser God has no abilities inherited!";
            }
            return message;
        }

        private static bool callInspectInheritence(WorldTile pTile, string pPowerID)
        {
            WorldTip.instance.showToolbarText(GetGodTraitsInherited(pTile));
            return true;
        }
        public static bool callInspectAbilities(WorldTile pTile, string pPowerID)
        {
            WorldTip.instance.showToolbarText(GetAbilitiesInherited(pTile));
            return true;
        }

        private static PowersTab getPowersTab(string id)
        {
            GameObject gameObject = GameObjects.FindEvenInactive("Tab_" + id);
            return gameObject.GetComponent<PowersTab>();
        }
        public static bool callSpawnUnit(WorldTile pTile, string pPowerID)
        {
            AssetManager.powers.spawnUnit(pTile, pPowerID);
            return true;
        }
        public static void ToggleEra(){
                Main.savedSettings.deathera = !Main.savedSettings.deathera;
                Main.saveSettings();
        }
        public static void ToggleAssassins()
        {
            Main.savedSettings.HunterAssasins = !Main.savedSettings.HunterAssasins;
            Main.saveSettings();
        }
        public static void ToggleDevineMiracles()
        {
            Main.savedSettings.DevineMiracles = !Main.savedSettings.DevineMiracles;
            Main.saveSettings();
        }
        public static void CreateDivineMiracle()
        {
            if (!WorldBehaviours.DivineMiracle())
            {
                WorldTip.instance.showToolbarText("Could Not Create A Divine Miracle! there can only be a miracle if there is a kingdom with no gods");
            }
        }
        public static void CreateGeneratedGod()
        {
            GodProceduralGenerator.GenerateGod();
        }
        public static void RemoveGeneratedGods()
        {
            GodProceduralGenerator.ClearAllGeneratedGods();
        }
        public static void CreateToggleButton(string ID, Sprite sprite, string name, string Description, Vector2 pos, UnityAction toggleAction, bool Enabled)
        {
            GodPower power = AssetManager.powers.add(new GodPower()
            {
                id = ID,
                name = name,
                toggle_name = ID,
                toggle_action = delegate
                {
                    toggleAction();
                    PlayerConfig.dict[ID].boolVal = !PlayerConfig.dict[ID].boolVal;
                    PowerButtonSelector.instance.checkToggleIcons();
                }
            });
            Debug.Log($"Creating Toggle Button: {ID} and Name: {power.name} and Snake is {WindowManager.ToSnakeCase(ID)}");
            Localization.AddOrSet(WindowManager.ToSnakeCase(ID), power.name);
            Localization.AddOrSet(WindowManager.ToSnakeCase(ID) + "_description", Description);
            PlayerConfig.dict.Add(ID, new PlayerOptionData(ID));
            var Button = PowerButtonCreator.CreateToggleButton(
                ID,
                sprite,
                tab.transform,
                pos,
                true
            );
            if (!Enabled)
            {
                PlayerConfig.dict[ID].boolVal = false;
                Button.checkToggleIcon();
            }
        }
        public static void ToggleGodKings()
        {
            Main.savedSettings.GodKings = !Main.savedSettings.GodKings;
            Main.saveSettings();
        }
        public static void ToggleSummmoned()
        {
            Main.savedSettings.MakeSummoned = !Main.savedSettings.MakeSummoned;
            Main.saveSettings();
        }
        public static void ToggleDiplomacy()
        {
            Main.savedSettings.DiplomacyChanges = !Main.savedSettings.DiplomacyChanges;
            Main.saveSettings();
        }
        public static void ToggleAutoTraits()
        {
            Main.savedSettings.AutoTraits = !Main.savedSettings.AutoTraits;
            Main.saveSettings();
        }
    }
}

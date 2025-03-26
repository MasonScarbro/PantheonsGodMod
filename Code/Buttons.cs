using NCMS.Utils;
using UnityEngine;
using ReflectionUtility;
using System;
using static UnityEngine.GraphicsBuffer;
using GodsAndPantheons.Patches;

namespace GodsAndPantheons
{
    class Buttons
    {
        public static void init()
        {
            
            Tab.createTab("Button Tab_GodsAndPantheons", "Tab_GodsAndPantheons", "GodsAndPantheons", "Gods Here", -150);
            loadButtons();
        }
        private static void loadButtons()
        {
            PowersTab tab = getPowersTab("GodsAndPantheons");

            var godHunterSpawn = new GodPower();
            godHunterSpawn.id = "GodHunter";
            godHunterSpawn.showSpawnEffect = true;
            godHunterSpawn.multiple_spawn_tip = true;
            godHunterSpawn.actorSpawnHeight = 3f;
            godHunterSpawn.name = "GodHunter";
            godHunterSpawn.spawnSound = "spawnelf";
            godHunterSpawn.actor_asset_id = "GodHunter";
            godHunterSpawn.click_action = new PowerActionWithID(callSpawnUnit);
            AssetManager.powers.add(godHunterSpawn);


            var darkOneSpawn = new GodPower();
            darkOneSpawn.id = "DarkOne";
            darkOneSpawn.showSpawnEffect = true;
            darkOneSpawn.multiple_spawn_tip = true;
            darkOneSpawn.actorSpawnHeight = 3f;
            darkOneSpawn.name = "DarkOne";
            darkOneSpawn.spawnSound = "spawnelf";
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
              "Spawn a GodHunter",
              new Vector2(100, 18),
              ButtonType.GodPower,
              tab.transform,
              null
              
          );
            PowerButtons.CreateButton(
              "DarkOne",
              Resources.Load<Sprite>("Actors/DarkOne/walk_0"),
              "DarkOne Spawn",
              "Spawn a Dark One",
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
            PowerButton butto = PowerButtons.CreateButton(
                    "ToggleDiplomacyModifiers",
                    Resources.Load<Sprite>("ui/Icons/iconDiplomacy"),
                    "Diplomacy Modifiers",
                    "if enabled, kingdoms will change their opinions on other kingdoms based on if their king's are gods",
                    new Vector2(388, -18),
                    ButtonType.Toggle,
                    tab.transform,
                    ToggleDiplomacy
              );
            if (Main.savedSettings.DiplomacyChanges)
                PowerButtons.ToggleButton(butto.name);
            PowerButton button = PowerButtons.CreateButton(
                    "ToggleDeathEras",
                    Resources.Load<Sprite>("ui/Icons/ages/iconAgeHope"),
                    "Death Eras",
                    "should deaths change the era?",
                    new Vector2(388, 18),
                    ButtonType.Toggle,
                    tab.transform,
                    ToggleEra
              );
            if (Main.savedSettings.deathera)
                PowerButtons.ToggleButton(button.name);
            PowerButton button2 = PowerButtons.CreateButton(
                    "HunterAssassins",
                    Resources.Load<Sprite>("ui/Icons/godKiller"),
                    "Hunter Assassins",
                    "if enabled, god hunters will become assassins and will hunt down gods",
                    new Vector2(460, 18),
                    ButtonType.Toggle,
                    tab.transform,
                    ToggleAssassins
              );
            if (Main.savedSettings.HunterAssasins)
                PowerButtons.ToggleButton(button2.name);
            PowerButton button3 = PowerButtons.CreateButton(
                    "DevineMiracles",
                    Resources.Load<Sprite>("ui/Icons/iconNightchild"),
                    "Devine Miracles",
                    "allows for a very, very rare chance that a mortal in a kingdom will become a god",
                    new Vector2(424, 18),
                    ButtonType.Toggle,
                    tab.transform,
                    ToggleDevineMiracles
              );
            if (Main.savedSettings.DevineMiracles)
                PowerButtons.ToggleButton(button3.name);
            PowerButtons.CreateButton(
                    "Create a Divine Miracle",
                    Resources.Load<Sprite>("ui/Icons/iconNightchild"),
                    "Create a Divine Miracle",
                    "press this to make a divine miracle manually",
                    new Vector2(424, -18),
                    ButtonType.Click,
                    tab.transform,
                    CreateDivineMiracle
              );
            PowerButton button4 = PowerButtons.CreateButton(
                    "GodKings",
                    Resources.Load<Sprite>("ui/Icons/iconKings"),
                    "God Kings",
                    "if enabled, gods will always take higher priority when electing a new king",
                    new Vector2(352, 18),
                    ButtonType.Toggle,
                    tab.transform,
                    ToggleGodKings
              );
            if (Main.savedSettings.GodKings)
                PowerButtons.ToggleButton(button4.name);
            PowerButton Button5 = PowerButtons.CreateButton(
                    "MakeSummonedOne",
                    Resources.Load<Sprite>("ui/Icons/iconBlessing"),
                    "Spawn Summoned Ones",
                    "if enabled, if you were to spawn a creature on a tile that already has a creature on it, the new creature will be a summoned one of that creature, note that summoned ones have their own AI and only live for 60 or 120s (unless they are immortal)",
                    new Vector2(460, -18),
                    ButtonType.Toggle,
                    tab.transform,
                    ToggleSummmoned
              );
            if (Main.savedSettings.MakeSummoned)
                PowerButtons.ToggleButton(Button5.name);
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
            PowerButton Button6 = PowerButtons.CreateButton(
                    "ToggleAutoTraits",
                    Resources.Load<Sprite>("ui/Icons/iconBlessing"),
                    "ToggleAutoTraits",
                    "if enabled, gods will automatically get some traits related to their god trait (all gods get the immortal trait, god of fire gets fireproof)",
                    new Vector2(352, -18),
                    ButtonType.Toggle,
                    tab.transform,
                    ToggleAutoTraits
              );
            if (Main.savedSettings.AutoTraits)
                PowerButtons.ToggleButton(Button6.name);
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
                    "EarthGodWindow",
                    Resources.Load<Sprite>("ui/Icons/earthGod"),
                    "Chance Modfier",
                    "Manage The Gods Powers",
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
                    "use this to inspect which god traits a lessergod/demi god inherited",
                    new Vector2(496, 18),
                    ButtonType.GodPower,
                    tab.transform,
                    null
              );
            PowerButtons.CreateButton(
                   "Inspect Abilities Inherited",
                   Resources.Load<Sprite>("ui/Icons/subGod"),
                   "Inspect Abilities Inherited",
                   "use this to inspect which god abiltiies a lessergod inherited",
                   new Vector2(496, -18),
                   ButtonType.GodPower,
                   tab.transform,
                   null
             );
        }
        public static string GetGodTraitsInherited(WorldTile pTile)
        {
            World.world.getObjectsInChunks(pTile, 2, MapObjectType.Actor);
            Actor pActor = null;
            foreach (Actor a in World.world.temp_map_objects)
            {
                if (a.hasTrait("Lesser God") || a.hasTrait("Demi God"))
                {
                    pActor = a;
                    break;
                }
            }
            if (pActor == null)
            {
               return "no Lesser God or Demi God found!";
            }
            string message = "";
            foreach (string trait in Traits.Getinheritedgodtraits(pActor.data))
            {
                message += (message == "" ? "" : ", ") + trait;
            }
            return message;
        }
        public static string GetAbilitiesInherited(WorldTile pTile)
        {
            World.world.getObjectsInChunks(pTile, 2, MapObjectType.Actor);
            Actor pActor = null;
            foreach (Actor a in World.world.temp_map_objects)
            {
                if (a.hasTrait("Lesser God"))
                {
                    pActor = a;
                    break;
                }
            }
            if (pActor == null)
            {
                return "no Lesser God found!";
            }
            string message = "";
            foreach (string godtrait in Traits.Getinheritedgodtraits(pActor.data))
            {
                foreach (AttackAction ability in Traits.GodAbilities[godtrait])
                {
                    pActor.data.get("Demi" + ability.Method.Name, out bool inherited);
                    if (inherited)
                    {
                        message += (message == "" ? "" : ", ") + ability.Method.Name;
                    }
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
            if (!UpdateWorldStuff.DivineMiracle(World.world))
            {
                WorldTip.instance.showToolbarText("Could Not Create A Divine Miracle! there can only be a miracle if there is a kingdom with no gods");
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

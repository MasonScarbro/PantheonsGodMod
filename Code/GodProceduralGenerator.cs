using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using NeoModLoader.constants;
using static GodsAndPantheons.Traits;


namespace GodsAndPantheons
{
    [Serializable]
    public class GeneratedGodData
    {
        public string id;
        public string name;
        public string description;
        public string rarity;
        public string path_icon;
        public string group_id;
        public bool can_be_given;
        public Dictionary<string, float> stats;
        public string generation_seed;
        public DateTime created_at;

        public GeneratedGodData()
        {
            stats = new Dictionary<string, float>();
            created_at = DateTime.Now;
        }
    }

    [Serializable]
    public class GeneratedGodsSettings
    {
        public string settingVersion = "1.0.0";
        public List<GeneratedGodData> generatedGods = new List<GeneratedGodData>();
    }

    public class GodProceduralGenerator
    {
        private const string correctSettingsVersion = "1.0.0";
        public static GeneratedGodsSettings savedGeneratedGods = new GeneratedGodsSettings();
        private static readonly string configFilePath = $"{Paths.ModsConfigPath}/GeneratedGods.json";

        public static void Init()
        {
            // Only load existing data, don't apply anything to the game yet
            loadGeneratedGods();
            Debug.Log($"God Procedural Generator initialized. Found {savedGeneratedGods.generatedGods.Count} existing gods.");
        }

        public static void GenerateGod(string baseName = null, string seed = null)
        {
            Debug.Log("Starting god generation...");
            
            // Use seed for reproducible generation
            if (seed == null)
                seed = System.Guid.NewGuid().ToString();

            var random = new System.Random(seed.GetHashCode());

            // Generate unique ID
            string godId = $"Generated_God_{random.Next(1000, 9999)}";

            // Ensure uniqueness - but only check our list, not game data
            while (savedGeneratedGods.generatedGods.Exists(g => g.id == godId))
            {
                godId = $"Generated_God_{random.Next(1000, 9999)}";
            }

            var newGod = new GeneratedGodData
            {
                id = godId,
                name = baseName ?? GenerateGodName(random),
                description = GenerateGodDescription(random),
                rarity = "R3_Legendary",
                path_icon = "ui/icons/subGod",
                group_id = "GeneratedGods",
                can_be_given = true,
                generation_seed = seed
            };

            // Generate stats
            newGod.stats = GenerateGodStats(random);

            Debug.Log($"Generated god data: {newGod.name} (ID: {newGod.id})");

            // Add to our collection
            savedGeneratedGods.generatedGods.Add(newGod);

            // Save to file - this is the main focus
            bool saveSuccess = saveGeneratedGods();
            
            if (saveSuccess)
            {
                Debug.Log($"Successfully saved god {newGod.name} to JSON file!");
                Debug.Log($"Total gods in collection: {savedGeneratedGods.generatedGods.Count}");
            }
            else
            {
                Debug.LogError($"Failed to save god {newGod.name} to JSON file!");
            }
        }

        private static Dictionary<string, float> GenerateGodStats(System.Random random)
        {
            var stats = new Dictionary<string, float>
            {
                { "damage", 30f + random.Next(0, 30) },
                { "health", 400f + random.Next(0, 300) },
                { "mana", 250f + random.Next(0, 150) },
                { "attack_speed", 10f + random.Next(0, 15) },
                { "accuracy", 25f + random.Next(0, 20) },
                { "armor", 15f + random.Next(0, 25) },
                { "critical_chance", 0.03f + (float)(random.NextDouble() * 0.07) },
                { "range", 6f + random.Next(0, 6) },
                { "scale", 0.06f + (float)(random.NextDouble() * 0.04) },
                { "max_nutrition", 50f + random.Next(0, 30) },
                { "offspring", 50f + random.Next(0, 40) }
            };

            Debug.Log($"Generated stats - Health: {stats["health"]}, Damage: {stats["damage"]}");
            return stats;
        }

        private static string GenerateGodName(System.Random random)
        {
            var prefixes = new[] { "Mighty", "Dark", "Ancient", "Eternal", "Divine", "Cursed", "Holy" };
            var names = new[] { "Zephyr", "Mortis", "Ignis", "Aqua", "Terra", "Umbra", "Lux", "Ventus" };
            var suffixes = new[] { "the Destroyer", "the Creator", "the Wise", "the Terrible", "the Just", "the Corrupt" };

            return $"{prefixes[random.Next(prefixes.Length)]} {names[random.Next(names.Length)]} {suffixes[random.Next(suffixes.Length)]}";
        }

        private static string GenerateGodDescription(System.Random random)
        {
            var descriptions = new[]
            {
                "A procedurally generated deity of immense power.",
                "A god whose power grows with each victory.",
                "Ancient beyond measure, wielding forgotten magics.",
                "A divine being shaped by the world's needs."
            };

            return descriptions[random.Next(descriptions.Length)];
        }

        public static bool saveGeneratedGods()
        {
            try
            {
                Debug.Log($"Attempting to save to: {configFilePath}");
                Debug.Log($"Number of gods to save: {savedGeneratedGods.generatedGods.Count}");
                
                // Ensure directory exists
                string directory = Path.GetDirectoryName(configFilePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                    Debug.Log($"Created directory: {directory}");
                }

                string json = JsonConvert.SerializeObject(savedGeneratedGods, Formatting.Indented);
                Debug.Log($"JSON to write (first 200 chars): {json.Substring(0, Math.Min(200, json.Length))}...");
                
                File.WriteAllText(configFilePath, json);
                Debug.Log("File write completed successfully!");
                
                // Verify the file was written
                if (File.Exists(configFilePath))
                {
                    var fileInfo = new FileInfo(configFilePath);
                    Debug.Log($"File exists! Size: {fileInfo.Length} bytes, Last modified: {fileInfo.LastWriteTime}");
                    return true;
                }
                else
                {
                    Debug.LogError("File does not exist after write attempt!");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to save generated gods: {ex.Message}");
                Debug.LogError($"Stack trace: {ex.StackTrace}");
                return false;
            }
        }

        public static bool loadGeneratedGods()
        {
            try
            {
                Debug.Log($"Attempting to load from: {configFilePath}");
                
                if (!File.Exists(configFilePath))
                {
                    Debug.Log("Config file doesn't exist, creating new settings.");
                    savedGeneratedGods = new GeneratedGodsSettings();
                    return saveGeneratedGods(); // Create the file
                }

                string data = File.ReadAllText(configFilePath);
                Debug.Log($"Read {data.Length} characters from file");
                
                GeneratedGodsSettings loadedData = JsonConvert.DeserializeObject<GeneratedGodsSettings>(data);
                
                if (loadedData == null)
                {
                    Debug.LogWarning("Loaded data is null, creating new settings");
                    savedGeneratedGods = new GeneratedGodsSettings();
                    return saveGeneratedGods();
                }
                
                if (loadedData.settingVersion != correctSettingsVersion)
                {
                    Debug.LogWarning($"Version mismatch: {loadedData.settingVersion} vs {correctSettingsVersion}");
                    savedGeneratedGods = new GeneratedGodsSettings();
                    return saveGeneratedGods();
                }

                savedGeneratedGods = loadedData;
                Debug.Log($"Successfully loaded {savedGeneratedGods.generatedGods.Count} gods from file");
                foreach (var god in savedGeneratedGods.generatedGods)
                {
                    Traits.TraitStats.Add(god.id, god.stats);
                    ActorTrait genGod = new ActorTrait();
                    genGod.id = god.id;
                    genGod.path_icon = god.path_icon;
                    genGod.group_id = "GodTraits";
                    genGod.rarity = Rarity.R3_Legendary;
                    AddTrait(genGod, god.description);
                    Debug.Log($"Loaded god: {god.name} (ID: {god.id})");
                    Debug.Log($" - Description: {god.description}");
                    Debug.Log($" - Rarity: {god.rarity}");
                    Debug.Log($" - Stats: {string.Join(", ", god.stats)}");
                    Debug.Log($" - Created at: {god.created_at}");
                    Debug.Log($" - Can be given: {god.can_be_given}");
                    Debug.Log($" - Icon path: {god.path_icon}");


                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to load generated gods: {ex.Message}");
                Debug.LogError($"Stack trace: {ex.StackTrace}");
                savedGeneratedGods = new GeneratedGodsSettings();
                return false;
            }
        }

        // Utility methods for managing the JSON data (no game interaction)
        public static void ClearAllGeneratedGods()
        {
            Debug.Log("Clearing all generated gods from memory and file");
            savedGeneratedGods.generatedGods.Clear();
            saveGeneratedGods();
        }

        public static void RemoveGeneratedGod(string godId)
        {
            int removed = savedGeneratedGods.generatedGods.RemoveAll(g => g.id == godId);
            Debug.Log($"Removed {removed} gods with ID: {godId}");
            if (removed > 0)
            {
                saveGeneratedGods();
            }
        }

        public static GeneratedGodData GetGeneratedGod(string godId)
        {
            return savedGeneratedGods.generatedGods.Find(g => g.id == godId);
        }

        public static List<GeneratedGodData> GetAllGeneratedGods()
        {
            return new List<GeneratedGodData>(savedGeneratedGods.generatedGods);
        }

       
    }
}
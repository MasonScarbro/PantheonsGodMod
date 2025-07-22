using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using NCMS;
using NeoModLoader.constants;

namespace GodsAndPantheons
{
    public static class ProceduralAssetGenerator
    {
        [System.Serializable]
        public class ColorPalette
        {
            public string godType;
            public Color[] colors;
            
            public ColorPalette(string type, Color[] colorArray)
            {
                godType = type;
                colors = colorArray;
            }
        }

        [System.Serializable]
        public class GeneratorSettings
        {
            public int canvasSize = 32;
            public int numberOfAssets = 100;
            public bool enableGeneration = true;
            public float motifChance = 0.5f;
            public float detailChance = 0.4f;
            public float proceduralMotifChance = 0.3f;
            
        }

        private static ColorPalette[] colorsByGod = new ColorPalette[]
        {
            new ColorPalette("Moon", new Color[] { 
                new Color(170/255f, 204/255f, 1f), 
                Color.white, 
                new Color(100/255f, 150/255f, 1f) 
            }),
            new ColorPalette("Lich", new Color[] { 
                new Color(80/255f, 80/255f, 80/255f), 
                new Color(180/255f, 1f, 190/255f), 
                new Color(0f, 1f, 180/255f) 
            }),
            new ColorPalette("Love", new Color[] { 
                new Color(1f, 100/255f, 150/255f), 
                new Color(1f, 180/255f, 200/255f), 
                new Color(200/255f, 50/255f, 120/255f) 
            }),
            new ColorPalette("Earth", new Color[] { 
                new Color(80/255f, 50/255f, 20/255f), 
                new Color(120/255f, 90/255f, 40/255f), 
                new Color(160/255f, 130/255f, 70/255f) 
            }),
            new ColorPalette("Light", new Color[] { 
                new Color(1f, 1f, 200/255f), 
                new Color(1f, 1f, 150/255f), 
                new Color(1f, 220/255f, 100/255f) 
            }),
            new ColorPalette("Night", new Color[] { 
                new Color(50/255f, 50/255f, 100/255f), 
                new Color(30/255f, 30/255f, 60/255f), 
                new Color(100/255f, 80/255f, 120/255f) 
            }),
            new ColorPalette("Knowledge", new Color[] { 
                new Color(150/255f, 1f, 1f), 
                new Color(100/255f, 180/255f, 200/255f), 
                new Color(50/255f, 100/255f, 150/255f) 
            })
        };
        // Special handling rules for different detail types
        [System.Serializable]
        public class DetailRule
        {
            public string detailName;
            public bool allowMotifs;
            public bool useSpreadPattern;
            public string spreadType; // "rain", "scatter", "cluster", "spiral"
            public int minSpread;
            public int maxSpread;
            public bool allowRotation;
            public bool allowScaling;
            public float minScale;
            public float maxScale;
            
            // Cloud-specific settings
            public bool isMultiShape; // For clouds that should use multiple shapes
            public int minShapeCount;
            public int maxShapeCount;
        }

        private static DetailRule[] detailRules = new DetailRule[]
        {
            new DetailRule { 
                detailName = "cloud", 
                allowMotifs = false, 
                useSpreadPattern = true,
                spreadType = "multi_cloud", 
                minSpread = 3, 
                maxSpread = 8, 
                allowRotation = true,
                allowScaling = true,
                minScale = 0.3f,
                maxScale = 0.8f,
                isMultiShape = true,
                minShapeCount = 3,
                maxShapeCount = 7
            },
            new DetailRule { 
                detailName = "star", 
                allowMotifs = true, 
                useSpreadPattern = true,
                spreadType = "scatter", 
                minSpread = 2, 
                maxSpread = 6, 
                allowRotation = true,
                allowScaling = true,
                minScale = 0.4f,
                maxScale = 1.0f
            },
            new DetailRule { 
                detailName = "leaf", 
                allowMotifs = true, 
                useSpreadPattern = true,
                spreadType = "spiral", 
                minSpread = 3, 
                maxSpread = 7, 
                allowRotation = true,
                allowScaling = true,
                minScale = 0.5f,
                maxScale = 1.0f
            },
            new DetailRule { 
                detailName = "ember", 
                allowMotifs = true, 
                useSpreadPattern = true,
                spreadType = "cluster", 
                minSpread = 4, 
                maxSpread = 10, 
                allowRotation = false,
                allowScaling = true,
                minScale = 0.3f,
                maxScale = 0.7f
            }
        };

        public static class GoldenRatio
        {
            public const float PHI = 1.618033988749f;
            public const float INV_PHI = 0.618033988749f; // 1/φ

            public static Vector2 GetGoldenPoint(int canvasSize, int quadrant = -1)
            {
                // If no quadrant specified, choose randomly
                if (quadrant == -1) quadrant = UnityEngine.Random.Range(0, 4);

                float goldenX = canvasSize * INV_PHI;
                float goldenY = canvasSize * INV_PHI;

                switch (quadrant)
                {
                    case 0: return new Vector2(goldenX, goldenY); // Bottom-right golden point
                    case 1: return new Vector2(canvasSize - goldenX, goldenY); // Bottom-left
                    case 2: return new Vector2(canvasSize - goldenX, canvasSize - goldenY); // Top-left
                    case 3: return new Vector2(goldenX, canvasSize - goldenY); // Top-right
                }
                return new Vector2(canvasSize * 0.5f, canvasSize * 0.5f); // Fallback to center
            }

            public static Vector2 GetComplementaryPoint(Vector2 primaryPoint, int canvasSize)
            {
                // Get the opposite golden ratio point
                return new Vector2(canvasSize - primaryPoint.x, canvasSize - primaryPoint.y);
            }

            public static Vector2 GetSpiralPoint(int canvasSize, float t, float radius = -1)
            {
                if (radius < 0) radius = canvasSize * 0.3f;

                // Golden spiral equation
                float angle = t * 2 * Mathf.PI;
                float r = radius * Mathf.Pow(PHI, angle / (2 * Mathf.PI));

                Vector2 center = new Vector2(canvasSize * 0.5f, canvasSize * 0.5f);
                return center + new Vector2(Mathf.Cos(angle) * r, Mathf.Sin(angle) * r);
            }
            public static Vector2 GetRuleOfThirdsPoint(int canvasSize, int position)
            {
                float third = canvasSize / 3f;
                switch (position % 9)
                {
                    case 0: return new Vector2(third, third);
                    case 1: return new Vector2(third * 2, third);
                    case 2: return new Vector2(third, third * 2);
                    case 3: return new Vector2(third * 2, third * 2);
                    case 4: return new Vector2(third * 0.5f, third * 1.5f);
                    case 5: return new Vector2(third * 2.5f, third * 1.5f);
                    case 6: return new Vector2(third * 1.5f, third * 0.5f);
                    case 7: return new Vector2(third * 1.5f, third * 2.5f);
                    default: return new Vector2(third * 1.5f, third * 1.5f);
                }
            }
        }


        private static GeneratorSettings settings = new GeneratorSettings();
        private static string modPath;
        private static string shapesPath;
        private static string motifsPath;
        private static string detailsPath;
        private static string outputPath;

        private static Texture2D[] shapeTextures;
        private static Texture2D[] motifTextures;
        private static Texture2D[] detailTextures;

        public static void Init()
        {
            try
            {
                Debug.Log("GodsAndPantheons: Initializing ProceduralAssetGenerator");
                // Get mod path using NeoModLoader constants
                modPath = Path.Combine(Paths.ModsPath, "GodsAndPantheons");

                // Setup paths
                shapesPath = "C:/Program Files (x86)/Steam/steamapps/common/worldbox/Mods/Pantheon Mod/GameResources/effects/shapes";
                motifsPath = "C:/Program Files (x86)/Steam/steamapps/common/worldbox/Mods/Pantheon Mod/GameResources/effects/motifs";
                detailsPath = "C:/Program Files (x86)/Steam/steamapps/common/worldbox/Mods/Pantheon Mod/GameResources/effects/details";
                outputPath = "C:/Program Files (x86)/Steam/steamapps/common/worldbox/Mods/Pantheon Mod/GameResources/effects/gened";

                

                // Load settings
                LoadSettings();

                // Load assets
                LoadAssets();
                GenerateAssets();
                Debug.Log("GodsAndPantheons: ProceduralAssetGenerator initialized successfully");
            }
            catch (Exception e)
            {
                Debug.LogError($"GodsAndPantheons: Failed to initialize ProceduralAssetGenerator: {e.Message}");
            }
        }

        private static void CreateDirectoryIfNotExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private static void LoadSettings()
        {
            string settingsPath = Path.Combine(Paths.ModsConfigPath, "GodsAndPantheons_AssetGenerator.json");
            
            if (File.Exists(settingsPath))
            {
                try
                {
                    string json = File.ReadAllText(settingsPath);
                    settings = JsonConvert.DeserializeObject<GeneratorSettings>(json) ?? new GeneratorSettings();
                }
                catch (Exception e)
                {
                    Debug.LogWarning($"GodsAndPantheons: Failed to load asset generator settings: {e.Message}");
                    settings = new GeneratorSettings();
                }
            }
            
            SaveSettings();
        }

        private static void SaveSettings()
        {
            try
            {
                string settingsPath = Path.Combine(Paths.ModsConfigPath, "GodsAndPantheons_AssetGenerator.json");
                string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
                File.WriteAllText(settingsPath, json);
            }
            catch (Exception e)
            {
                Debug.LogError($"GodsAndPantheons: Failed to save asset generator settings: {e.Message}");
            }
        }

        private static void LoadAssets()
        {
            shapeTextures = LoadTexturesFromPath(shapesPath);
            motifTextures = LoadTexturesFromPath(motifsPath);
            detailTextures = LoadTexturesFromPath(detailsPath);

            Debug.Log($"GodsAndPantheons: Loaded {shapeTextures.Length} shapes, {motifTextures.Length} motifs, {detailTextures.Length} details");
        }

        private static Texture2D[] LoadTexturesFromPath(string path)
        {
            List<Texture2D> textures = new List<Texture2D>();
            
            if (Directory.Exists(path))
            {
                string[] files = Directory.GetFiles(path, "*.png");
                foreach (string file in files)
                {
                    try
                    {
                        Texture2D texture = LoadTexture(file);
                        if (texture != null)
                        {
                            texture.name = Path.GetFileNameWithoutExtension(file);
                            Debug.Log($"GodsAndPantheons: Loaded texture {texture.name} from {file}");
                            textures.Add(texture);
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogWarning($"GodsAndPantheons: Failed to load texture {file}: {e.Message}");
                    }
                }
            }
            
            return textures.ToArray();
        }

        private static Texture2D LoadTexture(string filePath)
        {
            byte[] fileData = File.ReadAllBytes(filePath);
            Texture2D texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
            texture.LoadImage(fileData);
            return texture;
        }

        public static void GenerateAssets()
        {
            if (!settings.enableGeneration)
            {
                Debug.Log("GodsAndPantheons: Asset generation is disabled in settings");
                return;
            }

            if (shapeTextures.Length == 0)
            {
                Debug.LogWarning("GodsAndPantheons: No shape textures found, skipping generation");
                return;
            }

            try
            {
                for (int i = 0; i < settings.numberOfAssets; i++)
                {
                    GenerateSingleAsset(i);
                }
                Debug.Log($"GodsAndPantheons: Generated {settings.numberOfAssets} procedural assets");
            }
            catch (Exception e)
            {
                Debug.LogError($"GodsAndPantheons: Failed to generate assets: {e.Message}");
            }
        }

        private static void GenerateSingleAsset(int index)
        {
            // Choose random god type and palette
            ColorPalette selectedPalette = colorsByGod[UnityEngine.Random.Range(0, colorsByGod.Length)];
            Color[] palette = selectedPalette.colors;

            // Create base canvas
            Texture2D canvas = new Texture2D(settings.canvasSize, settings.canvasSize, TextureFormat.RGBA32, false);
            Color[] pixels = new Color[settings.canvasSize * settings.canvasSize];
            for (int i = 0; i < pixels.Length; i++)
                pixels[i] = Color.clear;
            canvas.SetPixels(pixels);

            // Track filled pixels for overlap detection
            bool[,] mask = new bool[settings.canvasSize, settings.canvasSize];

            // Add shapes
            Texture2D shapeTexture = shapeTextures[UnityEngine.Random.Range(0, shapeTextures.Length)];
            Texture2D coloredShape = ApplyPalette(shapeTexture, palette);

            int maxClones = UnityEngine.Random.Range(1, 4);
            int clonesAdded = 0;
            int attempts = 0;

            while (clonesAdded < maxClones && attempts < 20)
            {
                int x = UnityEngine.Random.Range(0, settings.canvasSize - coloredShape.width + 1);
                int y = UnityEngine.Random.Range(0, settings.canvasSize - coloredShape.height + 1);

                if (!CheckOverlap(mask, x, y, coloredShape))
                {
                    CompositeTexture(canvas, coloredShape, x, y);
                    UpdateMask(mask, x, y, coloredShape);
                    clonesAdded++;
                }
                attempts++;
            }

            string selectedDetailType = "";
            DetailRule activeRule = null;

            if (detailTextures.Length > 0 && UnityEngine.Random.value < settings.detailChance)
            {
                Texture2D selectedDetail = detailTextures[UnityEngine.Random.Range(0, detailTextures.Length)];
                selectedDetailType = selectedDetail.name.ToLower();
                
                // Find matching rule
                foreach (var rule in detailRules)
                {
                    if (selectedDetailType.Contains(rule.detailName))
                    {
                        activeRule = rule;
                        break;
                    }
                }
            }

            bool shouldAddMotif = motifTextures.Length > 0 && UnityEngine.Random.value < settings.motifChance;
            if (activeRule != null && !activeRule.allowMotifs)
                shouldAddMotif = false;

            if (shouldAddMotif)
            {
                
                // Chance for procedural motif instead of texture-based
                if (UnityEngine.Random.value < settings.proceduralMotifChance)
                {
                    AddProceduralMotif(canvas, palette, selectedPalette.godType);
                }
                else
                {
                    AddMotif(canvas, palette);
                }
            }
            if (detailTextures.Length > 0 && UnityEngine.Random.value < settings.detailChance)
            {
                if (activeRule != null && activeRule.useSpreadPattern)
                {
                    AddSpreadDetails(canvas, palette, activeRule);
                }
                else
                {
                    AddDetail(canvas, palette);
                }
            }

            canvas.Apply();
            byte[] pngData = canvas.EncodeToPNG();
            string filename = $"{selectedPalette.godType.ToLower()}_power_{index}.png";
            File.WriteAllBytes(Path.Combine(outputPath, filename), pngData);

            UnityEngine.Object.DestroyImmediate(canvas);
            UnityEngine.Object.DestroyImmediate(coloredShape);
        }

        private static Texture2D ApplyPalette(Texture2D source, Color[] palette)
        {
            Texture2D result = new Texture2D(source.width, source.height, TextureFormat.RGBA32, false);
            Color[] pixels = source.GetPixels();

            for (int i = 0; i < pixels.Length; i++)
            {
                Color pixel = pixels[i];
                if (pixel.a == 0) continue;

                float brightness = (pixel.r + pixel.g + pixel.b) / 3f;
                int index = Mathf.FloorToInt(brightness * (palette.Length - 1));
                index = Mathf.Clamp(index, 0, palette.Length - 1);
                
                Color newColor = palette[index];
                pixels[i] = new Color(newColor.r, newColor.g, newColor.b, pixel.a);
            }

            result.SetPixels(pixels);
            result.Apply();
            return result;
        }
        private static void AddProceduralMotif(Texture2D canvas, Color[] palette, string godType)
        {
            Color motifColor = palette[UnityEngine.Random.Range(0, palette.Length)];
            
            // Different procedural patterns based on god type
            switch (godType.ToLower())
            {
                case "light":
                    DrawRadialBurst(canvas, motifColor);
                    break;
                case "night":
                    DrawStarTrail(canvas, motifColor);
                    break;
                case "moon":
                    DrawCrescentTrail(canvas, motifColor);
                    break;
                case "earth":
                    DrawRootNetwork(canvas, motifColor);
                    break;
                case "love":
                    DrawHeartPulse(canvas, motifColor);
                    break;
                case "lich":
                    DrawNecroticWeb(canvas, motifColor);
                    break;
                default:
                    DrawSpiralEnergy(canvas, motifColor);
                    break;
            }
        }

        private static void AddSpreadDetails(Texture2D canvas, Color[] palette, DetailRule rule)
        {
            Texture2D detail = detailTextures[UnityEngine.Random.Range(0, detailTextures.Length)];
            if (!detail.name.ToLower().Contains(rule.detailName))
                return; // Safety check

            detail = ApplyPalette(detail, palette);
            int count = UnityEngine.Random.Range(rule.minSpread, rule.maxSpread + 1);

            switch (rule.spreadType)
            {
                case "rain":
                    SpreadRainPattern(canvas, detail, count, rule.allowRotation);
                    break;
                case "scatter":
                    SpreadScatterPattern(canvas, detail, count, rule.allowRotation);
                    break;
                case "cluster":
                    SpreadClusterPattern(canvas, detail, count, rule.allowRotation);
                    break;
                case "spiral":
                    SpreadSpiralPattern(canvas, detail, count, rule.allowRotation);
                    break;
            }

            UnityEngine.Object.DestroyImmediate(detail);
        }


        private static void DrawRadialBurst(Texture2D canvas, Color color)
        {
            int centerX = settings.canvasSize / 2;
            int centerY = settings.canvasSize / 2;
            int rays = UnityEngine.Random.Range(6, 12);
            
            for (int i = 0; i < rays; i++)
            {
                float angle = (float)i / rays * 2f * Mathf.PI;
                int length = UnityEngine.Random.Range(6, 12);
                DrawLine(canvas, centerX, centerY, 
                        centerX + (int)(Mathf.Cos(angle) * length),
                        centerY + (int)(Mathf.Sin(angle) * length), color);
            }
        }

        private static void DrawStarTrail(Texture2D canvas, Color color)
        {
            int points = UnityEngine.Random.Range(8, 16);
            for (int i = 0; i < points; i++)
            {
                int x = UnityEngine.Random.Range(2, settings.canvasSize - 2);
                int y = UnityEngine.Random.Range(2, settings.canvasSize - 2);
                
                // Create small star shape
                canvas.SetPixel(x, y, color);
                canvas.SetPixel(x-1, y, Color.Lerp(color, Color.clear, 0.5f));
                canvas.SetPixel(x+1, y, Color.Lerp(color, Color.clear, 0.5f));
                canvas.SetPixel(x, y-1, Color.Lerp(color, Color.clear, 0.5f));
                canvas.SetPixel(x, y+1, Color.Lerp(color, Color.clear, 0.5f));
            }
        }

        private static void DrawCrescentTrail(Texture2D canvas, Color color)
        {
            int centerX = settings.canvasSize / 2;
            int centerY = settings.canvasSize / 2;
            int radius = UnityEngine.Random.Range(4, 8);
            
            // Draw crescent arc
            for (float angle = -Mathf.PI/3; angle < Mathf.PI/3; angle += 0.2f)
            {
                int x = centerX + (int)(Mathf.Cos(angle) * radius);
                int y = centerY + (int)(Mathf.Sin(angle) * radius);
                if (x >= 0 && x < settings.canvasSize && y >= 0 && y < settings.canvasSize)
                    canvas.SetPixel(x, y, color);
            }
        }

        private static void DrawRootNetwork(Texture2D canvas, Color color)
        {
            int startX = settings.canvasSize / 2;
            int startY = settings.canvasSize - 1;
            
            // Recursive branching roots
            DrawBranch(canvas, startX, startY, -Mathf.PI/2, 6, color, 0);
        }

        private static void DrawBranch(Texture2D canvas, int x, int y, float angle, int length, Color color, int depth)
        {
            if (depth > 3 || length < 2) return;
            
            int endX = x + (int)(Mathf.Cos(angle) * length);
            int endY = y + (int)(Mathf.Sin(angle) * length);
            
            DrawLine(canvas, x, y, endX, endY, Color.Lerp(color, Color.clear, depth * 0.2f));
            
            // Branch
            if (UnityEngine.Random.value < 0.7f)
            {
                float leftAngle = angle - UnityEngine.Random.Range(0.3f, 0.8f);
                float rightAngle = angle + UnityEngine.Random.Range(0.3f, 0.8f);
                int newLength = length - UnityEngine.Random.Range(1, 3);
                
                DrawBranch(canvas, endX, endY, leftAngle, newLength, color, depth + 1);
                DrawBranch(canvas, endX, endY, rightAngle, newLength, color, depth + 1);
            }
        }

        private static void DrawHeartPulse(Texture2D canvas, Color color)
        {
            int centerX = settings.canvasSize / 2;
            int centerY = settings.canvasSize / 2;
            int rings = UnityEngine.Random.Range(2, 4);
            
            for (int ring = 0; ring < rings; ring++)
            {
                int radius = (ring + 1) * 3;
                Color ringColor = Color.Lerp(color, Color.clear, (float)ring / rings);
                
                // Draw circle
                for (float angle = 0; angle < 2 * Mathf.PI; angle += 0.3f)
                {
                    int x = centerX + (int)(Mathf.Cos(angle) * radius);
                    int y = centerY + (int)(Mathf.Sin(angle) * radius);
                    if (x >= 0 && x < settings.canvasSize && y >= 0 && y < settings.canvasSize)
                        canvas.SetPixel(x, y, ringColor);
                }
            }
        }

        private static void DrawNecroticWeb(Texture2D canvas, Color color)
        {
            int nodes = UnityEngine.Random.Range(4, 8);
            Vector2[] nodePositions = new Vector2[nodes];
            
            // Place nodes
            for (int i = 0; i < nodes; i++)
            {
                nodePositions[i] = new Vector2(
                    UnityEngine.Random.Range(4, settings.canvasSize - 4),
                    UnityEngine.Random.Range(4, settings.canvasSize - 4)
                );
            }
            
            // Connect nearby nodes
            for (int i = 0; i < nodes; i++)
            {
                for (int j = i + 1; j < nodes; j++)
                {
                    float distance = Vector2.Distance(nodePositions[i], nodePositions[j]);
                    if (distance < 12 && UnityEngine.Random.value < 0.6f)
                    {
                        DrawLine(canvas, 
                               (int)nodePositions[i].x, (int)nodePositions[i].y,
                               (int)nodePositions[j].x, (int)nodePositions[j].y, 
                               Color.Lerp(color, Color.clear, distance / 20f));
                    }
                }
            }
        }

        private static void DrawSpiralEnergy(Texture2D canvas, Color color)
        {
            int centerX = settings.canvasSize / 2;
            int centerY = settings.canvasSize / 2;
            float maxRadius = settings.canvasSize / 3;
            
            for (float t = 0; t < 4 * Mathf.PI; t += 0.2f)
            {
                float radius = (t / (4 * Mathf.PI)) * maxRadius;
                int x = centerX + (int)(Mathf.Cos(t) * radius);
                int y = centerY + (int)(Mathf.Sin(t) * radius);
                
                if (x >= 0 && x < settings.canvasSize && y >= 0 && y < settings.canvasSize)
                {
                    Color fadeColor = Color.Lerp(color, Color.clear, radius / maxRadius);
                    canvas.SetPixel(x, y, fadeColor);
                }
            }
        }

        // Spread pattern methods
        private static void SpreadRainPattern(Texture2D canvas, Texture2D detail, int count, bool allowRotation)
        {
            for (int i = 0; i < count; i++)
            {
                int x = UnityEngine.Random.Range(0, settings.canvasSize - detail.width + 1);
                int y = UnityEngine.Random.Range(0, (int)(settings.canvasSize * 0.7f)); // Upper portion
                
                CompositeTexture(canvas, detail, x, y);
            }
        }

        private static void SpreadScatterPattern(Texture2D canvas, Texture2D detail, int count, bool allowRotation)
        {
            for (int i = 0; i < count; i++)
            {
                int x = UnityEngine.Random.Range(0, settings.canvasSize - detail.width + 1);
                int y = UnityEngine.Random.Range(0, settings.canvasSize - detail.height + 1);
                
                Texture2D workingDetail = detail;
                if (allowRotation && UnityEngine.Random.value < 0.5f)
                {
                    int rotations = UnityEngine.Random.Range(1, 4);
                    workingDetail = detail;
                    for (int r = 0; r < rotations; r++)
                    {
                        Texture2D rotated = RotateTexture90(workingDetail);
                        if (workingDetail != detail) UnityEngine.Object.DestroyImmediate(workingDetail);
                        workingDetail = rotated;
                    }
                }
                
                CompositeTexture(canvas, workingDetail, x, y);
                if (workingDetail != detail) UnityEngine.Object.DestroyImmediate(workingDetail);
            }
        }

        private static void SpreadClusterPattern(Texture2D canvas, Texture2D detail, int count, bool allowRotation)
        {
            int clusters = UnityEngine.Random.Range(2, 4);
            int itemsPerCluster = count / clusters;
            
            for (int c = 0; c < clusters; c++)
            {
                int clusterX = UnityEngine.Random.Range(detail.width, settings.canvasSize - detail.width);
                int clusterY = UnityEngine.Random.Range(detail.height, settings.canvasSize - detail.height);
                int clusterRadius = UnityEngine.Random.Range(3, 7);
                
                for (int i = 0; i < itemsPerCluster; i++)
                {
                    float angle = UnityEngine.Random.Range(0f, 2f * Mathf.PI);
                    float distance = UnityEngine.Random.Range(0f, clusterRadius);
                    
                    int x = clusterX + (int)(Mathf.Cos(angle) * distance) - detail.width / 2;
                    int y = clusterY + (int)(Mathf.Sin(angle) * distance) - detail.height / 2;
                    
                    x = Mathf.Clamp(x, 0, settings.canvasSize - detail.width);
                    y = Mathf.Clamp(y, 0, settings.canvasSize - detail.height);
                    
                    CompositeTexture(canvas, detail, x, y);
                }
            }
        }

        private static void SpreadSpiralPattern(Texture2D canvas, Texture2D detail, int count, bool allowRotation)
        {
            int centerX = settings.canvasSize / 2;
            int centerY = settings.canvasSize / 2;
            float maxRadius = (settings.canvasSize / 2) - detail.width;
            
            for (int i = 0; i < count; i++)
            {
                float t = (float)i / count;
                float angle = t * 4 * Mathf.PI;
                float radius = t * maxRadius;
                
                int x = centerX + (int)(Mathf.Cos(angle) * radius) - detail.width / 2;
                int y = centerY + (int)(Mathf.Sin(angle) * radius) - detail.height / 2;
                
                x = Mathf.Clamp(x, 0, settings.canvasSize - detail.width);
                y = Mathf.Clamp(y, 0, settings.canvasSize - detail.height);
                
                CompositeTexture(canvas, detail, x, y);
            }
        }

        private static bool CheckOverlap(bool[,] mask, int x, int y, Texture2D texture)
        {
            Color[] pixels = texture.GetPixels();

            for (int py = 0; py < texture.height; py++)
            {
                for (int px = 0; px < texture.width; px++)
                {
                    int maskX = x + px;
                    int maskY = y + py;

                    if (maskX >= 0 && maskX < settings.canvasSize && maskY >= 0 && maskY < settings.canvasSize)
                    {
                        Color pixel = pixels[py * texture.width + px];
                        if (pixel.a > 0 && mask[maskX, maskY])
                            return true;
                    }
                }
            }
            return false;
        }

        private static void UpdateMask(bool[,] mask, int x, int y, Texture2D texture)
        {
            Color[] pixels = texture.GetPixels();
            
            for (int py = 0; py < texture.height; py++)
            {
                for (int px = 0; px < texture.width; px++)
                {
                    int maskX = x + px;
                    int maskY = y + py;
                    
                    if (maskX >= 0 && maskX < settings.canvasSize && maskY >= 0 && maskY < settings.canvasSize)
                    {
                        Color pixel = pixels[py * texture.width + px];
                        if (pixel.a > 0)
                            mask[maskX, maskY] = true;
                    }
                }
            }
        }

        private static void CompositeTexture(Texture2D canvas, Texture2D overlay, int x, int y)
        {
            Color[] canvasPixels = canvas.GetPixels();
            Color[] overlayPixels = overlay.GetPixels();

            for (int py = 0; py < overlay.height; py++)
            {
                for (int px = 0; px < overlay.width; px++)
                {
                    int canvasX = x + px;
                    int canvasY = y + py;
                    
                    if (canvasX >= 0 && canvasX < settings.canvasSize && canvasY >= 0 && canvasY < settings.canvasSize)
                    {
                        int canvasIndex = canvasY * settings.canvasSize + canvasX;
                        int overlayIndex = py * overlay.width + px;
                        
                        Color overlayPixel = overlayPixels[overlayIndex];
                        if (overlayPixel.a > 0)
                        {
                            Color canvasPixel = canvasPixels[canvasIndex];
                            canvasPixels[canvasIndex] = Color.Lerp(canvasPixel, overlayPixel, overlayPixel.a);
                        }
                    }
                }
            }
            
            canvas.SetPixels(canvasPixels);
        }

        private static void AddMotif(Texture2D canvas, Color[] palette)
        {
            Texture2D motif = motifTextures[UnityEngine.Random.Range(0, motifTextures.Length)];
            motif = ApplyPalette(motif, palette);

            // Rotation
            int rotations = UnityEngine.Random.Range(0, 4);
            for (int i = 0; i < rotations; i++)
            {
                Texture2D rotated = RotateTexture90(motif);
                if (motif != motifTextures[0])
                    UnityEngine.Object.DestroyImmediate(motif);
                motif = rotated;
            }

            // IMPROVED: Better scaling logic for 32x32 canvas
            float maxScaleX = (float)(settings.canvasSize - 4) / motif.width; // Leave 2px margin each side
            float maxScaleY = (float)(settings.canvasSize - 4) / motif.height;
            float maxScale = Mathf.Min(0.9f, maxScaleX, maxScaleY); // Max 90% of available space
            float scale = UnityEngine.Random.Range(0.4f, maxScale);
            
            int newWidth = Mathf.RoundToInt(motif.width * scale);
            int newHeight = Mathf.RoundToInt(motif.height * scale);
            Texture2D resized = ResizeTexture(motif, newWidth, newHeight);
            UnityEngine.Object.DestroyImmediate(motif);
            motif = resized;

            // IMPROVED: Better placement logic
            string[] modes = { "golden_ratio", "complementary_golden", "rule_of_thirds", "golden_spiral", "centered_offset" };
            string mode = modes[UnityEngine.Random.Range(0, modes.Length)];


            switch (mode)
            {
                case "golden_ratio":
                    Vector2 goldenPoint = GoldenRatio.GetGoldenPoint(settings.canvasSize);
                    int gx = Mathf.RoundToInt(goldenPoint.x) - motif.width / 2;
                    int gy = Mathf.RoundToInt(goldenPoint.y) - motif.height / 2;
                    gx = Mathf.Clamp(gx, 0, settings.canvasSize - motif.width);
                    gy = Mathf.Clamp(gy, 0, settings.canvasSize - motif.height);
                    CompositeTexture(canvas, motif, gx, gy);
                    break;

                case "complementary_golden":
                    Vector2 primary = GoldenRatio.GetGoldenPoint(settings.canvasSize, 0);
                    Vector2 complement = GoldenRatio.GetComplementaryPoint(primary, settings.canvasSize);
                    
                    // Place at primary
                    int px = Mathf.RoundToInt(primary.x) - motif.width / 2;
                    int py = Mathf.RoundToInt(primary.y) - motif.height / 2;
                    px = Mathf.Clamp(px, 0, settings.canvasSize - motif.width);
                    py = Mathf.Clamp(py, 0, settings.canvasSize - motif.height);
                    CompositeTexture(canvas, motif, px, py);
                    
                    // 50% chance to also place at complement
                    if (UnityEngine.Random.value < 0.5f)
                    {
                        int cx = Mathf.RoundToInt(complement.x) - motif.width / 2;
                        int cy = Mathf.RoundToInt(complement.y) - motif.height / 2;
                        cx = Mathf.Clamp(cx, 0, settings.canvasSize - motif.width);
                        cy = Mathf.Clamp(cy, 0, settings.canvasSize - motif.height);
                        CompositeTexture(canvas, motif, cx, cy);
                    }
                    break;

                case "rule_of_thirds":
                    Vector2 rulePoint = GoldenRatio.GetRuleOfThirdsPoint(settings.canvasSize, UnityEngine.Random.Range(0, 9));
                    int rx = Mathf.RoundToInt(rulePoint.x) - motif.width / 2;
                    int ry = Mathf.RoundToInt(rulePoint.y) - motif.height / 2;
                    rx = Mathf.Clamp(rx, 0, settings.canvasSize - motif.width);
                    ry = Mathf.Clamp(ry, 0, settings.canvasSize - motif.height);
                    CompositeTexture(canvas, motif, rx, ry);
                    break;

                case "golden_spiral":
                    int spiralSteps = UnityEngine.Random.Range(3, 6);
                    for (int step = 0; step < spiralSteps; step++)
                    {
                        float t = (float)step / spiralSteps;
                        Vector2 spiralPoint = GoldenRatio.GetSpiralPoint(settings.canvasSize, t);
                        int sx = Mathf.RoundToInt(spiralPoint.x) - motif.width / 2;
                        int sy = Mathf.RoundToInt(spiralPoint.y) - motif.height / 2;
                        sx = Mathf.Clamp(sx, 0, settings.canvasSize - motif.width);
                        sy = Mathf.Clamp(sy, 0, settings.canvasSize - motif.height);
                        
                        if (sx >= 0 && sy >= 0)
                            CompositeTexture(canvas, motif, sx, sy);
                    }
                    break;

                case "centered_offset":
                    // Slight offset from center using golden ratio
                    int centerX = settings.canvasSize / 2;
                    int centerY = settings.canvasSize / 2;
                    float offsetAmount = settings.canvasSize * (1 - GoldenRatio.INV_PHI) * 0.3f;
                    int offsetX = centerX - motif.width / 2 + UnityEngine.Random.Range(-(int)offsetAmount, (int)offsetAmount + 1);
                    int offsetY = centerY - motif.height / 2 + UnityEngine.Random.Range(-(int)offsetAmount, (int)offsetAmount + 1);
                    offsetX = Mathf.Clamp(offsetX, 0, settings.canvasSize - motif.width);
                    offsetY = Mathf.Clamp(offsetY, 0, settings.canvasSize - motif.height);
                    CompositeTexture(canvas, motif, offsetX, offsetY);
                    break;
            }

            UnityEngine.Object.DestroyImmediate(motif);
        }

        private static void DrawLine(Texture2D canvas, int x0, int y0, int x1, int y1, Color color)
        {
            int dx = Mathf.Abs(x1 - x0);
            int dy = Mathf.Abs(y1 - y0);
            int sx = x0 < x1 ? 1 : -1;
            int sy = y0 < y1 ? 1 : -1;
            int err = dx - dy;

            while (true)
            {
                if (x0 >= 0 && x0 < settings.canvasSize && y0 >= 0 && y0 < settings.canvasSize)
                    canvas.SetPixel(x0, y0, color);

                if (x0 == x1 && y0 == y1) break;
                
                int e2 = 2 * err;
                if (e2 > -dy) { err -= dy; x0 += sx; }
                if (e2 < dx) { err += dx; y0 += sy; }
            }
        }

        private static void AddDetail(Texture2D canvas, Color[] palette)
        {
            Texture2D detail = detailTextures[UnityEngine.Random.Range(0, detailTextures.Length)];
            string detailName = detail.name.ToLower();
            detail = ApplyPalette(detail, palette);

            // Don't rotate clouds or vines


            if (!detailName.Contains("cloud") && !detailName.Contains("vine"))
            {
                Debug.Log($"GodsAndPantheons: detail wasnt cloud: '{detailName}' rotating");
                int rotations = UnityEngine.Random.Range(0, 4);
                for (int i = 0; i < rotations; i++)
                {
                    Texture2D rotated = RotateTexture90(detail);
                    if (detail != detailTextures[0]) // Don't destroy original
                        UnityEngine.Object.DestroyImmediate(detail);
                    detail = rotated;
                }
            }

            int maxX = Mathf.Max(0, settings.canvasSize - detail.width);
            int maxY = Mathf.Max(0, settings.canvasSize - detail.height);


            if (maxX < 0 || maxY < 0)
            {
                // Detail is too large, scale it down
                float scaleX = (float)settings.canvasSize / detail.width;
                float scaleY = (float)settings.canvasSize / detail.height;
                float scale = Mathf.Min(scaleX, scaleY) * 0.8f; // 80% of max to leave margin
                
                int newWidth = Mathf.RoundToInt(detail.width * scale);
                int newHeight = Mathf.RoundToInt(detail.height * scale);
                
                Texture2D resized = ResizeTexture(detail, newWidth, newHeight);
                UnityEngine.Object.DestroyImmediate(detail);
                detail = resized;
                
                maxX = settings.canvasSize - detail.width;
                maxY = settings.canvasSize - detail.height;
            }

            int x = UnityEngine.Random.Range(0, maxX + 1);
            int y = UnityEngine.Random.Range(0, maxY + 1);
            CompositeTexture(canvas, detail, x, y);

            UnityEngine.Object.DestroyImmediate(detail);
        }

        private static Texture2D RotateTexture90(Texture2D source)
        {
            Texture2D result = new Texture2D(source.height, source.width, TextureFormat.RGBA32, false);
            Color[] pixels = source.GetPixels();
            Color[] newPixels = new Color[pixels.Length];

            for (int y = 0; y < source.height; y++)
            {
                for (int x = 0; x < source.width; x++)
                {
                    int sourceIndex = y * source.width + x;
                    int newX = source.height - 1 - y;
                    int newY = x;
                    int newIndex = newY * result.width + newX;
                    newPixels[newIndex] = pixels[sourceIndex];
                }
            }

            result.SetPixels(newPixels);
            result.Apply();
            return result;
        }

        private static Texture2D ResizeTexture(Texture2D source, int newWidth, int newHeight)
        {
            Texture2D result = new Texture2D(newWidth, newHeight, TextureFormat.RGBA32, false);
            
            for (int y = 0; y < newHeight; y++)
            {
                for (int x = 0; x < newWidth; x++)
                {
                    float u = (float)x / newWidth;
                    float v = (float)y / newHeight;
                    Color pixel = source.GetPixelBilinear(u, v);
                    result.SetPixel(x, y, pixel);
                }
            }
            
            result.Apply();
            return result;
        }

        // Public API for external access
        public static void SetGenerationEnabled(bool enabled)
        {
            settings.enableGeneration = enabled;
            SaveSettings();
        }

        public static void SetAssetCount(int count)
        {
            settings.numberOfAssets = Mathf.Max(1, count);
            SaveSettings();
        }

        public static void SetCanvasSize(int size)
        {
            settings.canvasSize = Mathf.Max(16, size);
            SaveSettings();
        }
    }
}
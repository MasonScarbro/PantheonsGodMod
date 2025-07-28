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
            public int maxLayers = 5;
            public float layerSpacing = 0.618f; // Golden ratio spacing
            
        }

        [System.Serializable]
        public class LayerInfo
        {
            public Rect bounds;
            public Vector2 center;
            public LayerType type;
            public string elementName;
            public Color dominantColor;
            public bool[,] pixelMask;
            public int layerIndex;
            public float influence; // How much this layer affects placement of others
            
            public LayerInfo(Rect bounds, Vector2 center, LayerType type, string name, Color color, bool[,] mask, int layer)
            {
                this.bounds = bounds;
                this.center = center;
                this.type = type;
                this.elementName = name;
                this.dominantColor = color;
                this.pixelMask = mask;
                this.layerIndex = layer;
                this.influence = CalculateInfluence(type);
            }
            
            private float CalculateInfluence(LayerType type)
            {
                switch (type)
                {
                    case LayerType.Shape: return 1.0f;
                    case LayerType.Detail: return 0.8f;
                    case LayerType.Motif: return 0.6f;
                    case LayerType.ProceduralMotif: return 0.4f;
                    default: return 0.5f;
                }
            }
        }

        public enum LayerType
        {
            Shape,
            Detail,
            Motif,
            ProceduralMotif,
            Background
        }

        public enum PlacementBehavior
        {
            Around,      // Place around the reference layer
            Behind,      // Place behind (draw first)
            Following,   // Follow the contours/edges
            Radiating,   // Radiate outward from center
            Orbiting,    // Circular placement around center
            Complementary // Use golden ratio complementary positioning
        }

        [System.Serializable]
        public class ElementRule
        {
            public string elementName;
            public LayerType preferredLayer;
            public PlacementBehavior behavior;
            public float minDistance;
            public float maxDistance;
            public bool canOverlap;
            public LayerType[] avoidLayers;
            public LayerType[] attractToLayers;
            public float scaleWithDistance; // Scale based on distance from attracted layers
        }

        private static ElementRule[] elementRules = new ElementRule[]
        {
            new ElementRule { 
                elementName = "cloud", 
                preferredLayer = LayerType.Detail,
                behavior = PlacementBehavior.Behind,
                minDistance = 2f,
                maxDistance = 8f,
                canOverlap = false,
                avoidLayers = new LayerType[] { LayerType.Shape },
                attractToLayers = new LayerType[] { },
                scaleWithDistance = 0.1f
            },
            new ElementRule { 
                elementName = "star", 
                preferredLayer = LayerType.Detail,
                behavior = PlacementBehavior.Radiating,
                minDistance = 8f,
                maxDistance = 16f,
                canOverlap = true,
                avoidLayers = new LayerType[] { },
                attractToLayers = new LayerType[] { LayerType.Shape },
                scaleWithDistance = -0.05f
            },
            new ElementRule { 
                elementName = "burst", 
                preferredLayer = LayerType.ProceduralMotif,
                behavior = PlacementBehavior.Radiating,
                minDistance = 4f,
                maxDistance = 12f,
                canOverlap = true,
                avoidLayers = new LayerType[] { },
                attractToLayers = new LayerType[] { LayerType.Shape },
                scaleWithDistance = 0f
            }
        };

        public class CompositionPlanner
        {
            public List<LayerInfo> layers = new List<LayerInfo>();
            public Texture2D canvas;
            public ColorPalette palette;
            public int canvasSize;
            
            public CompositionPlanner(int size, ColorPalette colorPalette)
            {
                canvasSize = size;
                palette = colorPalette;
                canvas = new Texture2D(size, size, TextureFormat.RGBA32, false);
                ClearCanvas();
            }
            
            private void ClearCanvas()
            {
                Color[] pixels = new Color[canvasSize * canvasSize];
                for (int i = 0; i < pixels.Length; i++)
                    pixels[i] = Color.clear;
                canvas.SetPixels(pixels);
            }
            
            public Vector2 FindOptimalPlacement(LayerType type, Vector2 preferredSize, PlacementBehavior behavior, LayerType[] attractTo, LayerType[] avoid)
            {
                List<Vector2> candidates = new List<Vector2>();
                
                // Generate candidate positions based on behavior
                switch (behavior)
                {
                    case PlacementBehavior.Around:
                        candidates.AddRange(GenerateAroundPositions(attractTo, preferredSize));
                        break;
                    case PlacementBehavior.Radiating:
                        candidates.AddRange(GenerateRadiatingPositions(attractTo, preferredSize));
                        break;
                    case PlacementBehavior.Orbiting:
                        candidates.AddRange(GenerateOrbitingPositions(attractTo, preferredSize));
                        break;
                    case PlacementBehavior.Complementary:
                        candidates.AddRange(GenerateComplementaryPositions(preferredSize));
                        break;
                    case PlacementBehavior.Following:
                        candidates.AddRange(GenerateFollowingPositions(attractTo, preferredSize));
                        break;
                    default:
                        candidates.AddRange(GenerateRandomPositions(preferredSize, 20));
                        break;
                }
                
                // Score each candidate position
                Vector2 bestPosition = Vector2.zero;
                float bestScore = float.MinValue;
                
                foreach (Vector2 candidate in candidates)
                {
                    float score = EvaluatePosition(candidate, preferredSize, attractTo, avoid);
                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestPosition = candidate;
                    }
                }
                
                return bestPosition;
            }
            
            private List<Vector2> GenerateAroundPositions(LayerType[] attractTo, Vector2 size)
            {
                List<Vector2> positions = new List<Vector2>();
                
                foreach (LayerInfo layer in layers)
                {
                    if (Array.IndexOf(attractTo, layer.type) >= 0)
                    {
                        // Generate positions around this layer
                        int ringCount = 3;
                        for (int ring = 1; ring <= ringCount; ring++)
                        {
                            float radius = Mathf.Max(layer.bounds.width, layer.bounds.height) * 0.5f + ring * settings.layerSpacing * canvasSize;
                            int pointsInRing = Mathf.Max(6, ring * 4);
                            
                            for (int i = 0; i < pointsInRing; i++)
                            {
                                float angle = (float)i / pointsInRing * 2f * Mathf.PI;
                                Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
                                Vector2 position = layer.center + offset - size * 0.5f;
                                
                                if (IsValidPosition(position, size))
                                    positions.Add(position);
                            }
                        }
                    }
                }
                
                return positions;
            }
            
            private List<Vector2> GenerateRadiatingPositions(LayerType[] attractTo, Vector2 size)
            {
                List<Vector2> positions = new List<Vector2>();
                
                foreach (LayerInfo layer in layers)
                {
                    if (Array.IndexOf(attractTo, layer.type) >= 0)
                    {
                        // Generate radial positions from layer center
                        int rayCount = UnityEngine.Random.Range(6, 12);
                        for (int ray = 0; ray < rayCount; ray++)
                        {
                            float angle = (float)ray / rayCount * 2f * Mathf.PI;
                            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                            
                            // Multiple distances along each ray
                            for (int dist = 1; dist <= 3; dist++)
                            {
                                float distance = (layer.bounds.width + layer.bounds.height) * 0.25f + dist * settings.layerSpacing * canvasSize;
                                Vector2 position = layer.center + direction * distance - size * 0.5f;
                                
                                if (IsValidPosition(position, size))
                                    positions.Add(position);
                            }
                        }
                    }
                }
                
                return positions;
            }
            
            private List<Vector2> GenerateOrbitingPositions(LayerType[] attractTo, Vector2 size)
            {
                List<Vector2> positions = new List<Vector2>();
                
                foreach (LayerInfo layer in layers)
                {
                    if (Array.IndexOf(attractTo, layer.type) >= 0)
                    {
                        // Generate orbital positions
                        float orbitRadius = Mathf.Max(layer.bounds.width, layer.bounds.height) * 0.5f + settings.layerSpacing * canvasSize;
                        int orbitPoints = UnityEngine.Random.Range(3, 8);
                        
                        for (int i = 0; i < orbitPoints; i++)
                        {
                            float angle = (float)i / orbitPoints * 2f * Mathf.PI;
                            Vector2 position = layer.center + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * orbitRadius - size * 0.5f;
                            
                            if (IsValidPosition(position, size))
                                positions.Add(position);
                        }
                    }
                }
                
                return positions;
            }
            
            private List<Vector2> GenerateComplementaryPositions(Vector2 size)
            {
                List<Vector2> positions = new List<Vector2>();
                
                // Use golden ratio and rule of thirds
                for (int i = 0; i < 9; i++)
                {
                    Vector2 goldenPoint = GoldenRatio.GetRuleOfThirdsPoint(canvasSize, i) - size * 0.5f;
                    if (IsValidPosition(goldenPoint, size))
                        positions.Add(goldenPoint);
                }
                
                // Add golden ratio points
                for (int quad = 0; quad < 4; quad++)
                {
                    Vector2 goldenPoint = GoldenRatio.GetGoldenPoint(canvasSize, quad) - size * 0.5f;
                    if (IsValidPosition(goldenPoint, size))
                        positions.Add(goldenPoint);
                }
                
                return positions;
            }
            
            private List<Vector2> GenerateFollowingPositions(LayerType[] attractTo, Vector2 size)
            {
                List<Vector2> positions = new List<Vector2>();
                
                foreach (LayerInfo layer in layers)
                {
                    if (Array.IndexOf(attractTo, layer.type) >= 0)
                    {
                        // Generate positions that follow the contour of the layer
                        Rect bounds = layer.bounds;
                        
                        // Top and bottom edges
                        for (float x = bounds.xMin; x <= bounds.xMax; x += size.x * 0.5f)
                        {
                            positions.Add(new Vector2(x, bounds.yMax + size.y * 0.1f));
                            positions.Add(new Vector2(x, bounds.yMin - size.y * 1.1f));
                        }
                        
                        // Left and right edges
                        for (float y = bounds.yMin; y <= bounds.yMax; y += size.y * 0.5f)
                        {
                            positions.Add(new Vector2(bounds.xMax + size.x * 0.1f, y));
                            positions.Add(new Vector2(bounds.xMin - size.x * 1.1f, y));
                        }
                    }
                }
                
                return positions;
            }
            
            private List<Vector2> GenerateRandomPositions(Vector2 size, int count)
            {
                List<Vector2> positions = new List<Vector2>();
                
                for (int i = 0; i < count; i++)
                {
                    Vector2 position = new Vector2(
                        UnityEngine.Random.Range(0, canvasSize - size.x),
                        UnityEngine.Random.Range(0, canvasSize - size.y)
                    );
                    positions.Add(position);
                }
                
                return positions;
            }
            
            private float EvaluatePosition(Vector2 position, Vector2 size, LayerType[] attractTo, LayerType[] avoid)
            {
                float score = 0f;
                Rect testRect = new Rect(position.x, position.y, size.x, size.y);
                
                foreach (LayerInfo layer in layers)
                {
                    float distance = Vector2.Distance(position + size * 0.5f, layer.center);
                    
                    // Attraction score
                    if (Array.IndexOf(attractTo, layer.type) >= 0)
                    {
                        // Closer to attracted layers is better, but not too close
                        float minDistance = Mathf.Max(layer.bounds.width, layer.bounds.height) * 0.6f;
                        if (distance < minDistance)
                            score -= 100f; // Too close penalty
                        else
                            score += 50f / (1f + distance * 0.1f); // Closer is better
                    }
                    
                    // Avoidance score
                    if (Array.IndexOf(avoid, layer.type) >= 0)
                    {
                        if (testRect.Overlaps(layer.bounds))
                            score -= 200f; // Overlap penalty
                        else
                            score += distance * 2f; // Further is better
                    }
                    
                    // General overlap penalty
                    if (testRect.Overlaps(layer.bounds))
                        score -= layer.influence * 50f;
                }
                
                // Golden ratio bonus
                Vector2 center = position + size * 0.5f;
                Vector2 goldenPoint = GoldenRatio.GetGoldenPoint(canvasSize);
                float goldenDistance = Vector2.Distance(center, goldenPoint);
                score += 20f / (1f + goldenDistance * 0.1f);
                
                // Edge avoidance
                float edgeDistance = Mathf.Min(
                    Mathf.Min(position.x, position.y),
                    Mathf.Min(canvasSize - (position.x + size.x), canvasSize - (position.y + size.y))
                );
                if (edgeDistance < 2f)
                    score -= 50f;
                
                return score;
            }
            
            private bool IsValidPosition(Vector2 position, Vector2 size)
            {
                return position.x >= 0 && position.y >= 0 && 
                       position.x + size.x <= canvasSize && position.y + size.y <= canvasSize;
            }
            
            public LayerInfo AddElement(LayerType type, string elementName, Texture2D texture, Vector2 position, Color dominantColor)
            {
                // Create pixel mask
                bool[,] mask = new bool[canvasSize, canvasSize];
                Color[] pixels = texture.GetPixels();
                
                for (int y = 0; y < texture.height; y++)
                {
                    for (int x = 0; x < texture.width; x++)
                    {
                        int canvasX = (int)position.x + x;
                        int canvasY = (int)position.y + y;
                        
                        if (canvasX >= 0 && canvasX < canvasSize && canvasY >= 0 && canvasY < canvasSize)
                        {
                            Color pixel = pixels[y * texture.width + x];
                            if (pixel.a > 0.1f)
                                mask[canvasX, canvasY] = true;
                        }
                    }
                }
                
                // Create layer info
                Rect bounds = new Rect(position.x, position.y, texture.width, texture.height);
                Vector2 center = new Vector2(position.x + texture.width * 0.5f, position.y + texture.height * 0.5f);
                LayerInfo layer = new LayerInfo(bounds, center, type, elementName, dominantColor, mask, layers.Count);
                
                layers.Add(layer);
                
                // Composite to canvas
                CompositeTexture(canvas, texture, (int)position.x, (int)position.y);
                
                return layer;
            }
            
            public void AddProceduralElement(LayerType type, string elementName, Vector2 position, Vector2 size, Color color, System.Action<Texture2D, Color> drawAction)
            {
                // Create temporary texture for procedural element
                Texture2D tempTexture = new Texture2D((int)size.x, (int)size.y, TextureFormat.RGBA32, false);
                Color[] pixels = new Color[(int)(size.x * size.y)];
                for (int i = 0; i < pixels.Length; i++)
                    pixels[i] = Color.clear;
                tempTexture.SetPixels(pixels);
                
                // Draw the procedural element
                drawAction(tempTexture, color);
                tempTexture.Apply();
                
                // Add as layer
                AddElement(type, elementName, tempTexture, position, color);
                
                UnityEngine.Object.DestroyImmediate(tempTexture);
            }
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
            public const float INV_PHI = 0.618033988749f;

            public static Vector2 GetGoldenPoint(int canvasSize, int quadrant = -1)
            {
                if (quadrant == -1) quadrant = UnityEngine.Random.Range(0, 4);

                float goldenX = canvasSize * INV_PHI;
                float goldenY = canvasSize * INV_PHI;

                switch (quadrant)
                {
                    case 0: return new Vector2(goldenX, goldenY);
                    case 1: return new Vector2(canvasSize - goldenX, goldenY);
                    case 2: return new Vector2(canvasSize - goldenX, canvasSize - goldenY);
                    case 3: return new Vector2(goldenX, canvasSize - goldenY);
                }
                return new Vector2(canvasSize * 0.5f, canvasSize * 0.5f);
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

            public static Vector2 GetSpiralPoint(int canvasSize, float t, float radius = -1)
            {
                if (radius < 0) radius = canvasSize * 0.3f;
                float angle = t * 2 * Mathf.PI;
                float r = radius * Mathf.Pow(PHI, angle / (2 * Mathf.PI));
                Vector2 center = new Vector2(canvasSize * 0.5f, canvasSize * 0.5f);
                return center + new Vector2(Mathf.Cos(angle) * r, Mathf.Sin(angle) * r);
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
            CompositionPlanner planner = new CompositionPlanner(settings.canvasSize, selectedPalette);

            PlacePrimaryShape(planner);

            // Step 2: Add elements based on rules and chances
            int maxElements = UnityEngine.Random.Range(2, settings.maxLayers);
            
            for (int i = 1; i < maxElements; i++)
            {
                float roll = UnityEngine.Random.value;
                
                if (roll < settings.detailChance && ShouldAddDetail(planner))
                {
                    PlaceDetail(planner);
                }
                else if (roll < settings.motifChance && ShouldAddMotif(planner))
                {
                    if (UnityEngine.Random.value < settings.proceduralMotifChance)
                        PlaceProceduralMotif(planner);
                    else
                        PlaceMotif(planner);
                }
            }

            // Save the result
            planner.canvas.Apply();
            byte[] pngData = planner.canvas.EncodeToPNG();
            string filename = $"{selectedPalette.godType.ToLower()}_power_{index}.png";
            File.WriteAllBytes(Path.Combine(outputPath, filename), pngData);

            UnityEngine.Object.DestroyImmediate(planner.canvas);
        }

        private static void PlacePrimaryShape(CompositionPlanner planner)
        {
            if (shapeTextures.Length == 0) return;

            Texture2D shapeTexture = shapeTextures[UnityEngine.Random.Range(0, shapeTextures.Length)];
            Texture2D coloredShape = ApplyPalette(shapeTexture, planner.palette.colors);

            // Primary shape uses golden ratio positioning
            Vector2 position = GoldenRatio.GetGoldenPoint(planner.canvasSize) - new Vector2(coloredShape.width, coloredShape.height) * 0.5f;
            position.x = Mathf.Clamp(position.x, 0, planner.canvasSize - coloredShape.width);
            position.y = Mathf.Clamp(position.y, 0, planner.canvasSize - coloredShape.height);

            Color dominantColor = planner.palette.colors[UnityEngine.Random.Range(0, planner.palette.colors.Length)];
            planner.AddElement(LayerType.Shape, "primary_shape", coloredShape, position, dominantColor);

            UnityEngine.Object.DestroyImmediate(coloredShape);
        }

        private static bool ShouldAddDetail(CompositionPlanner planner)
        {
            return detailTextures.Length > 0 && planner.layers.Count < settings.maxLayers;
        }

        private static bool ShouldAddMotif(CompositionPlanner planner)
        {
            return (motifTextures.Length > 0 || settings.proceduralMotifChance > 0) && planner.layers.Count < settings.maxLayers;
        }

        private static void PlaceDetail(CompositionPlanner planner)
        {
            if (detailTextures.Length == 0) return;

            Texture2D detail = detailTextures[UnityEngine.Random.Range(0, detailTextures.Length)];
            Texture2D coloredDetail = ApplyPalette(detail, planner.palette.colors);

            // Find rule for this detail
            ElementRule rule = FindElementRule(detail.name.ToLower());
            PlacementBehavior behavior = rule?.behavior ?? PlacementBehavior.Around;
            LayerType[] attractTo = rule?.attractToLayers ?? new LayerType[] { LayerType.Shape };
            LayerType[] avoid = rule?.avoidLayers ?? new LayerType[] { };

            Vector2 size = new Vector2(coloredDetail.width, coloredDetail.height);
            Vector2 position = planner.FindOptimalPlacement(LayerType.Detail, size, behavior, attractTo, avoid);

            Color dominantColor = planner.palette.colors[UnityEngine.Random.Range(0, planner.palette.colors.Length)];
            planner.AddElement(LayerType.Detail, detail.name, coloredDetail, position, dominantColor);

            UnityEngine.Object.DestroyImmediate(coloredDetail);
        }

        private static void PlaceMotif(CompositionPlanner planner)
        {
            if (motifTextures.Length == 0) return;

            Texture2D motif = motifTextures[UnityEngine.Random.Range(0, motifTextures.Length)];
            Texture2D coloredMotif = ApplyPalette(motif, planner.palette.colors);

            // Apply random rotation
            int rotations = UnityEngine.Random.Range(0, 4);
            for (int i = 0; i < rotations; i++)
            {
                Texture2D rotated = RotateTexture90(coloredMotif);
                UnityEngine.Object.DestroyImmediate(coloredMotif);
                coloredMotif = rotated;
            }

            // Apply scaling
            float scale = UnityEngine.Random.Range(0.4f, 0.8f);
            int newWidth = Mathf.RoundToInt(coloredMotif.width * scale);
            int newHeight = Mathf.RoundToInt(coloredMotif.height * scale);
            Texture2D scaledMotif = ResizeTexture(coloredMotif, newWidth, newHeight);
            UnityEngine.Object.DestroyImmediate(coloredMotif);
            coloredMotif = scaledMotif;

            Vector2 size = new Vector2(coloredMotif.width, coloredMotif.height);
            Vector2 position = planner.FindOptimalPlacement(
                LayerType.Motif, 
                size, 
                PlacementBehavior.Complementary, 
                new LayerType[] { LayerType.Shape }, 
                new LayerType[] { }
            );

            Color dominantColor = planner.palette.colors[UnityEngine.Random.Range(0, planner.palette.colors.Length)];
            planner.AddElement(LayerType.Motif, motif.name, coloredMotif, position, dominantColor);

            UnityEngine.Object.DestroyImmediate(coloredMotif);
        }
        private static void PlaceProceduralMotif(CompositionPlanner planner)
        {
            Vector2 size = new Vector2(
                UnityEngine.Random.Range(8, 16), 
                UnityEngine.Random.Range(8, 16)
            );
            
            Vector2 position = planner.FindOptimalPlacement(
                LayerType.ProceduralMotif, 
                size, 
                PlacementBehavior.Radiating, 
                new LayerType[] { LayerType.Shape }, 
                new LayerType[] { }
            );

            Color motifColor = planner.palette.colors[UnityEngine.Random.Range(0, planner.palette.colors.Length)];
            
            // Choose procedural pattern based on god type
            System.Action<Texture2D, Color> drawAction = GetProceduralDrawAction(planner.palette.godType);
            
            planner.AddProceduralElement(LayerType.ProceduralMotif, "procedural_motif", position, size, motifColor, drawAction);
        }

        private static System.Action<Texture2D, Color> GetProceduralDrawAction(string godType)
        {
            switch (godType.ToLower())
            {
                case "light":
                    return (texture, color) => DrawRadialBurstOnTexture(texture, color);
                case "night":
                    return (texture, color) => DrawStarTrailOnTexture(texture, color);
                case "moon":
                    return (texture, color) => DrawCrescentTrailOnTexture(texture, color);
                case "earth":
                    return (texture, color) => DrawRootNetworkOnTexture(texture, color);
                case "love":
                    return (texture, color) => DrawHeartPulseOnTexture(texture, color);
                case "lich":
                    return (texture, color) => DrawNecroticWebOnTexture(texture, color);
                default:
                    return (texture, color) => DrawSpiralEnergyOnTexture(texture, color);
            }
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
        

        private static ElementRule FindElementRule(string elementName)
        {
            foreach (ElementRule rule in elementRules)
            {
                if (rule.elementName.Equals(elementName, System.StringComparison.OrdinalIgnoreCase))
                    return rule;
            }
            return null;
        }
        
        private static void DrawRadialBurstOnTexture(Texture2D texture, Color color)
        {
            int centerX = texture.width / 2;
            int centerY = texture.height / 2;
            int rays = UnityEngine.Random.Range(6, 12);
            
            for (int i = 0; i < rays; i++)
            {
                float angle = (float)i / rays * 2f * Mathf.PI;
                int length = UnityEngine.Random.Range(3, Mathf.Min(texture.width, texture.height) / 2);
                DrawLineOnTexture(texture, centerX, centerY, 
                        centerX + (int)(Mathf.Cos(angle) * length),
                        centerY + (int)(Mathf.Sin(angle) * length), color);
            }
            texture.Apply();
        }

        private static void DrawStarTrailOnTexture(Texture2D texture, Color color)
        {
            int points = UnityEngine.Random.Range(4, 8);
            for (int i = 0; i < points; i++)
            {
                int x = UnityEngine.Random.Range(2, texture.width - 2);
                int y = UnityEngine.Random.Range(2, texture.height - 2);
                
                // Create small star shape
                texture.SetPixel(x, y, color);
                if (x > 0) texture.SetPixel(x-1, y, Color.Lerp(color, Color.clear, 0.5f));
                if (x < texture.width-1) texture.SetPixel(x+1, y, Color.Lerp(color, Color.clear, 0.5f));
                if (y > 0) texture.SetPixel(x, y-1, Color.Lerp(color, Color.clear, 0.5f));
                if (y < texture.height-1) texture.SetPixel(x, y+1, Color.Lerp(color, Color.clear, 0.5f));
            }
            texture.Apply();
        }
        private static void DrawCrescentTrailOnTexture(Texture2D texture, Color color)
        {
            int centerX = texture.width / 2;
            int centerY = texture.height / 2;
            int radius = Mathf.Min(texture.width, texture.height) / 3;
            
            for (float angle = -Mathf.PI/3; angle < Mathf.PI/3; angle += 0.3f)
            {
                int x = centerX + (int)(Mathf.Cos(angle) * radius);
                int y = centerY + (int)(Mathf.Sin(angle) * radius);
                if (x >= 0 && x < texture.width && y >= 0 && y < texture.height)
                    texture.SetPixel(x, y, color);
            }
            texture.Apply();
        }

        private static void DrawRootNetworkOnTexture(Texture2D texture, Color color)
        {
            int startX = texture.width / 2;
            int startY = texture.height - 1;
            DrawBranchOnTexture(texture, startX, startY, -Mathf.PI/2, texture.height/3, color, 0);
            texture.Apply();
        }

        private static void DrawNecroticWebOnTexture(Texture2D texture, Color color)
        {
            int nodes = UnityEngine.Random.Range(3, 6);
            Vector2[] nodePositions = new Vector2[nodes];
            
            for (int i = 0; i < nodes; i++)
            {
                nodePositions[i] = new Vector2(
                    UnityEngine.Random.Range(2, texture.width - 2),
                    UnityEngine.Random.Range(2, texture.height - 2)
                );
            }
            
            for (int i = 0; i < nodes; i++)
            {
                for (int j = i + 1; j < nodes; j++)
                {
                    float distance = Vector2.Distance(nodePositions[i], nodePositions[j]);
                    if (distance < texture.width * 0.7f && UnityEngine.Random.value < 0.6f)
                    {
                        DrawLineOnTexture(texture, 
                            (int)nodePositions[i].x, (int)nodePositions[i].y,
                            (int)nodePositions[j].x, (int)nodePositions[j].y, 
                            Color.Lerp(color, Color.clear, distance / (texture.width * 0.7f)));
                    }
                }
            }
            texture.Apply();
        }

        private static void DrawHeartPulseOnTexture(Texture2D texture, Color color)
        {
            int centerX = texture.width / 2;
            int centerY = texture.height / 2;
            int rings = UnityEngine.Random.Range(2, 4);
            int maxRadius = Mathf.Min(texture.width, texture.height) / 4;
            
            for (int ring = 0; ring < rings; ring++)
            {
                int radius = (ring + 1) * maxRadius / rings;
                Color ringColor = Color.Lerp(color, Color.clear, (float)ring / rings);
                
                for (float angle = 0; angle < 2 * Mathf.PI; angle += 0.5f)
                {
                    int x = centerX + (int)(Mathf.Cos(angle) * radius);
                    int y = centerY + (int)(Mathf.Sin(angle) * radius);
                    if (x >= 0 && x < texture.width && y >= 0 && y < texture.height)
                        texture.SetPixel(x, y, ringColor);
                }
            }
            texture.Apply();
        }

        private static void DrawSpiralEnergyOnTexture(Texture2D texture, Color color)
        {
            int centerX = texture.width / 2;
            int centerY = texture.height / 2;
            float maxRadius = Mathf.Min(texture.width, texture.height) / 3;
            
            for (float t = 0; t < 3 * Mathf.PI; t += 0.3f)
            {
                float radius = (t / (3 * Mathf.PI)) * maxRadius;
                int x = centerX + (int)(Mathf.Cos(t) * radius);
                int y = centerY + (int)(Mathf.Sin(t) * radius);
                
                if (x >= 0 && x < texture.width && y >= 0 && y < texture.height)
                {
                    Color fadeColor = Color.Lerp(color, Color.clear, radius / maxRadius);
                    texture.SetPixel(x, y, fadeColor);
                }
            }
            texture.Apply();
        }

        private static void DrawBranchOnTexture(Texture2D texture, int x, int y, float angle, int length, Color color, int depth)
        {
            if (depth > 2 || length < 2) return;
            
            int endX = x + (int)(Mathf.Cos(angle) * length);
            int endY = y + (int)(Mathf.Sin(angle) * length);
            
            DrawLineOnTexture(texture, x, y, endX, endY, Color.Lerp(color, Color.clear, depth * 0.3f));
            
            if (UnityEngine.Random.value < 0.6f)
            {
                float leftAngle = angle - UnityEngine.Random.Range(0.3f, 0.8f);
                float rightAngle = angle + UnityEngine.Random.Range(0.3f, 0.8f);
                int newLength = length - UnityEngine.Random.Range(1, 2);
                
                DrawBranchOnTexture(texture, endX, endY, leftAngle, newLength, color, depth + 1);
                DrawBranchOnTexture(texture, endX, endY, rightAngle, newLength, color, depth + 1);
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


        private static void DrawLineOnTexture(Texture2D texture, int x0, int y0, int x1, int y1, Color color)
        {
            int dx = Mathf.Abs(x1 - x0);
            int dy = Mathf.Abs(y1 - y0);
            int sx = x0 < x1 ? 1 : -1;
            int sy = y0 < y1 ? 1 : -1;
            int err = dx - dy;

            while (true)
            {
                if (x0 >= 0 && x0 < texture.width && y0 >= 0 && y0 < texture.height)
                    texture.SetPixel(x0, y0, color);

                if (x0 == x1 && y0 == y1) break;
                
                int e2 = 2 * err;
                if (e2 > -dy) { err -= dy; x0 += sx; }
                if (e2 < dx) { err += dx; y0 += sy; }
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
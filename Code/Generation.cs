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

            // Add motif
            if (motifTextures.Length > 0 && UnityEngine.Random.value < settings.motifChance)
            {
                AddMotif(canvas, palette);
            }

            // Add detail
            if (detailTextures.Length > 0 && UnityEngine.Random.value < settings.detailChance)
            {
                AddDetail(canvas, palette);
            }

            // Save the generated texture
            canvas.Apply();
            byte[] pngData = canvas.EncodeToPNG();
            string filename = $"{selectedPalette.godType.ToLower()}_power_{index}.png";
            File.WriteAllBytes(Path.Combine(outputPath, filename), pngData);

            // Cleanup
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

            // Rotation (90-degree increments)
            int rotations = UnityEngine.Random.Range(0, 4);
            for (int i = 0; i < rotations; i++)
            {
                Texture2D rotated = RotateTexture90(motif);
                if (motif != motifTextures[0]) // Don't destroy original
                    UnityEngine.Object.DestroyImmediate(motif);
                motif = rotated;
            }

            // Rescale
            float maxScaleX = (float)settings.canvasSize / motif.width;
            float maxScaleY = (float)settings.canvasSize / motif.height;
            float maxScale = Mathf.Min(1.2f, maxScaleX, maxScaleY);
            float scale = UnityEngine.Random.Range(0.6f, maxScale);
            
            int newWidth = Mathf.RoundToInt(motif.width * scale);
            int newHeight = Mathf.RoundToInt(motif.height * scale);
            Texture2D resized = ResizeTexture(motif, newWidth, newHeight);
            UnityEngine.Object.DestroyImmediate(motif);
            motif = resized;

            // Placement modes
            string[] modes = { "centered", "orbit", "trail" };
            string mode = modes[UnityEngine.Random.Range(0, modes.Length)];
            
            int centerX = settings.canvasSize / 2;
            int centerY = settings.canvasSize / 2;

            switch (mode)
            {
                case "centered":
                    int x = centerX - motif.width / 2 + UnityEngine.Random.Range(-2, 3);
                    int y = centerY - motif.height / 2 + UnityEngine.Random.Range(-2, 3);
                    CompositeTexture(canvas, motif, x, y);
                    break;

                case "orbit":
                    int radius = 6 + UnityEngine.Random.Range(0, 5);
                    int orbitCount = UnityEngine.Random.Range(2, 5);
                    for (int i = 0; i < orbitCount; i++)
                    {
                        float theta = UnityEngine.Random.Range(0f, 2f * Mathf.PI);
                        int orbitX = Mathf.RoundToInt(centerX + radius * Mathf.Cos(theta)) - motif.width / 2;
                        int orbitY = Mathf.RoundToInt(centerY + radius * Mathf.Sin(theta)) - motif.height / 2;
                        CompositeTexture(canvas, motif, orbitX, orbitY);
                    }
                    break;

                case "trail":
                    int steps = UnityEngine.Random.Range(2, 5);
                    int startX = UnityEngine.Random.Range(0, settings.canvasSize / 4 + 1);
                    int startY = UnityEngine.Random.Range(0, settings.canvasSize - motif.height + 1);
                    
                    for (int step = 0; step < steps; step++)
                    {
                        float t = (float)step / steps;
                        int trailX = Mathf.RoundToInt((1 - t) * startX + t * centerX) - motif.width / 2;
                        int trailY = Mathf.RoundToInt((1 - t) * startY + t * centerY) - motif.height / 2;
                        CompositeTexture(canvas, motif, trailX, trailY);
                    }
                    break;
            }

            UnityEngine.Object.DestroyImmediate(motif);
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

            int x = UnityEngine.Random.Range(0, settings.canvasSize - detail.width + 1);
            int y = UnityEngine.Random.Range(0, settings.canvasSize - detail.height + 1);
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
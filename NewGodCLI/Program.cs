using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis.Options;

namespace NewGodCLI
{
    class NewGodGenerator
    {
        public static Dictionary<string, float> Stats = new Dictionary<string, float>
        {
            { "S.damage", 0 },
            { "S.health", 0 },
            { "S.attack_speed", 0 },
            { "S.critical_chance", 0 },
            { "S.range", 0 },
            { "S.scale", 0 },
            {"S.speed", 0f},
            {"S.dodge", 0f},
            {"S.accuracy", 0f},
            { "S.max_children", 0 },
            { "S.fertility", 0 },
        };
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the God Creator CLI:\n");
            Console.WriteLine("\nWhat is your Gods Name: ");
            string godName = Console.ReadLine();
            

            Console.WriteLine("\nWhat Are Some Of Your Gods Abilities (type '!' to continue): ");
            Dictionary<string, float> abilities = new Dictionary<string, float>();
            string ability = Console.ReadLine();
            while (!ability.Equals("!"))
            {
                abilities.Add(ability, 0);
                Console.WriteLine($"\n{ability} Chance: ");
                if (float.TryParse(Console.ReadLine(), out float chance))
                {
                    abilities[ability] = chance;  // Store the float value in the dictionary
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid float number.");
                }
                Console.WriteLine("\nAnd: ");
                ability = Console.ReadLine();
            }

            Console.WriteLine("\nAnd Lastly What your Gods Stats: ");
            foreach (string key in Stats.Keys.ToList())
            {
                Console.WriteLine($"{key}: ");
                if (float.TryParse(Console.ReadLine(), out float stat))
                {
                    Stats[key] = stat;  // Update the dictionary safely
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid float number.");
                }
            }

            
            ReplacePlaceHolder("C:/Users/Admin/Desktop/TestTraits.cs", "//NEW_GOD_TRAIT_STATS", CodeGenStatsDict(godName, Stats));
            ReplacePlaceHolder("C:/Users/Admin/Desktop/TestTraits.cs", "//NEW_GOD_ABILITY_DICT", CodeGenGodAbilitiesDict(godName, abilities));
            ReplacePlaceHolder("C:/Users/Admin/Desktop/TestTraits.cs", "//NEW_GOD_ATTACK_FUNC", CodeGenGodAbilitiesFuncs(godName, abilities));
            ReplacePlaceHolder("C:/Users/Admin/Desktop/TestSavedSettings.cs", "//NEW_SAVED_SETTING", CodeGenSavedSettings(godName, abilities, 40));

        }

        private static string CodeGenStatsDict(string godName, Dictionary<string, float> stats)
        {
            string final = "{" + InQoutes(godName) + ", new Dictionary<string, float>(){\n";
            foreach (string key in stats.Keys)
            {
                if (stats[key] != 0) final += "\t{" + $"{key}, {stats[key]}" + "},\n";
                //else continue
            }
            final += "\n\t} \n},\n//NEW_GOD_TRAIT_STATS";
            return final;
        }

        private static string CodeGenGodAbilitiesDict(string godName, Dictionary<string, float> abilities)
        {
            string final = "{" + InQoutes(godName) +", new List<AttackAction>(){\n";
            foreach (string abilityName in abilities.Keys)
            {
                final += $"\tnew AttackAction({abilityName}),\n";
            }

            final += "\n\t}\n},\n//NEW_GOD_ABILITY_DICT";
            return final;
        }

        private static string CodeGenGodAbilitiesFuncs(string godName, Dictionary<string, float> abilities)
        {
            string final = $"#region {StripWhiteSpace(godName.Trim())}sAttack\n";
            foreach (string ability in abilities.Keys)
            {
                final +=
                    $"\npublic static bool {ability}(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)\n" +
                    "\t{" + $"if({ToolBoxChanceCode(godName, ability)})" + "\n\t\t{\n\t\t}\t\n}";
            }

            final += AttackInit(godName) + "\n#endregion\n//NEW_GOD_ATTACK_FUNC";
            return final;
        }

        private static string CodeGenSavedSettings(string godName, Dictionary<string, float> abilities, float inherit)
        {
            string final = "{" + InQoutes(StripWhiteSpace(godName.Trim() + "Window")) + ", \n\tnew Dictionary<string, InputOption>\n\t{";
            foreach (string key in abilities.Keys)
            {
                final += "\n\t{" + InQoutes(key + "%") + ", new InputOption{active = true, value = " + abilities[key] +
                         "}},";
            }

            final += "{" + InQoutes(godName + "inherit%") + ", new InputOption{active = true, value = " + inherit +
                     "}}";

            final += "\n\t}\n},\n//NEW_SAVED_SETTING";
            return final;
        }

        private static string CodeGenWindow(string godName)
        {
            return $"newWindow({InQoutes(godName.Trim() + "Window")}, {godName} Chance Modifier);\n//NEW_WINDOW_HERE";
        }


        private static string CodeGenInit(string godName, string description)
        {
            string final = $"ActorTait {RemoveFiller(godName)} = new ActorTrait();\n";
            final += $"{RemoveFiller(godName)}.id = {godName};\n";
            final += $"{RemoveFiller(godName)}.action_attack_target = new AttackAction({StripWhiteSpace(godName.Trim())}Attack);\n";
            final += $"{RemoveFiller(godName)}.group_id = {InQoutes("GodTraits")};\n";
            final += $"AddTrait({RemoveFiller(godName)}, {description})\n//NEW_GOD_INIT";
            return final;

        }
        


        private static string InQoutes(string str)
        {
            return '"' + str + '"';
        }

        private static string AttackInit(string godname)
        {
            return
                $"public static bool {StripWhiteSpace(godname.Trim())}Attack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile) => GodAttack(pSelf, pTarget, pTile, {InQoutes(godname.Trim())});";
        }

        private static string ToolBoxChanceCode(string godName, string abilityName)
        {
            return $"Toolbox.randomChance(GetEnhancedChance({InQoutes(godName)}, {InQoutes(abilityName + "%")}))";
        }

        private static void ReplacePlaceHolder(string path, string placeholder, string newCode)
        {
            string fileContent = File.ReadAllText(path);

            if (fileContent.Contains(placeholder))
            {
                fileContent = fileContent.Replace(placeholder, newCode);

                File.WriteAllText(path, fileContent);
                FormatCode(path);

                Console.WriteLine("Placeholder replaced successfully!");
            }
            else
            {
                Console.WriteLine("Placeholder not found in the file.");
            }
        }

        private static string RemoveFiller(string str)
        {
            if (str.Contains("the")) str.Replace("the", "");
            if (str.Contains("of")) str.Replace("of", "");
            str = StripWhiteSpace(str.Trim());
            return str;
        }

        private static void FormatCode(string path, CancellationToken cancelToken = default)
        {

            string code = File.ReadAllText(path);
            string source = CSharpSyntaxTree.ParseText(code)
                .GetRoot(cancelToken)
                .NormalizeWhitespace()
                .SyntaxTree
                .GetText(cancelToken)
                .ToString();
            File.WriteAllText(path, source);
        }

        private static string StripWhiteSpace(string str)
        {
            return str.Replace(" ", "");
        }
    }

    
}

using AdvantOfCode._2023.Day19;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdvantOfCode._2024.Day5
{
    public class Solutions
    {
        class Condition
        {
            public int Left { get; set; }
            public int Right { get; set; }
        }

        public void First()
        {
            var txt = File.ReadAllLines(@"C:\Users\akash.buch\source\repos\AdvantOfCode2023\AdvantOfCode\2024\Day5\input.txt");
            var output = 0;

            List<Condition> conditions = new List<Condition>();
            List<List<int>> inputs = new();

            foreach (var line in txt)
            {
                if (line.Contains("|"))
                {
                    var left = line.Split("|").First();
                    var right = line.Split("|").Last();

                    conditions.Add(new() { Left = Convert.ToInt32(left), Right = Convert.ToInt32(right) });
                }
                else if (line.Contains(","))
                {
                    var input = line.Split(",").Select(x=>Convert.ToInt32(x)).ToList();
                    inputs.Add(input);
                }
            }

            foreach (var input in inputs)
            {
                var hasError = false;
                for (int i = 0; i < input.Count - 1; i++)
                {
                    if (!conditions.Any(x => x.Left == input[i] && x.Right == input[i + 1]))
                    {
                        hasError = true;
                        break;
                    }
                    
                }

                if (!hasError)
                {
                    output += input[input.Count / 2];
                }
            }

            Console.WriteLine(output);
        }

        public void Secound()
        {
            var txt = File.ReadAllLines(@"C:\Users\akash.buch\source\repos\AdvantOfCode2023\AdvantOfCode\2024\Day5\input.txt");
            var output = 0;

            List<Condition> conditions = new List<Condition>();
            List<List<int>> inputs = new();

            foreach (var line in txt)
            {
                if (line.Contains("|"))
                {
                    var left = line.Split("|").First();
                    var right = line.Split("|").Last();

                    conditions.Add(new() { Left = Convert.ToInt32(left), Right = Convert.ToInt32(right) });
                }
                else if (line.Contains(","))
                {
                    var input = line.Split(",").Select(x => Convert.ToInt32(x)).ToList();
                    inputs.Add(input);
                }
            }

            foreach (var input in inputs)
            {
                var masterError = false;
                var hasError = false;
                do
                {
                    hasError = false;
                    for (int i = 0; i < input.Count - 1; i++)
                    {
                        if (!conditions.Any(x => x.Left == input[i] && x.Right == input[i + 1]))
                        {
                            var left = input[i];
                            var right = input[i + 1];

                            var temp = input[i];
                            input[i] = input[i + 1];
                            input[i + 1] = temp;
                            hasError = true;
                            masterError = true;
                        }
                    }
                } while (hasError);

                if (masterError)
                {
                    output += input[input.Count / 2];
                }
            }

            Console.WriteLine(output);
        }
    }
}

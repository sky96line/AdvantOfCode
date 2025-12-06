using AdvantOfCode._2023.Day19;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;

namespace AdvantOfCode._2024.Day18
{
    public class Solutions
    {
       
        public void First()
        {
            var inputs = File.ReadAllLines(@"C:\Users\akash.buch\source\repos\AdvantOfCode2023\AdvantOfCode\2024\Day18\input.txt");

            List<(int i, int j)> memory = new();

            foreach (var input in inputs)
            {
                var i = Convert.ToInt32(input.Split(',').First());
                var j = Convert.ToInt32(input.Split(',').Last());

                memory.Add((i, j));
            }


            memory = memory.Take(1024).ToList();

            for (int i = 0; i <= 70; i++)
            {
                for (int j = 0; j <= 70; j++)
                {
                    if (memory.Contains((j, i)))
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}

using AdvantOfCode._2023.Day19;
using AdvantOfCode._2024.Day8;
using System.Drawing.Printing;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AdvantOfCode._2024.Day11
{
    public class Solutions
    {
        public Dictionary<long, List<long>> memo = new();

        private void Print(int i, List<long> change)
        {
            Console.Write($"{i} | {change.Count} = ");
            foreach (var c in change)
            {
                Console.Write($"{c} ");
            }
            Console.WriteLine();
        }

        public void First()
        {
            var txt_inputs = File.ReadAllText(@"C:\Users\akash.buch\source\repos\AdvantOfCode2023\AdvantOfCode\2024\Day11\input.txt");
            var output = 0;


            List<long> stones = txt_inputs.Split(' ').Select(x => Convert.ToInt64(x)).ToList();
            
            Dictionary<(long, int), List<long>> memo = new();

            for (int i = 0; i < 75; i++)
            {
                List<long> new_stones = new();

                foreach (var stone in stones)
                {
                    if (memo.ContainsKey((stone, i)))
                    {
                        new_stones.AddRange(memo[(stone, i)]);
                    }
                    else
                    {
                        if (stone == 0)
                        {
                            new_stones.Add(1);
                            memo.Add((stone, i), new() { 1 });
                        }
                        else if (stone.ToString().Length % 2 == 0)
                        {
                            var s = SplitStone(stone);
                            new_stones.AddRange(s);
                            memo.Add((stone, i),new() { s[0], s[1] });
                        }
                        else
                        {
                            new_stones.Add(stone * 2024);
                            memo.Add((stone, i), new() { stone * 2024 });
                        }
                    }
                }

                Console.WriteLine($"{i+1}\t{new_stones.Count}");

                stones = new(new_stones);
            }
        }

        public List<long> SplitStone(long number)
        {
            string numStr = number.ToString();
            
            int mid = numStr.Length / 2;

            long left = long.Parse(numStr.Substring(0, mid));
            long right = long.Parse(numStr.Substring(mid));

            return new List<long> { left, right };
        }
    }
}

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
        static Dictionary<(List<long>, int), List<long>> memo = new Dictionary<(List<long>, int), List<long>>();

        private void Print(int i, List<long> change)
        {
            Console.Write($"{i} = ");
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

            Dictionary<long, List<long>> cache= new();

            List<long> stones = txt_inputs.Split(' ').Select(x => Convert.ToInt64(x)).ToList();

            Dictionary<long, int> count= new();


            // Initial setup
            //List<long> stones = new List<long> { 125, 17 }; // Initial stones
            int blinks = 75; // Number of blinks to simulate

            Console.WriteLine("Initial stones: " + string.Join(" ", stones));

            stones = TransformStonesRecursive(stones, blinks);
            Console.WriteLine("Final stones after " + blinks + " blinks: " + stones.Count);

        }

        List<long> TransformStonesRecursive(List<long> stones, int blinks)
        {
            if (blinks == 0)
            {
                return stones;
            }

            if (memo.ContainsKey((stones, blinks)))
            {
                return memo[(stones, blinks)];
            }

            List<long> newStones = new List<long>();

            foreach (var stone in stones)
            {
                if (stone == 0)
                {
                    // Rule 1: Replace 0 with 1
                    newStones.Add(1);
                }
                else if (HasEvenDigits(stone))
                {
                    // Rule 2: Split stone into two
                    newStones.AddRange(SplitStone(stone));
                }
                else
                {
                    // Rule 3: Multiply by 2024
                    newStones.Add(stone * 2024);
                }
            }

            var result = TransformStonesRecursive(newStones, blinks - 1);
            memo[(stones, blinks)] = result;

            return result;
        }
        public bool HasEvenDigits(long number)
        {
            int digitCount = number.ToString().Length;
            return digitCount % 2 == 0;
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

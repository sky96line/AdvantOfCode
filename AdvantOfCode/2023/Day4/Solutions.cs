using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdvantOfCode._2023.Day4
{
    public class Solutions
    {
        Dictionary<int, int> keyValues = new();


        public void First()
        {
            var inputs = File.ReadAllLines(@"C:\Users\akash.buch\Source\Repos\AdvantOfCode2023\AdvantOfCode2023\Day4\input4.txt");
            var total = 0;

            foreach (var input in inputs)
            {
                var id = int.Parse(input.Split(":").First().Split(" ").Last().Trim());
                var numbers = input.Split(":").Last().Trim();

                var gameNumbers = numbers.Split("|");

                var winningNumbers = System.Text.RegularExpressions.Regex.Split(gameNumbers.First().Trim(), @"\s+");

                var pickedNumbers = System.Text.RegularExpressions.Regex.Split(gameNumbers.Last().Trim(), @"\s+");

                var common = pickedNumbers.Intersect(winningNumbers).ToList();

                if (common.Count() > 0)
                    total += int.Parse(Math.Pow(2, common.Count()-1).ToString());
            }

            Console.WriteLine(total);
        }


        private void Add(int id)
        {
            if (keyValues.ContainsKey(id))
            {
                keyValues[id] = keyValues[id] + 1;
            }
            else
            {
                keyValues.Add(id, 1);
            }
        }

        public void Secound()
        {
            var inputs = File.ReadAllLines(@"C:\Users\akash.buch\Source\Repos\AdvantOfCode2023\AdvantOfCode2023\Day4\input4.txt");

            foreach (var input in inputs)
            {
                var id = int.Parse(input.Split(":").First().Split(" ").Last().Trim());
                var numbers = input.Split(":").Last().Trim();

                var gameNumbers = numbers.Split("|");

                var winningNumbers = System.Text.RegularExpressions.Regex.Split(gameNumbers.First().Trim(), @"\s+");

                var pickedNumbers = System.Text.RegularExpressions.Regex.Split(gameNumbers.Last().Trim(), @"\s+");

                var common = pickedNumbers.Intersect(winningNumbers).ToList();

                Add(id);

                for (int i = 0; i < keyValues[id]; i++)
                {
                    for (int j = 1; j <= common.Count(); j++)
                    {
                        Add(id + j);
                    }
                }
            }

            Console.WriteLine(keyValues.Values.Sum());
        }
    }
}

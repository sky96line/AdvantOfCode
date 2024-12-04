using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvantOfCode._2024.Day1
{
    public class Solutions
    {
        public void First()
        {
            var inputs = File.ReadAllLines(@"C:\Users\akash.buch\source\repos\AdvantOfCode2023\AdvantOfCode2023\2024\Day1\input.txt");
            var output = 0;

            List<int> left = new List<int>();
            List<int> right = new List<int>();
            foreach (var input in inputs)
            {
                var l = input.Split(" ").First();
                var r = input.Split(" ").Last();

                left.Add(Convert.ToInt32(l));
                right.Add(Convert.ToInt32(r));
            }

            left.Sort();
            right.Sort();

            for (int i = 0; i < left.Count; i++)
            {
                var off = Math.Abs(left[i] - right[i]);
                output += off;
            }

            Console.WriteLine(output);
        }

        public void Secound()
        {
            var inputs = File.ReadAllLines(@"C:\Users\akash.buch\source\repos\AdvantOfCode2023\AdvantOfCode2023\2024\Day1\input.txt");
            var output = 0;

            List<int> left = new List<int>();
            List<int> right = new List<int>();
            foreach (var input in inputs)
            {
                var l = input.Split(" ").First();
                var r = input.Split(" ").Last();

                left.Add(Convert.ToInt32(l));
                right.Add(Convert.ToInt32(r));
            }

            for (int i = 0; i < left.Count; i++)
            {
                var count = right.Count(x => x == left[i]);
                var off = left[i] * count;

                output += off;
            }

            Console.WriteLine(output);
        }
    }
}

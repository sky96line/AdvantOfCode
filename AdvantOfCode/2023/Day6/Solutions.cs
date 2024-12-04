using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdvantOfCode._2023.Day6
{
    public class Solutions
    {
        public void First()
        {
            var inputs = File.ReadAllLines(@"C:\Users\akash.buch\Source\Repos\AdvantOfCode2023\AdvantOfCode2023\Day6\input6.txt");

            var times_arr = System.Text.RegularExpressions.Regex.Split(inputs.First().Split(":").Last().Trim(), @"\s+");
            var distance_arr = System.Text.RegularExpressions.Regex.Split(inputs.Last().Split(":").Last().Trim(), @"\s+");

            var times = times_arr.Select(x => int.Parse(x)).ToList();
            var distance = distance_arr.Select(x => int.Parse(x)).ToList();

            var wins = new int[times.Count()];
            for (int i = 0; i < times.Count(); i++)
            {
                var win_dist = distance[i];

                for (int j = 0; j < times[i]; j++)
                {
                    var cover_dist = j * (times[i] - j);
                    if (cover_dist > win_dist)
                    {
                        wins[i] += 1;
                    }
                }
            }

            var ans = 1;
            foreach (var win in wins)
            {
                ans *= win;
            }
            Console.WriteLine(ans);
        }

        public void Secound()
        {
            var inputs = File.ReadAllLines(@"C:\Users\akash.buch\Source\Repos\AdvantOfCode2023\AdvantOfCode2023\Day6\input6.txt");

            var time = long.Parse(inputs.First().Split(":").Last().Trim().Replace(" ", ""));
            var distance = long.Parse(inputs.Last().Split(":").Last().Trim().Replace(" ", ""));

            var wins = 0;

            var win_dist = distance;

            for (int j = 0; j < time; j++)
            {
                var cover_dist = j * (time - j);
                if (cover_dist > win_dist)
                {
                    wins += 1;
                }
            }

            Console.WriteLine(wins);
        }
    }
}

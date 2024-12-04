using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdvantOfCode._2023.Day5
{
    public class Solutions
    {
        private class Map
        {
            public long Dest { get; set; }
            public long Source { get; set; }
            public long Range { get; set; }
        }

        public void First()
        {
            var fs = File.ReadAllText(@"C:\Users\akash.buch\Source\Repos\AdvantOfCode2023\AdvantOfCode2023\Day5\input5_test.txt");
            
            var inputs =  fs.Split("\r\n\r\n");

            var seeds = inputs.First().Split(":").Last().Trim().Split(" ").Select(x=>long.Parse(x)).ToList();

            foreach (var input in inputs.Skip(1))
            {
                var all_maps = input.Split("\r\n").Where(x => char.IsDigit(x[0])).ToList();

                List<Map> maps = new();
                foreach (var map in all_maps)
                {
                    Map m = new Map();
                    m.Dest = long.Parse(map.Split(" ")[0]);
                    m.Source = long.Parse(map.Split(" ")[1]);
                    m.Range = long.Parse(map.Split(" ")[2]);

                    maps.Add(m);
                }


                for (int i = 0; i < seeds.Count(); i++)
                {
                    var find = maps.FirstOrDefault(x => x.Source <= seeds[i] && (x.Source + x.Range - 1) >= seeds[i]);
                    if (find is null)
                    {
                        seeds[i] = seeds[i];
                    }
                    else
                    {
                        seeds[i] = seeds[i] - find.Source + find.Dest;
                    }
                }

            }

            Console.WriteLine(seeds.Min());
        }

        public void Print(List<int> list)
        {
            foreach (var item in list)
            {
                Console.Write($"{item} | ");
            }
        }

        public void Secound()
        {
            var fs = File.ReadAllText(@"C:\Users\akash.buch\Source\Repos\AdvantOfCode2023\AdvantOfCode2023\Day5\input5_test.txt");

            var inputs = fs.Split("\r\n\r\n");

            var seeds_raw = inputs.First().Split(":").Last().Trim().Split(" ").Select(x => long.Parse(x)).ToList();

            var seeds = new List<long>();
            for (int i = 0; i < seeds_raw.Count(); i = i+2)
            {
                for (int j = 0; j < seeds_raw[i+1]; j++)
                {
                    seeds.Add(seeds_raw[i] + j);
                }
            }


            int counter = 0;
            foreach (var input in inputs.Skip(1))
            {
                var all_maps = input.Split("\r\n").Where(x => char.IsDigit(x[0])).ToList();

                List<Map> maps = new();
                foreach (var map in all_maps)
                {
                    Map m = new Map();
                    m.Dest = long.Parse(map.Split(" ")[0]);
                    m.Source = long.Parse(map.Split(" ")[1]);
                    m.Range = long.Parse(map.Split(" ")[2]);

                    maps.Add(m);

                    //seeds.RemoveAll(x => x > m.Source && x <= (m.Source + m.Range - 1));
                    //seeds.Add(m.Source);
                }

                //seeds = seeds.Distinct().ToList();
                for (int i = 0; i < seeds.Count(); i++)
                {
                    var find = maps.FirstOrDefault(x => x.Source <= seeds[i] && (x.Source + x.Range - 1) >= seeds[i]);
                    if (find is null)
                    {
                        seeds[i] = seeds[i];
                    }
                    else
                    {
                        seeds[i] = seeds[i] - find.Source + find.Dest;
                    }
                }
            }

            Console.WriteLine("=========================");
            Console.WriteLine(seeds.Where(x=>x>0).Min());
            //File.WriteAllText("./output.txt", seeds.Min().ToString());
        }
    }
}

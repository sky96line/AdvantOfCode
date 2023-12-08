using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdvantOfCode2023.Day8
{

    public class Solutions
    {
        public List<string> inputs = new();

        public string direction = "";

        public List<string> nodes = new();

        public Solutions()
        {
            inputs = File.ReadAllLines(@"C:\Users\akash.buch\source\repos\AdvantOfCode2023\AdvantOfCode2023\Day8\input8.txt").ToList();

            direction = inputs.First();

            nodes = inputs.Skip(2).ToList();
        }
        public int FirstPart2(string node)
        {
            var step = 0;
            while (node[2] != 'Z')
            {
                if (direction[step % direction.Length] == 'L')
                {
                    var left = node.Split(" = ").Last().Split(',').First().Replace("(", "").Replace(" ", "");
                    node = nodes.FirstOrDefault(x => x.StartsWith($"{left} = "));
                }
                if (direction[step % direction.Length] == 'R')
                {
                    var right = node.Split(" = ").Last().Split(',').Last().Replace(")", "").Replace(" ", "");
                    node = nodes.FirstOrDefault(x => x.StartsWith($"{right} = "));
                }
                step++;
            }

            //Console.WriteLine(node);
            Console.WriteLine(step);
            return step;
        }

        public int lcm(int x, int y)
        {
            if (y == 0)
                return x;
            return gcd(y, x % y);
        }


        public int gcd(int x, int y)
        {
            if (y == 0)
                return x;
            return gcd(y, x % y);
        }

        public void Secound()
        {
            var allNodes = inputs.Skip(2).ToList();

            var nodes = allNodes.Where(x => x[2] == 'A').ToList();

            List<int> index = new List<int>();
            foreach (var node in nodes)
            {
                var i = FirstPart2(node);
                index.Add(i);
            }

            LCM lcm = new LCM(index);
            Console.WriteLine(lcm.getLCM());


            /// Force way to do it.

            //var step = 0;
            //while (nodes.Count(x => x[2] != 'Z') > 0)
            //{
            //    Utility.PrintList(nodes);
            //    Console.WriteLine("=================");
            //    if (direction[step % direction.Length] == 'L')
            //    {
            //        var new_dir = new List<string>();
            //        foreach (var node in nodes)
            //        {
            //            var left = node.Split(" = ").Last().Split(',').First().Replace("(", "").Replace(" ", "");
            //            var lefy_node = allNodes.First(x => x.StartsWith($"{left} = "));
            //            new_dir.Add(lefy_node);
                        
            //        }
            //        nodes.Clear();
            //        nodes.AddRange(new_dir);
            //    }
            //    if (direction[step % direction.Length] == 'R')
            //    {
            //        var new_dir = new List<string>();
            //        foreach (var node in nodes)
            //        {
            //            var right = node.Split(" = ").Last().Split(',').Last().Replace(")", "").Replace(" ", "");
            //            var right_node = allNodes.First(x => x.StartsWith($"{right} = "));
            //            new_dir.Add(right_node);
            //        }
            //        nodes.Clear();
            //        nodes.AddRange(new_dir);

            //        //node = allNodes.FirstOrDefault(x => x.StartsWith($"{right} = "));
            //    }
                
            //    step++;
            //}

            //Utility.PrintList(nodes);
            //Console.WriteLine(step);
        }
    }
    
}

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

        public void First()
        {
            var inputs = File.ReadAllLines(@"C:\Users\akash.buch\source\repos\AdvantOfCode2023\AdvantOfCode2023\Day8\input8_test.txt");

            var direction = inputs.First();

            var nodes = inputs.Skip(2).ToList();

            var node = nodes.First(x => x.StartsWith("AAA = "));

            var step = 0;
            while (!node.StartsWith("ZZZ = "))
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

            Console.WriteLine(node);
            Console.WriteLine(step);
        }
        public void Secound()
        {
        }
    }
    
}

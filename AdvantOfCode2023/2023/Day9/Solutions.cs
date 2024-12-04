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
using System.Transactions;

namespace AdvantOfCode._2023.Day9
{

    public class Solutions
    {
        public int RecFunction1(List<int> input)
        {
            // Utility.PrintList(input);

            if (input.TrueForAll(x=>x == 0))
                return 0;

            List<int> output = new List<int>();

            for (int i = 0; i < input.Count - 1; i++)
            {
                output.Add(input[i + 1] - input[i]);
            }

            return input.Last() + RecFunction1(output);
        }

        public void First()
        {
            //var inputs = File.ReadAllLines(@"C:\Users\akash.buch\source\repos\AdvantOfCode2023\AdvantOfCode2023\Day9\input9_test.txt").ToList();
            var inputs = File.ReadAllLines(@"C:\Users\akash.buch\source\repos\AdvantOfCode2023\AdvantOfCode2023\Day9\input9.txt").ToList();


            var total = 0;

            foreach (var input in inputs)
            {
                var inp = input.Split(" ").Select(x => int.Parse(x)).ToList();

                var ans = RecFunction1(inp);
                total += ans;

                Console.WriteLine($"{JsonConvert.SerializeObject(inp)}: {ans}");
            }

            Console.WriteLine(total);
        }




        public int RecFunction2(List<int> input)
        {
            // Utility.PrintList(input);

            if (input.TrueForAll(x => x == 0))
                return 0;

            List<int> output = new List<int>();

            for (int i = 0; i < input.Count - 1; i++)
            {
                output.Add(input[i + 1] - input[i]);
            }

            return input.First() - RecFunction2(output);
        }

        public void Secound()
        {
            //var inputs = File.ReadAllLines(@"C:\Users\akash.buch\source\repos\AdvantOfCode2023\AdvantOfCode2023\Day9\input9_test.txt").ToList();
            var inputs = File.ReadAllLines(@"C:\Users\akash.buch\source\repos\AdvantOfCode2023\AdvantOfCode2023\Day9\input9.txt").ToList();


            var total = 0;

            foreach (var input in inputs)
            {
                var inp = input.Split(" ").Select(x => int.Parse(x)).ToList();

                var ans = RecFunction2(inp);
                total += ans;

                Console.WriteLine($"{JsonConvert.SerializeObject(inp)}: {ans}");
            }

            Console.WriteLine(total);
        }
    }

}

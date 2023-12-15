using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace AdvantOfCode2023.Day13
{

    public class Solutions
    {
        public int LeftRight(int l , string left, int r , string right)
        {
            if (left.Equals(right)) return l + r + 2;

            return 0;
        }

        public int GameCalculate(string str)
        {
            //left - right
            var game = str.Split("\r\n");


            var td_max = 0;
            var td_max_count = 0;

            for (int i = 0; i < game.Length - 1; i++)
            {
                var ans = LeftRight(i, game[i], i+1, game[i + 1]);

                if(td_max_count < ans)
                {
                    td_max_count = ans;
                    td_max = i;
                }
            }


            var lr_max = 0;
            var lr_max_count = 0;

            for (int i = 0; i < game[0].Length - 1; i++)
            {
                var l = new string(game.Select(x => x[i]).ToArray());
                var r = new string(game.Select(x => x[i+1]).ToArray());

                var ans = LeftRight(i, l, i + 1, r);

                if (lr_max_count < ans)
                {
                    lr_max_count = ans;
                    lr_max = i;
                }
            }

            Console.WriteLine($"top-down: {td_max} | left-right: {lr_max}");
            return td_max > lr_max ? td_max : lr_max;
        }

        public void First()
        {
            var inputs = File.ReadAllText(@"C:\Users\akash.buch\source\repos\AdvantOfCode2023\AdvantOfCode2023\Day13\input13_test.txt");

            var games = inputs.Split("\r\n\r\n");

            var t1 = GameCalculate(games[0]);
            Console.WriteLine(t1);

            var t2 = GameCalculate(games[1]);
            Console.WriteLine(t2);

            //foreach (var game in games)
            //{
            //    var t = GameCalculate(game);
            //    Console.WriteLine(t);
            //}
        }

        public void Secound()
        {
            var inputs = File.ReadAllLines(@"C:\Users\akash.buch\source\repos\AdvantOfCode2023\AdvantOfCode2023\Day10\input10_test.txt").ToList();            
        }
    }

}

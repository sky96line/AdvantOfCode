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
        public string GetStringFromIndex(string[] str, int index)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string s in str)
            {
                sb.Append(s[index]);
            }
            return sb.ToString();
        }


        public int GameCalculate(string str)
        {
            //top to down
            var game = str.Split("\r\n");

            // find mirror using x = x+1
            var mirror_top = 0;
            var mirror_bottom = 0;

            for (int i = 1; i < game.Length - 1; i++)
            {
                if (game[i].Equals(game[i + 1]))
                {
                    mirror_top = i;
                    mirror_bottom = i + 1;
                    break;
                }
            }

            // outward logic

            var IsReflaction = false;
            var refcount = 1;
            while ((mirror_top - refcount) >= 0 && (mirror_bottom + refcount) < game.Count())
            {
                if (game[mirror_top - refcount].Equals(game[mirror_bottom + refcount]))
                {
                    if ((mirror_top - refcount) == 0 || (mirror_bottom + refcount) == game.Count() - 1)
                    {
                        IsReflaction = true;
                        break;
                    }
                }
                else
                {
                    break;
                }
                refcount++;
            }

            if (IsReflaction)
            {
                return (mirror_top + 1) * 100;
            }




            var mirror_left = 0;
            var mirror_right = 0;

            for (int i = 1; i < game[0].Length - 1; i++)
            {
                if (GetStringFromIndex(game,i).Equals(GetStringFromIndex(game, i + 1)))
                {
                    mirror_left = i;
                    mirror_right = i + 1;
                    break;
                }
            }


            refcount = 1;
            while ((mirror_left - refcount) >= 0 && (mirror_right + refcount) < game[0].Length)
            {
                if (GetStringFromIndex(game, mirror_left - refcount).Equals(GetStringFromIndex(game,mirror_right + refcount)))
                {
                    if ((mirror_left - refcount) == 0 || (mirror_right + refcount) == game[0].Length - 1)
                    {
                        IsReflaction = true;
                        break;
                    }
                }
                else
                {
                    break;
                }
                refcount++;
            }


            if (IsReflaction)
            {
                return (mirror_left + 1);
            }

            return 0;
        }

        public void First()
        {
            var inputs = File.ReadAllText(@"C:\Users\akash.buch\source\repos\AdvantOfCode2023\AdvantOfCode2023\Day13\input13.txt");

            var games = inputs.Split("\r\n\r\n");

            var total = 0;

            int i = 1;
            foreach (var game in games)
            {
                var t = GameCalculate(game);
                total += t;
                Console.WriteLine($"Count: {i++} | {t}");
            }

            Console.WriteLine(total);
        }

        public void Secound()
        {
            var inputs = File.ReadAllLines(@"C:\Users\akash.buch\source\repos\AdvantOfCode2023\AdvantOfCode2023\Day10\input10_test.txt").ToList();            
        }
    }

}

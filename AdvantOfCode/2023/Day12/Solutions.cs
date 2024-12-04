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

namespace AdvantOfCode._2023.Day12
{

    public class Solutions
    {
        private string Input(string q, List<int> a)
        {
            StringBuilder sb = new StringBuilder(q);

            for (int i = 0; i < a.Count(); i++)
            {
                for (int j = 0; j < q.Length; j++)
                {
                    if (sb[j] == '?')
                    {
                        int x = 0;
                        for (x = 0; x <= i; x++)
                        {
                            var index = j + x;
                            sb[index] = '#';
                        }

                        //if (sb[j + x] == '?')
                        //{
                        //    sb[j + x] = '.';
                        //}

                        Console.WriteLine(sb.ToString());
                    }
                }

                return sb.ToString();
            }

            return "";
        }


        public char Getch(string str, int index)
        {
            if (index == 0)
            {
                if (str[index + 1] == '.') return '#';
                if (str[index + 1] == '?') return '#';
                if (str[index + 1] == '#') return '.';
            }
            else if (index == str.Length)
            {
                if (str[index - 1] == '.') return '#';
                if (str[index - 1] == '?') return '#';
                if (str[index - 1] == '#') return '.';
            }
            else
            {
                if (str[index - 1] == '.' && str[index + 1] == '.') return '#';
                if (str[index - 1] == '.' && str[index + 1] == '?') return '#';
                if (str[index - 1] == '.' && str[index + 1] == '#') return '.';

                if (str[index - 1] == '?' && str[index + 1] == '.') return '#';
                if (str[index - 1] == '?' && str[index + 1] == '?') return '#';
                if (str[index - 1] == '?' && str[index + 1] == '#') return '.';

                if (str[index - 1] == '#' && str[index + 1] == '.') return '.';
                if (str[index - 1] == '#' && str[index + 1] == '?') return '.';
                if (str[index - 1] == '#' && str[index + 1] == '#') return '.';
            }
            return '\0';
        }

        public string fn(string str, int index, List<int> arrang)
        {
            StringBuilder sb = new StringBuilder(str.ToString());

            var flag = true;
            foreach (var arg in arrang)
            {
                var c = 0;
                for (int i = index; i < str.Length && c < arg; i++)
                {
                    if (flag)
                    {
                        var ch = Getch(sb.ToString(), i);
                        sb[i] = ch;
                        flag = false;
                    }
                    else
                    {
                        sb[i] = '#';
                    }

                    c++;
                }

                return sb.ToString();
            }

            return "";
        }

        public void First()
        {
            var inputs = File.ReadAllLines(@".\input12_test.txt").ToList();

            

            var game = ".??..??...?##. 1,1,3";


            //var game = ".??..??...?##. 1,1,3";

            var q = game.Split(" ").First();
            var a = game.Split(" ").Last().Split(",").Select(x => int.Parse(x)).ToList();

            var indexes = new List<int>();

            for (int i = 0; i < game.Length; i++)
            {
                if (game[i] == '?')
                {
                    indexes.Add(i);
                }
            }


            foreach (var index in indexes)
            {
                var ans = fn(q, index, a);

                Console.WriteLine(ans);
            }


            //StringBuilder sb = new StringBuilder(q);
            //for (int i = 0; i < indexes.Count; i++) // 0, 1, 2
            //{
            //    for (int j = indexes[i]; j < a.Count; j++) // 1, 1, 3
            //    {
            //        int k = 0;
            //        for (k = j; k < a[j]; k++) 
            //        {
            //            if (sb[k + j] == '?') sb[k] = '#';
            //        }
            //        if (sb[k + j] == '?') sb[k] = '.';
                    
            //        sb = new StringBuilder(sb.ToString());
            //        Console.WriteLine(sb.ToString());

            //        break;
            //    }
            //}



            //Console.WriteLine(sb.ToString());
        }

        public void Secound()
        {
            var inputs = File.ReadAllLines(@".\input10_test.txt").ToList();            
        }
    }

}

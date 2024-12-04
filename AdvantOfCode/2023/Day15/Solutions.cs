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

namespace AdvantOfCode._2023.Day15
{

    public class Solutions
    {
        public (int HASH,string AD, int FocalLength) Hash(string str)
        {
            var val = 0;
            var index = 0;
            foreach (var c in str)
            {
                if (c == '-' || c == '=')
                {
                    if (c == '-') return (val, "D", 0);
                    if (c == '=') return (val, "A", int.Parse(str[++index].ToString()));
                }
                index++;

                var ascii = (int)c + val;

                var ascii_mul = ascii * 17;

                var ascii_mod = ascii_mul % 256;

                val = ascii_mod;
            }

            return (val, "", 0);
        }

        public class Box
        {
            public string Lable { get; set; }
            public int FocalLength { get; set; }
        }

        //public class DataStruct
        //{
        //    public int BoxNumber { get; set; }
        //    List<Box> boxes= new List<Box>();
        //} 

        public void First()
        {
            var inputs = File.ReadAllText(@".\input15_test.txt");


            var vals = inputs.Split(',');
            var total = 0;
            foreach (var val in vals)
            {
                var v = Hash(val);
                total += v.Item1;
            }

            Console.WriteLine(total);


        }

        public void Print(string Label, Dictionary<int, List<Box>> dic)
        {
            Console.WriteLine($"After \"{Label}\"");
            foreach (var d in dic)
            {
                Console.Write($"Box {d.Key}");

                foreach (var item in d.Value)
                {
                    Console.Write($" [{item.Lable} {item.FocalLength}] ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void Secound()
        {
            var inputs = File.ReadAllText(@".\input15.txt");

            Dictionary<int, List<Box>> dic = new Dictionary<int, List<Box>>();

            var vals = inputs.Split(',');

            foreach (var val in vals)
            {
                var hash = Hash(val);
                var label = val.Contains("=") ? val.Split("=").First() : val.Split("-").First();

                if (hash.AD.Equals("A"))
                {
                    if (dic.ContainsKey(hash.HASH))
                    {
                        var v = dic[hash.HASH].FirstOrDefault(x => x.Lable.Equals(label));

                        if(v is null)
                        {
                            dic[hash.HASH].Add(new Box() { Lable = label, FocalLength = hash.FocalLength });
                        }
                        else
                        {
                            v.FocalLength = hash.FocalLength;
                        }
                    }
                    else
                    {
                        dic.Add(hash.HASH, new List<Box>() { new Box() { Lable = label, FocalLength = hash.FocalLength } });
                    }
                }
                else if (hash.AD.Equals("D"))
                {
                    if (dic.ContainsKey(hash.HASH))
                    {
                        dic[hash.HASH].RemoveAll(x=>x.Lable.Equals(label));

                        if (dic[hash.HASH].Count == 0)
                        {
                            dic.Remove(hash.HASH);
                        }
                    }
                }

                // Print(val, dic);
            }


            
            int total = 0;

            foreach (var d in dic)
            {
                int slot = 0;
                foreach (var item in d.Value)
                {
                    total += ((d.Key + 1) * (slot + 1) * (item.FocalLength));

                    slot++;
                }
            }

            Console.WriteLine(total);
            //Utility.PrintList(dic);
        }
    }

}

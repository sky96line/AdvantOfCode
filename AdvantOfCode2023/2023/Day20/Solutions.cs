using AdvantOfCode._2023.Day19;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace AdvantOfCode._2023.Day20
{
    public class Modual
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public bool Switch { get; set; }
        public int Pulse { get; set; }

        public Modual(string type, string name, bool @switch)
        {
            Type = type;
            Name = name;

            if (Type.Equals("%"))
            {
                Switch = false;
            }
            else if (Type.Equals("&"))
            {
                Switch = false;
                Pulse = 0;
            }
            else if (Type.Equals("broadcaster"))
            {
                Switch = false;
            }
        }

        public int SendPulse()
        {
            if (Type.Equals("%"))
            {

            }
            else if (Type.Equals("&"))
            {

            }
            else if (Type.Equals("broadcaster"))
            {
                Pulse = 0;
            }

            return Pulse;
        }
    }

    

    public class Solutions
    {
        //public List<Modual> Storage = new List<Modual>();
        Dictionary<string,int> Stack = new();

        List<string> inputs;
        public Solutions()
        {
            inputs = File.ReadAllLines("C:\\Users\\akash.buch\\source\\repos\\AdvantOfCode2023\\AdvantOfCode2023\\Day20\\input20_test.txt").ToList();
        }

        int SendPulse(string modual)
        {
            if (Stack.ContainsKey(modual))
            {
                return 0;
            }
            else
            {
                if (modual.Equals("broadcaster"))
                {
                    return 0;
                }
                else if (modual.StartsWith("%"))
                {
                    return 0;
                }
                else if (modual.StartsWith("&"))
                {
                    return 1;
                }
            }

            return -1;
        }

        void Loop(string input)
        {
            var sender = input.Split(" -> ").First();
            var recivers = input.Split(" -> ").Last().Split(", ").Select(x => x.Trim());

            foreach (var reciver in recivers)
            {
                Console.WriteLine($"{sender} -{SendPulse(sender)} -> {reciver}");
            }


        }

        public void First()
        {
            foreach (var input in inputs)
            {
                Loop(input);
            }
        }

        public void Secound()
        {

        }
    }

}

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

namespace AdvantOfCode2023.Day10
{

    public class Solutions
    {
        public class Pipe
        {
            public char Name { get; set; }
            public int row { get; set; }
            public int col { get; set; }
        }

        public class Path
        {
            public Pipe Curr { get; set; }
            public List<Pipe> NextPipes { get; set; } = new List<Pipe>();
            public int Count { get; set; }
        }

        //List<Pipe> data = new List<Pipe>();

        List<string> inputs;
        char[,] paths;

        public Solutions()
        {
            inputs = File.ReadAllLines(@"C:\Users\akash.buch\source\repos\AdvantOfCode2023\AdvantOfCode2023\Day10\input10_test.txt").ToList();

            paths = new char[inputs.Count, inputs[0].Length];

        }

        class Point
        {
            public int row { get; set; }
            public int col { get; set; }
        }

        List<Pipe> dic { get; set; } = new();

        private (int row, int col) GetCordt(char dir)
        {
            if (dir == 'N') return (-1, 0);
            if (dir == 'S') return (1, 0);
            if (dir == 'E') return (0, 1);
            if (dir == 'W') return (0, -1);
            return (0, 0);
        }

        private char GetDir(char ch, char dir)
        {
            if ('|' == ch && 'N' == dir) return 'N';
            if ('|' == ch && 'S' == dir) return 'S';

            if ('-' == ch && 'E' == dir) return 'E';
            if ('-' == ch  && 'W' == dir) return 'W';

            if ('L' == ch  && 'S' == dir) return 'E';
            if ('L' == ch  && 'W' == dir) return 'N';

            if ('J' == ch  && 'S' == dir) return 'W';
            if ('J' == ch  && 'E' == dir) return 'N';
            
            if ('7' == ch  && 'E' == dir) return 'S';
            if ('7' == ch  && 'N' == dir) return 'W';

            if ('F' == ch  && 'W' == dir) return 'S';
            if ('F' == ch  && 'N' == dir) return 'E';

            return 'N';
        }

        public void First()
        {
            //List<Path> paths = new List<Path>();
          
            int start_i = 0;
            int start_j = 0;

            int i = 0;
            foreach (var input in inputs)
            {
                int j = 0;
                foreach (var ch in input)
                {
                    if (ch == 'S')
                    {
                        start_i = i;
                        start_j = j;
                    }
                    paths[i,j] = ch;
                    j++;
                }
                i++;
            }

            //DFS(start_i, start_j, 'W', 0);

            var dir = 'E';
            var curr_i = start_i;
            var curr_j = start_j;

            int count = 0;
            while (true)
            {
                Console.WriteLine(paths[curr_i, curr_j]);


                var cordt = GetCordt(dir);

                curr_i += cordt.row;
                curr_j += cordt.col;

                var next = paths[curr_i, curr_j];

                if (next == 'S')
                    break;

                dir = GetDir(paths[curr_i, curr_j], dir);
                count++;
            }

            Console.WriteLine(count);
        }

        public void Secound()
        {
            var inputs = File.ReadAllLines(@"C:\Users\akash.buch\source\repos\AdvantOfCode2023\AdvantOfCode2023\Day10\input10_test.txt").ToList();            
        }
    }

}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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

namespace AdvantOfCode._2023.Day10
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
        char[,] ans;

        public Solutions()
        {
            inputs = File.ReadAllLines(@"C:\Users\akash.buch\source\repos\AdvantOfCode2023\AdvantOfCode2023\Day10\input10.txt").ToList();

            paths = new char[inputs.Count, inputs[0].Length];
            ans = new char[inputs.Count, inputs[0].Length];

        }

        class Point
        {
            public int row { get; set; }
            public int col { get; set; }
        }

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
                    paths[i, j] = ch;
                    j++;
                }
                i++;
            }

            //DFS(start_i, start_j, 'W', 0);

            var dir = 'S';
            var curr_i = start_i;
            var curr_j = start_j;

            int count = 0;
            while (true)
            {
                //Console.WriteLine(paths[curr_i, curr_j]);

                int c_i = curr_i * 2;
                int c_j = curr_j * 2;

                ans[c_i, c_j] = paths[curr_i, curr_j];

                var cordt = GetCordt(dir);

                curr_i += cordt.row;
                curr_j += cordt.col;

                var next = paths[curr_i, curr_j];

                if (next == 'S')
                    break;

                dir = GetDir(paths[curr_i, curr_j], dir);
                count++;
            }

            Console.WriteLine(Math.Ceiling((decimal)count / 2));

            DoubleAns();

            FloodFillRecursive(0, 0);

            int c_count = 0;
            StringBuilder sb = new StringBuilder();
            for (int x = 0; x < inputs.Count * 2; x++)
            {
                for (int y = 0; y < inputs[0].Length * 2; y++)
                {
                    if (ans[x, y] == '\0')
                    {
                        Console.Write(".");
                        
                        c_count++;
                    }
                    else
                    {
                        if (ans[x, y] == 'F') Console.Write("┌");
                        else if (ans[x, y] == '7') Console.Write("┐");
                        else if (ans[x, y] == 'J') Console.Write("┘");
                        else if (ans[x, y] == 'L') Console.Write("└");
                        else if (ans[x, y] == '|') Console.Write("│");
                        else if (ans[x, y] == '-') Console.Write("─");
                        else
                        {
                            Console.Write(ans[x, y]);
                        }
                        //else if (ans[x, y] == 'S') Console.Write("┌");
                        //else { Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("┌"); }

                        //Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                //sb.AppendLine("");
                Console.WriteLine("");
            }

            Console.WriteLine(c_count);
        }

        void DoubleAns()
        {
            char[,] result = new char[inputs.Count * 2, inputs[0].Length * 2];



        }

        List<char> dirs = new List<char>()
        {
            'F',
            '7',
            'J',
            'L',
            '|',
            '-'
        };

        void FloodFillRecursive(int x, int y)
        {
            // Check if the current position is within the canvas boundaries
            if (x < 0 || x >= ans.GetLength(0) || y < 0 || y >= ans.GetLength(1))
                return;

            // Check if the current pixel has the original color
            if (ans[x, y] == '#' || dirs.Contains(ans[x,y]))
                return;

            // Set the current pixel to the new color
            ans[x, y] = '#';

            // Recursive calls for neighboring pixels
            FloodFillRecursive(x + 1, y); // Right
            FloodFillRecursive(x - 1, y); // Left
            FloodFillRecursive(x, y + 1); // Down
            FloodFillRecursive(x, y - 1); // Up
            
            FloodFillRecursive(x - 1, y - 1); // Down
            FloodFillRecursive(x - 1, y + 1); // Up
            FloodFillRecursive(x + 1, y - 1); // Up
            FloodFillRecursive(x + 1, y + 1); // Up
        }

        public void Flood(int i, int j)
        {
            if (i == inputs.Count + 1 && j == inputs[0].Length + 1)
            {
                ans[i, j] = '#';
                return;
            }

            if (dirs.Contains(ans[i, j]))
                return;

            ans[i, j] = '#';

            if (i == inputs.Count + 1)
            {
                Flood(i, j + 1);
            }
            else if (j == inputs[0].Length + 1)
            {
                Flood(i + 1, j);
            }
            else
            {
                Flood(i, j + 1);
                Flood(i + 1, j + 1);
                Flood(i + 1, j);
            }
        }

        private int indexOf(char[] ch, bool firstOrLast)
        {
            if (firstOrLast)
            {
                for (int i = 0; i < ch.Length; i++)
                {
                    if (ch[i] == 'X') return i;
                }
            }
            else
            {
                for (int i = ch.Length; i >0; i--)
                {
                    if (ch[i] == 'X') return i;
                }
            }

            return -1;
        }


        public void Secound()
        {
            var inputs = File.ReadAllLines(@"C:\Users\akash.buch\source\repos\AdvantOfCode2023\AdvantOfCode2023\Day10\input10_test.txt").ToList();            
        }
    }

}

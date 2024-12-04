using AdvantOfCode._2023.Day19;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using System.Xml.Linq;

namespace AdvantOfCode._2023.Day21
{
    public class Point
    {
        public int i;
        public int j;
        public string Place { get; set; }
        public List<Point> Child { get; set; } = new();
    }

    public class Solutions
    {
        char[,] maps;

        List<Point> tree = new();
        Point start = new() { i = -1, j = -1 };

        public Solutions()
        {
            var inputs = File.ReadAllLines(@".\input21_test.txt");

            maps = new char[inputs.Length, inputs[0].Length];

            for (int i = 0; i < inputs.Length; i++)
            {
                for (int j = 0; j < inputs[0].Length; j++)
                {
                    maps[i, j] = inputs[i][j];

                    if (inputs[i][j] == 'S')
                    {
                        start.i = i;
                        start.j = j;
                    }
                }
            }
        }

        
        public void MakeTree()
        {
            for (int i = 0; i < maps.GetLength(0); i++)
            {
                for (int j = 0; j < maps.GetLength(1); j++)
                {
                    Point p = new();

                    p.i = i;
                    p.j= j;

                    if (i == 0 && j == 0)
                    {
                        if (maps[i, j + 1] != '#')
                        {
                            Point p_r = new();
                            p_r.i = i;
                            p_r.j = j + 1;
                            p.Child.Add(p_r);
                        }

                        if (maps[i + 1, j] != '#')
                        {
                            Point p_d = new();
                            p_d.i = i + 1;
                            p_d.j = j;
                            p.Child.Add(p_d);
                        }
                    }
                    else if (i == 0 && j == maps.GetLength(1) - 1)
                    {
                        if (maps[i, j - 1] != '#')
                        {
                            Point p_l = new();
                            p_l.i = i;
                            p_l.j = j - 1;
                            p.Child.Add(p_l);
                        }

                        if (maps[i + 1, j] != '#')
                        {
                            Point p_d = new();
                            p_d.i = i + 1;
                            p_d.j = j;
                            p.Child.Add(p_d);
                        }
                    }
                    else if (i == maps.GetLength(0) - 1 && j == 0)
                    {
                        if (maps[i - 1, j] != '#')
                        {
                            Point p_u = new();
                            p_u.i = i - 1;
                            p_u.j = j;
                            p.Child.Add(p_u);
                        }

                        if (maps[i, j + 1] != '#')
                        {
                            Point p_r = new();
                            p_r.i = i;
                            p_r.j = j + 1;
                            p.Child.Add(p_r);
                        }
                    }
                    else if (i == maps.GetLength(0) - 1 && j == maps.GetLength(1) - 1)
                    {
                        if (maps[i - 1, j] != '#')
                        {
                            Point p_u = new();
                            p_u.i = i - 1;
                            p_u.j = j;
                            p.Child.Add(p_u);
                        }
                        if (maps[i, j - 1] != '#')
                        {
                            Point p_l = new();
                            p_l.i = i;
                            p_l.j = j - 1;
                            p.Child.Add(p_l);
                        }
                    }
                    else if (i == 0)
                    {
                        if (maps[i, j - 1] != '#')
                        {
                            Point p_l = new();
                            p_l.i = i;
                            p_l.j = j - 1;
                            p.Child.Add(p_l);
                        }
                        if (maps[i, j + 1] != '#')
                        {
                            Point p_r = new();
                            p_r.i = i;
                            p_r.j = j + 1;
                            p.Child.Add(p_r);
                        }
                        if (maps[i + 1, j] != '#')
                        {
                            Point p_d = new();
                            p_d.i = i + 1;
                            p_d.j = j;
                            p.Child.Add(p_d);
                        }
                    }
                    else if (i == maps.GetLength(0) - 1)
                    {
                        if (maps[i, j - 1] != '#')
                        {
                            Point p_l = new();
                            p_l.i = i;
                            p_l.j = j - 1;
                            p.Child.Add(p_l);
                        }
                        if (maps[i, j + 1] != '#')
                        {
                            Point p_r = new();
                            p_r.i = i;
                            p_r.j = j + 1;
                            p.Child.Add(p_r);
                        }
                        if (maps[i - 1, j] != '#')
                        {
                            Point p_u = new();
                            p_u.i = i - 1;
                            p_u.j = j;
                            p.Child.Add(p_u);
                        }
                    }
                    else if (j == 0)
                    {
                        if (maps[i, j + 1] != '#')
                        {
                            Point p_r = new();
                            p_r.i = i;
                            p_r.j = j + 1;
                            p.Child.Add(p_r);
                        }
                        if (maps[i - 1, j] != '#')
                        {
                            Point p_u = new();
                            p_u.i = i - 1;
                            p_u.j = j;
                            p.Child.Add(p_u);
                        }
                        if (maps[i + 1, j] != '#')
                        {
                            Point p_d = new();
                            p_d.i = i + 1;
                            p_d.j = j;
                            p.Child.Add(p_d);
                        }
                    }
                    else if (j == maps.GetLength(1) - 1)
                    {
                        if (maps[i, j - 1] != '#')
                        {
                            Point p_l = new();
                            p_l.i = i;
                            p_l.j = j - 1;
                            p.Child.Add(p_l);
                        }
                        if (maps[i - 1, j] != '#')
                        {
                            Point p_u = new();
                            p_u.i = i - 1;
                            p_u.j = j;
                            p.Child.Add(p_u);
                        }
                        if (maps[i + 1, j] != '#')
                        {
                            Point p_d = new();
                            p_d.i = i + 1;
                            p_d.j = j;
                            p.Child.Add(p_d);
                        }
                    }
                    else
                    {
                        if (maps[i - 1, j] != '#')
                        {
                            Point p_u = new();
                            p_u.i = i - 1;
                            p_u.j = j;
                            p.Child.Add(p_u);
                        }

                        if (maps[i, j + 1] != '#')
                        {
                            Point p_r = new();
                            p_r.i = i;
                            p_r.j = j + 1;
                            p.Child.Add(p_r);
                        }

                        if (maps[i + 1, j] != '#')
                        {
                            Point p_d = new();
                            p_d.i = i + 1;
                            p_d.j = j;
                            p.Child.Add(p_d);
                        }

                        if (maps[i, j - 1] != '#')
                        {
                            Point p_l = new();
                            p_l.i = i;
                            p_l.j = j - 1;
                            p.Child.Add(p_l);
                        }
                    }

                    tree.Add(p);
                }
            }
        }

        public void PrintNode(Point p)
        {
            Console.Write($"Parent : [{p.i},{p.j}] |");

            foreach (var c in p.Child)
            {
                Console.Write($" [{c.i},{c.j}] ");
            }

            Console.WriteLine();
        }

        public void DFS(Point point,  int depth, int max_depth)
        {
            if (point == null || depth > max_depth)
                return;

            Console.WriteLine($"[{point.i},{point.j}] | {depth}");

            foreach (Point child in point.Child)
            {
                DFS(child, depth + 1, max_depth);
            }
        }


        public void Print(int x, int y)
        {
            for (int i = 0; i < maps.GetLength(0); i++)
            {
                for (int j = 0; j < maps.GetLength(1); j++)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    if (x == i && y == j)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.Write(maps[i, j]);
                }
                Console.WriteLine();
            }
        }

        public void First()
        {
            MakeTree();
            var s = tree.First(x => x.i == start.i && x.j == start.j);
            DFS(s, 1, 4);
        }

        public void Secound()
        {

        }
    }

}

using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

namespace AdvantOfCode._2023.Day17
{

    public class Solutions
    {
        private (int row, int col) GetCordt(char dir)
        {
            if (dir == 'N') return (-1, 0);
            if (dir == 'S') return (1, 0);
            if (dir == 'E') return (0, 1);
            if (dir == 'W') return (0, -1);
            return (0, 0);
        }

        private (char, char) GetDir(char ch, char dir)
        {
            if ('|' == ch && ('N' == dir || 'S' == dir)) return (dir,'\0');
            if ('|' == ch && ('E' == dir || 'W' == dir)) return ('N','S');

            if ('-' == ch && ('E' == dir || 'W' == dir)) return (dir, '\0');
            if ('-' == ch && ('N' == dir || 'S' == dir)) return ('E', 'W');

            
            if ('\\' == ch && 'N' == dir) return ('W','\0');
            if ('\\' == ch && 'S' == dir) return ('E','\0');
            if ('\\' == ch && 'E' == dir) return ('S','\0');
            if ('\\' == ch && 'W' == dir) return ('N','\0');

            if ('/' == ch && 'N' == dir) return ('E', '\0');
            if ('/' == ch && 'S' == dir) return ('W', '\0');
            if ('/' == ch && 'E' == dir) return ('N', '\0');
            if ('/' == ch && 'W' == dir) return ('S', '\0');

            return (dir, '\0');
        }


        List<string> lookup = new();

        int[,] map;

        public int FindTotal()
        {
            var total = 0;
            for (int i = 0; i < inputs.Count; i++)
            {
                for (int j = 0; j < inputs[0].ToString().Length; j++)
                {
                    if (map[i, j] == '#')
                    {
                        total += 1;
                        Console.Write('#');
                    }
                    else
                    {
                        Console.Write('.');
                    }
                }
                Console.WriteLine();
            }

            lookup = new List<string>();
            map = new int[inputs.Count, inputs[0].ToString().Length];
            
            Console.WriteLine();
            Console.WriteLine();

            return total;
        }

        public (int, char) futhure(int curr_i, int curr_j, char dir)
        {
            var min = 10000;
            var min_index = 10000;
            var next_dir = '\0';

            for (int i = 0; i < 3; i++)
            {

                if (dir == 'E' || dir == 'W')
                {
                    if(curr_i == 0)
                    {
                        if (inputs[curr_i + 1].ToString()[curr_j + i] < min)
                        {
                            min = inputs[curr_i + 1].ToString()[curr_j + i];
                            min_index = i;
                            next_dir = 'S';
                        }
                    }
                    else if (curr_i == inputs.Count() - 1)
                    {
                        if (inputs[curr_i - 1].ToString()[curr_j - i] < min)
                        {
                            min = inputs[curr_i - 1].ToString()[curr_j - i];
                            min_index = i;
                            next_dir = 'N';
                        }
                    }
                    else
                    {
                        if (inputs[curr_i + 1].ToString()[curr_j + i] < min)
                        {
                            min = inputs[curr_i + 1].ToString()[curr_j + i];
                            min_index = i;
                            next_dir = 'S';
                        }
                        if (inputs[curr_i - 1].ToString()[curr_j - i] < min)
                        {
                            min = inputs[curr_i - 1].ToString()[curr_j - i];
                            min_index = i;
                            next_dir = 'N';
                        }
                    }
                }

                if (dir == 'N' || dir == 'S')
                {
                    if (curr_j == 0)
                    {
                        if (inputs[curr_i + i].ToString()[curr_j + 1] < min)
                        {
                            min = inputs[curr_i + i].ToString()[curr_j + 1];
                            min_index = i;
                            next_dir = 'E';
                        }
                    }
                    else if (curr_j == inputs[0].ToString().Length - 1)
                    {
                        if (inputs[curr_i - i].ToString()[curr_j - 1] < min)
                        {
                            min = inputs[curr_i - i].ToString()[curr_j - 1];
                            min_index = i;
                            next_dir = 'W';
                        }
                    }
                    else
                    {
                        if (inputs[curr_i + i].ToString()[curr_j + 1] < min)
                        {
                            min = inputs[curr_i + i].ToString()[curr_j + 1];
                            min_index = i;
                            next_dir = 'E';
                        }
                        if (inputs[curr_i - i].ToString()[curr_j - 1] < min)
                        {
                            min = inputs[curr_i - i].ToString()[curr_j - 1];
                            min_index = i;
                            next_dir = 'W';
                        }
                    }
                }
            }


            return (min_index, next_dir);
        }

        public void travel(int i, int j, char dir)
        {
            if(i < 0 || i > inputs[0].ToString().Length - 1 || j < 0 || j > inputs.Count - 1 || lookup.Exists(x=>x.Equals($"[{i},{j},{dir}]"))) return;
            //if (i < 0 || i > inputs[0].Length - 1 || j < 0 || j > inputs.Count - 1) return;

            Console.WriteLine($"[{i},{j}] {inputs[i].ToString()[j]}");

            map[i, j] = '#';
            lookup.Add($"[{i},{j},{dir}]");

            var (new_index, new_dir) = futhure(i, j, dir);

            if (dir == 'N')
            {
                travel(i - new_index, j, new_dir);
            }
            else if (dir == 'E')
            {
                travel(i, j + new_index, new_dir);
            }
            else if (dir == 'S')
            {
                travel(i + new_index, j, new_dir);
            }
            else if (dir == 'W')
            {
                travel(i, j - new_index, new_dir);
            }


            //var (dir1, dir2) = GetDir(inputs[i].ToString()[j], dir);

            //if (dir1 != '\0')
            //{
            //    var cordt = GetCordt(dir1);

            //    var d_i = i + cordt.row;
            //    var d_j = j + cordt.col;

            //    travel(d_i, d_j, dir1);
            //}
            //if (dir2 != '\0')
            //{
            //    var cordt = GetCordt(dir2);

            //    var d_i = i + cordt.row;
            //    var d_j = j + cordt.col;

            //    travel(d_i, d_j, dir2);
            //}
        }

        List<long> inputs;
        public Solutions()
        {
            inputs = File.ReadAllLines(@".\input17_test.txt").Select(x=>long.Parse(x)).ToList();
            map = new int[inputs.Count, inputs[0].ToString().Length];
        }
        public void First()
        {
            var row = 0;
            var col = 0;
            travel(row, col, 'E');
            var total_e = FindTotal();

          
            Console.WriteLine(total_e);

            //var dir = 'S';
            //var x = 0;
            //var y = 3;

            //travel(x, y, dir);
        }

        public void Secound()
        {
        }
    }

}

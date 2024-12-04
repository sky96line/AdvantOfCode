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

namespace AdvantOfCode._2023.Day16
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

        char[,] map;

        public int FindTotal()
        {
            var total = 0;
            for (int i = 0; i < inputs.Count; i++)
            {
                for (int j = 0; j < inputs[0].Length; j++)
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
            map = new char[inputs.Count, inputs[0].Length];
            
            Console.WriteLine();
            Console.WriteLine();

            return total;
        }

        public void travel(int i, int j, char dir)
        {
            if(i < 0 || i > inputs[0].Length - 1 || j < 0 || j > inputs.Count - 1 || lookup.Exists(x=>x.Equals($"[{i},{j},{dir}]"))) return;
            //if (i < 0 || i > inputs[0].Length - 1 || j < 0 || j > inputs.Count - 1) return;

            // Console.WriteLine($"[{i},{j}] {inputs[i][j]}");

            map[i, j] = '#';
            lookup.Add($"[{i},{j},{dir}]");

            var (dir1, dir2) = GetDir(inputs[i][j], dir);

            if (dir1 != '\0')
            {
                var cordt = GetCordt(dir1);

                var d_i = i + cordt.row;
                var d_j = j + cordt.col;

                travel(d_i, d_j, dir1);
            }
            if (dir2 != '\0')
            {
                var cordt = GetCordt(dir2);

                var d_i = i + cordt.row;
                var d_j = j + cordt.col;
                
                travel(d_i, d_j, dir2);
            }
        }

        List<string> inputs;
        public Solutions()
        {
            inputs = File.ReadAllLines(@"C:\Users\skyli\source\repos\AdvantOfCode2023\AdvantOfCode2023\Day16\input16.txt").ToList();
            map = new char[inputs.Count, inputs[0].Length];
        }
        public void First()
        {
            var max = 0;
            for (int row = 0; row < inputs.Count; row++)
            {
                for (int col = 0; col < inputs[0].Length; col++)
                {
                    if (row == 0 && col == 0)
                    {
                        Console.WriteLine($"Working for: [{row}, {col}]");

                        travel(row, col, 'E');
                        var total_e = FindTotal();

                        travel(row, col, 'S');
                        var total_s = FindTotal();
                        
                        var local_max = total_e > total_s ? total_e : total_s;

                        if (local_max > max)
                            max = local_max;

                        File.AppendAllText("./output.txt", $"[{row}, {col}] = {local_max} | max: [{max}]\n");
                    }

                    else if (row == 0 && col == inputs[0].Length - 1)
                    {
                        Console.WriteLine($"Working for: [{row}, {col}]");

                        travel(row, col, 'W');
                        var total_w = FindTotal();

                        travel(row, col, 'S');
                        var total_s = FindTotal();

                        var local_max = total_w > total_s ? total_w : total_s;

                        if (local_max > max)
                            max = local_max;

                        File.AppendAllText("./output.txt", $"[{row}, {col}] = {local_max} | max: [{max}]\n");
                    }

                    else if (row == inputs.Count - 1 && col == 0)
                    {
                        Console.WriteLine($"Working for: [{row}, {col}]");

                        travel(row, col, 'E');
                        var total_e = FindTotal();

                        travel(row, col, 'N');
                        var total_n = FindTotal();

                        var local_max = total_e > total_n ? total_e : total_n;

                        if (local_max > max)
                            max = local_max;

                        File.AppendAllText("./output.txt", $"[{row}, {col}] = {local_max} | max: [{max}]\n");
                    }

                    else if (row == inputs.Count - 1 && col == inputs[0].Length - 1)
                    {
                        Console.WriteLine($"Working for: [{row}, {col}]");

                        travel(row, col, 'W');
                        var total_w = FindTotal();

                        travel(row, col, 'N');
                        var total_n = FindTotal();

                        var local_max = total_w > total_n ? total_w : total_n;

                        if (local_max > max)
                            max = local_max;

                        File.AppendAllText("./output.txt", $"[{row}, {col}] = {local_max} | max: [{max}]\n");
                    }
                    else
                    {
                        if (row == 0)
                        {
                            Console.WriteLine($"Working for: [{row}, {col}]");

                            travel(row, col, 'S');
                            var local_max = FindTotal();

                            if (local_max > max)
                                max = local_max;

                            File.AppendAllText("./output.txt", $"[{row}, {col}] = {local_max} | max: [{max}]\n");
                        }
                        else if (col == 0)
                        {
                            Console.WriteLine($"Working for: [{row}, {col}]");

                            travel(row, col, 'E');
                            var local_max = FindTotal();

                            if (local_max > max)
                                max = local_max;

                            File.AppendAllText("./output.txt", $"[{row}, {col}] = {local_max} | max: [{max}]\n");
                        }
                        else if (col == inputs[0].Length - 1)
                        {
                            Console.WriteLine($"Working for: [{row}, {col}]");

                            travel(row, col, 'W');
                            var local_max = FindTotal();

                            if (local_max > max)
                                max = local_max;

                            File.AppendAllText("./output.txt", $"[{row}, {col}] = {local_max} | max: [{max}]\n");
                        }
                        else if (row == inputs.Count - 1)
                        {
                            Console.WriteLine($"Working for: [{row}, {col}]");

                            travel(row, col, 'N');
                            var local_max = FindTotal();

                            if (local_max > max)
                                max = local_max;

                            File.AppendAllText("./output.txt", $"[{row}, {col}] = {local_max} | max: [{max}]\n");
                        }
                    }

                }
            }

            Console.WriteLine(max);

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

using AdvantOfCode._2023.Day19;
using AdvantOfCode._2024.Day8;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AdvantOfCode._2024.Day12
{
    public class Solutions
    {
       
        char[,] input;

        private List<(int i, int j)> GetUp(int i, int j)
        {
            var c = input[i, j];

            List<(int i, int j)> result = new();

            for (int x = i - 1; x >= 0; x--)
            {
                if (input[x, j] != c) break;
                result.Add((x, j));
            }

            return result;
        }

        private List<(int i, int j)> GetRight(int i, int j)
        {
            var c = input[i, j];

            List<(int i, int j)> result = new();

            for (int x = j + 1; x < input.GetLength(0); x++)
            {
                if (input[i, x] != c) break;
                result.Add((i, x));
            }

            return result;
        }

        private List<(int i, int j)> GetDown(int i, int j)
        {
            var c = input[i, j];

            List<(int i, int j)> result = new();

            for (int x = i + 1; x < input.GetLength(1) ; x++)
            {
                if (input[x, j] != c) break;
                result.Add((x, j));
            }

            return result;
        }

        private List<(int i, int j)> GetLeft(int i, int j)
        {
            var c = input[i, j];

            List<(int i, int j)> result = new();

            for (int x = j - 1; x >= 0; x--)
            {
                if (input[i, x] != c) break;
                result.Add((i, x));
            }

            return result;
        }

        public class Node
        {
            public char Name { get; set; }
            public int i { get; set; }
            public int j { get; set; }
        }

       
        public void First()
        {
            var text_inputs = File.ReadAllLines(@"C:\Users\akash.buch\source\repos\AdvantOfCode2023\AdvantOfCode\2024\Day12\input.txt");

            var row = text_inputs.Length;
            var col = text_inputs[0].Count();

            input = new char[row, col];
            
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    input[i, j] = text_inputs[i][j];
                }
            }

            var output = 0;
            List<(char d, int i, int j)> all_visited = new();
            
            for (int x = 0; x < row; x++)
            {
                for (int y = 0; y < col; y++)
                {
                    if (all_visited.Any(f => f.i == x && f.j == y))
                    {
                        continue;
                    }

                    List<(char d, int i, int j)> visited = new();
                    List<(int i, int j)> paths = new();
                    paths.Add((x, y));

                    while (true)
                    {
                        List<(int i, int j)> in_paths = new();

                        foreach (var path in paths)
                        {
                            if (!visited.Contains(('U', path.i, path.j)))
                            {
                                visited.Add(('U', path.i, path.j));

                                var result = GetUp(path.i, path.j);
                                in_paths.AddRange(result);

                                foreach (var res in result)
                                {
                                    visited.Add(('U', res.i, res.j));
                                }
                            }

                            if (!visited.Contains(('R', path.i, path.j)))
                            {
                                visited.Add(('R', path.i, path.j));

                                var result = GetRight(path.i, path.j);
                                in_paths.AddRange(result);

                                foreach (var res in result)
                                {
                                    visited.Add(('R', res.i, res.j));
                                }
                            }

                            if (!visited.Contains(('D', path.i, path.j)))
                            {
                                visited.Add(('D', path.i, path.j));

                                var result = GetDown(path.i, path.j);
                                in_paths.AddRange(result);

                                foreach (var res in result)
                                {
                                    visited.Add(('D', res.i, res.j));
                                }
                            }

                            if (!visited.Contains(('L', path.i, path.j)))
                            {
                                visited.Add(('L', path.i, path.j));

                                var result = GetLeft(path.i, path.j);
                                in_paths.AddRange(result);

                                foreach (var res in result)
                                {
                                    visited.Add(('L', res.i, res.j));
                                }
                            }
                        }

                        if (in_paths.Count == 0) break;
                        paths = new(in_paths);
                    }

                    all_visited.AddRange(visited);

                    List<(int i, int j)> dist = new();
                    foreach (var v in visited)
                    {
                        if (!dist.Contains((v.i, v.j)))
                        {
                            dist.Add((v.i, v.j));
                        }
                    }
                    
                    var min_i = dist.Min(x => x.i);
                    var min_j = dist.Min(x => x.j);

                    var max_i = dist.Max(x => x.i);
                    var max_j = dist.Max(x => x.j);

                    var count = 0;
                    //top
                    for (int i = min_i; i <= max_i; i++)
                    {
                        var flag = false;
                        for (int j = min_j; j <= max_j; j++)
                        {
                            if (dist.Contains((i, j)) && !dist.Contains((i - 1, j)))
                            {
                                if (flag == false)
                                {
                                    flag = true;
                                    count++;
                                }
                            }
                            else
                            {
                                flag = false;
                            }
                        }
                    }



                    //right
                    for (int j = min_j; j <= max_j; j++)
                    {
                        var flag = false;
                        for (int i = min_i; i <= max_i; i++)
                        {
                            if (dist.Contains((i, j)) && !dist.Contains((i, j - 1)))
                            {
                                if (flag == false)
                                {
                                    flag = true;
                                    count++;
                                }
                            }
                            else
                            {
                                flag = false;
                            }
                        }
                    }

                    //left
                    for (int j = max_j; j >= min_j; j--)
                    {
                        var flag = false;
                        for (int i = min_i; i <= max_i; i++)
                        {
                            if (dist.Contains((i, j)) && !dist.Contains((i, j + 1)))
                            {
                                if (flag == false)
                                {
                                    flag = true;
                                    count++;
                                }
                            }
                            else
                            {
                                flag = false;
                            }
                        }
                    }

                    //down
                    for (int i = max_i; i >= min_i; i--)
                    {
                        var flag = false;
                        for (int j = min_j; j <= max_j; j++)
                        {
                            if (dist.Contains((i, j)) && !dist.Contains((i + 1, j)))
                            {
                                if (flag == false)
                                {
                                    flag = true;
                                    count++;
                                }
                            }
                            else
                            {
                                flag = false;
                            }
                        }
                    }

                    output += (dist.Count * count);
                    Console.WriteLine($"{dist.Count} * {count}");

                    // 0,0      0,3
                    // 1,0      1,3
                    // 2,2      2,4
                    // 3,2
                    

                }
            }

            Console.WriteLine(output);
           
        }
    }
}

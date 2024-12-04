using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdvantOfCode._2024.Day4
{
    public class Solutions
    {
        public void First()
        {
            var list_inputs = File.ReadAllLines(@".\input.txt");
            var output = 0;

            var row = list_inputs.Length;
            var col = list_inputs[0].Count();

            List<(int i, int j)> points = new List<(int i, int j)>();

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    //top
                    if (0 <= i - 3 && string.Concat(list_inputs[i][j], list_inputs[i - 1][j], list_inputs[i - 2][j], list_inputs[i - 3][j]).Equals("XMAS"))
                    {
                        output++;
                        points.Add((i, j));
                        points.Add((i - 1, j));
                        points.Add((i - 2, j));
                        points.Add((i - 3, j));
                    }

                    //top-right
                    if (0 <= i - 3 && col > j + 3 && string.Concat(list_inputs[i][j], list_inputs[i - 1][j + 1], list_inputs[i - 2][j + 2], list_inputs[i - 3][j + 3]).Equals("XMAS"))
                    {
                        output++;
                        points.Add((i, j));
                        points.Add((i - 1, j + 1));
                        points.Add((i - 2, j + 2));
                        points.Add((i - 3, j + 3));
                    }

                    //right
                    if (col > j + 3 && string.Concat(list_inputs[i][j], list_inputs[i][j + 1], list_inputs[i][j + 2], list_inputs[i][j + 3]).Equals("XMAS"))
                    {
                        output++;
                        points.Add((i, j));
                        points.Add((i, j + 1));
                        points.Add((i, j + 2));
                        points.Add((i, j + 3));
                    }

                    //righ-bottom
                    if (row > i + 3 && col > j + 3 && string.Concat(list_inputs[i][j], list_inputs[i + 1][j + 1], list_inputs[i + 2][j + 2], list_inputs[i + 3][j + 3]).Equals("XMAS"))
                    {
                        output++;
                        points.Add((i, j));
                        points.Add((i + 1, j + 1));
                        points.Add((i + 2, j + 2));
                        points.Add((i + 3, j + 3));
                    }

                    //bottom
                    if (row > i + 3 && string.Concat(list_inputs[i][j], list_inputs[i + 1][j], list_inputs[i + 2][j], list_inputs[i + 3][j]).Equals("XMAS"))
                    {
                        output++;
                        points.Add((i, j));
                        points.Add((i + 1, j));
                        points.Add((i + 2, j));
                        points.Add((i + 3, j));
                    }

                    //bottom-left
                    if (row > i + 3 && 0 <= j - 3 && string.Concat(list_inputs[i][j], list_inputs[i + 1][j - 1], list_inputs[i + 2][j - 2], list_inputs[i + 3][j - 3]).Equals("XMAS"))
                    {
                        output++;
                        points.Add((i, j));
                        points.Add((i + 1, j - 1));
                        points.Add((i + 2, j - 2));
                        points.Add((i + 3, j - 3));
                    }

                    //left
                    if (0 <= j - 3 && string.Concat(list_inputs[i][j], list_inputs[i][j - 1], list_inputs[i][j - 2], list_inputs[i][j - 3]).Equals("XMAS"))
                    {
                        output++;
                        points.Add((i, j));
                        points.Add((i, j - 1));
                        points.Add((i, j - 2));
                        points.Add((i, j - 3));
                    }

                    //left-top
                    if (0 <= i - 3 && 0 <= j - 3 && string.Concat(list_inputs[i][j], list_inputs[i - 1][j - 1], list_inputs[i - 2][j - 2], list_inputs[i - 3][j - 3]).Equals("XMAS"))
                    {
                        output++;
                        points.Add((i, j));
                        points.Add((i - 1, j - 1));
                        points.Add((i - 2, j - 2));
                        points.Add((i - 3, j - 3));
                    }
                }
                Console.WriteLine("");
            }


            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (points.Contains((i, j)))
                    {
                        Console.Write(list_inputs[i][j]);
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine("");
            }

            Console.WriteLine(output);
        }

        public void Secound()
        {
            var s = AppDomain.CurrentDomain.BaseDirectory;

            var list_inputs = File.ReadAllLines(@".\input.txt");
            var output = 0;

            var row = list_inputs.Length;
            var col = list_inputs[0].Count();

            List<(int i, int j)> points = new List<(int i, int j)>();

            for (int i = 1; i < row-1; i++)
            {
                for (int j = 1; j < col-1; j++)
                {

                    if (
                        (string.Concat(list_inputs[i - 1][j - 1], list_inputs[i][j], list_inputs[i + 1][j + 1]).Equals("MAS") ||
                        string.Concat(list_inputs[i - 1][j - 1], list_inputs[i][j], list_inputs[i + 1][j + 1]).Equals("SAM")) &&
                        (string.Concat(list_inputs[i - 1][j + 1], list_inputs[i][j], list_inputs[i + 1][j - 1]).Equals("MAS") ||
                        string.Concat(list_inputs[i - 1][j + 1], list_inputs[i][j], list_inputs[i + 1][j - 1]).Equals("SAM"))
                        )
                    {
                        output++;
                        
                        points.Add((i - 1, j - 1));
                        points.Add((i - 1, j + 1));
                        points.Add((i, j));
                        points.Add((i + 1, j + 1));
                        points.Add((i + 1, j - 1));
                    }

                    ////top
                    //if (0 <= i - 3 && string.Concat(list_inputs[i][j], list_inputs[i - 1][j], list_inputs[i - 2][j], list_inputs[i - 3][j]).Equals("XMAS"))
                    //{
                    //    output++;
                    //    points.Add((i, j));
                    //    points.Add((i - 1, j));
                    //    points.Add((i - 2, j));
                    //    points.Add((i - 3, j));
                    //}

                    ////top-right
                    //if (0 <= i - 3 && col > j + 3 && string.Concat(list_inputs[i][j], list_inputs[i - 1][j + 1], list_inputs[i - 2][j + 2], list_inputs[i - 3][j + 3]).Equals("XMAS"))
                    //{
                    //    output++;
                    //    points.Add((i, j));
                    //    points.Add((i - 1, j + 1));
                    //    points.Add((i - 2, j + 2));
                    //    points.Add((i - 3, j + 3));
                    //}

                    ////right
                    //if (col > j + 3 && string.Concat(list_inputs[i][j], list_inputs[i][j + 1], list_inputs[i][j + 2], list_inputs[i][j + 3]).Equals("XMAS"))
                    //{
                    //    output++;
                    //    points.Add((i, j));
                    //    points.Add((i, j + 1));
                    //    points.Add((i, j + 2));
                    //    points.Add((i, j + 3));
                    //}

                    ////righ-bottom
                    //if (row > i + 3 && col > j + 3 && string.Concat(list_inputs[i][j], list_inputs[i + 1][j + 1], list_inputs[i + 2][j + 2], list_inputs[i + 3][j + 3]).Equals("XMAS"))
                    //{
                    //    output++;
                    //    points.Add((i, j));
                    //    points.Add((i + 1, j + 1));
                    //    points.Add((i + 2, j + 2));
                    //    points.Add((i + 3, j + 3));
                    //}

                    ////bottom
                    //if (row > i + 3 && string.Concat(list_inputs[i][j], list_inputs[i + 1][j], list_inputs[i + 2][j], list_inputs[i + 3][j]).Equals("XMAS"))
                    //{
                    //    output++;
                    //    points.Add((i, j));
                    //    points.Add((i + 1, j));
                    //    points.Add((i + 2, j));
                    //    points.Add((i + 3, j));
                    //}

                    ////bottom-left
                    //if (row > i + 3 && 0 <= j - 3 && string.Concat(list_inputs[i][j], list_inputs[i + 1][j - 1], list_inputs[i + 2][j - 2], list_inputs[i + 3][j - 3]).Equals("XMAS"))
                    //{
                    //    output++;
                    //    points.Add((i, j));
                    //    points.Add((i + 1, j - 1));
                    //    points.Add((i + 2, j - 2));
                    //    points.Add((i + 3, j - 3));
                    //}

                    ////left
                    //if (0 <= j - 3 && string.Concat(list_inputs[i][j], list_inputs[i][j - 1], list_inputs[i][j - 2], list_inputs[i][j - 3]).Equals("XMAS"))
                    //{
                    //    output++;
                    //    points.Add((i, j));
                    //    points.Add((i, j - 1));
                    //    points.Add((i, j - 2));
                    //    points.Add((i, j - 3));
                    //}

                    ////left-top
                    //if (0 <= i - 3 && 0 <= j - 3 && string.Concat(list_inputs[i][j], list_inputs[i - 1][j - 1], list_inputs[i - 2][j - 2], list_inputs[i - 3][j - 3]).Equals("XMAS"))
                    //{
                    //    output++;
                    //    points.Add((i, j));
                    //    points.Add((i - 1, j - 1));
                    //    points.Add((i - 2, j - 2));
                    //    points.Add((i - 3, j - 3));
                    //}
                }
                Console.WriteLine("");
            }


            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (points.Contains((i, j)))
                    {
                        Console.Write(list_inputs[i][j]);
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine("");
            }

            Console.WriteLine(output);
        }
    }
}

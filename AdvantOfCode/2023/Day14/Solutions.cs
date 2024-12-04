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

namespace AdvantOfCode._2023.Day14
{

    public class Solutions
    {
        public int CalcLoad(int row, int col, char[,] chars)
        {
            var total = 0;

            for (int i = 0; i < row; i++)
            {
                int t = 0;
                for (int j = 0; j < col; j++)
                {
                    if (chars[i, j] == 'O') t++;
                }
                var v = ((row - i) * t);
                total += v;
            }

            return total;
        }

        public char[,] Copy(char[,] a, int r, int c)
        {
            char[,] cc = new char[r, c];
            Array.Copy(a, cc, r * c);

            return cc;
        }

        public string Flat(char[,] a)
        {
            var c = "";
            foreach (var aa in a.Cast<char>())
            {
                c += aa;
            }
            return c;
        }

        public char[,] FlatToArray(string a, int row, int col)
        {
            char[,] c = new char[row, col];

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    c[i, j] = a[i * col + j];
                }
            }

            return c;
        }

        public void First()
        {
            var inputs = File.ReadAllLines(@".\input14.txt").ToList();

            char[,] chars = new char[inputs.Count, inputs[0].Length];

            for (int i = 0; i < inputs.Count; i++)
            {
                for (int j = 0; j < inputs[0].Length; j++)
                {
                    chars[i,j] = inputs[i][j];
                }
            }

            var row = inputs.Count;
            var col = inputs[0].Length;

            var cycle = 1000000000;
            int count = 0;
            Dictionary<int, string> map = new();

            int repeat_one = 0;
            int repeat_two = 0;

            for (count = 1; count <= cycle; count++)
            {
                

                #region Loop

                //North (Down to Up)
                var flagN = true;
                while (flagN)
                {
                    flagN = false;
                    for (int i = 0; i < row - 1; i++) // Row 
                    {
                        for (int j = 0; j < col; j++) // Col
                        {
                            if (chars[i, j] == '.' && chars[i + 1, j] == 'O')
                            {
                                chars[i, j] = 'O';
                                chars[i + 1, j] = '.';
                                flagN = true;
                            }
                        }
                    }
                }


                //West(Left to Right)
                var flagW = true;
                while (flagW)
                {
                    flagW = false;
                    for (int j = 0; j < col - 1; j++)
                    {
                        for (int i = 0; i < row; i++) //Col
                        {

                            if (chars[i, j] == '.' && chars[i, j + 1] == 'O')
                            {
                                chars[i, j] = 'O';
                                chars[i, j + 1] = '.';
                                flagW = true;
                            }
                        }
                    }
                }

                //South (Up to Down)
                var flagS = true;
                while (flagS)
                {
                    flagS = false;
                    for (int i = row - 1; i > 0; i--) // Row 
                    {
                        for (int j = col - 1; j >= 0; j--) // Col
                        {
                            if (chars[i, j] == '.' && chars[i - 1, j] == 'O')
                            {
                                chars[i, j] = 'O';
                                chars[i - 1, j] = '.';
                                flagS = true;
                            }
                        }
                    }
                }

                //East (Right to Left )
                var flagE = true;
                while (flagE)
                {
                    flagE = false;
                    for (int j = col - 1; j > 0; j--)
                    {
                        for (int i = row - 1; i >= 0; i--) //Col
                        {

                            if (chars[i, j] == '.' && chars[i, j - 1] == 'O')
                            {
                                chars[i, j] = 'O';
                                chars[i, j - 1] = '.';
                                flagE = true;
                            }
                        }
                    }
                }

                #endregion

                var cx = CalcLoad(inputs.Count, inputs[0].Length, chars);
                Console.WriteLine($"Key: {count} | {cx}");
                //for (int i = 0; i < inputs.Count; i++)
                //{
                //    for (int j = 0; j < inputs[0].Length; j++)
                //    {
                //        Console.Write(chars[i, j]);
                //    }
                //    Console.WriteLine();
                //}

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();



                var flat = Flat(chars);
                if (map.ContainsValue(flat))
                {
                    var kkk = map.FirstOrDefault(x => x.Value.Equals(flat));
                    repeat_one = kkk.Key;
                    repeat_two = count;
                    break;
                }
                else
                {
                    //var ccc = Copy(chars, inputs.Count, inputs[0].Length);
                    map.Add(count, flat);
                }
            }




            var index = cycle % (repeat_two - repeat_one);

            //var r = (repeat_two - repeat_one) - index;
            //index = index;
            chars = FlatToArray(map[index], inputs.Count, inputs[0].Length);


            for (int i = 0; i < inputs.Count; i++)
            {
                for (int j = 0; j < inputs[0].Length; j++)
                {
                    Console.Write(chars[i, j]);
                }
                Console.WriteLine();
            }

            var c = CalcLoad(inputs.Count, inputs[0].Length, chars);

            Console.WriteLine($"{c} | {index}");

        }

      



        public void Secound()
        {
            var inputs = File.ReadAllLines(@".\input14_test.txt").ToList();            
        }
    }

}

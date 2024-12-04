using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

namespace AdvantOfCode._2023.Day18
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

        class DigPlan
        {
            public char Dir { get; set; }
            public int Number { get; set; }
            public string Color { get; set; }
        }

        List<DigPlan> data = new();
        List<List<char>> result = new();



        List<double[]> vertices = new();

        public Solutions()
        {
            var inputs = File.ReadAllLines(@".\input18_test.txt").ToList();

            foreach (var input in inputs)
            {
                DigPlan dp = new DigPlan();
                dp.Dir = input.Split(" ").First()[0];
                dp.Number = int.Parse(input.Split(" ").Skip(1).First());
                dp.Color = input.Split(" ").Last();

                data.Add(dp);
            }

        }
        public void First()
        {
            var r = data.Where(x => x.Dir == 'R').Select(x => x.Number).Sum();
            var l = data.Where(x => x.Dir == 'L').Select(x => x.Number).Sum();

            var u = data.Where(x => x.Dir == 'U').Select(x => x.Number).Sum();
            var d = data.Where(x => x.Dir == 'D').Select(x => x.Number).Sum();

            var row = r > l ? r : l;
            var col = u > d ? u : d;

            for (int x = 0; x < row * 2; x++)
            {
                List<char> result_1 = new List<char>();
                for (int y = 0; y < col * 2; y++)
                {
                    result_1.Add('.');
                }

                result.Add(result_1);
                //Console.WriteLine();
            }

            var i = row / 2;
            var j = col / 2;

            //var i = 0;
            //var j = 0;

            //vertices.Add(new double[] { 0, 0 });
            //vertices.Add(new double[] { 0, 10 });
            //vertices.Add(new double[] { 3, 10 });
            //vertices.Add(new double[] { 3, 0 });
            //vertices.Add(new double[] { 0, 0 });


            foreach (var dd in data)
            {
                int x = 0;
                for (x = 0; x < dd.Number; x++)
                {
                    if (dd.Dir == 'R') result[i][j+x] = '#';
                    else if (dd.Dir == 'D') result[i + x][j] = '#';
                    else if (dd.Dir == 'L') result[i][j - x] = '#';
                    else if (dd.Dir == 'U') result[i - x][j] = '#';
                }

                if (dd.Dir == 'R')
                {
                    j += x;
                }
                else if (dd.Dir == 'D')
                {
                    i += x;

                }
                else if (dd.Dir == 'L')
                {
                    j -= x;
                }
                else if (dd.Dir == 'U')
                {
                    i -= x;
                }

                vertices.Add(new double[]{ i, j });
            }


            var in_dex = 0;
            for (int x = 0; x < row * 2; x++)
            {
                for (int y = 0; y < col * 2; y++)
                {
                    foreach (var item in vertices)
                    {
                        if (item[0] == x && item[1] == y)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        }
                    }
                    if (result[x][y] == '#') Console.Write(result[x][y]);
                    else Console.Write('.');

                    Console.ForegroundColor = ConsoleColor.White;

                    //if(chars[x, y] == '#') Console.Write(chars[x, y]);
                    //else Console.Write(' ');

                }
                Console.WriteLine();
            }

            var area = CalculatePolygonArea(vertices);

            Console.WriteLine(area);
        }

        double CalculatePolygonArea(List<double[]> vertices)
        {
            int n = vertices.Count;

            if (n < 3)
            {
                throw new ArgumentException("Invalid number of vertices for a polygon.");
            }

            double sum = 0;

            for (int i = 0; i < n - 1; i++)
            {
                sum += (vertices[i][0] * vertices[i + 1][1] - vertices[i + 1][0] * vertices[i][1]);
            }

            // Add the last term
            sum += (vertices[n - 1][0] * vertices[0][1] - vertices[0][0] * vertices[n - 1][1]);

            // Take the absolute value and divide by 2
            return Math.Abs(sum) / 2.0;
        }

        
        public void Secound()
        {
        }
    }

}

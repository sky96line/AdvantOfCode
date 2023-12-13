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

namespace AdvantOfCode2023.Day11
{

    public class Solutions
    {
        private class Result
        {
            public Point Source { get; set; }
            public Point Destination { get; set; }
            public int Steps { get; set; }
        }

        private class Point
        {
            public int row, col, index;
        }

        public void First()
        {
            var inputs = File.ReadAllLines(@"C:\Users\akash.buch\source\repos\AdvantOfCode2023\AdvantOfCode2023\Day11\input11.txt").ToList();

            List<Point> points= new List<Point>();
            List<Result> results = new List<Result>();

            var row = 0;
            var col = 0;
            var ind = 1;

            var fill_row = new List<int>();
            var fill_col = new List<int>();

            for (int i = 0; i < inputs.Count(); i++)
            {
                fill_row.Add(i);
            }

            for (int i = 0; i < inputs[0].Count(); i++)
            {
                fill_col.Add(i);
            }

            foreach (var input in inputs)
            {
                col = 0;
                foreach (var ch in input)
                {
                    if(ch == '#')
                    {
                        points.Add(new Point() { row = row, col = col, index = ind });

                        if (fill_row.Contains(row))
                        {
                            fill_row.Remove(row);
                        }
                        if (fill_col.Contains(col))
                        {
                            fill_col.Remove(col);
                        }

                        ind++;
                    }
                    
                    col++;
                }
                row++;
            }

            


            foreach (var source in points)
            {
                foreach (var destination in points)
                {
                    if (source.index == 1 && destination.index == 6)
                    {

                    }
                    var min_r = source.row > destination.row ? destination.row : source.row;
                    var max_r = source.row <= destination.row ? destination.row : source.row;

                    var min_c = source.col > destination.col ? destination.col : source.col;
                    var max_c = source.col <= destination.col ? destination.col : source.col;

                    var r = fill_row.Where(x => x > min_r && x < max_r).Count();
                    var c = fill_col.Where(x => x > min_c && x < max_c).Count();

                    Result result1 = new Result();
                    result1.Source = source;
                    result1.Destination = destination;

                    if(r > 0)
                    {
                        result1.Steps += (max_r - min_r - r) + (r * 1000000);
                    }
                    else
                    {
                        result1.Steps += (max_r - min_r);
                    }


                    if (c > 0)
                    {
                        result1.Steps += (max_c - min_c - c) + (c * 1000000);
                    }
                    else
                    {
                        result1.Steps += (max_c - min_c);
                    }


                    if (source.row < destination.row)
                    {
                        results.Add(result1);
                    }
                    else if (source.row == destination.row && source.col < destination.col)
                    {
                        results.Add(result1);
                    }
                }
            }

            //var result = results.Sum(x => x.Steps);

            long total = 0;
            foreach (var res in results)
            {
                total += res.Steps;
                //Console.WriteLine($"[{res.Source.index}, {res.Destination.index}] : {res.Steps}");
            }
            //Utility.PrintList(results.Select(x=>x.Steps));

            Console.WriteLine(total);
        }

        public void Secound()
        {
            var inputs = File.ReadAllLines(@"C:\Users\akash.buch\source\repos\AdvantOfCode2023\AdvantOfCode2023\Day10\input10_test.txt").ToList();            
        }
    }

}

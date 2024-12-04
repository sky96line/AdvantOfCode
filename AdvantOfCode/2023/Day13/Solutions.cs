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

namespace AdvantOfCode._2023.Day13
{

    public class Solutions
    {
        public string GetStringFromIndex(string[] str, int index)
        {
            StringBuilder sb = new StringBuilder();
            
            foreach (string s in str)
            {
                sb.Append(s[index]);
            }

            //Console.WriteLine($"{index} | {sb.ToString()}");
            return sb.ToString();
        }

        public (bool IsSame, bool SmugedFix) CompareString(string str1, string str2, bool smudgFixed)
        {
            var fixedSmudge = smudgFixed;
            if (fixedSmudge)
            {
                return (str1.Equals(str2), fixedSmudge);
            }
            else
            {
                foreach (var item in Enumerable.Zip(str1, str2))
                {
                    if (fixedSmudge)
                    {
                        if (!item.First.Equals(item.Second))
                        {
                            return (false, fixedSmudge);
                        }
                    }
                    else
                    {
                        if (!item.First.Equals(item.Second))
                        {
                            fixedSmudge = true;
                        }
                    }
                }

                return (true, fixedSmudge);
            }
        }

        public int GameCalculate(string str)
        {
            //top to down
            var game = str.Split("\r\n");

            // find mirror using x = x+1
            
            
            int i = 0;

           
            td:
            var fix_Smudge = false;
            var mirror_top = i;
            var mirror_bottom = i+1;
            var IsReflaction = false;
            var refcount = 0;
            while ((mirror_top - refcount) >= 0 && (mirror_bottom + refcount) < game.Count() && mirror_bottom > 0)
            {
                var chk = CompareString(game[mirror_top - refcount], game[mirror_bottom + refcount], fix_Smudge);
                if (chk.IsSame)
                {
                    if(!fix_Smudge)
                        fix_Smudge = chk.SmugedFix;

                    if (((mirror_top - refcount) == 0 || (mirror_bottom + refcount) == game.Count() - 1))
                    {
                        if (fix_Smudge)
                        {
                            IsReflaction = true;
                            break;
                        }
                        else
                        {
                            i++;
                            goto td;
                        }
                    }
                }
                else
                {
                    i++;
                    goto td;
                }
                refcount++;
            }


            if (IsReflaction)
            {
                return (mirror_top + 1) * 100;
            }


            i = 0;

            lr:
            fix_Smudge = false;
            var mirror_left = i;
            var mirror_right = i + 1;
            refcount = 0;
            while ((mirror_left - refcount) >= 0 && (mirror_right + refcount) < game[0].Length && mirror_right > 0)
            {
                var chk = CompareString(GetStringFromIndex(game, mirror_left - refcount), GetStringFromIndex(game, mirror_right + refcount), fix_Smudge);
                if (chk.IsSame)
                {
                    if (!fix_Smudge)
                        fix_Smudge = chk.SmugedFix;
                    
                    if (((mirror_left - refcount) == 0 || (mirror_right + refcount) == game[0].Length - 1))
                    {
                        if (fix_Smudge)
                        {
                            IsReflaction = true;
                            break;
                        }
                        else
                        {
                            i++;
                            goto lr;
                        }
                    }
                }
                else
                {
                    i++;
                    goto lr;
                    //break;
                }
                refcount++;
            }


            if (IsReflaction)
            {
                return (mirror_left + 1);
            }

            return 0;
        }

        private IEnumerable<string> maps;

        private (bool didFind, int number) FindMirror(IEnumerable<int> rows, int ignore = -2)
        {
            var rowsAndIndex = rows.Select((row, index) => (row, index));
            var zippedRows = rowsAndIndex.Zip(rowsAndIndex.Skip(1), (a, b) => ((a.row, a.index), (b.row, b.index)));
            foreach (var zippedRow in zippedRows)
            {
                if (zippedRow.Item1.row == zippedRow.Item2.row)
                {
                    var part1 = rows.Take(zippedRow.Item1.index + 1).ToArray();
                    var part2 = rows.Skip(zippedRow.Item1.index + 1).ToArray();
                    var mirrored = part1.Reverse().Zip(part2);
                    var isMirrored = mirrored.All(pair => pair.First == pair.Second);
                    if (isMirrored && (zippedRow.Item1.index + 1) != ignore)
                    {
                        return (true, zippedRow.Item1.index + 1);
                    }
                }
            }

            return (false, -1);
        }

        private IEnumerable<int> RowsToNumbers(string map)
        {
            var rows = map.Split("\n");
            foreach (var row in rows)
            {
                var bits = row.Replace("#", "1").Replace(".", "0");
                yield return Convert.ToInt32(bits, 2);
            }
        }

        private IEnumerable<int> ColumnsToNumbers(string map)
        {
            var rows = map.Split("\n");
            for (int c = 0; c < rows[0].Length; c++) // loop over the columns
            {
                var column = rows.Select(row => row[c]);
                var bits = string.Join("", column).Replace("#", "1").Replace(".", "0");
                yield return Convert.ToInt32(bits, 2);
            }
        }

        public void First()
        {
            var inputs = File.ReadAllText(@"C:\Users\skyli\source\repos\AdvantOfCode2023\AdvantOfCode2023\Day13\input13.txt");

            var games = inputs.Split("\r\n\r\n");

            var total = 0;

            int i = 1;
            foreach (var game in games)
            {
                if (i == 35)
                {

                }
                var t = GameCalculate(game);
                total += t;
                Console.WriteLine($"Count: {i++} | {t}");
            }

            Console.WriteLine(total);
        }

        public int First_solution()
        {
            var inputs = File.ReadAllText(@"C:\Users\skyli\source\repos\AdvantOfCode2023\AdvantOfCode2023\Day13\input13.txt");

            maps = inputs.Trim().Split("\r\n\r\n");

            var sum = 0;
            var count = 1;
            foreach (var map in maps)
            {
                var rows = RowsToNumbers(map);
                var horizontal = FindMirror(rows);
                if (horizontal.didFind)
                {
                    Console.WriteLine($"Count: {count++} | {horizontal.number * 100}");
                    sum += horizontal.number * 100;
                    continue;
                }

                var columns = ColumnsToNumbers(map);
                var vertical = FindMirror(columns);
                if (vertical.didFind)
                {
                    Console.WriteLine($"Count: {count++} | {vertical.number}");
                    sum += vertical.number;
                    continue;
                }
            }
            return sum;
        }

        public int Secound_solution()
        {
            var inputs = File.ReadAllText(@"C:\Users\skyli\source\repos\AdvantOfCode2023\AdvantOfCode2023\Day13\input13.txt").Replace("\r\n", "\n");

            maps = inputs.Trim().Split("\n\n");



            var sum = 0;
            var count = 1;

            foreach (var originalMap in maps)
            {
                var orgHorizontal = FindMirror(RowsToNumbers(originalMap));
                var orgVertical = FindMirror(ColumnsToNumbers(originalMap));

                var list = Unsmudged(originalMap).ToList();
                foreach (var map in list)
                {
                    var rows = RowsToNumbers(map).ToList();
                    var horizontal = FindMirror(rows, orgHorizontal.number);
                    if (horizontal.didFind)
                    {
                        Console.WriteLine($"Count: {count++} | {horizontal.number * 100}");

                        sum += horizontal.number * 100;
                        break;
                    }

                    var columns = ColumnsToNumbers(map);
                    var vertical = FindMirror(columns, orgVertical.number);
                    if (vertical.didFind)
                    {
                        Console.WriteLine($"Count: {count++} | {vertical.number}");

                        sum += vertical.number;
                        break;
                    }
                }
            }

            Console.WriteLine(sum);
            return sum;
        }
        IEnumerable<string> Unsmudged(string map)
        {
            var rows = map.Split("\n").Length;
            var columns = map.Split("\n")[0].Length;

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    StringBuilder sb = new StringBuilder(map);
                    int pos = r * (columns + 1) + c;
                    var current = map[pos];
                    sb[pos] = current == '#' ? '.' : '#';
                    yield return string.Join("\n", sb.ToString());
                }
            }
        }

        public void Secound()
        {
            var inputs = File.ReadAllLines(@"C:\Users\akash.buch\source\repos\AdvantOfCode2023\AdvantOfCode2023\Day10\input10_test.txt").ToList();
        }
    }

}

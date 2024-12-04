using Newtonsoft.Json;
using System.Data.Common;

namespace AdvantOfCode._2023.Day3
{
    public class Solutions
    {
        private bool IsSymbol(char c)
        {
            if(Char.IsDigit(c))
                return false;

            return c == '.' ? false : true;
        }

        enum SymanticType
        {
            Digit,
            Symbol
        }

        private class Point
        {
            public int r, c;

            public Point(int row, int col)
            {
                r = row; c = col;
            }
        }

        private class KVP
        {
            public SymanticType type { get; set; }
            public string key { get; set; }
            public List<Point> indexes { get; set; } = new List<Point>();

            public KVP(SymanticType type, string key)
            {
                this.type = type;
                this.key = key;
            }

            public void AddPoint(Point point)
            {
                this.indexes.Add(point);
            }

            public void AddAllPoint(List<Point> points)
            {
                this.indexes.AddRange(points);
            }
        }

        public void First()
        {
            var inputs = File.ReadAllLines(@".\Input3.txt");

            List<KVP> kvps = new List<KVP>();

            var digit = "";
            List<Point> points = new List<Point>();

            int row = 0;
            foreach (var input in inputs)
            {
                int col = 0;
                foreach (var c in input)
                {
                    if (char.IsDigit(c))
                    {
                        digit += c;
                        Point point = new Point(row, col);
                        points.Add(point);
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(digit))
                        {
                            KVP kvp = new KVP(SymanticType.Digit,digit);
                            kvp.AddAllPoint(points);
                            
                            kvps.Add(kvp);

                            digit = "";
                            points = new List<Point>();
                        }
                        
                        if (IsSymbol(c))
                        {
                            Point point = new Point(row, col);
                            
                            KVP kvpSymobl = new KVP(SymanticType.Symbol, c.ToString());
                            kvpSymobl.AddPoint(point);

                            kvps.Add(kvpSymobl);
                        }
                    }
                    col++;
                }
                row++;
            }

            if (!string.IsNullOrWhiteSpace(digit))
            {
                KVP kvp = new KVP(SymanticType.Digit, digit);
                kvp.AddAllPoint(points);

                kvps.Add(kvp);
            }

            //PrintKVP(kvps);
            //return;

            var kvpsSymbol = kvps.Where(x => x.type == SymanticType.Symbol);

            List<KVP> result = new List<KVP>();

            foreach (var kvp in kvpsSymbol)
            {
                var r = kvp.indexes[0].r;
                var c = kvp.indexes[0].c;
                
                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        if (i == 0 && j == 0)
                        {
                            continue;
                        }
                            
                        var key = GetKey(r + i, c + j, kvps);

                        if (key is not null)
                        {
                            result.Add(key);
                        }
                    }
                }
            }

            result = result.Distinct().ToList();

            var total = result.Select(x => int.Parse(x.key)).Sum();

            Console.WriteLine("===============");
            Console.WriteLine(total);
        }

        private KVP? GetKey(int row, int col, List<KVP> kvps)
        {
            return kvps.FirstOrDefault(x => x.indexes.Any(x=>x.r == row) && x.indexes.Any(x => x.c == col));
        }

        private void PrintKVP(List<KVP> kvps)
        {
            Console.WriteLine(JsonConvert.SerializeObject(kvps, Formatting.Indented));
        }



        public void Secound()
        {
            var inputs = File.ReadAllLines(@".\Input3.txt");

            List<KVP> kvps = new List<KVP>();

            var digit = "";
            List<Point> points = new List<Point>();

            int row = 0;
            foreach (var input in inputs)
            {
                int col = 0;
                foreach (var c in input)
                {
                    if (char.IsDigit(c))
                    {
                        digit += c;
                        Point point = new Point(row, col);
                        points.Add(point);
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(digit))
                        {
                            KVP kvp = new KVP(SymanticType.Digit, digit);
                            kvp.AddAllPoint(points);

                            kvps.Add(kvp);

                            digit = "";
                            points = new List<Point>();
                        }

                        if (IsSymbol(c) && c == '*')
                        {
                            Point point = new Point(row, col);

                            KVP kvpSymobl = new KVP(SymanticType.Symbol, c.ToString());
                            kvpSymobl.AddPoint(point);

                            kvps.Add(kvpSymobl);
                        }
                    }
                    col++;
                }
                row++;
            }

            if (!string.IsNullOrWhiteSpace(digit))
            {
                KVP kvp = new KVP(SymanticType.Digit, digit);
                kvp.AddAllPoint(points);

                kvps.Add(kvp);
            }

            //PrintKVP(kvps);
            //return;
            var total = 0;


            var kvpsSymbol = kvps.Where(x => x.type == SymanticType.Symbol);

            List<KVP> result = new List<KVP>();

            foreach (var kvp in kvpsSymbol)
            {
                var mul = 1;
                var r = kvp.indexes[0].r;
                var c = kvp.indexes[0].c;

                int isCount = 0;

                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        if (i == 0 && j == 0)
                        {
                            continue;
                        }

                        var key = GetKey(r + i, c + j, kvps);

                        if (key is not null && !result.Contains(key))
                        {
                            isCount++;
                            result.Add(key);
                            mul *= int.Parse(key.key);
                        }
                    }
                }

                if(isCount>1)
                    total += mul;
            }

            Console.WriteLine("===============");
            Console.WriteLine(total);
        }

    }
}

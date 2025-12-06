namespace AdvantOfCode._2025.Day2
{
    public class Solutions
    {
        public void First()
        {
            var inputs = File.ReadAllText(@"C:\\Users\\akash.buch\\source\\repos\\AdvantOfCode2023\\AdvantOfCode\\2025\\Day2\\input.txt");

            var datas = inputs.Split(",");
            var invalid = new List<long>();

            foreach (var data in datas)
            {
                var from = long.Parse(data.Split("-").First());
                var to = long.Parse(data.Split("-").Last());

                for (long i = from; i <= to; i++)
                {
                    var len = i.ToString().Length;
                    if ((len % 2) != 0) continue;

                    // 121121
                    // 6
                    // 3
                    var left = i.ToString().Substring(0, (len / 2));
                    var right = i.ToString().Substring((len / 2), (len / 2));

                    if (left.Equals(right))
                    {
                        invalid.Add(i);
                    }
                }
            }

            Console.WriteLine(invalid.Sum());
        }

        private List<long> GetParts(long input)
        {
            var len = input.ToString().Length;
            //if ((len % 2) != 0)
            //{
            //    return new List<long>();
            //}

            var parts = new List<long>();
            for (int i = 1; i <= (len / 2); i++)
            {
                var part = input.ToString().Substring(0, i);
                parts.Add(long.Parse(part));
            }

            return parts;
        }

        private long GetString(long str, int len)
        {
            var s = "";
            for (int i = 0; i < len; i++)
            {
                s += str.ToString();
            }
            return long.Parse(s);
        }

        public void Secound()
        {
            var inputs = File.ReadAllText(@"C:\\Users\\akash.buch\\source\\repos\\AdvantOfCode2023\\AdvantOfCode\\2025\\Day2\\input.txt");

            var datas = inputs.Split(",");
            var invalid = new List<long>();

            foreach (var data in datas)
            {
                var from = long.Parse(data.Split("-").First());
                var to = long.Parse(data.Split("-").Last());

                var result = new List<long>();
                for (long i = from; i <= to; i++)
                {
                    var len = i.ToString().Length;
                    //if ((len % 2) != 0) continue;

                    var parts = GetParts(i);

                    // 92916254
                    // 9
                    // 92
                    // 929
                    // 9291

                    var inval = false;
                    foreach (var part in parts)
                    {
                        var val = GetString(part, len / part.ToString().Length);

                        if (val == i)
                        {
                            inval = true;
                            break;
                        }

                    }

                    if (inval)
                    {
                        invalid.Add(i);
                    }
                }
            }

            foreach (var item in invalid)
            {
                Console.WriteLine($" => {item}");
            }

            Console.WriteLine(invalid.Sum());
        }
    }
}

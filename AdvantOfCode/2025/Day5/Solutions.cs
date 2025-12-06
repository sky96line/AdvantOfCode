namespace AdvantOfCode._2025.Day5
{
    public class Solutions
    {
        List<(long from, long to)> ranges = new();
        List<long> inputs = new();
        List<(long from, long to)> freshIds = new();

        void GenerateInput(string[] txt)
        {

            ranges = txt
                .Where(x => x.Contains("-"))
                .Select(x => (long.Parse(x.Split("-").First()), long.Parse(x.Split("-").Last())))
                .ToList();

            inputs = txt
                .Where(x => !string.IsNullOrWhiteSpace(x) && !x.Contains("-"))
                .Select(x => (long.Parse(x)))
                .ToList();
        }

        void Print()
        {
        }

        bool InRange(long input)
        {
            return ranges.Any(x => x.from <= input && x.to >= input);
        }

        public void First()
        {
            var result = 0;
            var txt = File.ReadAllLines(@"C:\\Users\\akash.buch\\source\\repos\\AdvantOfCode2023\\AdvantOfCode\\2025\\Day5\\input.txt");

            GenerateInput(txt);

            foreach (var input in inputs)
            {
                var in_range = InRange(input);
                if (in_range) result++;
            }

            Console.WriteLine(result);
        }

        public (long f, long t)? SubRange((long from, long to) range)
        {
            if (range.from > range.to)
                return null;

            // __X___R___R___X__
            if (freshIds.Any(x => x.from <= range.from && range.to <= x.to))
            {
                return null;
            }
            // __R___X___R___X__
            else if (freshIds.Any(x => x.from <= range.to && range.to <= x.to))
            {
                var x = freshIds.Where(x => x.from <= range.to && range.to <= x.to).OrderBy(x => x.from).FirstOrDefault();
                return SubRange((range.from, x.from - 1));
            }
            // __X___R___X___R__
            else if (freshIds.Any(x => x.from <= range.from && range.from <= x.to))
            {
                var x = freshIds.Where(x => x.from <= range.from && range.from <= x.to).OrderByDescending(x => x.to).FirstOrDefault();
                return SubRange((x.to + 1, range.to));
            }
            // __R___X___X___R__
            else if (freshIds.Any(x => range.from < x.from && x.to < range.to))
            {
                var xs = freshIds.Where(x => range.from <= x.from && x.to <= range.to).OrderBy(x => x.from).ToList();

                foreach (var x in xs)
                {
                    freshIds.Remove((x.from, x.to));
                }

                return SubRange((range.from, range.to));
            }
            return (range.from, range.to);
        }

        public void Secound()
        {
            long result = 0;
            var inputs = File.ReadAllLines(@"C:\\Users\\akash.buch\\source\\repos\\AdvantOfCode2023\\AdvantOfCode\\2025\\Day5\\input.txt");

            GenerateInput(inputs);

            foreach (var range in ranges)
            {
                Console.WriteLine($"Working on {range.from} - {range.to}");

                var subId = SubRange(range);
                if (!subId.HasValue)
                    continue;

                freshIds.Add(subId.Value);
            }

            foreach (var freshId in freshIds)
            {
                Console.WriteLine($"{freshId.from} - {freshId.to}");
                result += (freshId.to - freshId.from) + 1;
            }
            Console.WriteLine(result);
        }
    }
}

namespace AdvantOfCode._2025.Day3
{
    public class Solutions
    {
        public void First()
        {
            var result = 0;
            var inputs = File.ReadAllLines(@"C:\\Users\\akash.buch\\source\\repos\\AdvantOfCode2023\\AdvantOfCode\\2025\\Day3\\input.txt");

            foreach (var input in inputs)
            {
                var max = -1;
                var maxIndex = -1;
                for (int i = 0; i < input.Length - 1; i++)
                {
                    Console.Write(input[i].ToString());

                    if (int.Parse(input[i].ToString()) > max)
                    {
                        max = int.Parse(input[i].ToString());
                        maxIndex = i;
                    }
                }
                Console.WriteLine("");

                var s_max = -1;
                var s_maxIndex = -1;
                for (int i = maxIndex + 1; i < input.Length; i++)
                {
                    Console.Write(input[i].ToString());

                    if (int.Parse(input[i].ToString()) > s_max)
                    {
                        s_max = int.Parse(input[i].ToString());
                        s_maxIndex = i;
                    }
                }
                Console.WriteLine("");

                var maxVal = $"{input[maxIndex]}{input[s_maxIndex]}";
                Console.WriteLine($"{maxVal}");
                result += int.Parse(maxVal);
            }

            Console.WriteLine(result);
        }

        private (int num, int index) FindMax(string input, int previousMaxIndex, int rotation)
        {
            var max = -1;
            var maxIndex = -1;
            for (int i = previousMaxIndex + 1; i < input.Length - (12 - rotation); i++)
            {
                if (int.Parse(input[i].ToString()) > max)
                {
                    max = int.Parse(input[i].ToString());
                    maxIndex = i;
                }
            }

            return (max, maxIndex);
        }

        public void Secound()
        {
            long result = 0;
            var inputs = File.ReadAllLines(@"C:\\Users\\akash.buch\\source\\repos\\AdvantOfCode2023\\AdvantOfCode\\2025\\Day3\\input.txt");

            foreach (var input in inputs)
            {
                string ans = "";
                var previousMaxIndex = -1;
                for (int i = 1; i <= 12; i++)
                {
                    (var num, var index) = FindMax(input, previousMaxIndex, i);
                    ans += num;
                    previousMaxIndex = index;
                }

                result += long.Parse(ans);
            }

            Console.WriteLine(result);
        }
    }
}

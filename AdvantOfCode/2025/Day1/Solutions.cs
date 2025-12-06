namespace AdvantOfCode._2025.Day1
{
    public class Solutions
    {
        public void First()
        {
            var inputs = File.ReadAllLines(@"C:\\Users\\akash.buch\\source\\repos\\AdvantOfCode2023\\AdvantOfCode\\2025\\Day1\\input.txt");
            var dial = 50;
            var output = 0;
            foreach (var input in inputs)
            {
                var dir = input[0];
                var value = int.Parse(input[1..]);

                value = value % 100;

                dial = dir == 'L' ? dial - value : dial + value;

                if (dial < 0) dial += 100;
                else if (dial > 99) dial -= 100;

                Console.WriteLine(dial);
                if (dial == 0) output++;
            }

            Console.WriteLine(output);
        }

        public void Secound()
        {
            var inputs = File.ReadAllLines(@"C:\\Users\\akash.buch\\source\\repos\\AdvantOfCode2023\\AdvantOfCode\\2025\\Day1\\input.txt");
            var dial = 50;
            var output = 0;
            var p_dial = 50;
            foreach (var input in inputs)
            {
                var dir = input[0];
                var o_value = int.Parse(input[1..]);

                if (dir == 'R' && o_value == 632)
                {

                }

                var circle = o_value / 100;
                var value = o_value % 100;

                dial = dir == 'L' ? dial - value : dial + value;

                if (dial < 0)
                {
                    dial += 100;
                    if (p_dial != 0)
                        output++;
                }
                else if (dial > 99)
                {
                    dial -= 100;
                    if (p_dial != 0)
                        output++;
                }
                else if (dial == 0)
                {
                    output++;
                }
                output += circle;
                p_dial = dial;
                Console.WriteLine($"{dir} | {o_value} => Circle: {circle} | Value: {value} | Dial: {dial} | Result: {output}");
            }

            Console.WriteLine(output);
        }
    }
}

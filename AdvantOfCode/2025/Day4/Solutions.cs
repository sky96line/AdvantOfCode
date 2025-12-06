namespace AdvantOfCode._2025.Day4
{
    public class Solutions
    {
        int rows = 0;
        int cols = 0;
        char[,] wall;

        void GenerateInput(string[] inputs)
        {
            rows = inputs.Count();
            cols = inputs[0].Length;

            wall = new char[rows, cols];

            int row = 0;
            foreach (var input in inputs)
            {
                for (int col = 0; col < input.Length; col++)
                {
                    wall[row, col] = input[col];
                }
                row++;
            }
        }

        void Print()
        {
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    Console.Write(wall[row, col]);
                }
                Console.WriteLine();
            }
        }

        bool IsAccessable(int row, int col)
        {
            if (wall[row, col] != '@') return false;

            List<(int r, int c)> dirs = new();
            dirs.Add((0 - 1, 0 - 1));
            dirs.Add((0 - 1, 0));
            dirs.Add((0 - 1, 0 + 1));

            dirs.Add((0, 0 - 1));
            dirs.Add((0, 0 + 1));

            dirs.Add((0 + 1, 0 - 1));
            dirs.Add((0 + 1, 0));
            dirs.Add((0 + 1, 0 + 1));

            var count = 0;
            foreach (var dir in dirs)
            {
                var r = row + dir.r;
                var c = col + dir.c;

                if (r < 0 || r > (rows - 1) || c < 0 || c > (cols - 1)) continue;

                if (wall[r, c] == '@') count++;
            }

            return count < 4;
        }

        public void First()
        {
            var result = 0;
            var inputs = File.ReadAllLines(@"C:\\Users\\akash.buch\\source\\repos\\AdvantOfCode2023\\AdvantOfCode\\2025\\Day4\\input.txt");

            GenerateInput(inputs);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    var access = IsAccessable(i, j);
                    if (access)
                    {
                        Console.Write('X');
                        result++;
                    }
                    else
                    {
                        Console.Write(wall[i, j]);
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine(result);
        }

        public void Secound()
        {
            var result = 0;
            var inputs = File.ReadAllLines(@"C:\\Users\\akash.buch\\source\\repos\\AdvantOfCode2023\\AdvantOfCode\\2025\\Day4\\input.txt");

            GenerateInput(inputs);

            var run_again = false;

            do
            {
                var sub = 0;
                List<(int i, int j)> replace = new();
                run_again = false;

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        var access = IsAccessable(i, j);
                        if (access)
                        {
                            replace.Add((i, j));
                            run_again = true;
                            sub++;
                        }
                    }
                }

                foreach (var rplc in replace)
                {
                    wall[rplc.i, rplc.j] = '.';
                }

                Console.WriteLine($" => {sub}");
                result += sub;

            } while (run_again);

            Console.WriteLine(result);
        }
    }
}

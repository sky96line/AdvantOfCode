namespace AdvantOfCode._2024.Day8
{
  
    class Point
    {
        public int x { get; set; }
        public int y { get; set; }
    }

    class Antena
    {
        public Char Name { get; set; }
        public Point Location { get; set; }
    }

    

    public class Solutions
    {
        private List<Antena> GetAdjantAntena(Antena searchingAntena, List<Antena> allAntenas)
        {
            return allAntenas.Where(x => x.Name == searchingAntena.Name && x.Location.x > searchingAntena.Location.x).ToList();
        }

        public void First()
        {
            List<Antena> antenas= new List<Antena>();
            var text_inputs = File.ReadAllLines(@"C:\Users\akash.buch\source\repos\AdvantOfCode2023\AdvantOfCode\2024\Day8\input.txt");
            long output = 0;

            var row = text_inputs.Length;
            var col = text_inputs[0].Count();

            var input = new char[row, col];

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    input[i, j] = text_inputs[i][j];

                    if (char.IsLetterOrDigit(text_inputs[i][j]))
                    {
                        antenas.Add(new() { Name = text_inputs[i][j], Location = new() { x = i, y = j } });
                    }
                }
            }

            List<Point> points= new List<Point>();

            foreach (var antena in antenas)
            {
                var adjestAntenas = GetAdjantAntena(antena, antenas);

                foreach (var adj in adjestAntenas)
                {
                    var off_x = antena.Location.x - adj.Location.x;
                    var off_y = antena.Location.y - adj.Location.y;

                    // 3 - 5 = -2
                    // 4 - 5 = -1

                    var adj_x_1 = antena.Location.x + off_x;
                    var adj_y_1 = antena.Location.y + off_y;

                    while (adj_x_1 >= 0 && adj_x_1 < row && adj_y_1 >= 0 && adj_y_1 < col)
                    {
                        if (!points.Any(p => p.x == adj_x_1 && p.y == adj_y_1))
                        {
                            points.Add(new() { x = adj_x_1, y = adj_y_1 });

                            Console.WriteLine($"{adj.Name} ({adj_x_1}, {adj_y_1})");
                        }

                        adj_x_1 += off_x;
                        adj_y_1 += off_y;
                    }

                    off_x = off_x * -1;
                    off_y = off_y * -1;

                    var adj_x_2 = adj.Location.x + off_x;
                    var adj_y_2 = adj.Location.y + off_y;

                    while (adj_x_2 >= 0 && adj_x_2 < row && adj_y_2 >= 0 && adj_y_2 < col)
                    {
                        if (!points.Any(p => p.x == adj_x_2 && p.y == adj_y_2))
                        {
                            points.Add(new() { x = adj_x_2, y = adj_y_2 });

                            Console.WriteLine($"{adj.Name} ({adj_x_2}, {adj_y_2})");
                        }

                        adj_x_2 += off_x;
                        adj_y_2 += off_y;
                    }
                }
            }

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (points.Any(x => x.x == i && x.y == j))
                    {
                        if (char.IsLetterOrDigit(input[i, j]))
                        {
                            Console.Write(input[i, j]);
                        }
                        else
                        {
                            Console.Write("#");
                        }
                        
                        output++;
                    }
                    else
                    {
                        if (char.IsLetterOrDigit(input[i,j]))
                        {
                            Console.Write(input[i, j]);
                            output++;
                        }
                        else
                        {
                            Console.Write(".");
                        }
                    }
                }
                Console.WriteLine("");
            }

            Console.WriteLine(output);
        }

        public void Secound()
        {
            var txt_inputs = File.ReadAllLines(@"C:\Users\akash.buch\source\repos\AdvantOfCode2023\AdvantOfCode\2024\Day7\input.txt");
            var output = 0;

            Console.WriteLine(output);
        }
    }
}

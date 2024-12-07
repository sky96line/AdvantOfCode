namespace AdvantOfCode._2024.Day6
{
    public class Solutions
    {
        private (int new_dir_i, int new_dir_j) TurnRight(int dir_i , int dir_j)
        {
            if (dir_i == -1 && dir_j == 0) return (0, 1);
            else if (dir_i == 0 && dir_j == 1) return (1, 0);
            else if (dir_i == 1 && dir_j == 0) return (0, -1);
            else return (-1, 0);
        }

        
        public void First()
        {
            var txt_inputs = File.ReadAllLines(@"C:\Users\akash.buch\source\repos\AdvantOfCode2023\AdvantOfCode\2024\Day6\input.txt");
            var output = 0;

            var row = txt_inputs.Length;
            var col = txt_inputs[0].Count();

            var input = new char[row, col];

            var pos_i = 0;
            var pos_j = 0;
            
            var dir_i = 0;
            var dir_j = 0;

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    input[i,j] = txt_inputs[i][j];
                    
                    if (txt_inputs[i][j] != '#' && txt_inputs[i][j] != '.')
                    {
                        pos_i = i;
                        pos_j = j;

                        if (txt_inputs[i][j] == '^') { dir_i = -1; dir_j = 0; }
                        else if (txt_inputs[i][j] == '>') { dir_i = 0; dir_j = 1; }
                        else if (txt_inputs[i][j] == 'v') { dir_i = 1; dir_j = 0; }
                        else if (txt_inputs[i][j] == '<') { dir_i = 0; dir_j = -1; }

                        input[i, j] = '.';
                    }
                }
            }

            while (pos_i > 0 && pos_i < row - 1 && pos_j > 0 && pos_j < col - 1)
            {
                input[pos_i, pos_j] = 'X';

                var next_check = input[pos_i + dir_i, pos_j + dir_j];
                if (next_check == '#')
                {
                    (dir_i, dir_j) = TurnRight(dir_i, dir_j);
                }
                
                pos_i = pos_i + dir_i;
                pos_j = pos_j + dir_j;
            }

            input[pos_i, pos_j] = 'X';



            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (input[i, j] == 'X')
                        output++;

                    Console.Write(input[i, j]);
                }
                Console.WriteLine("");
            }

            Console.WriteLine(output);
        }

        public void Secound()
        {
            var txt_inputs = File.ReadAllLines(@"C:\Users\akash.buch\source\repos\AdvantOfCode2023\AdvantOfCode\2024\Day6\input.txt");
            var output = 0;

            var row = txt_inputs.Length;
            var col = txt_inputs[0].Count();

            var input = new char[row, col];

            var pos_i = 0;
            var pos_j = 0;

            var dir_i = 0;
            var dir_j = 0;

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    input[i, j] = txt_inputs[i][j];

                    if (txt_inputs[i][j] != '#' && txt_inputs[i][j] != '.')
                    {
                        pos_i = i;
                        pos_j = j;

                        if (txt_inputs[i][j] == '^') { dir_i = -1; dir_j = 0; }
                        else if (txt_inputs[i][j] == '>') { dir_i = 0; dir_j = 1; }
                        else if (txt_inputs[i][j] == 'v') { dir_i = 1; dir_j = 0; }
                        else if (txt_inputs[i][j] == '<') { dir_i = 0; dir_j = -1; }

                        input[i, j] = '.';
                    }
                }
            }



            for (int i = 1; i < row -1; i++)
            {
                for (int j = 1; j < col -1; j++)
                {
                    //Up
                    var up_i = -1;
                    var up_j = -1;
                    for (int x = i; x >= 0; x--)
                    {
                        if (input[x, j - 1] == '#')
                        {
                            up_i = x;
                            up_j = j - 1;
                            break;
                        }
                    }


                    //Right
                    var right_i = -1;
                    var right_j = -1;
                    for (int x = up_j; x < col; x++)
                    {
                        if (input[up_i+1, x] == '#')
                        {
                            right_i = up_i + 1;
                            right_j = x;
                            break;
                        }
                    }


                    if (input[up_i + 1, right_j + 1] == '#')
                    {

                    }
                }
            }

            while (pos_i > 0 && pos_i < row - 1 && pos_j > 0 && pos_j < col - 1)
            {
                input[pos_i, pos_j] = 'X';

                var next_check = input[pos_i + dir_i, pos_j + dir_j];
                if (next_check == '#')
                {
                    (dir_i, dir_j) = TurnRight(dir_i, dir_j);
                }

                pos_i = pos_i + dir_i;
                pos_j = pos_j + dir_j;
            }

            input[pos_i, pos_j] = 'X';



            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (input[i, j] == 'X')
                        output++;

                    Console.Write(input[i, j]);
                }
                Console.WriteLine("");
            }

            Console.WriteLine(output);
        }
    }
}

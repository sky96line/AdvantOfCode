using AdvantOfCode._2023.Day19;
using AdvantOfCode._2024.Day8;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AdvantOfCode._2024.Day10
{

    public class Solutions
    {
        int[,] input;
        List<(int, int)> trailheads;
        int row;
        int col;

        public Solutions()
        {
            var text_inputs = File.ReadAllLines(@"C:\Users\akash.buch\source\repos\AdvantOfCode2023\AdvantOfCode\2024\Day10\input.txt");

            row = text_inputs.Length;
            col = text_inputs[0].Count();

            input = new int[row, col];
            trailheads = new List<(int, int)>();

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    input[i, j] = Convert.ToInt32(text_inputs[i][j].ToString());
                    if (input[i, j] == 0)
                    {
                        trailheads.Add((i, j));
                    }
                }
            }
        }
        
        private void Print(int[,] ints)
        {
            for (int i = 0; i < ints.GetLength(0); i++)
            {
                for (int j = 0; j < ints.GetLength(1); j++)
                {
                    Console.Write(ints[i, j]);
                }
                Console.WriteLine("");
            }
        }
        
        private int DFS(int r, int c, bool[,] visited, string dir, int found)
        {
            if (r < 0 || r >= row || c < 0 || c >= col || visited[r, c])
            {
                return found;
            }

            //Console.WriteLine($"{input[r, c]} = ({r}, {c}) = {dir}");
            // Mark current cell as visited
            visited[r, c] = true;

            if (input[r, c] == 9)
            {
                found += 1;
                return found;
            }

            try
            {
                if (input[r, c] + 1 == input[r - 1, c])
                {
                    found = DFS(r - 1, c, visited, "UP", found); // Up
                }
            }
            catch (Exception)
            {

            }

            try
            {
                if (input[r, c] + 1 == input[r, c + 1])
                {
                    found = DFS(r, c + 1, visited, "RIGHT", found); // Right
                }
            }
            catch (Exception)
            {

            }

            try
            {
                if (input[r, c] + 1 == input[r + 1, c])
                {
                    found = DFS(r + 1, c, visited, "DOWN", found); // Down
                }
            }
            catch (Exception)
            {

            }

            try
            {
                if (input[r, c] + 1 == input[r, c - 1])
                {
                    found = DFS(r, c - 1, visited, "LEFT", found); // Left
                }
            }
            catch (Exception)
            {

            }

            

            return found;
        }

        private bool TakeStep(char direction, int i, int j)
        {
            var curr = input[i, j];
            Console.WriteLine($"{curr} = ({i}, {j})");
            var flag = false;
            if (curr == 9)
            {
                return true;
            }

            if (!flag && i == 0 && j == 0)
            {
                if (!flag && direction != 'L' && input[i, j + 1] == curr + 1)
                {
                    flag = TakeStep('R', i, j + 1); 
                }
                if (!flag && direction != 'U' && input[i + 1, j] == curr + 1)
                { 
                    flag = TakeStep('D', i + 1, j); 
                }
            }
            else if (!flag && i == 0 && j == col - 1)
            {
                if (!flag && direction != 'U' && input[i + 1, j] == curr + 1)
                { 
                    flag = TakeStep('D', i + 1, j); 
                }
                if (!flag && direction != 'R' && input[i, j - 1] == curr + 1)
                { 
                    flag = TakeStep('L', i, j - 1); 
                }
            }
            else if (!flag && i == row - 1 && j == 0)
            {
                if (!flag && direction != 'D' && input[i - 1, j] == curr + 1)
                { 
                    flag = TakeStep('U', i - 1, j); 
                }
                if (!flag && direction != 'L' && input[i, j + 1] == curr + 1)
                { 
                    flag = TakeStep('R', i, j + 1); 
                }
            }
            else if (!flag && i == row - 1 && j == col - 1)
            {
                if (!flag && direction != 'R' && input[i, j - 1] == curr + 1)
                { 
                    flag = TakeStep('L', i, j - 1); 
                }
                if (!flag && direction != 'D' && input[i - 1, j] == curr + 1)
                { 
                    flag = TakeStep('U', i - 1, j); 
                }
            }
            else if (!flag && i == 0)
            {
                if (!flag && direction != 'L' && input[i, j + 1] == curr + 1)
                { 
                    flag = TakeStep('R', i, j + 1); 
                }
                if (!flag && direction != 'U' && input[i + 1, j] == curr + 1)
                { 
                    flag = TakeStep('D', i + 1, j); 
                }
                if (!flag && direction != 'R' && input[i, j - 1] == curr + 1)
                { 
                    flag = TakeStep('L', i, j - 1); 
                }
            }
            else if (!flag && i == row - 1)
            {
                if (!flag && direction != 'D' && input[i - 1, j] == curr + 1)
                { 
                    flag = TakeStep('U', i - 1, j); 
                }
                if (!flag && direction != 'L' && input[i, j + 1] == curr + 1)
                { 
                    flag = TakeStep('R', i, j + 1); 
                }
                if (!flag && direction != 'R' && input[i, j - 1] == curr + 1)
                { 
                    flag = TakeStep('L', i, j - 1); 
                }
            }
            else if (!flag && j == 0)
            {
                if (!flag && direction != 'D' && input[i - 1, j] == curr + 1)
                { 
                    flag = TakeStep('U', i - 1, j); 
                }
                if (!flag && direction != 'L' && input[i, j + 1] == curr + 1)
                { 
                    flag = TakeStep('R', i, j + 1); 
                }
                if (!flag && direction != 'U' && input[i + 1, j] == curr + 1)
                { 
                    flag = TakeStep('D', i + 1, j); 
                }
            }
            else if (!flag && j == col - 1)
            {
                if (!flag && direction != 'D' && input[i - 1, j] == curr + 1)
                { 
                    flag = TakeStep('U', i - 1, j); 
                }
                if (!flag && direction != 'U' && input[i + 1, j] == curr + 1)
                { 
                    flag = TakeStep('D', i + 1, j); 
                }
                if (!flag && direction != 'R' && input[i, j - 1] == curr + 1)
                { 
                    flag = TakeStep('L', i, j - 1); 
                }
            }
            else
            {
                if (!flag && direction != 'D' && input[i - 1, j] == curr + 1)
                { 
                    flag = TakeStep('U', i - 1, j); 
                }
                if (!flag && direction != 'L' && input[i, j + 1] == curr + 1)
                { 
                    flag = TakeStep('R', i, j + 1); 
                }
                if (!flag && direction != 'U' && input[i + 1, j] == curr + 1)
                { 
                    flag = TakeStep('D', i + 1, j); 
                }
                if (!flag && direction != 'R' && input[i, j - 1] == curr + 1)
                { 
                    flag = TakeStep('L', i, j - 1); 
                }
            }

            return flag;
        }

        public void First()
        {
            var output = 0;
            foreach (var trail in trailheads)
            {
                try
                {
                    bool[,] visited = new bool[row, col];
                    var f = DFS(trail.Item1, trail.Item2, visited, "", 0);
                    output += f;
                }
                catch (Exception ex)
                {

                }   
            }

            Console.WriteLine(output);
        }
    }
}

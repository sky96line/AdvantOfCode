using AdvantOfCode._2023.Day19;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;

namespace AdvantOfCode._2024.Day16
{
    public class Solutions
    {
        

        static readonly int[] dX = { 0, 1, 0, -1 }; // Directions: East, South, West, North
        static readonly int[] dY = { 1, 0, -1, 0 };
        static readonly string[] directions = { "East", "South", "West", "North" };

        static void SolveMaze(char[,] maze)
        {
            int startX = 13, startY = 1; // Starting position (row 13, column 1)
            int endX = 1, endY = 13;    // End position (row 1, column 13)

            var queue = new Queue<(int x, int y, int direction, int score, List<string> path)>();
            queue.Enqueue((startX, startY, 0, 0, new List<string>()));

            var visited = new HashSet<(int, int, int)>();
            visited.Add((startX, startY, 0));

            while (queue.Count > 0)
            {
                var (x, y, dir, score, path) = queue.Dequeue();

                // If we reach the end, print the solution
                if (x == endX && y == endY)
                {
                    Console.WriteLine($"Path found with score: {score}");
                    foreach (var step in path)
                    {
                        Console.WriteLine(step);
                    }
                    return;
                }

                // Move forward
                int newX = x + dX[dir];
                int newY = y + dY[dir];
                if (maze[newX, newY] != '#' && !visited.Contains((newX, newY, dir)))
                {
                    var newPath = new List<string>(path) { $"Move forward to ({newX}, {newY})" };
                    queue.Enqueue((newX, newY, dir, score + 1, newPath));
                    visited.Add((newX, newY, dir));
                }

                // Rotate clockwise
                int newDir = (dir + 1) % 4;
                if (!visited.Contains((x, y, newDir)))
                {
                    var newPath = new List<string>(path) { $"Rotate clockwise to face {directions[newDir]}" };
                    queue.Enqueue((x, y, newDir, score + 1000, newPath));
                    visited.Add((x, y, newDir));
                }

                // Rotate counterclockwise
                newDir = (dir + 3) % 4;
                if (!visited.Contains((x, y, newDir)))
                {
                    var newPath = new List<string>(path) { $"Rotate counterclockwise to face {directions[newDir]}" };
                    queue.Enqueue((x, y, newDir, score + 1000, newPath));
                    visited.Add((x, y, newDir));
                }
            }

            Console.WriteLine("No path found.");
        }


        public void Print(char[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j]);
                }
                Console.WriteLine("");
            }

            Console.WriteLine("");
            Console.WriteLine("");
        }

        public void First()
        {
            List<(int i, int j)> visited = new();

            var input = File.ReadAllLines(@"C:\Users\akash.buch\source\repos\AdvantOfCode2023\AdvantOfCode\2024\Day16\input.txt");

            var row = input.Length;
            var col = input[0].Count();

            char[,] map = new char[row, col];

            (int i, int j) robot = (0,0);

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    map[i, j] = input[i][j];
                    if (map[i, j] == 'S')
                        robot = (i, j);
                }
            }

            Print(map);

            SolveMaze(map);
        }
    }
}

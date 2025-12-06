using AdvantOfCode._2023.Day19;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;

namespace AdvantOfCode._2024.Day15
{
    public class Solutions
    {
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
            var txt = File.ReadAllText(@"C:\Users\akash.buch\source\repos\AdvantOfCode2023\AdvantOfCode\2024\Day15\input.txt");

            var input = txt.Split("\r\n\r\n").First().Split("\r\n");
            var instruction = txt.Split("\r\n\r\n").Last();

            var row = input.Length;
            var col = input[0].Count();

            var map = new char[row, col];

            (int i, int j) robot = (0,0);

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    map[i, j] = input[i][j];
                    if (map[i, j] == '@')
                    {
                        robot = (i, j);
                    }
                }
            }

            Print(map);

            for (int x = 0; x < instruction.Length; x++)
            {
                char dir = instruction[x];
                Console.WriteLine("Dir: "+ dir);

                if (dir == '<')
                {
                    var doMove = true;
                    var moveIndex = -1;

                    for (int j = robot.j - 1; j >= 0; j--)
                    {
                        if (map[robot.i, j] == '#')
                        {
                            doMove = false;
                            moveIndex = -1;
                        }
                        else if (map[robot.i, j] == '.')
                        {
                            moveIndex = j;
                            break;
                        }
                    }

                    if (doMove)
                    {
                        for (int j = moveIndex; j < robot.j; j++)
                        {
                            var swap = map[robot.i, j + 1];
                            map[robot.i, j + 1] = map[robot.i, j];
                            map[robot.i, j] = swap;

                            if (swap == '@')
                            {
                                robot = (robot.i, j);
                            }
                        }
                    }
                }

                else if (dir == '>')
                {
                    var doMove = true;
                    var moveIndex = -1;

                    for (int j = robot.j + 1; j < map.GetLength(1); j++)
                    {
                        if (map[robot.i, j] == '#')
                        {
                            doMove = false;
                            moveIndex = -1;
                        }
                        else if (map[robot.i, j] == '.')
                        {
                            moveIndex = j;
                            break;
                        }
                    }

                    if (doMove)
                    {
                        for (int j = moveIndex; j > robot.j; j--)
                        {
                            var swap = map[robot.i, j - 1];
                            map[robot.i, j - 1] = map[robot.i, j];
                            map[robot.i, j] = swap;

                            if (swap == '@')
                            {
                                robot = (robot.i, j);
                            }
                        }
                    }
                }

                else if (dir == 'v')
                {
                    var doMove = true;
                    var moveIndex = -1;

                    for (int i = robot.i + 1; i < map.GetLength(0); i++)
                    {
                        if (map[i, robot.j] == '#')
                        {
                            doMove = false;
                            moveIndex = -1;
                        }
                        else if (map[i, robot.j] == '.')
                        {
                            moveIndex = i;
                            break;
                        }
                    }

                    if (doMove)
                    {
                        for (int i = moveIndex; i > robot.i; i--)
                        {
                            var swap = map[i - 1, robot.j];
                            map[i - 1, robot.j] = map[i, robot.j];
                            map[i, robot.j] = swap;

                            if (swap == '@')
                            {
                                robot = (i, robot.j);
                            }
                        }
                    }
                }


                else if (dir == '^')
                {
                    var doMove = true;
                    var moveIndex = -1;

                    for (int i = robot.i - 1; i >= 0; i--)
                    {
                        if (map[i, robot.j] == '#')
                        {
                            doMove = false;
                            moveIndex = -1;
                        }
                        else if (map[i, robot.j] == '.')
                        {
                            moveIndex = i;
                            break;
                        }
                    }

                    if (doMove)
                    {
                        for (int i = moveIndex; i < robot.i; i++)
                        {
                            var swap = map[i + 1, robot.j];
                            map[i + 1, robot.j] = map[i, robot.j];
                            map[i, robot.j] = swap;

                            if (swap == '@')
                            {
                                robot = (i, robot.j);
                            }
                        }
                    }
                }


                //Print(map);
            }


            var output = 0;
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == 'O')
                    {
                        output += (i * 100) + j;
                    }
                }
            }

            Console.WriteLine(output);
        }
    }
}

using AdvantOfCode._2023.Day19;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace AdvantOfCode._2024.Day17
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
            var txt = File.ReadAllText(@"C:\Users\akash.buch\source\repos\AdvantOfCode2023\AdvantOfCode\2024\Day17\input.txt");

            
            var regA = Convert.ToInt32(txt.Split("\r\n\r\n").First().Split("\r\n")[0].Split(": ").Last());
            var regB = Convert.ToInt32(txt.Split("\r\n\r\n").First().Split("\r\n")[1].Split(": ").Last());
            var regC = Convert.ToInt32(txt.Split("\r\n\r\n").First().Split("\r\n")[2].Split(": ").Last());

            var program = txt.Split("\r\n\r\n").Last().Split(": ").Last().Split(',').Select(x=>Convert.ToInt32(x)).ToList();

            int count = 64;
            while (true)
            {
                List<int> output = new();
                regA = count;

                for (int i = 0; i < program.Count(); i += 2)
                {
                    var Opcode = program[i];
                    var Operand = program[i + 1];

                    var calc_Operand = 0;
                    if (Operand <= 3)
                    {
                        calc_Operand = Operand;
                    }
                    else if (Operand == 4)
                    {
                        calc_Operand = regA;
                    }
                    else if (Operand == 5)
                    {
                        calc_Operand = regB;
                    }
                    else if (Operand == 6)
                    {
                        calc_Operand = regC;
                    }



                    if (Opcode == 0)
                    {
                        //regA = (int)(regA / Math.Pow(2, calc_Operand));
                        regA = regA >> calc_Operand;
                    }
                    else if (Opcode == 1)
                    {
                        regB = regB ^ Operand;
                    }
                    else if (Opcode == 2)
                    {
                        regB = calc_Operand % 8;
                    }
                    else if (Opcode == 3)
                    {
                        if (regA != 0)
                        {
                            i = Operand - 2;
                            continue;
                        }
                    }
                    else if (Opcode == 4)
                    {
                        regB = regB ^ regC;
                    }
                    else if (Opcode == 5)
                    {
                        output.Add(calc_Operand % 8);

                        //var a = calc_Operand % 8;

                        //if (program[output.Count] == a)
                        //{
                        //    output.Add(calc_Operand % 8);
                        //}
                        //else
                        //{
                        //    break;
                        //}
                    }
                    else if (Opcode == 6)
                    {
                        //regB = (int)(regA / Math.Pow(2, calc_Operand));
                        regB = regA >> calc_Operand;
                    }
                    else if (Opcode == 7)
                    {
                        //regC = (int)(regA / Math.Pow(2, calc_Operand));
                        regC = regA >> calc_Operand;
                    }
                }

                Console.WriteLine($"{count} = {string.Join(',', output)}");

                var isMatch = true;
                if (program.Count == output.Count)
                {
                    for (int i = 0; i < program.Count; i++)
                    {
                        if (program[i] != output[i])
                        {
                            isMatch = false;
                            break;
                        }
                    }
                }
                else
                {
                    isMatch = false;
                }

                if (isMatch) { break; }

                count++;
            }

            Console.WriteLine(count);
        }
    }
}

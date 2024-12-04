using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdvantOfCode._2024.Day3
{
    public class Solutions
    {
        public void First()
        {
            var input = File.ReadAllText(@"C:\Users\akash.buch\source\repos\AdvantOfCode2023\AdvantOfCode2023\2024\Day3\input.txt");
            var output = 0;

            var enable = true;

            var detectionStarted = false;
            
            var firstDigit = "";
            var firstDigitFlag = false;

            var secoundDigit = "";
            var secoundDigitFlag = false;

            for (int i = 0; i < input.Length; i++)
            {
                if (i < input.Length - 3 && string.Concat(input[i], input[i + 1], input[i + 2], input[i + 3]).Equals("mul("))
                {
                    detectionStarted = true;
                    firstDigitFlag = true;
                    i = i + 3;
                    continue;
                }

                if(enable && detectionStarted)
                {
                    if (char.IsDigit(input[i]) && firstDigitFlag)
                    {
                        firstDigit += input[i];
                    }
                    else if (char.IsDigit(input[i]) && secoundDigitFlag)
                    {
                        secoundDigit += input[i];
                    }
                    else if (input[i].Equals(','))
                    {
                        firstDigitFlag = false;
                        secoundDigitFlag = true;
                    }
                    else if(input[i].Equals(')'))
                    {
                        detectionStarted = false;
                        firstDigitFlag = false;
                        secoundDigitFlag = false;

                        Console.WriteLine($"{firstDigit} * {secoundDigit} + ");
                        output += Convert.ToInt32(firstDigit) * Convert.ToInt32(secoundDigit);
                        

                        firstDigit = "";
                        secoundDigit = "";
                    }
                    else
                    {
                        detectionStarted = false;
                        firstDigitFlag = false;
                        secoundDigitFlag = false;

                        firstDigit = "";
                        secoundDigit = "";
                    }
                }
            }

            Console.WriteLine(output);
        }

        public void Secound()
        {
            var input = File.ReadAllText(@"C:\Users\akash.buch\source\repos\AdvantOfCode2023\AdvantOfCode2023\2024\Day3\input.txt");
            var output = 0;

            var enable = true;

            var detectionStarted = false;

            var firstDigit = "";
            var firstDigitFlag = false;

            var secoundDigit = "";
            var secoundDigitFlag = false;

            for (int i = 0; i < input.Length; i++)
            {
                if (i < input.Length - 6 && string.Concat(input[i], input[i + 1], input[i + 2], input[i + 3], input[i + 4], input[i + 5], input[i + 6]).Equals("don't()"))
                {
                    enable = false;
                    detectionStarted = false;
                    firstDigitFlag = false;
                    secoundDigitFlag = false;

                    firstDigit = "";
                    secoundDigit = "";

                    i = i + 6;
                }
                if (i < input.Length - 6 && string.Concat(input[i], input[i + 1], input[i + 2], input[i + 3]).Equals("do()"))
                {
                    enable = true;
                    detectionStarted = false;
                    firstDigitFlag = false;
                    secoundDigitFlag = false;

                    firstDigit = "";
                    secoundDigit = "";

                    i = i + 3;
                }

                if (i < input.Length - 3 && string.Concat(input[i], input[i + 1], input[i + 2], input[i + 3]).Equals("mul("))
                {
                    detectionStarted = true;
                    firstDigitFlag = true;
                    i = i + 3;
                    continue;
                }

                if (enable && detectionStarted)
                {
                    if (char.IsDigit(input[i]) && firstDigitFlag)
                    {
                        firstDigit += input[i];
                    }
                    else if (char.IsDigit(input[i]) && secoundDigitFlag)
                    {
                        secoundDigit += input[i];
                    }
                    else if (input[i].Equals(','))
                    {
                        firstDigitFlag = false;
                        secoundDigitFlag = true;
                    }
                    else if (input[i].Equals(')'))
                    {
                        detectionStarted = false;
                        firstDigitFlag = false;
                        secoundDigitFlag = false;

                        Console.WriteLine($"{firstDigit} * {secoundDigit} + ");
                        output += Convert.ToInt32(firstDigit) * Convert.ToInt32(secoundDigit);


                        firstDigit = "";
                        secoundDigit = "";
                    }
                    else
                    {
                        detectionStarted = false;
                        firstDigitFlag = false;
                        secoundDigitFlag = false;

                        firstDigit = "";
                        secoundDigit = "";
                    }
                }
            }

            Console.WriteLine(output);
        }
    }
}

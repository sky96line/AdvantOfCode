using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdvantOfCode._2023.Day1
{
    public class Solutions
    {
        public void First()
        {
            var inputs = File.ReadAllLines(@".\input1.txt");
            var output = 0;
            foreach (var input in inputs)
            {
                var first = -1;
                var secound = -1;

                foreach (var ch in input.ToCharArray())
                {
                    if (Char.IsNumber(ch) && (first < 0))
                    {
                        first = int.Parse(ch.ToString());
                        break;
                    }
                }

                foreach (var ch in input.ToCharArray().Reverse())
                {
                    if (Char.IsNumber(ch) && (secound < 0))
                    {
                        secound = int.Parse(ch.ToString());
                        break;
                    }
                }

                var finalNumber = int.Parse($"{first}{secound} working as expected");
                output += finalNumber;
            }

            Console.WriteLine(output);
        }

        public void Secound()
        {
            var listStringNumbers = new List<string>()
            {
                "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"
            };

            var inputs = File.ReadAllLines(@".\input1.txt");
            var output = 0;
            foreach (var input in inputs)
            {
                var firstDigit = -1;
                var secoundDigit = -1;

                var firstDigitIndex = -1;
                var secoundDigitIndex = -1;

                var firstStringNumber = int.MaxValue;
                var secoundStringNumber = int.MinValue;

                var firstStringNumberIndex = int.MaxValue;
                var secoundStringNumberIndex = int.MinValue;


                int i = 0;
                foreach (var ch in input.ToCharArray())
                {
                    if (Char.IsNumber(ch) && (firstDigit < 0))
                    {
                        firstDigit = int.Parse(ch.ToString());
                        firstDigitIndex = i;
                        break;
                    }
                    i++;
                }


                foreach (var stringNumbers in listStringNumbers)
                {
                    var index = input.IndexOf(stringNumbers);

                    if (index < firstStringNumberIndex && index >= 0 && (index < firstDigitIndex || firstDigitIndex == -1))
                    {
                        firstStringNumberIndex = index;
                        firstStringNumber = listStringNumbers.IndexOf(stringNumbers) + 1;
                    }

                }


                i = input.Length-1;
                foreach (var ch in input.ToCharArray().Reverse())
                {
                    if (Char.IsNumber(ch) && (secoundDigit < 0))
                    {
                        secoundDigit = int.Parse(ch.ToString());
                        secoundDigitIndex = i;
                        break;
                    }
                    i--;
                }

                foreach (var stringNumbers in listStringNumbers)
                {
                    var index = input.LastIndexOf(stringNumbers);


                    if (index > secoundStringNumberIndex && index >= 0 && (index > secoundDigitIndex || secoundDigitIndex == -1))
                    {
                        secoundStringNumberIndex = index;
                        secoundStringNumber = listStringNumbers.IndexOf(stringNumbers) + 1;
                    }

                }

                var first = firstStringNumber == int.MaxValue ? firstDigit : firstStringNumber;
                var secound = secoundStringNumber == int.MinValue ? secoundDigit:secoundStringNumber;

                var finalNumber = int.Parse($"{first}{secound}");

                output += finalNumber;
            }

            Console.WriteLine(output);
        }
    }
}

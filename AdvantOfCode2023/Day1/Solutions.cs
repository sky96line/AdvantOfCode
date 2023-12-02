using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdvantOfCode2023.Day1
{
    public class Solutions
    {
        public void First()
        {
            var inputs = File.ReadAllLines(@"C:\Users\skyli\source\repos\AdvantOfCode2023\AdvantOfCode2023\Day1\input1.txt");
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

                var finalNumber = int.Parse($"{first}{secound}");
                output += finalNumber;
            }

            Console.WriteLine(output);
        }


        public void Secound()
        {
            var inp = "two1nine";
            var i = inp.IndexOf("two");
            Console.WriteLine(i);


            var listStringNumbers = new List<string>()
            {
                "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"
            };

            var inputs = File.ReadAllLines(@"C:\Users\skyli\source\repos\AdvantOfCode2023\AdvantOfCode2023\Day1\input1.txt");
            var output = 0;
            foreach (var input in inputs)
            {
                var firstDigit = -1;
                var secoundDigit = -1;

                var firstStringNumber = -1;
                var secoundStringNumber = -1;

                foreach (var ch in input.ToCharArray())
                {
                    if (Char.IsNumber(ch) && (firstDigit < 0))
                    {
                        firstDigit = int.Parse(ch.ToString());
                        break;
                    }
                }


                foreach (var stringNumbers in listStringNumbers)
                {
                    var index = input.IndexOf(stringNumbers);
                    if ((index < firstStringNumber || firstStringNumber == -1) && index >= 0)
                    {
                        index = listStringNumbers.IndexOf(stringNumbers) + 1;
                        firstStringNumber = index;
                    }
                        
                }


                foreach (var ch in input.ToCharArray().Reverse())
                {
                    if (Char.IsNumber(ch) && (secoundDigit < 0))
                    {
                        secoundDigit = int.Parse(ch.ToString());
                        break;
                    }
                }

                foreach (var stringNumbers in listStringNumbers)
                {
                    var index = input.IndexOf(stringNumbers);
                    if (secoundStringNumber > index)
                        secoundStringNumber = index;
                }

                var first = 0;
                var secound = 0;
                if (firstDigit > 0)
                {
                    first = (firstStringNumber + 1) > firstDigit ? firstDigit : (firstStringNumber + 1);
                }
                else
                {
                    first = (firstStringNumber + 1);
                }


                if (secound > 0)
                {
                    secound = (secoundStringNumber + 1) > secoundDigit ? (secoundStringNumber + 1) : secoundDigit;
                }
                else
                {
                    secound = (secoundStringNumber + 1);
                }

                 


                Console.WriteLine($"{firstDigit} | {firstStringNumber}");
                //Console.WriteLine($"{secoundDigit} | {secoundStringNumber}");

                Console.WriteLine($"{first}{secound}");

                Console.WriteLine($"==========");

                var finalNumber = int.Parse($"{first}{secound}");
                output += finalNumber;
            }

            Console.WriteLine(output);
        }
    }
}

using AdvantOfCode._2023.Day19;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;

namespace AdvantOfCode._2024.Day19
{
    public class Solutions
    {
       
        public void First()
        {
            var text = File.ReadAllText(@"C:\Users\akash.buch\source\repos\AdvantOfCode2023\AdvantOfCode\2024\Day19\input.txt");

            var towls = text.Split("\r\n\r\n").First().Split(", ");
            var inputs = text.Split("\r\n\r\n").Last().Split("\r\n");

            foreach (var input in inputs)
            {
                var match = true;
                int i = 0;

                while (i < input.Length)
                {
                    if (input.Length - i >= 3 && towls.Contains(input.Substring(i, 3)))
                    {
                        i += 3;
                    }
                    else if (input.Length - i >= 2 && towls.Contains(input.Substring(i, 2)))
                    {
                        i += 2;
                    }
                    if (input.Length - i >= 1 && towls.Contains(input.Substring(i, 1)))
                    {
                        i += 1;
                    }
                    else
                    {
                        match = false;
                        break;
                    }
                }

                if (match)
                {
                    Console.WriteLine("Found");
                }
                else
                {
                    Console.WriteLine("Not Found");
                }
            }
        }
    }
}

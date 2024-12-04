using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdvantOfCode._2024.Day2
{
    public class Solutions
    {
        public void First()
        {
            var inputs = File.ReadAllLines(@"C:\Users\akash.buch\source\repos\AdvantOfCode2023\AdvantOfCode2023\2024\Day2\input.txt");
            var output = 0;

            foreach (var input in inputs)
            {
                var reports = input.Split(" ").Select(x => Convert.ToInt32(x)).ToList();

                var problemIndex = -1;
                for (int i = 1; i < reports.Count - 1; i++)
                {
                    if (reports[i - 1] == reports[i])
                    {
                        problemIndex = i - 1;
                        break;
                    }
                    else if (reports[i] == reports[i + 1])
                    {
                        problemIndex = i;
                        break;
                    }
                    else if ((reports[i + 1] - reports[i] > 0 && reports[i] - reports[i - 1] < 0) || (reports[i + 1] - reports[i] < 0 && reports[i] - reports[i - 1] > 0))
                    {
                        problemIndex = i;
                        break;
                    }
                    else if (reports[i + 1] - reports[i] > 3)
                    {
                        problemIndex = i;
                        break;
                    }
                }
                

                if (problemIndex == -1)
                {
                    Console.WriteLine("Safe");
                    output++;
                }
                else
                {
                    Console.WriteLine("Unsafe");
                }
            }

            Console.WriteLine(output);
        }

        public void Secound()
        {
            var inputs = File.ReadAllLines(@"C:\Users\akash.buch\source\repos\AdvantOfCode2023\AdvantOfCode2023\2024\Day2\input.txt");
            var output = 0;

            foreach (var input in inputs)
            {
                var reports = input.Split(" ").Select(x => Convert.ToInt32(x)).ToList();

                bool safeFlag = true;
                bool? increaseFalg = null;
                bool acceptError = true;
                for (int i = 0; i < reports.Count - 1; i++)
                {
                    var off = reports[i + 1] - reports[i];
                    if (off == 0)
                    {
                        if (acceptError)
                        {
                            acceptError = false;
                            continue;
                        }
                        else
                        {
                            safeFlag = false;
                            break;
                        }
                    }
                    else
                    {
                        if (increaseFalg is null)
                        {
                            if (off > 0) increaseFalg = true;
                            else increaseFalg = false;
                        }
                    }

                    if (increaseFalg.Value && (off < 0 || off > 3))
                    {
                        if (acceptError)
                        {
                            acceptError = false;
                            continue;
                        }
                        else
                        {
                            safeFlag = false;
                            break;
                        }
                    }
                    else if (!increaseFalg.Value && (off > 0 || off < -3))
                    {
                        if (acceptError)
                        {
                            acceptError = false;
                            continue;
                        }
                        else
                        {
                            safeFlag = false;
                            break;
                        }
                    }
                }

                if (safeFlag)
                {
                    Console.WriteLine("Safe");
                    output++;
                }
                else
                {
                    Console.WriteLine("Unsafe");
                }
            }

            Console.WriteLine(output);
        }
    }
}

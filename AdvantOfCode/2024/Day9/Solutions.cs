using AdvantOfCode._2023.Day19;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AdvantOfCode._2024.Day9
{

    public class Solutions
    {
        class Node
        {
            public int Block { get; set; }
        }

        private string ListToString(List<int> ints)
        {
            StringBuilder str = new StringBuilder();
            foreach (var i in ints)
            {
                if (i == -1)
                {
                    str.Append('.');
                }
                else
                {
                    str.Append(i.ToString());
                }
                
            }

            return str.ToString();
        }

        private int ListToString(List<int> ints, int count)
        {
            var res = 0;
            for (int i = 0; i < ints.Count; i++)
            {
                if (ints[i] == -1)
                {
                    res++;
                }
                else
                {
                    res = 0;
                }

                if (res == count)
                    return i - res + 1;
            }

            return -1;
        }

        public void First()
        {
            List<int> output = new();

            var text_inputs = File.ReadAllText(@"C:\Users\akash.buch\source\repos\AdvantOfCode2023\AdvantOfCode\2024\Day9\input.txt");

            var freeSpaceFalg = false;
            var id = 0;

            foreach (var input in text_inputs)
            {
                var len = int.Parse(input.ToString());
                for (int i = 0; i < len; i++)
                {
                    if (freeSpaceFalg)
                    {
                        output.Add(-1);
                    }
                    else
                    {
                        output.Add(id);
                    }
                }
                if (!freeSpaceFalg) id++;
                freeSpaceFalg = !freeSpaceFalg;
            }


            var orignial_output = new List<int>(output);
            //foreach (var p in output)
            //{
            //    if (p == -1)
            //    {
            //        Console.Write(".");
            //    }
            //    else
            //    {
            //        Console.Write(p);
            //    }
            //}
            //Console.WriteLine("");

            List<int> searched = new();
            for (int i = output.Count-1; i >= 0; i--)
            {
                if (output[i] != -1)
                {
                    var last = output.LastOrDefault(x => x != -1 && !searched.Contains(x));
                    var lastCount = output.Count(x => x == last);
                    var lastIndex = output.IndexOf(last);

                    if (last == 0)
                        break;

                    searched.Add(last);
                    var s = ListToString(output);
                    var index = ListToString(output, lastCount);

                    if (index > 0 && lastIndex > index)
                    {
                        for (int j = 0; j < lastCount; j++)
                        {
                            output[index + j] = last;
                            output[lastIndex + j] = -1;
                        }
                    }

                    //foreach (var p in output)
                    //{
                    //    if (p == -1)
                    //    {
                    //        Console.Write(".");
                    //    }
                    //    else
                    //    {
                    //        Console.Write(p);
                    //    }
                    //}
                    //Console.WriteLine("");
                }
            }


            foreach (var p in output)
            {
                if (p == -1)
                {
                    Console.Write(".");
                }
                else
                {
                    Console.Write(p);
                }
            }
            Console.WriteLine("");

            long result = 0;
            for (int i = 0; i < output.Count; i++)
            {
                if (output[i] != -1)
                {
                    var c = int.Parse(output[i].ToString());
                    result += (c * i);
                }
            }

            Console.WriteLine(result);
        }
    }
}

using AdvantOfCode._2023.Day19;
using AdvantOfCode._2024.Day8;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AdvantOfCode._2024.Day13
{
    public class Equation
    {
        public Equation(long x, long y, long total)
        {
            this.x = x;
            this.y = y;
            Total = total;
        }

        public long x { get; set; }
        public long y { get; set; }
        public long Total { get; set; }
    }

    public class Solutions
    {
        public void First()
        {
            var read = File.ReadAllText(@"C:\Users\akash.buch\source\repos\AdvantOfCode2023\AdvantOfCode\2024\Day13\input.txt");

            var input_texts = read.Split("\r\n\r\n");
            decimal output = 0;
            foreach (var input_text in input_texts)
            {
                var inputs = input_text.Split("\r\n");

                var x1 = inputs[0].Split('+')[1].Split(',').First();
                var y1 = inputs[1].Split('+')[1].Split(',').First();
                var total1 = inputs[2].Split('=')[1].Split(',').First();

                var x = Convert.ToInt64(x1);
                var y = Convert.ToInt64(y1);
                var total = Convert.ToInt64(total1) + 10000000000000;

                Equation eq1 = new(x, y, total);

                var x2 = inputs[0].Split('+').Last();
                var y2 = inputs[1].Split('+').Last();
                var total2 = inputs[2].Split('=').Last();

                x = Convert.ToInt64(x2);
                y = Convert.ToInt64(y2);
                total = Convert.ToInt64(total2) + 10000000000000;

                Equation eq2 = new(x, y, total);

                //calculating y
                var temp_x1 = eq1.x * eq2.x;
                var temp_y1 = eq1.y * eq2.x ;
                var temp_total1 = eq1.Total * eq2.x ;

                var temp_x2 = eq2.x * eq1.x;
                var temp_y2 = eq2.y * eq1.x;
                var temp_total2 = eq2.Total * eq1.x;


                var yy = temp_y1 - temp_y2;
                var tt_y = temp_total1 - temp_total2;

                decimal ans_y = Decimal.Divide(tt_y, yy);

                if (ans_y.ToString().Contains("."))
                    continue;

                //calculating x
                temp_x1 = eq1.x * eq2.y;
                temp_y1 = eq1.y * eq2.y;
                temp_total1 = eq1.Total * eq2.y;

                temp_x2 = eq2.x * eq1.y;
                temp_y2 = eq2.y * eq1.y;
                temp_total2 = eq2.Total * eq1.y;

                var xx = temp_x1 - temp_x2;
                var tt_x = temp_total1 - temp_total2;

                decimal ans_x = Decimal.Divide(tt_x, xx);

                if (ans_x.ToString().Contains("."))
                    continue;

                output += (ans_x * 3 + ans_y);
            }

            Console.WriteLine(output);
        }
    }
}

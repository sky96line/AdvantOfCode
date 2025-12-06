using AdvantOfCode._2023.Day19;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;

namespace AdvantOfCode._2024.Day14
{
    class Node
    {
        public int pos_x { get; set; }
        public int pos_y { get; set; }
        public int vel_x { get; set; }
        public int vel_y { get; set; }
    }

    public class Solutions
    {
        int width = 101;
        int height = 103;

        //int width = 11;
        //int height = 7;

        private List<Node> Work(List<Node> inputs)
        {
            List<Node> result = new();

            foreach (var input in inputs)
            {
                var new_x = (input.pos_x + input.vel_x);
                var new_y = (input.pos_y + input.vel_y);

                if (new_x < 0)
                {
                    new_x = width + new_x;
                }
                else if(new_x >= width)
                {
                    new_x = new_x - width;
                }


                if (new_y < 0)
                {
                    new_y = height + new_y;
                }
                else if (new_y >= height)
                {
                    new_y = new_y - height;
                }


                Node node = new Node()
                {
                    pos_x = new_x,
                    pos_y = new_y,
                    vel_x = input.vel_x,
                    vel_y = input.vel_y
                };

                result.Add(node);
            }

            return result;
        }

        public void First()
        {
            var read = File.ReadAllLines(@"C:\Users\akash.buch\source\repos\AdvantOfCode2023\AdvantOfCode\2024\Day14\input.txt");

            List<Node> inputs = new List<Node>();
            foreach (var r in read)
            {
                var p_x = r.Split(" ").First().Split("=").Last().Split(",").First();
                var p_y = r.Split(" ").First().Split("=").Last().Split(",").Last();

                var v_x = r.Split(" ").Last().Split("=").Last().Split(",").First();
                var v_y = r.Split(" ").Last().Split("=").Last().Split(",").Last();

                Node node = new()
                {
                    pos_x = Convert.ToInt32(p_x),
                    pos_y = Convert.ToInt32(p_y),

                    vel_x = Convert.ToInt32(v_x),
                    vel_y = Convert.ToInt32(v_y)
                };

                inputs.Add(node);
            }

            for (int i = 0; i < 10000; i++)
            {
                Print(i, inputs);
                inputs = Work(inputs);
               
            }

            var left = inputs.Count(x => x.pos_x < (width - 1) / 2 && x.pos_y < (height - 1) / 2);

            var right = inputs.Count(x => x.pos_x > (width - 1) / 2 && x.pos_y < (height - 1) / 2);

            var left_btm = inputs.Count(x => x.pos_x < (width - 1) / 2 && x.pos_y > (height - 1) / 2);

            var right_btm = inputs.Count(x => x.pos_x > (width - 1) / 2 && x.pos_y > (height - 1) / 2);


            Console.WriteLine(left * right * left_btm * right_btm);
        }

        void Print(int index, List<Node> inputs)
        {
            if (index > 8000 && index < 9000)
            {
                StringBuilder builder = new StringBuilder();

                builder.AppendLine(index.ToString());

                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        if (inputs.Any(x => x.pos_y == i && x.pos_x == j))
                        {
                            builder.Append("*");
                            //Console.Write("*");
                        }
                        else
                        {
                            builder.Append(" ");
                            //Console.Write(".");
                        }
                    }
                    builder.Append(Environment.NewLine);
                    //Console.WriteLine("");
                }

                File.AppendAllTextAsync($@"C:\Users\akash.buch\source\repos\AdvantOfCode2023\AdvantOfCode\2024\Day14\images\text.txt", builder.ToString());
            }

            
        }


        void DrawText(String text, Font font, Color textColor, Color backColor, int i)
        {
            //first, create a dummy bitmap just to get a graphics object
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);

            //measure the string to see how big the image needs to be
            SizeF textSize = drawing.MeasureString(text, font);

            //free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size
            img = new Bitmap((int)textSize.Width, (int)textSize.Height);

            drawing = Graphics.FromImage(img);

            //paint the background
            drawing.Clear(backColor);

            //create a brush for the text
            Brush textBrush = new SolidBrush(textColor);

            drawing.DrawString(text, font, textBrush, 0, 0);
            
            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();

            img.Save($@"C:\Users\akash.buch\source\repos\AdvantOfCode2023\AdvantOfCode\2024\Day14\images\{i}.png");
        }
    }
}

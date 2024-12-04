using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace AdvantOfCode._2023.Day19
{
    class Input
    {
        public int x { get; set; }
        public int m { get; set; }
        public int a { get; set; }
        public int s { get; set; }
    }

    class Workflow
    {
        public string Name { get; set; }
        public string Condition { get; set; }
    }

    class Condition
    {
        public int Val { get; set; }
        public string LT_GT { get; set; }
    }

    public class Solutions
    {

        List<Input> inputs = new List<Input>();
        List<Workflow> workflows = new List<Workflow>();

        public Solutions()
        {
            var data = File.ReadAllText(@".\input19_test.txt");

            var workflow = data.Split("\r\n\r\n").First();
            var input_item = data.Split("\r\n\r\n").Last();

            foreach (var flow in workflow.Split("\r\n"))
            {
                var name = flow.Split('{').First();
                var condition = flow.Split('{').Last().Replace("}", "");

                Workflow w = new Workflow();
                w.Name = name;
                w.Condition = condition;

                workflows.Add(w);
            }

            foreach (var i in input_item.Split("\r\n"))
            {
                var ins = i.Replace("{", "");
                ins = ins.Replace("}", "");

                var cons = ins.Split(",").Select(x => x.Trim());

                Input input1 = new Input();
                foreach (var con in cons)
                {
                    var key = con.Split("=").First();
                    var val = int.Parse(con.Split("=").Last());

                    if (key == "x") input1.x = val;
                    else if (key == "m") input1.m = val;
                    else if (key == "a") input1.a = val;
                    else if (key == "s") input1.s = val;
                }
                inputs.Add(input1);
            }    
        }


        Dictionary<string, Input> calc = new();

        private long Calc(List<Input> inputs)
        {
            Input gin = new() { x = 4000, m = 4000, s = 4000, a = 4000 };
            calc.Add("start", gin);

            long total = 0;
            foreach (var input in inputs)
            {
                var flow_name = "in";
                Console.Write($"{JsonConvert.SerializeObject(input)}: ");
                while (true)
                {
                    if (flow_name.Equals("A") || flow_name.Equals("R"))
                    {
                        if (flow_name.Equals("A"))
                        {
                            //total += (input.x * input.m * input.a * input.s);
                            total += 1;
                        }
                        Console.Write($"{flow_name}");

                        break;
                    }

                    var curr_flow = workflows.First(x => x.Name.Equals(flow_name));

                    Console.Write($"{flow_name} -> ");

                    var flows = curr_flow.Condition.Split(',');

                    for (int i = 0; i < flows.Count(); i++)
                    {   
                        var flow = flows[i];

                        if (!(flow.Contains(">") || flow.Contains("<")))
                        {
                            flow_name = flow;
                            break;
                        }

                        if (flow[0] == 'x')
                        {
                            if (flow.Contains(">"))
                            {
                                var val = int.Parse(flow.Split(">").Last().Split(":").First());
                                var con = flow.Split(":").Last();
                                                                                                
                                if (input.x > val)
                                {
                                    bif_x.Add(new Condition() { Val = val, LT_GT = "GT" });
                                    flow_name = con;
                                    break;
                                }
                                else
                                {
                                    continue;
                                }

                            }
                            else
                            {
                                var val = int.Parse(flow.Split("<").Last().Split(":").First());
                                var con = flow.Split(":").Last();
                                                                                                
                                if (input.x < val)
                                {
                                    bif_x.Add(new Condition() { Val = val, LT_GT = "LT" });
                                    flow_name = con;
                                    break;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                        else if (flow[0] == 'm')
                        {
                            if (flow.Contains(">"))
                            {
                                var val = int.Parse(flow.Split(">").Last().Split(":").First());
                                var con = flow.Split(":").Last();
                                                                                                
                                if (input.m > val)
                                {
                                    bif_m.Add(new Condition() { Val = val, LT_GT = "GT" });
                                    flow_name = con;
                                    break;
                                }
                                else
                                {
                                    continue;
                                }

                            }
                            else
                            {
                                var val = int.Parse(flow.Split("<").Last().Split(":").First());
                                var con = flow.Split(":").Last();
                                                                                                
                                if (input.m < val)
                                {
                                    bif_m.Add(new Condition() { Val = val, LT_GT = "GT" });
                                    flow_name = con;
                                    break;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                        else if (flow[0] == 'a')
                        {
                            if (flow.Contains(">"))
                            {
                                var val = int.Parse(flow.Split(">").Last().Split(":").First());
                                var con = flow.Split(":").Last();
                                                                                                
                                if (input.a > val)
                                {
                                    bif_a.Add(new Condition() { Val = val, LT_GT = "GT" });
                                    flow_name = con;
                                    break;
                                }
                                else
                                {
                                    continue;
                                }

                            }
                            else
                            {
                                var val = int.Parse(flow.Split("<").Last().Split(":").First());
                                var con = flow.Split(":").Last();
                                                                                                
                                if (input.a < val)
                                {
                                    bif_a.Add(new Condition() { Val = val, LT_GT = "GT" });
                                    flow_name = con;
                                    break;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                        else if (flow[0] == 's')
                        {
                            if (flow.Contains(">"))
                            {
                                var val = int.Parse(flow.Split(">").Last().Split(":").First());
                                var con = flow.Split(":").Last();
                                                                                             
                                if (input.s > val)
                                {
                                    
                                    bif_s.Add(new Condition() { Val = val, LT_GT = "GT" });
                                    flow_name = con;
                                    break;
                                }
                                else
                                {
                                    continue;
                                }

                            }
                            else
                            {
                                var val = int.Parse(flow.Split("<").Last().Split(":").First());
                                var con = flow.Split(":").Last();
                                                                                                
                                if (input.s < val)
                                {
                                    bif_s.Add(new Condition() { Val = val, LT_GT = "GT" });
                                    flow_name = con;
                                    break;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                    }
                }

                Console.WriteLine("");

            }
            return total;
        }

        public void First()
        {

            var total = Calc(inputs);
            Console.WriteLine($"{total}");
        }


        List<Condition> bif_x = new();
        List<Condition> bif_m = new();
        List<Condition> bif_a = new();
        List<Condition> bif_s = new();

        public void Secound()
        {

        }
    }

}

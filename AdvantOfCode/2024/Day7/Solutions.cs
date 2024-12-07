namespace AdvantOfCode._2024.Day7
{
    class Node
    {
        public bool IsCorrect { get; set; }
        public long Total { get; set; }
        public List<int> Numbers { get; set; } = new List<int>();
    }

    public class Solutions
    {
        char[] operators = new char[] { '+', '*', '|' };

        List<char> GetOperatorCombination(int index, int length)
        {
            
            List<char> combination = new List<char>();

            for (int i = 0; i < length; i++)
            {
                combination.Add(operators[index % operators.Length]);
                index /= operators.Length;
            }

            return combination;
        }

        public void First()
        {
            var text_inputs = File.ReadAllLines(@"C:\Users\akash.buch\source\repos\AdvantOfCode2023\AdvantOfCode\2024\Day7\input.txt");
            long output = 0;

            List<Node> inputs = new();
            foreach (var input in text_inputs) 
            {
                long total = Convert.ToInt64(input.Split(":").First().Trim());
                var list_input = input.Split(":").Last().Trim().Split(" ").Select(x => Convert.ToInt32(x)).ToList();

                inputs.Add(new() { Total = total, Numbers = list_input });
            }


            foreach (var input in inputs)
            {
                var loop = Math.Pow(operators.Count(), input.Numbers.Count - 1);
                
                for (int i = 0; i < loop; i++)
                {
                    var combination = GetOperatorCombination(i, input.Numbers.Count - 1);

                    long result = input.Numbers.First();
                    
                    for (int j = 0; j < combination.Count; j++)
                    {
                        if (combination[j] == '+')
                        {
                            result = result + input.Numbers[j + 1];
                        }
                        else if (combination[j] == '*')
                        {
                            result = result * input.Numbers[j + 1];
                        }
                        else if (combination[j] == '|')
                        {
                            result = Convert.ToInt64(result.ToString() + input.Numbers[j + 1]);
                        }
                    }

                    if (result == input.Total)
                    {
                        output += result;
                        break;
                    }       
                }
            }

            Console.WriteLine(output);
        }

        public void Secound()
        {
            var txt_inputs = File.ReadAllLines(@"C:\Users\akash.buch\source\repos\AdvantOfCode2023\AdvantOfCode\2024\Day7\input.txt");
            var output = 0;

            Console.WriteLine(output);
        }
    }
}

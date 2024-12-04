using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvantOfCode._2023.Day2
{
    public class Solutions
    {

        public void First()
        {
            var PossibleRed = 12;
            var PossibleGreen = 13;
            var PossibleBlue = 14;

            var inputs = File.ReadAllLines(@"C:\Users\skyli\source\repos\AdvantOfCode2023\AdvantOfCode2023\Day2\input2.txt");

            var result = 0;
            foreach (var input in inputs)
            {
                var gameId =  int.Parse(input.Trim().Split(":").First().Split(' ').Last());

                var gameData = input.Trim().Split(":").Last();

                var Possible = true;

                foreach (var setPickedCube in gameData.Split(';'))
                {
                    foreach (var pickedCube in setPickedCube.Trim().Split(','))
                    {
                        var pickedCubeCount = int.Parse(pickedCube.Trim().Split(" ").First());
                        var pickedCubeColor = pickedCube.Trim().Split(' ').Last();

                        if (pickedCubeColor.Equals("red") && pickedCubeCount > PossibleRed)
                        {
                            Possible = false;
                        }
                        if (pickedCubeColor.Equals("green") && pickedCubeCount > PossibleGreen)
                        {
                            Possible = false;
                        }
                        if (pickedCubeColor.Equals("blue") && pickedCubeCount > PossibleBlue)
                        {
                            Possible = false;
                        }

                        if (!Possible)
                            break;
                    }
                    if (!Possible)
                        break;
                }

                if (Possible)
                    result += gameId;
            }

            Console.WriteLine(result);
        }

        public void Secound()
        {
            var inputs = File.ReadAllLines(@"C:\Users\skyli\source\repos\AdvantOfCode2023\AdvantOfCode2023\Day2\input2.txt");

            var result = 0;
            foreach (var input in inputs)
            {
                var gameId = int.Parse(input.Trim().Split(":").First().Split(' ').Last());

                var gameData = input.Trim().Split(":").Last();

                var Possible = true;

                var maxRed = 1;
                var maxGreen = 1;
                var maxBlue = 1;

                foreach (var setPickedCube in gameData.Split(';'))
                {
                   
                    foreach (var pickedCube in setPickedCube.Trim().Split(','))
                    {
                        var pickedCubeCount = int.Parse(pickedCube.Trim().Split(" ").First());
                        var pickedCubeColor = pickedCube.Trim().Split(' ').Last();

                        if (pickedCubeColor.Equals("red"))
                        {
                            if(maxRed < pickedCubeCount)
                                maxRed = pickedCubeCount;
                        }
                        if (pickedCubeColor.Equals("green"))
                        {
                            if (maxGreen < pickedCubeCount)
                                maxGreen = pickedCubeCount;
                        }
                        if (pickedCubeColor.Equals("blue"))
                        {
                            if (maxBlue < pickedCubeCount)
                                maxBlue = pickedCubeCount;
                        }
                    }
                }

                if (Possible)
                    result += (maxRed * maxGreen * maxBlue);
            }

            Console.WriteLine(result);
        }
    }
}

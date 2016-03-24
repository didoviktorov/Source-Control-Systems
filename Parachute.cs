namespace _16.Parachute
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Parachute
    {
        static void Main()
        {
            string input = Console.ReadLine();
            List<string> linesToAdd = new List<string>();
            while (input != "END")
            {
                linesToAdd.Add(input);
                input = Console.ReadLine();
            }

            int rows = linesToAdd.Count;
            int cols = linesToAdd[0].Length;

            char[,] area = new char[rows, cols];
            int parachuteRow = 0;
            int parachuteCol = 0;
            for (int row = 0; row < rows; row++)
            {
                string line = linesToAdd[row];
                for (int col = 0; col < cols; col++)
                {
                    if (line[col] == 'o')
                    {
                        parachuteRow = row;
                        parachuteCol = col;
                    }

                    area[row, col] = line[col];
                }
            }
            char[] cliffs = { '/', '\\', '|' };
            char water = '~';
            char right = '>';
            char left = '<';
            char land = '_';
            for (int row = parachuteRow; row < rows; row++)
            {
                int leftWind = 0;
                int rightWind = 0;
                for (int col = 0; col < cols; col++)
                {
                    if (area[row + 1, col] == right)
                    {
                        rightWind++;
                    }
                    else if (area[row + 1, col] == left)
                    {
                        leftWind++;
                    }
                }

                if (leftWind > rightWind)
                {
                    parachuteCol -= leftWind - rightWind;
                    parachuteRow++;
                }
                else if (rightWind > leftWind)
                {
                    parachuteCol += rightWind - leftWind;
                    parachuteRow++;
                }
                else if (rightWind == leftWind)
                {
                    parachuteRow++;
                }
                
                char position = area[parachuteRow, parachuteCol];

                if (position.Equals(cliffs.FirstOrDefault(cliff => cliff == position)))
                {
                    Console.WriteLine("Got smacked on the rock like a dog!");
                    Console.WriteLine("{0} {1}", parachuteRow, parachuteCol);
                    break;
                }

                if (position == water)
                {
                    Console.WriteLine("Drowned in the water like a cat!");
                    Console.WriteLine("{0} {1}", parachuteRow, parachuteCol);
                    break;
                }

                if (position == land)
                {
                    Console.WriteLine("Landed on the ground like a boss!");
                    Console.WriteLine("{0} {1}", parachuteRow, parachuteCol);
                    break;
                }
            }
        }
    }
}

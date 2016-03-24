namespace IT_Village
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ITVillage
    {
        static int rows = 4;
        static int cols = 4;

        static readonly string[,] gameBoard = new string[rows, cols];

        private static int startingMoney = 50;

        static void Main()
        {
            int insOnTheBoard = 0;
            string[] fields = Console.ReadLine().Split('|');           
            for (int row = 0; row < rows; row++)
            {
                string[] rowTofill = fields[row].Split(new char[]{' '}, StringSplitOptions.RemoveEmptyEntries);
                for (int col = 0; col < cols; col++)
                {                   
                    gameBoard[row, col] = rowTofill[col];
                    if (rowTofill[col] == "I")
                    {
                        insOnTheBoard++;
                    }
                }
            }

            int[] startingCoordinates =
                Console.ReadLine()
                    .Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();
            
            int[] diceNumbers =
                Console.ReadLine()
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

            int startRow = startingCoordinates[0] - 1;
            int startCol = startingCoordinates[1] - 1;

            int movesLeft = diceNumbers.Length;
            int ownedInns = 0;

            for (int move = 0; move < diceNumbers.Length; move++)
            {
                int diceNumber = diceNumbers[move];
                movesLeft--;

                while (diceNumber > 0)
                {
                    if (startRow == 0 && startCol < cols - 1)
                    {
                        startCol++;
                        diceNumber--;
                    }
                    else if (startRow == 0 && startCol == cols - 1)
                    {
                        startRow++;
                        diceNumber--;
                    }
                    else if(startRow < rows - 1 && startCol == cols - 1)
                    {
                        startRow++;
                        diceNumber--;
                    }
                    else if (startRow == rows - 1 && startCol == cols - 1)
                    {
                        startCol--;
                        diceNumber--;
                    }
                    else if (startRow == rows - 1 && startCol > 0)
                    {
                        startCol--;
                        diceNumber--;
                    }
                    else if (startRow == rows - 1 && startCol == 0)
                    {
                        startRow--;
                        diceNumber--;
                    }
                    else if (startRow > 0 && startCol == 0)
                    {
                        startRow--;
                        diceNumber--;
                    }                  
                }

                ReciveInnsRent(ownedInns, insOnTheBoard);

                string symbol = gameBoard[startRow, startCol];

                switch (symbol)
                {
                    case "P":
                        startingMoney -= 5;
                        break;
                    case "F":
                        startingMoney += 20;
                        break;
                    case "S":
                        move += 2;
                        movesLeft -= 2;
                        break;
                    case "V":
                        startingMoney *= 10;
                        break;
                    case "I":
                        if (startingMoney > 100)
                        {
                            startingMoney -= 100;
                            ownedInns++;
                        }
                        else
                        {
                            startingMoney -= 10;
                        }
                        break;
                }

                if (startingMoney < 0)
                {
                    Console.WriteLine("<p>You lost! You ran out of money!<p>");
                    break;
                }

                if (ownedInns == insOnTheBoard)
                {
                    Console.WriteLine("<p>You won! You own the village now! You have {0} coins!<p>", startingMoney);
                    break;
                }

                if (movesLeft <= 0)
                {
                    Console.WriteLine("<p>You lost! No more moves! You have {0} coins!<p>", startingMoney);
                    break;
                }

                if (symbol == "N")
                {
                    Console.WriteLine("<p>You won! Nakov's force was with you!<p>");
                    break;
                }                                        
            }
        }

        private static void ReciveInnsRent(int ownedInns, int insOnTheBoard)
        {
            if (ownedInns != 0 && ownedInns < insOnTheBoard)
            {
                startingMoney += ownedInns * 20;
            }
        }
    }
}

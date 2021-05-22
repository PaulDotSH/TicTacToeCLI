using System;

namespace TicTacToeCLI
{
    class Program
    {
        enum GameTable
        {
           X,
           Zero,
           Empty
        }
        static uint BoardSize;

        static GameTable PlayerTurn = GameTable.X;

        static void Main()
        {
            start:
            Console.WriteLine("Enter the board size");

            //Get board size from user input
            try { BoardSize = UInt16.Parse(Console.ReadLine()); if (BoardSize == 0) throw new Exception(); } catch { Console.Clear(); Console.WriteLine("Wrong size"); goto start; }
            
            //We initialise the gametable
            GameTable[,] Table = new GameTable[BoardSize, BoardSize];

            for (int i = 0; i < BoardSize; i++)
                for (int j = 0; j < BoardSize; j++)
                    Table[i, j] = GameTable.Empty;

            do
            {
                if (PlayedTurns == BoardSize * BoardSize)
                {
                    IsDraw = true;
                    break;
                }
                Console.Clear();
                Draw(Table);
            } while (GetInput(Table) == GameTable.Empty);

            DisplayWinner(Table);

            Console.ReadKey();
        }
        static bool IsDraw = false;
        static uint PlayedTurns = 0;
        static void DisplayWinner(GameTable[,] Table)
        {
            Console.Clear();
            Draw(Table);
            if (IsDraw==false)
            {
                char Xor0;
                if (PlayerTurn == GameTable.X)
                    Xor0 = 'X';
                else
                    Xor0 = '0';
                Console.WriteLine($"Congrats player {Xor0}, you won" +
                    $"!");
            }
            else
                Console.WriteLine("There was a draw");
        }

        static GameTable GetInput(GameTable[,] Table)
        {
            PlayedTurns++;
            char Xor0;
            if (PlayerTurn == GameTable.X)
                Xor0 = 'X';
            else
                Xor0 = '0';
            Console.WriteLine($"Chose the position where you want to put {Xor0} (from 1 to {BoardSize * BoardSize})");
        GetInput:
            uint Aux = 0, i = 0, j = 0;


            //We check if the position is valid
            try { uint Position = UInt16.Parse(Console.ReadLine());

                if (Position == 0 || Position > BoardSize * BoardSize)
                    throw new Exception();
                for (i = 0; i < BoardSize; i++)
                {
                    for (j = 0; j < BoardSize; j++)
                    {
                        Aux++;
                        if (Aux == Position)
                        {
                            if (Table[i, j] == GameTable.Empty)
                                Table[i, j] = PlayerTurn;
                            else throw new Exception();
                            goto Won;
                        }
                    }
                }

            } catch { Console.WriteLine("Please input a valid spot!"); goto GetInput; }

        Won:
            //We check the main diagonal of the matrix
            if (i == j)
            {
                Aux = 0;
                for (int k = 0; k < BoardSize; k++)
                {
                    if (Table[k, k] == PlayerTurn)
                        Aux++;
                    else
                        break;
                }
                if (Aux == BoardSize)
                    return PlayerTurn;
            }

            //We check the secondary diagonal of the matrix
            if (i + j == BoardSize - 1)
            {
                Aux = 0;
                for (int k = 0; k < BoardSize; k++)
                {
                    if (Table[k, BoardSize-k-1] == PlayerTurn)
                        Aux++;
                    else
                        break;
                }
                if (Aux == BoardSize)
                    return PlayerTurn;
            }

            //We check the line the user placed his character this turn
            Aux = 0;
            for (int k=0; k<BoardSize; k++)
            {
                if (Table[i, k] == PlayerTurn)
                    Aux++;
                else
                    break;
            }
            if (Aux == BoardSize)
                return PlayerTurn;

            //We check the column the user placed his character this turn
            Aux = 0;
            for (int k = 0; k < BoardSize; k++)
            {
                if (Table[k, j] == PlayerTurn)
                    Aux++;
                else
                    break;
            }
            if (Aux == BoardSize)
                return PlayerTurn;

            //Switch turns
            if (PlayerTurn == GameTable.X)
                PlayerTurn = GameTable.Zero;
            else
                PlayerTurn = GameTable.X;

            return GameTable.Empty;
        }

        static void Draw(GameTable[,] Table)
        {
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    switch (Table[i,j])
                    {
                        case GameTable.Empty:
                            Console.Write(" ");
                            break;
                        case GameTable.X:
                            Console.Write("X");
                            break;
                        case GameTable.Zero:
                            Console.Write("0");
                            break;
                    }
                    if (j!=BoardSize-1)
                        Console.Write($"|");
                }
                Console.Write("\n");
            }
        }
    }
}

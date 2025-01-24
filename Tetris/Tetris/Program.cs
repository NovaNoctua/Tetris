using System;
using System.Threading;

///ETML
///Author : Maël Naudet
///Date : 17.01.2025
///Description : Tetris game in console mode
///*******************************************************************************************

namespace Tetris
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Start.Introduction();
            Config.GameSize();

          
            GameGrid grid = new GameGrid(55, 45);


            Square square = new Square(5, 5, 2);
            Square square2 = new Square(5, 5, 3);
            square.XPosition = 6; 
            square.YPosition = 6;
            square2.XPosition = 9;
            square2.YPosition = 6;


            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                grid.Display();
                square.Display();
                square2.Display();
                Thread.Sleep(100);
                square.Erase();
                square2.Erase();
                square2.YPosition++;
                square.YPosition++;

                Thread.Sleep(100);
            }

            Console.ReadLine();
        }
    }
}

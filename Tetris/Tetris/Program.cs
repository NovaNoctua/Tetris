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

          
            Grid grid = new Grid(55, 45);


            Square square = new Square();
            Square square2 = new Square();
            square.XPosition = 6; 
            square.YPosition = 6;
            square2.XPosition = 9;
            square2.YPosition = 6;


            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                grid.Display();
                square.Display(2);
                square2.Display(3);
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

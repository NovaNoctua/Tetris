using System;
using System.Collections.Generic;
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


            Block carre = new BlockL(6, 6);



            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                grid.Display();
                carre.Display();               
                Thread.Sleep(1000);
                carre.Erase();
                carre.Rotate();
            }
        }
    }
}

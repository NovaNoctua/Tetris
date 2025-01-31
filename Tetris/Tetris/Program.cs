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

            Game game = new Game();

            game.Initialize();

            while(true)
            {
                game.GameLoop();
            }
             
        }
    }
}

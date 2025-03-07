/// *******************************************************************************************
/// ETML
/// Author : Maël Naudet
/// Date : 17.01.2025
/// Description : Tetris game in console mode
/// *******************************************************************************************
/// 
using System;

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

            while(!game.endGame)
            {
                game.GameLoop();
            }
            game.EndScreen();
        }
    }
}

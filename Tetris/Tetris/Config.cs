/// *******************************************************************************************
/// ETML
/// Author : Maël Naudet
/// Date : 17.01.2025
/// *******************************************************************************************

using System;

namespace Tetris
{
    /// <summary>
    /// configurates the screen size
    /// </summary>
    static class Config
    {
        public const int SCREEN_HEIGHT = 55;
        public const int SCREEN_WIDTH = 150;

        public static void GameSize()
        {
            Console.SetWindowSize(SCREEN_WIDTH, SCREEN_HEIGHT);
            Console.SetBufferSize(SCREEN_WIDTH, SCREEN_HEIGHT);
        }
    }
}

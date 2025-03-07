/// *******************************************************************************************
/// ETML
/// Author : Maël Naudet
/// Date : 31.01.2025
/// *******************************************************************************************

using System;

namespace Tetris
{
    /// <summary>
    /// Customs the color of the blocks
    /// </summary>
    internal static class Custom
    {
        private static Random random = new Random();

        public static int GetRandomColorIndex()
        {
            return random.Next(Colors.Length);
        }

        public static readonly ConsoleColor[] Colors = {
            ConsoleColor.DarkBlue,
            ConsoleColor.DarkGreen,
            ConsoleColor.DarkCyan,
            ConsoleColor.DarkRed,
            ConsoleColor.DarkMagenta,
            ConsoleColor.DarkYellow,
            ConsoleColor.Blue,
            ConsoleColor.Green,
            ConsoleColor.Cyan,
            ConsoleColor.Red,
            ConsoleColor.Magenta,
            ConsoleColor.Yellow
        };

    }
}

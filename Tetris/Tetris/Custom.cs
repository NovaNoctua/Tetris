using System;

namespace Tetris
{
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

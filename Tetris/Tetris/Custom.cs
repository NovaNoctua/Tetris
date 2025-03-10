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
        // Déclaration et initialisation des attributs ***************************************

        private static Random random = new Random();        // permet l'aléatoire

        public static readonly ConsoleColor[] Colors = {    // couleur aléatoire de la console
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

        // Déclaration et implémentation des méthodes ****************************************

        /// <summary>
        /// Retourne un index aléatoire de Colors
        /// </summary>
        /// <returns></returns>
        public static int GetRandomColorIndex()
        {
            return random.Next(Colors.Length);
        }

        

    }
}

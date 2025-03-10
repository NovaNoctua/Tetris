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
        // Déclaration et initialisation des constantes **********************************
        public const int SCREEN_HEIGHT = 55;    // hauteur de l'écran
        public const int SCREEN_WIDTH = 150;    // largeur de l'écran

        // Déclaration et implémentation des méthodes ************************************

        /// <summary>
        /// Calibre la taille de l'écran et le buffer
        /// </summary>
        public static void GameSize()
        {
            Console.SetWindowSize(SCREEN_WIDTH, SCREEN_HEIGHT);
            Console.SetBufferSize(SCREEN_WIDTH, SCREEN_HEIGHT);
        }
    }
}

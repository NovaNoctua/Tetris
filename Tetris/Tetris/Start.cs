/// *******************************************************************************************
/// ETML
/// Author : Maël Naudet
/// Date : 17.01.2025
/// *******************************************************************************************

using System;

namespace Tetris
{
    /// <summary>
    /// Starting Screen
    /// </summary>
    internal static class Start
    {
        // Déclaration et implémentation des méthodes ***********************************
        public static void Introduction()
        {
            Console.WriteLine("\t╔═══════════════════════════════════════════════╗");
            Console.WriteLine("\t║\t  Bienvenue dans le jeu Tetris   \t║");
            Console.WriteLine("\t║\t  Réalisé par Maël Naudet        \t║");
            Console.WriteLine("\t╚═══════════════════════════════════════════════╝\n\n");
            Console.WriteLine("\tAppuyez sur n'importe quelle touche pour commencer le jeu...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}

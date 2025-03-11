/// *******************************************************************************************
/// ETML
/// Author : Maël Naudet
/// Date : 17.01.2025
/// Description : Tetris game in console mode
/// *******************************************************************************************
 
using System;

namespace Tetris
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Déclaration et initialisation de la variable de jeu
            Game game = new Game();

            // Programme principal *****************************************

            // Introduction et mise en place des paramètres de jeu de la fenêtre
            Console.CursorVisible = false;
            Start.Introduction();
            Config.GameSize();

            // Initialisation du jeu
            game.Initialize();

            // boucle de jeu
            while(!game.endGame)
            {
                game.GameLoop();
            }

            // fin du jeu
            game.EndScreen();
        }
    }
}

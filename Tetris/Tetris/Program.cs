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
            ConsoleExtensions.DisableEcho();

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

    public static class ConsoleExtensions
    {
        const int STD_INPUT_HANDLE = -10;
        const uint ENABLE_ECHO_INPUT = 0x0004;

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        static extern IntPtr GetStdHandle(int nStdHandle);

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

        public static void DisableEcho()
        {
            var handle = GetStdHandle(STD_INPUT_HANDLE);
            if (GetConsoleMode(handle, out uint mode))
                SetConsoleMode(handle, mode & ~ENABLE_ECHO_INPUT);
        }
    }
}

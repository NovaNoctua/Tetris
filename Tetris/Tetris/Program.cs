/// *******************************************************************************************
/// ETML
/// Author : Maël Naudet
/// Date : 17.01.2025
/// Description : Tetris game in console mode
/// *******************************************************************************************
 
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
            //ConsoleExtensions.DisableEcho();

            // Initialisation du jeu
            game.Initialize();

            // boucle de jeu
            while(!game.endGame)
            {
                game.GameLoop();
            }

            // fin du jeu
            game.EndScreen();

            SendAPI(game).Wait();

            Console.ReadLine();

        }

        public static async Task SendAPI(Game game)
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true); // vide le buffer sans afficher
            }
            //ConsoleExtensions.EnableEcho();
            Console.CursorVisible = true;
            Console.SetCursorPosition(10 + game.grid.Row * 3, 25);
            Console.Write("Inscrivez votre nom : ");

            string player = Console.ReadLine();
            int score = game.score;
            int linesDestroyed = game.linesDestroyed;
            Console.CursorVisible = false;

            // Construct JSON
            string json = $"{{\"player\":\"{player}\",\"score\":{score},\"linesDestroyed\":{linesDestroyed}}}";

            using (var client = new HttpClient())
            {
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage response = await client.PostAsync("http://localhost:3333/highscores", content);
                    string result = await response.Content.ReadAsStringAsync();

                    Console.SetCursorPosition(10 + game.grid.Row * 3, 26);
                    Console.WriteLine("Score envoyé !");
                }
                catch (Exception ex)
                {
                    Console.SetCursorPosition(10 + game.grid.Row * 3, 26);
                    Console.WriteLine("Erreur API : " + ex.Message);
                }
            }
        }



    }

    //public static class ConsoleExtensions
    //{
    //    const int STD_INPUT_HANDLE = -10;
    //    const uint ENABLE_ECHO_INPUT = 0x0004;

    //    [System.Runtime.InteropServices.DllImport("kernel32.dll")]
    //    static extern IntPtr GetStdHandle(int nStdHandle);

    //    [System.Runtime.InteropServices.DllImport("kernel32.dll")]
    //    static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

    //    [System.Runtime.InteropServices.DllImport("kernel32.dll")]
    //    static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

    //    public static void DisableEcho()
    //    {
    //        var handle = GetStdHandle(STD_INPUT_HANDLE);
    //        if (GetConsoleMode(handle, out uint mode))
    //            SetConsoleMode(handle, mode & ~ENABLE_ECHO_INPUT);
    //    }

    //    public static void EnableEcho()
    //    {
    //        var handle = GetStdHandle(STD_INPUT_HANDLE);
    //        if (GetConsoleMode(handle, out uint mode))
    //            SetConsoleMode(handle, mode | ENABLE_ECHO_INPUT);
    //    }

    //}
}

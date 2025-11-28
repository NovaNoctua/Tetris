/// *******************************************************************************************
/// ETML
/// Author : Maël Naudet
/// Date : 17.01.2025
/// Description : Tetris game in console mode
/// *******************************************************************************************
 
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
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

            Thread.Sleep(2000);

            System.Environment.Exit(1);

        }

        public static async Task SendAPI(Game game)
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true); // vide le buffer sans afficher
            }
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
                    Console.SetCursorPosition(10 + game.grid.Row * 3, 27);
                    Console.WriteLine("Fermeture du jeu...");
                }
                catch (Exception ex)
                {
                    Console.SetCursorPosition(10 + game.grid.Row * 3, 26);
                    Console.WriteLine("Erreur API : " + ex.Message);
                }
            }
        }
    }
}

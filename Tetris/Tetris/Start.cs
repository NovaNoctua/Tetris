using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    internal static class Start
    {
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

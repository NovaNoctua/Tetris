/// *******************************************************************************************
/// ETML
/// Author : Maël Naudet
/// Date : 07.02.2025
/// *******************************************************************************************

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;


namespace Tetris
{
    /// <summary>
    /// Main logic for the game
    /// </summary>
    internal class Game
    {
        // Déclaration et initialisation des constantes *********************************
        private const int _VK_SPACE = 0x20;                         // clé virtuelle de la barre espace
        private const int _VK_LEFT = 0x25;                          // clé virtuelle de la flèche gauche
        private const int _VK_RIGHT = 0x27;                         // clé virtuelle de la flèche droite
        private const int _VK_DOWN = 0x28;                          // clé virtuelle de la flèche du bas
        private const byte _STARTING_Y_POSITION = 0;                // position y de commencement
        private const byte _STARTING_X_POSITION = _WIDTH / 2 - 2;   // position x de commencement
        private const byte _WIDTH = 15;                             // largeur de la grille
        private const byte _HEIGHT = 20;                            // hauteur de la grille
        private const int _SCORE_ADDER = 500;                       // nombre de score par ligne détruite

        // Déclaration et initialisation des attributs **********************************
        private GameGrid grid;                                          // grille de jeu
        private int blockFallsAfter = 500;                              // temps de cooldown avant qu'un bloc tombe
        private int cooldownMovement = 100;                             // temps de cooldown avant chaque input
        private DateTime startCoolDown;                                 // début du cooldown
        private DateTime endCoolDown;                                   // fin du cooldown
        private DateTime startOfTurn;                                   // début du tour
        private DateTime endOfTurn;                                     // fin du tour
        private readonly List<Block> allBlocks = new List<Block>();     // liste de tous les blocs
        public bool endGame;                                            // fin du jeu
        private int _linesDestroyed;                                    // lignes détruites
        private int score;                                              // score du jeu
        private Block _nextBlock;                                       // prochain bloc

        private bool wasPaused = false;
        public bool IsPaused { get; private set; } = false;             // Pause

        // Déclaration et implémentation des méthodes ***********************************

        /// <summary>
        /// Initialise les principales variables et affiche toutes les choses importantes
        /// </summary>
        public void Initialize()
        {
            grid = new GameGrid(_WIDTH, _HEIGHT);
            grid.Display();
            endGame = false;
            ShowScore();
            _nextBlock = Block.GetRandomBlock(_WIDTH + 2, 1);
        }

        /// <summary>
        /// Boucle de jeu à faire tourner ne boucle
        /// </summary>
        public void GameLoop()
        {
            // bloc en prenant le prochain
            Block blockFalling = _nextBlock;
            blockFalling.MoveBlock(_STARTING_X_POSITION, _STARTING_Y_POSITION);

            // on remet le prochain bloc
            _nextBlock = Block.GetRandomBlock(_WIDTH + 2, 2);
            ShowNextBlock();

            // ajoute le bloc au bon endroit
            allBlocks.Add(blockFalling);
            grid.CreateBlockInGrid(blockFalling);

            // s'il peut être placé on continue le jeu
            if (!grid.CanBlockFit(blockFalling, 0, 1))
            {
                endGame = true;
            }

            // début des cooldowns
            startOfTurn = DateTime.Now;
            startCoolDown = DateTime.Now;

            // tant que le bloc peut descendre
            while (grid.CanBlockFit(blockFalling, 0, 1))
            {
                // début la fin du tour
                endOfTurn = DateTime.Now;
                endCoolDown = DateTime.Now;

                // affiche le bloc
                blockFalling.Display();

                // --- Gestion de la touche P ---
                if ((GetAsyncKeyState(0x50) & 0x8000) != 0)
                {
                    TogglePause();
                    Thread.Sleep(150);
                }

                // --- Affichage pause une seule fois ---
                if (IsPaused && !wasPaused)
                {
                    wasPaused = true;

                    Console.ForegroundColor = ConsoleColor.White;
                    string pauseText = "=== PAUSE ===";
                    Console.SetCursorPosition(6 + (_WIDTH * 3 - pauseText.Length) / 2, 6 + _HEIGHT);
                    Console.Write(pauseText);
                }

                // --- Tout se fige en pause ---
                if (IsPaused)
                {
                    Thread.Sleep(50);
                    continue;
                }

                // --- Sortie de pause : redessine proprement une seule fois ---
                if (!IsPaused && wasPaused)
                {
                    wasPaused = false;
                    DisplayAllBlocks();
                    ShowScore();
                    ShowNextBlock();
                }

                // flèche de droite
                if ((GetAsyncKeyState(_VK_RIGHT) & 0x8000) != 0 && (endCoolDown - startCoolDown).TotalMilliseconds > cooldownMovement)
                    {
                        // s'il peut aller à droite
                        if (grid.CanBlockFit(blockFalling, 1, 0))
                        {
                            // bouge le bloc à droite commence le cooldown et efface le précédent bloc
                            grid.MoveBlock(blockFalling, 1, 0);
                            blockFalling.Erase();
                            blockFalling.Move(3, 0);
                            blockFalling.Display();
                            startCoolDown = DateTime.Now;
                        }
                    }

                    // flèche de gauche
                    else if ((GetAsyncKeyState(_VK_LEFT) & 0x8000) != 0 && (endCoolDown - startCoolDown).TotalMilliseconds > cooldownMovement)
                    {
                        // bouge le bloc à gauche si c'est possible
                        if (grid.CanBlockFit(blockFalling, -1, 0))
                        {
                            grid.MoveBlock(blockFalling, -1, 0);
                            blockFalling.Erase();
                            blockFalling.Move(-3, 0);
                            blockFalling.Display();
                            startCoolDown = DateTime.Now;
                        }
                    }

                    // flèche du bas
                    else if ((GetAsyncKeyState(_VK_DOWN) & 0x8000) != 0 && (endCoolDown - startCoolDown).TotalMilliseconds > cooldownMovement)
                    {
                        // bouge le bloc en bas si c'est possible
                        if (grid.CanBlockFit(blockFalling, 0, 1))
                        {
                            grid.MoveBlock(blockFalling, 0, 1);
                            blockFalling.Erase();
                            blockFalling.Move(0, 2);
                            blockFalling.Display();
                            startCoolDown = DateTime.Now;
                        }
                    }

                    // barre espace
                    else if ((GetAsyncKeyState(_VK_SPACE) & 0x8000) != 0 && (endCoolDown - startCoolDown).TotalMilliseconds > cooldownMovement)
                    {
                        // fais rotationner le bloc si c'est possible
                        if (grid.CanBlockRotate(blockFalling))
                        {
                            grid.RotateBlock(blockFalling);
                            startCoolDown = DateTime.Now;
                        }
                    }
                
                // après le cooldown, on finit le tour et on descend le bloc de 1
                if (grid.CanBlockFit(blockFalling, 0, 1) && (endOfTurn - startOfTurn).Milliseconds > blockFallsAfter)
                {
                    grid.MoveBlock(blockFalling, 0, 1);
                    blockFalling.Erase();
                    blockFalling.Move(0, 2);
                    blockFalling.Display();
                    startOfTurn = DateTime.Now;
                }

                
            }

            // si y'a des lignes à détruire, on les détruit
            _linesDestroyed += grid.ClearFullRow();

            // augmente le score si des lignes ont été détruites
            if (_linesDestroyed > 0)
            {
                Thread.Sleep(500);
                DisplayAllBlocks();
                score += _linesDestroyed * _SCORE_ADDER;
                blockFallsAfter = Math.Max(blockFallsAfter - 10, 100);
                _linesDestroyed = 0;             
                ShowScore();
            }
        }

        /// <summary>
        /// Fin du jeu
        /// </summary>
        public void EndScreen()
        {
            // Affichage d'un message de fin de jeu avec le score et le nombre de lignes détruites
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(10 + grid.Row * 3, 5);
            Console.Write("Dommage, vous avez perdu, la prochaine pièce n'a pas pu être placée");
            Console.SetCursorPosition(10 + grid.Row * 3, 6);
            Console.Write($"Vous avez fait un score de {score}.");
            Console.SetCursorPosition(10 + grid.Row * 3, 6);
            Console.Write($"Nombre de lignes détruite(s) : {score / _SCORE_ADDER}");

            Console.ReadLine();
        }

        /// <summary>
        /// Permet de savoir en permanence si une touche est appuyée ou non
        /// </summary>
        /// <param name="vKey">clé virtuelle</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        /// <summary>
        /// Affiche tous les blocs
        /// </summary>
        private void DisplayAllBlocks()
        {
            // Clear the entire grid area first
            ClearConsoleGrid();
            UpdateSquarePosition();

            // Re-draw based on the SquareGrid
            for (int i = 0; i < grid.Row; i++)
            {
                for (int j = 0; j < grid.Column; j++)
                {
                    Square square = grid.SquareGrid[i, j];
                    if (square != null)
                    {
                        square.Display();
                    }
                }
            }
        }

        /// <summary>
        /// Efface tous les blocs dans la grille de jeu
        /// </summary>
        private void ClearConsoleGrid()
        {
            for (int i = 0; i < grid.Column * 2; i++)
            {
                for (int j = 0; j < grid.Row * 3; j++)
                {
                    Console.SetCursorPosition(6 + j, 6 + i); // Adjusting to grid position
                    Console.Write(" "); // Clear with spaces
                }
            }
        }

        /// <summary>
        /// Mets à jour la position de square
        /// </summary>
        private void UpdateSquarePosition()
        {
            for (int i = 0; i < grid.Row; i++)
            {
                for (int j = 0; j < grid.Column; j++)
                {
                    if (grid.SquareGrid[i, j] != null)
                    {
                        grid.SquareGrid[i, j].Position = new Position(6 + 3 * i, 6 + 2 * j);
                    }                   
                }
            }
        }

        /// <summary>
        /// Affiche les scores
        /// </summary>
        private void ShowScore()
        {
            Console.ForegroundColor = ConsoleColor.White;
            DestroyScore();
            Console.SetCursorPosition(grid.Row * 3 - 3, 3);
            Console.Write($"Score : {score}");
            Console.SetCursorPosition(grid.Row * 3 - 24, 4);
            Console.Write($"Nombre de lignes détruites : {score / 500}");
        }

        /// <summary>
        /// Efface les scores
        /// </summary>
        private void DestroyScore()
        {
            Console.SetCursorPosition(grid.Row * 3 - 3, 3);
            Console.Write("        ");
        }

        /// <summary>
        /// Affiche le prochain bloc à droite de la grille
        /// </summary>
        private void ShowNextBlock()
        {
            Console.ForegroundColor= ConsoleColor.White;
            Console.SetCursorPosition(7 + _WIDTH * 3, 8);
            Console.Write("|---------------|");

            for (int i = 9; i < 19; i++)
            {
                Console.SetCursorPosition(7 + _WIDTH * 3, i);
                Console.Write("|               |");
            }

            Console.SetCursorPosition(7 + _WIDTH * 3, 19);
            Console.Write("|---------------|");
            Console.SetCursorPosition(7 + _WIDTH * 3, 20);
            Console.Write("  PROCHAIN BLOC");

            _nextBlock.Display();
        }

        /// <summary>
        /// Toggle la pause
        /// </summary>
        public void TogglePause()
        {
            IsPaused = !IsPaused;
        }

    }
}

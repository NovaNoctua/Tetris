using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Data.Common;

namespace Tetris
{
    internal class Game
    {
        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);
        private const int VK_SPACE = 0x20;
        private const int VK_LEFT = 0x25;
        private const int VK_RIGHT = 0x27;       
        private const int VK_DOWN = 0x28;
        private GameGrid grid;
        private int blockFallsAfter = 500;
        private int coolDownMovement = 100;
        private DateTime startCoolDown;
        private DateTime endCoolDown;
        private DateTime startOfTurn;
        private DateTime endOfTurn;
        private readonly List<Block> allBlocks = new List<Block>();
        public bool endGame;
        private int _linesDestroyed;
        private int score;
        private Block _nextBlock;


        private const byte STARTING_Y_POSITION = 0;
        private const byte STARTING_X_POSITION = 7;

        public int LinesDestroyed 
        {
            get
            {
                return _linesDestroyed;
            }
            set
            {
                _linesDestroyed = value;
            }
        }
        public void Initialize()
        {
            grid = new GameGrid(20, 20);
            grid.Display();
            endGame = false;
            ShowScore();
            _nextBlock = Block.GetRandomBlock(STARTING_X_POSITION, STARTING_Y_POSITION);
        }

        public void GameLoop()
        {
            Block blockFalling = _nextBlock;
            _nextBlock = Block.GetRandomBlock(STARTING_X_POSITION, STARTING_Y_POSITION);
            allBlocks.Add(blockFalling);
            grid.CreateBlockInGrid(blockFalling);
            if (!grid.CanBlockFit(blockFalling, 0, 1))
            {
                endGame = true;
            }
            startOfTurn = DateTime.Now;
            startCoolDown = DateTime.Now;
            while (grid.CanBlockFit(blockFalling, 0, 1))
            {
                endOfTurn = DateTime.Now;
                endCoolDown = DateTime.Now;
                blockFalling.Display();

                if ((GetAsyncKeyState(VK_RIGHT) & 0x8000) != 0 && (endCoolDown - startCoolDown).TotalMilliseconds > coolDownMovement)
                {
                    if (grid.CanBlockFit(blockFalling, 1, 0))
                    {
                        grid.MoveBlock(blockFalling, 1, 0);
                        blockFalling.Erase();
                        blockFalling.Move(3, 0);
                        blockFalling.Display();
                        startCoolDown = DateTime.Now;
                    }
                }
                else if ((GetAsyncKeyState(VK_LEFT) & 0x8000) != 0 && (endCoolDown - startCoolDown).TotalMilliseconds > coolDownMovement)
                {
                    if (grid.CanBlockFit(blockFalling, -1, 0))
                    {
                        grid.MoveBlock(blockFalling, -1, 0);
                        blockFalling.Erase();
                        blockFalling.Move(-3, 0);
                        blockFalling.Display();
                        startCoolDown = DateTime.Now;
                    }
                }
                else if ((GetAsyncKeyState(VK_DOWN) & 0x8000) != 0 && (endCoolDown - startCoolDown).TotalMilliseconds > coolDownMovement)
                {
                    if (grid.CanBlockFit(blockFalling, 0, 1))
                    {
                        grid.MoveBlock(blockFalling, 0, 1);
                        blockFalling.Erase();
                        blockFalling.Move(0, 2);
                        blockFalling.Display();
                        startCoolDown = DateTime.Now;
                    }
                }
                else if ((GetAsyncKeyState(VK_SPACE) & 0x8000) != 0 && (endCoolDown - startCoolDown).TotalMilliseconds > coolDownMovement)
                {
                    if (grid.CanBlockRotate(blockFalling))
                    {
                        grid.RotateBlock(blockFalling);
                        startCoolDown = DateTime.Now;
                    }
                }


                if (grid.CanBlockFit(blockFalling, 0, 1) && (endOfTurn - startOfTurn).Milliseconds > blockFallsAfter)
                {
                    grid.MoveBlock(blockFalling, 0, 1);
                    blockFalling.Erase();
                    blockFalling.Move(0, 2);
                    blockFalling.Display();
                    startOfTurn = DateTime.Now;
                }

                
            }
            LinesDestroyed += grid.ClearFullRow();

            if (LinesDestroyed > 0)
            {
                DisplayAllBlocks();
                score += LinesDestroyed * 500;
                LinesDestroyed = 0;             
                ShowScore();
            }
        }
        public void EndScreen()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(10 + grid.Row * 3, 5);
            Console.Write("Dommage, vous avez perdu, la prochaine pièce n'a pas pu être placée");
            Console.SetCursorPosition(10 + grid.Row * 3, 6);
            Console.Write($"Vous avez fait un score de {score}.");
            Console.SetCursorPosition(10 + grid.Row * 3, 6);
            Console.Write($"Nombre de lignes détruite(s) : {score / 500}");

            Console.ReadLine();
        }
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

        private void UpdateSquarePosition()
        {
            for (int i = 0; i < grid.Row; i++)
            {
                for (int j = 0; j < grid.Column; j++)
                {
                    if (grid.SquareGrid[i, j] != null)
                    {
                        grid.SquareGrid[i, j].position = new Position(6 + 3 * i, 6 + 2 * j);
                    }                   
                }
            }
        }

        private void ShowScore()
        {
            Console.ForegroundColor = ConsoleColor.White;
            DestroyScore();
            Console.SetCursorPosition(grid.Row * 3 - 3, 3);
            Console.Write($"Score : {score}");
        }
        private void DestroyScore()
        {
            Console.SetCursorPosition(grid.Row * 3 - 3, 3);
            Console.Write("        ");
        }
    }
}

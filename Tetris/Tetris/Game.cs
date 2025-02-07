using System;
using System.Collections.Generic;
using System.Threading;

namespace Tetris
{
    internal class Game
    {
        private GameGrid grid;
        private List<Block> allBlocks = new List<Block>();
        private bool endGame;

        private const byte STARTING_Y_POSITION = 0;
        private const byte STARTING_X_POSITION = 7;


        public void Initialize()
        {
            grid = new GameGrid(20, 20);
            grid.Display();
            endGame = false;
        }

        public void GameLoop()
        {
            Block blockFalling = GetRandomBlock();
            allBlocks.Add(blockFalling);
            grid.CreateBlockInGrid(blockFalling);
            if (!grid.CanBlockFit(blockFalling, 0, 1))
            {
                endGame = true;
            }

            while(grid.CanBlockFit(blockFalling, 0, 1))
            {
                grid.DisplayGridInt();
                blockFalling.Display();


                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInputUser = Console.ReadKey(true);
                    switch (keyInputUser.Key)
                    {
                        case ConsoleKey.RightArrow:
                            if (grid.CanBlockFit(blockFalling, 1, 0))
                            {
                                grid.MoveBlock(blockFalling, 1, 0);
                                blockFalling.Erase();
                                blockFalling.Move(3, 0);
                                blockFalling.Display();
                            }
                            break;

                        case ConsoleKey.LeftArrow:
                            if (grid.CanBlockFit(blockFalling, -1, 0))
                            {
                                grid.MoveBlock(blockFalling, -1, 0);
                                blockFalling.Erase();
                                blockFalling.Move(-3, 0);
                                blockFalling.Display();
                            }
                            break;

                        case ConsoleKey.UpArrow:
                            blockFalling.Erase();
                            blockFalling.Rotate();
                            blockFalling.Display();
                            break;

                        case ConsoleKey.DownArrow:
                            if (grid.CanBlockFit(blockFalling, 0, 1))
                            {
                                grid.MoveBlock(blockFalling, 0, 1);
                                blockFalling.Erase();
                                blockFalling.Move(0, 2);
                                blockFalling.Display();

                            }
                            break;

                        default:
                            break;
                    }
                }
                Thread.Sleep(500);
                if (grid.CanBlockFit(blockFalling, 0, 1))
                {
                    grid.MoveBlock(blockFalling, 0, 1);
                    blockFalling.Erase();
                    blockFalling.Move(0, 2);
                    blockFalling.Display();
                }
            }

            if (endGame)
            {
                Console.Clear();
                Console.WriteLine("FINITO");
            }
        }

        private Block GetRandomBlock()
        {
            Random random = new Random();
            int index = random.Next(0, 7);  // Il y a 7 types de blocs
            switch (index)
            {
                case 0: return new BlockI(STARTING_X_POSITION, STARTING_Y_POSITION);
                case 1: return new BlockJ(STARTING_X_POSITION, STARTING_Y_POSITION);
                case 2: return new BlockL(STARTING_X_POSITION, STARTING_Y_POSITION);
                case 3: return new BlockO(STARTING_X_POSITION, STARTING_Y_POSITION);
                case 4: return new BlockS(STARTING_X_POSITION, STARTING_Y_POSITION);
                case 5: return new BlockT(STARTING_X_POSITION, STARTING_Y_POSITION);
                case 6: return new BlockZ(STARTING_X_POSITION, STARTING_Y_POSITION);
                default: throw new Exception("Unexpected block type");
            }
        }
    }
}

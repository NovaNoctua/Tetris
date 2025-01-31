using System;
using System.Threading;

namespace Tetris
{
    internal class Game
    {
        private GameGrid grid;
        private Block[] blocks;
        private Block blockFalling;


        public void Initialize()
        {
            grid = new GameGrid(55, 45);

            blocks = new Block[]
            {
                new BlockI(6, 6),
                new BlockJ(6, 6),
                new BlockL(6, 6),
                new BlockO(6, 6),
                new BlockS(6, 6),
                new BlockT(6, 6),
                new BlockZ(6, 6),
            };

            grid.Display();
        }

        public void GameLoop()
        {
            blockFalling = blocks[new Random().Next(0, blocks.Length)];


            do
            {
                blockFalling.Display();

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInputUser = Console.ReadKey(true);
                    switch (keyInputUser.Key)
                    {

                        case ConsoleKey.RightArrow:
                            blockFalling.Erase();
                            blockFalling.Move(3, 0);
                            blockFalling.Display();
                            break;

                        case ConsoleKey.LeftArrow:
                            blockFalling.Erase();
                            blockFalling.Move(-3, 0);
                            blockFalling.Display();
                            break;

                        case ConsoleKey.UpArrow:
                            blockFalling.Erase();
                            blockFalling.Rotate();
                            blockFalling.Display();
                            break;
                        case ConsoleKey.DownArrow:
                            blockFalling.Erase();
                            blockFalling.Move(0, 2);
                            blockFalling.Display();
                            break;
                    }
                }       
                Thread.Sleep(500);
                blockFalling.Erase();
                //blockFalling.Move(0, 2);
            } while (true);
        }
    }
}

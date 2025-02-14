using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

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
            Block blockFalling = Block.GetRandomBlock(STARTING_X_POSITION, STARTING_Y_POSITION);
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
                grid.DisplayGridInt();
                blockFalling.Display();

                if((GetAsyncKeyState(VK_RIGHT) & 0x8000) != 0 && (endCoolDown - startCoolDown).TotalMilliseconds > coolDownMovement)
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
                else if((GetAsyncKeyState(VK_LEFT) & 0x8000) != 0 && (endCoolDown - startCoolDown).TotalMilliseconds > coolDownMovement)
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
                else if((GetAsyncKeyState(VK_DOWN) & 0x8000) != 0 && (endCoolDown - startCoolDown).TotalMilliseconds > coolDownMovement)
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
                else if((GetAsyncKeyState(VK_SPACE) & 0x8000) != 0 && (endCoolDown - startCoolDown).TotalMilliseconds > coolDownMovement)
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

            if (endGame)
            {
                Console.Clear();
                Console.WriteLine("FINITO");
            }
        }        
    }
}

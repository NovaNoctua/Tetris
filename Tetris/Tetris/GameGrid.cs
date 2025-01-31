using System;

///ETML
///Auteur : Maël Naudet
///Date : 17.01.2025

namespace Tetris
{
    /// <summary>
    /// Game's grid where the pieces will evolve in
    /// </summary>
    internal class GameGrid
    {

        //properties
        private readonly int _row;          
        private readonly int _column;
        private int[,] _grid;                //where are each blocks
        private int _cursorXPosition = 5;    //the x position of the cursor
        private int _cursorYPosition = 5;    //the y position of the cursor

        public int Row
        {
            get
            {
                return _row;
            }
        }

        public int Column
        {
            get
            {
                return _column;
            }
        }

        public int[,] Grid
        {
            get
            {
                return _grid;
            }
        }


        //custom constructor
        public GameGrid(int row, int column)
        {
            _row = row;
            _column = column;

            _grid = new int[Row, Column];
        }

        /// <summary>
        /// See if something is inside the grid
        /// </summary>
        /// <param name="r">row</param>
        /// <param name="c">column</param>
        /// <returns></returns>
        public bool IsInside(int r, int c)
        {
            return r >= 0 && r < Row && c >= 0 && c <= Column;
        }

        /// <summary>
        /// Check if cell is empty
        /// </summary>
        /// <param name="r"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool isEmpty(int r, int c)
        {
            return IsInside(r, c) && Grid[r, c] == 0;
        }

        /// <summary>
        /// check if row is full
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public bool isRowFull(int r)
        {
            for(int i = 0; i < Column; i++)
            {
                if (Grid[r, i] == 0)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// check if row is empty
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public bool isRowEmpty(int r)
        {
            for(int i = 0; i < Column; i++)
            {
                if (Grid[r, i] != 0)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// clear a row
        /// </summary>
        /// <param name="r">row number</param>
        private void ClearRow(int r)
        {
            for (int i = 0; i < Column; i++)
            {
                Grid[r, i] = 0;
            }
        }

        /// <summary>
        /// moves the row down
        /// </summary>
        /// <param name="r">row number</param>
        /// <param name="numRow">amount of row down (like the amount of y axis you want to move down</param>
        private void MoveRowDown(int r, int numRow)
        {
            for(int i = 0; i< Column; i++)
            {
                Grid[r + numRow, i] = Grid[r, i];
                Grid[r, i] = 0;
            }
        }

        /// <summary>
        /// clears a full row
        /// </summary>
        /// <returns></returns>
        public int ClearFullRow()
        {
            int cleared = 0;
            for (int i = Row - 1; i >= 0; i--)
            {
                if (isRowFull(i))
                {
                    ClearRow(i);
                    cleared++;
                }
                else if (cleared > 0){
                    MoveRowDown(i, cleared);
                }
            }
            return cleared;
        }

        /// <summary>
        /// displays the GameGrid
        /// </summary>
        public void Display()
        {
            Console.SetCursorPosition(_cursorXPosition, _cursorYPosition);

            //first line
            Console.Write("╔");
            for (int i = 0; i < Row; i++)
            {
                Console.Write("═");
            }
            Console.Write("╗");

            //the rest of the grid without last line           
            for (int i = 0; i < Column; i++)
            {
                //puts Cursor on line after
                _cursorYPosition++;
                Console.SetCursorPosition(_cursorXPosition, _cursorYPosition);

                Console.Write("║");
                //the void between the grid
                for (int j = 0; j < Row; j++)
                {
                    Console.Write(" ");
                }
                Console.Write("║");
            }

            //puts Cursor on line after
            _cursorYPosition++;
            Console.SetCursorPosition(_cursorXPosition, _cursorYPosition);

            //last line
            Console.Write("╚");
            for (int i = 0; i < Row; i++)
            {
                Console.Write("═");
            }
            Console.Write("╝");

            _cursorXPosition = 5;
            _cursorYPosition = 5;
        }
    }
}

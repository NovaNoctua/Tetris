using System;

///ETML
///Auteur : Maël Naudet
///Date : 17.01.2025

namespace Tetris
{
    /// <summary>
    /// Game's grid where the pieces will evolve in
    /// </summary>
    public class Grid
    {

        //properties
        private int height;                 //height of the grid
        private int width;                  //width of the grid
        private int cursorXPosition = 5;    //the x position of the cursor
        private int cursorYPosition = 5;    //the y position of the cursor

        //gets the grid's height
        public int Height
        {
            get
            {
                return height;
            }
        }

        //gets the grid's width
        public int Width
        {
            get
            {
                return width;
            }
        }

        //custom constructor
        public Grid(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        /// <summary>
        /// display the grid depending on its size
        /// </summary>
        public void Display()
        {
            //puts Cursor in the correct place
            Console.SetCursorPosition(cursorXPosition, cursorYPosition);

            //first line
            Console.Write("╔");
            for (int i = 0; i < width - 2; i++)
            {
                Console.Write("═");
            }
            Console.Write("╗");

            //the rest of the grid without last line           
            for (int i = 0; i < height - 2; i++)
            {
                //puts Cursor on line after
                cursorYPosition++;
                Console.SetCursorPosition(cursorXPosition, cursorYPosition);

                Console.Write("║");
                //the void between the grid
                for (int j = 0; j < width - 2; j++)
                {
                    Console.Write(" ");
                }
                Console.Write("║");               
            }
            
            //puts Cursor on line after
            cursorYPosition++;
            Console.SetCursorPosition(cursorXPosition, cursorYPosition);

            //last line
            Console.Write("╚");
            for (int i = 0; i < width - 2; i++)
            {
                Console.Write("═");
            }
            Console.Write("╝");

            cursorXPosition = 5;
            cursorYPosition = 5;
        }
    }
}

using System;

namespace Tetris
{
    public class Square
    {
        //propreties
        private int xPosition;      //X Position of square

        public int XPosition
        {
            get { return xPosition; }
            set { xPosition = value; }
        }

        private int  yPosition;     //Y Position of square

        public int YPosition
        {
            get { return  yPosition; }
            set {  yPosition = value; }
        }


        //list of possible colors
        protected ConsoleColor[] colors = {
            ConsoleColor.Black,
            ConsoleColor.DarkBlue,
            ConsoleColor.DarkGreen,
            ConsoleColor.DarkCyan,
            ConsoleColor.DarkRed,
            ConsoleColor.DarkMagenta,
            ConsoleColor.DarkYellow,
            ConsoleColor.Gray,
            ConsoleColor.DarkGray,
            ConsoleColor.Blue
        };

        public void Display(int color)
        {
            //changes color
            Console.ForegroundColor = colors[color];

            //displays top of square
            Console.SetCursorPosition(xPosition, yPosition);
            Console.Write("╔═╗");

            //displays bottom of square
            Console.SetCursorPosition(xPosition, yPosition + 1);
            Console.Write("╚═╝");
        }
    }
}

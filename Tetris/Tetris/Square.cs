using System;

namespace Tetris
{
    public class Square
    {
        /// <summary>
        /// Attributs of square
        /// </summary>
        private int _xPosition;      //X Position of square
        private int _yPosition;     //Y Position of square
        private readonly int _color;          //color of the square

        /// <summary>
        /// setter and getter of the attributs
        /// </summary>
        public int YPosition
        {
            get { return  _yPosition; }
            set {  _yPosition = value; }
        }

        public int XPosition
        {
            get { return _xPosition; }
            set { _xPosition = value; }
        }

        public ConsoleColor Color
        {
            get { return colors[_color]; }
            private set { colors[_color] = value; }
        }

        //list of possible colors
        private ConsoleColor[] colors = {
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

        //model of the square
        private string[] models = 
        { 
            "╔═╗", 
            "╚═╝" 
        };

        public Square(int xPosition, int yPosition, int color)
        {
            this._xPosition = xPosition;
            this._yPosition = yPosition;
            this._color = color;
        }

        /// <summary>
        /// Displays the square
        /// </summary>
        public void Display()
        {
            //changes color
            Console.ForegroundColor = Color;

            for(int i = 0; i < models.Length; i++)
            {
                Console.SetCursorPosition(_xPosition, _yPosition + i);
                Console.WriteLine(models[i]);
            }          
        }


        /// <summary>
        /// Erases the square
        /// </summary>
        public void Erase()
        {
            for(int i = 0; i < models.Length; i++)
            {
                Console.SetCursorPosition(_xPosition, _yPosition + i);
                Console.WriteLine("   ");
            }
        }
    }
}

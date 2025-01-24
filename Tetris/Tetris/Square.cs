using System;

namespace Tetris
{
    public class Square
    {
        /// <summary>
        /// Attributs of square
        /// </summary>
        private Position position;
        private readonly int _color;          //color of the square

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
            position = new Position(xPosition, yPosition);
            Color = colors[color];
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
                Console.SetCursorPosition(position.Row, position.Column + i);
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
                Console.SetCursorPosition(position.Row, position.Column + i);
                Console.WriteLine("   ");
            }
        }
    }
}

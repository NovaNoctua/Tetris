/// *******************************************************************************************
/// ETML
/// Author : Maël Naudet
/// Date : 17.01.2025
/// *******************************************************************************************

using System;

namespace Tetris
{
    /// <summary>
    /// Square that compose a block
    /// </summary>
    internal class Square
    {
        /// <summary>
        /// Attributs of square
        /// </summary>
        public Position position;
        private readonly ConsoleColor _color;          //color of the square
        private readonly int _colorIndex;
        public ConsoleColor Color => _color;
        public int ColorIndex => _colorIndex;

        //model of the square
        private string[] models = 
        { 
            "╔═╗", 
            "╚═╝" 
        };

        public Square(int xPosition, int yPosition, int colorIndex)
        {
            position = new Position(xPosition, yPosition);
            _colorIndex = colorIndex;
            _color = Custom.Colors[colorIndex];
        }

        /// <summary>
        /// Displays the square
        /// </summary>
        public void Display()
        {
            //changes color
            Console.ForegroundColor = _color;

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

        public void Move(int deltaX, int deltaY)
        {
            position.Row += deltaX; 
            position.Column += deltaY;
        }

        public void SetPosition(int newRow, int newColumn)
        {
            position.Row = newRow;
            position.Column = newColumn;    
        }
    }
}

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
        // Déclaration des attributs *********************************

        private Position _position;
        private readonly ConsoleColor _color;          //color of the square
        private readonly int _colorIndex;
        private string[] _model =
        {
            "╔═╗",
            "╚═╝"
        };

        // Déclaration des propriétés ********************************
        public Position Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
            }
        }

        public ConsoleColor Color => _color;
        public int ColorIndex => _colorIndex;


        // Déclaration des constructeurs *****************************

        public Square(int xPosition, int yPosition, int colorIndex)
        {
            Position = new Position(xPosition, yPosition);
            _colorIndex = colorIndex;
            _color = Custom.Colors[colorIndex];
        }


        // Déclaration et implémentation des méthodes ****************

        /// <summary>
        /// Displays the square
        /// </summary>
        public void Display()
        {
            //changes color
            Console.ForegroundColor = _color;

            for(int i = 0; i < _model.Length; i++)
            {
                Console.SetCursorPosition(Position.Row, Position.Column + i);
                Console.WriteLine(_model[i]);
            }          
        }


        /// <summary>
        /// Erases the square
        /// </summary>
        public void Erase()
        {
            for(int i = 0; i < _model.Length; i++)
            {
                Console.SetCursorPosition(Position.Row, Position.Column + i);
                Console.WriteLine("   ");
            }
        }

        /// <summary>
        /// Bouge le carré
        /// </summary>
        /// <param name="deltaX"></param>
        /// <param name="deltaY"></param>
        public void Move(int deltaX, int deltaY)
        {
            Position.Row += deltaX; 
            Position.Column += deltaY;
        }

        /// <summary>
        /// Donne une nouvelle position au carré
        /// </summary>
        /// <param name="newRow"></param>
        /// <param name="newColumn"></param>
        public void SetPosition(int newRow, int newColumn)
        {
            Position.Row = newRow;
            Position.Column = newColumn;    
        }
    }
}

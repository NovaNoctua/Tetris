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

        private Position _position;                     // position of the square
        private readonly ConsoleColor _color;           // color of the square
        private readonly int _colorIndex;               // color index of the square
        private string[] _model =                       // model of a square
        {
            "╔═╗",
            "╚═╝"
        };

        // Déclaration des propriétés ********************************

        /// <summary>
        /// Position du carré
        /// </summary>
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

        /// <summary>
        /// Couleur du carré
        /// </summary>
        public ConsoleColor Color => _color;

        /// <summary>
        /// Index couleur du carré
        /// </summary>
        public int ColorIndex => _colorIndex;


        // Déclaration des constructeurs *****************************

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="xPosition">position x</param>
        /// <param name="yPosition">position y</param>
        /// <param name="colorIndex">index de couleur</param>
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

/// *******************************************************************************************
/// ETML
/// Author : Maël Naudet
/// Date : 31.01.2025
/// *******************************************************************************************

using System.Collections.Generic;
using System.Linq;


namespace Tetris
{
    /// <summary>
    /// Block en forme de O
    /// </summary>
    internal class BlockO : Block
    {
        // Déclaration du constructeur *************************************************

        public BlockO(int startX, int startY)
        {
            _squares = new List<Square>
            {
                new Square(6 + 3 * (startX), 6 + 2 * (startY), _color),
                new Square(6 + 3 * (startX) + 3, 6 + 2 * (startY), _color),
                new Square(6 + 3 * (startX), 6 + 2 * (startY) + 2, _color),
                new Square(6 + 3 * (startX) + 3, 6 + 2 * (startY) + 2, _color)
            };
            _position = new Position(startX, startY);
        }

        // Déclaration et implémentation des méthodes ***********************************

        public override void MoveBlock(int x, int y)
        {
            Position = new Position(x, y);
            _squares = new List<Square>
            {
                new Square(6 + 3 * (x), 6 + 2 * (y), _color),
                new Square(6 + 3 * (x) + 3, 6 + 2 * (y), _color),
                new Square(6 + 3 * (x), 6 + 2 * (y) + 2, _color),
                new Square(6 + 3 * (x) + 3, 6 + 2 * (y) + 2, _color)
            };
        }

        /// <summary>
        /// Faire rotationner le bloc
        /// </summary>
        public override void Rotate()
        {

        }

        /// <summary>
        /// Clone le bloc
        /// </summary>
        /// <returns></returns>
        public override Block Clone()
        {
            // Créer une nouvelle instance avec la position actuelle
            BlockO clone = new BlockO(_position.Row, _position.Column);

            // Copier l'état de rotation
            clone._rotationState = this._rotationState;

            // Copier la liste des carrés avec la bonne couleur
            clone._squares = this._squares.Select(s => new Square(s.Position.Row, s.Position.Column, Custom.Colors.ToList().IndexOf(s.Color))).ToList();

            return clone;
        }

    }
}

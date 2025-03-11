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
    /// Block en forme de S
    /// </summary>
    internal class BlockS : Block
    {
        // Déclaration des attributs ***************************************************

        private bool _isHorizontal;

        // Déclaration du constructeur *************************************************

        public BlockS(int startX, int startY)
        {
            _squares = new List<Square>
            {
                new Square(6 + 3 * (startX), 6 + 2 * (startY) + 2, _color),
                new Square(6 + 3 * (startX) + 3, 6 + 2 * (startY) + 2, _color),
                new Square(6 + 3 * (startX) + 3, 6 + 2 * (startY), _color),
                new Square(6 + 3 * (startX) + 6, 6 + 2 * (startY), _color)
            };
            _position = new Position(startX, startY);
            _isHorizontal = true;
        }

        // Déclaration et implémentation des méthodes ***********************************

        /// <summary>
        /// Faire rotationner le bloc
        /// </summary>
        public override void Rotate()
        {
            Square pivot = _squares[1];

            if(_isHorizontal)
            {
                _squares[0].SetPosition(pivot.Position.Row, pivot.Position.Column - 2);
                _squares[2].SetPosition(pivot.Position.Row + 3, pivot.Position.Column);
                _squares[3].SetPosition(pivot.Position.Row + 3, pivot.Position.Column + 2);

                _isHorizontal = false;
            }
            else
            {
                _squares[0].SetPosition(pivot.Position.Row - 3, pivot.Position.Column);
                _squares[2].SetPosition(pivot.Position.Row, pivot.Position.Column - 2);
                _squares[3].SetPosition(pivot.Position.Row + 3, pivot.Position.Column - 2);

                _isHorizontal = true;
            }
        }

        /// <summary>
        /// Clone le bloc
        /// </summary>
        /// <returns></returns>
        public override Block Clone()
        {
            // Créer une nouvelle instance avec la position actuelle
            BlockS clone = new BlockS(_position.Row, _position.Column);

            // Copier l'état de rotation
            clone._isHorizontal = this._isHorizontal;

            // Copier la liste des carrés avec la bonne couleur
            clone._squares = this._squares.Select(s => new Square(s.Position.Row, s.Position.Column, Custom.Colors.ToList().IndexOf(s.Color))).ToList();

            return clone;
        }
    }
}

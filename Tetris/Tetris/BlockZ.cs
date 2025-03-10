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
    /// BLock en forme de Z
    /// </summary>
    internal class BlockZ : Block
    {
        // Déclaration des attributs ***************************************************

        private bool _isHorizontal;

        // Déclaration du constructeur *************************************************

        public BlockZ(int startX, int startY)
        {
            _squares = new List<Square>
            {
                new Square(6 + 3 * (startX), 6 + 2 * (startY), _color),
                new Square(6 + 3 * (startX) + 3, 6 + 2 * (startY), _color),
                new Square(6 + 3 * (startX) + 3, 6 + 2 * (startY) + 2, _color),
                new Square(6 + 3 * (startX) + 6, 6 + 2 * (startY) + 2, _color)
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
            Square pivot = _squares[2];

            if (_isHorizontal)
            {
                _squares[0].SetPosition(pivot.position.Row + 3, pivot.position.Column - 2);
                _squares[1].SetPosition(pivot.position.Row + 3, pivot.position.Column);
                _squares[3].SetPosition(pivot.position.Row, pivot.position.Column + 2);

                _isHorizontal = false;

            } else
            {
                _squares[0].SetPosition(pivot.position.Row - 3, pivot.position.Column - 2);
                _squares[1].SetPosition(pivot.position.Row, pivot.position.Column - 2);
                _squares[3].SetPosition(pivot.position.Row + 3, pivot.position.Column);

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
            BlockZ clone = new BlockZ(_position.Row, _position.Column);

            // Copier l'état de rotation
            clone._isHorizontal = this._isHorizontal;

            // Copier la liste des carrés avec la bonne couleur
            clone._squares = this._squares.Select(s => new Square(s.position.Row, s.position.Column, Custom.Colors.ToList().IndexOf(s.Color))).ToList();

            return clone;
        }
    }
}

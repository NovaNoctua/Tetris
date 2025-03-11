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
    /// Block en forme de J
    /// </summary>
    internal class BlockJ : Block
    {
        // Déclaration du constructeur *************************************************

        /// <summary>
        /// Constructeur simple
        /// </summary>
        /// <param name="startX">coordonnée x de départ</param>
        /// <param name="startY">coordonnée y de départ</param>
        public BlockJ(int startX, int startY)
        {
            _squares = new List<Square>
            {
                new Square(6 + 3 * (startX), 6 + 2 * (startY), _color),
                new Square(6 + 3 * (startX), 6 + 2 * (startY) + 2, _color),
                new Square(6 + 3 * (startX) + 3, 6 + 2 * (startY) + 2, _color),
                new Square(6 + 3 * (startX) + 6, 6 + 2 * (startY) + 2, _color)
            };
            _position = new Position(startX, startY);
            _rotationState = 0;
        }

        // Déclaration et implémentation des méthodes ***********************************

        /// <summary>
        /// Faire rotationner le bloc
        /// </summary>
        public override void Rotate()
        {
            Square pivot = _squares[2];

            if (_rotationState == 0)
            {
                _squares[0].SetPosition(pivot.Position.Row + 3, pivot.Position.Column - 2);
                _squares[1].SetPosition(pivot.Position.Row, pivot.Position.Column - 2);
                _squares[3].SetPosition(pivot.Position.Row, pivot.Position.Column + 2);

                _rotationState = 1;
            }
            else if (_rotationState == 1)
            {
                _squares[0].SetPosition(pivot.Position.Row + 3, pivot.Position.Column + 2);
                _squares[1].SetPosition(pivot.Position.Row + 3, pivot.Position.Column);
                _squares[3].SetPosition(pivot.Position.Row - 3, pivot.Position.Column);

                _rotationState = 2;
            }
            else if (_rotationState == 2)
            {
                _squares[0].SetPosition(pivot.Position.Row - 3, pivot.Position.Column + 2);
                _squares[1].SetPosition(pivot.Position.Row, pivot.Position.Column + 2);
                _squares[3].SetPosition(pivot.Position.Row, pivot.Position.Column - 2);

                _rotationState = 3;
            }
            else if (_rotationState == 3)
            {
                _squares[0].SetPosition(pivot.Position.Row - 3, pivot.Position.Column - 2);
                _squares[1].SetPosition(pivot.Position.Row - 3, pivot.Position.Column);
                _squares[3].SetPosition(pivot.Position.Row + 3, pivot.Position.Column);

                _rotationState = 0;
            }
        }

        /// <summary>
        /// Clone le bloc
        /// </summary>
        /// <returns></returns>
        public override Block Clone()
        {
            // Créer une nouvelle instance avec la position actuelle
            BlockJ clone = new BlockJ(_position.Row, _position.Column);

            // Copier l'état de rotation
            clone._rotationState = this._rotationState;

            // Copier la liste des carrés avec la bonne couleur
            clone._squares = this._squares.Select(s => new Square(s.Position.Row, s.Position.Column, Custom.Colors.ToList().IndexOf(s.Color))).ToList();

            return clone;
        }
    }
}

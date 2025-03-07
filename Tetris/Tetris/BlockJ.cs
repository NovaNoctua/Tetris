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

        public override void Rotate()
        {
            Square pivot = _squares[2];

            if (_rotationState == 0)
            {
                _squares[0].SetPosition(pivot.position.Row + 3, pivot.position.Column - 2);
                _squares[1].SetPosition(pivot.position.Row, pivot.position.Column - 2);
                _squares[3].SetPosition(pivot.position.Row, pivot.position.Column + 2);

                _rotationState = 1;
            }
            else if (_rotationState == 1)
            {
                _squares[0].SetPosition(pivot.position.Row + 3, pivot.position.Column + 2);
                _squares[1].SetPosition(pivot.position.Row + 3, pivot.position.Column);
                _squares[3].SetPosition(pivot.position.Row - 3, pivot.position.Column);

                _rotationState = 2;
            }
            else if (_rotationState == 2)
            {
                _squares[0].SetPosition(pivot.position.Row - 3, pivot.position.Column + 2);
                _squares[1].SetPosition(pivot.position.Row, pivot.position.Column + 2);
                _squares[3].SetPosition(pivot.position.Row, pivot.position.Column - 2);

                _rotationState = 3;
            }
            else if (_rotationState == 3)
            {
                _squares[0].SetPosition(pivot.position.Row - 3, pivot.position.Column - 2);
                _squares[1].SetPosition(pivot.position.Row - 3, pivot.position.Column);
                _squares[3].SetPosition(pivot.position.Row + 3, pivot.position.Column);

                _rotationState = 0;
            }
        }

        public override List<(int row, int col)> GetRotatedPositions()
        {
            // On commence par récupérer la position du pivot
            Square pivot = _squares[2];
            List<(int row, int col)> rotatedPositions = new List<(int row, int col)>();

            // On applique la logique de rotation selon l'état de rotation actuel
            if (_rotationState == 0)
            {
                rotatedPositions.Add((pivot.position.Row + 3, pivot.position.Column - 2));
                rotatedPositions.Add((pivot.position.Row, pivot.position.Column - 2));
                rotatedPositions.Add((pivot.position.Row, pivot.position.Column + 2));
            }
            else if (_rotationState == 1)
            {
                rotatedPositions.Add((pivot.position.Row + 3, pivot.position.Column + 2));
                rotatedPositions.Add((pivot.position.Row + 3, pivot.position.Column));
                rotatedPositions.Add((pivot.position.Row - 3, pivot.position.Column));
            }
            else if (_rotationState == 2)
            {
                rotatedPositions.Add((pivot.position.Row - 3, pivot.position.Column + 2));
                rotatedPositions.Add((pivot.position.Row, pivot.position.Column + 2));
                rotatedPositions.Add((pivot.position.Row, pivot.position.Column - 2));
            }
            else if (_rotationState == 3)
            {
                rotatedPositions.Add((pivot.position.Row - 3, pivot.position.Column - 2));
                rotatedPositions.Add((pivot.position.Row - 3, pivot.position.Column));
                rotatedPositions.Add((pivot.position.Row + 3, pivot.position.Column));
            }

            return rotatedPositions;
        }

        public override Block Clone()
        {
            // Créer une nouvelle instance avec la position actuelle
            BlockJ clone = new BlockJ(_position.Row, _position.Column);

            // Copier l'état de rotation
            clone._rotationState = this._rotationState;

            // Copier la liste des carrés avec la bonne couleur
            clone._squares = this._squares.Select(s => new Square(s.position.Row, s.position.Column, Custom.Colors.ToList().IndexOf(s.Color))).ToList();

            return clone;
        }
    }
}

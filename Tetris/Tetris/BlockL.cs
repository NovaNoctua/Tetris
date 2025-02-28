using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    internal class BlockL : Block
    {
        

        public BlockL(int startX, int startY)
        {
            _squares = new List<Square>
            {
                new Square(6 + 3 * (startX), 6 + 2 * (startY) + 2, _color),
                new Square(6 + 3 * (startX) + 3, 6 + 2 * (startY) + 2, _color),
                new Square(6 + 3 * (startX) + 6, 6 + 2 * (startY) + 2, _color),
                new Square(6 + 3 * (startX) + 6, 6 + 2 * (startY), _color)
            };
            _position = new Position(startX, startY);

            _rotationState = 0;
        }

        public override void Rotate()
        {
            Square pivot = _squares[1];

            if (_rotationState == 0)
            {
                _squares[0].SetPosition(pivot.position.Row, pivot.position.Column - 2);
                _squares[2].SetPosition(pivot.position.Row, pivot.position.Column + 2);
                _squares[3].SetPosition(pivot.position.Row + 3, pivot.position.Column + 2);

                _rotationState = 1;
            }
            else if (_rotationState == 1)
            {
                _squares[0].SetPosition(pivot.position.Row + 3, pivot.position.Column);
                _squares[2].SetPosition(pivot.position.Row - 3, pivot.position.Column);
                _squares[3].SetPosition(pivot.position.Row - 3, pivot.position.Column + 2);

                _rotationState = 2;
            }
            else if (_rotationState == 2)
            {
                _squares[0].SetPosition(pivot.position.Row, pivot.position.Column + 2);
                _squares[2].SetPosition(pivot.position.Row, pivot.position.Column - 2);
                _squares[3].SetPosition(pivot.position.Row - 3, pivot.position.Column - 2);

                _rotationState = 3;
            }
            else if (_rotationState == 3)
            {
                _squares[0].SetPosition(pivot.position.Row - 3, pivot.position.Column);
                _squares[2].SetPosition(pivot.position.Row + 3, pivot.position.Column);
                _squares[3].SetPosition(pivot.position.Row + 3, pivot.position.Column - 2);

                _rotationState = 0;
            }
        }

        public override List<(int row, int col)> GetRotatedPositions()
        {
            // On commence par récupérer la position du pivot
            Square pivot = _squares[1];
            List<(int row, int col)> rotatedPositions = new List<(int row, int col)>();

            // On applique la logique de rotation selon l'état de rotation actuel
            if (_rotationState == 0)
            {
                rotatedPositions.Add((pivot.position.Row, pivot.position.Column - 2)); // carré 0
                rotatedPositions.Add((pivot.position.Row, pivot.position.Column + 2)); // carré 2
                rotatedPositions.Add((pivot.position.Row + 3, pivot.position.Column + 2)); // carré 3
            }
            else if (_rotationState == 1)
            {
                rotatedPositions.Add((pivot.position.Row + 3, pivot.position.Column)); // carré 0
                rotatedPositions.Add((pivot.position.Row - 3, pivot.position.Column)); // carré 2
                rotatedPositions.Add((pivot.position.Row - 3, pivot.position.Column + 2)); // carré 3
            }
            else if (_rotationState == 2)
            {
                rotatedPositions.Add((pivot.position.Row, pivot.position.Column + 2)); // carré 0
                rotatedPositions.Add((pivot.position.Row, pivot.position.Column - 2)); // carré 2
                rotatedPositions.Add((pivot.position.Row - 3, pivot.position.Column - 2)); // carré 3
            }
            else if (_rotationState == 3)
            {
                rotatedPositions.Add((pivot.position.Row - 3, pivot.position.Column)); // carré 0
                rotatedPositions.Add((pivot.position.Row + 3, pivot.position.Column)); // carré 2
                rotatedPositions.Add((pivot.position.Row + 3, pivot.position.Column - 2)); // carré 3
            }

            return rotatedPositions;
        }

        public override Block Clone()
        {
            // Créer une nouvelle instance avec la position actuelle
            BlockL clone = new BlockL(_position.Row, _position.Column);

            // Copier l'état de rotation
            clone._rotationState = this._rotationState;
            clone._color = 0;

            // Copier la liste des carrés avec la bonne couleur
            clone._squares = this._squares.Select(s => new Square(s.position.Row, s.position.Column, Custom.Colors.ToList().IndexOf(s.Color))).ToList();

            return clone;
        }

    }
}

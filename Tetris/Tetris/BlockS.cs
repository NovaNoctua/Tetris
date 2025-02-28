using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    internal class BlockS : Block
    {
        private bool _isHorizontal;

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

        public override void Rotate()
        {
            Square pivot = _squares[1];

            if(_isHorizontal)
            {
                _squares[0].SetPosition(pivot.position.Row, pivot.position.Column - 2);
                _squares[2].SetPosition(pivot.position.Row + 3, pivot.position.Column);
                _squares[3].SetPosition(pivot.position.Row + 3, pivot.position.Column + 2);

                _isHorizontal = false;
            }
            else
            {
                _squares[0].SetPosition(pivot.position.Row - 3, pivot.position.Column);
                _squares[2].SetPosition(pivot.position.Row, pivot.position.Column - 2);
                _squares[3].SetPosition(pivot.position.Row + 3, pivot.position.Column - 2);

                _isHorizontal = true;
            }
        }

        public override List<(int row, int col)> GetRotatedPositions()
        {
            Square pivot = _squares[1];
            List<(int row, int col)> rotatedPositions = new List<(int row, int col)>();

            if (_isHorizontal)
            {
                rotatedPositions.Add((pivot.position.Row, pivot.position.Column - 2));  // Position de _squares[0]
                rotatedPositions.Add((pivot.position.Row + 3, pivot.position.Column));  // Position de _squares[2]
                rotatedPositions.Add((pivot.position.Row + 3, pivot.position.Column + 2));  // Position de _squares[3]
            }
            else
            {
                rotatedPositions.Add((pivot.position.Row - 3, pivot.position.Column));  // Position de _squares[0]
                rotatedPositions.Add((pivot.position.Row, pivot.position.Column - 2));  // Position de _squares[2]
                rotatedPositions.Add((pivot.position.Row + 3, pivot.position.Column - 2));  // Position de _squares[3]
            }

            return rotatedPositions;
        }

        public override Block Clone()
        {
            // Créer une nouvelle instance avec la position actuelle
            BlockS clone = new BlockS(_position.Row, _position.Column);

            // Copier l'état de rotation
            clone._rotationState = this._rotationState;

            // Copier la liste des carrés avec la bonne couleur
            clone._squares = this._squares.Select(s => new Square(s.position.Row, s.position.Column, Custom.Colors.ToList().IndexOf(s.Color))).ToList();

            return clone;
        }
    }
}

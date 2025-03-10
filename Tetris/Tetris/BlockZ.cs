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
        private bool _isHorizontal;

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

        public override List<(int row, int col)> GetRotatedPositions()
        {
            Square pivot = _squares[2];
            List<(int row, int col)> rotatedPositions = new List<(int row, int col)>();

            if (_isHorizontal)
            {
                rotatedPositions.Add((pivot.position.Row + 3, pivot.position.Column - 2));  // Position de _squares[0]
                rotatedPositions.Add((pivot.position.Row + 3, pivot.position.Column));      // Position de _squares[1]
                rotatedPositions.Add((pivot.position.Row, pivot.position.Column + 2));      // Position de _squares[3]
            }
            else
            {
                rotatedPositions.Add((pivot.position.Row - 3, pivot.position.Column - 2));  // Position de _squares[0]
                rotatedPositions.Add((pivot.position.Row, pivot.position.Column - 2));      // Position de _squares[1]
                rotatedPositions.Add((pivot.position.Row + 3, pivot.position.Column));      // Position de _squares[3]
            }

            return rotatedPositions;
        }


        public override Block Clone()
        {
            // Créer une nouvelle instance avec la position actuelle
            BlockZ clone = new BlockZ(_position.Row, _position.Column);

            // Copier l'état de rotation
            //clone._rotationState = this._rotationState;
            clone._isHorizontal = this._isHorizontal;

            // Copier la liste des carrés avec la bonne couleur
            clone._squares = this._squares.Select(s => new Square(s.position.Row, s.position.Column, Custom.Colors.ToList().IndexOf(s.Color))).ToList();

            return clone;
        }
    }
}

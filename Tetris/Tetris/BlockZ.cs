using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
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
    }
}

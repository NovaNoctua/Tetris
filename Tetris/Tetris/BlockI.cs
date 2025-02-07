using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    internal class BlockI : Block
    {
        private bool _isHorizontal;

        public BlockI(int startX, int startY)
        {
            _squares = new List<Square>
            {
                new Square(6 + 3 * (startX), 6 + 2 * (startY), _color),
                new Square(6 + 3 * (startX), 6 + 2 * (startY) + 2, _color),
                new Square(6 + 3 * (startX), 6 + 2 * (startY) + 4, _color),
                new Square(6 + 3 * (startX), 6 + 2 * (startY) + 6, _color)
            };
            _position = new Position(startX, startY);
            _isHorizontal = true;
        }

        public override void Rotate()
        {
            Square pivot = _squares[1];

            if(_isHorizontal)
            {
                _squares[0].SetPosition(pivot.position.Row - 3, pivot.position.Column);
                _squares[2].SetPosition(pivot.position.Row + 3, pivot.position.Column);
                _squares[3].SetPosition(pivot.position.Row + 6, pivot.position.Column);
            }

            if (!_isHorizontal)
            {
                _squares[0].SetPosition(pivot.position.Row, pivot.position.Column - 2);
                _squares[2].SetPosition(pivot.position.Row, pivot.position.Column + 2);
                _squares[3].SetPosition(pivot.position.Row, pivot.position.Column + 4);
            }

            _isHorizontal = !_isHorizontal;
        }
    }
}

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
    }
}

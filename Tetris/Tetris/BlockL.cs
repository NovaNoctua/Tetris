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
            squares = new List<Square>
            {
                new Square(startX, startY + 2, _color),
                new Square(startX + 3, startY + 2, _color),
                new Square(startX + 6, startY + 2, _color),
                new Square(startX + 6, startY, _color)
            };

            _rotationState = 0;
        }

        public override void Rotate()
        {
            Square pivot = squares[1];

            if (_rotationState == 0)
            {
                squares[0].SetPosition(pivot.position.Row, pivot.position.Column - 2);
                squares[2].SetPosition(pivot.position.Row, pivot.position.Column + 2);
                squares[3].SetPosition(pivot.position.Row + 3, pivot.position.Column + 2);

                _rotationState = 1;
            }
            else if (_rotationState == 1)
            {
                squares[0].SetPosition(pivot.position.Row + 3, pivot.position.Column);
                squares[2].SetPosition(pivot.position.Row - 3, pivot.position.Column);
                squares[3].SetPosition(pivot.position.Row - 3, pivot.position.Column + 2);

                _rotationState = 2;
            }
            else if (_rotationState == 2)
            {
                squares[0].SetPosition(pivot.position.Row, pivot.position.Column + 2);
                squares[2].SetPosition(pivot.position.Row, pivot.position.Column - 2);
                squares[3].SetPosition(pivot.position.Row - 3, pivot.position.Column - 2);

                _rotationState = 3;
            }
            else if (_rotationState == 3)
            {
                squares[0].SetPosition(pivot.position.Row - 3, pivot.position.Column);
                squares[2].SetPosition(pivot.position.Row + 3, pivot.position.Column);
                squares[3].SetPosition(pivot.position.Row + 3, pivot.position.Column - 2);

                _rotationState = 0;
            }
        }
    }
}

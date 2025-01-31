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
            squares = new List<Square>
            {
                new Square(startX, startY + 2, _color),
                new Square(startX + 3, startY + 2, _color),
                new Square(startX + 3, startY, _color),
                new Square(startX + 6, startY, _color)
            };
            _isHorizontal = true;
        }

        public override void Rotate()
        {
            Square pivot = squares[1];

            if(_isHorizontal)
            {
                squares[0].SetPosition(pivot.position.Row, pivot.position.Column - 2);
                squares[2].SetPosition(pivot.position.Row + 3, pivot.position.Column);
                squares[3].SetPosition(pivot.position.Row + 3, pivot.position.Column + 2);

                _isHorizontal = false;
            }
            else
            {
                squares[0].SetPosition(pivot.position.Row - 3, pivot.position.Column);
                squares[2].SetPosition(pivot.position.Row, pivot.position.Column - 2);
                squares[3].SetPosition(pivot.position.Row + 3, pivot.position.Column - 2);

                _isHorizontal = true;
            }
        }
    }
}

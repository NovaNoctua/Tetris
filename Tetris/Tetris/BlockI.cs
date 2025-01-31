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

        public BlockI(int startX, int startY, int color)
        {
            squares = new List<Square>
            {
                new Square(startX, startY, color),
                new Square(startX, startY + 2, color),
                new Square(startX, startY + 4, color),
                new Square(startX, startY + 6, color)
            };
            _isHorizontal = true;
        }

        public override void Rotate()
        {
            Square pivot = squares[1];

            if(_isHorizontal)
            {
                squares[0].SetPosition(pivot.position.Row - 3, pivot.position.Column);
                squares[2].SetPosition(pivot.position.Row + 3, pivot.position.Column);
                squares[3].SetPosition(pivot.position.Row + 6, pivot.position.Column);
            }

            if (!_isHorizontal)
            {
                squares[0].SetPosition(pivot.position.Row, pivot.position.Column - 2);
                squares[2].SetPosition(pivot.position.Row, pivot.position.Column + 2);
                squares[3].SetPosition(pivot.position.Row, pivot.position.Column + 4);
            }

            _isHorizontal = !_isHorizontal;
        }
    }
}

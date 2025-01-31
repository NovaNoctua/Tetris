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

        public BlockZ(int startX, int startY, int color)
        {
            squares = new List<Square>
            {
                new Square(startX, startY, color),
                new Square(startX + 3, startY, color),
                new Square(startX + 3, startY + 2, color),
                new Square(startX + 6, startY + 2, color)
            };
            _isHorizontal = true;
        }

        public override void Rotate()
        {
            Square pivot = squares[2];

            if (_isHorizontal)
            {
                squares[0].SetPosition(pivot.position.Row + 3, pivot.position.Column - 2);
                squares[1].SetPosition(pivot.position.Row + 3, pivot.position.Column);
                squares[3].SetPosition(pivot.position.Row, pivot.position.Column + 2);

                _isHorizontal = false;

            } else
            {
                squares[0].SetPosition(pivot.position.Row - 3, pivot.position.Column - 2);
                squares[1].SetPosition(pivot.position.Row, pivot.position.Column - 2);
                squares[3].SetPosition(pivot.position.Row + 3, pivot.position.Column);

                _isHorizontal = true;
            }
        }
    }
}

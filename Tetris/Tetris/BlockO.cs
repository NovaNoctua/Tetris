using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    internal class BlockO : Block
    {
        public BlockO(int startX, int startY) 
        {
            squares = new List<Square>
            {
                new Square(startX, startY, _color),
                new Square(startX + 3, startY, _color),
                new Square(startX, startY + 2, _color),
                new Square(startX + 3, startY + 2, _color)
            };
        }

        public override void Rotate()
        {
           
        }
    }
}

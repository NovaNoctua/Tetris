using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    internal class BlockO : Block
    {
        public BlockO(int startX, int startY, int color) 
        {
            squares = new List<Square>
            {
                new Square(startX, startY, color),
                new Square(startX + 3, startY, color),
                new Square(startX, startY + 2, color),
                new Square(startX + 3, startY + 2, color)
            };
        }

        public override void Rotate()
        {
           
        }
    }
}

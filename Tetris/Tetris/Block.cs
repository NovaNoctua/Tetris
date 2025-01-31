using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    internal abstract class Block 
    {
        protected List<Square> squares;
        protected byte _rotationState;
        protected int _color = new Random().Next(Custom.Colors.Count());

        public abstract void Rotate();

        public void Display()
        {
            foreach (var square in squares)
            {
                square.Display();
            }
        }

        public void Erase()
        {
            foreach (var square in squares)
            {
                square.Erase();
            }
        }

        public void Move(int deltaX, int deltaY)
        {
            foreach (var square in squares)
            {
                square.Move(deltaX, deltaY);
            }
        }
    }
}

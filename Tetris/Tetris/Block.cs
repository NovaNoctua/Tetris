using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    internal abstract class Block 
    {
        protected List<Square> _squares;
        protected byte _rotationState;
        protected int _color = Custom.GetRandomColorIndex();
        protected int _id;
        protected Position _position;
        public Position Position
        {
            get
            {
                return _position;
            }
        }
        public List<Square> Squares
        {
            get
            {
                return _squares;
            }
        }

        public abstract void Rotate();

        public void Display()
        {
            foreach (var square in Squares)
            {
                square.Display();
            }
        }

        public void Erase()
        {
            foreach (var square in Squares)
            {
                square.Erase();
            }
        }

        public void Move(int deltaX, int deltaY)
        {
            foreach (var square in Squares)
            {
                square.Move(deltaX, deltaY);
            }
        }
    }
}

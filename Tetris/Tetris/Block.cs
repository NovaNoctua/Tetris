/// *******************************************************************************************
/// ETML
/// Author : Maël Naudet
/// Date : 24.01.2025
/// *******************************************************************************************

using System;
using System.Collections.Generic;

namespace Tetris
{
    /// <summary>
    /// Classe abstraite qui permet de créer tous les blocs
    /// </summary>
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
        public abstract Block Clone();

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

        static public Block GetRandomBlock(int xPosition, int yPosition)
        {
            Random random = new Random();
            int index = random.Next(0, 7);  // Il y a 7 types de blocs
            switch (index)
            {
                case 0: return new BlockI(xPosition, yPosition);
                case 1: return new BlockJ(xPosition, yPosition);
                case 2: return new BlockL(xPosition, yPosition);
                case 3: return new BlockO(xPosition, yPosition);
                case 4: return new BlockS(xPosition, yPosition);
                case 5: return new BlockT(xPosition, yPosition);
                case 6: return new BlockZ(xPosition, yPosition);
                default: throw new Exception("Unexpected block type");
            }
        }

        public abstract List<(int row, int col)> GetRotatedPositions();
    }
}

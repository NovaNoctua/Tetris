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
            _squares = new List<Square>
            {
                new Square(6 + 3 * (startX), 6 + 2 * (startY), _color),
                new Square(6 + 3 * (startX) + 3, 6 + 2 * (startY), _color),
                new Square(6 + 3 * (startX), 6 + 2 * (startY) + 2, _color),
                new Square(6 + 3 * (startX) + 3, 6 + 2 * (startY) + 2, _color)
            };
            _position = new Position(startX, startY);
        }

        public override void Rotate()
        {
           
        }

        public override List<(int row, int col)> GetRotatedPositions()
        {
            // Le bloc O n'a pas de changement de forme, donc ses positions restent identiques à chaque rotation
            List<(int row, int col)> rotatedPositions = new List<(int row, int col)>();

            foreach (var square in _squares)
            {
                rotatedPositions.Add((square.position.Row, square.position.Column));  // Ajoute simplement les positions actuelles
            }

            return rotatedPositions;
        }

        public override Block Clone()
        {
            // Créer une nouvelle instance avec la position actuelle
            BlockO clone = new BlockO(_position.Row, _position.Column);

            // Copier l'état de rotation
            clone._rotationState = this._rotationState;

            // Copier la liste des carrés avec la bonne couleur
            clone._squares = this._squares.Select(s => new Square(s.position.Row, s.position.Column, Custom.Colors.ToList().IndexOf(s.Color))).ToList();

            return clone;
        }

    }
}

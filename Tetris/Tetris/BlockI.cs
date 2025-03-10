/// *******************************************************************************************
/// ETML
/// Author : Maël Naudet
/// Date : 24.01.2025
/// *******************************************************************************************

using System.Collections.Generic;
using System.Linq;


namespace Tetris
{
    /// <summary>
    /// Block en forme de I
    /// </summary>
    internal class BlockI : Block
    {
        // Déclaration des attributs *********************************************
        private bool _isHorizontal;     // savoir si le bloc est horizontal

        // Déclaration des constructeurs *****************************************

        /// <summary>
        /// Constructeur qui donne une liste de square en I
        /// </summary>
        /// <param name="startX">position de départ x</param>
        /// <param name="startY">position de départ Y</param>
        public BlockI(int startX, int startY)
        {
            _squares = new List<Square>
            {
                new Square(6 + 3 * (startX), 6 + 2 * (startY), _color),
                new Square(6 + 3 * (startX), 6 + 2 * (startY) + 2, _color),
                new Square(6 + 3 * (startX), 6 + 2 * (startY) + 4, _color),
                new Square(6 + 3 * (startX), 6 + 2 * (startY) + 6, _color)
            };
            _position = new Position(startX, startY);
            _isHorizontal = true;
        }


        // Déclaration et implémentation des méthodes ******************************

        /// <summary>
        /// Fais rotationner le bloc en fonction de sa position initiale
        /// </summary>
        public override void Rotate()
        {
            Square pivot = _squares[1];

            if(_isHorizontal)
            {
                _squares[0].SetPosition(pivot.position.Row - 3, pivot.position.Column);
                _squares[2].SetPosition(pivot.position.Row + 3, pivot.position.Column);
                _squares[3].SetPosition(pivot.position.Row + 6, pivot.position.Column);
            }

            if (!_isHorizontal)
            {
                _squares[0].SetPosition(pivot.position.Row, pivot.position.Column - 2);
                _squares[2].SetPosition(pivot.position.Row, pivot.position.Column + 2);
                _squares[3].SetPosition(pivot.position.Row, pivot.position.Column + 4);
            }

            _isHorizontal = !_isHorizontal;
        }

        /// <summary>
        /// Clone le bloc
        /// </summary>
        /// <returns></returns>
        public override Block Clone()
        {
            // Créer une nouvelle instance avec la position actuelle
            BlockI clone = new BlockI(_position.Row, _position.Column);

            // Copier l'état de rotation
            clone._isHorizontal = this._isHorizontal;

            // Copier la liste des carrés avec la bonne couleur
            clone._squares = this._squares.Select(s => new Square(s.position.Row, s.position.Column, Custom.Colors.ToList().IndexOf(s.Color))).ToList();

            return clone;
        }

    }
}

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
        // Déclaration et des attributs *******************************************************
        protected List<Square> _squares;                                    // liste de squares qui composent le bloc
        protected byte _rotationState;                                      // état de rotation
        protected int _color = Custom.GetRandomColorIndex();                // Couleur aléatoire attribuée au bloc
        //protected int _id;                                                // id du bloc
        protected Position _position;                                       // Position du bloc
        

        // Déclaration des propriétés *********************************************************

        /// <summary>
        /// Retourne la position du bloc
        /// </summary>
        public Position Position
        {
            get
            {
                return _position;
            }
        }

        /// <summary>
        /// Retourne la liste de squares qui composent le bloc
        /// </summary>
        public List<Square> Squares
        {
            get
            {
                return _squares;
            }
        }

        // Déclaration et (potentielle) implémentation des méthodes ***************************

        /// <summary>
        /// Méthode permettant de faire une rotation au bloc
        /// </summary>
        public abstract void Rotate();

        /// <summary>
        /// Clone une bloc
        /// </summary>
        /// <returns>le clone du bloc</returns>
        public abstract Block Clone();

        /// <summary>
        /// Affiche simplement tous les squares du bloc
        /// </summary>
        public void Display()
        {
            foreach (var square in Squares)
            {
                square.Display();
            }
        }

        /// <summary>
        /// Efface tous les squares du bloc
        /// </summary>
        public void Erase()
        {
            foreach (var square in Squares)
            {
                square.Erase();
            }
        }

        /// <summary>
        /// Bouge le bloc en fonction de la différence inscrite
        /// </summary>
        /// <param name="deltaX">différence en coordonnée X</param>
        /// <param name="deltaY">différence en coordonnée Y</param>
        public void Move(int deltaX, int deltaY)
        {
            foreach (var square in Squares)
            {
                square.Move(deltaX, deltaY);
            }
        }

        /// <summary>
        /// Permet d'obtenir un bloc aléatoirement
        /// </summary>
        /// <param name="xPosition">position x du bloc</param>
        /// <param name="yPosition">position y du bloc</param>
        /// <returns>Un bloc compris dans la liste de bloc trouvée plus bas</returns>
        /// <exception cref="Exception">au cas ou</exception>
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

        
    }
}

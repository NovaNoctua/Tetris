/// *******************************************************************************************
/// ETML
/// Author : Maël Naudet
/// Date : 17.01.2025
/// *******************************************************************************************

using System;
using System.Collections.Generic;

namespace Tetris
{
    /// <summary>
    /// Game's grid where the pieces will evolve in
    /// </summary>
    internal class GameGrid
    {
        // Déclaration des attributs *********************************************

        private readonly int _row;
        private readonly int _column;
        private bool[,] _grid;                  // false = vide, true = occupé
        private Square[,] _squareGrid;
        private int _cursorXPosition = 5;       // la position X du curseur
        private int _cursorYPosition = 5;       // la position Y du curseur


        // Déclaration des propriétés ********************************************

        public int Row
        {
            get
            {
                return _row;
            }
        }

        public int Column
        {
            get
            {
                return _column;
            }
        }

        public bool[,] Grid
        {
            get
            {
                return _grid;
            }
            set
            {
                _grid = value;
            }
        }

        public Square[,] SquareGrid
        {
            get
            {
                return _squareGrid;
            }
            set
            {
                _squareGrid = value;
            }
        }

        // Déclaration du constructeur ********************************************

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="row">ligne</param>
        /// <param name="column">nombre de colonnes</param>
        public GameGrid(int row, int column)
        {
            _row = row;
            _column = column;
            _grid = new bool[Row, Column];
            _squareGrid = new Square[Row, Column];
        }

        // Déclaration et implémentation des méthodes ******************************

        /// <summary>
        /// Vérifie si une cellule est à l'intérieur de la grille
        /// </summary>
        /// <param name="r">ligne</param>
        /// <param name="c">colonne</param>
        /// <returns></returns>
        public bool IsInside(int r, int c)
        {
            return r >= 0 && r < Row && c >= 0 && c < Column;
        }

        /// <summary>
        /// Vérifie si la cellule est vide
        /// </summary>
        /// <param name="r">ligne</param>
        /// <param name="c">colonne</param>
        /// <returns></returns>
        public bool isEmpty(int r, int c)
        {
            return IsInside(r, c) && !Grid[r, c];
        }

        /// <summary>
        /// Vérifie si une ligne est pleine
        /// </summary>
        /// <param name="r">ligne</param>
        /// <returns></returns>
        public bool isRowFull(int r)
        {
            for (int i = 0; i < Row; i++)
            {
                if (!Grid[i, r])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Vérifie si une ligne est vide
        /// </summary>
        /// <param name="r">ligne</param>
        /// <returns></returns>
        public bool isRowEmpty(int r)
        {
            for (int i = 0; i < Column; i++)
            {
                if (Grid[r, i])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Efface une ligne
        /// </summary>
        /// <param name="r">numéro de la ligne</param>
        private void ClearRow(int r)
        {
            for (int i = 0; i < Row; i++)
            {
                Grid[i, r] = false;
                SquareGrid[i, r] = null;
            }
        }

        /// <summary>
        /// Déplace une ligne vers le bas
        /// </summary>
        /// <param name="r">numéro de la ligne</param>
        /// <param name="numRow">numéro de lignes à descendre</param>
        private void MoveRowDown(int r, int numRow)
        {
            for (int i = 0; i < Row; i++)
            {
                Grid[i, r + numRow] = Grid[i, r];
                Grid[i, r] = false;
                SquareGrid[i, r + numRow] = SquareGrid[i, r];
                SquareGrid[i, r] = null;
            }
        }

        /// <summary>
        /// Efface les lignes pleines
        /// </summary>
        /// <returns>Nombre de lignes effacées</returns>
        public int ClearFullRow()
        {
            int cleared = 0;
            for (int i = Column - 1; i >= 0; i--)
            {
                if (isRowFull(i))
                {
                    ClearRow(i);
                    cleared++;
                }
                else if (cleared > 0)
                {
                    MoveRowDown(i, cleared);
                }
            }
            return cleared;
        }

        /// <summary>
        /// Affiche la grille de jeu
        /// </summary>
        public void Display()
        {
            Console.SetCursorPosition(_cursorXPosition, _cursorYPosition);

            // Première ligne
            Console.Write("╔");
            for (int i = 0; i < Row; i++)
            {
                Console.Write("═══");
            }
            Console.Write("╗");

            // Le reste de la grille sans la dernière ligne           
            for (int i = 0; i < Column * 2; i++)
            {
                _cursorYPosition++;
                Console.SetCursorPosition(_cursorXPosition, _cursorYPosition);
                Console.Write("║");
                for (int j = 0; j < Row; j++)
                {
                    Console.Write("   ");
                }
                Console.Write("║");
            }

            _cursorYPosition++;
            Console.SetCursorPosition(_cursorXPosition, _cursorYPosition);
            // Dernière ligne
            Console.Write("╚");
            for (int i = 0; i < Row; i++)
            {
                Console.Write("═══");
            }
            Console.Write("╝");

            _cursorXPosition = 5;
            _cursorYPosition = 5;
        }

        /// <summary>
        /// Crée un bloc dans les grilles de gestion
        /// </summary>
        /// <param name="block">bloc</param>
        public void CreateBlockInGrid(Block block)
        {
            foreach (Square square in block.Squares)
            {
                Grid[(square.Position.Row - 6) / 3, (square.Position.Column - 6) / 2] = true;
                SquareGrid[(square.Position.Row - 6) / 3, (square.Position.Column - 6) / 2] = new Square(square.Position.Row, square.Position.Column, square.ColorIndex);
            }
        }

        /// <summary>
        /// Détruit un bloc dans les grilles de gestion
        /// </summary>
        /// <param name="block"></param>
        private void DestroyBlockInGrid(Block block)
        {
            foreach (Square square in block.Squares)
            {
                Grid[(square.Position.Row - 6) / 3, (square.Position.Column - 6) / 2] = false;
                SquareGrid[(square.Position.Row - 6) / 3, (square.Position.Column - 6) / 2] = null;
            }
        }

        /// <summary>
        /// Check si un bloc peut entrer à un endroit
        /// </summary>
        /// <param name="block">bloc</param>
        /// <param name="deltaX">endroit x</param>
        /// <param name="deltaY">endroit y</param>
        /// <returns>booléen</returns>
        public bool CanBlockFit(Block block, int deltaX, int deltaY)
        {
            // Récupère les positions actuelles (indices de grille) occupées par le block
            var currentPositions = new HashSet<(int row, int col)>();
            foreach (Square square in block.Squares)
            {
                // Convertit la position du carré en indices de grille
                int curRow = (square.Position.Row - 6) / 3;  // Calcul pour obtenir la ligne dans la grille
                int curCol = (square.Position.Column - 6) / 2;  // Calcul pour obtenir la colonne dans la grille

                // Ajoute la position actuelle à l'ensemble des positions du bloc
                currentPositions.Add((curRow, curCol));
            }

            // Pour chaque carré, on calcule la nouvelle position dans la grille après déplacement (et rotation, si nécessaire)
            foreach (Square square in block.Squares)
            {
                // Calcul de la nouvelle position dans la grille en tenant compte du déplacement
                int newRow = (square.Position.Row - 6) / 3 + deltaX;  // Applique le décalage deltaX à la ligne
                int newCol = (square.Position.Column - 6) / 2 + deltaY;  // Applique le décalage deltaY à la colonne

                if (newRow < 0 || newRow >= Grid.GetLength(0) || newCol < 0 || newCol >= Grid.GetLength(1))
                {
                    return false; // Si l'indice est en dehors des limites, retourner false immédiatement
                }

                try
                {
                    // Si la nouvelle position n'est pas déjà occupée par un carré du même bloc
                    // et qu'elle est occupée dans la grille par un autre bloc, on ne peut pas y déplacer le bloc
                    if (!currentPositions.Contains((newRow, newCol)) && Grid[newRow, newCol])
                    {
                        return false;  // Retourne false si la position est déjà occupée
                    }
                }
                catch (System.IndexOutOfRangeException)
                {
                    // Si la nouvelle position dépasse les limites de la grille, on ne peut pas y déplacer le bloc
                    return false;  // Retourne false si la position est hors de la grille
                }
            }

            // Si toutes les vérifications passent (aucune collision et aucune sortie de la grille), on peut déplacer le bloc
            return true;

        }

        /// <summary>
        /// Bouge un bloc dans la grille 
        /// </summary>
        /// <param name="block">bloc</param>
        /// <param name="deltaX">différence x</param>
        /// <param name="deltaY">différence y</param>
        public void MoveBlock(Block block, int deltaX, int deltaY)
        {
            DestroyBlockInGrid(block);
            foreach (Square square in block.Squares)
            {
                Grid[(square.Position.Row - 6) / 3 + deltaX, (square.Position.Column - 6) / 2 + deltaY] = true;
                SquareGrid[(square.Position.Row - 6) / 3 + deltaX, (square.Position.Column - 6) / 2 + deltaY] = new Square(square.Position.Row, square.Position.Column, square.ColorIndex);
            }
        }

        /// <summary>
        /// Pour afficher la grille de gestion (pour le debug)
        /// </summary>
        public void DisplayGridInt()
        {
            for (int i = 0; i < Column; i++)
            {
                Console.SetCursorPosition(80, i);
                for (int j = 0; j < Row; j++)
                {
                    Console.Write(Grid[j, i] ? "1" : "0");
                }
            }
        }

        /// <summary>
        /// Pour afficher la grille de gestion (pour le debug)
        /// </summary>
        public void DisplayGridSquare()
        {
            for (int i = 0; i < Column; i++)
            {
                Console.SetCursorPosition(100, i);
                for (int j = 0; j < Row; j++)
                {
                    if (SquareGrid[j, i] != null)
                    {
                        Console.Write('1');
                    }
                    else
                    {
                        Console.Write('0');
                    }
                }
            }
        }

        /// <summary>
        /// Check si on peut rotationner un bloc
        /// </summary>
        /// <param name="block"></param>
        /// <returns></returns>
        public bool CanBlockRotate(Block block)
        {
            Block clone = block.Clone();

            clone.Rotate();

            if (CanBlockFit(clone, 0, 0))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Fais rotationner un bloc
        /// </summary>
        /// <param name="block"></param>
        public void RotateBlock(Block block)
        {
            DestroyBlockInGrid(block);
            block.Erase();
            block.Rotate();
            CreateBlockInGrid(block);
            block.Display();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading;

///ETML
///Auteur : Maël Naudet
///Date : 17.01.2025

namespace Tetris
{
    /// <summary>
    /// Game's grid where the pieces will evolve in
    /// </summary>
    internal class GameGrid
    {
        //properties
        private readonly int _row;
        private readonly int _column;
        private bool[,] _grid;                // false = vide, true = occupé
        private int _cursorXPosition = 5;       // la position X du curseur
        private int _cursorYPosition = 5;       // la position Y du curseur

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

        // Constructeur personnalisé
        public GameGrid(int row, int column)
        {
            _row = row;
            _column = column;
            _grid = new bool[Row, Column];
        }

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

        public void FullBlockDisplay()
        {

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

        public void CreateBlockInGrid(Block block)
        {
            foreach (Square square in block.Squares)
            {
                Grid[(square.position.Row - 6) / 3, (square.position.Column - 6) / 2] = true;
            }
        }

        private void DestroyBlockInGrid(Block block)
        {
            foreach (Square square in block.Squares)
            {
                Grid[(square.position.Row - 6) / 3, (square.position.Column - 6) / 2] = false;
            }
        }


        public bool CanBlockFit(Block block, int deltaX, int deltaY)
        {
            //// Sauvegarde l'état du bloc original dans la grille (avant de tester).
            //this.DestroyBlockInGrid(block);

            //// Crée un clone pour tester la nouvelle position sans affecter l'original.
            //Block clone = block.Clone();
            //clone.Move(deltaX, deltaY); // Déplace le clone

            //// Vérifie chaque carré du clone dans la nouvelle position
            //foreach (Square square in clone.Squares)
            //{
            //    try
            //    {
            //        // Calcul de la nouvelle position du carré dans la grille
            //        int newRow = (square.position.Row - 6) / 3;
            //        int newCol = (square.position.Column - 6) / 2;

            //        // Si la position est déjà occupée ou hors de la grille, retourne false
            //        if (Grid[newRow, newCol])
            //        {
            //            this.CreateBlockInGrid(block); // Réinsère le bloc original dans la grille
            //            return false;
            //        }
            //    }
            //    catch (System.IndexOutOfRangeException)
            //    {
            //        this.CreateBlockInGrid(block); // Réinsère le bloc original dans la grille
            //        return false; // Hors de la grille
            //    }
            //}

            //// Si tout est ok, réinsère le bloc d'origine dans la grille
            //this.CreateBlockInGrid(block);
            //return true;


            // Récupère les positions actuelles (indices de grille) occupées par le block
            var currentPositions = new HashSet<(int row, int col)>();
            foreach (Square square in block.Squares)
            {
                // Convertit la position du carré en indices de grille
                int curRow = (square.position.Row - 6) / 3;  // Calcul pour obtenir la ligne dans la grille
                int curCol = (square.position.Column - 6) / 2;  // Calcul pour obtenir la colonne dans la grille

                // Ajoute la position actuelle à l'ensemble des positions du bloc
                currentPositions.Add((curRow, curCol));
            }

            // Pour chaque carré, on calcule la nouvelle position dans la grille après déplacement (et rotation, si nécessaire)
            foreach (Square square in block.Squares)
            {
                // Calcul de la nouvelle position dans la grille en tenant compte du déplacement
                int newRow = (square.position.Row - 6) / 3 + deltaX;  // Applique le décalage deltaX à la ligne
                int newCol = (square.position.Column - 6) / 2 + deltaY;  // Applique le décalage deltaY à la colonne

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

        public void MoveBlock(Block block, int deltaX, int deltaY)
        {
            DestroyBlockInGrid(block);
            foreach (Square square in block.Squares)
            {
                Grid[(square.position.Row - 6) / 3 + deltaX, (square.position.Column - 6) / 2 + deltaY] = true;
            }
        }

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

        public bool CanBlockRotate(Block block)
        {
            Block clone = block.Clone();

            clone.Rotate();

            if (CanBlockFit(clone, 0, 0))
            {
                return true;
            }
            return false;
            //// On récupère les nouvelles positions après rotation
            //List<(int row, int col)> rotatedPositions = block.GetRotatedPositions();

            //// On vérifie si toutes les nouvelles positions sont valides
            //foreach (var (newRow, newCol) in rotatedPositions)
            //{
            //    // Si une position est en dehors de la grille ou occupée par un autre bloc, on ne peut pas faire la rotation
            //    if (!IsInside((newRow / 3) - 6, (newCol / 2) - 6) || Grid[(newRow / 3) - 6, (newCol / 2) - 6] == true)
            //    {
            //        return false;
            //    }
            //}

            //// Si toutes les positions sont valides, la rotation est possible
            //return true;
        }

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

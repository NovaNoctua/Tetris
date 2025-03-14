/// *******************************************************************************************
/// ETML
/// Author : Maël Naudet
/// Date : 17.01.2025
/// *******************************************************************************************

namespace Tetris
{
    /// <summary>
    /// Positions of something
    /// </summary>
    internal class Position
    {
        // Déclaration des propriétés *********************************

        /// <summary>
        /// Position x
        /// </summary>
        public int Row { get ; set; }

        /// <summary>
        /// Position y
        /// </summary>
        public int Column { get ; set; }

        // Déclaration du constructeur ********************************

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="row">position x</param>
        /// <param name="column">position y</param>
        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}

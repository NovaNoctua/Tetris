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
        public int Row { get ; set; }
        public int Column { get ; set; }

        // Déclaration du constructeur ********************************
        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}

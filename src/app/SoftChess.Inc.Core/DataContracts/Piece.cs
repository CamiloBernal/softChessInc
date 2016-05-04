namespace SoftChess.Inc.Core.DataContracts
{
    public class Piece
    {
        /// <summary>
        ///     Boolean flag indicates if piece is enabled or not in database
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        ///     Piece color
        /// </summary>
        public PieceColor PieceColor { get; set; }

        /// <summary>
        ///     Piece type
        /// </summary>
        public PieceType PieceType { get; set; }
    }
}
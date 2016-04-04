using System;

namespace SoftChess.Inc.Core.DataContracts
{
    public class HistoricalMovement
    {
        /// <summary>
        ///     Piece position before movement
        /// </summary>
        public Position FromPosition { get; set; }

        /// <summary>
        ///     Date of movement
        /// </summary>
        public DateTime MovementDate { get; set; }

        /// <summary>
        ///     Piece to move
        /// </summary>
        public Piece Piece { get; set; }

        /// <summary>
        ///     Piece position after movement
        /// </summary>
        public Position ToPosition { get; set; }
    }
}
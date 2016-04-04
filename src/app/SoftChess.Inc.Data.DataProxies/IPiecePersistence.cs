using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SoftChess.Inc.Core.DataContracts;

namespace SoftChess.Inc.Data.DataProxies
{
    public interface IPiecePersistence
    {
        /// <summary>
        ///     Get all enabled pieces on Db
        /// </summary>
        /// <param name="cancellationToken">Cancellation token for async operation</param>
        /// <returns>List of enabled pieces</returns>
        Task<IEnumerable<Piece>> GetEnabledPiecesAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     Upsert information for a piece
        /// </summary>
        /// <param name="piece">Piece to db upsert</param>
        /// <param name="cancellationToken">Cancellation token for async operation</param>
        /// <returns>Operation result</returns>
        Task<IDbResult> UpsertPieceAsync(Piece piece, CancellationToken cancellationToken = default(CancellationToken));
    }
}
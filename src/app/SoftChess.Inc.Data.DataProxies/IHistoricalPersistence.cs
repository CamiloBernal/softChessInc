using System.Threading;
using System.Threading.Tasks;
using SoftChess.Inc.Core.DataContracts;

namespace SoftChess.Inc.Data.DataProxies
{
    public interface IHistoricalPersistence
    {
        /// <summary>
        ///     Register a piece movement in historical data.
        /// </summary>
        /// <param name="movement">Movement to register</param>
        /// <param name="cancellationToken">Cancellation token for async operation.</param>
        /// <returns>Operation result</returns>
        Task<IDbResult> RegisterPieceMovementAsync(HistoricalMovement movement,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
using System.Threading;
using System.Threading.Tasks;
using SoftChess.Inc.Core.DataContracts;

namespace SoftChess.Inc.Data.DataProxies
{
    public interface IRulePersistence
    {
        /// <summary>
        ///     Returns a ruleset for piece.
        /// </summary>
        /// <param name="forPiece">Piece to query ruleset</param>
        /// <param name="cancellationToken">Cancellation token for async operation</param>
        /// <returns>A rule set for piece</returns>
        Task<PieceRuleSet> GetPieceRulesetAsync(PieceType forPiece,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     Register a new rule for piece in piece ruleset
        /// </summary>
        /// <param name="forPiece">Piece to add rule</param>
        /// <param name="rule">Rule to register</param>
        /// <param name="cancellationToken">Cancellation token for async operation</param>
        /// <returns>Operation result</returns>
        Task<IDbResult> RegisterMovementRuleAsync(Piece forPiece, MovementRule rule,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     Register a new ruleset for piece in piece ruleset
        /// </summary>
        /// <param name="ruleSet">Ruleset for register</param>
        /// <param name="cancellationToken">Cancellation token for async operation</param>
        /// <returns>Operation result</returns>
        Task<IDbResult> RegisterPieceRuleSetAsync(PieceRuleSet ruleSet,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
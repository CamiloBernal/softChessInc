using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using SoftChess.Inc.Core.DataContracts;
using SoftChess.Inc.Data.DbProviders.Mongo.Settings;
using SoftChess.Inc.Data.Settings;

namespace SoftChess.Inc.Data.DataProxies.Mongo.PersistenceServices
{
    public class RulePersistenceService : PersistenceServiceBase, IRulePersistence
    {
        public RulePersistenceService(DbSettings<MongoDbSettings> settings) : base(settings)
        {
            //Default CTOR
        }

        public async Task<PieceRuleSet> GetPieceRulesetAsync(PieceType forPiece,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var filter = Builders<PieceRuleSet>.Filter.Eq(p => p.ForPiece.PieceType, forPiece);
            var rules =
                await
                    DbHelper.FindAsync(Settings.Settings.RuleSetsCollectionName, filter, DbHelper.DefaultProjection,
                        DbHelper.DefaultCancellationToken).ConfigureAwait(false);
            var pieceRuleSets = rules as IList<PieceRuleSet> ?? rules.ToList();
            return pieceRuleSets.Any() ? pieceRuleSets.First() : null;
        }

        public async Task<IDbResult> RegisterMovementRuleAsync(Piece forPiece, MovementRule rule,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var filter = Builders<PieceRuleSet>.Filter.Eq(p => p.ForPiece.PieceType, forPiece.PieceType);
            var update = Builders<PieceRuleSet>.Update.Push(p => p.Rules, rule);
            var result =
                await
                    DbHelper.UpdateOneAsync(Settings.Settings.RuleSetsCollectionName, filter, update, cancellationToken,
                        false).ConfigureAwait(false);
            var dbResult = new DefaultDbResult(false);
            if (result != null && result.ModifiedCount > 0) dbResult.Success = false;
            return dbResult;
        }

        public async Task<IDbResult> RegisterPieceRuleSetAsync(PieceRuleSet ruleSet,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var filter = Builders<PieceRuleSet>.Filter.Eq(p => p.ForPiece.PieceType, ruleSet.ForPiece.PieceType);
            var result =
                await
                    DbHelper.ReplaceOneAsync(Settings.Settings.RuleSetsCollectionName, filter, ruleSet,
                        cancellationToken).ConfigureAwait(false);
            var dbResult = new DefaultDbResult(false);
            if (result != null && result.ModifiedCount > 0) dbResult.Success = false;
            return dbResult;
        }
    }
}
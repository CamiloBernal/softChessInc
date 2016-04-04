using System.Threading;
using System.Threading.Tasks;
using SoftChess.Inc.Core.DataContracts;
using SoftChess.Inc.Data.DbProviders.Mongo.Settings;
using SoftChess.Inc.Data.Settings;

namespace SoftChess.Inc.Data.DataProxies.Mongo.PersistenceServices
{
    public class HistoricalPersistenceService : PersistenceServiceBase, IHistoricalPersistence
    {
        public HistoricalPersistenceService(DbSettings<MongoDbSettings> settings) :
            base(settings)
        {
            //Default CTOR
        }

        public async Task<IDbResult> RegisterPieceMovementAsync(HistoricalMovement movement,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            await
                DbHelper.InsertOneAsync(Settings.Settings.HistoricalMovementsCollectionName, movement, cancellationToken)
                    .ConfigureAwait(false);
            var dbResult = new DefaultDbResult(true);
            return dbResult;
        }
    }
}
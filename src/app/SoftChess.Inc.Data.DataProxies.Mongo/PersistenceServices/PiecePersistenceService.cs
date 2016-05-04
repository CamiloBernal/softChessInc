using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using SoftChess.Inc.Core.DataContracts;
using SoftChess.Inc.Data.DbProviders.Mongo.Settings;
using SoftChess.Inc.Data.Settings;

namespace SoftChess.Inc.Data.DataProxies.Mongo.PersistenceServices
{
    public class PiecePersistenceService : PersistenceServiceBase, IPiecePersistence
    {
        public PiecePersistenceService(DbSettings<MongoDbSettings> settings) : base(settings)
        {
            //Default CTOR
        }

        public async Task<IEnumerable<Piece>> GetEnabledPiecesAsync(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var filter = Builders<Piece>.Filter.Eq(p => p.Enabled, true);
            return
                await
                    DbHelper.FindAsync(Settings.Settings.PiecesCollectionName, filter, DbHelper.DefaultProjection,
                        DbHelper.DefaultCancellationToken).ConfigureAwait(false);
        }

        public async Task<IDbResult> UpsertPieceAsync(Piece piece,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var filter = Builders<Piece>.Filter.Eq(p => p.PieceType, piece.PieceType);
            var result =
                await
                    DbHelper.ReplaceOneAsync(Settings.Settings.PiecesCollectionName, filter, piece, cancellationToken)
                        .ConfigureAwait(false);
            var dbResult = new DefaultDbResult(false);
            if (result != null && result.ModifiedCount > 0) dbResult.Success = false;
            return dbResult;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using SoftChess.Inc.Data.DbProviders.Mongo.Settings;
using SoftChess.Inc.Data.Settings;

namespace SoftChess.Inc.Data.DbProviders.Mongo
{
    /// <summary>
    ///     Provides the necessary logic to perform data access operations to MongoDb provider.
    /// </summary>
    public sealed class MongoDbHelper
    {
        private readonly DbSettings<MongoDbSettings> _dbSettings;

        public MongoDbHelper(DbSettings<MongoDbSettings> dbSettings)
        {
            if (dbSettings == null) throw new ArgumentNullException(nameof(dbSettings));
            _dbSettings = dbSettings;
            Initialize();
        }

        public CancellationToken DefaultCancellationToken => CancellationToken.None;

        /// <summary>
        ///     Default filter for MongoDb queries
        /// </summary>
        public BsonDocument DefaultFilter => BsonDocument.Parse("{}");

        /// <summary>
        ///     Default projection for MongoDb queries.
        /// </summary>
        public BsonDocument DefaultProjection => BsonDocument.Parse("{}");

        /// <summary>
        ///     Delete matched documents in Collection when filter match filter criteria.
        /// </summary>
        /// <typeparam name="T">Type of searched documents.</typeparam>
        /// <param name="collectionName">Name of collection to delete documents.</param>
        /// <param name="filters">Filters to perform the delete action.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public async Task DeleteManyAsync<T>(string collectionName, List<FilterDefinition<T>> filters,
            CancellationToken cancellationToken)
            where T : class
        {
            if (string.IsNullOrEmpty(collectionName)) throw new ArgumentNullException(nameof(collectionName));
            var collection = GetCollection<T>(collectionName);

            var tasks =
                filters.Select(filter => collection.DeleteOneAsync(filter, cancellationToken)).Cast<Task>().ToList();

            await Task.WhenAll(tasks).ConfigureAwait(false);
        }

        /// <summary>
        ///     Delete matched documents in Collection when filter match filter criteria.
        /// </summary>
        /// <typeparam name="T">Type of searched documents.</typeparam>
        /// <param name="collectionName">Name of collection to delete documents.</param>
        /// <param name="filter">Filter to perform the delete action.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public Task<DeleteResult> DeleteManyAsync<T>(string collectionName, FilterDefinition<T> filter,
            CancellationToken cancellationToken)
            where T : class
        {
            if (string.IsNullOrEmpty(collectionName)) throw new ArgumentNullException(nameof(collectionName));
            var collection = GetCollection<T>(collectionName);
            return collection.DeleteManyAsync(filter, cancellationToken);
        }

        /// <summary>
        ///     Delete matched documents in Collection when filter match filter criteria.
        /// </summary>
        /// <typeparam name="T">Type of searched documents.</typeparam>
        /// <param name="collectionName">Name of collection to delete documents.</param>
        /// <param name="filter">Filter to perform the delete action.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public Task<DeleteResult> DeleteOneAsync<T>(string collectionName, FilterDefinition<T> filter,
            CancellationToken cancellationToken)
            where T : class
        {
            if (string.IsNullOrEmpty(collectionName)) throw new ArgumentNullException(nameof(collectionName));
            var collection = GetCollection<T>(collectionName);
            return collection.DeleteOneAsync(filter, cancellationToken);
        }

        public IEnumerable<T> Find<T>(string collectionName, FilterDefinition<T> filter,
            ProjectionDefinition<T> projection) where T : class
        {
            if (string.IsNullOrEmpty(collectionName)) throw new ArgumentNullException(nameof(collectionName));
            var collection = GetCollection<T>(collectionName);
            return collection.Find(filter).Project<T>(projection).ToList();
        }

        public async Task<IEnumerable<T>> FindAndSortAsync<T>(string collectionName, FilterDefinition<T> filter,
            ProjectionDefinition<T> projection, SortDefinition<T> sort, CancellationToken cancellationToken)
            where T : class
        {
            if (string.IsNullOrEmpty(collectionName)) throw new ArgumentNullException(nameof(collectionName));

            var collection = GetCollection<T>(collectionName);
            var result = await collection.Find(filter)
                .Sort(sort)
                .Project<T>(projection).ToListAsync(cancellationToken).ConfigureAwait(false);
            return result.AsEnumerable();
        }

        /// <summary>
        ///     Returns all elements in collection called as "CollectionName" parameter.
        /// </summary>
        /// <typeparam name="T">Expected type for the MongoDb collection.</typeparam>
        /// <param name="collectionName">Collection to find data.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>All data from collection.</returns>
        public async Task<IEnumerable<T>> FindAsync<T>(string collectionName, CancellationToken cancellationToken)
            where T : class
            =>
                await
                    FindAsync<T>(collectionName, DefaultFilter, DefaultProjection, cancellationToken)
                        .ConfigureAwait(false);

        /// <summary>
        ///     Returns all elements in collection called as "CollectionName" parameter filtering data
        ///     with "Filter" parameter and project data with "Projection" parameter.
        /// </summary>
        /// <typeparam name="T">Expected type for the MongoDb collection.</typeparam>
        /// <param name="collectionName">Collection to find data.</param>
        /// <param name="filter">Filter to use in MongoDb query.</param>
        /// <param name="projection">Projection to use in MongoDb query.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Data in MongoDb collection matched with filter.</returns>
        public async Task<IEnumerable<T>> FindAsync<T>(string collectionName, FilterDefinition<T> filter,
            ProjectionDefinition<T> projection, CancellationToken cancellationToken)
            where T : class
        {
            if (string.IsNullOrEmpty(collectionName)) throw new ArgumentNullException(nameof(collectionName));

            var collection = GetCollection<T>(collectionName);
            var result =
                await
                    collection.Find(filter).Project<T>(projection).ToListAsync(cancellationToken).ConfigureAwait(false);
            return result.AsEnumerable();
        }

        public async Task<IEnumerable<T>> FindAsync<T>(string collectionName, Expression<Func<T, bool>> filter,
            ProjectionDefinition<T> projection, CancellationToken cancellationToken)
            where T : class
        {
            if (string.IsNullOrEmpty(collectionName)) throw new ArgumentNullException(nameof(collectionName));

            var collection = GetCollection<T>(collectionName);
            var result =
                await
                    collection.Find(filter).Project<T>(projection).ToListAsync(cancellationToken).ConfigureAwait(false);
            return result.AsEnumerable();
        }

        /// <summary>
        ///     Returns the MongoDb collection that coincide with the "collectionName" parameter.
        /// </summary>
        /// <typeparam name="T">Type of collection to match</typeparam>
        /// <param name="collectionName">Name of collection to search.</param>
        /// <returns>The MongoDb collection that coincide with the "collectionName" parameter.</returns>
        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            var client = new MongoClient(_dbSettings.Settings.ConnectionString);
            var database = client.GetDatabase(_dbSettings.Settings.DatabaseName);
            return database.GetCollection<T>(collectionName);
        }

        public async Task<long> GetCountAsync<T>(string collectionName, FilterDefinition<T> filter,
            CancellationToken cancellationToken) where T : class
        {
            if (string.IsNullOrEmpty(collectionName)) throw new ArgumentNullException(nameof(collectionName));
            var collection = GetCollection<T>(collectionName);
            return await collection.Find(filter).CountAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        ///     Insert some documents in MongoDb collection ("CollectionName" parameter).
        /// </summary>
        /// <typeparam name="T">Type of documents to insert in collection.</typeparam>
        /// <param name="collectionName">Name of collection in which the documents is inserted.</param>
        /// <param name="documents">Documents to insert in MongoDb collection.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <param name="isOrdered">Indicates if query contains IsOrdered option.</param>
        public async Task InsertManyAsync<T>(string collectionName, IEnumerable<T> documents,
            CancellationToken cancellationToken, bool isOrdered = false)
            where T : class
        {
            if (string.IsNullOrEmpty(collectionName)) throw new ArgumentNullException(nameof(collectionName));
            var collection = GetCollection<T>(collectionName);
            var options = new InsertManyOptions {IsOrdered = isOrdered};
            await collection.InsertManyAsync(documents, options, cancellationToken);
        }

        /// <summary>
        ///     Insert a document in MongoDb collection ("CollectionName" parameter).
        /// </summary>
        /// <typeparam name="T">Type of document to insert in collection.</typeparam>
        /// <param name="collectionName">Name of the collection in which the document is inserted.</param>
        /// <param name="document">The document to insert in MongoDb collection.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public async Task InsertOneAsync<T>(string collectionName, T document, CancellationToken cancellationToken)
            where T : class
        {
            if (string.IsNullOrEmpty(collectionName)) throw new ArgumentNullException(nameof(collectionName));
            var collection = GetCollection<T>(collectionName);
            await collection.InsertOneAsync(document, null, cancellationToken);
        }

        /// <summary>
        ///     Update (By replacement strategy) the first occurrence of matched filter documents.
        /// </summary>
        /// <typeparam name="T">Type of updated document.</typeparam>
        /// <param name="collectionName">Name of update collection.</param>
        /// <param name="filter">Filter to match documents in collection.</param>
        /// <param name="document">Document to update</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <param name="isUpsert">Indicates if the update is upsert option.</param>
        /// <returns></returns>
        public Task<ReplaceOneResult> ReplaceOneAsync<T>(string collectionName, FilterDefinition<T> filter, T document,
            CancellationToken cancellationToken, bool isUpsert = true) where T : class
        {
            if (string.IsNullOrEmpty(collectionName)) throw new ArgumentNullException(nameof(collectionName));
            var collection = GetCollection<T>(collectionName);
            var options = new UpdateOptions {IsUpsert = isUpsert};
            return collection.ReplaceOneAsync(filter, document, options, cancellationToken);
        }

        /// <summary>
        ///     Update all matches documents in collection with the new data passed as parameters.
        /// </summary>
        /// <typeparam name="T">Type of updated documents.</typeparam>
        /// <param name="collectionName">Name of update collection.</param>
        /// <param name="filter">Filter to match documents in collection.</param>
        /// <param name="documents">Documents to perform the update action.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <param name="isUpsert">Indicates if the update is upsert option.</param>
        public Task<UpdateResult> UpdateManyAsync<T>(string collectionName, FilterDefinition<T> filter,
            UpdateDefinition<T> documents, CancellationToken cancellationToken, bool isUpsert = true)
            where T : class
        {
            if (string.IsNullOrEmpty(collectionName)) throw new ArgumentNullException(nameof(collectionName));
            var collection = GetCollection<T>(collectionName);
            var options = new UpdateOptions {IsUpsert = isUpsert};
            return collection.UpdateManyAsync(filter, documents, options, cancellationToken);
        }

        /// <summary>
        ///     Update the first occurrence of matched filter documents.
        /// </summary>
        /// <typeparam name="T">Type of updated document.</typeparam>
        /// <param name="collectionName">Name of update collection.</param>
        /// <param name="filter">Filter to match documents in collection.</param>
        /// <param name="document">Document to update</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <param name="isUpsert">Indicates if the update is upsert option.</param>
        /// <returns></returns>
        public Task<UpdateResult> UpdateOneAsync<T>(string collectionName, FilterDefinition<T> filter,
            UpdateDefinition<T> document, CancellationToken cancellationToken, bool isUpsert = true) where T : class
        {
            if (string.IsNullOrEmpty(collectionName)) throw new ArgumentNullException(nameof(collectionName));
            var collection = GetCollection<T>(collectionName);
            var options = new UpdateOptions {IsUpsert = isUpsert};
            return collection.UpdateOneAsync(filter, document, options, cancellationToken);
        }

        private static void Initialize()
        {
            //Register the GUID representation to String
            var guidSerializer = (GuidSerializer) BsonSerializer.LookupSerializer(typeof (Guid));
            if (guidSerializer == null)
            {
                BsonSerializer.RegisterSerializer(typeof (Guid), new GuidSerializer(BsonType.String));
            }
        }
    }
}
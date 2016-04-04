using SoftChess.Inc.Data.Settings;

namespace SoftChess.Inc.Data.DbProviders.Mongo.Settings
{
    public class MongoDbSettings : IDbSettingSet
    {
        /// <summary>
        ///     Name of SoftChess database
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        ///     Collecttion name for Historical movements storage
        /// </summary>
        public string HistoricalMovementsCollectionName { get; set; }

        /// <summary>
        ///     Collection name for pieces
        /// </summary>
        public string PiecesCollectionName { get; set; }

        /// <summary>
        ///     Collection name for Ruleset collection
        /// </summary>
        public string RuleSetsCollectionName { get; set; }

        /// <summary>
        ///     Connection string to connect to SoftChess database
        /// </summary>
        public string ConnectionString { get; set; }
    }
}
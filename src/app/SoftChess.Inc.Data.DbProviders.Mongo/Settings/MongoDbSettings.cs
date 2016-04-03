using SoftChess.Inc.Data.Settings;

namespace SoftChess.Inc.Data.DbProviders.Mongo.Settings
{
    public class MongoDbSettings : IDbSettingSet
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
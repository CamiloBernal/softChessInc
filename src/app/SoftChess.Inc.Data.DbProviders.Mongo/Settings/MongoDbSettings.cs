using SoftChess.Inc.Data.Settings;

namespace SoftChess.Inc.Data.DbProviders.Mongo.Settings
{
    public class MongoDbSettings : IDbSettingSet
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
    }
}
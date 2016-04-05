using System.Configuration;
using Ninject.Modules;
using SoftChess.Inc.Data.DataProxies;
using SoftChess.Inc.Data.DataProxies.Mongo.PersistenceServices;
using SoftChess.Inc.Data.DbProviders.Mongo.Settings;
using SoftChess.Inc.Data.Settings;

namespace SoftChess.Inc.Clients.Web
{
    public class NinjectBootstrap : NinjectModule
    {
        public override void Load()
        {
            var defaultStorage = ConfigurationManager.AppSettings["DefaultStorage"];
            if (!defaultStorage.Equals("MongoDb")) return;
            var mongoDbSettings = new MongoDbSettings
            {
                DatabaseName = ConfigurationManager.AppSettings["DatabaseName"],
                ConnectionString = ConfigurationManager.AppSettings["DefaultMongoConnectionString"],
                PiecesCollectionName = ConfigurationManager.AppSettings["PiecesCollectionName"],
                RuleSetsCollectionName = ConfigurationManager.AppSettings["RuleSetsCollectionName"],
                HistoricalMovementsCollectionName =
                    ConfigurationManager.AppSettings["HistoricalMovementsCollectionName"]
            };
            var dbSettings = new DbSettings<MongoDbSettings>(mongoDbSettings);
            Bind<IRulePersistence>()
                .To<RulePersistenceService>()
                .WithConstructorArgument("settings", dbSettings);
        }
    }
}
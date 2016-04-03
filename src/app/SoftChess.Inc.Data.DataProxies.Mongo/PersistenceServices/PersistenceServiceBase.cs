using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using SoftChess.Inc.Data.DbProviders.Mongo;
using SoftChess.Inc.Data.DbProviders.Mongo.Settings;
using SoftChess.Inc.Data.Settings;

namespace SoftChess.Inc.Data.DataProxies.Mongo.PersistenceServices
{
    public abstract class PersistenceServiceBase : IDataProxy
    {
        // ReSharper disable once InconsistentNaming
        protected bool _initialized;

        protected PersistenceServiceBase(DbSettings<MongoDbSettings> settings, MongoDbHelper dbHelper)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));
            if (dbHelper == null) throw new ArgumentNullException(nameof(settings));
            DbHelper = dbHelper;
            Settings = settings;

            Configure();
        }

        protected MongoDbHelper DbHelper { get; }

        protected DbSettings<MongoDbSettings> Settings { get; }

        protected void Configure()
        {
            Initialize();
        }

        public virtual void Initialize()
        {
            RegisterConventions.RegisterMongoConventions();
            //Register the GUID representation to String
            var guidSerializer = (GuidSerializer)BsonSerializer.LookupSerializer(typeof(Guid));
            if (guidSerializer == null)
            {
                BsonSerializer.RegisterSerializer(typeof(Guid), new GuidSerializer(BsonType.String));
            }
            _initialized = true;
        }

        public bool Initialized => _initialized;
    }
}
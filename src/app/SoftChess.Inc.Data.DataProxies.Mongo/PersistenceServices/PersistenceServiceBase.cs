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
        protected readonly MongoDbHelper DbHelper;

        // ReSharper disable once InconsistentNaming
        protected bool _initialized;

        protected PersistenceServiceBase(DbSettings<MongoDbSettings> settings)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));
            DbHelper = new MongoDbHelper(settings);
            Settings = settings;
            Configure();
        }

        protected DbSettings<MongoDbSettings> Settings { get; }

        public bool Initialized => _initialized;

        public virtual void Initialize()
        {
            RegisterConventions.RegisterMongoConventions();
            //Register the GUID representation to String
            var guidSerializer = (GuidSerializer) BsonSerializer.LookupSerializer(typeof (Guid));
            if (guidSerializer == null)
            {
                BsonSerializer.RegisterSerializer(typeof (Guid), new GuidSerializer(BsonType.String));
            }
            _initialized = true;
        }

        protected void Configure()
        {
            Initialize();
        }
    }
}
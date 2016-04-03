using MongoDB.Bson.Serialization.Conventions;

namespace SoftChess.Inc.Data.DbProviders.Mongo.Settings
{
    public static class RegisterConventions
    {
        public static void RegisterMongoConventions()
        {
            var conventionPack = new ConventionPack
            {
                new IgnoreExtraElementsConvention(true),
                new IgnoreIfNullConvention(true)
            };
            ConventionRegistry.Register("SoftChess.Inc.Data.MongoDB.Conventions", conventionPack, type => true);
        }
    }
}
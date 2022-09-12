namespace Play.Catalog.Service.Settings
{
    public class MongoDbSettings
    {
        //we set init to prevent future changes to the below properties
        public string Host { get; init; }
        public int Port { get; init; }

        public string ConnectionString => $"mongodb://{Host}:{Port}";
    }

}
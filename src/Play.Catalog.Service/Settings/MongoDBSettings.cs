namespace Play.Catalog.Service.Settings
{
    public class MongoDBSettings
    {
        public string Host { get; init; }
        public int Port { get; init; }
        public string ConnecctionString => $"mongodb://{Host}:{Port}";
    }
}

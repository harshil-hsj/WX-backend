using MongoDB.Driver;
using WeoponX.Models;
public class MongoDbContext
{
    private readonly IMongoDatabase _database;
    public IMongoCollection<User> Users { get; }

    public MongoDbContext(IConfiguration config)
    {
        var connectionString = config["MongoDB:ConnectionString"];
        var databaseName = config["MongoDB:DatabaseName"];

        if (string.IsNullOrEmpty(connectionString))
            throw new Exception("MongoDB connection string missing");

        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
        Users = _database.GetCollection<User>("Users");
    }
}

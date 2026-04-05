using MongoDB.Driver;
using WeoponX.Models;
public class MongoDbContext
{
    private readonly IMongoDatabase _database;
    public IMongoCollection<User> Users { get; }
    public IMongoCollection<EmailOtp> EmailOtps { get; }
    public IMongoCollection<EmailLog> EmailLogs { get; }

    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        return _database.GetCollection<T>(collectionName);
    }

    public MongoDbContext(IConfiguration config)
    {
        var connectionString = config["MongoDB:ConnectionString"];
        var databaseName = config["MongoDB:DatabaseName"];

        if (string.IsNullOrEmpty(connectionString))
            throw new Exception("MongoDB connection string missing");

        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
        Users = _database.GetCollection<User>("Users");
        EmailOtps = _database.GetCollection<EmailOtp>("EmailOtps");
        EmailLogs = _database.GetCollection<EmailLog>("EmailLogs");
    }
}

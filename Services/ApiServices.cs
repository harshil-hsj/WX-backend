using MongoDB.Driver;
using WeoponX.Models;

namespace WeoponX.Services;

public class ApiServices : IApiServices
{
   private readonly MongoDbContext _db;

    public ApiServices(MongoDbContext db)
    {
        _db = db;
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _db.Users
    .Find(_ => true)
    .Project<User>(Builders<User>.Projection.Exclude("_id"))
    .ToListAsync();
    }
}

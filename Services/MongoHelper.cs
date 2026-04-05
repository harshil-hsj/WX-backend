using System.Threading.Tasks;
using MongoDB.Driver;

namespace WeoponX.Services
{
    public static class MongoHelper
    {
        public static async Task InsertAsync<T>(MongoDbContext dbContext, T document, string? collectionName = null)
        {
            var type = typeof(T);
            var collection = collectionName != null
                ? dbContext.GetCollection<T>(collectionName)
                : dbContext.GetCollection<T>(type.Name + "s"); // e.g., User -> Users
            await collection.InsertOneAsync(document);
        }

        public static async Task<bool> ExistsAsync<T>(MongoDbContext dbContext, FilterDefinition<T> filter, string? collectionName = null)
        {
            var type = typeof(T);
            var collection = collectionName != null
                ? dbContext.GetCollection<T>(collectionName)
                : dbContext.GetCollection<T>(type.Name + "s");
            return await collection.Find(filter).AnyAsync();
        }
    }
}

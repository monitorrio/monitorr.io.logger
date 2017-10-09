using System.Threading.Tasks;
using log.elmahbucket.io.Infrastructure.Repositories.Interfaces;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using log.elmahbucket.io.Infrastructure.Configuration;
using log.elmahbucket.io.Infrastructure.Domain;
using System.Collections.Generic;
using MongoDB.Bson;

namespace log.elmahbucket.io.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoDatabase _database;
        private readonly Settings _settings;

        public UserRepository(IOptions<Settings> settings)
        {
            _settings = settings.Value;
            _database = Connect();
        }
        public async Task<IList<User>> GetUserByIdAsync(IList<ObjectId> userIds)
        {
            var filter = Builders<User>.Filter.In(u => u._id, userIds);
            return await _database.GetCollection<User>("User").Find(filter).ToListAsync();
        }

        private IMongoDatabase Connect()
        {
            var client = new MongoClient(_settings.App.MongoConnection);
            var database = client.GetDatabase(_settings.App.Database);

            return database;
        }
    }
}

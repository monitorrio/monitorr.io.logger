using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using log.elmahbucket.io.Infrastructure.Configuration;
using log.elmahbucket.io.Infrastructure.Interfaces;
using log.elmahbucket.io.Infrastructure.Domain;

namespace log.elmahbucket.io.Infrastructure.Repositories
{
    public class ErrorRepository : IErrorRepository
    {
        private readonly IMongoDatabase _database;
        private readonly Settings _settings;

        public ErrorRepository(IOptions<Settings> settings)
        {
            _settings = settings.Value;
            _database = Connect();
        }

        public void Add(Error error)
        {
            _database.GetCollection<Error>("Error").InsertOneAsync(error);
        }

        public async Task AddAsync(Error error)
        {
           await _database.GetCollection<Error>("Error").InsertOneAsync(error);
        }

        private IMongoDatabase Connect()
        {
            var client = new MongoClient(_settings.App.MongoConnection);
            var database = client.GetDatabase(_settings.App.Database);

            return database;
        }
    }
}

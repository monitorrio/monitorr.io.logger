using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using monitorr.logger.Infrastructure.Configuration;
using monitorr.logger.Infrastructure.Interfaces;
using monitorr.logger.Infrastructure.Domain;

namespace monitorr.logger.Infrastructure.Repositories
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

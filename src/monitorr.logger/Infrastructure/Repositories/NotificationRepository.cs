using monitorr.logger.Infrastructure.Configuration;
using monitorr.logger.Infrastructure.Domain;
using monitorr.logger.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace monitorr.logger.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly IMongoDatabase _database;
        private readonly Settings _settings;

        readonly IMongoCollection<Notification> _collection;

        public NotificationRepository()
        {
        }

        public NotificationRepository(IOptions<Settings> settings)
        {
            _settings = settings.Value;
            var CollectionName = new Notification().GetType().Name;
            var client = new MongoClient(_settings.App.MongoConnection);
            var database = client.GetDatabase(_settings.App.Database);
            _collection = database.GetCollection<Notification>(CollectionName);

        }

        public async Task<IList<ObjectId>> GetSubscribedToNewErrorAsync(string logId)
        {
            return await _collection
                  .Find(log => log.LogId == logId && log.IsNewErrorEmail)
                  .Project(l => ObjectId.Parse(l.UserId))
                  .ToListAsync();
        }
    }
}

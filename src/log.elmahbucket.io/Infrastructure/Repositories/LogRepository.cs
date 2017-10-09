﻿using log.elmahbucket.io.Infrastructure.Configuration;
using log.elmahbucket.io.Infrastructure.Domain;
using log.elmahbucket.io.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace log.elmahbucket.io.Infrastructure.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly IMongoDatabase _database;
        private readonly Settings _settings;

        public LogRepository(IOptions<Settings> settings)
        {
            _settings = settings.Value;
            _database = Connect();
        }

        public async Task<Log> GetByIdAsync(string logId)
        {
            return await _database.GetCollection<Log>("Log")
                  .Find(log => log.LogId == logId)
                  .SingleAsync();
        }

        private IMongoDatabase Connect()
        {
            var client = new MongoClient(_settings.App.MongoConnection);
            var database = client.GetDatabase(_settings.App.Database);

            return database;
        }
    }

}

using log.elmahbucket.io.Infrastructure.Models;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace log.elmahbucket.io.Infrastructure.Services
{
    public interface INotificationService
    {
        Task SendNewErrorNotificationAsync(ErrorModel model, IList<ObjectId> userIds);
    }
}
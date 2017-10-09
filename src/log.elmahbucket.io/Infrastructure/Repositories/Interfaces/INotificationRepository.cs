using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace log.elmahbucket.io.Infrastructure.Repositories.Interfaces
{
    public interface INotificationRepository
    {
        Task<IList<ObjectId>> GetSubscribedToNewErrorAsync(string logId);
    }
}
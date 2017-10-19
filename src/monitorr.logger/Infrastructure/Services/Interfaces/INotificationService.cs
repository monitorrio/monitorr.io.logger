using monitorr.logger.Infrastructure.Models;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace monitorr.logger.Infrastructure.Services
{
    public interface INotificationService
    {
        Task SendNewErrorNotificationAsync(ErrorModel model, IList<ObjectId> userIds);
    }
}
using monitorr.logger.Infrastructure.Domain;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace monitorr.logger.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IList<User>> GetUserByIdAsync(IList<ObjectId> userIds);
    }
}

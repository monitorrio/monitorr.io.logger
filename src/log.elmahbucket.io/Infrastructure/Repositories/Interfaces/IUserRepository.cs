using log.elmahbucket.io.Infrastructure.Domain;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace log.elmahbucket.io.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IList<User>> GetUserByIdAsync(IList<ObjectId> userIds);
    }
}

using log.elmahbucket.io.Infrastructure.Domain;
using System.Threading.Tasks;

namespace log.elmahbucket.io.Infrastructure.Repositories.Interfaces
{
    public interface ILogRepository
    {
        Task<Log> GetByIdAsync(string logId);
    }
}

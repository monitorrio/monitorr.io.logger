using monitorr.logger.Infrastructure.Domain;
using System.Threading.Tasks;

namespace monitorr.logger.Infrastructure.Repositories.Interfaces
{
    public interface ILogRepository
    {
        Task<Log> GetByIdAsync(string logId);
    }
}

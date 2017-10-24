using monitorr.logger.Infrastructure.Domain;
using System.Threading.Tasks;

namespace monitorr.logger.Infrastructure.Interfaces
{
    public interface IErrorRepository
    {
        void Add(Error error);
        Task AddAsync(Error error);
    }
}

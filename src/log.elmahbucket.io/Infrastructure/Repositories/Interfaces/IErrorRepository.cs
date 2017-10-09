using log.elmahbucket.io.Infrastructure.Domain;
using System.Threading.Tasks;

namespace log.elmahbucket.io.Infrastructure.Interfaces
{
    public interface IErrorRepository
    {
        void Add(Error error);
        Task AddAsync(Error error);
    }
}

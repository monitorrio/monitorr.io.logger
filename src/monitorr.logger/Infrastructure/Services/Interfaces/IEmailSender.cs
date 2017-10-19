using monitorr.logger.Infrastructure.Models;
using System.Threading.Tasks;

namespace monitorr.logger.Infrastructure.Services.Interfaces
{
    public interface IEmailSender
    {
        Task SendAsync(SendEmailModel sendEmailModel);
    }
}

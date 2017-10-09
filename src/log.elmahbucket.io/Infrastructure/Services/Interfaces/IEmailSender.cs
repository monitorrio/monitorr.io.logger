using log.elmahbucket.io.Infrastructure.Models;
using System.Threading.Tasks;

namespace log.elmahbucket.io.Infrastructure.Services.Interfaces
{
    public interface IEmailSender
    {
        Task SendAsync(SendEmailModel sendEmailModel);
    }
}

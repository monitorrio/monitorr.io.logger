using log.elmahbucket.io.Infrastructure.Configuration;
using log.elmahbucket.io.Infrastructure.Models;
using log.elmahbucket.io.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using log.elmahbucket.io.Infrastructure.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;

namespace log.elmahbucket.io.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private IViewRenderService _viewRenderService;
        private IEmailSender _emailSender;
        private IOptions<Settings> _settings;
        private IUserRepository _userRepository;

        public NotificationService(IUserRepository userRepository,
            IViewRenderService viewRenderService,
            IEmailSender emailSender,
            IOptions<Settings> settings)
        {
            _userRepository = userRepository;
            _viewRenderService = viewRenderService;
            _emailSender = emailSender;
            _settings = settings;
        }
        public async Task SendNewErrorNotificationAsync(ErrorModel model, IList<ObjectId> userIds)
        {
            var templateData = BuildNewErrorTemplateModel(model);
            var message = await _viewRenderService.RenderToStringAsync("Emails/NewErrorTemplate", templateData);

            var users = await _userRepository.GetUserByIdAsync(userIds);

            var amazonSesRecipientsLimit = 50;
            var splittedUsers = ChunkBy(users, amazonSesRecipientsLimit);

            foreach (var chunk in splittedUsers)
            {
                var emails = chunk.Select(u => u.Email).ToList();

                var sendEmailModel = new SendEmailModel
                {
                    Body = message,
                    Subject = _settings.Value.Email.NewErrorSubject,
                    BccEmails = emails,
                    FromEmail = _settings.Value.Email.From
                };
                await _emailSender.SendAsync(sendEmailModel);
            }
        }

        private List<List<T>> ChunkBy<T>(IList<T> source, int chunkSize)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }

        private NewErrorEmailTemplateModel BuildNewErrorTemplateModel(ErrorModel model)
        {
            return new NewErrorEmailTemplateModel
            {
                Message = model.Message,
                Time = model.Time,
                Detail = model.Detail
            };
        }
    }
}

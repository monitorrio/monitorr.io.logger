using Amazon;
using Amazon.Runtime;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using monitorr.logger.Infrastructure.Configuration;
using monitorr.logger.Infrastructure.Models;
using monitorr.logger.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace monitorr.logger.Infrastructure.Services
{
    public class AmazonEmailService : IEmailSender
    {
        private readonly IOptions<Settings> _settings;
        private readonly IHostingEnvironment _currentEnvironment;
        public AmazonEmailService(IHostingEnvironment env,
            IOptions<Settings> settings)
        {
            _currentEnvironment = env;
            _settings = settings;
        }
        public async Task SendAsync(SendEmailModel sendEmailModel)
        {
            var from = sendEmailModel.FromEmail;

            var destination = new Destination
            {
                ToAddresses = sendEmailModel.Emails,
                BccAddresses = sendEmailModel.BccEmails                
            };

            // Only send email to real users in production
            if (!_currentEnvironment.IsProduction())
            {
                destination.ToAddresses = _settings.Value.Email.TestEmailRecivers.Split(',').ToList();
            }

            Content subject = new Content(sendEmailModel.Subject);
            Content textBody = new Content(sendEmailModel.Body);
            Body body = new Body
            {
                Html = textBody
            };
            Message message = new Message(subject, body);

            SendEmailRequest request = new SendEmailRequest(from, destination, message);

            if (!string.IsNullOrWhiteSpace(sendEmailModel.ReplyTo))
            {
                request.ReplyToAddresses = new List<string>();
                request.ReplyToAddresses.Add(sendEmailModel.ReplyTo);
            }            

            AWSCredentials credentials = new BasicAWSCredentials(_settings.Value.Aws.ClientId,
              _settings.Value.Aws.SecretKey);

            RegionEndpoint region = RegionEndpoint.GetBySystemName(_settings.Value.Aws.SesRegion);

            AmazonSimpleEmailServiceClient client = new AmazonSimpleEmailServiceClient(credentials, region);

            var resp = await client.SendEmailAsync(request);
        }
    }
}

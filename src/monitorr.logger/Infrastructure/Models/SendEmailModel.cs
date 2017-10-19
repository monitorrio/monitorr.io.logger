using System.Collections.Generic;

namespace monitorr.logger.Infrastructure.Models
{
    public class SendEmailModel
    {
        public List<string> Emails { get; set; } = new List<string>();
        public List<string> BccEmails { get; set; } = new List<string>();
        public string Subject { get; set; }
        public string Body { get; set; }
        public string FromEmail { get; set; }
        public string ReplyTo { get; set; }

        public SendEmailModel AddRecipient(string email)
        {
            Emails.Add(email);
            return this;
        }
    }
}

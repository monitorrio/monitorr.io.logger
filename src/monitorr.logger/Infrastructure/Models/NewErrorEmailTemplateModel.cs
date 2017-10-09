using System;

namespace monitorr.logger.Infrastructure.Models
{
    public class NewErrorEmailTemplateModel
    {
        public string Message { get; internal set; }
        public string Detail { get; internal set; }
        public DateTime Time { get; internal set; }
    }
}

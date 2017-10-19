using System;
using System.Collections.Generic;

namespace monitorr.logger.Infrastructure.Domain
{
    public class Error : MongoEntity
    {
        public string Guid { get; set; }
        public string ApplicationName { get; set; }
        public string Host { get; set; }
        public string Type { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }
        public string User { get; set; }
        public DateTime Time { get; set; }
        public int? StatusCode { get; set; }
        public string Browser { get; set; }
        public string Detail { get; set; }
        public string Url { get; set; }

        public bool IsCustom { get; set; }

        public Dictionary<string, string> ServerVariables { get; set; }
        public Dictionary<string, string> QueryString { get; set; }
        public Dictionary<string, string> Form { get; set; }
        public Dictionary<string, string> Cookies { get; set; }

        public string LogId { get; set; }
        public Severity Severity { get; set; }
        public string Method { get; set; }
    }
}

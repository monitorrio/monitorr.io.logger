using monitorr.logger.Infrastructure.Domain;
using monitorr.logger.Infrastructure.Models;

namespace monitorr.logger.Infrastructure.Extensions
{
    public static class ErrorModelExtensions
    {
        public static Error MapToEntity(this ErrorModel model)
        {
            var entity = new Error
            {
                Guid = model.Guid,
                Host = model.Host,
                Type = model.Type,
                Source = model.Source,
                Message = model.Message,
                User = model.User,
                Time = model.Time,
                StatusCode = model.StatusCode,
                Browser = model.Browser,
                Detail = model.Detail,
                ServerVariables = model.ServerVariables,
                QueryString = model.QueryString,
                Form = model.Form,
                Cookies = model.Cookies,
                LogId = model.LogId,
                IsCustom = model.IsCustom,
                Url = model.Url,
                Severity = model.Severity,
                Method = model.Method,
                CustomData = model.CustomData
            };

            return entity;
        }
    }
}

using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace monitorr.logger.Infrastructure.Filters
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly IHostingEnvironment _env;
        private readonly ILogger<ApiExceptionFilter> _logger;
        private ApiError _apiError;

        public ApiExceptionFilter(ILoggerFactory loggerFactory, IHostingEnvironment env)
        {
            _loggerFactory = loggerFactory;
            _env = env;
            _logger = loggerFactory.CreateLogger<ApiExceptionFilter>();
        }

        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ApiException)
            {
                HandleKnownError(context);
            }
            else if (context.Exception is UnauthorizedAccessException)
            {
                HandleUnauthorizedException(context);
            }
            else
            {
                UnhandeledException(context);
            }

            context.Result = new JsonResult(_apiError);

            base.OnException(context);
        }

        private void UnhandeledException(ExceptionContext context)
        {
            string msg, stack;
            if (_env.IsProduction())
            {
                msg = "An unhandled error occurred.";
                stack = null;
            }
            else
            {
                msg = context.Exception.GetBaseException().Message;
                stack = context.Exception.StackTrace;
            }

            _apiError = new ApiError(msg)
            {
                Detail = stack
            };

            context.HttpContext.Response.StatusCode = 500;

            _logger.LogError(new EventId(0), context.Exception, msg);
        }

        private void HandleUnauthorizedException(ExceptionContext context)
        {
            _apiError = new ApiError("Unauthorized Access");
            context.HttpContext.Response.StatusCode = 401;
            _logger.LogWarning("Unauthorized Access in Controller Filter.");
        }

        private void HandleKnownError(ExceptionContext context)
        {
            var ex = context.Exception as ApiException;
            context.Exception = null;
            _apiError = new ApiError(ex.Message)
            {
                Errors = ex.Errors
            };

            context.HttpContext.Response.StatusCode = ex.StatusCode;

            _logger.LogWarning($"Application thrown error: {ex.Message}", ex);
        }
    }
}

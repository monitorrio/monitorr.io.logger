using monitorr.logger.Infrastructure;
using monitorr.logger.Infrastructure.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;

namespace monitorr.logger.Controllers
{
    [Route("whoami")]
    public class WhoamiController : Controller
    {
        private readonly IHostingEnvironment _currentEnvironment;
        private readonly Settings _settings;

        public WhoamiController(IHostingEnvironment env, IOptions<Settings> settings)
        {
            _currentEnvironment = env;
            _settings = settings.Value;
        }

        [HttpGet, Route("")]
        public IActionResult Get()
        {
            var res = new
            {
                PlatformServices.Default.Application.ApplicationName,
                PlatformServices.Default.Application.ApplicationVersion,
                PlatformServices.Default.Application.RuntimeFramework,
                PlatformServices.Default.Application.ApplicationBasePath,
                _currentEnvironment.EnvironmentName,
                IsDevelopment = _currentEnvironment.IsDevelopment(),
                IsStaging = _currentEnvironment.IsStaging(),
                IsProduction = _currentEnvironment.IsProduction(),
                _settings.App.MongoConnection,
                _settings.App.Database
            };

            return Ok(res);
        }
    }
}

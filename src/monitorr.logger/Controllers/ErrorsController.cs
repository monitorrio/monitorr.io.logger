using System.Threading.Tasks;
using monitorr.logger.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using monitorr.logger.Infrastructure.Services;
using monitorr.logger.Infrastructure.Models;
using monitorr.logger.Infrastructure.Interfaces;
using monitorr.logger.Infrastructure.Repositories.Interfaces;

namespace monitorr.logger.Controllers
{
    [Route("/v1/[controller]")]
    public class ErrorsController : Controller
    {
        private readonly IErrorRepository _errorRepository;
        private readonly INotificationService _notificationService;
        private readonly ILogRepository _logRepository;
        private INotificationRepository _notificationRepository;

        public ErrorsController(IErrorRepository errorRepository,
            ILogRepository logRepository,
            INotificationRepository notificationRepository,
            INotificationService notificationService)
        {
            _errorRepository = errorRepository;
            _logRepository = logRepository;
            _notificationRepository = notificationRepository;
            _notificationService = notificationService;
        }

        [HttpPost, Route("/v1/errors")]
        public async Task<IActionResult> Post([FromBody] ErrorModel model)
        {
            if (string.IsNullOrWhiteSpace(model.LogId))
            {
                return BadRequest("Log Id cannot be empty");
            }

            RemoveInvalidCharacters(model);

            var entity = model.MapToEntity();
            await _errorRepository.AddAsync(entity);

            var userIds = await _notificationRepository.GetSubscribedToNewErrorAsync(model.LogId);
            if(userIds.Count > 0)
            {
                await _notificationService.SendNewErrorNotificationAsync(model,userIds);
            }           

            return Ok();
        }

        private void RemoveInvalidCharacters(ErrorModel model)
        {
            model.Cookies = model.Cookies.RemoveInvalidCharacters();
            model.Form = model.Form.RemoveInvalidCharacters();
            model.ServerVariables = model.ServerVariables.RemoveInvalidCharacters();
            model.QueryString = model.QueryString.RemoveInvalidCharacters();
        }
    }
}

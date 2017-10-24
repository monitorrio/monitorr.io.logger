using System.Threading.Tasks;

namespace monitorr.logger.Infrastructure.Services.Interfaces
{
    public interface IViewRenderService
    {
        Task<string> RenderToStringAsync(string viewName, object model);
    }
}

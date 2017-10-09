using System.Threading.Tasks;

namespace log.elmahbucket.io.Infrastructure.Services.Interfaces
{
    public interface IViewRenderService
    {
        Task<string> RenderToStringAsync(string viewName, object model);
    }
}

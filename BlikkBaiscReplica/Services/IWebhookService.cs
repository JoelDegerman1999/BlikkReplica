using System.Threading.Tasks;

namespace BlikkBasicReplica.API.Services
{
    public interface IWebhookService
    {
        Task<bool> SendHookToSubscribedHooks<T>(string eventName, T entity, string userId);
    }
}

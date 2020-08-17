using System.Threading.Tasks;

namespace BlikkBasicReplica.API.Services
{
    public interface IWebhookService
    {
        Task<bool> SendHookToSubscribed<T>(string eventName, T entity, string userId);
    }
}

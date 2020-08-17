using System.Threading.Tasks;

namespace BlikkBasicReplica.Webhooks.Services
{
    public interface IWebhookService
    {
        Task<bool> SendHookToSubscribed<T>(string eventName, T entity, string userId);
    }
}

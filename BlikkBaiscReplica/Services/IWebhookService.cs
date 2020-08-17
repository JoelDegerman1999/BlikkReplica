using System.Threading.Tasks;

namespace BlikkBaiscReplica.Services
{
    public interface IWebhookService
    {
        Task<bool> SendHookToSubscribed<T>(string eventName, T entity, string userId);
    }
}

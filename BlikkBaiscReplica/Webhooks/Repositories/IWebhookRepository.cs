using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlikkBaiscReplica.Webhooks.Repositories
{
    public interface IWebhookRepository
    {
        Task<WebhookSubscription> CreateSubscription(WebhookSubscription model);
        Task<WebhookSubscription> DeleteSubscription(string ownerId);
        Task<WebhookSubscription> SearchSubscription(string ownerId);
        Task<WebhookSubscription> UpdateSubscription(WebhookSubscription model);
        Task<List<WebhookSubscription>> ListSubscriptions();
        Task<List<WebhookSubscription>> ListSubscriptions(string eventName);
    }
}

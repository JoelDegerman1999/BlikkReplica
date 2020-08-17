using System.Collections.Generic;
using System.Threading.Tasks;
using BlikkBasicReplica.API.Webhooks.Models;

namespace BlikkBasicReplica.API.Webhooks.Repositories
{
    public interface IWebhookRepository
    {
        Task<WebhookSubscription> CreateSubscription(WebhookSubscription model);
        Task<WebhookSubscription> DeleteSubscription(WebhookSubscription model);
        Task<WebhookSubscription> SearchSubscription(int id);
        Task<WebhookSubscription> UpdateSubscription(WebhookSubscription model);
        Task<List<WebhookSubscription>> ListSubscriptions();
        Task<List<WebhookSubscription>> ListSubscriptions(string eventName);
    }
}

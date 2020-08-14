using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlikkBaiscReplica.RestHooks;

namespace BlikkBaiscReplica.RestHooks
{
    public interface IWebhookRepository
    {
        Task<WebhookSubscription> CreateSubscription(WebhookSubscription model);
        Task<WebhookSubscription> DeleteSubscription(string ownerId);
        Task<WebhookSubscription> SearchSubscription(string ownerId);
        Task<WebhookSubscription> UpdateSubscription(WebhookSubscription model);
        Task<List<WebhookSubscription>> ListSubscriptions();
    }
}

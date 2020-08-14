using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlikkBaiscReplica.Data;
using Microsoft.EntityFrameworkCore;

namespace BlikkBaiscReplica.Webhooks.Repositories
{
    public class WebhookRepository : IWebhookRepository
    {
        private readonly ApplicationDbContext _context;

        public WebhookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<WebhookSubscription> CreateSubscription(WebhookSubscription model)
        {
            await _context.WebhookSubscriptions.AddAsync(model);
            var result = await _context.SaveChangesAsync();
            return result > 0 ? model : null;
        }

        public async Task<WebhookSubscription> DeleteSubscription(WebhookSubscription model)
        {
            _context.WebhookSubscriptions.Remove(model);
            var result = await _context.SaveChangesAsync();

            return result > 0 ? model : null;
        }

        public async Task<List<WebhookSubscription>> ListSubscriptions()
        {
            var webSubs = await _context.WebhookSubscriptions.ToListAsync();
            return webSubs;
        }

        public async Task<List<WebhookSubscription>> ListSubscriptions(string eventName)
        {
            var webSubs = await _context.WebhookSubscriptions
                .Where(x => x.EventName == eventName)
                .ToListAsync();

            return webSubs;
        }

        public async Task<WebhookSubscription> SearchSubscription(int id)
        {
            var webSub = await _context.WebhookSubscriptions
                .FirstOrDefaultAsync(x => x.Id == id);
            return webSub ?? null;
        }

        public async Task<WebhookSubscription> UpdateSubscription(WebhookSubscription model)
        {
            _context.WebhookSubscriptions.Update(model);

            var result = await _context.SaveChangesAsync();

            return result > 0 ? model : null;
        }
    }
}
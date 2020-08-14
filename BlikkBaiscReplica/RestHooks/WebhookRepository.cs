using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlikkBaiscReplica.Data;
using BlikkBaiscReplica.RestHooks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlikkBaiscReplica.RestHooks
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

        public async Task<WebhookSubscription> DeleteSubscription(string ownerId)
        {
            var webSub = await _context.WebhookSubscriptions.FirstOrDefaultAsync(x => x.OwnerId == ownerId);

            if (webSub != null) _context.WebhookSubscriptions.Remove(webSub);
            var result = await _context.SaveChangesAsync();

            return result > 0 ? webSub : null;
        }

        public async Task<List<WebhookSubscription>> ListSubscriptions()
        {
            var webSubs = await _context.WebhookSubscriptions.ToListAsync();
            return webSubs;
        }

        public async Task<WebhookSubscription> SearchSubscription(string ownerId)
        {
            var webSub = await _context.WebhookSubscriptions
                .FirstOrDefaultAsync(x => x.OwnerId == ownerId);
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
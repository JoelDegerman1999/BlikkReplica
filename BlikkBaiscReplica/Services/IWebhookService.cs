using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BlikkBaiscReplica.Helpers;
using BlikkBaiscReplica.Webhooks;

namespace BlikkBaiscReplica.Services
{
    public interface IWebhookService
    {
        Task<bool> SendHookToSubscribed<T>(string eventName, T entity, string userId);
    }
}

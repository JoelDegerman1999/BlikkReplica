using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BlikkBasicReplica.API.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BlikkBasicReplica.API.Services
{
    public class WebhookService : IWebhookService
    {
        private readonly IWebhookRepository _repository;
        private readonly IHttpClientFactory _clientFactory;

        public WebhookService(IWebhookRepository repository, IHttpClientFactory clientFactory)
        {
            _repository = repository;
            _clientFactory = clientFactory;
        }

        public async Task<bool> SendHookToSubscribedHooks<T>(string eventName, T entity, string userId)
        {
            var subs = await _repository.ListSubscriptions(eventName);

            //Filtrerar bort subscriptions som inte tillhör användaren
            if (subs.Count > 0)
            {
                subs = subs.Where(q => q.OwnerId == userId).ToList();
            }

            var payload = CreateJsonPayload(entity);

            //Loopar igenom alla subscriptions den inloggade användaren har och postar en request till den med en/ett request payload
            foreach (var sub in subs)
            {
                try
                {
                    var client = _clientFactory.CreateClient();
                    var success = await client.PostAsync(sub.TargetUrl, payload);
                    while (!success.IsSuccessStatusCode)
                    {
                        success = await client.PostAsync(sub.TargetUrl, payload);
                    }
                }

                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            return true;
        }

        private static StringContent CreateJsonPayload<T>(T entity)
        {
            var settings = new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver()};
            var serialized = JsonConvert.SerializeObject(entity, settings);
            var payload = new StringContent(serialized);
            return payload;
        }
    }
}
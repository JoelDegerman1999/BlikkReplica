﻿using System;
using System.Net.Http;
using System.Security.Claims;
using BlikkBaiscReplica.Webhooks.Repositories;
using System.Threading.Tasks;
using BlikkBaiscReplica.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BlikkBaiscReplica.Services
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
        public async Task<bool> SendHookToSubscribed<T>(string eventName, T entity)
        {

            var payload = CreateJsonPayload<T>(entity);

            var subs = await _repository.ListSubscriptions(eventName);
            foreach (var sub in subs)
            {
                try
                {
                    var client = _clientFactory.CreateClient();
                    await client.PostAsync(sub.TargetUrl, payload);
                    
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
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var serialized = JsonConvert.SerializeObject(entity, settings);
            var payload = new StringContent(serialized);
            return payload;
        }
    }
}
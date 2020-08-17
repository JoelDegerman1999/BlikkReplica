using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlikkBaiscReplica.Models;
using BlikkBaiscReplica.Repositories;
using BlikkBaiscReplica.Webhooks;
using BlikkBaiscReplica.Webhooks.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlikkBaiscReplica.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class WebhookController : ControllerBase
    {
        private readonly IWebhookRepository _repository;

        public WebhookController(IWebhookRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Subscribe(WebhookSubscription model)
        {
            if (!ModelState.IsValid) return BadRequest();

            model.OwnerId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = await _repository.CreateSubscription(model);

            return CreatedAtAction(nameof(Subscribe), result);
        }

        [HttpGet]
        public async Task<IActionResult> ListWebhooks()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var webHooks = await _repository.ListSubscriptions();
            webHooks = webHooks.Where(q => q.OwnerId == userId).ToList();
            return Ok(webHooks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWebhook(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = await _repository.SearchSubscription(id);
            if (result == null) return NotFound();

            if (result.OwnerId != userId) return Unauthorized();

            return Ok(result);
        }

        //Denna kanske inte ens behövs
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubscription(int id, WebhookSubscription model)
        {
            var webSub = await _repository.SearchSubscription(id);
            if (webSub.OwnerId != User.FindFirst(ClaimTypes.NameIdentifier).Value) return Unauthorized();

            if (model.Id != id) return BadRequest();
            var result = await _repository.UpdateSubscription(model);
            if (result == null) return BadRequest();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Unsubscribe(int id)
        {
            var webSub = await _repository.SearchSubscription(id);
            if (webSub == null) return NotFound();

            if (webSub.OwnerId != User.FindFirst(ClaimTypes.NameIdentifier).Value) return Unauthorized();

            await _repository.DeleteSubscription(webSub);

            return NoContent();
        }
    }
}
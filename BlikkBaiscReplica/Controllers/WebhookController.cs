using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlikkBasicReplica.API.Webhooks.Models;
using BlikkBasicReplica.API.Webhooks.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IWebhookService = BlikkBasicReplica.API.Services.IWebhookService;

namespace BlikkBasicReplica.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class WebhookController : ControllerBase
    {
        private readonly IWebhookRepository _repository;
        private readonly IWebhookService _webhookService;

        public WebhookController(IWebhookRepository repository, IWebhookService webhookService)
        {
            _repository = repository;
            _webhookService = webhookService;
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

        //Denna kanske inte behövs
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
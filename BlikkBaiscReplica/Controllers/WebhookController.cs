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
            if (!ModelState.IsValid) return StatusCode(StatusCodes.Status409Conflict);
            var ownerId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.OwnerId = ownerId;
            var result = await _repository.CreateSubscription(model);
            return CreatedAtAction(nameof(Subscribe), result);
        }
        [HttpGet]
        public async Task<IActionResult> ListWebhooks()
        {
            return Ok(await _repository.ListSubscriptions());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWebhook( int id)
        {
            var result = await _repository.SearchSubscription(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubscription(int id, WebhookSubscription model)
        {
            if (model.Id != id) return BadRequest();
            var result = await _repository.UpdateSubscription(model);
            if (result == null) return BadRequest(); 
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Unsubscribe(int id)
        {
           var result = await _repository.DeleteSubscription(await _repository.SearchSubscription(id));

           if (result == null) return NotFound();
           return NoContent();
        }
    }
}

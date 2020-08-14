using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlikkBaiscReplica.Models;
using BlikkBaiscReplica.Repositories;
using BlikkBaiscReplica.RestHooks;
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
        [HttpGet("{ownerId}")]
        public async Task<IActionResult> GetWebhook( string ownerId)
        {
            var result = await _repository.SearchSubscription(ownerId);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPut("{ownerId}")]
        public async Task<IActionResult> UpdateSubscription(WebhookSubscription model, string ownerId)
        {
            if (model.OwnerId != ownerId) return BadRequest();
            var result = await _repository.UpdateSubscription(model);
            if (result == null) return BadRequest(); 
            return NoContent();
        }

        [HttpDelete("{ownerId}")]
        public async Task<IActionResult> Unsubscribe(string ownerId)
        {
           var result = await _repository.DeleteSubscription(ownerId);

           if (result == null) return NotFound();
           return NoContent();
        }
    }
}

using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlikkBasicReplica.Helpers;
using BlikkBasicReplica.Models;
using BlikkBasicReplica.Repositories;
using BlikkBasicReplica.Webhooks.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlikkBasicReplica.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly OrderRepository _repository;
        private readonly IWebhookService _webhookService;

        public OrdersController(OrderRepository repository, IWebhookService webhookService)
        {
            _repository = repository;
            _webhookService = webhookService;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllOrdersFromUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var contacts = await _repository.GetAll();
            contacts = contacts.Where(q => q.ApplicationUserId == userId).ToList();
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Get(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var order = await _repository.Get(id);
            if (order == null) return NotFound();
            if (order.ApplicationUserId != userId) return Unauthorized();

            return Ok(order);
        }

        [HttpPost]
        [ProducesResponseType(404)]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Add(Order order)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            order.ApplicationUserId = userId;
            var result = await _repository.Add(order);
            if (result == null) return BadRequest();
            await _webhookService.SendHookToSubscribed(WebhookConstants.OrderCreated, result, userId);

            return CreatedAtAction(nameof(Get), new {id = order.Id}, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Update(Order order, int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var entity = await _repository.Get(id);

            if (id != order.Id) return BadRequest();
            if (entity.ApplicationUserId != userId) return Unauthorized();

            var result = await _repository.Update(order);
            if (result == null) return BadRequest();

            await _webhookService.SendHookToSubscribed(WebhookConstants.OrderUpdated, result, userId);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var order = await _repository.Get(id);
            if (order == null) return NotFound();
            if (order.ApplicationUserId != userId) return Unauthorized();
            await _webhookService.SendHookToSubscribed(WebhookConstants.OrderDeleted, order, userId);
            await _repository.Delete(order);
            return NoContent();
        }
    }
}
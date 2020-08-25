using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlikkBasicReplica.API.Models;
using BlikkBasicReplica.API.Repositories;
using BlikkBasicReplica.API.Services;
using BlikkBasicReplica.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlikkBasicReplica.API.Controllers
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

            var orders = await _repository.GetAll();
            orders = orders.Where(q => q.ApplicationUserId == userId).ToList();
            return Ok(orders);
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

            await _webhookService.SendHookToSubscribedHooks(WebhookEventNameConstants.OrderCreated, result, userId);

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

            await _webhookService.SendHookToSubscribedHooks(WebhookEventNameConstants.OrderUpdated, result, userId);
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
            var result = await _repository.Delete(order);
            if (result == null) return Conflict();

            await _webhookService.SendHookToSubscribedHooks(WebhookEventNameConstants.OrderDeleted, order, userId);

            return NoContent();
        }
    }
}
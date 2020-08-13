using System.Threading.Tasks;
using BlikkBaiscReplica.Models;
using BlikkBaiscReplica.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlikkBaiscReplica.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly OrderRepository _repository;

        public OrdersController(OrderRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllOrdersFromUser()
        {
            return Ok(await _repository.GetAll());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Get(int id)
        {
            var order = await _repository.Get(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPost]
        [ProducesResponseType(404)]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Add(Order order)
        {
            var result = await _repository.Add(order);
            if (result == null) return BadRequest();

            return CreatedAtAction(nameof(Get), new {id = order.Id}, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Update(Order order, int id)
        {

            if (id != order.Id) return BadRequest();

            var result = await _repository.Update(order);
            if (result == null) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repository.Delete(id);
            if (result == null) return NotFound();
            return NoContent();
        }
    }
}
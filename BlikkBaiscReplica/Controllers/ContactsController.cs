using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using BlikkBaiscReplica.Models;
using BlikkBaiscReplica.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace BlikkBaiscReplica.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly ContactRepository _repository;

        public ContactsController(ContactRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// List of contacts
        /// </summary>
        /// <returns>List of contacts</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAll()
        {
            var contacts = await _repository.GetAll();
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(int id)
        {
            var contact = await _repository.Get(id);
            if (contact == null) return NotFound();
            return Ok(contact);
        }

        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Add(Contact contact)
        {
            var result = await _repository.Add(contact);
            if (result == null) return BadRequest();

            return CreatedAtAction(nameof(Get), new {id = contact.Id}, contact);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Update(Contact contact, int id)
        {
            if (id != contact.Id) return BadRequest();

            var result = await _repository.Update(contact);

            if (result == null) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repository.Delete(id);
            if (result == null) return NotFound();
            return NoContent();
        }
    }
}
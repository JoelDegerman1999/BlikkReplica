using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlikkBasicReplica.Helpers;
using BlikkBasicReplica.Models;
using BlikkBasicReplica.Repositories;
using BlikkBasicReplica.Webhooks.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlikkBasicReplica.Controllers
{
    [Authorize]
    [ApiController]
    //Kanske routen ska vara /api/[controller]/userId för att vara tydlig att varje ny kontakt
    //är kopplad till en användare
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly ContactRepository _repository;
        private readonly IWebhookService _webhookService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ContactsController(ContactRepository repository, IWebhookService webhookService,
            UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _webhookService = webhookService;
            _userManager = userManager;
        }

        /// <summary>
        /// List of contacts
        /// </summary>
        /// <returns>List of contacts</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAll()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var contacts = await _repository.GetAll();
            contacts = contacts.Where(q => q.ApplicationUserId == userId).ToList();

            return Ok(contacts);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var contact = await _repository.Get(id);

            if (contact == null) return NotFound();
            if (contact.ApplicationUserId != userId) return Unauthorized();
            return Ok(contact);
        }

        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Add(Contact contact)
        {
            if (!ModelState.IsValid) return BadRequest();

            var user = await _userManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            contact.ApplicationUserId = user.Id;
            var result = await _repository.Add(contact);

            //user.Contacts.Add(contact);
            //var succeeded = await _userManager.UpdateAsync(user);

            if (result == null) return BadRequest();

            await _webhookService.SendHookToSubscribed(WebhookConstants.ContactCreated, result, user.Id);
            return CreatedAtAction(nameof(Get), new {id = contact.Id}, contact);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Update(Contact contact, int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var entity = await _repository.Get(id);
            if (entity.ApplicationUserId != userId) return Unauthorized();

            if (id != contact.Id) return BadRequest();

            var result = await _repository.Update(contact);

            if (result == null) return BadRequest();

            await _webhookService.SendHookToSubscribed(WebhookConstants.ContactUpdated, result, userId);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var contact = await _repository.Get(id);

            if (contact == null) return NotFound();
            if (contact.ApplicationUserId != userId) return Unauthorized();

            await _webhookService.SendHookToSubscribed(WebhookConstants.ContactDeleted, contact, userId);
            await _repository.Delete(contact);
            return NoContent();
        }
    }
}
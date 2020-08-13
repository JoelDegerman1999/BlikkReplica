using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlikkBaiscReplica.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlikkBaiscReplica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebhookController : ControllerBase
    {
        [HttpPost("subscribe")]
        public IActionResult Subscribe(WebhookModel model)
        {
            return Ok(model);
        }
    }
}

using System.Security.Claims;
using System.Threading.Tasks;
using BlikkBaiscReplica.Models;
using BlikkBaiscReplica.Models.Auth;
using BlikkBaiscReplica.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlikkBaiscReplica.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController(IUserService service, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var result = await _service.Register(model);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var result = await _service.Login(model);
            return Ok(result);
        }

        [HttpGet("me")]
        public async Task<IActionResult> Users()
        {
            var result = await _userManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);
           return Ok(result);
        }
    }
}
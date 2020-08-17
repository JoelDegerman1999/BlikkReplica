using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BlikkBasicReplica.Models;
using BlikkBasicReplica.Models.Auth;
using BlikkBasicReplica.Models.ViewModels;
using BlikkBasicReplica.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlikkBasicReplica.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public AuthController(IUserService service, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _service = service;
            _userManager = userManager;
            _mapper = mapper;
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
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = await _userManager.Users.Include(q => q.Contacts)
                .Include(q => q.Orders).FirstOrDefaultAsync(q => q.Id == userId);
            var dto = _mapper.Map<ApplicationUserVm>(result);
            return Ok(dto);
        }
    }
}
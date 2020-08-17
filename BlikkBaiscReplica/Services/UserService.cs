using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BlikkBaiscReplica.Models;
using BlikkBaiscReplica.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BlikkBaiscReplica.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public UserService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<UserManagerResponse> Register(RegisterModel model)
        {
            if (model == null) throw new NullReferenceException("Model is null");

            if (model.Password != model.ConfirmPassword)
                return new UserManagerResponse() {Message = "Password does not match"};

            var user = new ApplicationUser()
            {
                Email = model.Email,
                UserName = model.Email,
                Name = model.Name
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return new UserManagerResponse()
                {
                    Message = "User did not create successfully",
                    Success = false
                };
            }

            return new UserManagerResponse()
            {
                Message = "User created successfully",
                Success = true
            };
        }

        public async Task<UserManagerResponse> Login(LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return new UserManagerResponse() {Message = "Email or password is incorrect", Success = false};

            var result = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!result) return new UserManagerResponse() {Message = "Email or password is incorrect", Success = false};

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Secret"]));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id), 
            };

            var token = new JwtSecurityToken(
                _configuration["AuthSettings:Issuer"],
                _configuration["AuthSettings:Audience"],
                claims,
                DateTime.Now,
                DateTime.Now.AddHours(2),
                signingCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserManagerResponse()
            {
                Message = "Logged in successfully",
                UserId = user.Id,
                Token = tokenString,
                Success = true
            };
        }
    }
}
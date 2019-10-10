using Microsoft.AspNetCore.Mvc;
using DatingApp.API.Data;
using System.Threading.Tasks;
using DatingApp.API.Models;
using DatingApp.API.DTOs;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository repo, IConfiguration config){
            _repo = repo;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegister userForRegister)
        {
            userForRegister.UserName = userForRegister.UserName.ToLower();

            if(await _repo.UserExists(userForRegister.UserName))
            return BadRequest("Username already exists");

            var createdUser = new User 
            {
                UserName = userForRegister.UserName
            };

            var thisUser = _repo.RegisterUser(createdUser, userForRegister.Password);

            return StatusCode(201); // status code for CreatedAtRoute(which returns route name, and the obj)
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLogin loginUser)
        {
            var user = await _repo.Login(loginUser.UserName.ToLower(), loginUser.Password);

            if(user == null) return Unauthorized();

            // token will contain two claims one for id and other will be the username.
            var claims = new [] 
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new { token = tokenHandler.WriteToken(token) });
        }
        
    }
}
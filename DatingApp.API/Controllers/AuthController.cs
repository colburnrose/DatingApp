using Microsoft.AspNetCore.Mvc;
using DatingApp.API.Data;
using System.Threading.Tasks;
using DatingApp.API.Models;
using DatingApp.API.DTOs;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo){
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegister userForRegister)
        {
            // validate the request

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
        
    }
}
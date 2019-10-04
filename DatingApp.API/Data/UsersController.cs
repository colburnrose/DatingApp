using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Data
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repo.GetUsers();
        
            var usersDetails = _mapper.Map<IEnumerable<UserDetails>>(users);

            return Ok(usersDetails);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _repo.GetUserById(id);

            var userDetails =  _mapper.Map<UserDetails>(user);

            return Ok(userDetails);
            
        }
    }
}
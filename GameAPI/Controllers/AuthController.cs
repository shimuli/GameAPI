using GameAPI.DTO.Auth;
using GameAPI.Helpers;
using GameAPI.Model;
using GameAPI.Repository.IRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepo _authRepo;

        public AuthController(IAuthRepo authRepo)
        {
            _authRepo = authRepo;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register([FromForm] RegisterDto registerDto)
        {
            var response = await _authRepo.Register
                (
                new User { Username = registerDto.Username }, 
                registerDto.Password
                );
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<int>>> Login([FromForm] LoginDto loginDto)
        {
            var response = await _authRepo.Login
                (
                loginDto.Username,
                loginDto.Password
                );
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

    }
}

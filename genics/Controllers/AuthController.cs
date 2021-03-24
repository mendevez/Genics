using genics.Data;
using genics.Dtos;
using genics.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace genics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserRepository _userRepository;
        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userRepository.RegisterNewUser(model);

                if (result.Success)
                {
                    return Ok(result);
                }
            }

            return BadRequest(new AuthResponse { User = null, Message = "Could not register - invalid details", Success = false });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            if(ModelState.IsValid)
            {
                var result = await _userRepository.LoginUser(model);
                if(result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }

            return BadRequest(new AuthResponse { Message = "Could not login - invalid credentials", Success = false });
        }

    }
}

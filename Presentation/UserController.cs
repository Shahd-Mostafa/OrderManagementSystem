using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.AuthDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    public class UserController:ApiController
    {
        private readonly IAuthenticationService _authenticationService;
        public UserController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var result = await _authenticationService.RegisterAsync(dto);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await _authenticationService.LoginAsync(dto);
            return Ok(result);
        }
    }
}

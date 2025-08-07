using Microsoft.AspNetCore.Mvc;
using Quala.Productos.API.Core.DTOs;
using Quala.Productos.API.Core.Interfaces;

namespace Quala.Productos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDto loginDto)
        {
            // En una aplicación real, se validaria contra una base de datos.
            // Para esta prueba, se usara credenciales quemadas.
            if (loginDto.Username == "quala" && loginDto.Password == "admin")
            {
                var token = _tokenService.GenerateToken(loginDto.Username);
                return Ok(new AuthResponseDto { Token = token });
            }

            return Unauthorized("Credenciales inválidas.");
        }
    }
}

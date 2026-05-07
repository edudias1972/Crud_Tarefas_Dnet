using Microsoft.AspNetCore.Mvc;
using CrudTarefas.DTOs;
using CrudTarefas.Service;

namespace CrudTarefas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public AuthController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var sucesso = await _loginService.RegisterAsync(dto);
            if (!sucesso)
                return BadRequest(new { message = "Email já cadastrado" });

            return Ok(new { message = "Usuário registrado com sucesso" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var response = await _loginService.LoginAsync(dto);
            if (response == null)
                return Unauthorized(new { message = "Credenciais inválidas" });

            return Ok(response); // Agora retorna LoginResponseDto
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] string refreshToken)
        {
            var response = await _loginService.RefreshTokenAsync(refreshToken);
            if (response == null)
                return Unauthorized(new { message = "Refresh token inválido ou expirado" });

            return Ok(response); // Agora retorna RefreshTokenResponseDto
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] string refreshToken)
        {
            var sucesso = await _loginService.LogoutAsync(refreshToken);
            if (!sucesso)
                return BadRequest(new { message = "Falha ao realizar logout" });

            return Ok(new { message = "Logout realizado com sucesso" });
        }
    }
}

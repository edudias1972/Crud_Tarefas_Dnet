using CrudTarefas.Data;
using CrudTarefas.DTOs;
using CrudTarefas.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudTarefas.Service
{
    public class LoginService : ILoginService
    {
        private readonly AppDbContext _context;
        private readonly IJwtService _jwtService;

        public LoginService(AppDbContext context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        public async Task<bool> RegisterAsync(RegisterDto dto)
        {
            var existe = await _context.Usuarios.AnyAsync(u => u.Email == dto.Email);
            if (existe) return false;

            var usuario = new Usuario
            {
                Nome = dto.Nome ?? string.Empty,
                Email = dto.Email ?? string.Empty,
                SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha ?? string.Empty),
                Role = "User"
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginDto dto)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.SenhaHash))
                return null;

            var token = _jwtService.GenerateJwtToken(usuario);

            return new LoginResponseDto
            {
                Token = token,
                Email = usuario.Email,
                Role = usuario.Role
            };
        }

        public async Task<RefreshTokenResponseDto?> RefreshTokenAsync(string refreshToken)
        {
            var token = await _context.RefreshTokens.FirstOrDefaultAsync(r => r.Token == refreshToken);
            if (token == null || token.ExpiraEm < DateTime.UtcNow) return null;

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == token.UsuarioId);
            if (usuario == null) return null;

            var novoToken = _jwtService.GenerateJwtToken(usuario);

            return new RefreshTokenResponseDto
            {
                Token = novoToken,
                ExpiraEm = DateTime.UtcNow.AddHours(1)
            };
        }

        public async Task<bool> LogoutAsync(string refreshToken)
        {
            var token = await _context.RefreshTokens.FirstOrDefaultAsync(r => r.Token == refreshToken);
            if (token == null) return false;

            _context.RefreshTokens.Remove(token);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

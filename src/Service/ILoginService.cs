using CrudTarefas.DTOs;
using CrudTarefas.Models;

namespace CrudTarefas.Service
{
    public interface ILoginService
    {
        Task<bool> RegisterAsync(RegisterDto dto);
        Task<LoginResponseDto?> LoginAsync(LoginDto dto);
        Task<RefreshTokenResponseDto?> RefreshTokenAsync(string refreshToken);
        Task<bool> LogoutAsync(string refreshToken);
    }
}

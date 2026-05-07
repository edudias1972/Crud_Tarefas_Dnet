using CrudTarefas.DTOs;

namespace CrudTarefas.Service
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioResponseDto>> ListarAsync();
        Task<UsuarioResponseDto?> BuscarPorIdAsync(int id);
        Task<UsuarioResponseDto?> AtualizarPerfilAsync(int id, UpdatePerfilDto dto);
        Task<bool> DeletarAsync(int id);
    }
}

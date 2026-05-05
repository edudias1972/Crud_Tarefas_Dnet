using CrudTarefas.DTOs;

namespace CrudTarefas.Service
{
    public interface ITarefaService
    {
        Task<IReadOnlyList<TarefaResponseDto>> ListarPorUsuarioAsync(int usuarioId);
        Task<TarefaResponseDto?> BuscarPorIdAsync(int id, int usuarioId);
        Task<TarefaResponseDto> CriarAsync(TarefaCreateDto dto, int usuarioId);
        Task<bool> AtualizarAsync(int id, TarefaUpdateDto dto, int usuarioId);
        Task<bool> DeletarAsync(int id, int usuarioId);
    }
}




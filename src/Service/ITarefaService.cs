using CrudTarefas.Models;

namespace CrudTarefas.Service
{
    public interface ITarefaService
    {
        Task<IEnumerable<Tarefa>> ListarTodasAsync();
        Task<Tarefa?> BuscarPorIdAsync(int id);
        Task<Tarefa> CriarAsync(Tarefa tarefa);
        Task<bool> AtualizarAsync(Tarefa tarefa);
        Task<bool> DeletarAsync(int id);
    }
}


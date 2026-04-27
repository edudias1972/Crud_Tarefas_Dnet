using CrudTarefas.Data;
using CrudTarefas.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudTarefas.Service
{
    public class TarefaService : ITarefaService
    {
        private readonly AppDbContext _context;

        public TarefaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tarefa>> ListarTodasAsync()
        {
            return await _context.Tarefas.ToListAsync();
        }

        public async Task<Tarefa?> BuscarPorIdAsync(int id)
        {
            return await _context.Tarefas.FindAsync(id);
        }

        public async Task<Tarefa> CriarAsync(Tarefa tarefa)
        {
            _context.Tarefas.Add(tarefa);
            await _context.SaveChangesAsync();
            return tarefa;
        }

        public async Task<bool> AtualizarAsync(Tarefa tarefa)
        {
            var existente = await _context.Tarefas.FindAsync(tarefa.Id);

            if (existente == null)
                return false;

            existente.Titulo = tarefa.Titulo;
            existente.Concluida = tarefa.Concluida;

            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeletarAsync(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null) return false;

            _context.Tarefas.Remove(tarefa);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}


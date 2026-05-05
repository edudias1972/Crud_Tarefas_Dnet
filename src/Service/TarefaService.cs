using CrudTarefas.Data;
using CrudTarefas.DTOs;
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

        public async Task<IReadOnlyList<TarefaResponseDto>> ListarPorUsuarioAsync(int usuarioId)
        {
            return await _context.Tarefas
                .Where(t => t.UsuarioId == usuarioId)
                .Select(t => new TarefaResponseDto
                {
                    Id = t.Id,
                    Titulo = t.Titulo,
                    Descricao = t.Descricao ?? string.Empty,
                    Concluida = t.Concluida,
                    CriadaEm = t.CriadaEm,
                    ConcluidaEm = t.ConcluidaEm
                })
                .ToListAsync();
        }

        public async Task<TarefaResponseDto?> BuscarPorIdAsync(int id, int usuarioId)
        {
            var tarefa = await _context.Tarefas
                .FirstOrDefaultAsync(t => t.Id == id && t.UsuarioId == usuarioId);

            if (tarefa == null) return null;

            return new TarefaResponseDto
            {
                Id = tarefa.Id,
                Titulo = tarefa.Titulo,
                Descricao = tarefa.Descricao ?? string.Empty,
                Concluida = tarefa.Concluida,
                CriadaEm = tarefa.CriadaEm,
                ConcluidaEm = tarefa.ConcluidaEm
            };
        }

        public async Task<TarefaResponseDto> CriarAsync(TarefaCreateDto dto, int usuarioId)
        {
            var tarefa = new Tarefa
            {
                UsuarioId = usuarioId,
                Titulo = dto.Titulo ?? string.Empty,
                Descricao = dto.Descricao ?? string.Empty,
                Concluida = false,
                CriadaEm = DateTime.UtcNow
            };

            _context.Tarefas.Add(tarefa);
            await _context.SaveChangesAsync();

            return new TarefaResponseDto
            {
                Id = tarefa.Id,
                Titulo = tarefa.Titulo,
                Descricao = tarefa.Descricao ?? string.Empty,
                Concluida = tarefa.Concluida,
                CriadaEm = tarefa.CriadaEm,
                ConcluidaEm = tarefa.ConcluidaEm
            };
        }

        public async Task<bool> AtualizarAsync(int id, TarefaUpdateDto dto, int usuarioId)
        {
            var existente = await _context.Tarefas
                .FirstOrDefaultAsync(t => t.Id == id && t.UsuarioId == usuarioId);

            if (existente == null) return false;

            existente.Titulo = dto.Titulo ?? string.Empty;
            existente.Descricao = dto.Descricao ?? string.Empty;
            existente.Concluida = dto.Concluida;
            existente.DataAtualizacao = DateTime.UtcNow;
            existente.ConcluidaEm = dto.Concluida ? DateTime.UtcNow : null;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeletarAsync(int id, int usuarioId)
        {
            var tarefa = await _context.Tarefas
                .FirstOrDefaultAsync(t => t.Id == id && t.UsuarioId == usuarioId);

            if (tarefa == null) return false;

            _context.Tarefas.Remove(tarefa);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}


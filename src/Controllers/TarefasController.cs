using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using CrudTarefas.Service;
using CrudTarefas.DTOs;

namespace CrudTarefas.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TarefasController : ControllerBase
    {
        private readonly ITarefaService _service;

        public TarefasController(ITarefaService service)
        {
            _service = service;
        }

        // Listar tarefas do usuário logado
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var tarefas = await _service.ListarPorUsuarioAsync(usuarioId);

            var retorno = tarefas.Select(t => new TarefaResponseDto
            {
                Id = t.Id,
                Titulo = t.Titulo,
                Descricao = t.Descricao,
                Concluida = t.Concluida,
                CriadaEm = t.CriadaEm,
                ConcluidaEm = t.ConcluidaEm
            });

            return Ok(retorno);
        }

        // Buscar tarefa por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var tarefa = await _service.BuscarPorIdAsync(id, usuarioId);

            if (tarefa == null) return NotFound();

            return Ok(new TarefaResponseDto
            {
                Id = tarefa.Id,
                Titulo = tarefa.Titulo,
                Descricao = tarefa.Descricao,
                Concluida = tarefa.Concluida,
                CriadaEm = tarefa.CriadaEm,
                ConcluidaEm = tarefa.ConcluidaEm
            });
        }

        // Criar nova tarefa
        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] TarefaCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var novaTarefa = await _service.CriarAsync(dto, usuarioId);

            var response = new TarefaResponseDto
            {
                Id = novaTarefa.Id,
                Titulo = novaTarefa.Titulo,
                Descricao = novaTarefa.Descricao,
                Concluida = novaTarefa.Concluida,
                CriadaEm = novaTarefa.CriadaEm,
                ConcluidaEm = novaTarefa.ConcluidaEm
            };

            return CreatedAtAction(nameof(Buscar), new { id = response.Id }, response);
        }

        // Atualizar tarefa
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] TarefaUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var atualizado = await _service.AtualizarAsync(id, dto, usuarioId);

            if (!atualizado) return NotFound();
            return NoContent();
        }

        // Deletar tarefa
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var deletado = await _service.DeletarAsync(id, usuarioId);

            if (!deletado) return NotFound();
            return NoContent();
        }
    }
}



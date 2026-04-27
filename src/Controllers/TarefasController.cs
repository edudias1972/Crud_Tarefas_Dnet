using Microsoft.AspNetCore.Mvc;
using CrudTarefas.Models;
using CrudTarefas.Service;

namespace CrudTarefas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
    {
        private readonly ITarefaService _tarefaService;

        // O ASP.NET injeta o serviço automaticamente aqui 💉
        public TarefasController(ITarefaService tarefaService)
        {
            _tarefaService = tarefaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarefa>>> Get()
        {
            var tarefas = await _tarefaService.ListarTodasAsync();
            return Ok(tarefas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tarefa>> GetPorId(int id)
        {
            var tarefa = await _tarefaService.BuscarPorIdAsync(id);
            if (tarefa == null) return NotFound();
            return Ok(tarefa);
        }

        [HttpPost]
        public async Task<ActionResult<Tarefa>> Post([FromBody] Tarefa tarefa)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var novaTarefa = await _tarefaService.CriarAsync(tarefa);

            return CreatedAtAction(
                nameof(GetPorId),
                new { id = novaTarefa.Id },
                novaTarefa);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Tarefa tarefa)
        {
            if (id != tarefa.Id)
                return BadRequest();

            var sucesso = await _tarefaService.AtualizarAsync(tarefa);

            if (!sucesso)
                return NotFound();

            return Ok("Tarefa atualizada com sucesso.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var sucesso = await _tarefaService.DeletarAsync(id);
            if (!sucesso) return NotFound();
            return Ok("Tarefa removida com sucesso.");
        }
    }
}

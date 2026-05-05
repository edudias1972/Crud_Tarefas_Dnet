using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using CrudTarefas.Service;
using CrudTarefas.DTOs;

namespace CrudTarefas.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/usuarios")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuariosController(IUsuarioService service)
        {
            _service = service;
        }

        // ADMIN - listar todos
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var usuarios = await _service.ListarAsync();
            var retorno = usuarios.Select(x => new UsuarioResponseDto
            {
                Id = x.Id,
                Nome = x.Nome,
                Email = x.Email,
                Role = x.Role
            });
            return Ok(retorno);
        }

        // Usuário logado - perfil
        [HttpGet("me")]
        public async Task<IActionResult> MeuPerfil()
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var usuario = await _service.BuscarPorIdAsync(id);

            if (usuario == null) return NotFound();

            return Ok(new UsuarioResponseDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Role = usuario.Role
            });
        }

        // Usuário logado - atualizar perfil
        [HttpPut("me")]
        public async Task<IActionResult> EditarMeuPerfil([FromBody] UpdatePerfilDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var atualizado = await _service.AtualizarPerfilAsync(id, dto);

            if (atualizado == null) return NotFound();

            return Ok(new UsuarioResponseDto
            {
                Id = atualizado.Id,
                Nome = atualizado.Nome,
                Email = atualizado.Email,
                Role = atualizado.Role
            });
        }

        // ADMIN - deletar usuário
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var removido = await _service.DeletarAsync(id);
            if (!removido) return NotFound();
            return NoContent();
        }
    }
}

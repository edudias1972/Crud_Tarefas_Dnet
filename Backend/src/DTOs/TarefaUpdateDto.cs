using System.ComponentModel.DataAnnotations;

namespace CrudTarefas.DTOs
{
    public class TarefaUpdateDto
    {
        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(100, ErrorMessage = "O título deve ter no máximo 100 caracteres.")]
        public string Titulo { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres.")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "O status de conclusão é obrigatório.")]
        public bool Concluida { get; set; }
    }
}

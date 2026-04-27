using System.ComponentModel.DataAnnotations;

namespace CrudTarefas.Models
{
    public class Tarefa
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Título obrigatório.")]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "Título deve ter entre 3 e 100 caracteres.")]
        public string Titulo { get; set; } = string.Empty;

        public bool Concluida { get; set; } = false;

        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        public DateTime? DataAtualizacao { get; set; }
    }
}
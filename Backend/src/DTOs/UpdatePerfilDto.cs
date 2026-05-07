using System.ComponentModel.DataAnnotations;

namespace CrudTarefas.DTOs
{
    public class UpdatePerfilDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        public string Email { get; set; } = string.Empty;

        // Opcional: permitir alteração de senha
        [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres.")]
        public string? Senha { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace CrudTarefas.DTOs
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).+$",
            ErrorMessage = "A senha deve conter pelo menos uma letra maiúscula, um número e um caractere especial.")]
        public string Senha { get; set; } = string.Empty;
    }
}


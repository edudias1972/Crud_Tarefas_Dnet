using System;
using System.ComponentModel.DataAnnotations;

namespace CrudTarefas.Models
{
    public class RefreshToken
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Token { get; set; } = string.Empty;

        [Required]
        public DateTime ExpiraEm { get; set; }

        public bool Ativo { get; set; } = true;

        [Required]
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;
    }
}

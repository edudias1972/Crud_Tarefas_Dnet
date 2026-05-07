namespace CrudTarefas.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }

        public string Token { get; set; } = "";

        public DateTime ExpiraEm { get; set; }

        public bool Ativo { get; set; } = true;

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;
    }
}
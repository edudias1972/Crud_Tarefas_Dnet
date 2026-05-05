namespace CrudTarefas.Models
{
    public class Tarefa
    {

        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;

        public string? Descricao { get; set; }
        public DateTime CriadaEm { get; set; } = DateTime.UtcNow;
        public DateTime? ConcluidaEm { get; set; }

        public bool Concluida { get; set; } = false;
        public DateTime? DataAtualizacao { get; set; }

        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
    } // garante não-nulo
}

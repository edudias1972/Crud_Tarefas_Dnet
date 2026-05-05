namespace CrudTarefas.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Propriedade para senha criptografada
        public string SenhaHash { get; set; } = string.Empty;

        public string Role { get; set; } = "User";

        public ICollection<Tarefa> Tarefas { get; set; } = new List<Tarefa>();
    }
}


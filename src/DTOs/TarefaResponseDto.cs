namespace CrudTarefas.DTOs
{
    public class TarefaResponseDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public bool Concluida { get; set; }
        public DateTime CriadaEm { get; set; }
        public DateTime? ConcluidaEm { get; set; }
    }
}

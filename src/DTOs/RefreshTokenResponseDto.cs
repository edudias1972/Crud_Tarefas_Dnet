namespace CrudTarefas.DTOs
{
    public class RefreshTokenResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiraEm { get; set; }
    }
}

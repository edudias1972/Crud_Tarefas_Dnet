using System.Text.Json.Serialization;

namespace CrudTarefas.Models;

public class Usuario
{
    public int Id { get; set; }

    public string Nome { get; set; } = "";

    public string Email { get; set; } = "";

    [JsonIgnore]
    public string SenhaHash { get; set; } = "";

    public string Role { get; set; } = "User";
}
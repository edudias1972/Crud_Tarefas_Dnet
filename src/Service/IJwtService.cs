using CrudTarefas.Models;

namespace CrudTarefas.Service
{
    public interface IJwtService
    {
        string GenerateJwtToken(Usuario usuario);
    }
}


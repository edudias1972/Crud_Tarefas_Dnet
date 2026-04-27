using Microsoft.EntityFrameworkCore;
using CrudTarefas.Models;

namespace CrudTarefas.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Tabela Tarefas
        public DbSet<Tarefa> Tarefas { get; set; }

        // Tabela Usuarios
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
    



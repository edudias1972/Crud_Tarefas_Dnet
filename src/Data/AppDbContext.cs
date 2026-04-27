using Microsoft.EntityFrameworkCore;
using CrudTarefas.Models;

namespace CrudTarefas.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Esta linha diz ao EF: "Crie uma tabela chamada 'Tarefas' baseada na classe 'Tarefa'"
        public DbSet<Tarefa> Tarefas { get; set; }
    }
}



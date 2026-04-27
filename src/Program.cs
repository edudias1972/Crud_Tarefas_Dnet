using Microsoft.EntityFrameworkCore;
using CrudTarefas.Data;
using CrudTarefas.Service;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddScoped<ITarefaService, TarefaService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Migration automática
await using (var scope = app.Services.CreateAsyncScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        await context.Database.MigrateAsync();

        Console.WriteLine("✅ Banco atualizado.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Erro migration: {ex.Message}");
    }
}

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();



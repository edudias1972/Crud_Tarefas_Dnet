using Microsoft.EntityFrameworkCore;
using CrudTarefas.Data;
using CrudTarefas.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CrudTarefas.Models;

var senhaHash = BCrypt.Net.BCrypt.HashPassword("senhaDoAdmin");
var admin = new Usuario {
    Email = "admin@teste.com",
    Nome = "Administrador",
    Role = "Admin",
    SenhaHash = "@K&{<&[D3u[$S/*B#x(e,Yhk3RU`}cZ,h/I3U<tD6Dq8ei)&(=@(I/*9b4VDEPep* sXI_Y8>&4!';EP jefQd9tma7\"F"
};


var builder = WebApplication.CreateBuilder(args);

// ---------------- SERVICES ----------------
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ITarefaService, TarefaService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// ---------------- SWAGGER ----------------
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Insira o token JWT no formato: Bearer {seu token}"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// ---------------- CORS ----------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});


// ---------------- JWT ----------------
var jwtKey = builder.Configuration["Jwt:Key"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // em produção, mantenha true
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!))
    };
});

var app = builder.Build();

// ---------------- MIGRATION + SEED ----------------
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();

    var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
    var adminEmail = config["UsuarioAdmin:email"];
    var adminSenha = config["UsuarioAdmin:senha"];

    if (!context.Usuarios.Any(u => u.Email == adminEmail))
    {
        context.Usuarios.Add(new Usuario
        {
            Nome = "Administrador",
            Email = adminEmail!,
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(adminSenha!), // senha com hash
            Role = "Admin"
        });
        context.SaveChanges();
    }
}

// ---------------- MIDDLEWARE ----------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAngular");   // 🔑 CORS aqui

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();



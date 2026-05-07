using CrudTarefas.Data;
using CrudTarefas.Models;
using CrudTarefas.Service;
using CrudTarefas.DTOs;
using Microsoft.EntityFrameworkCore; // ESSA É A CHAVE PARA O UseInMemoryDatabase
using Moq;
using Xunit;

namespace CrudTarefas.Tests;

public class LoginServiceTests
{
    private readonly AppDbContext _context;
    private readonly Mock<IJwtService> _mockJwtService;
    private readonly LoginService _service;

    public LoginServiceTests()
    {
        // Configura um banco em memória
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new AppDbContext(options);
        _mockJwtService = new Mock<IJwtService>();
        _service = new LoginService(_context, _mockJwtService.Object);
    }

    [Fact]
    public async Task LoginAsync_DeveRetornarNulo_QuandoCredenciaisInvalidas()
    {
        // Arrange
        // Note: Se o Email não estiver no banco, o FirstOrDefaultAsync retorna null e o teste passa
        var loginDto = new LoginDto { Email = "teste@email.com", Senha = "senhaErrada" };

        // Act
        var resultado = await _service.LoginAsync(loginDto);

        // Assert
        Assert.Null(resultado);
    }

    [Fact]
    public async Task LoginAsync_DeveRetornarToken_QuandoCredenciaisValidas()
    {
        // Arrange
        var email = "usuario@teste.com";
        var senha = "senhaSegura123";

        // 1. Criar usuário e salvar no banco InMemory
        var usuario = new Usuario
        {
            Email = email,
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(senha),
            Role = "User"
        };
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        // 2. Configurar o Mock do JWT para retornar um token fake
        var tokenFake = "meu-token-jwt-fake";
        _mockJwtService.Setup(x => x.GenerateJwtToken(It.IsAny<Usuario>()))
                       .Returns(tokenFake);

        var loginDto = new LoginDto { Email = email, Senha = senha };

        // Act
        var resultado = await _service.LoginAsync(loginDto);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(tokenFake, resultado.Token);
        Assert.Equal("User", resultado.Role);
    }
    [Fact]
    public async Task LogoutAsync_DeveRetornarVerdadeiro_QuandoTokenExistir()
    {
        // Arrange
        var tokenValue = "token-para-remover";
        var token = new RefreshToken { Token = tokenValue, ExpiraEm = DateTime.UtcNow.AddHours(1) };

        _context.RefreshTokens.Add(token);
        await _context.SaveChangesAsync();

        // Act
        var resultado = await _service.LogoutAsync(tokenValue);

        // Assert
        Assert.True(resultado);

        // Verifica se realmente foi removido do banco
        var tokenNoBanco = await _context.RefreshTokens.FirstOrDefaultAsync(r => r.Token == tokenValue);
        Assert.Null(tokenNoBanco);
    }
    [Fact]
    public async Task LogoutAsync_DeveRetornarFalso_QuandoTokenNaoExistir()
    {
        // Arrange
        var tokenInexistente = "token-que-nao-existe";

        // Act
        var resultado = await _service.LogoutAsync(tokenInexistente);

        // Assert
        Assert.False(resultado);
    }
    [Fact]
    public async Task RefreshTokenAsync_DeveRetornarNulo_QuandoTokenEstiverExpirado()
    {
        // Arrange
        var tokenValue = "token-expirado";
        var token = new RefreshToken
        {
            Token = tokenValue,
            ExpiraEm = DateTime.UtcNow.AddHours(-1) // Data passada (expirado)
        };

        _context.RefreshTokens.Add(token);
        await _context.SaveChangesAsync();

        // Act
        var resultado = await _service.RefreshTokenAsync(tokenValue);

        // Assert
        Assert.Null(resultado);
    }
    [Fact]
    public async Task RefreshTokenAsync_DeveRetornarNovoToken_QuandoTokenValido()
    {
        // 1. Arrange: Criar um usuário e um token válido no banco
        var usuario = new Usuario { Email = "user@test.com", SenhaHash = "...", Role = "User" };
        _context.Usuarios.Add(usuario);
        var tokenVálido = new RefreshToken { Token = "token-ok", ExpiraEm = DateTime.UtcNow.AddHours(1), UsuarioId = usuario.Id };
        _context.RefreshTokens.Add(tokenVálido);
        await _context.SaveChangesAsync();

        var novoToken = "novo-token-jwt";
        _mockJwtService.Setup(x => x.GenerateJwtToken(It.IsAny<Usuario>())).Returns(novoToken);

        // 2. Act
        var resultado = await _service.RefreshTokenAsync("token-ok");

        // 3. Assert
        Assert.NotNull(resultado);
        Assert.Equal(novoToken, resultado.Token);
    }
    //Backend testadp com sucesso, agora é só testar o frontend 
}
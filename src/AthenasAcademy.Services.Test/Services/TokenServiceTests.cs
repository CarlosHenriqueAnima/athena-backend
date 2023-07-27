using AthenasAcademy.Services.Core.Configurations.Enums;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Xunit;

namespace AthenasAcademy.Services.Test.Services
{
    public class TokenServiceTests
    {
        private readonly TokenService _tokenService;
        private readonly Mock<IConfiguration> _configurationMock;

        public TokenServiceTests()
        {
            _configurationMock = new Mock<IConfiguration>();
            _tokenService = new TokenService(_configurationMock.Object);
        }

        [Fact]
        public async Task GerarTokenUsuario_ValidUsuario_ReturnsTokenModel()
        {
            // Arrange
            var usuario = new UsuarioTokenModel
            {
                Usuario = "testuser",
                Perfil = Role.Usuario
            };

            // Act
            var result = await _tokenService.GerarTokenUsuario(usuario);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<TokenModel>(result);
            Assert.NotNull(result.Token);
            Assert.NotNull(result.Validade);
            Assert.Equal("Token OK", result.Menssagem);
        }

        [Fact]
        public async Task GerarTokenRequestClient_ValidConfig_ReturnsToken()
        {
            // Arrange
            _configurationMock.Setup(config => config["LegadoAwsSecretKeyBase"])
                              .Returns("secret");

            // Act
            var result = await _tokenService.GerarTokenRequestClient();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GerarTokenUsuario_ValidUsuario_ReturnsTokenModelWithValidToken()
        {
            // Arrange
            var usuario = new UsuarioTokenModel
            {
                Usuario = "testuser",
                Perfil = Role.Usuario
            };

            // Act
            var result = await _tokenService.GerarTokenUsuario(usuario);

            // Assert
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secret")),
                ValidateAudience = false,
                ValidateIssuer = false
            };

            var claimsPrincipal = tokenHandler.ValidateToken(result.Token, validationParameters, out var validatedToken);
            Assert.True(validatedToken.ValidTo > DateTime.UtcNow);
            Assert.Contains(claimsPrincipal.Claims, c => c.Type == ClaimTypes.Name && c.Value == usuario.Usuario);
            Assert.Contains(claimsPrincipal.Claims, c => c.Type == ClaimTypes.Role && c.Value == nameof(Role.Usuario));
        }
    }
}

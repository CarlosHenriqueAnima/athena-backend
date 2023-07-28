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
        public async Task GerarTokenUsuario_UsuarioValido_RetornaTokenModel()
        {
            // Arrange
            var usuario = new UsuarioTokenModel
            {
                Usuario = "testuser",
                Perfil = (int)Role.Usuario
            };

            _configurationMock.Setup(config => config["TokenConfiguration:ExpireHours"]).Returns("24");

            _configurationMock.Setup(config => config["JwtKeyBase"]).Returns(Convert.ToBase64String(new byte[32]));

            // Act
            var result = await _tokenService.GerarTokenUsuario(usuario);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<TokenModel>(result);
            Assert.NotNull(result.Token);
            Assert.Equal("Token OK", result.Menssagem);
        }

        [Fact]
        public async Task GerarTokenRequestClient_ConfiguracaoValida_RetornaToken()
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
        public async Task GerarTokenUsuario_UsuarioValido_RetornaTokenModelComTokenValido()
        {
            // Arrange
            var usuario = new UsuarioTokenModel
            {
                Usuario = "testuser",
                Perfil = (int)Role.Usuario
            };

            var random = new Random();
            var keyBytes = new byte[32];
            random.NextBytes(keyBytes);
            string jwtKey = Convert.ToBase64String(keyBytes);
            string kid = Guid.NewGuid().ToString();

            _configurationMock.Setup(config => config["TokenConfiguration:ExpireHours"]).Returns("24");
            _configurationMock.Setup(config => config["JwtKeyBase"]).Returns(jwtKey);

            // Act
            var result = await _tokenService.GerarTokenUsuario(usuario);

            // Assert
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                ValidateAudience = false,
                ValidateIssuer = false
            };

            var claimsPrincipal = tokenHandler.ValidateToken(result.Token, validationParameters, out var validatedToken);
            Assert.True(validatedToken.ValidTo > DateTime.UtcNow);
        }
    }
}

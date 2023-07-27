using AthenasAcademy.Services.Core.Arguments;
using AthenasAcademy.Services.Core.Configurations.Mappers;
using AthenasAcademy.Services.Core.Exceptions;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Repositories.Interfaces;
using AthenasAcademy.Services.Core.Services;
using AthenasAcademy.Services.Core.Services.Interfaces;
using AthenasAcademy.Services.Domain.Requests;
using AthenasAcademy.Services.Domain.Responses;
using AthenasAcademy.Services.Test.Factory;
using Moq;
using Xunit;

namespace AthenasAcademy.Services.Test.Services
{
    public class AutorizaUsuarioServiceTests
    {
        private readonly AutorizaUsuarioService _autorizaUsuarioService;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;
        private readonly Mock<IObjectConverter> _mapperMock;

        private readonly AutorizacaoFactory _autorizacaoFactory;

        public AutorizaUsuarioServiceTests()
        {
            _tokenServiceMock = new Mock<ITokenService>();
            _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            _mapperMock = new Mock<IObjectConverter>();
            _autorizaUsuarioService = new AutorizaUsuarioService(_tokenServiceMock.Object, _usuarioRepositoryMock.Object, _mapperMock.Object);
            _autorizacaoFactory = new AutorizacaoFactory();
        }

        [Fact]
        public async Task CadastrarUsuario_SolicitacaoValida_RetornaNovoUsuarioResponse()
        {
            // Arrange
            var novoUsuarioRequest = _autorizacaoFactory.ObterNovoUsuarioRequestValido();
            var novoUsuarioArgument = _autorizacaoFactory.RetornarNovoUsuarioArgumentValido();
            var expectedUsuarioModel = _autorizacaoFactory.ObterUsuarioModelValido();
            var expectedTokenModel = _autorizacaoFactory.ValidarToken();
            var expectedNovoUsuarioResponse = _autorizacaoFactory.ObterNovoUsuarioValido();
            _mapperMock.Setup(mapper => mapper.Map<NovoUsuarioArgument>(It.IsAny<NovoUsuarioRequest>()))
                       .Returns(novoUsuarioArgument);
            _mapperMock.Setup(mapper => mapper.Map<TokenResponse>(It.IsAny<TokenModel>()))
                       .Returns(_autorizacaoFactory.RetornarTokenValido());
            _usuarioRepositoryMock.Setup(repo => repo.CadastrarUsuario(novoUsuarioArgument))
                                  .ReturnsAsync(expectedUsuarioModel);
            _tokenServiceMock.Setup(service => service.GerarTokenUsuario(It.IsAny<UsuarioTokenModel>()))
                             .ReturnsAsync(expectedTokenModel);

            // Act
            var result = await _autorizaUsuarioService.CadastrarUsuario(novoUsuarioRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUsuarioModel.Usuario, result.Usuario);
            Assert.NotNull(result.Token);
        }

        [Fact]
        public async Task CadastrarUsuario_UsuarioJaExiste_RetornaAPICustomException()
        {
            // Arrange
            var novoUsuarioRequest = _autorizacaoFactory.ObterNovoUsuarioRequestValido();
            var novoUsuarioArgument = _autorizacaoFactory.RetornarNovoUsuarioArgumentValido();
            var existingUsuarioModel = _autorizacaoFactory.ObterUsuarioModelValido();
            _mapperMock.Setup(mapper => mapper.Map<NovoUsuarioArgument>(It.IsAny<NovoUsuarioRequest>()))
                       .Returns(novoUsuarioArgument);
            _usuarioRepositoryMock.Setup(repo => repo.BuscarUsuario(It.IsAny<UsuarioArgument>()))
                                  .ReturnsAsync(existingUsuarioModel);

            // Act & Assert
            await Assert.ThrowsAsync<APICustomException>(() => _autorizaUsuarioService.CadastrarUsuario(novoUsuarioRequest));
        }

        [Fact]
        public async Task CadastrarUsuario_ExcecaoDeRepositorio_RetornaAPICustomException()
        {
            // Arrange
            var novoUsuarioRequest = _autorizacaoFactory.ObterNovoUsuarioRequestValido();
            _mapperMock.Setup(mapper => mapper.Map<NovoUsuarioArgument>(It.IsAny<NovoUsuarioRequest>()))
                       .Returns(_autorizacaoFactory.RetornarNovoUsuarioArgumentValido());
            _usuarioRepositoryMock.Setup(repo => repo.CadastrarUsuario(It.IsAny<NovoUsuarioArgument>()))
                                  .ThrowsAsync(new Exception("Usuário já existe"));

            // Act & Assert
            await Assert.ThrowsAsync<APICustomException>(() => _autorizaUsuarioService.CadastrarUsuario(novoUsuarioRequest));
        }

        [Fact]
        public async Task LoginUsuario_SolicitacaoValida_RetornaLoginUsuarioResponse()
        {
            // Arrange
            var loginUsuarioRequest = _autorizacaoFactory.ObterLoginUsuarioRequestValido();
            var existingUsuarioModel = _autorizacaoFactory.ObterUsuarioModelValido();
            var expectedTokenModel = _autorizacaoFactory.ValidarToken();
            var expectedLoginUsuarioResponse = _autorizacaoFactory.RetornarLoginUsuarioResponseValido();
            _usuarioRepositoryMock.Setup(repo => repo.BuscarUsuario(It.IsAny<UsuarioArgument>()))
                                  .ReturnsAsync(existingUsuarioModel);
            _tokenServiceMock.Setup(service => service.GerarTokenUsuario(It.IsAny<UsuarioTokenModel>()))
                             .ReturnsAsync(expectedTokenModel);

            // Act
            var result = await _autorizaUsuarioService.LoginUsuario(loginUsuarioRequest);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Resultado);
            Assert.NotNull(result.Token);
        }

        [Fact]
        public async Task LoginUsuario_UsuarioInvalido_RetornaAPICustomException()
        {
            // Arrange
            var loginUsuarioRequest = _autorizacaoFactory.ObterLoginUsuarioRequestValido();
            _usuarioRepositoryMock.Setup(repo => repo.BuscarUsuario(It.IsAny<UsuarioArgument>()))
                                  .ReturnsAsync((UsuarioModel)null);

            // Act & Assert
            await Assert.ThrowsAsync<APICustomException>(() => _autorizaUsuarioService.LoginUsuario(loginUsuarioRequest));
        }

        [Fact]
        public async Task LoginUsuario_SenhaInvalida_RetornaAPICustomException()
        {
            // Arrange
            var loginUsuarioRequest = _autorizacaoFactory.ObterLoginUsuarioRequestValido();
            var existingUsuarioModel = new UsuarioModel { Senha = "@5enhA_1nvalidA#" };
            _usuarioRepositoryMock.Setup(repo => repo.BuscarUsuario(It.IsAny<UsuarioArgument>()))
                                  .ReturnsAsync(existingUsuarioModel);

            // Act & Assert
            await Assert.ThrowsAsync<APICustomException>(() => _autorizaUsuarioService.LoginUsuario(loginUsuarioRequest));
        }

        [Fact]
        public async Task LoginUsuario_ExcecaoDeRepositorio_RetornaAPICustomException()
        {
            // Arrange
            var loginUsuarioRequest = _autorizacaoFactory.ObterLoginUsuarioRequestValido();
            _usuarioRepositoryMock.Setup(repo => repo.BuscarUsuario(It.IsAny<UsuarioArgument>()))
                                  .ThrowsAsync(new Exception("Login não permitido"));

            // Act & Assert
            await Assert.ThrowsAsync<APICustomException>(() => _autorizaUsuarioService.LoginUsuario(loginUsuarioRequest));
        }

        [Fact]
        public async Task ObterUsuarios_SolicitacaoValida_RetornaListaUsuarioResponse()
        {
            // Arrange
            var expectedUsuario = _autorizacaoFactory.ObterUsuarioModelValido();
            _usuarioRepositoryMock.Setup(repo => repo.BuscarUsuario(_autorizacaoFactory.RetornarUsuarioArgumentValido()))
                                  .ReturnsAsync(expectedUsuario);

            // Act
            var result = await _autorizaUsuarioService.ObterUsuario("email@dominio.com", false);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ObterUsuarios_SemResultados_RetornaListaVazia()
        {
            // Arrange
            _usuarioRepositoryMock.Setup(repo => repo.BuscarUsuario(_autorizacaoFactory.RetornarUsuarioArgumentValido()))
                                  .ReturnsAsync(new UsuarioModel());

            // Act
            var result = await _autorizaUsuarioService.ObterUsuario("email@dominio.com", false);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ObterUsuarios_ExcecaoDeRepositorio_RetornaAPICustomException()
        {
            // Arrange
            _usuarioRepositoryMock.Setup(repo => repo.BuscarUsuario(_autorizacaoFactory.RetornarUsuarioArgumentValido()))
                                  .ThrowsAsync(new Exception("Nenhum usuario encontrado"));

            // Act & Assert
            await Assert.ThrowsAsync<APICustomException>(() => _autorizaUsuarioService.ObterUsuario("email@dominio.com", true));
        }

        [Fact]
        public async Task ObterUsuario_UsuarioValido_RetornaUsuarioModel()
        {
            // Arrange
            var usuario = "usuario.login";
            var expectedUsuarioModel = _autorizacaoFactory.ObterUsuarioModelValido();
            _usuarioRepositoryMock.Setup(repo => repo.BuscarUsuario(It.IsAny<UsuarioArgument>()))
                                  .ReturnsAsync(expectedUsuarioModel);

            // Act
            var result = await _autorizaUsuarioService.ObterUsuario(usuario, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUsuarioModel, result);
        }

        [Fact]
        public async Task ObterUsuario_UsuarioInvalido_RetornaAPICustomException()
        {
            // Arrange
            var usuario = "usuario.invalido";
            _usuarioRepositoryMock.Setup(repo => repo.BuscarUsuario(It.IsAny<UsuarioArgument>()))
                                  .ReturnsAsync((UsuarioModel)null);

            // Act & Assert
            await Assert.ThrowsAsync<APICustomException>(() => _autorizaUsuarioService.ObterUsuario(usuario, true));
        }

        [Fact]
        public async Task ObterUsuario_ExcecaoDeRepositorio_RetornaAPICustomException()
        {
            // Arrange
            var usuario = "usuario.invalido";
            _usuarioRepositoryMock.Setup(repo => repo.BuscarUsuario(It.IsAny<UsuarioArgument>()))
                                  .ThrowsAsync(new Exception("Usuario não encontrado"));

            // Act & Assert
            await Assert.ThrowsAsync<APICustomException>(() => _autorizaUsuarioService.ObterUsuario(usuario, true));
        }
    }
}

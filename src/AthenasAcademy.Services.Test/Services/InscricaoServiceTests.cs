using AthenasAcademy.Services.Core.Arguments;
using AthenasAcademy.Services.Core.Exceptions;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Repositories.Interfaces;
using AthenasAcademy.Services.Core.Services;
using AthenasAcademy.Services.Core.Services.Interfaces;
using AthenasAcademy.Services.Domain.Requests;
using AthenasAcademy.Services.Domain.Responses;
using Moq;
using Xunit;

namespace AthenasAcademy.Services.Test.Services
{
    public class InscricaoServiceTests
    {
        private readonly InscricaoService _inscricaoService;
        private readonly Mock<IInscricaoRepository> _inscricaoRepositoryMock;
        private readonly Mock<IAutorizaUsuarioService> _usuarioServiceMock;
        private readonly Mock<ICursoService> _cursoServiceMock;
        private readonly Mock<IAlunoService> _alunoServiceMock;

        public InscricaoServiceTests()
        {
            _inscricaoRepositoryMock = new Mock<IInscricaoRepository>();
            _usuarioServiceMock = new Mock<IAutorizaUsuarioService>();
            _cursoServiceMock = new Mock<ICursoService>();
            _alunoServiceMock = new Mock<IAlunoService>();

            _inscricaoService = new InscricaoService(
                _inscricaoRepositoryMock.Object,
                _usuarioServiceMock.Object,
                _cursoServiceMock.Object,
                _alunoServiceMock.Object
            );
        }

        [Fact]
        public async Task CadastrarCandidato_ValidRequest_ReturnsInscricaoCandidatoResponse()
        {
            // Arrange
            var request = new NovaInscricaoCandidatoRequest
            {
            };

            var usuario = new UsuarioModel
            {
                Id = 1,
            };
            _usuarioServiceMock.Setup(service => service.ObterUsuario(request.Email.Trim().ToLower()))
                               .ReturnsAsync(usuario);

            var opcaoCurso = new CursoResponse
            {
                Id = 1,
            };
            _cursoServiceMock.Setup(service => service.ObterCurso(request.Curso.CodigoCurso))
                             .ReturnsAsync(opcaoCurso);

            var inscricaoCandidato = new InscricaoCandidatoModel
            {
                CodigoInscricao = 1001,
            };
            _inscricaoRepositoryMock.Setup(repo => repo.RegistrarNovaInscricao(It.IsAny<InscricaoCandidatoArgument>()))
                                    .ReturnsAsync(inscricaoCandidato);

            // Act
            var result = await _inscricaoService.CadastrarCandidato(request);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<InscricaoCandidatoResponse>(result);
            Assert.Equal(inscricaoCandidato.CodigoInscricao, result.Inscricao);
        }

        [Fact]
        public async Task CadastrarCandidato_UserDoesNotExist_ThrowsAPICustomException()
        {
            // Arrange
            var request = new NovaInscricaoCandidatoRequest
            {
            };

            _usuarioServiceMock.Setup(service => service.ObterUsuario(request.Email.Trim().ToLower()))
                               .ReturnsAsync((UsuarioModel)null);

            // Act and Assert
            await Assert.ThrowsAsync<APICustomException>(() => _inscricaoService.CadastrarCandidato(request));
        }

        [Fact]
        public async Task CadastrarCandidato_UserIsInactive_ThrowsAPICustomException()
        {
            // Arrange
            var request = new NovaInscricaoCandidatoRequest
            {
            };

            var usuario = new UsuarioModel
            {
                Id = 1,
                Ativo = false
            };
            _usuarioServiceMock.Setup(service => service.ObterUsuario(request.Email.Trim().ToLower()))
                               .ReturnsAsync(usuario);

            // Act and Assert
            await Assert.ThrowsAsync<APICustomException>(() => _inscricaoService.CadastrarCandidato(request));
        }
    }
}

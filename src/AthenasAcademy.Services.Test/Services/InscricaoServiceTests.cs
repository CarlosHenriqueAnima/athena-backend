using AthenasAcademy.Services.Core.Arguments;
using AthenasAcademy.Services.Core.CrossCutting;
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
    public class InscricaoServiceTests
    {
        private readonly InscricaoService _inscricaoService;
        private readonly Mock<IInscricaoRepository> _inscricaoRepositoryMock;
        private readonly Mock<IAutorizaUsuarioService> _usuarioServiceMock;
        private readonly Mock<ICursoService> _cursoServiceMock;
        private readonly Mock<IAlunoService> _alunoServiceMock;
        private readonly Mock<IMatriculaService> _matriculaServiceMock;
        
        private readonly InscricaoFactory _inscricaoFactory;
        private readonly AutorizacaoFactory _autorizacaoFactory;
        private readonly CursoFactory _cursoFactory;
        private readonly AlunoFactory _alunoFactory;

        public InscricaoServiceTests()
        {
            _inscricaoRepositoryMock = new Mock<IInscricaoRepository>();
            _usuarioServiceMock = new Mock<IAutorizaUsuarioService>();
            _cursoServiceMock = new Mock<ICursoService>();
            _alunoServiceMock = new Mock<IAlunoService>();
            _matriculaServiceMock = new Mock<IMatriculaService>();

            _inscricaoService = new InscricaoService(
                _inscricaoRepositoryMock.Object,
                _usuarioServiceMock.Object,
                _cursoServiceMock.Object,
                _alunoServiceMock.Object,
                _matriculaServiceMock.Object
            );

            _inscricaoFactory = new InscricaoFactory();
            _autorizacaoFactory = new AutorizacaoFactory();
            _cursoFactory = new CursoFactory();
            _alunoFactory = new AlunoFactory();
        }

        [Fact]
        public async Task CadastrarCandidato_SolicitacaoValida_RetornaInscricaoCandidatoResponse()
        {
            // Arrange
            var request = _inscricaoFactory.ObterNovaInscricaoCandidatoRequestValido();

            var usuario = _autorizacaoFactory.ObterUsuarioModelValido();
            _usuarioServiceMock.Setup(service => service.ObterUsuario(request.Email.Trim().ToLower(), false))
                               .ReturnsAsync(usuario);

            var opcaoCurso = _cursoFactory.ObterCursoResponseValido();
            _cursoServiceMock.Setup(service => service.ObterCurso(request.Curso.CodigoCurso))
                             .ReturnsAsync(opcaoCurso);

            var inscricaoCandidato = _inscricaoFactory.ObterInscricaoCandidatoModelValido();
            _inscricaoRepositoryMock.Setup(repo => repo.RegistrarNovaInscricao(It.IsAny<InscricaoCandidatoArgument>()))
                                    .ReturnsAsync(inscricaoCandidato);

            var fichaAluno = _alunoFactory.ObterFichaAlunoValida();
            _alunoServiceMock.Setup(service => service.CadastrarAluno(It.IsAny<NovoAlunoArgument>()))
                             .ReturnsAsync(fichaAluno.Aluno);
            _alunoServiceMock.Setup(service => service.CadastrarEnderecoAluno(It.IsAny<NovoEnderecoAlunoArgument>()))
                             .ReturnsAsync(fichaAluno.Endereco);
            _alunoServiceMock.Setup(service => service.CadastrarTelefoneAluno(It.IsAny<NovoTelefoneAlunoArgument>()))
                             .ReturnsAsync(fichaAluno.Telefone);
            _alunoServiceMock.Setup(service => service.CadastrarDetalheAluno(It.IsAny<NovoDetalheAlunoArgument>()))
                             .ReturnsAsync(fichaAluno.DetalhesFicha);

            // Act
            var result = await _inscricaoService.CadastrarCandidato(request);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<InscricaoCandidatoResponse>(result);
            Assert.Equal(inscricaoCandidato.CodigoInscricao, result.Inscricao);
        }

        [Fact]
        public async Task CadastrarCandidato_UsuarioInvalido_RetornaAPICustomException()
        {
            // Arrange
            var request = _inscricaoFactory.ObterNovaInscricaoCandidatoRequestValido();

            _usuarioServiceMock.Setup(service => service.ObterUsuario(request.Email.Trim().ToLower(), false))
                               .ReturnsAsync((UsuarioModel)null);

            // Act & Assert
            await Assert.ThrowsAsync<APICustomException>(() => _inscricaoService.CadastrarCandidato(request));
        }

        [Fact]
        public async Task CadastrarCandidato_UsuarioInativo_RetornaAPICustomException()
        {
            // Arrange
            var request = _inscricaoFactory.ObterNovaInscricaoCandidatoRequestValido();

            var usuario = _autorizacaoFactory.ObterUsuarioModelValido();
            _usuarioServiceMock.Setup(service => service.ObterUsuario(request.Email.Trim().ToLower(), false))
                               .ReturnsAsync(usuario);

            // Act & Assert
            await Assert.ThrowsAsync<APICustomException>(() => _inscricaoService.CadastrarCandidato(request));
        }
    }
}

using AthenasAcademy.Services.Core.Arguments;
using AthenasAcademy.Services.Core.Configurations.Mappers;
using AthenasAcademy.Services.Core.CrossCutting;
using AthenasAcademy.Services.Core.Exceptions;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Repositories.Interfaces;
using AthenasAcademy.Services.Core.Services;
using AthenasAcademy.Services.Core.Services.Interfaces;
using AthenasAcademy.Services.Domain.Configurations.Enums;
using AthenasAcademy.Services.Domain.Requests;
using AthenasAcademy.Services.Domain.Responses;
using AthenasAcademy.Services.Test.Factory;
using Moq;
using System.Net;
using Xunit;

namespace AthenasAcademy.Services.Test.Services
{
    public class CursoServiceTests
    {
        private readonly Mock<ICursoRepository> _cursoRepositoryMock;
        private readonly ICursoService _cursoService;

        private readonly CursoFactory _cursoFactory;
        private readonly DisciplinaFactory _disciplinaFactory;
        private readonly AreaConhecimentoFactory _areaConhecimentoFactory;

        public CursoServiceTests()
        {
            _cursoRepositoryMock = new Mock<ICursoRepository>();
            var mapper = new ObjectConverter();
            _cursoService = new CursoService(_cursoRepositoryMock.Object, mapper);

            _cursoFactory = new CursoFactory();
            _disciplinaFactory = new DisciplinaFactory();
            _areaConhecimentoFactory = new AreaConhecimentoFactory();
        }

        #region Curso
        [Fact]
        public async Task ObterCurso_IdValido_RetornaCursoResponse()
        {
            // Arrange
            var id = 1;
            var expectedCurso = _cursoFactory.ObterCursoModelValido();
            _cursoRepositoryMock.Setup(repo => repo.ObterCurso(id))
                                .ReturnsAsync(expectedCurso);

            // Act
            var result = await _cursoService.ObterCurso(id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CursoResponse>(result);
        }

        [Fact]
        public async Task ObterCurso_IdInvalido_RetornaAPICustomException()
        {
            // Arrange
            var id = 1;
            _cursoRepositoryMock.Setup(repo => repo.ObterCurso(id))
                                .ReturnsAsync((CursoModel)null);

            // Act & Assert
            await Assert.ThrowsAsync<APICustomException>(() => _cursoService.ObterCurso(id));
        }

        [Fact]
        public async Task ObterCursos_CursosExistentes_RetornaListaDeCursoResponse()
        {
            // Arrange
            var cursos = _cursoFactory.ObterListaDeCursos();
            var disciplinas = _disciplinaFactory.ObterListaDisciplinaModelValidos();
            var areaConhecimento = _areaConhecimentoFactory.ObterListaAreaConhecimentoModelValidos();
            _cursoRepositoryMock.Setup(repo => repo.ObterCursos())
                                .ReturnsAsync(cursos);
            _cursoRepositoryMock.Setup(repo => repo.ObterDisciplinas())
                                .ReturnsAsync(disciplinas);
            _cursoRepositoryMock.Setup(repo => repo.ObterAreasConhecimento())
                                .ReturnsAsync(areaConhecimento);

            // Act
            var result = await _cursoService.ObterCursos();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<CursoResponse>>(result);
        }

        [Fact]
        public async Task ObterCursos_SemCurso_RetornaAPICustomException()
        {
            // Arrange
            _cursoRepositoryMock.Setup(repo => repo.ObterCursos())
                                .ReturnsAsync(new List<CursoModel>());

            // Act & Assert
            await Assert.ThrowsAsync<APICustomException>(() => _cursoService.ObterCursos());
        }

        // CadastrarCurso
        [Fact]
        public async Task CadastrarCurso_SolicitacaoValida_RetornaNovoCursoResponse()
        {
            // Arrange
            var request = _cursoFactory.ObterNovoCursoRequestValido();
            var areaConhecimento = _areaConhecimentoFactory.ObterAreaConhecimentoValida();
            _cursoRepositoryMock.Setup(repo => repo.ObterAreaConhecimento(request.IdAreaConhecimento))
                                .ReturnsAsync(areaConhecimento);
            _cursoRepositoryMock.Setup(repo => repo.CadastrarCurso(It.IsAny<CursoArgument>()))
                                .ReturnsAsync(_cursoFactory.ObterCursoModelValido());

            // Act
            var result = await _cursoService.CadastrarCurso(request);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NovoCursoResponse>(result);
        }

        [Fact]
        public async Task CadastrarCurso_CursoInvalido_RetornaAPICustomException()
        {
            // Arrange
            var request = _cursoFactory.ObterNovoCursoRequestValido();
            _cursoRepositoryMock.Setup(repo => repo.ObterAreaConhecimento(request.IdAreaConhecimento))
                                .ReturnsAsync((AreaConhecimentoModel)null);

            // Act & Assert
            await Assert.ThrowsAsync<APICustomException>(() => _cursoService.CadastrarCurso(request));
        }

        [Fact]
        public async Task AtualizarCurso_SolicitacaoValida_RetornaCursoResponse()
        {
            // Arrange
            var request = _cursoFactory.ObterCursoRequestValido(
                                                                _disciplinaFactory.ObterListaDisciplinaRequestValidas(),
                                                                _areaConhecimentoFactory.ObterAreaConhecimentoRequestValida()
                                                               );
            var curso = _cursoFactory.ObterCursoModelValido();
            var disciplinas = _disciplinaFactory.ObterListaDisciplinaModelValidos();
            var areaConhecimento = _areaConhecimentoFactory.ObterAreaConhecimentoValida();
            _cursoRepositoryMock.Setup(repo => repo.ObterCurso(request.Id))
                                .ReturnsAsync(curso);
            _cursoRepositoryMock.Setup(repo => repo.ObterDisciplinas())
                                .ReturnsAsync(disciplinas);
            _cursoRepositoryMock.Setup(repo => repo.ObterDisciplinasDoCurso(request.Id))
                                .ReturnsAsync(disciplinas);
            _cursoRepositoryMock.Setup(repo => repo.ObterAreaConhecimento(request.Id))
                                .ReturnsAsync(areaConhecimento);
            _cursoRepositoryMock.Setup(repo => repo.AtualizarCurso(It.IsAny<CursoArgument>()))
                                .ReturnsAsync(_cursoFactory.ObterCursoModelValido());

            // Act
            var result = await _cursoService.AtualizarCurso(request);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CursoResponse>(result);
        }

        [Fact]
        public async Task AtualizarCurso_SolicitacaoInvalida_RetornaAPICustomException()
        {
            // Arrange
            var request = _cursoFactory.ObterCursoRequestValido(
                                                                _disciplinaFactory.ObterListaDisciplinaRequestValidas(),
                                                                _areaConhecimentoFactory.ObterAreaConhecimentoRequestValida()
                                                               );
            _cursoRepositoryMock.Setup(repo => repo.ObterCurso(request.Id))
                                .ReturnsAsync((CursoModel)null);

            // Act & Assert
            await Assert.ThrowsAsync<APICustomException>(() => _cursoService.AtualizarCurso(request));
        }

        [Fact]
        public async Task DesativarCurso_IdValido_RetornaTrue()
        {
            // Arrange
            var id = 1;
            _cursoRepositoryMock.Setup(repo => repo.ObterCurso(id))
                                .ReturnsAsync(_cursoFactory.ObterCursoModelValido());
            _cursoRepositoryMock.Setup(repo => repo.DesativarCurso(id))
                                .ReturnsAsync(true);

            // Act
            var result = await _cursoService.DesativarCurso(id);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DesativarCurso_IdInvalido_RetornaAPICustomException()
        {
            // Arrange
            var id = 999999;
            _cursoRepositoryMock.Setup(repo => repo.ObterCurso(id))
                                .ReturnsAsync((CursoModel)null);

            // Act & Assert
            await Assert.ThrowsAsync<APICustomException>(() => _cursoService.DesativarCurso(id));
        }
        #endregion

        #region Disciplina
        [Fact]
        public async Task ObterDisciplina_IdInvalido_RetornaException()
        {
            // Arrange
            var id = 1;
            _cursoRepositoryMock.Setup(repo => repo.ObterDisciplina(id))
                                .ThrowsAsync(new APICustomException(string.Format("Nenhuma disciplina localizada para o id {0}. Por favor, revise os detalhes da requisição.", id), ExceptionResponseType.Warning, HttpStatusCode.NotFound));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<APICustomException>(() => _cursoService.ObterDisciplina(id));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
            Assert.Equal(ExceptionResponseType.Warning, exception.ResponseType);
        }

        [Fact]
        public async Task ObterDisciplinas_ListaVazia_RetornaAPICustomException()
        {
            // Arrange
            _cursoRepositoryMock.Setup(repo => repo.ObterDisciplinas())
                                .ReturnsAsync(new List<DisciplinaModel>());

            // Act & Assert
            await Assert.ThrowsAsync<APICustomException>(() => _cursoService.ObterDisciplinas());
        }

        [Fact]
        public async Task CadastrarDisciplina_CursoInvalido_RetornaAPICustomException()
        {
            // Arrange
            var request = _disciplinaFactory.ObterNovaDisciplinaRequestValido();
            _cursoRepositoryMock.Setup(repo => repo.ObterCurso(request.IdCurso))
                                .ReturnsAsync((CursoModel)null);

            // Act & Assert
            await Assert.ThrowsAsync<APICustomException>(() => _cursoService.CadastrarDisciplina(request));
        }

        [Fact]
        public async Task AtualizarDisciplina_IdInvalido_RetornaException()
        {
            // Arrange
            var request = _disciplinaFactory.ObterDisciplinaRequestValido();
            var disciplina = _disciplinaFactory.ObterDisciplinaModelValido();
            _cursoRepositoryMock.Setup(repo => repo.ObterDisciplina(request.Id))
                                .ThrowsAsync(new Exception("Deu erro aqui"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _cursoService.AtualizarDisciplina(request));
        }

        [Fact]
        public async Task DesativarDisciplina_IdInvalido_RetornaAPICustomException()
        {
            // Arrange
            var id = 1;
            _cursoRepositoryMock.Setup(repo => repo.ObterDisciplina(id))
                                .ReturnsAsync((DisciplinaModel)null);

            // Act & Assert
            await Assert.ThrowsAsync<APICustomException>(() => _cursoService.DesativarDisciplina(id));
        }

        [Fact]
        public async Task DesativarDisciplina_JaDesativada_RetornaFalse()
        {
            // Arrange
            var id = 1;
            var request = _cursoFactory.ObterCursoRequestValido(
                                                                _disciplinaFactory.ObterListaDisciplinaRequestValidas(),
                                                                _areaConhecimentoFactory.ObterAreaConhecimentoRequestValida()
                                                               );
            var disciplinas = _disciplinaFactory.ObterListaDisciplinaModelValidos();
            _cursoRepositoryMock.Setup(repo => repo.ObterDisciplinas())
                                .ReturnsAsync(disciplinas);
            _cursoRepositoryMock.Setup(repo => repo.ObterDisciplinasDoCurso(request.Id))
                                .ReturnsAsync(disciplinas);
            _cursoRepositoryMock.Setup(repo => repo.DesativarDisciplina(id))
                                .ReturnsAsync(false);

            // Act
            var result = await _cursoService.DesativarDisciplina(id);

            // Assert
            Assert.False(result);
        }
        #endregion

        #region AreaConhecimento
        [Fact]
        public async Task ObterAreaConhecimento_IdInvalido_RetornaAPICustomException()
        {
            // Arrange
            var id = 1;
            _cursoRepositoryMock.Setup(repo => repo.ObterAreaConhecimento(id))
                                .ReturnsAsync((AreaConhecimentoModel)null);

            // Act & Assert
            await Assert.ThrowsAsync<APICustomException>(() => _cursoService.ObterAreaConhecimento(id));
        }

        [Fact]
        public async Task ObterAreasConhecimento_ListaVazia_RetornaAPICustomException()
        {
            // Arrange
            _cursoRepositoryMock.Setup(repo => repo.ObterAreasConhecimento())
                                .ReturnsAsync(new List<AreaConhecimentoModel>());

            // Act & Assert
            await Assert.ThrowsAsync<APICustomException>(() => _cursoService.ObterAreasConhecimento());
        }

        [Fact]
        public async Task CadastrarAreaConhecimento_AreaInvalida_RetornaAPICustomException()
        {
            // Arrange
            var request = _areaConhecimentoFactory.ObterNovaAreaConhecimentoRequest();
            _cursoRepositoryMock.Setup(repo => repo.CadastrarAreaConhecimento(
                It.IsAny<AreaConhecimentoArgument>()))
                .ThrowsAsync(new Exception("Area de conhecimento já cadastrada."));

            // Act & Assert
            await Assert.ThrowsAsync<APICustomException>(() => _cursoService.CadastrarAreaConhecimento(request));
        }

        [Fact]
        public async Task AtualizarAreaConhecimento_SolicitacaoInvalida_RetornaException()
        {
            // Arrange
            var request = _areaConhecimentoFactory.ObterAreaConhecimentoRequestValida();
            _cursoRepositoryMock.Setup(repo => repo.ObterAreaConhecimento(request.Id))
                                .ThrowsAsync(new Exception("null"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _cursoService.AtualizarAreaConhecimento(request));
        }

        [Fact]
        public async Task DesativarAreaConhecimento_IdInvalida_RetornaAPICustomException()
        {
            // Arrange
            var id = 1;
            _cursoRepositoryMock.Setup(repo => repo.ObterAreaConhecimento(id))
                                .ReturnsAsync((AreaConhecimentoModel)null);

            // Act & Assert
            await Assert.ThrowsAsync<APICustomException>(() => _cursoService.DesativarAreaConhecimento(id));
        }
        #endregion
    }
}

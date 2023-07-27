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
    public class CursoServiceTests
    {
        private readonly Mock<ICursoRepository> _cursoRepositoryMock;
        private readonly ICursoService _cursoService;

        private readonly CursoFactory _cursoFactory;

        public CursoServiceTests()
        {
            _cursoRepositoryMock = new Mock<ICursoRepository>();
            var mapper = new ObjectConverter();
            _cursoService = new CursoService(_cursoRepositoryMock.Object, mapper);

            _cursoFactory = new CursoFactory();
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
            _cursoRepositoryMock.Setup(repo => repo.ObterCursos())
                                .ReturnsAsync(cursos);

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
            var request = new NovoCursoRequest { /* mock */ };
            var areaConhecimento = new AreaConhecimentoModel { /* mock */ };
            _cursoRepositoryMock.Setup(repo => repo.ObterAreaConhecimento(request.IdAreaConhecimento))
                                .ReturnsAsync(areaConhecimento);
            _cursoRepositoryMock.Setup(repo => repo.CadastrarCurso(It.IsAny<CursoArgument>()))
                                .ReturnsAsync(new CursoModel { /* mock */ });

            // Act
            var result = await _cursoService.CadastrarCurso(request);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NovoCursoResponse>(result);
        }

        [Fact]
        public async Task CadastrarCurso_InvalidCurso_ThrowsAPICustomException()
        {
            // Arrange
            var request = new NovoCursoRequest { /* mock */ };
            _cursoRepositoryMock.Setup(repo => repo.ObterAreaConhecimento(request.IdAreaConhecimento))
                                .ReturnsAsync((AreaConhecimentoModel)null);

            // Act & Assert
            await Assert.ThrowsAsync<APICustomException>(() => _cursoService.CadastrarCurso(request));
        }

        // AtualizarCurso
        [Fact]
        public async Task AtualizarCurso_ValidRequest_ReturnsCursoResponse()
        {
            // Arrange
            var request = new CursoRequest { /* mock */ };
            var curso = new CursoModel { /* mock */ };
            var disciplinas = new List<DisciplinaModel> { new DisciplinaModel { /* mock */ } };
            var areaConhecimento = new AreaConhecimentoModel { /* mock */ };
            _cursoRepositoryMock.Setup(repo => repo.ObterCurso(request.Id))
                                .ReturnsAsync(curso);
            _cursoRepositoryMock.Setup(repo => repo.ObterDisciplinasDoCurso(request.Id))
                                .ReturnsAsync(disciplinas);
            _cursoRepositoryMock.Setup(repo => repo.ObterAreaConhecimento(request.Id))
                                .ReturnsAsync(areaConhecimento);
            _cursoRepositoryMock.Setup(repo => repo.AtualizarCurso(It.IsAny<CursoArgument>()))
                                .ReturnsAsync(new CursoModel { /* mock */ });

            // Act
            var result = await _cursoService.AtualizarCurso(request);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CursoResponse>(result);
        }

        [Fact]
        public async Task AtualizarCurso_InvalidRequest_ThrowsAPICustomException()
        {
            // Arrange
            var request = new CursoRequest { /* mock */ };
            _cursoRepositoryMock.Setup(repo => repo.ObterCurso(request.Id))
                                .ReturnsAsync((CursoModel)null);

            // Act & Assert
            await Assert.ThrowsAsync<APICustomException>(() => _cursoService.AtualizarCurso(request));
        }

        [Fact]
        public async Task DesativarCurso_ValidId_ReturnsTrue()
        {
            // Arrange
            var id = 1;
            _cursoRepositoryMock.Setup(repo => repo.ObterCurso(id))
                                .ReturnsAsync(new CursoModel { /* mock */ });
            _cursoRepositoryMock.Setup(repo => repo.DesativarCurso(id))
                                .ReturnsAsync(true);

            // Act
            var result = await _cursoService.DesativarCurso(id);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DesativarCurso_InvalidId_ThrowsAPICustomException()
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
        public async Task ObterDisciplina_InvalidId_ThrowsAPICustomException()
        {
            // Arrange
            var id = 1;
            _cursoRepositoryMock.Setup(repo => repo.ObterDisciplina(id))
                                .ReturnsAsync((DisciplinaModel)null);

            // Act & Assert
            await Assert.ThrowsAsync<APICustomException>(() => _cursoService.ObterDisciplina(id));
        }

        [Fact]
        public async Task ObterDisciplinas_NoDisciplinas_ThrowsAPICustomException()
        {
            // Arrange
            _cursoRepositoryMock.Setup(repo => repo.ObterDisciplinas())
                                .ReturnsAsync(new List<DisciplinaModel>());

            // Act & Assert
            await Assert.ThrowsAsync<APICustomException>(() => _cursoService.ObterDisciplinas());
        }

        [Fact]
        public async Task CadastrarDisciplina_InvalidCurso_ThrowsAPICustomException()
        {
            // Arrange
            var request = new NovaDisciplinaRequest { /* mock */ };
            _cursoRepositoryMock.Setup(repo => repo.ObterCurso(request.IdCurso))
                                .ReturnsAsync((CursoModel)null);

            // Act & Assert
            await Assert.ThrowsAsync<APICustomException>(() => _cursoService.CadastrarDisciplina(request));
        }

        [Fact]
        public async Task AtualizarDisciplina_InvalidId_ThrowsAPICustomException()
        {
            // Arrange
            var request = new DisciplinaRequest { /* mock */ };
            var disciplina = new DisciplinaModel { /* mock */ };
            _cursoRepositoryMock.Setup(repo => repo.ObterDisciplina(request.Id))
                                .ReturnsAsync((DisciplinaModel)null);

            // Act & Assert
            await Assert.ThrowsAsync<APICustomException>(() => _cursoService.AtualizarDisciplina(request));
        }

        [Fact]
        public async Task DesativarDisciplina_InvalidId_ThrowsAPICustomException()
        {
            // Arrange
            var id = 1;
            _cursoRepositoryMock.Setup(repo => repo.ObterDisciplina(id))
                                .ReturnsAsync((DisciplinaModel)null);

            // Act & Assert
            await Assert.ThrowsAsync<APICustomException>(() => _cursoService.DesativarDisciplina(id));
        }

        [Fact]
        public async Task DesativarDisciplina_AlreadyInactive_ReturnsFalse()
        {
            // Arrange
            var id = 1;
            _cursoRepositoryMock.Setup(repo => repo.ObterDisciplinas())
                .ReturnsAsync(new List<DisciplinaModel> { new DisciplinaModel { Id = id, Ativo = false } });
            _cursoRepositoryMock.Setup(repo => repo.DesativarDisciplina(id))
                                .ReturnsAsync(true);

            // Act
            var result = await _cursoService.DesativarDisciplina(id);

            // Assert
            Assert.False(result);
        }
        #endregion

        #region AreaConhecimento
        [Fact]
        public async Task ObterAreaConhecimento_InvalidId_ThrowsAPICustomException()
        {
            // Arrange
            var id = 1;
            _cursoRepositoryMock.Setup(repo => repo.ObterAreaConhecimento(id))
                                .ReturnsAsync((AreaConhecimentoModel)null);

            // Act & Assert
            await Assert.ThrowsAsync<APICustomException>(() => _cursoService.ObterAreaConhecimento(id));
        }

        [Fact]
        public async Task ObterAreasConhecimento_NoAreas_ThrowsAPICustomException()
        {
            // Arrange
            _cursoRepositoryMock.Setup(repo => repo.ObterAreasConhecimento())
                                .ReturnsAsync(new List<AreaConhecimentoModel>());

            // Act & Assert
            await Assert.ThrowsAsync<APICustomException>(() => _cursoService.ObterAreasConhecimento());
        }

        [Fact]
        public async Task CadastrarAreaConhecimento_ThrowsAPICustomException()
        {
            // Arrange
            var request = new NovaAreaConhecimentoRequest { /* mock */ };
            _cursoRepositoryMock.Setup(repo => repo.CadastrarAreaConhecimento(
                It.IsAny<AreaConhecimentoArgument>()))
                .ThrowsAsync(new Exception("Some error occurred."));

            // Act & Assert
            await Assert.ThrowsAsync<APICustomException>(() => _cursoService.CadastrarAreaConhecimento(request));
        }

        [Fact]
        public async Task AtualizarAreaConhecimento_InvalidRequest_ThrowsAPICustomException()
        {
            // Arrange
            var request = new AreaConhecimentoRequest { /* mock */ };
            _cursoRepositoryMock.Setup(repo => repo.ObterAreaConhecimento(request.Id))
                                .ReturnsAsync((AreaConhecimentoModel)null);

            // Act & Assert
            await Assert.ThrowsAsync<APICustomException>(() => _cursoService.AtualizarAreaConhecimento(request));
        }

        [Fact]
        public async Task DesativarAreaConhecimento_InvalidId_ThrowsAPICustomException()
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

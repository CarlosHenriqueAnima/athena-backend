using AthenasAcademy.Services.Core.Arguments;
using AthenasAcademy.Services.Core.Exceptions;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Services;
using AthenasAcademy.Services.Test.Factory;
using Moq;
using Xunit;

namespace AthenasAcademy.Services.Test.Services
{
    public class AlunoServiceTests
    {
        private readonly AlunoService _alunoService;
        private readonly Mock<IAlunoRepository> _alunoRepositoryMock;

        private readonly AlunoFactory _AlunoFactory;

        public AlunoServiceTests()
        {
            _alunoRepositoryMock = new Mock<IAlunoRepository>();
            _alunoService = new AlunoService(_alunoRepositoryMock.Object);
            _AlunoFactory = new AlunoFactory();
        }

        [Fact]
        public async Task CadastrarAluno_ArgumentValido_RetornaAlunoModel()
        {
            // Arrange
            var novoAlunoArgument = _AlunoFactory.RetornarNovoAlunoArgumentValido();
            var expectedAlunoModel = _AlunoFactory.ObterAlunoModelValido();

            _alunoRepositoryMock.Setup(repo => repo.CadastrarAluno(It.IsAny<NovoAlunoArgument>()))
                                .ReturnsAsync(expectedAlunoModel);

            // Act
            var result = await _alunoService.CadastrarAluno(novoAlunoArgument);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CadastrarAluno_ExcecaoDeRepositorio_RetornaAPICustomException()
        {
            // Arrange
            var novoAlunoArgument = _AlunoFactory.RetornarNovoAlunoArgumentValido();
            _alunoRepositoryMock.Setup(repo => repo.CadastrarAluno(It.IsAny<NovoAlunoArgument>()))
                                .ThrowsAsync(new Exception("Aluno não existe."));

            // Act & Assert
            await Assert.ThrowsAsync<APICustomException>(() => _alunoService.CadastrarAluno(novoAlunoArgument));
        }

        [Fact]
        public async Task CadastrarDetalheAluno_ArgumentValido_RetornaDetalheAlunoModel()
        {
            // Arrange
            var novoDetalheAlunoArgument = _AlunoFactory.RetornarNovoDetalheAlunoArgumentValido();
            var expectedDetalheAlunoModel = _AlunoFactory.ObterDetalheAlunoModelValido();
            _alunoRepositoryMock.Setup(repo => repo.CadastrarDetalheAluno(It.IsAny<NovoDetalheAlunoArgument>()))
                                .ReturnsAsync(expectedDetalheAlunoModel);

            // Act
            var result = await _alunoService.CadastrarDetalheAluno(novoDetalheAlunoArgument);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CadastrarDetalheAluno_ExcecaoDeRepositorio_RetornaAPICustomException()
        {
            // Arrange
            var novoDetalheAlunoArgument = _AlunoFactory.RetornarNovoDetalheAlunoArgumentValido();
            _alunoRepositoryMock.Setup(repo => repo.CadastrarDetalheAluno(It.IsAny<NovoDetalheAlunoArgument>()))
                                .ThrowsAsync(new Exception("Dados incorretos"));

            // Act & Assert
            await Assert.ThrowsAsync<APICustomException>(() => _alunoService.CadastrarDetalheAluno(novoDetalheAlunoArgument));
        }

        [Fact]
        public async Task CadastrarEnderecoAluno_ArgumentValido_RetornaEnderecoAlunoModel()
        {
            // Arrange
            var novoEnderecoAlunoArgument = _AlunoFactory.RetornarNovoEnderecoAlunoArgumentValido();
            var expectedEnderecoAlunoModel = _AlunoFactory.ObterEnderecoAlunoModelValido();
            _alunoRepositoryMock.Setup(repo => repo.CadastrarEnderecoAluno(It.IsAny<NovoEnderecoAlunoArgument>()))
                                .ReturnsAsync(expectedEnderecoAlunoModel);

            // Act
            var result = await _alunoService.CadastrarEnderecoAluno(novoEnderecoAlunoArgument);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CadastrarEnderecoAluno_ExcecaoDeRepositorio_RetornaAPICustomException()
        {
            // Arrange
            var novoEnderecoAlunoArgument = _AlunoFactory.RetornarNovoEnderecoAlunoArgumentValido();
            _alunoRepositoryMock.Setup(repo => repo.CadastrarEnderecoAluno(It.IsAny<NovoEnderecoAlunoArgument>()))
                                .ThrowsAsync(new Exception("Endereço incompleto"));

            // Act & Assert
            await Assert.ThrowsAsync<APICustomException>(() => _alunoService.CadastrarEnderecoAluno(novoEnderecoAlunoArgument));
        }

        [Fact]
        public async Task CadastrarTelefoneAluno_ArgumentValido_RetornaTelefoneAlunoModel()
        {
            // Arrange
            var novoTelefoneAlunoArgument = _AlunoFactory.RetornarNovoTelefoneAlunoArgumentValido();
            var expectedTelefoneAlunoModel = _AlunoFactory.ObterTelefoneAlunoModelValido();
            _alunoRepositoryMock.Setup(repo => repo.CadastrarTelefoneAluno(It.IsAny<NovoTelefoneAlunoArgument>()))
                                .ReturnsAsync(expectedTelefoneAlunoModel);

            // Act
            var result = await _alunoService.CadastrarTelefoneAluno(novoTelefoneAlunoArgument);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CadastrarTelefoneAluno_ExcecaoDeRepositorio_RetornaAPICustomException()
        {
            // Arrange
            var novoTelefoneAlunoArgument = _AlunoFactory.RetornarNovoTelefoneAlunoArgumentValido();
            _alunoRepositoryMock.Setup(repo => repo.CadastrarTelefoneAluno(It.IsAny<NovoTelefoneAlunoArgument>()))
                                .ThrowsAsync(new Exception("Telefone inválido"));

            // Act & Assert
            await Assert.ThrowsAsync<APICustomException>(() => _alunoService.CadastrarTelefoneAluno(novoTelefoneAlunoArgument));
        }

        [Fact]
        public async Task ObterAluno_IdValido_RetornaAlunoModel()
        {
            // Arrange
            int alunoId = 1;
            var expectedAlunoModel = _AlunoFactory.ObterAlunoModelValido();
            _alunoRepositoryMock.Setup(repo => repo.ObterAluno(alunoId))
                                .ReturnsAsync(expectedAlunoModel);

            // Act
            var result = await _alunoService.ObterAluno(alunoId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedAlunoModel, result);
        }

        [Fact]
        public async Task ObterAluno_IdInvalido_RetornaAPICustomException()
        {
            // Arrange
            int alunoId = 99;
            _alunoRepositoryMock.Setup(repo => repo.ObterAluno(alunoId))
                                .ReturnsAsync((AlunoModel)null);

            // Act & Assert
            await Assert.ThrowsAsync<APICustomException>(() => _alunoService.ObterAluno(alunoId));
        }
    }
}

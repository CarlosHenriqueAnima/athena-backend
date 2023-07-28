using AthenasAcademy.Services.Core.CrossCutting;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Repositories.Interfaces;
using AthenasAcademy.Services.Core.Services.SQSProducer;
using AthenasAcademy.Services.Core.Services;
using AthenasAcademy.Services.Domain.Responses;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AthenasAcademy.Services.Test.Factory;

namespace AthenasAcademy.Services.Test.Services
{
    public class MatriculaServiceTests
    {
        private readonly MatriculaService _matriculaService;
        private readonly Mock<IQueueProducerService> _queueProducerServiceMock;
        private readonly Mock<IAlunoService> _alunoServiceMock;
        private readonly Mock<IMatriculaRepository> _matriculaRepositoryMock;

        private readonly MatriculaFactory _matriculaFactory;
        private readonly AlunoFactory _alunoFactory;
        public MatriculaServiceTests()
        {
            _queueProducerServiceMock = new Mock<IQueueProducerService>();
            _alunoServiceMock = new Mock<IAlunoService>();
            _matriculaRepositoryMock = new Mock<IMatriculaRepository>();

            _matriculaService = new MatriculaService(
                _queueProducerServiceMock.Object,
                _alunoServiceMock.Object,
                _matriculaRepositoryMock.Object
            );

            _matriculaFactory = new MatriculaFactory();
            _alunoFactory = new AlunoFactory();
        }

        [Fact]
        public async Task MatricularAluno_InscricaoValida_RetornaMatriculaStatusResponse()
        {
            // Arrange
            var inscricao = 1001;
            var fichaAluno = _alunoFactory.ObterFichaAlunoValida();
            var matricula = _matriculaFactory.ObterMatriculaModelValida();
            _alunoServiceMock.Setup(service => service.ObterFichaAluno(inscricao))
                                    .ReturnsAsync(fichaAluno);
            _matriculaRepositoryMock.Setup(repo => repo.ObterMatricula(inscricao))
                                    .ReturnsAsync(matricula);
            _matriculaRepositoryMock.Setup(repo => repo.AtivarMatricula(fichaAluno))
                                    .ReturnsAsync(matricula);
            _queueProducerServiceMock.Setup(service => service.GerarBoleto(fichaAluno))
                                     .ReturnsAsync(true);
            _queueProducerServiceMock.Setup(service => service.GerarContrato(fichaAluno))
                                     .ReturnsAsync(true);

            // Act
            var result = await _matriculaService.MatricularAluno(inscricao);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<MatriculaStatusResponse>(result);
            Assert.Equal(matricula.Matricula, result.Matricula);
            Assert.Equal(matricula.CodigoContrato, result.Contrato);
            Assert.True(result.BoletoPago);
            Assert.True(result.ContratoAssinado);
        }

        [Fact]
        public async Task RegistrarPreMatricula_FichaAlunoValida_RegistraPreMatriculaEnviaFilas()
        {
            // Arrange
            var fichaAluno = _alunoFactory.ObterFichaAlunoValida();
            var matricula = _matriculaFactory.ObterMatriculaModelValida();
            _matriculaRepositoryMock.Setup(repo => repo.GerarPreMatricula(fichaAluno))
                                    .ReturnsAsync(matricula);
            _queueProducerServiceMock.Setup(service => service.GerarContrato(fichaAluno))
                                     .ReturnsAsync(true);
            _queueProducerServiceMock.Setup(service => service.GerarBoleto(fichaAluno))
                                     .ReturnsAsync(true);

            // Act
            await _matriculaService.RegistrarPreContratoMatricula(fichaAluno);

            // Assert
            Assert.Equal(matricula.Matricula, fichaAluno.Contrato.Matricula);
            Assert.Equal(matricula.CodigoContrato, fichaAluno.Contrato.NumeroContrato);
            _matriculaRepositoryMock.Verify(repo => repo.GerarPreMatricula(fichaAluno), Times.Once);
            _queueProducerServiceMock.Verify(service => service.GerarBoleto(fichaAluno), Times.Once);
            _queueProducerServiceMock.Verify(queue => queue.GerarContrato(fichaAluno), Times.Once);
        }
    }
}

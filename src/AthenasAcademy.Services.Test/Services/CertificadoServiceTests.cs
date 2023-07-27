//using AthenasAcademy.Services.Core.Arguments;
//using AthenasAcademy.Services.Core.Exceptions;
//using AthenasAcademy.Services.Core.Models;
//using AthenasAcademy.Services.Core.Repositories.Clients;
//using AthenasAcademy.Services.Core.Repositories.Interfaces;
//using AthenasAcademy.Services.Core.Repositories.S3;
//using AthenasAcademy.Services.Core.Requests;
//using AthenasAcademy.Services.Core.Services;
//using AthenasAcademy.Services.Core.Services.Interfaces;
//using AthenasAcademy.Services.Domain.Responses;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Http.HttpResults;
//using Moq;
//using Refit;
//using System.Net;
//using Xunit;

//namespace AthenasAcademy.Services.Test.Services
//{
//    public class CertificadoServiceTests
//    {
//        private readonly CertificadoService _certificadoService;
//        private readonly Mock<IAwsS3Repository> _awsS3RepositoryMock;
//        private readonly Mock<ICertificadoRepository> _certificadoRepositoryMock;
//        private readonly Mock<IGeradorCertificadoRepository> _geradorCertificadoClientMock;
//        private readonly Mock<ITokenService> _tokenServiceMock;
//        private readonly Mock<IMatriculaService> _matriculaServiceMock;
//        private readonly Mock<IAlunoService> _alunoServiceMock;
//        private readonly Mock<ICursoService> _cursoServiceMock;

//        public CertificadoServiceTests()
//        {
//            _awsS3RepositoryMock = new Mock<IAwsS3Repository>();
//            _certificadoRepositoryMock = new Mock<ICertificadoRepository>();
//            _geradorCertificadoClientMock = new Mock<IGeradorCertificadoRepository>();
//            _tokenServiceMock = new Mock<ITokenService>();
//            _matriculaServiceMock = new Mock<IMatriculaService>();
//            _alunoServiceMock = new Mock<IAlunoService>();
//            _cursoServiceMock = new Mock<ICursoService>();
//            _certificadoService = new CertificadoService(
//                _awsS3RepositoryMock.Object,
//                _certificadoRepositoryMock.Object,
//                _geradorCertificadoClientMock.Object,
//                _tokenServiceMock.Object,
//                _matriculaServiceMock.Object,
//                _alunoServiceMock.Object,
//                _cursoServiceMock.Object);
//        }

//        [Fact]
//        public async Task GerarCertificado_ValidMatricula_ReturnsCertificadoUri()
//        {
//            // Arrange
//            int matricula = 1;
//            var expectedCertificadoUri = "https://mock.com/certificados/certificado1.pdf";
//            var novoCertificadoRequest = new NovoCertificadoRequest { /* mock */ };
//            var novoCertificadoArgument = new NovoCertificadoArgument { /* mock */ };
//            var expectedCertificadoModel = new CertificadoModel { /* mock */ };
//            var expectedNovoCertificadoPDFResponse = new NovoCertificadoPDFResponse { /* mock */ };
//            _matriculaServiceMock.Setup(service => service.ObterDetalhesMatricula(matricula))
//                                 .ReturnsAsync(new DetalheMatriculaAlunoModel { /* mock */ });
//            _alunoServiceMock.Setup(service => service.ObterAluno(It.IsAny<int>()))
//                             .ReturnsAsync(new AlunoModel { /* mock */ });
//            _cursoServiceMock.Setup(service => service.ObterCurso(It.IsAny<int>()))
//                             .ReturnsAsync(new CursoResponse { /* mock */ });
//            _tokenServiceMock.Setup(service => service.GerarTokenRequestClient())
//                             .ReturnsAsync("some_random_token");
//            _geradorCertificadoClientMock.Setup(client => client.GerarCertificadoPDF(novoCertificadoRequest, "token"))
//                                         .ReturnsAsync(new ApiResponse<NovoCertificadoPDFResponse>(
//                                             HttpStatusCode.OK,
//                                             new System.Net.Http.Headers.HttpResponseHeaders(),
//                                             expectedNovoCertificadoPDFResponse));
//            _awsS3RepositoryMock.Setup(repo => repo.ObterPDFAsync(expectedNovoCertificadoPDFResponse.CaminhoArquivo))
//                                .ReturnsAsync(new MemoryStream(new byte[] { 1, 2, 3 }));
//            _certificadoRepositoryMock.Setup(repo => repo.GerarCertificado(novoCertificadoArgument))
//                                      .ReturnsAsync(new CertificadoModel());

//            // Act
//            var result = await _certificadoService.GerarCertificado(matricula);

//            // Assert
//            Assert.Equal(expectedCertificadoUri, result);
//            // Add additional assertions based on the expected result
//        }

//        [Fact]
//        public async Task GerarCertificado_RepositoryError_ThrowsAPICustomException()
//        {
//            // Arrange
//            int matricula = 1;
//            var novoCertificadoRequest = new NovoCertificadoRequest { /* mock */ };
//            _matriculaServiceMock.Setup(service => service.ObterDetalhesMatricula(matricula))
//                                 .ReturnsAsync(new DetalheMatriculaAlunoModel { /* mock */ });
//            _alunoServiceMock.Setup(service => service.ObterAluno(It.IsAny<int>()))
//                             .ReturnsAsync(new AlunoModel { /* mock */ });
//            _cursoServiceMock.Setup(service => service.ObterCurso(It.IsAny<int>()))
//                             .ReturnsAsync(new CursoResponse { /* mock */ });
//            _tokenServiceMock.Setup(service => service.GerarTokenRequestClient())
//                             .ReturnsAsync("some_random_token");
//            _geradorCertificadoClientMock.Setup(client => client.GerarCertificadoPDF(novoCertificadoRequest, "token"))
//                                         .ReturnsAsync(new ApiResponse<NovoCertificadoPDFResponse>(
//                                             HttpStatusCode.InternalServerError,
//                                             new System.Net.Http.Headers.HttpResponseHeaders(),
//                                             (NovoCertificadoPDFResponse)null));

//            // Act & Assert
//            await Assert.ThrowsAsync<APICustomException>(() => _certificadoService.GerarCertificado(matricula));
//        }

//        [Fact]
//        public async Task GerarCertificado_RepositoryException_ThrowsAPICustomException()
//        {
//            // Arrange
//            int matricula = 1;
//            var novoCertificadoRequest = new NovoCertificadoRequest { /* mock */ };
//            _matriculaServiceMock.Setup(service => service.ObterDetalhesMatricula(matricula))
//                                 .ReturnsAsync(new DetalheMatriculaAlunoModel { /* mock */ });
//            _alunoServiceMock.Setup(service => service.ObterAluno(It.IsAny<int>()))
//                             .ReturnsAsync(new AlunoModel { /* mock */ });
//            _cursoServiceMock.Setup(service => service.ObterCurso(It.IsAny<int>()))
//                             .ReturnsAsync(new CursoResponse { /* mock */ });
//            _tokenServiceMock.Setup(service => service.GerarTokenRequestClient())
//                             .ReturnsAsync("some_random_token");
//            _geradorCertificadoClientMock.Setup(client => client.GerarCertificadoPDF(novoCertificadoRequest, "token"))
//                                         .ThrowsAsync(new Exception("Some repository exception"));

//            // Act & Assert
//            await Assert.ThrowsAsync<APICustomException>(() => _certificadoService.GerarCertificado(matricula));
//        }

//        [Fact]
//        public async Task ObterCertificado_ValidMatricula_ReturnsMemoryStream()
//        {
//            // Arrange
//            int matricula = 1;
//            var expectedCertificadoModel = new CertificadoModel { /* mock */ };
//            var expectedMemoryStream = new MemoryStream(new byte[] { 1, 2, 3 });
//            _certificadoRepositoryMock.Setup(repo => repo.ObterCertificado(matricula))
//                                      .ReturnsAsync(expectedCertificadoModel);
//            _awsS3RepositoryMock.Setup(repo => repo.ObterPDFAsync(expectedCertificadoModel.CaminhoCertificadoPdf))
//                                .ReturnsAsync(expectedMemoryStream);

//            // Act
//            var result = await _certificadoService.ObterCertificado(matricula);

//            // Assert
//            Assert.NotNull(result);
//            // Add additional assertions based on the expected result
//        }

//        [Fact]
//        public async Task ObterCertificado_InvalidMatricula_ThrowsAPICustomException()
//        {
//            // Arrange
//            int matricula = 1;
//            _certificadoRepositoryMock.Setup(repo => repo.ObterCertificado(matricula))
//                                      .ReturnsAsync((CertificadoModel)null);

//            // Act & Assert
//            await Assert.ThrowsAsync<APICustomException>(() => _certificadoService.ObterCertificado(matricula));
//        }
//    }
//}

using AthenasAcademy.Services.Core.Arguments;
using AthenasAcademy.Services.Core.CrossCutting;
using AthenasAcademy.Services.Core.Exceptions;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Repositories.Clients;
using AthenasAcademy.Services.Core.Repositories.Interfaces;
using AthenasAcademy.Services.Core.Repositories.S3;
using AthenasAcademy.Services.Core.Requests;
using AthenasAcademy.Services.Core.Services;
using AthenasAcademy.Services.Core.Services.Interfaces;
using AthenasAcademy.Services.Domain.Responses;
using AthenasAcademy.Services.Test.Factory;
using Moq;
using Refit;
using System.Net;
using Xunit;

namespace AthenasAcademy.Services.Test.Services
{
    public class CertificadoServiceTests
    {
        private readonly CertificadoService _certificadoService;
        private readonly Mock<IAwsS3Repository> _awsS3RepositoryMock;
        private readonly Mock<ICertificadoRepository> _certificadoRepositoryMock;
        private readonly Mock<IGeradorCertificadoRepository> _geradorCertificadoClientMock;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly Mock<IAlunoService> _alunoServiceMock;
        private readonly Mock<ICursoService> _cursoServiceMock;

        private readonly CertificadoFactory _certificadoFactory;
        private readonly CursoFactory _cursoFactory;
        private readonly AlunoFactory _alunoFactory;

        public CertificadoServiceTests()
        {
            _awsS3RepositoryMock = new Mock<IAwsS3Repository>();
            _certificadoRepositoryMock = new Mock<ICertificadoRepository>();
            _geradorCertificadoClientMock = new Mock<IGeradorCertificadoRepository>();
            _tokenServiceMock = new Mock<ITokenService>();
            _alunoServiceMock = new Mock<IAlunoService>();
            _cursoServiceMock = new Mock<ICursoService>();

            _certificadoService = new CertificadoService(
                _awsS3RepositoryMock.Object,
                _certificadoRepositoryMock.Object,
                _geradorCertificadoClientMock.Object,
                _tokenServiceMock.Object,
                _alunoServiceMock.Object,
                _cursoServiceMock.Object
            );

            _certificadoFactory = new CertificadoFactory();
            _cursoFactory = new CursoFactory();
            _alunoFactory = new AlunoFactory();
        }

        //[Fact] // travei aqui Hugo Damasceno
        //public async Task GerarCertificado_MatriculaValida_ReturnsUriDownload()
        //{
        //    // Arrange
        //    var matricula = 1;
        //    var request = _certificadoFactory.ObterNovoCertificadoRequestValido();
        //    var response = _certificadoFactory.ObterNovoCertificadoPDFResponseValido();
        //    _alunoServiceMock.Setup(service => service.ObterDetalheAluno(null, null, matricula))
        //                     .ReturnsAsync(_alunoFactory.RetornarDetalheAlunoArgumentoModelValido());
        //    _cursoServiceMock.Setup(service => service.ObterCurso(request.CodigoCurso))
        //                     .ReturnsAsync(_cursoFactory.ObterCursoResponseValido());
        //    _tokenServiceMock.Setup(service => service.GerarTokenRequestClient())
        //                     .ReturnsAsync("token_valido");
        //    _geradorCertificadoClientMock.Setup(client => client.GerarCertificadoPDF(request, "token_valido"))
        //                                 .ReturnsAsync(response);
        //    _certificadoRepositoryMock.Setup(repo => repo.GerarCertificado(It.IsAny<NovoCertificadoArgument>()))
        //                              .Returns(Task.CompletedTask);

        //    // Act
        //    var result = await _certificadoService.GerarCertificado(matricula);

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.Equal(response.Content.UriDownload, result);
        //    _alunoServiceMock.Verify(service => service.ObterDetalheAluno(null, null, matricula), Times.Once);
        //    _cursoServiceMock.Verify(service => service.ObterCurso(request.CodigoCurso), Times.Once);
        //    _tokenServiceMock.Verify(service => service.GerarTokenRequestClient(), Times.Once);
        //    _geradorCertificadoClientMock.Verify(client => client.GerarCertificadoPDF(request, "token_valido"), Times.Once);
        //    _certificadoRepositoryMock.Verify(repo => repo.GerarCertificado(It.IsAny<NovoCertificadoArgument>()), Times.Once);
        //}

        //[Fact] // travei aqui Hugo Damasceno
        //public async Task GerarCertificado_ErroServicoGerador_RetornaAPICustomException()
        //{
        //    // Arrange
        //    var matricula = 1;
        //    var request = _certificadoFactory.ObterNovoCertificadoRequestValido();
        //    var httpResponse = new HttpResponseMessage(HttpStatusCode.BadRequest);
        //    var customException = new APICustomException("Internal server error");
        //    var response = new ApiResponse<NovoCertificadoPDFResponse>(httpResponse, null, null, new ApiException("Internal server error"));
        //    _alunoServiceMock.Setup(service => service.ObterDetalheAluno(null, null, matricula))
        //                     .ReturnsAsync(_alunoFactory.RetornarDetalheAlunoArgumentoModelValido());
        //    _cursoServiceMock.Setup(service => service.ObterCurso(request.CodigoCurso))
        //                     .ReturnsAsync(_cursoFactory.ObterCursoResponseValido());
        //    _tokenServiceMock.Setup(service => service.GerarTokenRequestClient())
        //                     .ReturnsAsync("token_valido");
        //    _geradorCertificadoClientMock.Setup(client => client.GerarCertificadoPDF(request, "token_valido"))
        //                                 .ReturnsAsync(response);

        //    // Act & Assert
        //    await Assert.ThrowsAsync<APICustomException>(async () => await _certificadoService.GerarCertificado(matricula));
        //    _alunoServiceMock.Verify(service => service.ObterDetalheAluno(null, null, matricula), Times.Once);
        //    _cursoServiceMock.Verify(service => service.ObterCurso(request.CodigoCurso), Times.Once);
        //    _tokenServiceMock.Verify(service => service.GerarTokenRequestClient(), Times.Once);
        //    _geradorCertificadoClientMock.Verify(client => client.GerarCertificadoPDF(request, "token_valido"), Times.Once);
        //    _certificadoRepositoryMock.Verify(repo => repo.GerarCertificado(It.IsAny<NovoCertificadoArgument>()), Times.Never);
        //}

        [Fact]
        public async Task ObterCertificado_MatriculaValida_RetornaMemoryStream()
        {
            // Arrange
            var matricula = 1001;
            var certificado = _certificadoFactory.ObterCertificadoModelValido();
            var memoryStream = new MemoryStream();
            _certificadoRepositoryMock.Setup(repo => repo.ObterCertificado(matricula))
                                      .ReturnsAsync(certificado);
            _awsS3RepositoryMock.Setup(repo => repo.ObterPDFAsync(certificado.CaminhoCertificadoPdf))
                                .ReturnsAsync(memoryStream);

            // Act
            var result = await _certificadoService.ObterCertificado(matricula);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<MemoryStream>(result);
            _certificadoRepositoryMock.Verify(repo => repo.ObterCertificado(matricula), Times.Once);
            _awsS3RepositoryMock.Verify(repo => repo.ObterPDFAsync(certificado.CaminhoCertificadoPdf), Times.Once);
        }

        [Fact]
        public async Task ObterCertificado_CertificadoNaoEncontrado_RetornaAPICustomException()
        {
            // Arrange
            var matricula = 1001;
            CertificadoModel certificado = null;
            _certificadoRepositoryMock.Setup(repo => repo.ObterCertificado(matricula))
                                      .ReturnsAsync(certificado);

            // Act & Assert
            await Assert.ThrowsAsync<APICustomException>(async () => await _certificadoService.ObterCertificado(matricula));
            _certificadoRepositoryMock.Verify(repo => repo.ObterCertificado(matricula), Times.Once);
            _awsS3RepositoryMock.Verify(repo => repo.ObterPDFAsync(It.IsAny<string>()), Times.Never);
        }
    }
}

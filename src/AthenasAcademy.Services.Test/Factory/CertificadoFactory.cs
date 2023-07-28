using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Requests;
using AthenasAcademy.Services.Domain.Responses;
using Refit;

namespace AthenasAcademy.Services.Test.Factory
{
    public class CertificadoFactory
    {
        private readonly AlunoFactory _alunoFactory;
        private readonly CursoFactory _cursoFactory;
        public CertificadoFactory()
        {
            _alunoFactory = new AlunoFactory();
            _cursoFactory = new CursoFactory();
        }
        public NovoCertificadoRequest ObterNovoCertificadoRequestValido()
        {
            return new NovoCertificadoRequest()
            {
                NomeAluno = "John Doe",
                Matricula = 1,
                NomeCurso = "Programming 101",
                CodigoCurso = 1,
                CargaHoraria = 100,
                DataConclusao = DateTime.Now,
                Aproveitamento = 95
            };
        }

        public CertificadoModel ObterCertificadoModelValido()
        {
            return new CertificadoModel
            {
                Id = 1,
                NomeAluno = "João da Silva",
                Matricula = "2023-ABC123",
                NomeCurso = "Curso de Programação",
                CodigoCurso = "ABC123",
                CargaHorariaCurso = 80,
                Aproveitamento = 85.5m,
                DataConclusao = DateTime.Now,
                Gerado = true,
                CaminhoCertificadoPdf = "/certificados/certificado_123456.pdf",
                CaminhoCertificadoPng = "/certificados/certificado_123456.png"
            };
        }

        // travei aqui Hugo Damasceno
        //public ApiResponse<NovoCertificadoPDFResponse> ObterNovoCertificadoPDFResponseValido()
        //{
        //    byte[] pdfBytes = new byte[] { 0x25, 0x50, 0x44, 0x46, 0x2D, 0x31, 0x2E, 0x35, 0x0D, 0x25, 0xE2, 0xE3, 0xCF, 0xD3, 0x0D, 0x0A };

        //    //NovoCertificadoRequest certificadoRequest = ObterNovoCertificadoRequestValido();

        //    var response = new NovoCertificadoPDFResponse
        //    {
        //        NomeArquivo = "Certificado_Joao_Silva.pdf",
        //        CaminhoArquivo = "/certificados/certificado_123456.pdf",
        //        PDFArquivo = pdfBytes,
        //        UriDownload = "https://example.com/certificados/certificado_123456.pdf"
        //    };

        //    return new ApiResponse<NovoCertificadoPDFResponse>(response);
        //}
    }
}

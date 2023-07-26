namespace AthenasAcademy.Services.Core.Services.Interfaces;

public interface ICertificadoService
{
    Task<string> GerarCertificado(int matricula);

    Task<MemoryStream> ObterCertificado(int matricula);
}
namespace AthenasAcademy.Services.Core.Services.Interfaces;

public interface ICertificadoService
{
    Task<string> GerarCertificado(string matricula);

    Task<MemoryStream> ObterCertificado(string matricula);
}
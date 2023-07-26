using AthenasAcademy.Services.Core.Arguments;
using AthenasAcademy.Services.Core.Models;

namespace AthenasAcademy.Services.Core.Repositories.Interfaces;

public interface ICertificadoRepository
{
    Task<CertificadoModel> GerarCertificado(NovoCertificadoArgument matricula);

    Task<CertificadoModel> ObterCertificado(int matricula);
}
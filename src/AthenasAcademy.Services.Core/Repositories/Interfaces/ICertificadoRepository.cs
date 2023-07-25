using AthenasAcademy.Services.Core.Arguments;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Domain.Requests;

namespace AthenasAcademy.Services.Core.Repositories.Interfaces;

public interface ICertificadoRepository
{
    Task<CertificadoModel> GerarCertificado(NovoCertificadoArgument matricula);

    Task<CertificadoModel> ObterCertificado(string matricula);
}
using AthenasAcademy.Services.Core.Requests;
using AthenasAcademy.Services.Domain.Responses;
using Refit;

namespace AthenasAcademy.Services.Core.Repositories.Clients;

public interface IGeradorCertificadoRepository
{
    [Post("")]
    Task<ApiResponse<NovoCertificadoPDFResponse>> GerarCertificadoPDF(
        [Body] NovoCertificadoRequest body,
        [Query] string token);
}
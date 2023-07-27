using AthenasAcademy.Services.Core.Requests;
using AthenasAcademy.Services.Domain.Responses;
using Refit;

namespace AthenasAcademy.Services.Core.Repositories.Clients;

/// <summary>
/// Interface responsável por definir o contrato para o repositório que se comunica com o serviço de geração de certificados em formato PDF.
/// </summary>
public interface IGeradorCertificadoRepository
{
    /// <summary>
    /// Envia uma solicitação para gerar um certificado em formato PDF com base nos dados fornecidos na requisição e um token de autenticação.
    /// </summary>
    /// <param name="body">Os dados da requisição para gerar o certificado, representados por um objeto do tipo <see cref="NovoCertificadoRequest"/>.</param>
    /// <param name="token">O token de autenticação usado para autorização na API.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona de envio da solicitação e que retorna um objeto do tipo <see cref="ApiResponse{NovoCertificadoPDFResponse}"/> contendo a resposta da API.</returns>
    [Post("/gerar-certificado")]
    Task<ApiResponse<NovoCertificadoPDFResponse>> GerarCertificadoPDF(
        [Body] NovoCertificadoRequest body,
        [Query] string token);
}
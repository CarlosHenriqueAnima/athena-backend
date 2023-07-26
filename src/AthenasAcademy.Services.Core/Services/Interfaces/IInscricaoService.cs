using AthenasAcademy.Services.Domain.Requests;
using AthenasAcademy.Services.Domain.Responses;

namespace AthenasAcademy.Services.Core.Services.Interfaces;

/// <summary>
/// Interface responsável por definir os métodos para o serviço de inscrição de candidatos.
/// </summary>
public interface IInscricaoService
{
    /// <summary>
    /// Realiza o cadastro de um novo candidato.
    /// </summary>
    /// <param name="request">Os dados do novo candidato a ser cadastrado.</param>
    /// <returns>Uma tarefa que representa o cadastro do candidato.</returns>
    Task<InscricaoCandidatoResponse> CadastrarCandidato(NovaInscricaoCandidatoRequest request);
}
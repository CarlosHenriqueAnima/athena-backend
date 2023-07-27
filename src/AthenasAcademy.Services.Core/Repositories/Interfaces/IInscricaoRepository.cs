using AthenasAcademy.Services.Core.Arguments;
using AthenasAcademy.Services.Core.Models;

namespace AthenasAcademy.Services.Core.Repositories.Interfaces;

/// <summary>
/// Interface responsável por definir o contrato para o repositório de inscrições de candidatos.
/// </summary>
public interface IInscricaoRepository
{
    /// <summary>
    /// Registra uma nova inscrição de candidato no sistema.
    /// </summary>
    /// <param name="argument">Os dados da inscrição do candidato a serem registrados, representados por um objeto do tipo <see cref="InscricaoCandidatoArgument"/>.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona de registro da inscrição do candidato, que retorna um objeto do tipo <see cref="InscricaoCandidatoModel"/>.</returns>
    Task<InscricaoCandidatoModel> RegistrarNovaInscricao(InscricaoCandidatoArgument argument);

    /// <summary>
    /// Obtém informações sobre uma inscrição de candidato com base no número da inscrição fornecido.
    /// </summary>
    /// <param name="inscricao">O número da inscrição do candidato a ser consultada.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona de obtenção das informações da inscrição do candidato, que retorna um objeto do tipo <see cref="InscricaoCandidatoModel"/>.</returns>
    Task<InscricaoCandidatoModel> ObterInscricao(int inscricao);
}
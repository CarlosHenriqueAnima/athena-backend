using AthenasAcademy.Services.Core.Arguments;
using AthenasAcademy.Services.Core.Models;

/// <summary>
/// Interface responsável por definir o repositório para o registro de inscrições de candidatos.
/// </summary>
public interface IInscricaoRepository
{
    /// <summary>
    /// Registra uma nova inscrição de candidato.
    /// </summary>
    /// <param name="argument">Os dados da inscrição do candidato a serem registrados.</param>
    /// <returns>Uma tarefa que representa o registro da inscrição do candidato.</returns>
    Task<InscricaoCandidatoModel> RegistrarNovaInscricao(InscricaoCandidatoArgument argument);
}

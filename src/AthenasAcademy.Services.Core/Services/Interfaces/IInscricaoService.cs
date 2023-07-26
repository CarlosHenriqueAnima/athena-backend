using AthenasAcademy.Services.Core.Models;
<<<<<<< HEAD
using AthenasAcademy.Services.Domain.Requests;
using AthenasAcademy.Services.Domain.Responses;
=======

namespace AthenasAcademy.Services.Core.Services.Interfaces;
>>>>>>> dev


namespace AthenasAcademy.Services.Core.Services.Interfaces
{
<<<<<<< HEAD
    public interface IInscricaoService
    {
        /// <summary>
        /// Obtém uma inscrição pelo ID.
        /// </summary>
        /// <param name="id">ID da inscrição a ser obtida.</param>
        /// <returns>Objeto contendo informações da inscrição.</returns>
        Task<InscricaoResponse> ObterInscricao(int id);

        /// <summary>
        /// Obtém todas as inscrições cadastradas.
        /// </summary>
        /// <returns>Lista de inscrições cadastradas.</returns>
        Task<IEnumerable<InscricaoResponse>> ObterTodasInscricoes();

        /// <summary>
        /// Cadastra uma nova inscrição.
        /// </summary>
        /// <param name="request">Objeto contendo os dados da nova inscrição.</param>
        /// <returns>Objeto contendo informações da inscrição cadastrada.</returns>
        Task<NovaInscricaoResponse> CadastrarInscricao(NovaInscricaoRequest request);

        /// <summary>
        /// Atualiza os dados de uma inscrição existente.
        /// </summary>
        /// <param name="request">Objeto contendo os novos dados da inscrição.</param>
        /// <returns>Objeto contendo informações da inscrição atualizada.</returns>
        Task<InscricaoResponse> AtualizarInscricao(NovaInscricaoRequest request);

        /// <summary>
        /// Desativa uma inscrição existente.
        /// </summary>
        /// <param name="id">ID da inscrição a ser desativada.</param>
        /// <returns>Indicação de sucesso na desativação da inscrição.</returns>
        Task<bool> DesativarInscricao(int id);
        Task CadastrarInscricao(InscricaoModel inscricao);
        Task AtualizarInscricao(InscricaoModel inscricao);
    }
=======
    Task<CandidatoModel> ObterInscricaoPorIdAsync(int id);
    Task<IEnumerable<CandidatoModel>> ObterInscricoesPendentesAsync();
    Task AdicionarInscricaoAsync(CandidatoModel inscricao);
    Task AtualizarInscricaoAsync(CandidatoModel inscricao);
    Task CancelarInscricaoAsync(int id);
>>>>>>> dev
}

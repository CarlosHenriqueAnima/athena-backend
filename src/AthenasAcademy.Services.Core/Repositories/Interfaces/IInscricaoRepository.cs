using AthenasAcademy.Services.Core.Models;

namespace AthenasAcademy.Services.Core.Repositories.Interfaces;

public interface IInscricaoRepository
{
    Task<CandidatoModel> ObterInscricaoPorIdAsync(int id);
    Task<IEnumerable<CandidatoModel>> ObterInscricoesPendentesAsync();
    Task AdicionarInscricaoAsync(CandidatoModel inscricao);
    Task AtualizarInscricaoAsync(CandidatoModel inscricao);
    Task CancelarInscricaoAsync(int id);
}
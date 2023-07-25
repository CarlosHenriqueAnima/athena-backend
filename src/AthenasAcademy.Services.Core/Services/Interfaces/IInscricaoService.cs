using AthenasAcademy.Services.Core.Models;

namespace AthenasAcademy.Services.Core.Services.Interfaces;

public interface IInscricaoService
{
    Task<CandidatoModel> ObterInscricaoPorIdAsync(int id);
    Task<IEnumerable<CandidatoModel>> ObterInscricoesPendentesAsync();
    Task AdicionarInscricaoAsync(CandidatoModel inscricao);
    Task AtualizarInscricaoAsync(CandidatoModel inscricao);
    Task CancelarInscricaoAsync(int id);
}

using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Repositories.Interfaces;
using AthenasAcademy.Services.Core.Services.Interfaces;

namespace AthenasAcademy.Services.Core.Services;

public class InscricaoService : IInscricaoService
{
    private readonly IInscricaoRepository _inscricaoRepository;

    public InscricaoService(IInscricaoRepository inscricaoRepository)
    {
        _inscricaoRepository = inscricaoRepository;
    }

    public async Task<CandidatoModel> ObterInscricaoPorIdAsync(int id)
    {
        return await _inscricaoRepository.ObterInscricaoPorIdAsync(id);
    }

    public async Task<IEnumerable<CandidatoModel>> ObterInscricoesPendentesAsync()
    {
        return await _inscricaoRepository.ObterTodasInscricoesAsync();
    }

    public async Task AdicionarInscricaoAsync(CandidatoModel inscricao)
    {
        if (inscricao == null)
        {
            throw new ArgumentNullException(nameof(inscricao), "A inscrição não pode ser nula.");
        }

        await _inscricaoRepository.AdicionarInscricaoAsync(inscricao);
    }

    public async Task AtualizarInscricaoAsync(CandidatoModel inscricao)
    {
        if (inscricao == null)
        {
            throw new ArgumentNullException(nameof(inscricao), "A inscrição não pode ser nula.");
        }

        await _inscricaoRepository.AtualizarInscricaoAsync(inscricao);
    }

    public async Task CancelarInscricaoAsync(int id)
    {
        await _inscricaoRepository.CancelarInscricaoAsync(id);
    }
}

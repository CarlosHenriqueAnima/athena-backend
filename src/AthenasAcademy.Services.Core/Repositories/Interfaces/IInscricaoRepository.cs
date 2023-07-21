using AthenasAcademy.Services.Domain.Requests;
using AthenasAcademy.Services.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AthenasAcademy.Services.Core.Repositories.Interfaces
{
    public interface IInscricaoRepository
    {
        Task<InscricaoResponse> ObterInscricaoPorIdAsync(int id);

        Task<IEnumerable<InscricaoResponse>> ObterTodasInscricoesAsync();

        Task<InscricaoResponse> CadastrarInscricaoAsync(NovaInscricaoRequest request);

        Task<InscricaoResponse> AtualizarInscricaoAsync(AtualizarInscricaoRequest request);

        Task<bool> RemoverInscricaoAsync(int id);
    }
}

using AthenasAcademy.Services.Core.Configurations.Enums;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Repositories.Interfaces;
using AthenasAcademy.Services.Core.Repositories.Interfaces.Base;
using AthenasAcademy.Services.Domain.Requests;
using AthenasAcademy.Services.Domain.Responses;
using System.Data;

namespace AthenasAcademy.Services.Core.Repositories
{
    public class InscricaoRepository : BaseRepository, IInscricaoRepository
    {
        public InscricaoRepository(IConfiguration configuration) : base(configuration) { }

        #region Inscricao

        Task<InscricaoResponse> IInscricaoRepository.ObterInscricaoPorIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<InscricaoResponse>> IInscricaoRepository.ObterTodasInscricoesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<InscricaoResponse> CadastrarInscricaoAsync(NovaInscricaoRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<InscricaoResponse> AtualizarInscricaoAsync(AtualizarInscricaoRequest request)
        {
            throw new NotImplementedException();
        }

        Task<bool> IInscricaoRepository.RemoverInscricaoAsync(int id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Repositories.Interfaces;
using AthenasAcademy.Services.Core.Services.Interfaces;
using AthenasAcademy.Services.Domain.Requests;
using AthenasAcademy.Services.Domain.Responses;

namespace AthenasAcademy.Services.Core.Services
{
    public class InscricaoService : IInscricaoService
    {
        public Task<Interfaces.InscricaoResponse> AtualizarInscricao(NovaInscricaoRequest request)
        {
            throw new NotImplementedException();
        }

        public Task AtualizarInscricao(InscricaoModel inscricao)
        {
            throw new NotImplementedException();
        }

        public Task<NovaInscricaoResponse> CadastrarInscricao(NovaInscricaoRequest request)
        {
            throw new NotImplementedException();
        }

        public Task CadastrarInscricao(InscricaoModel inscricao)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DesativarInscricao(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Interfaces.InscricaoResponse> ObterInscricao(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Interfaces.InscricaoResponse>> ObterTodasInscricoes()
        {
            throw new NotImplementedException();
        }
    }
}

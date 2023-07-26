<<<<<<< HEAD
﻿using AthenasAcademy.Services.Domain.Requests;
using AthenasAcademy.Services.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;
=======
﻿using AthenasAcademy.Services.Core.Models;

namespace AthenasAcademy.Services.Core.Repositories.Interfaces;
>>>>>>> dev

namespace AthenasAcademy.Services.Core.Repositories.Interfaces
{
<<<<<<< HEAD
    public interface IInscricaoRepository
    {
        Task<InscricaoResponse> ObterInscricaoPorIdAsync(int id);

        Task<IEnumerable<InscricaoResponse>> ObterTodasInscricoesAsync();

        Task<InscricaoResponse> CadastrarInscricaoAsync(NovaInscricaoRequest request);

        Task<InscricaoResponse> AtualizarInscricaoAsync(AtualizarInscricaoRequest request);

        Task<bool> RemoverInscricaoAsync(int id);
    }
}
=======
    Task<CandidatoModel> ObterInscricaoPorIdAsync(int id);
    Task<IEnumerable<CandidatoModel>> ObterInscricoesPendentesAsync();
    Task AdicionarInscricaoAsync(CandidatoModel inscricao);
    Task AtualizarInscricaoAsync(CandidatoModel inscricao);
    Task CancelarInscricaoAsync(int id);
}
>>>>>>> dev

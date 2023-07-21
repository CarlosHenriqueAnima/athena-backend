using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Domain.Requests;
using AthenasAcademy.Services.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AthenasAcademy.Services.Core.Services.Interfaces
{
    public interface IMatriculaService
    {
        Task<MatriculaResponse> ObterMatricula(int id);
        Task<IEnumerable<MatriculaResponse>> ObterTodasMatriculas();
        Task<NovaMatriculaResponse> CadastrarMatricula(NovaMatriculaRequest request);
        Task<MatriculaResponse> AtualizarMatricula(NovaMatriculaRequest request);
        Task<bool> DesativarMatricula(int id);
        Task CadastrarMatricula(MatriculaModel matricula);
        Task AtualizarMatricula(MatriculaModel matricula);
    }
}

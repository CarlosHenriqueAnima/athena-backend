using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Domain.Requests;
using AthenasAcademy.Services.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AthenasAcademy.Services.Core.Services.Interfaces
{
<<<<<<< HEAD
    
        // Métodos relacionados a matrículas
        public interface IMatriculaService
        {
        Task<MatriculaResponse> ObterMatricula(int id);
        Task<IEnumerable<MatriculaResponse>> ObterTodasMatriculas();
        Task<NovaMatriculaResponse> CadastrarMatricula(MatriculaModel matricula);
        Task<MatriculaResponse> AtualizarMatricula(MatriculaModel matricula);
        Task<bool> DesativarMatricula(int id);
    }

     
    }

=======
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
>>>>>>> 7f24942145c284dece18f132624604ac986811b1

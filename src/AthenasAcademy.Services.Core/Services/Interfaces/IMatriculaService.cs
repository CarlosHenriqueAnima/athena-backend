using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Domain.Requests;
using AthenasAcademy.Services.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AthenasAcademy.Services.Core.Services.Interfaces
{

    
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

using AthenasAcademy.Services.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AthenasAcademy.Services.Core.Repositories.Interfaces
{
    public interface IMatriculaRepository
    {

     
         Task<MatriculaModel> ObterMatriculaPorIdAsync(int id);
         Task<IEnumerable<MatriculaModel>> ObterTodasMatriculasAsync();
         Task AdicionarMatriculaAsync(MatriculaModel matricula);
         Task AtualizarMatriculaAsync(MatriculaModel matricula);
         Task RemoverMatriculaAsync(int id);
       

    }
}

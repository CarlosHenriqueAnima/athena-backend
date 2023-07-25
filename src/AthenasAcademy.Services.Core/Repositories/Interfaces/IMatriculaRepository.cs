using AthenasAcademy.Services.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AthenasAcademy.Services.Core.Repositories.Interfaces
{
    public interface IMatriculaRepository
    {
<<<<<<< HEAD
     
         Task<MatriculaModel> ObterMatriculaPorIdAsync(int id);
         Task<IEnumerable<MatriculaModel>> ObterTodasMatriculasAsync();
         Task AdicionarMatriculaAsync(MatriculaModel matricula);
         Task AtualizarMatriculaAsync(MatriculaModel matricula);
         Task RemoverMatriculaAsync(int id);
       
=======
        // Declare os métodos para acessar e manipular os dados
        // de matrícula no banco de dados.
        // Exemplo:
        // Task<MatriculaModel> ObterMatriculaPorIdAsync(int id);
        // Task<IEnumerable<MatriculaModel>> ObterTodasMatriculasAsync();
        // Task AdicionarMatriculaAsync(MatriculaModel matricula);
        // Task AtualizarMatriculaAsync(MatriculaModel matricula);
        // Task RemoverMatriculaAsync(int id);
>>>>>>> 7f24942145c284dece18f132624604ac986811b1
    }
}

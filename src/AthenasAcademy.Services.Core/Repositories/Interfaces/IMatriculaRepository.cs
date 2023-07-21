using AthenasAcademy.Services.Core.Models;

namespace AthenasAcademy.Services.Core.Repositories.Interfaces;

public interface IMatriculaRepository
{
    Task<AlunoDetalhesModel> ObterMatriculaPorId(int id);
    Task<IEnumerable<AlunoDetalhesModel>> ObterTodasMatriculas();
    Task AdicionarMatricula(AlunoDetalhesModel matricula);
    Task AtualizarMatricula(AlunoDetalhesModel matricula);
    Task CancelarMatricula(int id);
}
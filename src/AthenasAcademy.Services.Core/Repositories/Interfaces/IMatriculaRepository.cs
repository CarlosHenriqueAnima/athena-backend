using AthenasAcademy.Services.Core.Models;

namespace AthenasAcademy.Services.Core.Repositories.Interfaces;

public interface IMatriculaRepository
{
    Task<AlunoDetalhesModel> ObterMatriculaPorIdAsync(int matriculaId);
    Task<IEnumerable<AlunoDetalhesModel>> ObterTodasMatriculasAsync();
    Task AdicionarMatriculaAsync(AlunoDetalhesModel matricula);
    Task AtualizarMatriculaAsync(AlunoDetalhesModel matricula);
    Task CancelarMatriculaAsync(int matriculaId);
}
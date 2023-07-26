using AthenasAcademy.Services.Core.CrossCutting;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Repositories.Interfaces;
using AthenasAcademy.Services.Core.Repositories.Interfaces.Base;

namespace AthenasAcademy.Services.Core.Repositories;

public class MatriculaRepository : BaseRepository, IMatriculaRepository
{
    public MatriculaRepository(IConfiguration configuration) : base(configuration) { }

    public Task<MatriculaAlunoModel> AtivarMatricula(FichaAluno fichaAluno)
    {
        throw new NotImplementedException();
    }

    public Task<ContratoMatriculaAlunoModel> GerarContratoMatricula(FichaAluno fichaAluno)
    {
        throw new NotImplementedException();
    }

    public Task<MatriculaAlunoModel> GerarPreMatricula(FichaAluno fichaAluno)
    {
        throw new NotImplementedException();
    }
}
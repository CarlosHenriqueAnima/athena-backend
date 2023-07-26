using AthenasAcademy.Services.Core.CrossCutting;
using AthenasAcademy.Services.Core.Models;

namespace AthenasAcademy.Services.Core.Repositories.Interfaces;

public interface IMatriculaRepository
{
    Task<MatriculaAlunoModel> GerarPreMatricula(FichaAluno fichaAluno);

    Task<MatriculaAlunoModel> AtivarMatricula(FichaAluno fichaAluno);

    Task<ContratoMatriculaAlunoModel> GerarContratoMatricula(FichaAluno fichaAluno);
}
using AthenasAcademy.Services.Core.CrossCutting;
using AthenasAcademy.Services.Core.Models;

namespace AthenasAcademy.Services.Core.Repositories.Interfaces;

public interface IMatriculaRepository
{
    Task<MatriculaModel> GerarPreMatricula(FichaAluno fichaAluno);

    Task<MatriculaModel> AtivarMatricula(FichaAluno fichaAluno);

    Task<MatriculaModel> ObterMatricula(int matricula);

    Task<ContratoMatriculaAlunoModel> AssinarContratoMatricula(FichaAluno fichaAluno);
}
using System.Collections.Generic;
using AthenasAcademy.Services.Core.Models;
namespace AthenasAcademy.Services.Core.Repositories.Interfaces;

public interface IMatriculaRepository
{
    MatriculaModel GetMatriculaById(int id);
    IEnumerable<MatriculaModel> GetAllMatriculas();
    void CreateMatricula(MatriculaModel matricula);
    void UpdateMatricula(MatriculaModel matricula);
    void DeleteMatricula(int id);
}
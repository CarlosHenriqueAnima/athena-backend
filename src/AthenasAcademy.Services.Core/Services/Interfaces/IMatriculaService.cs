using System.Collections.Generic;
using AthenasAcademy.Services.Core.Models;

namespace AthenasAcademy.Services.Core.Services.Interfaces
{

    MatriculaModel CreateMatricula(int contratoId, int detalheContratoId, string codigoMatricula);
    MatriculaModel GetMatriculaById(int id);
    IEnumerable<MatriculaModel> GetAllMatriculas();
    void UpdateMatricula(MatriculaModel matricula);
    void DeleteMatricula(int id);
}


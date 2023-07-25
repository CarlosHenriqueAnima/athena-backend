using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Domain.Requests;

namespace AthenasAcademy.Services.Core.Helpers.Mappers
{
    public static class MatriculaMapper
    {
        public static MatriculaModel ToMatriculaModel(this MatriculaRequest request)
        {
            return new MatriculaModel
            {
                AlunoId = request.AlunoId,
                DataMatricula = DateTime.Now, // Defina a data de matrícula como a data atual
                Curso = request.Curso // Defina o curso com base na solicitação
            };
        }
    }
}

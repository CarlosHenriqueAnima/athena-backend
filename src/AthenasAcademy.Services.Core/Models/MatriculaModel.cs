namespace AthenasAcademy.Services.Core.Models
{
    public class MatriculaModel
        {
            public int Id { get; set; }
            public int AlunoId { get; set; }
            public DateTime DataMatricula { get; set; }
            public string Curso { get; set; }
        }
}



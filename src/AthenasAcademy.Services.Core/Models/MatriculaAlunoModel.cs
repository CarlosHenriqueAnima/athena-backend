namespace AthenasAcademy.Services.Core.Models;

public class MatriculaAlunoModel
{
    public int Id { get; set; }
    public int Matricula { get; set; }
    public bool Ativa { get; set; }
    public int CodigoAluno { get; set; }
    public string NomeAluno { get; set; }
    public DateTime? DataMatricula { get; set; }
}
namespace AthenasAcademy.Services.Core.Models;

public class DetalheAlunoModel
{
    public int Id { get; set; }
    public int IdAluno { get; set; }
    public int CodigoUsuario { get; set; }
    public DateTime DataUsuario { get; set; }
    public int CodigoInscricao { get; set; }
    public DateTime DataInscricao { get; set; }
    public int CodigoCurso { get; set; }
    public int? CodigoMatricula { get; set; }
    public DateTime? DataMatricula { get; set; }
    public int? CodigoContrato { get; set; }
    public DateTime? DataContrato { get; set; }
}
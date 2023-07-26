namespace AthenasAcademy.Services.Core.Models;

public class DetalheAlunoModel
{
    public int Id { get; set; }
    public int IdAluno { get; set; }
    public string CodigoUsuario { get; set; }
    public DateTime DataUsuario { get; set; }
    public string CodigoInscricao { get; set; }
    public DateTime DataInscricao { get; set; }
    public string CodigoMatricula { get; set; }
    public DateTime? DataMatricula { get; set; }
    public DateTime? DataContrato { get; set; }
    public string CodigoContrato { get; set; }
}
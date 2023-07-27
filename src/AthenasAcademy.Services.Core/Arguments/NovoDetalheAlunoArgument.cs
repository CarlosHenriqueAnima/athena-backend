namespace AthenasAcademy.Services.Core.Arguments;

public class NovoDetalheAlunoArgument
{
    public int IdAluno { get; set; }
    public int? CodigoUsuario { get; set; }
    public DateTime DataUsuario { get; set; }
    public int CodigoInscricao { get; set; }
    public DateTime DataInscricao { get; set; }
    public int CodigoMatricula { get; set; }
    public DateTime? DataMatricula { get; set; }
    public DateTime? DataContrato { get; set; }
    public int? CodigoContrato { get; set; }
    public int? CodigoCurso { get; set; }
}
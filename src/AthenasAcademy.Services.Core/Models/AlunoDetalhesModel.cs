namespace AthenasAcademy.Services.Core.Models;

public class AlunoDetalhesModel
{
    public int Id { get; set; }
    public int AlunoId { get; set; }
    public string CodigoContrato { get; set; }
    public string CodigoInscricao { get; set; }
    public DateTime? DataMatricula { get; set; }
    public string CodigoMatricula { get; set; }
    public string CodigoUsuario { get; set; }
}

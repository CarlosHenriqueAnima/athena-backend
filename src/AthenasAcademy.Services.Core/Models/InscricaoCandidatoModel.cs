namespace AthenasAcademy.Services.Core.Models;

public class InscricaoCandidatoModel
{
    public int Id { get; set; }
    public int CodigoInscricao { get; set; }
    public DateTime DataInscricao { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public string CodigoCurso { get; set; }
    public string NomeCurso { get; set; }
    public string Boleto { get; set; }
    public bool BoletoPago { get; set; }
}
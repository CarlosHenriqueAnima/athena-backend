namespace AthenasAcademy.Services.Core.Arguments;

public class InscricaoCandidatoArgument
{
    public string Nome { get; set; }

    public string Email { get; set; }

    public string Telefone { get; set; }

    public int CodigoCurso { get; set; }

    public string NomeCurso { get; set; }

    public bool BoletoPago => false;

    public DateTime DataInscricao => DateTime.Now;
}
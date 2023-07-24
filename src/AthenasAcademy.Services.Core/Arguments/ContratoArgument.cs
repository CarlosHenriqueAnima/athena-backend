namespace AthenasAcademy.Services.Core.Arguments;

public class ContratoArgument
{
    public int Id { get; set; }
    public string Contrato { get; set; }
    public int DetalheContratoId { get; set; }
    public bool Ativo { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime? DataAlteracao { get; set; }

    public DetalheContratoArgument DetalheContrato { get; set; }
}
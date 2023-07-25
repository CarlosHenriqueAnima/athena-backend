namespace AthenasAcademy.Services.Core.Arguments;

public class DetalheContratoArgument
{
    public int Id { get; set; }

    public string CodigoAluno { get; set; }

    public string CodigoCurso { get; set; }

    public string CodigoFormaPagamento { get; set; }

    public DateTime DataVencimentoPagamento { get; set; }

    public IEnumerable<ContratoArgument> Contratos { get; set; }
}
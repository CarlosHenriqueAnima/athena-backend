namespace AthenasAcademy.Services.Core.Models;

public class DetalheContratoModel
{
    public int Id { get; set; }

    public string CodigoAluno { get; set; }

    public string CodigoCurso { get; set; }

    public string CodigoFormaPagamento { get; set; }

    public DateTime DataVencimentoPagamento { get; set; }

    public IEnumerable<ContratoModel> Contratos { get; set; }
}
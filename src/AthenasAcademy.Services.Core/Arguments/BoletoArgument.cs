namespace AthenasAcademy.Services.Core.Arguments;

public class BoletoArgument
{
    public int Id { get; set; }

    public decimal Valor { get; set; }

    public string Banco { get; set; }

    public DateTime Vencimento { get; set; }

    public bool Ativo { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public ICollection<BoletoPagamentoArgument> Pagamentos { get; set; }
}

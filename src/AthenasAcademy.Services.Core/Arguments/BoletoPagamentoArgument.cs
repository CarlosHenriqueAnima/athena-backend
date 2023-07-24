namespace AthenasAcademy.Services.Core.Arguments;

public class BoletoPagamentoArgument
{
    public int Id { get; set; }

    public int BoletoId { get; set; }

    public bool Pago { get; set; }

    public DateTime? DataPagamento { get; set; }

    public decimal? ValorPago { get; set; }

    public BoletoArgument Boleto { get; set; }
}
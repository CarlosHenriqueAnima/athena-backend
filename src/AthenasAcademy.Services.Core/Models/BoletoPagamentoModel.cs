namespace AthenasAcademy.Services.Core.Models;

public class BoletoPagamentoModel
{
    public int Id { get; set; }

    public int BoletoId { get; set; }

    public bool Pago { get; set; }

    public DateTime? DataPagamento { get; set; }

    public decimal? ValorPago { get; set; }

    public BoletoModel Boleto { get; set; }
}
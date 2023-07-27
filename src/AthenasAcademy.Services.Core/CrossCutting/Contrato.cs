namespace AthenasAcademy.Services.Core.CrossCutting;

public class Contrato
{
    public int NumeroContrato { get; set; }

    public int Matricula { get; set; }

    public bool Assinado { get; set; }

    public string FormaPagamento => "Boleto";

    public decimal ValorContrato => new Random().Next(100, 200);
}
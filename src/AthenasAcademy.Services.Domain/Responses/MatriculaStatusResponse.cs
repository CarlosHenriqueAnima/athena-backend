namespace AthenasAcademy.Services.Domain.Responses;

public class MatriculaStatusResponse
{
    public int Matricula { get; set; }

    public int Contrato { get; set; }

    public bool BoletoPago { get; set; }

    public bool ContratoAssinado { get; set; }

    public string Mensagem =>
        this.ContratoAssinado && this.BoletoPago ? "Matrícula Ativa" :
        (this.ContratoAssinado && !this.BoletoPago) ? "Matrícula Pendente: Pagamento Boleto" :
        (!this.ContratoAssinado && this.BoletoPago) ? "Matrícula Pendente: Assinatura Contrato" :
        "Matrícula Pendente: Pagamento Boleto | Assinatura Contrato";
}

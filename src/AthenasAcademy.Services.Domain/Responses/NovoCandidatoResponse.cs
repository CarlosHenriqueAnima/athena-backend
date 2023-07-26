namespace AthenasAcademy.Services.Domain.Responses;

public class InscricaoCandidatoResponse
{
    public int Inscricao { get; set; }

    public string MensagemGeracaoBoleto => "Boleto em Processamento.";

    public string MensagemGeracaoContrato => "Contrato em Processamento.";
}
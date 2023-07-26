namespace AthenasAcademy.Services.Domain.Responses;

public class InscricaoCandidatoResponse
{
    public int Inscricao { get; set; }

    public static string MensagemGeracaoBoleto => "Boleto em Processamento.";

    public static string MensagemGeracaoContrato => "Contrato em Processamento.";
}
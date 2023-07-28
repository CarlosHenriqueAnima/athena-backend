namespace AthenasAcademy.Services.Domain.Responses;

public class InscricaoCandidatoResponse
{
    public int Inscricao { get; set; }

    public int Matricula { get; set; }

    public int Contrato { get; set; }

    public string MensagemGeracaoBoleto { get; set; }  = "Boleto em Processamento.";

    public string MensagemGeracaoContrato { get; set; }  = "Contrato em Processamento.";
}
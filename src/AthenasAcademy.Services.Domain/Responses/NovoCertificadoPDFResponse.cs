namespace AthenasAcademy.Services.Domain.Responses;

public class NovoCertificadoPDFResponse
{
    public string NomeArquivo { get; set; }

    public string CaminhoArquivo { get; set; }

    public byte[] PDFArquivo { get; set; }

    public string UriDownload { get; set; }
}
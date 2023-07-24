namespace AthenasAcademy.Services.Domain.Responses;

public class TokenResponse
{
    public string Token { get; set; }

    public DateTime Validade { get; set; }

    public string Menssagem { get; set; }
}
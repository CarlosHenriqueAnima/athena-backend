namespace AthenasAcademy.Services.Domain.Responses;

public class LoginUsuarioResponse
{
    public bool Resultado { get; set; }

    public TokenResponse Token { get; set; }

    public DadosUsuarioResponse DadosUsuario { get; set; }
}
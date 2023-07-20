using AthenasAcademy.Services.Domain.Configurations.DTO;

namespace AthenasAcademy.Services.Domain.Responses;

public class LoginUsuarioResponse
{
    public bool Resultado { get; set; }

    public TokenDTO Token { get; set; }
}
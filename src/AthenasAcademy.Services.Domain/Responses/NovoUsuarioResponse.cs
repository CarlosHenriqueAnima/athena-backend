using AthenasAcademy.Services.Domain.Configurations.DTO;

namespace AthenasAcademy.Services.Domain.Responses;

public class NovoUsuarioResponse
{
    public bool Resultado { get; set; }

    public TokenDTO Token { get; set; }
}
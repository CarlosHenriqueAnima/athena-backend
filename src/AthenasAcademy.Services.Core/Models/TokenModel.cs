using AthenasAcademy.Services.Domain.Configurations.DTO;

namespace AthenasAcademy.Services.Core.Models;

public class TokenModel : TokenDTO
{
    public DateTime Validade { get; set; }
}
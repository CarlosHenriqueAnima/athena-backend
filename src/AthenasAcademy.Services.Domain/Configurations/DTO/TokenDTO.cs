namespace AthenasAcademy.Services.Domain.Configurations.DTO;

public class TokenDTO
{
    public bool Atenticado { get; set; }
    public DateTime Expira { get; set; }
    public string Token { get; set; }
    public string Menssagem { get; set; }
}
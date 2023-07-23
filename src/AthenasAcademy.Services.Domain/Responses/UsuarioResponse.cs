namespace AthenasAcademy.Services.Domain.Responses;

public class UsuarioResponse
{
    public string Usuario { get; set; }

    public string Perfil { get; set; }
    
    public DateTime DataCadastro { get; set; }
    
    public bool Ativo { get; set; }
}
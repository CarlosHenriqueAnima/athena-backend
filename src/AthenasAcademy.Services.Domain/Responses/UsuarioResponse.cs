namespace AthenasAcademy.Services.Domain.Responses;

public class UsuarioResponse
{
    public string Usuario { get; set; }

    public string Perfil { get; set; }
    
    public int DataCadastro { get; set; }
    
    public int Ativo { get; set; }
}
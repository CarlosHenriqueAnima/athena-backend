namespace AthenasAcademy.Services.Core.Arguments;

public class NovoUsuarioArgument
{
    public string Usuario { get; set; }

    public string Email { get; set; }

    public string Senha { get; set; }

    public bool Ativo { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }
}
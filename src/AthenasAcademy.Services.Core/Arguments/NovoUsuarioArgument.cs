namespace AthenasAcademy.Services.Core.Arguments;

public class NovoUsuarioArgument
{
    public string Usuario { get; set; }

    public string Email { get; set; }

    public string Senha { get; set; }

    public bool Ativo { get; set; } = true;

    public DateTime DataCadastro { get; set; } = DateTime.Now;

    public DateTime DataAlteracao { get; set; } = DateTime.Now;
}
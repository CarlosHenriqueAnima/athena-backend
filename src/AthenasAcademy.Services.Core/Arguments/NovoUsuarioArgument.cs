namespace AthenasAcademy.Services.Core.Arguments;

public class NovoUsuarioArgument
{
    public int Usuario { get; set; }

    public int Email { get; set; }

    public int Senha { get; set; }

    public bool Ativo { get; set; } = true;

    public DateTime DataCadastro { get; set; } = DateTime.Now;

    public DateTime DataAlteracao { get; set; } = DateTime.Now;
}
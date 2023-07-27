namespace AthenasAcademy.Services.Core.Arguments;

public class NovoAlunoArgument
{
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public string CPF { get; set; }
    public char Sexo { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Email { get; set; }
    public bool Ativo => false;
    public DateTime DataCadastro => DateTime.Now;
}
namespace AthenasAcademy.Services.Core.Arguments;

public class NovoEnderecoAlunoArgument
{
    public int IdAluno { get; set; }
    public string Logradouro { get; set; }
    public string Numero { get; set; }
    public string Complemento { get; set; }
    public string Bairro { get; set; }
    public string Localidade { get; set; }
    public string UF { get; set; }
    public string CEP { get; set; }
    public bool Ativo => true;
    public DateTime DataCadastro => DateTime.Now;
}
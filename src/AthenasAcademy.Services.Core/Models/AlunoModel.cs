namespace AthenasAcademy.Services.Core.Models;

public class AlunoModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public string Sexo { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Email { get; set; }
    public DateTime DataInscricao { get; set; }
    public bool Ativo { get; set; }
}

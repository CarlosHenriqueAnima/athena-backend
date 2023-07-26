using AthenasAcademy.Services.Core.Models;

namespace AthenasAcademy.Services.Core.CrossCutting;

public class FichaAluno
{
    public AlunoModel Aluno { get; set; }

    public EnderecoAlunoModel Endereco { get; set; }

    public TelefoneAlunoModel Telefone { get; set; }

    public DetalheAlunoModel DetalhesFicha { get; set; }

    public OpcaoCurso OpcaoCurso { get; set; }
}
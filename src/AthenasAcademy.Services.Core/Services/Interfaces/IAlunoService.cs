using AthenasAcademy.Services.Core.Arguments;
using AthenasAcademy.Services.Core.Models;

namespace AthenasAcademy.Services.Core.Services.Interfaces;

public interface IAlunoService
{
    Task<AlunoModel> ObterAluno(int id);

    Task<AlunoModel> CadastrarAluno(NovoAlunoArgument aluno);

    Task<EnderecoAlunoModel> CadastrarEnderecoAluno(NovoEnderecoAlunoArgument endereco);

    Task<TelefoneAlunoModel> CadastrarTelefoneAluno(NovoTelefoneAlunoArgument telefone);

    Task<DetalheAlunoModel> CadastrarDetalheAluno(NovoDetalheAlunoArgument detalhe);
}
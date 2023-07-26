using AthenasAcademy.Services.Core.Arguments;
using AthenasAcademy.Services.Core.Models;

namespace AthenasAcademy.Services.Core.Repositories.Interfaces;

public interface IAlunoRepository
{
    Task<AlunoModel> ObterAluno(int id);

    Task<AlunoModel> CadastrarAluno(NovoAlunoArgument aluno);

    Task<EnderecoAlunoModel> CadastrarEnderecoAluno(NovoEnderecoAlunoArgument endereco);

    Task<TelefoneAlunoModel> CadastrarTelefoneAluno(NovoTelefoneAlunoArgument telefone);

    Task<DetalheAlunoModel> CadastrarDetalheAluno(NovoDetalheAlunoArgument detalhe);
}
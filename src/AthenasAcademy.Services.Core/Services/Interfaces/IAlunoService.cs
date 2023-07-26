using AthenasAcademy.Services.Core.Arguments;
using AthenasAcademy.Services.Core.Models;

/// <summary>
/// Interface responsável por definir os métodos de serviço para manipulação de alunos.
/// </summary>
public interface IAlunoService
{
    /// <summary>
    /// Obtém um aluno pelo seu ID.
    /// </summary>
    /// <param name="id">O ID do aluno a ser obtido.</param>
    /// <returns>Uma tarefa que representa a operação de obtenção do aluno.</returns>
    Task<AlunoModel> ObterAluno(int id);

    /// <summary>
    /// Cadastra um novo aluno.
    /// </summary>
    /// <param name="aluno">Os dados do novo aluno a ser cadastrado.</param>
    /// <returns>Uma tarefa que representa o cadastro do aluno.</returns>
    Task<AlunoModel> CadastrarAluno(NovoAlunoArgument aluno);

    /// <summary>
    /// Cadastra o endereço de um aluno.
    /// </summary>
    /// <param name="endereco">Os dados do endereço do aluno a ser cadastrado.</param>
    /// <returns>Uma tarefa que representa o cadastro do endereço do aluno.</returns>
    Task<EnderecoAlunoModel> CadastrarEnderecoAluno(NovoEnderecoAlunoArgument endereco);

    /// <summary>
    /// Cadastra o telefone de um aluno.
    /// </summary>
    /// <param name="telefone">Os dados do telefone do aluno a ser cadastrado.</param>
    /// <returns>Uma tarefa que representa o cadastro do telefone do aluno.</returns>
    Task<TelefoneAlunoModel> CadastrarTelefoneAluno(NovoTelefoneAlunoArgument telefone);

    /// <summary>
    /// Cadastra detalhes de um aluno.
    /// </summary>
    /// <param name="detalhe">Os dados dos detalhes do aluno a serem cadastrados.</param>
    /// <returns>Uma tarefa que representa o cadastro dos detalhes do aluno.</returns>
    Task<DetalheAlunoModel> CadastrarDetalheAluno(NovoDetalheAlunoArgument detalhe);
}

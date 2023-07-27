using AthenasAcademy.Services.Core.Arguments;
using AthenasAcademy.Services.Core.CrossCutting;
using AthenasAcademy.Services.Core.Models;

namespace AthenasAcademy.Services.Core.Repositories.Interfaces;

/// <summary>
/// Interface responsável por definir os métodos para o repositório de alunos.
/// </summary>
public interface IAlunoRepository
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


    /// <summary>
    /// Obtém detalhes de um aluno com base em um critério específico.
    /// </summary>
    /// <param name="argumento">O critério de busca para detalhes do aluno (por exemplo: "codigo_matricula" ou "codigo_contrato").</param>
    /// <param name="valor">O valor correspondente ao critério de busca.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona de obtenção dos detalhes do aluno.</returns>
    /// <remarks>
    /// Este método permite obter detalhes do aluno com base em um critério específico, como "codigo_matricula" ou "codigo_contrato".
    /// O parâmetro "argumento" especifica qual critério será usado na busca, e o parâmetro "valor" é o valor correspondente ao critério.
    /// </remarks>
    Task<DetalheAlunoArgumentoModel> ObterDetalheAluno(string argumento, int valor);

    /// <summary>
    /// Obtém a ficha completa de um aluno com base no número de inscrição.
    /// </summary>
    /// <param name="inscricao">O número de inscrição do aluno.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona de obter a ficha do aluno.</returns>
    /// <remarks>
    /// A ficha do aluno inclui informações detalhadas do aluno, como seus dados pessoais, endereço, telefones e outros detalhes cadastrais.
    /// O número de inscrição é um identificador único para cada aluno e é utilizado para recuperar suas informações completas.
    /// Caso não seja possível localizar o aluno com o número de inscrição fornecido, a tarefa retornará nulo.
    /// </remarks>
    Task<FichaAluno> ObterFichaAluno(int inscricao);
}

using AthenasAcademy.Services.Core.Arguments;
using AthenasAcademy.Services.Core.CrossCutting;
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
    /// Obtém os detalhes de um aluno com base em um dos seguintes critérios: ID, número de inscrição ou número de matrícula.
    /// </summary>
    /// <param name="id">O ID do aluno (opcional).</param>
    /// <param name="inscricao">O número de inscrição do aluno (opcional).</param>
    /// <param name="matricula">O número de matrícula do aluno (opcional).</param>
    /// <returns>Uma tarefa que representa a operação assíncrona de obter os detalhes do aluno.</returns>
    /// <remarks>
    /// O método pode ser chamado passando apenas um dos critérios (ID, número de inscrição ou número de matrícula) para localizar o aluno.
    /// Se nenhum critério for fornecido ou se mais de um critério for fornecido, o método pode retornar informações incorretas ou nulas.
    /// </remarks>
    Task<DetalheAlunoArgumentoModel> ObterDetalheAluno(int? id = null, int? inscricao = null, int? matricula = null);

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

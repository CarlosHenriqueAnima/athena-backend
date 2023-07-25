using AthenasAcademy.Services.Domain.Requests;
using AthenasAcademy.Services.Domain.Responses;

namespace AthenasAcademy.Services.Core.Services.Interfaces;

/// <summary>
/// Interface responsável por definir os métodos relacionados a cursos, disciplinas e áreas de conhecimento.
/// </summary>
public interface ICursoService
{
    // Métodos relacionados a cursos

    /// <summary>
    /// Obtém um curso pelo ID.
    /// </summary>
    /// <param name="id">ID do curso a ser obtido.</param>
    /// <returns>Objeto contendo informações do curso.</returns>
    Task<CursoResponse> ObterCurso(int id);

    /// <summary>
    /// Obtém todos os cursos cadastrados.
    /// </summary>
    /// <returns>Lista de cursos cadastrados.</returns>
    Task<IEnumerable<CursoResponse>> ObterCursos();

    /// <summary>
    /// Cadastra um novo curso.
    /// </summary>
    /// <param name="request">Objeto contendo os dados do novo curso.</param>
    /// <returns>Objeto contendo informações do curso cadastrado.</returns>
    Task<NovoCursoResponse> CadastrarCurso(NovoCursoRequest request);

    /// <summary>
    /// Atualiza os dados de um curso existente.
    /// </summary>
    /// <param name="request">Objeto contendo os novos dados do curso.</param>
    /// <returns>Objeto contendo informações do curso atualizado.</returns>
    Task<CursoResponse> AtualizarCurso(CursoRequest request);

    /// <summary>
    /// Desativa um curso existente.
    /// </summary>
    /// <param name="id">ID do curso a ser desativado.</param>
    /// <returns>Indicação de sucesso na desativação do curso.</returns>
    Task<bool> DesativarCurso(int id);

    // Métodos relacionados a disciplinas

    /// <summary>
    /// Obtém uma disciplina pelo ID.
    /// </summary>
    /// <param name="id">ID da disciplina a ser obtida.</param>
    /// <returns>Objeto contendo informações da disciplina.</returns>
    Task<DisciplinaResponse> ObterDisciplina(int id);

    /// <summary>
    /// Obtém todas as disciplinas cadastradas.
    /// </summary>
    /// <returns>Lista de disciplinas cadastradas.</returns>
    Task<IEnumerable<DisciplinaResponse>> ObterDisciplinas();

    /// <summary>
    /// Cadastra uma nova disciplina.
    /// </summary>
    /// <param name="request">Objeto contendo os dados da nova disciplina.</param>
    /// <returns>Objeto contendo informações da disciplina cadastrada.</returns>
    Task<NovaDisciplinaResponse> CadastrarDisciplina(NovaDisciplinaRequest request);

    /// <summary>
    /// Atualiza os dados de uma disciplina existente.
    /// </summary>
    /// <param name="request">Objeto contendo os novos dados da disciplina.</param>
    /// <returns>Objeto contendo informações da disciplina atualizada.</returns>
    Task<DisciplinaResponse> AtualizarDisciplina(DisciplinaRequest request);

    /// <summary>
    /// Desativa uma disciplina existente.
    /// </summary>
    /// <param name="id">ID da disciplina a ser desativada.</param>
    /// <returns>Indicação de sucesso na desativação da disciplina.</returns>
    Task<bool> DesativarDisciplina(int id);

    // Métodos relacionados a áreas de conhecimento

    /// <summary>
    /// Obtém uma área de conhecimento pelo ID.
    /// </summary>
    /// <param name="id">ID da área de conhecimento a ser obtida.</param>
    /// <returns>Objeto contendo informações da área de conhecimento.</returns>
    Task<AreaConhecimentoResponse> ObterAreaConhecimento(int id);

    /// <summary>
    /// Obtém todas as áreas de conhecimento cadastradas.
    /// </summary>
    /// <returns>Lista de áreas de conhecimento cadastradas.</returns>
    Task<IEnumerable<AreaConhecimentoResponse>> ObterAreasConhecimento();

    /// <summary>
    /// Cadastra uma nova área de conhecimento.
    /// </summary>
    /// <param name="request">Objeto contendo os dados da nova área de conhecimento.</param>
    /// <returns>Objeto contendo informações da área de conhecimento cadastrada.</returns>
    Task<NovaAreaConhecimentoResponse> CadastrarAreaConhecimento(NovaAreaConhecimentoRequest request);

    /// <summary>
    /// Atualiza os dados de uma área de conhecimento existente.
    /// </summary>
    /// <param name="request">Objeto contendo os novos dados da área de conhecimento.</param>
    /// <returns>Objeto contendo informações da área de conhecimento atualizada.</returns>
    Task<AreaConhecimentoResponse> AtualizarAreaConhecimento(AreaConhecimentoRequest request);

    /// <summary>
    /// Desativa uma área de conhecimento existente.
    /// </summary>
    /// <param name="id">ID da área de conhecimento a ser desativada.</param>
    /// <returns>Indicação de sucesso na desativação da área de conhecimento.</returns>
    Task<bool> DesativarAreaConhecimento(int id);
}

using AthenasAcademy.Services.Core.Arguments;
using AthenasAcademy.Services.Core.Models;

namespace AthenasAcademy.Services.Core.Repositories.Interfaces;

/// <summary>
/// Interface que define os métodos de acesso aos dados relacionados a cursos, disciplinas e áreas de conhecimento.
/// </summary>
public interface ICursoRepository
{
    #region Cursos

    /// <summary>
    /// Obtém um curso pelo seu ID.
    /// </summary>
    /// <param name="id">ID do curso a ser obtido.</param>
    /// <returns>Objeto CursoModel contendo as informações do curso.</returns>
    Task<CursoModel> ObterCurso(int id);

    /// <summary>
    /// Obtém todos os cursos cadastrados.
    /// </summary>
    /// <returns>Enumerable de CursoModel contendo os cursos encontrados.</returns>
    Task<IEnumerable<CursoModel>> ObterCursos();

    /// <summary>
    /// Cadastra um novo curso.
    /// </summary>
    /// <param name="argument">Objeto CursoArgument contendo os dados do novo curso a ser cadastrado.</param>
    /// <returns>Resposta contendo as informações do curso recém-cadastrado.</returns>
    Task<CursoModel> CadastrarCurso(CursoArgument argument);

    /// <summary>
    /// Atualiza as informações de um curso existente.
    /// </summary>
    /// <param name="argument">Objeto CursoArgument contendo os dados atualizados do curso.</param>
    /// <returns>Objeto CursoModel contendo as informações atualizadas do curso.</returns>
    Task<CursoModel> AtualizarCurso(CursoArgument argument);

    /// <summary>
    /// Desativa um curso pelo seu ID.
    /// </summary>
    /// <param name="id">ID do curso a ser desativado.</param>
    /// <returns>True se o curso foi desativado com sucesso, False caso contrário.</returns>
    Task<bool> DesativarCurso(int id);

    #endregion

    #region Disciplinas

    /// <summary>
    /// Obtém uma disciplina pelo seu ID.
    /// </summary>
    /// <param name="id">ID da disciplina a ser obtida.</param>
    /// <returns>Objeto DisciplinaModel contendo as informações da disciplina.</returns>
    Task<DisciplinaModel> ObterDisciplina(int id);

    /// <summary>
    /// Obtém todas as disciplinas cadastradas.
    /// </summary>
    /// <returns>Enumerable de DisciplinaModel contendo as disciplinas encontradas.</returns>
    Task<IEnumerable<DisciplinaModel>> ObterDisciplinas();

    /// <summary>
    /// Obtém todas as disciplinas cadastradas.
    /// </summary>
    /// <returns>Enumerable de DisciplinaModel contendo as disciplinas encontradas.</returns>
    Task<IEnumerable<DisciplinaModel>> ObterDisciplinasDoCurso(int idCurso);

    /// <summary>
    /// Cadastra uma nova disciplina.
    /// </summary>
    /// <param name="argument">Objeto DisciplinaArgument contendo os dados da nova disciplina a ser cadastrada.</param>
    /// <returns>Resposta contendo as informações da disciplina recém-cadastrada.</returns>
    Task<DisciplinaModel> CadastrarDisciplina(DisciplinaArgument argument);

    /// <summary>
    /// Atualiza as informações de uma disciplina existente.
    /// </summary>
    /// <param name="argument">Objeto DisciplinaArgument contendo os dados atualizados da disciplina.</param>
    /// <returns>Objeto DisciplinaModel contendo as informações atualizadas da disciplina.</returns>
    Task<DisciplinaModel> AtualizarDisciplina(DisciplinaArgument argument);

    /// <summary>
    /// Desativa uma disciplina pelo seu ID.
    /// </summary>
    /// <param name="id">ID da disciplina a ser desativada.</param>
    /// <returns>True se a disciplina foi desativada com sucesso, False caso contrário.</returns>
    Task<bool> DesativarDisciplina(int id);

    #endregion

    #region Áreas de Conhecimento

    /// <summary>
    /// Obtém uma área de conhecimento pelo seu ID.
    /// </summary>
    /// <param name="id">ID da área de conhecimento a ser obtida.</param>
    /// <returns>Objeto AreaConhecimentoModel contendo as informações da área de conhecimento.</returns>
    Task<AreaConhecimentoModel> ObterAreaConhecimento(int id);

    /// <summary>
    /// Obtém todas as áreas de conhecimento cadastradas.
    /// </summary>
    /// <returns>Enumerable de AreaConhecimentoModel contendo as áreas de conhecimento encontradas.</returns>
    Task<IEnumerable<AreaConhecimentoModel>> ObterAreasConhecimento();

    /// <summary>
    /// Cadastra uma nova área de conhecimento.
    /// </summary>
    /// <param name="argument">Objeto AreaConhecimentoArgument contendo os dados da nova área de conhecimento a ser cadastrada.</param>
    /// <returns>Resposta contendo as informações da área de conhecimento recém-cadastrada.</returns>
    Task<AreaConhecimentoModel> CadastrarAreaConhecimento(AreaConhecimentoArgument argument);

    /// <summary>
    /// Atualiza as informações de uma área de conhecimento existente.
    /// </summary>
    /// <param name="argument">Objeto AreaConhecimentoArgument contendo os dados atualizados da área de conhecimento.</param>
    /// <returns>Objeto AreaConhecimentoModel contendo as informações atualizadas da área de conhecimento.</returns>
    Task<AreaConhecimentoModel> AtualizarAreaConhecimento(AreaConhecimentoArgument argument);

    /// <summary>
    /// Desativa uma área de conhecimento pelo seu ID.
    /// </summary>
    /// <param name="id">ID da área de conhecimento a ser desativada.</param>
    /// <returns>True se a área de conhecimento foi desativada com sucesso, False caso contrário.</returns>
    Task<bool> DesativarAreaConhecimento(int id);

    #endregion
}

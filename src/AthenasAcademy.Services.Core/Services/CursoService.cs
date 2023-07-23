using AthenasAcademy.Services.Core.Arguments;
using AthenasAcademy.Services.Core.Configurations.Mappers;
using AthenasAcademy.Services.Core.Exceptions;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Repositories.Interfaces;
using AthenasAcademy.Services.Core.Services.Interfaces;
using AthenasAcademy.Services.Domain.Configurations.Enums;
using AthenasAcademy.Services.Domain.Requests;
using AthenasAcademy.Services.Domain.Responses;
using System.Net;

namespace AthenasAcademy.Services.Core.Services;

public class CursoService : ICursoService
{
    private readonly ICursoRepository _cursoRepository;
    private readonly IObjectConverter _mapper;

    public CursoService(ICursoRepository cursoRepository, IObjectConverter mapper)
    {
        _cursoRepository = cursoRepository;
        _mapper = mapper;
    }

    #region Curso
    public async Task<CursoResponse> ObterCurso(int id)
    {
        CursoModel curso = await _cursoRepository.ObterCurso(id);

        if (curso is null)
            throw new APICustomException(string.Format("Nenhum curso localizado para a o id {0}. Por favor, revise os detalhes da requisição.", id), ExceptionResponseType.Warning, HttpStatusCode.NotFound);

        CursoResponse response = _mapper.Map<CursoResponse>(curso);

        response.AreaConhecimento = _mapper.Map<AreaConhecimentoResponse>(
            await _cursoRepository.ObterAreaConhecimento(curso.IdAreaConhecimento));

        response.Disciplinas = _mapper.Map<IEnumerable<DisciplinaResponse>>(
            await _cursoRepository.ObterDisciplinasDoCurso(curso.Id));

        return response;
    }

    public async Task<IEnumerable<CursoResponse>> ObterCursos()
    {
        IEnumerable<CursoModel> cursos = await _cursoRepository.ObterCursos();
        if (cursos.Count() == 0)
            throw new APICustomException("Nenhum 'curso' cadastrado.", ExceptionResponseType.Warning, HttpStatusCode.NotFound);

        IEnumerable<DisciplinaModel> disciplinas = await _cursoRepository.ObterDisciplinas();
        if (disciplinas.Count() == 0)
            throw new APICustomException("Nenhuma 'disciplina' cadastrada.", ExceptionResponseType.Warning, HttpStatusCode.NotFound);

        IEnumerable<AreaConhecimentoModel> areasConecimento = await _cursoRepository.ObterAreasConhecimento();
        if (areasConecimento.Count() == 0)
            throw new APICustomException("Nenhuma 'area do conecimento' cadastrada.", ExceptionResponseType.Warning, HttpStatusCode.NotFound);

        IEnumerable<CursoResponse> response = cursos.Select(curso => new CursoResponse
        {
            Id = curso.Id,
            Nome = curso.Nome,
            Descricao = curso.Descricao,
            CargaHoraria = curso.CargaHoraria,
            Ativo = curso.Ativo,
            DataCadastro = curso.DataCadastro,
            DataAtualizacao = curso.DataAlteracao,

            Disciplinas = disciplinas
            .Where(disciplina => disciplina.IdCurso == curso.Id)
            .Select(disciplina => new DisciplinaResponse
            {
                Id = disciplina.Id,
                Nome = disciplina.Nome,
                Descricao = disciplina.Descricao,
                CargaHoraria = disciplina.CargaHoraria,
                Ativo = disciplina.Ativo,
                DataCadastro = disciplina.DataCadastro,
                DataAtualizacao = disciplina.DataAlteracao
            }),

            AreaConhecimento = areasConecimento
            .Where(areasConecimento => areasConecimento.Id == curso.IdAreaConhecimento)
            .Select(ac => new AreaConhecimentoResponse
            {
                Id = ac.Id,
                Nome = ac.Nome,
                Descricao = ac.Descricao,
                Ativo = ac.Ativo,
                DataCadastro = ac.DataCadastro,
                DataAtualizacao = ac.DataAlteracao ?? ac.DataCadastro
            }).First()
        });

        return response;
    }

    public async Task<NovoCursoResponse> CadastrarCurso(NovoCursoRequest request)
    {
        var areaConhecimento = await ObterAreaConhecimento(request.IdAreaConhecimento);

        var argument = _mapper.Map<CursoArgument>(request);
        argument.Ativo = true;
        argument.DataCadastro = DateTime.Now;
        argument.DataAlteracao = null;

        CursoModel novoCurso = await _cursoRepository.CadastrarCurso(argument);

        NovoCursoResponse response = _mapper.Map<NovoCursoResponse>(novoCurso);

        response.Disciplinas = new List<DisciplinaResponse>();
        response.AreaConhecimento = _mapper.Map<AreaConhecimentoResponse>(areaConhecimento);

        return response;
    }

    public async Task<CursoResponse> AtualizarCurso(CursoRequest request)
    {
        CursoModel curso = await ObterCursoValidadoAsync(request.Id);
        IEnumerable<DisciplinaModel> disciplinas = await ObterDisciplinasValidadasAsync(request.Disciplinas);
        AreaConhecimentoModel areaConhecimento = await ObterAreaConhecimentoValidadaAsync(request.AreaConhecimento);

        CursoArgument argument = _mapper.Map<CursoArgument>(request);
        argument.Ativo = curso.Ativo;
        argument.IdAreaConhecimento = request.AreaConhecimento.Id;
        argument.DataAlteracao = DateTime.Now;
        argument.CargaHoraria = request.Disciplinas.Sum(disciplina => disciplina.CargaHoraria);

        CursoModel cursoAtualizado = await _cursoRepository.AtualizarCurso(argument);

        CursoResponse response = _mapper.Map<CursoResponse>(cursoAtualizado);
        response.Disciplinas = _mapper.Map<IEnumerable<DisciplinaResponse>>(request.Disciplinas);
        response.AreaConhecimento = _mapper.Map<AreaConhecimentoResponse>(request.AreaConhecimento);

        return response;
    }

    public async Task<bool> DesativarCurso(int id)
    {
        await ObterCursoValidadoAsync(id);

        return await _cursoRepository.DesativarCurso(id);
    }
    #endregion

    #region Disciplina
    public async Task<DisciplinaResponse> ObterDisciplina(int id)
    {
        DisciplinaModel disciplina = await _cursoRepository.ObterDisciplina(id);

        if (disciplina is null)
            throw new APICustomException(string.Format("Nenhuma disciplina localizada para a o id {0}. Por favor, revise os detalhes da requisição.", disciplina.Id), ExceptionResponseType.Warning, HttpStatusCode.NotFound);

        return _mapper.Map<DisciplinaResponse>(await _cursoRepository.ObterDisciplina(id));
    }

    public async Task<IEnumerable<DisciplinaResponse>> ObterDisciplinas()
    {
        IEnumerable<DisciplinaModel> disciplinas = await _cursoRepository.ObterDisciplinas();

        if (disciplinas.Count() == 0)
            throw new APICustomException("Nenhuma disciplina cadastrada.", ExceptionResponseType.Warning, HttpStatusCode.NotFound);

        return _mapper.Map<IEnumerable<DisciplinaResponse>>(disciplinas);
    }

    public async Task<NovaDisciplinaResponse> CadastrarDisciplina(NovaDisciplinaRequest request)
    {
        var curso = await _cursoRepository.ObterCurso(request.IdCurso);

        if (curso is null)
            throw new APICustomException(string.Format("Nenhum curso localizado para a o id {0}. Por favor, revise os detalhes da requisição.", request.IdCurso), ExceptionResponseType.Warning, HttpStatusCode.NotFound);

        DisciplinaArgument argument = _mapper.Map<DisciplinaArgument>(request);
        argument.Ativo = true;
        argument.DataCadastro = DateTime.Now;
        argument.DataAlteracao = null;

        DisciplinaModel novaDisciplina = await _cursoRepository.CadastrarDisciplina(argument);

        await AtualizarCargaHorariaCurso(novaDisciplina.IdCurso);

        return _mapper.Map<NovaDisciplinaResponse>(novaDisciplina);
    }

    public async Task<DisciplinaResponse> AtualizarDisciplina(DisciplinaRequest request)
    {
        var disciplina = await ObterDisciplina(request.Id);

        DisciplinaArgument argument = _mapper.Map<DisciplinaArgument>(request);
        argument.Ativo = disciplina.Ativo;
        argument.DataCadastro = disciplina.DataCadastro;
        argument.DataAlteracao = DateTime.Now;
        argument.CargaHoraria = request.CargaHoraria;

        DisciplinaModel disciplinaAtualizada = await _cursoRepository.AtualizarDisciplina(argument);

        DisciplinaResponse response = _mapper.Map<DisciplinaResponse>(disciplinaAtualizada);

        await AtualizarCargaHorariaCurso(request.IdCurso);

        return response;
    }

    public async Task<bool> DesativarDisciplina(int id)
    {
        DisciplinaModel disciplina = (await ObterDisciplinasValidadasAsync(
            new() { new() { Id = id } })).First(d => d.Id == id);

        bool resultado = await _cursoRepository.DesativarDisciplina(id);

        if (resultado)
            await AtualizarCargaHorariaCurso(disciplina.IdCurso);

        return resultado;
    }
    #endregion

    #region Área Conhecimento

    public async Task<AreaConhecimentoResponse> ObterAreaConhecimento(int id)
    {
        AreaConhecimentoModel areaConhecimento = await _cursoRepository.ObterAreaConhecimento(id);

        if (areaConhecimento is null)
            throw new APICustomException(string.Format("Nenhuma Área do Conhecimento localizada para o id {0}. Por favor, revise os detalhes da requisição.", id)
                , ExceptionResponseType.Warning, HttpStatusCode.NotFound);

        return _mapper.Map<AreaConhecimentoResponse>(areaConhecimento);
    }

    public async Task<IEnumerable<AreaConhecimentoResponse>> ObterAreasConhecimento()
    {
        IEnumerable<AreaConhecimentoModel> areasConhecimento = await _cursoRepository.ObterAreasConhecimento();

        if (areasConhecimento.Count() == 0)
            throw new APICustomException("Nenhuma Area do Conhecimento cadastrada."
                , ExceptionResponseType.Warning, HttpStatusCode.NotFound);

        return _mapper.Map<IEnumerable<AreaConhecimentoResponse>>(areasConhecimento);
    }

    public async Task<NovaAreaConhecimentoResponse> CadastrarAreaConhecimento(NovaAreaConhecimentoRequest request)
    {
        try
        {
            AreaConhecimentoArgument argument = _mapper.Map<AreaConhecimentoArgument>(request);

            argument.Ativo = true;
            argument.DataCadastro = DateTime.Now;
            argument.DataAlteracao = null;

            AreaConhecimentoModel model = await _cursoRepository.CadastrarAreaConhecimento(argument);

            return _mapper.Map<NovaAreaConhecimentoResponse>(model);
        }
        catch (Exception ex)
        {
            throw new APICustomException(string.Format("Não foi possível processar a sua requisição nesse momento. {0}", ex.Message), ExceptionResponseType.Error, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<AreaConhecimentoResponse> AtualizarAreaConhecimento(AreaConhecimentoRequest request)
    {
        AreaConhecimentoModel areaConhecimentoModel = await _cursoRepository.ObterAreaConhecimento(request.Id);

        if (areaConhecimentoModel is null || request is null)
            throw new APICustomException(string.Format("Área do Conhecimento com ID {0} ainda não foi cadastrada.",
                areaConhecimentoModel.Id == 0 ? request.Id : areaConhecimentoModel.Id), ExceptionResponseType.Error, HttpStatusCode.BadRequest);

        if (!areaConhecimentoModel.Ativo)
            throw new APICustomException(string.Format("Área do Conhecimento com ID {0} está desativada.",
                areaConhecimentoModel.Id == 0 ? request.Id : areaConhecimentoModel.Id), ExceptionResponseType.Error, HttpStatusCode.BadRequest);

        AreaConhecimentoArgument argument = _mapper.Map<AreaConhecimentoArgument>(request);

        argument.Ativo = areaConhecimentoModel.Ativo;
        argument.DataCadastro = areaConhecimentoModel.DataCadastro;
        argument.DataAlteracao = areaConhecimentoModel.DataAlteracao ?? null;

        AreaConhecimentoModel model = await _cursoRepository.AtualizarAreaConhecimento(argument);

        return _mapper.Map<AreaConhecimentoResponse>(model);
    }

    public async Task<bool> DesativarAreaConhecimento(int id)
    {
        AreaConhecimentoResponse areaConhecimentoBanco = await ObterAreaConhecimento(id);

        if (!areaConhecimentoBanco.Ativo)
            throw new APICustomException(string.Format("Área do Conhecimento com ID {0} está desativada."), ExceptionResponseType.Error, HttpStatusCode.BadRequest);

        return await _cursoRepository.DesativarAreaConhecimento(id);
    }
    #endregion

    #region Métodos Privados
    private async Task<CursoModel> ObterCursoValidadoAsync(int id)
    {
        CursoModel curso = await _cursoRepository.ObterCurso(id);

        if (curso is null)
            throw new APICustomException(string.Format("Nenhum curso localizado para a o id {0}. Por favor, revise os detalhes da requisição.", id), ExceptionResponseType.Warning, HttpStatusCode.NotFound);

        if (!curso.Ativo)
            throw new APICustomException(string.Format("O curso Id:{0} - {1} já foi desativado", id, curso.Nome), ExceptionResponseType.Warning, HttpStatusCode.NotFound);

        return curso;
    }

    private async Task<IEnumerable<DisciplinaModel>> ObterDisciplinasValidadasAsync(List<DisciplinaRequest> disciplinas)
    {
        IEnumerable<DisciplinaModel> disciplinasDoCurso = await _cursoRepository.ObterDisciplinas();

        if (disciplinas.Count() == 0 || disciplinas is null)
            return disciplinasDoCurso;

        IEnumerable<int> idsDisciplinasRequest = disciplinas.Select(disciplinaRequest => disciplinaRequest.Id);
        IEnumerable<int> idsDisciplinasCadastradas = disciplinasDoCurso.Select(disciplinaModel => disciplinaModel.Id);
        IEnumerable<int> idsDisciplinasNaoCadastradas = idsDisciplinasRequest.Except(idsDisciplinasCadastradas).ToList();

        if (idsDisciplinasNaoCadastradas.Any())
        {
            IEnumerable<string> disciplinasNaoCadastradas = disciplinas
                .Where(disciplinaRequest => idsDisciplinasNaoCadastradas.Contains(disciplinaRequest.Id))
                .Select(disciplinaRequest => $"ID {disciplinaRequest.Id}: {disciplinaRequest.Nome}");

            string mensagem = $"Disciplinas não cadastradas: {string.Join(", ", disciplinasNaoCadastradas)}";

            throw new APICustomException(mensagem, ExceptionResponseType.Error, HttpStatusCode.BadRequest);
        }

        IEnumerable<string> idsDisciplinasDesativadas = disciplinas
            .Where(disciplinaRequest => disciplinasDoCurso.Any(disciplinaModel => disciplinaModel.Id == disciplinaRequest.Id && !disciplinaModel.Ativo))
            .Select(disciplinaRequest => $"ID {disciplinaRequest.Id}: {disciplinaRequest.Nome}");

        if (idsDisciplinasDesativadas.Any())
        {
            string mensagem = $"Disciplinas desativadas: {string.Join(", ", idsDisciplinasDesativadas)}";

            throw new APICustomException(mensagem, ExceptionResponseType.Error, HttpStatusCode.BadRequest);
        }

        return disciplinasDoCurso;
    }

    private async Task<AreaConhecimentoModel> ObterAreaConhecimentoValidadaAsync(AreaConhecimentoRequest areaConhecimento)
    {
        AreaConhecimentoModel areaConhecimentoBanco = await _cursoRepository.ObterAreaConhecimento(areaConhecimento.Id);

        if (areaConhecimentoBanco is null)
            throw new APICustomException(string.Format("Nenhuma Área do Conhecimento localizada para a o id {0}. Por favor, revise os detalhes da requisição.", areaConhecimento.Id), ExceptionResponseType.Warning, HttpStatusCode.NotFound);

        if (!areaConhecimento.Ativo)
            throw new APICustomException(string.Format("A Área do Conhecimento de Id:{0} - {1} já foi desativado", areaConhecimento.Id, areaConhecimento.Nome), ExceptionResponseType.Warning, HttpStatusCode.NotFound);

        return areaConhecimentoBanco;
    }

    private async Task<IEnumerable<DisciplinaResponse>> ObterDisciplinasPorCurso(int idCurso)
    {
        IEnumerable<DisciplinaModel> disciplinas = await _cursoRepository.ObterDisciplinasDoCurso(idCurso);

        if (disciplinas.Count() == 0)
            throw new APICustomException(string.Format("Nenhum curso cadastrado para o ID {0}.", idCurso), ExceptionResponseType.Warning, HttpStatusCode.NotFound);

        return _mapper.Map<IEnumerable<DisciplinaResponse>>(disciplinas);
    }

    private async Task AtualizarCargaHorariaCurso(int idCurso)
    {
        IEnumerable<DisciplinaResponse> disciplinas = await ObterDisciplinasPorCurso(idCurso);
        CursoResponse curso = await ObterCurso(idCurso);

        curso.CargaHoraria = disciplinas.Where(disciplina => disciplina.Ativo).Sum(disciplina => disciplina.CargaHoraria);

        var argument = _mapper.Map<CursoArgument>(curso);
        argument.Ativo = curso.Ativo;
        argument.DataAlteracao = DateTime.Now;
        argument.IdAreaConhecimento = curso.AreaConhecimento.Id;

        await _cursoRepository.AtualizarCurso(argument);
    }
    #endregion
}

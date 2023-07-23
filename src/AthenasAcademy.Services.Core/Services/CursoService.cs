using AthenasAcademy.Services.Core.Arguments;
using AthenasAcademy.Services.Core.Configurations.Mappers;
using AthenasAcademy.Services.Core.Exceptions;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Repositories.Interfaces;
using AthenasAcademy.Services.Core.Services.Interfaces;
using AthenasAcademy.Services.Domain.Configurations.Enums;
using AthenasAcademy.Services.Domain.Requests;
using AthenasAcademy.Services.Domain.Responses;
using System.Collections.Generic;
using System.Linq;
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
        try
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
        catch (Exception ex)
        {
            throw new APICustomException(string.Format("Não foi possível processar a sua requisição nesse momento. {0}", ex.Message), ExceptionResponseType.Error, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<IEnumerable<CursoResponse>> ObterCursos()
    {
        try
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
        catch (Exception ex)
        {
            throw new APICustomException(string.Format("Não foi possível processar a sua requisição nesse momento. {0}", ex.Message), ExceptionResponseType.Error, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<NovoCursoResponse> CadastrarCurso(NovoCursoRequest request)
    {
        try
        {
            await ValidarDisciplinasAsync(request.Disciplinas);

            await ValidarAreaConhecimentoAsync(request.AreaConhecimento);

            CursoModel novoCurso = await _cursoRepository.CadastrarCurso(
                _mapper.Map<CursoArgument>(request));

            NovoCursoResponse response = _mapper.Map<NovoCursoResponse>(novoCurso);

            response.Disciplinas = _mapper.Map<IEnumerable<DisciplinaResponse>>(request.Disciplinas);
            response.AreaConhecimento = _mapper.Map<AreaConhecimentoResponse>(request.AreaConhecimento);

            return response;
        }
        catch (Exception ex)
        {
            throw new APICustomException(string.Format("Não foi possível processar a sua requisição nesse momento. {0}", ex.Message), ExceptionResponseType.Error, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<CursoResponse> AtualizarCurso(CursoRequest request)
    {
        try
        {
            await ValidarCursoAsync(request.Id);

            await ValidarDisciplinasAsync(request.Disciplinas);

            await ValidarAreaConhecimentoAsync(request.AreaConhecimento);

            CursoArgument argument = _mapper.Map<CursoArgument>(request);

            CursoResponse response = _mapper.Map<CursoResponse>(
                await _cursoRepository.AtualizarCurso(argument));

            response.Disciplinas = _mapper.Map<IEnumerable<DisciplinaResponse>>(request.Disciplinas);
            response.AreaConhecimento = _mapper.Map<AreaConhecimentoResponse>(request.AreaConhecimento);

            return response;
        }
        catch (Exception ex)
        {
            throw new APICustomException(string.Format("Não foi possível processar a sua requisição nesse momento. {0}", ex.Message), ExceptionResponseType.Error, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<bool> DesativarCurso(int id)
    {
        try
        {
            await ValidarCursoAsync(id);

            return await _cursoRepository.DesativarCurso(id);
        }
        catch (Exception ex)
        {
            throw new APICustomException(string.Format("Não foi possível processar a sua requisição nesse momento. {0}", ex.Message), ExceptionResponseType.Error, HttpStatusCode.InternalServerError);
        }
    }
    #endregion

    #region Disciplina
    public async Task<DisciplinaResponse> ObterDisciplina(int id)
    {
        try
        {
            DisciplinaModel disciplina = await _cursoRepository.ObterDisciplina(id);

             await ValidarDisciplinaAsync(_mapper.Map<DisciplinaRequest>(disciplina));

            return _mapper.Map<DisciplinaResponse>(await _cursoRepository.ObterDisciplina(id));
        }
        catch (Exception ex)
        {
            throw new APICustomException(string.Format("Não foi possível processar a sua requisição nesse momento. {0}", ex.Message), ExceptionResponseType.Error, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<IEnumerable<DisciplinaResponse>> ObterDisciplinas()
    {
        try
        {
            IEnumerable<DisciplinaModel> disciplinas = await _cursoRepository.ObterDisciplinas();

            if (disciplinas.Count() == 0)
                throw new APICustomException("Nenhuma disciplina cadastrada.", ExceptionResponseType.Warning, HttpStatusCode.NotFound);

            return _mapper.Map<IEnumerable<DisciplinaResponse>>(disciplinas);
        }
        catch (Exception ex)
        {
            throw new APICustomException(string.Format("Não foi possível processar a sua requisição nesse momento. {0}", ex.Message), ExceptionResponseType.Error, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<NovaDisciplinaResponse> CadastrarDisciplina(NovaDisciplinaRequest request)
    {
        try
        {
            if (_cursoRepository.ObterDisciplina(request.IdCurso) is null)
                throw new APICustomException(string.Format("Nenhum curso localizado para a o id {0}. Por favor, revise os detalhes da requisição.", request.IdCurso), ExceptionResponseType.Warning, HttpStatusCode.NotFound);

            DisciplinaModel novaDisciplina = await _cursoRepository.CadastrarDisciplina(
                _mapper.Map<DisciplinaArgument>(request));

            await AtualizarCargaHorariaCurso(novaDisciplina.IdCurso);

            return _mapper.Map<NovaDisciplinaResponse>(novaDisciplina);
        }
        catch (Exception ex)
        {
            throw new APICustomException(string.Format("Não foi possível processar a sua requisição nesse momento. {0}", ex.Message), ExceptionResponseType.Error, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<DisciplinaResponse> AtualizarDisciplina(DisciplinaRequest request)
    {
        try
        {
            await ValidarDisciplinasAsync(new() { request });

            DisciplinaArgument argument = _mapper.Map<DisciplinaArgument>(request);

            DisciplinaResponse response = _mapper.Map<DisciplinaResponse>(
                _cursoRepository.AtualizarDisciplina(argument));

            await AtualizarCargaHorariaCurso(request.IdCurso);

            return response;
        }
        catch (Exception ex)
        {
            throw new APICustomException(string.Format("Não foi possível processar a sua requisição nesse momento. {0}", ex.Message), ExceptionResponseType.Error, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<bool> DesativarDisciplina(int id)
    {
        try
        {
            await ValidarDisciplinasAsync(new() { new() { Id = id } });

            return await _cursoRepository.DesativarDisciplina(id);
        }
        catch (Exception ex)
        {
            throw new APICustomException(string.Format("Não foi possível processar a sua requisição nesse momento. {0}", ex.Message), ExceptionResponseType.Error, HttpStatusCode.InternalServerError);
        }
    }
    #endregion

    #region Área Conhecimento

    public async Task<AreaConhecimentoResponse> ObterAreaConhecimento(int id)
    {
        try
        {
            AreaConhecimentoModel areaConhecimento = await _cursoRepository.ObterAreaConhecimento(id);

            if (areaConhecimento is null)
                throw new APICustomException(string.Format("Nenhuma Área do Conhecimento localizada para o id {0}. Por favor, revise os detalhes da requisição.", id), ExceptionResponseType.Warning, HttpStatusCode.NotFound);

            return _mapper.Map<AreaConhecimentoResponse>(areaConhecimento);
        }
        catch (Exception ex)
        {
            throw new APICustomException(string.Format("Não foi possível processar a sua requisição nesse momento. {0}", ex.Message), ExceptionResponseType.Error, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<IEnumerable<AreaConhecimentoResponse>> ObterAreasConhecimento()
    {
        try
        {
            IEnumerable<AreaConhecimentoModel> areasConhecimento = await _cursoRepository.ObterAreasConhecimento();

            if (areasConhecimento.Count() == 0)
                throw new APICustomException("Nenhuma Area do Conhecimento cadastrada.", ExceptionResponseType.Warning, HttpStatusCode.NotFound);

            return _mapper.Map<IEnumerable<AreaConhecimentoResponse>>(areasConhecimento);
        }
        catch (Exception ex)
        {
            throw new APICustomException(string.Format("Não foi possível processar a sua requisição nesse momento. {0}", ex.Message), ExceptionResponseType.Error, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<NovaAreaConhecimentoResponse> CadastrarAreaConhecimento(NovaAreaConhecimentoRequest request)
    {
        try
        {
            AreaConhecimentoArgument argument = _mapper.Map<AreaConhecimentoArgument>(request);

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
        try
        {
            await ValidarAreaConhecimentoAsync(request);

            AreaConhecimentoArgument argument = _mapper.Map<AreaConhecimentoArgument>(request);

            AreaConhecimentoModel model = await _cursoRepository.AtualizarAreaConhecimento(argument);

            return _mapper.Map<AreaConhecimentoResponse>(model);
        }
        catch (Exception ex)
        {
            throw new APICustomException(string.Format("Não foi possível processar a sua requisição nesse momento. {0}", ex.Message), ExceptionResponseType.Error, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<bool> DesativarAreaConhecimento(int id)
    {
        try
        {
            await ValidarAreaConhecimentoAsync(new() { Id = id });

            return await _cursoRepository.DesativarAreaConhecimento(id);
        }
        catch (Exception ex)
        {
            throw new APICustomException(string.Format("Não foi possível processar a sua requisição nesse momento. {0}", ex.Message), ExceptionResponseType.Error, HttpStatusCode.InternalServerError);
        }
    }
    #endregion

    #region Métodos Privados
    private async Task ValidarCursoAsync(int id)
    {
        CursoModel curso = await _cursoRepository.ObterCurso(id);

        if (curso is null)
            throw new APICustomException(string.Format("Nenhum curso localizado para a o id {0}. Por favor, revise os detalhes da requisição.", id), ExceptionResponseType.Warning, HttpStatusCode.NotFound);

        if (!curso.Ativo)
            throw new APICustomException(string.Format("O curso Id:{0} - {1} já foi desativado", id, curso.Nome), ExceptionResponseType.Warning, HttpStatusCode.NotFound);

    }
    private async Task ValidarDisciplinasAsync(List<DisciplinaRequest> disciplinas)
    {
        if (disciplinas.Count() == 0 || disciplinas is null)
            throw new APICustomException("É necessário informar pelo menos uma disciplina.", ExceptionResponseType.Warning, HttpStatusCode.NotFound);

        IEnumerable<DisciplinaModel> disciplinasDoCurso = await _cursoRepository.ObterDisciplinas();

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
    }

    private async Task ValidarDisciplinaAsync(DisciplinaRequest disciplina)
    {
        if (disciplina is null)
            throw new APICustomException(string.Format("Nenhuma disciplina localizada para a o id {0}. Por favor, revise os detalhes da requisição.", disciplina.Id), ExceptionResponseType.Warning, HttpStatusCode.NotFound);
    }

    private async Task ValidarAreaConhecimentoAsync(AreaConhecimentoRequest areaConhecimento)
    {
        if (areaConhecimento is null)
            throw new APICustomException("É necessário informar a Área de Conhecimento.", ExceptionResponseType.Warning, HttpStatusCode.NotFound);

        AreaConhecimentoModel areaConhecimentoModel = await _cursoRepository.ObterAreaConhecimento(areaConhecimento.Id);

        if (areaConhecimentoModel is null)
            throw new APICustomException(string.Format("Área do Conhecimento com ID {0} ainda não foi cadastrada."), ExceptionResponseType.Error, HttpStatusCode.BadRequest);

        if (!areaConhecimento.Ativo)
            throw new APICustomException(string.Format("Área do Conhecimento com ID {0} está desativada."), ExceptionResponseType.Error, HttpStatusCode.BadRequest);
    }

    private async Task<IEnumerable<DisciplinaResponse>> ObterDisciplinasPorCurso(int idCurso)
    {
        try
        {
            IEnumerable<DisciplinaModel> disciplinas = await _cursoRepository.ObterDisciplinasDoCurso(idCurso);

            if (disciplinas.Count() == 0)
                throw new APICustomException(string.Format("Nenhum curso cadastrado para o ID {0}.", idCurso), ExceptionResponseType.Warning, HttpStatusCode.NotFound);

            return _mapper.Map<IEnumerable<DisciplinaResponse>>(disciplinas);
        }
        catch (Exception ex)
        {
            throw new APICustomException(string.Format("Não foi possível processar a sua requisição nesse momento. {0}", ex.Message), ExceptionResponseType.Error, HttpStatusCode.InternalServerError);
        }
    }
    
    private async Task AtualizarCargaHorariaCurso(int idCurso)
    {
        IEnumerable<DisciplinaResponse> disciplinas = await ObterDisciplinasPorCurso(idCurso);
        CursoResponse curso = await ObterCurso(idCurso);

        curso.CargaHoraria = disciplinas.Sum(disciplina => disciplina.CargaHoraria);

        await AtualizarCurso(_mapper.Map<CursoRequest>(curso));
    }
    #endregion

}

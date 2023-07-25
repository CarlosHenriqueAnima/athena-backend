using AthenasAcademy.Services.Core.Exceptions;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Repositories.Interfaces;
using AthenasAcademy.Services.Core.Services.Interfaces;
using AthenasAcademy.Services.Domain.Configurations.Enums;
using System.Net;

namespace AthenasAcademy.Services.Core.Services;

public class MatriculaService : IMatriculaService
{
    private readonly IMatriculaRepository _matriculaRepository;

    public MatriculaService(IMatriculaRepository matriculaRepository)
    {
        _matriculaRepository = matriculaRepository;
    }

    public async Task<AlunoDetalhesModel> ObterMatriculaPorIdAsync(int id)
    {
        return await _matriculaRepository.ObterMatriculaPorIdAsync(id);
    }

    public async Task<IEnumerable<AlunoDetalhesModel>> ObterTodasMatriculasAsync()
    {
        return await _matriculaRepository.ObterTodasMatriculasAsync();
    }

    public async Task AdicionarMatriculaAsync(AlunoDetalhesModel matricula)
    {
        if (matricula == null)
            throw new APICustomException(string.Format("Não há dados de matrícula."), ExceptionResponseType.Error, HttpStatusCode.BadRequest);

        if (String.IsNullOrEmpty(matricula.CodigoContrato) || String.IsNullOrEmpty(matricula.CodigoMatricula))
            throw new APICustomException(string.Format("A matrícula não foi finalizada."), ExceptionResponseType.Warning, HttpStatusCode.BadRequest);

        if (String.IsNullOrEmpty(matricula.CodigoUsuario))
            throw new APICustomException(string.Format("Usuário não vinculado."), ExceptionResponseType.Error, HttpStatusCode.BadRequest);

        if (String.IsNullOrEmpty(matricula.CodigoInscricao))
            throw new APICustomException(string.Format("Informe um curso de interesse."), ExceptionResponseType.Error, HttpStatusCode.BadRequest);

        await _matriculaRepository.AdicionarMatriculaAsync(matricula);
    }

    public async Task AtualizarMatriculaAsync(AlunoDetalhesModel matricula)
    {
        if (matricula == null)
            throw new APICustomException(string.Format("Não há dados de inscrição."), ExceptionResponseType.Error, HttpStatusCode.BadRequest);

        if (String.IsNullOrEmpty(matricula.CodigoContrato) || String.IsNullOrEmpty(matricula.CodigoUsuario) || String.IsNullOrEmpty(matricula.CodigoInscricao))
            throw new APICustomException(string.Format("A matrícula precisa de um contrato, usuário e uma inscrição."), ExceptionResponseType.Error, HttpStatusCode.BadRequest);

        await _matriculaRepository.AtualizarMatriculaAsync(matricula);
    }

    public async Task CancelarMatriculaAsync(int matriculaId)
    {
        await _matriculaRepository.CancelarMatriculaAsync(matriculaId);
    }
}

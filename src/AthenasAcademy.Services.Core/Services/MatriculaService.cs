using AthenasAcademy.Services.Core.CrossCutting;
using AthenasAcademy.Services.Core.Exceptions;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Repositories.Interfaces;
using AthenasAcademy.Services.Core.Services.Interfaces;
using AthenasAcademy.Services.Core.Services.SQSProducer;
using AthenasAcademy.Services.Domain.Responses;
using System.Net;

namespace AthenasAcademy.Services.Core.Services;

public class MatriculaService : IMatriculaService
{
    private readonly IQueueProducerService _queueProducerService;
    private readonly IAlunoService _alunoService;
    private readonly IMatriculaRepository _matriculaRepository;

    public MatriculaService(
        IQueueProducerService queueProducerService,
        IAlunoService alunoService,
        IMatriculaRepository matriculaRepository)
    {
        _queueProducerService = queueProducerService;
        _alunoService = alunoService;
        _matriculaRepository = matriculaRepository;
    }

    public async Task<MatriculaStatusResponse> MatricularAluno(int inscricao)
    {
        await ValidarProcessoMatricula(inscricao);

        var fichaAluno = await _alunoService.ObterFichaAluno(inscricao);

        var matricula = await _matriculaRepository.AtivarMatricula(fichaAluno);

        await _queueProducerService.Certificado(fichaAluno).Send();

        return await Task.FromResult(new MatriculaStatusResponse
        {
            Matricula = matricula.Matricula,
            Contrato = matricula.CodigoContrato,
            BoletoPago = true,
            ContratoAssinado = true,
        });
    }

    public async Task RegistrarPreMatricula(FichaAluno fichaAluno)
    {
        MatriculaModel matricula = await _matriculaRepository.GerarPreMatricula(fichaAluno);

        fichaAluno.Contrato.Matricula = matricula.Matricula;
        fichaAluno.Contrato.NumeroContrato = matricula.CodigoContrato;

        await _queueProducerService.Contrato(fichaAluno).Send();
        await _queueProducerService.Boleto(fichaAluno).Send();
    }

    private async Task ValidarProcessoMatricula(int inscricao)
    {
        /// TODO - StackOverFlow: BUG
        //// buscar inscricao - StackOverFlow: BURG
        //InscricaoCandidatoModel inscricaoAluno = await _inscricaoService.ObterInscricao(inscricao);

        //if (inscricaoAluno is null)
        //    throw new APICustomException(
        //        message: $"Inscrição {inscricao} não localizada.",
        //        responseType: Domain.Configurations.Enums.ExceptionResponseType.Error,
        //        statusCode: HttpStatusCode.BadRequest);

        //if (!inscricaoAluno.BoletoPago)
        //    throw new APICustomException(
        //        message: $"Boleto da  inscrição {inscricao} ainda foi não pago.",
        //        responseType: Domain.Configurations.Enums.ExceptionResponseType.Error,
        //        statusCode: HttpStatusCode.BadRequest);

        // buscar matricula
        MatriculaModel matriculaAluno = await _matriculaRepository.ObterMatricula(inscricao);

        if (matriculaAluno is null)
            throw new APICustomException(
                message: $"Não constam matrículas para inscrição {inscricao}.",
                responseType: Domain.Configurations.Enums.ExceptionResponseType.Error,
                statusCode: HttpStatusCode.BadRequest);

        if (!matriculaAluno.Ativa)
            throw new APICustomException(
                message: $"Matrícula {matriculaAluno.Matricula} ainda não foi ativada.",
                responseType: Domain.Configurations.Enums.ExceptionResponseType.Error,
                statusCode: HttpStatusCode.BadRequest);

        if (!matriculaAluno.Assinado)
            throw new APICustomException(
                message: $"Contrato de matrícula {matriculaAluno.CodigoContrato} ainda foi não assinado.",
                responseType: Domain.Configurations.Enums.ExceptionResponseType.Error,
                statusCode: HttpStatusCode.BadRequest);

        if (matriculaAluno.Assinado && matriculaAluno.Ativa)
            throw new APICustomException(
                message: $"Aluno já matriculado.",
                responseType: Domain.Configurations.Enums.ExceptionResponseType.Error,
                statusCode: HttpStatusCode.BadRequest);

        throw new NotImplementedException();
    }
}
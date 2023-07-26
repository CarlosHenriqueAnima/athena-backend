using AthenasAcademy.Services.Core.CrossCutting;
using AthenasAcademy.Services.Core.Repositories.Interfaces;
using AthenasAcademy.Services.Core.Services.Interfaces;
using AthenasAcademy.Services.Core.Services.SQSProducer;
using AthenasAcademy.Services.Domain.Responses;

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
        // validar se inscricao existe
        await ValidarInscricao(inscricao);

        // validar se boleto foi pago
        await ValidarPagamentoBoleto(inscricao);

        // validar se contrato foi assinado
        await ValidarContratoAssinado(inscricao);

        // obter ficha aluno
        var fichaAluno = await _alunoService.ObterFichaAluno(inscricao);


        var matricula = await _matriculaRepository.AtivarMatricula(fichaAluno);

        // liberar certificado
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
        // cadastrar contrato
        await _matriculaRepository.GerarPreMatricula(fichaAluno);

        // cadastrar pré-contrato
        await _matriculaRepository.GerarContratoMatricula(fichaAluno);

        await _queueProducerService.Contrato(fichaAluno).Send();
        await _queueProducerService.Boleto(fichaAluno).Send();

        throw new NotImplementedException();
    }

    private Task ValidarContratoAssinado(int inscricao)
    {
        throw new NotImplementedException();
    }

    private Task ValidarPagamentoBoleto(int inscricao)
    {
        throw new NotImplementedException();
    }

    private Task ValidarInscricao(int inscricao)
    {
        throw new NotImplementedException();
    }
}
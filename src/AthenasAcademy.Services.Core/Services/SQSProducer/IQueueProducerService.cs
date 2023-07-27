using AthenasAcademy.Services.Core.CrossCutting;

namespace AthenasAcademy.Services.Core.Services.SQSProducer;

/// <summary>
/// Interface responsável por definir o contrato para o serviço de produção de mensagens em fila.
/// </summary>
public interface IQueueProducerService
{
    /// <summary>
    /// Gera um contrato com base nas informações contidas na ficha do aluno e envia-o para a fila de processamento.
    /// </summary>
    /// <param name="fichaAluno">A ficha do aluno contendo as informações necessárias para gerar o contrato.</param>
    /// <returns>Uma tarefa que representa o processamento assíncrono da geração do contrato.</returns>
    Task<bool> GerarContrato(FichaAluno fichaAluno);

    /// <summary>
    /// Gera um boleto com base nas informações contidas na ficha do aluno e envia-o para a fila de processamento.
    /// </summary>
    /// <param name="fichaAluno">A ficha do aluno contendo as informações necessárias para gerar o boleto.</param>
    /// <returns>Uma tarefa que representa o processamento assíncrono da geração do boleto.</returns>
    Task<bool> GerarBoleto(FichaAluno fichaAluno);
}

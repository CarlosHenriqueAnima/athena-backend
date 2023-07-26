using AthenasAcademy.Services.Core.CrossCutting;

namespace AthenasAcademy.Services.Core.Services.SQSProducer;

/// <summary>
/// Interface responsável por definir o contrato para o serviço de envio de mensagens para uma fila.
/// </summary>
public interface IQueueProducerService
{
    /// <summary>
    /// Configura a ação de envio de contrato para o aluno especificado.
    /// </summary>
    /// <param name="fichaAluno">O objeto FichaAluno contendo os dados para o contrato.</param>
    /// <returns>Uma instância de IQueueProducerService com a ação de contrato configurada.</returns>
    IQueueProducerService Contrato(FichaAluno fichaAluno);

    /// <summary>
    /// Configura a ação de envio de boleto para o aluno especificado.
    /// </summary>
    /// <param name="fichaAluno">O objeto FichaAluno contendo os dados para o boleto.</param>
    /// <returns>Uma instância de IQueueProducerService com a ação de boleto configurada.</returns>
    IQueueProducerService Boleto(FichaAluno fichaAluno);

    /// <summary>
    /// Configura a ação de envio de certificado para o aluno especificado.
    /// </summary>
    /// <param name="fichaAluno">O objeto FichaAluno contendo os dados para o certificado.</param>
    /// <returns>Uma instância de IQueueProducerService com a ação de certificado configurada.</returns>
    IQueueProducerService Certificado(FichaAluno fichaAluno);

    /// <summary>
    /// Executa a ação configurada anteriormente (Contrato, Boleto ou Certificado) para enviar a mensagem para a fila.
    /// </summary>
    /// <returns>Uma tarefa assíncrona que representa a conclusão do envio.</returns>
    Task Send();
}
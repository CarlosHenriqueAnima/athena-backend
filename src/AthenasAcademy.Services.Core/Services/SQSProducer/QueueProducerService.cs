using AthenasAcademy.Services.Core.CrossCutting;

namespace AthenasAcademy.Services.Core.Services.SQSProducer;

public class QueueProducerService : IQueueProducerService
{
    private FichaAluno _fichaAluno;

    public IQueueProducerService Contrato(FichaAluno fichaAluno)
    {
        _fichaAluno = fichaAluno;
        return this;
    }

    public IQueueProducerService Boleto(FichaAluno fichaAluno)
    {
        _fichaAluno = fichaAluno;
        return this;
    }

    public IQueueProducerService Certificado(FichaAluno fichaAluno)
    {
        _fichaAluno = fichaAluno;
        return this;
    }

    public async Task Send()
    {
        if (_fichaAluno == null)
            throw new InvalidOperationException("Ficha de aluno não definida. Chame Contrato, Boleto ou Certificado antes de chamar Send.");

        _fichaAluno = null;

        await Task.Yield();
    }
}
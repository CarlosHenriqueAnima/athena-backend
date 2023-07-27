using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using AthenasAcademy.Services.Core.CrossCutting;
using AthenasAcademy.Services.Core.Exceptions;
using AthenasAcademy.Services.Domain.MessageEvents;
using System.Text.Json;

namespace AthenasAcademy.Services.Core.Services.SQSProducer;

public class QueueProducerService : IQueueProducerService
{
    private readonly IConfiguration _configuration;

    public QueueProducerService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool> GerarContrato(FichaAluno fichaAluno)
    {
        string queueUrl = _configuration["AwsSQSFilaContratotBase"];

        if (string.IsNullOrEmpty(queueUrl))
            throw new InvalidOperationException("Não foi possível gerar o contrato. A fila não está configurada.");

        try
        {
            using AmazonSQSClient cliente = GetClient(queueUrl);
            SendMessageRequest request = new()
            {
                QueueUrl = queueUrl,
                MessageBody = GerarContratoEventMessage(fichaAluno)
            };

            await cliente.SendMessageAsync(request);
            return true;
        }
        catch (Exception ex)
        {
            throw new APICustomException(
                message: $"Erro ao enviar mensagem para a fila. {ex}",
                responseType: Domain.Configurations.Enums.ExceptionResponseType.Error,
                statusCode: System.Net.HttpStatusCode.InternalServerError);
        }
    }

    public async Task<bool> GerarBoleto(FichaAluno fichaAluno)
    {
        string queueUrl = _configuration["AwsSQSFilaPagamentotBase"];

        if (string.IsNullOrEmpty(queueUrl))
            throw new InvalidOperationException("Não foi possível gerar o contrato. A fila não está configurada.");

        try
        {
            using AmazonSQSClient cliente = GetClient(queueUrl);
            SendMessageRequest request = new()
            {
                QueueUrl = queueUrl,
                MessageBody = GerarBoletoEventMessage(fichaAluno)
            };

            await cliente.SendMessageAsync(request);
            return true;
        }
        catch (Exception ex)
        {
            throw new APICustomException(
                message: $"Erro ao enviar mensagem para a fila. {ex}",
                responseType: Domain.Configurations.Enums.ExceptionResponseType.Error,
                statusCode: System.Net.HttpStatusCode.InternalServerError);
        }
    }

    private AmazonSQSClient GetClient(string queueUrl)
    {
        string accessKey = _configuration["AwsAccessKey"];
        string secretKey = _configuration["AwsSecretKey"];

        if (string.IsNullOrEmpty(accessKey) || string.IsNullOrEmpty(secretKey))
            throw new InvalidOperationException("As chaves de acesso da AWS não foram configuradas corretamente no arquivo appsettings.json.");

        AmazonSQSConfig sqsConfig = new()
        {
            ServiceURL = queueUrl,
            RegionEndpoint = RegionEndpoint.USWest2
        };
        return new AmazonSQSClient(accessKey, secretKey, sqsConfig);
    }

    private static string GerarBoletoEventMessage(FichaAluno fichaAluno)
    {
        return JsonSerializer.Serialize(new BoletoEventMessage
        {
            CodigoInscricao = fichaAluno.DetalhesFicha.CodigoInscricao,
            DataVencimento = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy"),
            ValorDocumento = fichaAluno.Contrato.ValorContrato,
            Pagador = new Pagador
            {
                Nome = $"{fichaAluno.Aluno.Nome} {fichaAluno.Aluno.Sobrenome}",
                CPF = fichaAluno.Aluno.CPF,
                Logradouro = fichaAluno.Endereco.Logradouro,
                Numero = fichaAluno.Endereco.Numero,
                Complemento = fichaAluno.Endereco.Complemento,
                Bairro = fichaAluno.Endereco.Bairro,
                CEP = fichaAluno.Endereco.CEP,
                UF = fichaAluno.Endereco.UF
            }
        });
    }

    private static string GerarContratoEventMessage(FichaAluno fichaAluno)
    {
        return JsonSerializer.Serialize(new ContratoMessageEvent
        {
            CodigoContrato = fichaAluno.Contrato.NumeroContrato,
            ValorContrato = fichaAluno.Contrato.ValorContrato,
            Aluno = new()
            {
                Nome = $"{fichaAluno.Aluno.Nome} {fichaAluno.Aluno.Sobrenome}",
                CPF = fichaAluno.Aluno.CPF,
                Sexo = fichaAluno.Aluno.Sexo == 'M' ? "Masculino" :
                       fichaAluno.Aluno.Sexo == 'F' ? "Feminino" : "Outros",
                DataNascimento = fichaAluno.Aluno.DataNascimento.ToString("dd/MM/yyyy"),
                Email = fichaAluno.Aluno.Email.Trim().Normalize().ToLower()
            },
            Curso = new()
            {
                Nome = $"{fichaAluno.OpcaoCurso.Id} - {fichaAluno.OpcaoCurso.Nome}",
                CargaHoraria = fichaAluno.OpcaoCurso.CargaHoraria
            }
        });
    }
}
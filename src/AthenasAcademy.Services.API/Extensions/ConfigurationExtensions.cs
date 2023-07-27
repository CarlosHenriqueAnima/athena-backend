using Amazon.Runtime.Internal.Transform;
using AthenasAcademy.Services.Core.Configurations.Credentials;

namespace AthenasAcademy.Services.API.Extensions;

/// <summary>
/// Classe de extensão para a interface IConfigurationBuilder que permite adicionar as credenciais lidas do arquivo de configuração.
/// </summary>
public static class ConfigurationExtensions
{
    /// <summary>
    /// Adiciona as credenciais lidas do arquivo de configuração à coleção de configuração.
    /// </summary>
    /// <param name="configuration">O construtor da configuração.</param>
    /// <returns>O construtor da configuração com as credenciais adicionadas.</returns>
    public static IConfigurationBuilder AddReadCredentials(this IConfigurationBuilder configuration)
    {
        var credenciais = ReadCredentials.GetCredentials();

        configuration.AddInMemoryCollection(new Dictionary<string, string>
        {
            {"AwsAccessKey", credenciais.AwsAccessKey},
            {"AwsSecretKey", credenciais.AwsSecretKey},
            {"AwsBucketBase", credenciais.AwsBucketBase},
            {"AwsSQSFilaCertificadotBase", credenciais.AwsSQSFilaCertificadotBase},
            {"AwsSQSFilaContratotBase", credenciais.AwsSQSFilaContratotBase},
            {"AwsSQSFilaPagamentotBase", credenciais.AwsSQSFilaPagamentotBase},
            {"LegadoAwsSecretKeyBase", credenciais.ClientLegadoAwsSecretKey},
            {"JwtKeyBase", credenciais.JwtSecretKey},
            {"AthenasBase", credenciais.StringConnectionAthenasBase},
            {"UsuarioBase", credenciais.StringConnectionBaseUsuario },
            {"InscricaoBase", credenciais.StringConnectionBaseInscricao},
            {"AlunoBase", credenciais.StringConnectionBaseAluno},
            {"MatriculaBase", credenciais.StringConnectionBaseMatricula},
            {"PagamentoBase", credenciais.StringConnectionBasePagamento},
            {"CursoBase", credenciais.StringConnectionBaseCurso},
            {"CertificadoBase", credenciais.StringConnectionBaseCertificado}
        });

        return configuration;
    }
}

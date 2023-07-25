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
        // Lê as credenciais do arquivo de configuração
        var credenciais = ReadCredentials.GetCredentials();

        // Adiciona as credenciais à coleção de configuração em memória
        configuration.AddInMemoryCollection(new Dictionary<string, string>
        {
            {"AwsAccessKey", credenciais.AwsAccessKey},
            {"AwsSecretKey", credenciais.AwsSecretKey},
            {"AwsBucketBase", credenciais.AwsBucketBase},
            {"InscricaoBase", credenciais.StringConnectionBaseInscricao},
            {"AlunoBase", credenciais.StringConnectionBaseAluno},
            {"MatriculaBase", credenciais.StringConnectionBaseMatricula},
            {"PagamentoBase", credenciais.StringConnectionBasePagamento},
            {"CursoBase", credenciais.StringConnectionBaseCurso},
            {"CertificadoBase", credenciais.StringConnectionBaseCertificado}
        });

        // Retorna o construtor da configuração com as credenciais adicionadas
        return configuration;
    }
}

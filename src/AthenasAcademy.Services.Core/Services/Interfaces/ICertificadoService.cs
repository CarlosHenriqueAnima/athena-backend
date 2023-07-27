namespace AthenasAcademy.Services.Core.Services.Interfaces;

/// <summary>
/// Interface para o serviço de manipulação de certificados.
/// </summary>
public interface ICertificadoService
{
    /// <summary>
    /// Gera um certificado para o aluno com base na matrícula fornecida.
    /// </summary>
    /// <param name="matricula">O número de matrícula do aluno.</param>
    /// <returns>
    /// Uma tarefa que representa a operação assíncrona de geração do certificado.
    /// O resultado da tarefa é uma string contendo o caminho ou o nome do arquivo do certificado gerado.
    /// </returns>
    Task<string> GerarCertificado(int matricula);

    /// <summary>
    /// Obtém o certificado para o aluno com base na matrícula fornecida.
    /// </summary>
    /// <param name="matricula">O número de matrícula do aluno.</param>
    /// <returns>
    /// Uma tarefa que representa a operação assíncrona de obtenção do certificado em formato MemoryStream.
    /// O resultado da tarefa é um MemoryStream contendo o conteúdo do certificado.
    /// </returns>
    Task<MemoryStream> ObterCertificado(int matricula);
}
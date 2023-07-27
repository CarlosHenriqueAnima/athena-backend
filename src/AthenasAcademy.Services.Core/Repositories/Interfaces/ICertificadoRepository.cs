using AthenasAcademy.Services.Core.Arguments;
using AthenasAcademy.Services.Core.Models;

namespace AthenasAcademy.Services.Core.Repositories.Interfaces;

/// <summary>
/// Interface responsável por definir o contrato para o repositório de certificados.
/// </summary>
public interface ICertificadoRepository
{
    /// <summary>
    /// Gera um certificado com base na matrícula fornecida.
    /// </summary>
    /// <param name="matricula">A matrícula do aluno para a qual o certificado será gerado.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona de geração do certificado, que retorna um objeto do tipo <see cref="CertificadoModel"/>.</returns>
    Task<CertificadoModel> GerarCertificado(NovoCertificadoArgument matricula);

    /// <summary>
    /// Obtém o certificado associado à matrícula fornecida.
    /// </summary>
    /// <param name="matricula">A matrícula do aluno para a qual o certificado será obtido.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona de obtenção do certificado, que retorna um objeto do tipo <see cref="CertificadoModel"/>.</returns>
    Task<CertificadoModel> ObterCertificado(int matricula);
}
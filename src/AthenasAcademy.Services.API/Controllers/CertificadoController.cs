using AthenasAcademy.Services.Core.Responses;
using AthenasAcademy.Services.Core.Services.Interfaces;
using AthenasAcademy.Services.Domain.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AthenasAcademy.Services.API.Controllers;

/// <summary>
/// Controlador responsável por gerar e obter certificados.
/// </summary>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
[Authorize]
public class CertificadoController : ControllerBase
{
    /// TODO: Problemas com token CertificadoController

    private readonly ICertificadoService _certificadoService;

    /// <summary>
    /// Construtor da classe CertificadoController.
    /// </summary>
    /// <param name="certificadoService">Instância do serviço de certificados.</param>
    public CertificadoController(ICertificadoService certificadoService)
    {
        _certificadoService = certificadoService;
    }

    /// <summary>
    /// Endpoint para gerar um certificado em PDF com base no contrato fornecido.
    /// </summary>
    /// <param name="matricula">Número da matricula para o qual o certificado será gerado.</param>
    /// <returns>Retorna um IActionResult que contém o certificado gerado.</returns>
    [HttpPost("gerar/{matricula:int}")]
    [AllowAnonymous]
    //[Authorize(Roles = "Aluno, Admnistrador")]
    [ProducesResponseType(typeof(NovoCertificadoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GerarCertificado(int matricula)
    {
        return Ok(await _certificadoService.GerarCertificado(matricula));
    }

    /// <summary>
    /// Endpoint para obter o certificado em PDF com base no contrato fornecido.
    /// </summary>
    /// <param name="matricula">Número da matricula para o qual o certificado será obtido.</param>
    /// <returns>Retorna um IActionResult que contém o certificado em formato PDF.</returns>
    [HttpGet("obter/{matricula:int}")]
    //[Authorize(Roles = "Aluno, Admnistrador")]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ObterCertificado(int matricula)
    {
        return File(
            fileStream: await _certificadoService.ObterCertificado(matricula),
            contentType: "application/pdf", 
            fileDownloadName: $"certificado_{matricula}.pdf");
    }
}
using AthenasAcademy.Services.Core.Services.Interfaces;
using AthenasAcademy.Services.Domain.Responses;
using Microsoft.AspNetCore.Mvc;

namespace AthenasAcademy.Services.API.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class InscricaoController : ControllerBase
{
    private readonly IInscricaoService _inscricaoService;

    public InscricaoController(IInscricaoService inscricaoService)
    {
        _inscricaoService = inscricaoService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public IActionResult GetInscricao()
    {
        return Ok();
    }
}
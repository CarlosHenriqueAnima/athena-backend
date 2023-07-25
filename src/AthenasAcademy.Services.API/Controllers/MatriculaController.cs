using AthenasAcademy.Services.Core.Services.Interfaces;
using AthenasAcademy.Services.Domain.Responses;
using Microsoft.AspNetCore.Mvc;

namespace AthenasAcademy.Services.API.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class MatriculaController : ControllerBase
{
    private readonly IMatriculaService _matriculaService;

    public MatriculaController(IMatriculaService matriculaService)
    {
        _matriculaService = matriculaService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public IActionResult GetMatricula()
    {
        return Ok();
    }
}
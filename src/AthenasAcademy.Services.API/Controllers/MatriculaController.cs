using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AthenasAcademy.Services.API.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class MatriculaController : ControllerBase
{
    private readonly MatriculaService _matriculaService;

    public MatriculaController(MatriculaService matriculaService)
    {
        _matriculaService = matriculaService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AlunoDetalhesModel>> ObterMatriculaPorId(int id)
    {
        var matricula = await _matriculaService.ObterMatriculaPorId(id);
        if (matricula == null)
        {
            return NotFound();
        }

        return matricula;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AlunoDetalhesModel>>> ObterTodasMatriculas()
    {
        var matriculas = await _matriculaService.ObterTodasMatriculas();
        return Ok(matriculas);
    }

    [HttpPost]
    public async Task<ActionResult> AdicionarMatricula([FromBody] AlunoDetalhesModel matricula)
    {
        await _matriculaService.AdicionarMatricula(matricula);
        return CreatedAtAction(nameof(ObterMatriculaPorId), new { id = matricula.Id }, matricula);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarMatricula(int id, [FromBody] AlunoDetalhesModel matricula)
    {
        if (id != matricula.Id)
        {
            return BadRequest();
        }

        await _matriculaService.AtualizarMatricula(matricula);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> CancelarMatricula(int id)
    {
        var matricula = await _matriculaService.ObterMatriculaPorId(id);
        if (matricula == null)
        {
            return NotFound();
        }

        await _matriculaService.CancelarMatricula(id);
        return NoContent();
    }
}
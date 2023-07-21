using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AthenasAcademy.Services.API.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class InscricaoController : ControllerBase
{
    private readonly InscricaoService _inscricaoService;

    public InscricaoController(InscricaoService inscricaoService)
    {
        _inscricaoService = inscricaoService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CandidatoModel>> ObterInscricaoPorIdAsync(int id)
    {
        var inscricao = await _inscricaoService.ObterInscricaoPorIdAsync(id);
        if (inscricao == null)
        {
            return NotFound();
        }

        return inscricao;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CandidatoModel>>> ObterTodasInscricoesAsync()
    {
        var inscricoes = await _inscricaoService.ObterTodasInscricoesAsync();
        return Ok(inscricoes);
    }

    [HttpPost]
    public async Task<ActionResult> AdicionarInscricaoAsync([FromBody] CandidatoModel inscricao)
    {
        await _inscricaoService.AdicionarInscricaoAsync(inscricao);
        return CreatedAtAction(nameof(ObterInscricaoPorIdAsync), new { id = inscricao.Id }, inscricao);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarInscricaoAsync(int id, [FromBody] CandidatoModel inscricao)
    {
        if (id != inscricao.Id)
        {
            return BadRequest();
        }

        await _inscricaoService.AtualizarInscricaoAsync(inscricao);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> CancelarInscricaoAsync(int id)
    {
        var inscricao = await _inscricaoService.ObterInscricaoPorIdAsync(id);
        if (inscricao == null)
        {
            return NotFound();
        }

        await _inscricaoService.CancelarInscricaoAsync(id);
        return NoContent();
    }
}

using System;
using AthenasAcademy.Services.Core.Services.Interfaces;
using AthenasAcademy.Services.Domain.Requests;
using AthenasAcademy.Services.Domain.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AthenasAcademy.Services.API.Controllers
{
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
        [ProducesResponseType(typeof(MatriculaResponse[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllMatriculas()
        {
            var matriculas = _matriculaService.GetAllMatriculas();
            return Ok(matriculas);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(MatriculaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
        public IActionResult GetMatriculaById(int id)
        {
            var matricula = _matriculaService.GetMatriculaById(id);
            if (matricula == null)
            {
                return NotFound(new ExceptionResponse { Message = "Matrícula não encontrada." });
            }

            return Ok(matricula);
        }

        [HttpPost]
        [ProducesResponseType(typeof(MatriculaResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
        public IActionResult CreateMatricula([FromBody] MatriculaRequest request)
        {
            try
            {
                var matricula = _matriculaService.CreateMatricula(request.ContratoId, request.DetalheContratoId, request.CodigoMatricula);
                return CreatedAtAction(nameof(GetMatriculaById), new { id = matricula.Id }, matricula);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionResponse { Message = ex.Message });
            }
        }
    }
}

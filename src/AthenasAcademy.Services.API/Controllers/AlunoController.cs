using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AthenasAcademy.Services.API.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class AlunoController : ControllerBase
{
    [HttpGet]
    public IActionResult Teste()
    {
        return Ok();
    }
}
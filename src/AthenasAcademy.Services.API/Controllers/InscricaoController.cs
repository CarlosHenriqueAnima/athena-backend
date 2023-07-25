using Microsoft.AspNetCore.Mvc;

namespace AthenasAcademy.Services.API.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class InscricaoController : ControllerBase
{
    public async Task<IActionResult> Get()
    {
        return Ok();
    }
}

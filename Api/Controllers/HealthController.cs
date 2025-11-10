using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {// En HealthController, temporal:
        [HttpGet("boom")]
        public IActionResult Boom() => throw new Exception("Excepción de prueba");
    }
}
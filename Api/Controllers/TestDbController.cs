using Microsoft.AspNetCore.Mvc;
using Api.Infrastructure.Persistence;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestDbController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TestDbController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult ProbarConexion()
        {
            try
            {
                bool puedeConectar = _context.Database.CanConnect();
                return Ok(new { conectado = puedeConectar });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al conectar con la base de datos", detalle = ex.Message });
            }
        }
    }
}

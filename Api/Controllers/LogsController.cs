using Api.Entities;
using Api.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LogsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetLogs(
            [FromQuery] string? tipo,
            [FromQuery] DateTime? from,
            [FromQuery] DateTime? to,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            // Base query
            var query = _context.LogsApi.AsQueryable();

            // Filtros opcionales
            if (!string.IsNullOrEmpty(tipo))
                query = query.Where(l => l.TipoLog == tipo);

            if (from.HasValue)
                query = query.Where(l => l.Fecha >= from.Value);

            if (to.HasValue)
                query = query.Where(l => l.Fecha <= to.Value);

            // Total de registros
            var total = await query.CountAsync();

            // Paginación
            var logs = await query
                .OrderByDescending(l => l.Fecha)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new
            {
                total,
                data = logs
            });
        }

        [HttpPost]
        public async Task<IActionResult> AgregarLog([FromBody] LogApi log)
        {
            if (log == null)
                return BadRequest(new { mensaje = "El log no puede ser nulo." });

            log.Fecha = DateTime.UtcNow;
            _context.LogsApi.Add(log);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Log registrado correctamente." });
        }
    }
}

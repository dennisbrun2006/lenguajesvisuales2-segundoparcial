using Api.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO.Compression;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ClientesController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // ============================================================
        // POST: Registrar cliente + fotos
        // ============================================================
        /// <summary>
        /// Registra un nuevo cliente y permite subir hasta 3 fotos.
        /// </summary>
        /// <param name="ci">Cédula del cliente</param>
        /// <param name="nombres">Nombres y apellidos</param>
        /// <param name="direccion">Dirección</param>
        /// <param name="telefono">Teléfono</param>
        /// <param name="fotos">Hasta 3 fotos de la casa</param>
        [HttpPost("registrar")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> RegistrarClienteConFotos(
            [FromForm] string ci,
            [FromForm] string nombres,
            [FromForm] string direccion,
            [FromForm] string telefono,
            [FromForm] List<IFormFile>? fotos)
        {
            try
            {
                // Validar duplicado
                if (await _context.Clientes.AnyAsync(c => c.Ci == ci))
                    return Conflict(new { mensaje = "Ya existe un cliente con este CI." });

                // Crear cliente
                var cliente = new Entities.Cliente
                {
                    Ci = ci,
                    Nombres = nombres,
                    Direccion = direccion,
                    Telefono = telefono
                };

                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();

                // Subida de fotos
                if (fotos != null && fotos.Count > 0)
                {
                    string carpetaFotos = Path.Combine(_env.WebRootPath, "fotos_clientes", ci);
                    if (!Directory.Exists(carpetaFotos))
                        Directory.CreateDirectory(carpetaFotos);

                    int contador = 1;
                    foreach (var foto in fotos)
                    {
                        string extension = Path.GetExtension(foto.FileName);
                        string nombreArchivo = $"foto_{contador}{extension}";
                        string ruta = Path.Combine(carpetaFotos, nombreArchivo);

                        using (var stream = new FileStream(ruta, FileMode.Create))
                        {
                            await foto.CopyToAsync(stream);
                        }

                        // Guardar URL
                        switch (contador)
                        {
                            case 1: cliente.FotoCasa1Url = $"/fotos_clientes/{ci}/{nombreArchivo}"; break;
                            case 2: cliente.FotoCasa2Url = $"/fotos_clientes/{ci}/{nombreArchivo}"; break;
                            case 3: cliente.FotoCasa3Url = $"/fotos_clientes/{ci}/{nombreArchivo}"; break;
                        }

                        contador++;
                        if (contador > 3) break;
                    }

                    await _context.SaveChangesAsync();
                }

                return Ok(new { mensaje = "Cliente registrado correctamente con fotos." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al registrar cliente", detalle = ex.Message });
            }
        }

        // ============================================================
        // POST: Subir ZIP de cliente
        // ============================================================
        /// <summary>
        /// Sube un archivo ZIP asociado a un cliente (según su CI) y lo descomprime automáticamente.
        /// </summary>
        /// <param name="ci">CI del cliente</param>
        /// <param name="archivoZip">Archivo ZIP a subir</param>
        [HttpPost("{ci}/archivos/zip")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> SubirZipCliente(string ci, IFormFile archivoZip)
        {
            try
            {
                var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Ci == ci);
                if (cliente == null)
                    return NotFound(new { mensaje = "Cliente no encontrado" });

                if (archivoZip == null || archivoZip.Length == 0)
                    return BadRequest(new { mensaje = "No se envió ningún archivo ZIP" });

                string carpetaCliente = Path.Combine(_env.WebRootPath, "archivos_clientes", ci);
                if (!Directory.Exists(carpetaCliente))
                    Directory.CreateDirectory(carpetaCliente);

                string zipPath = Path.Combine(carpetaCliente, archivoZip.FileName);
                using (var stream = new FileStream(zipPath, FileMode.Create))
                {
                    await archivoZip.CopyToAsync(stream);
                }

                // Extraer y borrar el ZIP
                ZipFile.ExtractToDirectory(zipPath, carpetaCliente, overwriteFiles: true);
                System.IO.File.Delete(zipPath);

                return Ok(new { mensaje = "Archivo ZIP subido y extraído correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al subir ZIP", detalle = ex.Message });
            }
        }

        // ============================================================
        // GET: Todos los clientes
        // ============================================================
        [HttpGet]
        public async Task<IActionResult> GetClientes()
        {
            var clientes = await _context.Clientes
                .Include(c => c.ArchivosCliente)
                .ToListAsync();

            return Ok(clientes);
        }

        // ============================================================
        // GET: Cliente por CI
        // ============================================================
        [HttpGet("{ci}")]
        public async Task<IActionResult> GetClientePorCi(string ci)
        {
            var cliente = await _context.Clientes
                .Include(c => c.ArchivosCliente)
                .FirstOrDefaultAsync(c => c.Ci == ci);

            if (cliente == null)
                return NotFound(new { mensaje = "Cliente no encontrado" });

            return Ok(cliente);
        }
    }
}

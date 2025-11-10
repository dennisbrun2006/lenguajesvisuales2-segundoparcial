using System.Net;
using System.Text;
using Api.Entities;
using Api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Api.Middlewares
{
    public class ExceptionLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, AppDbContext db)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // Captura del cuerpo de la solicitud
                context.Request.EnableBuffering();
                string requestBody = "";
                using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true))
                {
                    requestBody = await reader.ReadToEndAsync();
                    context.Request.Body.Position = 0;
                }

                // Registro del log de error
                var log = new LogApi
                {
                    TipoLog = "Error",
                    MetodoHttp = context.Request.Method,
                    UrlEndpoint = $"{context.Request.Path}{context.Request.QueryString}",
                    DireccionIp = context.Connection.RemoteIpAddress?.ToString(),
                    RequestBody = requestBody,
                    ResponseBody = ex.ToString(),
                    Detalle = ex.Message,
                    Fecha = DateTime.UtcNow
                };

                db.LogsApi.Add(log);
                await db.SaveChangesAsync();

                // Respuesta genérica al cliente
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync($@"
                {{
                    ""status"": 500,
                    ""mensaje"": ""Ha ocurrido un error interno en el servidor."",
                    ""detalle"": ""{ex.Message}""
                }}");
            }
        }
    }
}

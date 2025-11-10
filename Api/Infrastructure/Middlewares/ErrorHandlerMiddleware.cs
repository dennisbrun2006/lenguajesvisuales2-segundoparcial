using System.Net;

namespace Api.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var result = new
                {
                    status = 500,
                    mensaje = "Ha ocurrido un error interno en el servidor.",
                    detalle = ex.Message
                };

                await context.Response.WriteAsJsonAsync(result);
            }
        }
    }
}

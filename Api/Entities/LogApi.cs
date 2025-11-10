using System;

namespace Api.Entities
{
    public class LogApi
    {
        public int Id { get; set; }
        public string TipoLog { get; set; } = "Info"; // Ej: "Error", "Info", etc.
        public string MetodoHttp { get; set; }        // Ej: "GET", "POST"
        public string UrlEndpoint { get; set; }       // Ruta del endpoint
        public string DireccionIp { get; set; }       // IP del cliente
        public string RequestBody { get; set; }       // JSON del request
        public string ResponseBody { get; set; }      // JSON del response
        public string Detalle { get; set; }           // Mensaje de error o detalle
        public DateTime Fecha { get; set; }
    }
}

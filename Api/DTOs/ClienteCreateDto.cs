using Microsoft.AspNetCore.Http;

namespace Api.DTOs
{
    public class ClienteCreateDto
    {
        public string CI { get; set; } = null!;
        public string Nombres { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public IFormFile? FotoCasa1 { get; set; }
        public IFormFile? FotoCasa2 { get; set; }
        public IFormFile? FotoCasa3 { get; set; }
    }
}

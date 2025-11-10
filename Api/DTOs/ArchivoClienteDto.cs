namespace Api.DTOs
{
    public class ArchivoClienteDto
    {
        public int Id { get; set; }
        public string NombreArchivo { get; set; } = null!;
        public string UrlArchivo { get; set; } = null!;
        public string Extension { get; set; } = null!;
    }
}

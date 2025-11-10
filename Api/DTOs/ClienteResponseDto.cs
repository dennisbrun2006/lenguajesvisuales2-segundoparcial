namespace Api.DTOs
{
    public class ClienteResponseDto
    {
        public int Id { get; set; }
        public string CI { get; set; } = null!;
        public string Nombres { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string? FotoCasa1Url { get; set; }
        public string? FotoCasa2Url { get; set; }
        public string? FotoCasa3Url { get; set; }
        // No devolvemos la colección de Archivos aquí para evitar esquemas recursivos
    }
}

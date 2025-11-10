using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Entities
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }
        public string Ci { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string? FotoCasa1Url { get; set; }
        public string? FotoCasa2Url { get; set; }
        public string? FotoCasa3Url { get; set; }

        public List<ArchivoCliente> ArchivosCliente { get; set; } = new();
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Entities
{
    public class ArchivoCliente
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Cliente")]
        public int ClienteId { get; set; }

        public string NombreArchivo { get; set; } = string.Empty;
        public string UrlArchivo { get; set; } = string.Empty;
        public string Extension { get; set; } = string.Empty;
        public long TamanoBytes { get; set; }
        public DateTime FechaSubida { get; set; } = DateTime.UtcNow;

        public Cliente? Cliente { get; set; }
    }
}

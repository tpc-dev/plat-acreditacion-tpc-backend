using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.Entidades
{
    public class EventoEnvio 
    {
        [Required]
        public int Id { get; set; }
        [MaxLength(150)]
        public string Nombre { get; set; }
        [MaxLength(13)]
        public string Rut { get; set; }
        [MaxLength(1)]
        public string DvRut { get; set; }
        public DateTime FechaEvento { get; set; }
        public string? TipoEvento { get; set; }
        [MaxLength(1)]
        public string EnviadoDt { get; set; }
        public int? TipoRespuesta { get; set; }
        public Guid? GuidAccessPoint { get; set; }
    }
}

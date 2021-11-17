using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.DTOs
{
    public class IngresoVisitaDTO
    {
        [Required]
        public string Tipo { get; set; }
        [Required]
        public DateTime FechaEvento { get; set; }
        [Required]
        public int VisitaId { get; set; }
    }
}

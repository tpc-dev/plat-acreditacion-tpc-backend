using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.Entidades
{
    public class IngresoVisitas
    {
        [Required]
        public int Id {  get; set; }
        [Required]
        public string Tipo { get; set; }
        [Required]
        public DateTime FechaEvento { get; set; }
        [Required]
        public int VisitaId { get; set; }
        public Visita Visita { get; set; }
    }
}

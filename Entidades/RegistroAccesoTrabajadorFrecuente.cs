using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.Entidades
{
    public class RegistroAccesoTrabajadorFrecuente
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime FechaEvento { get; set; }
        [Required]
        public string TipoEvento { get; set; }
        [Required]
        public int NombradaDiariaId { get; set; }
        public NombradaDiaria NombradaDiaria{ get; set; }
        [Required]
        public int TrabajadorFrecuenteId { get; set; }
        public TrabajadorFrecuente TrabajadorFrecuente { get; set; }
    }
}

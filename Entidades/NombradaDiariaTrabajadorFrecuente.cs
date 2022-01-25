using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.Entidades
{
    public class NombradaDiariaTrabajadorFrecuente
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int NombradaDiariaId { get; set; }
        public NombradaDiaria NombradaDiaria { get; set; }
        [Required]
        public int TrabajadorFrecuenteId { get; set; }
        public TrabajadorFrecuente TrabajadorFrecuente { get; set; }
    }
}

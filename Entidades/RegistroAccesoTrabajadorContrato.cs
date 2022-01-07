using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.Entidades
{
    public class RegistroAccesoTrabajadorContrato
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime FechaEvento { get; set; }
        [Required]
        public string TipoEvento { get; set; }
        [Required]
        public int ContratoTrabajadorContratoId { get; set; }
        public Contrato Contrato { get; set; }
        [Required]
        public int ContratoTrabajadorTrabajadorId { get; set; }
        public Trabajador Trabajador { get; set; }
    }
}

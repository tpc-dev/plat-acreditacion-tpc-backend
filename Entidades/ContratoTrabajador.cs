using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.Entidades
{
    public class ContratoTrabajador
    {
        [Required]
        public int ContratoId { get; set; }
        public Contrato Contrato { get; set; }
        [Required]
        public int TrabajadorId { get; set; }
        public Trabajador Trabajador { get; set; }
        [Required]
        public int CargoId { get; set; }
        public Cargo Cargo { get; set; }
        [Required]
        public int TurnoId { get; set; }
        public Turno Turno { get; set; }
        public int EstadoAcreditacionId { get; set; }
        public EstadoAcreditacion EstadoAcreditacion { get; set; }
        public List<TrabajadorTipoDocumentoAcreditacion> ListTrabajadorTiposDocumentoAcreditacion { get; set; }
        public List<RegistroAccesoTrabajadorContrato> RegistrosAccesosTrabajadorContrato { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.Entidades
{
    public class RegistroAccesoVehiculoContrato
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime FechaEvento { get; set; }
        [Required]
        public string TipoEvento { get; set; }
        [Required]
        public int ContratoVehiculoContratoId { get; set; }
        public Contrato Contrato { get; set; }
        [Required]
        public int ContratoVehiculoVehiculoId { get; set; }
        public Vehiculo Vehiculo { get; set; }
    }
}

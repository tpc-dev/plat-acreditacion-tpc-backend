using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.Entidades
{
    public class VehiculoTipoDocumentoAcreditacion
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int TipoDocumentoAcreditacionId { get; set; }
        public TipoDocumentoAcreditacion TipoDocumentoAcreditacion { get; set; }
        [Required]
        public int ContratoVehiculoContratoId { get; set; }
        public Contrato Contrato { get; set; }
        [Required]
        public int ContratoVehiculoVehiculoId { get; set; }
        public Vehiculo Vehiculo { get; set; }
        [Required]
        public int EstadoAcreditacionId { get; set; }
        public EstadoAcreditacion EstadoAcreditacion { get; set; }
        public List<HistoricoAcreditacionVehiculoTipoDocumentoAcreditacion> ListHistoricosAcreditacionVehiculoTipoDocumentoAcreditacion { get; set; }
        [Required]
        public DateTime FechaInicio { get; set; }
        [Required]
        public DateTime FechaTermino { get; set; }
        [Required]
        public string UrlFile { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

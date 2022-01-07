using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.Entidades
{
    public class HistoricoAcreditacionVehiculoTipoDocumentoAcreditacion
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int VehiculoTipoDocumentoAcreditacionId { get; set; }
        public VehiculoTipoDocumentoAcreditacion VehiculoTipoDocumentoAcreditacion { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public int EstadoAcreditacionId { get; set; }
        public EstadoAcreditacion EstadoAcreditacion { get; set; }
    }
}

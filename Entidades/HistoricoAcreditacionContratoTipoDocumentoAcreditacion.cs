using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.Entidades
{
    public class HistoricoAcreditacionContratoTipoDocumentoAcreditacion
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int ContratoTipoDocumentoAcreditacionId { get; set; }
        public ContratoTipoDocumentoAcreditacion ContratoTipoDocumentoAcreditacion { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public int EstadoAcreditacionId { get; set; }
        public EstadoAcreditacion EstadoAcreditacion { get; set; }
    }
}

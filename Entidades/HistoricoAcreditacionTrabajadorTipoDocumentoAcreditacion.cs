using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.Entidades
{
    public class HistoricoAcreditacionTrabajadorTipoDocumentoAcreditacion
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int TrabajadorTipoDocumentoAcreditacionId { get; set; }
        public TrabajadorTipoDocumentoAcreditacion TrabajadorTipoDocumentoAcreditacion { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public int EstadoAcreditacionId { get; set; }
        public EstadoAcreditacion EstadoAcreditacion { get; set; }
    }
}

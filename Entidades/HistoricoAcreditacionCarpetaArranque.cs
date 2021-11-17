using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.Entidades
{
    public class HistoricoAcreditacionCarpetaArranque
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public int EstadoAcreditacionId { get; set; }
        public EstadoAcreditacion EstadoAcreditacion { get; set; }
        [Required]
        public int CarpetaArranqueId { get; set; }
        public CarpetaArranque CarpetaArranque { get; set; }
    }
}

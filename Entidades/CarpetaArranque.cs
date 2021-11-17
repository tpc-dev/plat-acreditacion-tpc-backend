using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.Entidades
{
    public class CarpetaArranque
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime FechaInicio { get; set; }
        [Required]
        public int HistoricoAcreditacionCarpetaArranqueId { get; set; }
        public HistoricoAcreditacionCarpetaArranque HistoricoAcreditacionCarpetaArranque { get; set; }
        [Required]
        public int ContratoId { get; set; }
        public Contrato Contrato { get; set; }
        [Required]
        public int ItemCarpetaArranqueId { get; set; }
        public ItemCarpetaArranque ItemCarpetaArranque { get; set; }
    }
}

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
        public int ContratoId { get; set; }
        public Contrato Contrato { get; set; }
        public List<ItemCarpetaArranqueCarpetaArranque> ItemsCarpetaArranqueCarpetaArranque { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }
    }
}

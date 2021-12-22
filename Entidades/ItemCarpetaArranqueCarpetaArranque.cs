using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.Entidades
{
    public class ItemCarpetaArranqueCarpetaArranque
    {
        [Required]
        public int CarpetaArranqueId { get; set; }
        public CarpetaArranque CarpetaArranque { get; set; }
        [Required]
        public int ItemCarpetaArranqueId { get; set; }
        public ItemCarpetaArranque ItemCarpetaArranque { get; set; }
        [Required]
        public bool Obligatorio { get; set; }
    }
}

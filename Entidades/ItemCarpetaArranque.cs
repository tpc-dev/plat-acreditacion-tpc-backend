using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.Entidades
{
    public class ItemCarpetaArranque
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string  Description { get; set; }
        [Required]
        public string Indice{ get; set; }
        [Required]
        public string Evidencia { get; set; }
        [Required]
        public bool Obligatorio { get; set; }
        public bool Activo { get; set; }
    }
}

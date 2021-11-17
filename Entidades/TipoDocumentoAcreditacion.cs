using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.Entidades
{
    public class TipoDocumentoAcreditacion
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        [Required]
        public int ItemCarpetaArranqueId { get; set; }
        public ItemCarpetaArranque ItemCarpetaArranque { get; set; }
    }
}


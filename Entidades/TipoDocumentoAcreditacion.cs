using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.Entidades
{
    public class TipoDocumentoAcreditacion
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string PerteneA { get; set; }
        public bool Obligatorio { get; set; }
        [Required]
        public int ItemCarpetaArranqueId { get; set; }
        public ItemCarpetaArranque ItemCarpetaArranque { get; set; }  
        [Required]
        public int DocumentoClasificacionId { get; set; }
        public DocumentoClasificacion DocumentoClasificacion { get; set; }
    }
}


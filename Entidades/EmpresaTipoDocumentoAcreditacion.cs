using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.Entidades
{
    public class EmpresaTipoDocumentoAcreditacion
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int TipoDocumentoAcreditacionId { get; set; }
        public TipoDocumentoAcreditacion TipoDocumentoAcreditacion { get; set; }
        [Required]
        public int EstadoAcreditacionId { get; set; }
        public EstadoAcreditacion EstadoAcreditacion { get; set; }

    }
}

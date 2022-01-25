using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.Entidades
{
    public class HistoricoAcreditacionEmpresaTipoDocumentoAcreditacion
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int EstadoAcreditacionId { get; set; }
        public EstadoAcreditacion EstadoAcreditacion { get; set; }
        [Required]
        public int EmpresaTipoDocumentoAcreditacionId { get; set; }
        public EmpresaTipoDocumentoAcreditacion EmpresaTipoDocumentoAcreditacion { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
    }
}

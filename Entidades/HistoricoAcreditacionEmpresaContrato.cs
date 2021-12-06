using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.Entidades
{
    public class HistoricoAcreditacionEmpresaContrato
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int EmpresaContratoId { get; set; }
        public EmpresaContrato EmpresaContrato { get; set; }
        [Required]
        public int EstadoAcreditacionId { get; set; }
        public EstadoAcreditacion EstadoAcreditacion { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
    }
}

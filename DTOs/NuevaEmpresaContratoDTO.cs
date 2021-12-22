using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.DTOs
{
    public class NuevaEmpresaContratoDTO
    {
        [Required]
        public int EmpresaId { get; set; }
        [Required]
        public int ContratoId { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}

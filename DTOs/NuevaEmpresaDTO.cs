using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.DTOs
{
    public class NuevaEmpresaDTO
    {
        [Required]
        public string Rut { get; set; }
        [Required]
        public string RazonSocial { get; set; }
        [Required]
        public int EstadoAcreditacionId { get; set; }
        [Required]
        public bool Activo { get; set; }
    }
}

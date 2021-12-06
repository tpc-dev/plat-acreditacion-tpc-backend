using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.DTOs
{
    public class NuevoEstadoAcreditacionDTO
    {
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public bool Activo { get; set; }
    }
}

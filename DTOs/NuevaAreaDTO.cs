using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.DTOs
{
    public class NuevaAreaDTO
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public bool Activo { get; set; }

    }
}

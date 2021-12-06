using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.DTOs
{
    public class NuevoTipoRolDTO
    {
        [Required]
        public string Nombre { get; set; }
    }
}

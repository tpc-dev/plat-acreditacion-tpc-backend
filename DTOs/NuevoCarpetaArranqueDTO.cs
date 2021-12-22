using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.DTOs
{
    public class NuevoCarpetaArranqueDTO
    {
        [Required]
        public int ContratoId { get; set; }
        [Required]
        public DateTime FechaInicio { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }
    }
}

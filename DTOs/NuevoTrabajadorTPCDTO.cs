using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.DTOs
{
    public class NuevoTrabajadorTPCDTO
    {
        [Required]
        public string Rut { get; set; }
        [Required]
        public string Nombres { get; set; }
        [Required]
        public string ApellidoPaterno { get; set; }
        [Required]
        public string ApellidoMaterno { get; set; }
        [Required]
        public int GerenciaId { get; set; }
        [Required]
        public int GeneroId { get; set; }
        [Required]
        public int EstadoCivilId { get; set; }
        [Required]
        public DateTime FechaNacimiento { get; set; }
        [Required]
        public int NivelEducacionalId { get; set; }
        [Required]
        public int PaisId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

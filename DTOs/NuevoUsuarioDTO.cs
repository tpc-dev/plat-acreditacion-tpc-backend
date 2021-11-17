using System.ComponentModel.DataAnnotations;


namespace PlatAcreditacionTPCBackend.DTOs
{
    public class NuevoUsuarioDTO
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(20)]
        public string Apellido1 { get; set; }
        [Required]
        [MaxLength(20)]
        public string Apellido2 { get; set; }
        [Required]
        public string Rut { get; set; }
        [Required]
        public string Telefono { get; set; }
        [Required]
        public int TipoRolId { get; set; }
        [Required]
        public bool Activo { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(20)]
        public string Apellido1 { get; set; }
        [Required]
        [MaxLength(20)]    
        public string Apellido2 { get; set; }
        [Required]
        public string Rut { get; set; }
        [Required]
        public string Email { get; set; } 
        [Required]
        public string Telefono { get; set; }
        [Required]
        public int TipoRolId { get; set; }
        [Required]
        public bool Activo{ get; set; }
        public TipoRol TipoRol { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }
    }
}

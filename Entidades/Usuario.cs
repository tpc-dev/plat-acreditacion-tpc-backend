using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.Entidades
{
    public class Usuario
    {
        [Required]
        public int Id{ get; set; }
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
        public bool Activo{ get; set; }
        [Required]
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
        public TipoRol TipoRol { get; set; }
        public List<ContratoUsuario> ContratosUsuarios { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }
    }
}

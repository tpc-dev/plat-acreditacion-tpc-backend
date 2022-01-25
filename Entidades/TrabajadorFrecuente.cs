using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.Entidades
{
    public class TrabajadorFrecuente
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Rut { get; set; }
        [Required]
        public string Nombres { get; set; }
        [Required]
        public string ApellidoPaterno { get; set; }
        [Required]
        public string ApellidoMaterno { get; set; }
        [Required]
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

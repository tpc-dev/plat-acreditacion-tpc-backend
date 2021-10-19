using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.Entidades
{
    public class Visita
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        public string Rut { get; set; }
        [Required]
        public int UsuarioId { get; set; }
        [Required]
        public string Comentario { get; set; }
        [Required]
        public bool HaIngresado { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime FechaVisita { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}

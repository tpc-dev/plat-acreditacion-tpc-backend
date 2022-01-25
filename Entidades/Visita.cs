using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.Entidades
{
    public class Visita
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        public string Rut { get; set; }
        [Required]
        public string Comentario { get; set; }
        [Required]
        public bool HaIngresado { get; set; }
        [Required]
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        [Required]
        public int AreaId { get; set; }
        public Area Area { get; set; }
        public List<IngresoVisitas> IngresosVisitas { get; set; }
        public DateTime FechaVisita { get; set; }
        public string Hora{ get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}

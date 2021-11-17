using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.Entidades
{
    public class EstadoAcreditacion
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.Entidades
{
    public class TipoRol
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.Entidades
{
    public class Empresa
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Rut { get; set; }
        [Required]
        public string RazonSocial { get; set; }
        [Required]
        public int EstadoAcreditacionId { get; set; }
        public EstadoAcreditacion EstadoAcreditacion { get; set; }
        [Required]
        public bool Activo { get; set; }
    }
}

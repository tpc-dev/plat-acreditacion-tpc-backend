using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.Entidades
{
    public class EtapaCreacionContrato
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int Orden { get; set; }
        [Required]
        public string  Nombre { get; set; }
        public bool Activo { get; set; }
    }
}

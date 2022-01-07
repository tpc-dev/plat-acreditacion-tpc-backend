using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.Entidades
{
    public class RegistroInduccion
    {
        [Required]
        public int  Id { get; set; }
        [Required]
        public DateTime FechaRealizacion{ get; set; }
        [Required]
        public DateTime FechaVencimiento { get; set; }
        [Required]
        public string Rut { get; set; }
    }
}

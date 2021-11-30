using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.Entidades
{
    public class RegistroCovidAccesos
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public int Temperatura { get; set; }
        [Required]
        public int RegistroCovidFormularioId { get; set; }
        public RegistroCovidFormulario RegistroCovidFormulario { get; set; }
    }
}

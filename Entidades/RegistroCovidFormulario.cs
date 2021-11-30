using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PlatAcreditacionTPCBackend.Entidades
{
    public class RegistroCovidFormulario 
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido{ get; set; }
        [Required]
        public string Rut{ get; set; }
        [Required]
        public string Empresa { get; set; }
        [Required]
        public bool HaTenidoSintomas { get; set; }
        [Required]
        public string Sintomas { get; set; }
        [Required]
        public bool HaTenidoContactoEstrecho { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
    }
}

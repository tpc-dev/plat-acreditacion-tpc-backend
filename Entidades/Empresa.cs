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
        public List<EmpresaContrato> EmpresasContrato{ get; set; }  // ESTA TABLA GUARDA LOS REGISTROS CUANDO SE TIENE QUE ACREDITAR LA EMPRESA POR CADA CONTRATO QUE TENGA ESTA
        [Required]
        public bool Activo { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }
    }
}

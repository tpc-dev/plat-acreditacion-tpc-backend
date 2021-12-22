using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.DTOs
{
    public class NuevoContratoDTO
    {
        [Required]
        public string CodigoContrato { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public int AreaId { get; set; }
        [Required]
        public int EtapaCreacionContratoId { get; set; }
        [Required]
        public DateTime InicioContrato { get; set; }
        [Required]
        public DateTime TerminoContrato { get; set; }
        public bool Activo { get; set; }
        public int EmpresaId { get; set; }
    }
}

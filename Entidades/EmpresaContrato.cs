using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.Entidades
{
    public class EmpresaContrato
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
        [Required]
        public int ContratoId { get; set; }
        public Contrato Contrato { get; set; }
        public List<HistoricoAcreditacionCarpetaArranque> ListadoHistoricoAcreditacionCarpetaArranque { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
    }
}

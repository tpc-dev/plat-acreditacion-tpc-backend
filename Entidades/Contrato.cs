using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.Entidades
{
    public class Contrato
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string CodigoContrato { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public int AreaId { get; set; }
        public Area Area { get; set; }
        [Required]
        public int EtapaCreacionContratoId { get; set; }
        public EtapaCreacionContrato EtapaCreacionContrato { get; set; }
        [Required]
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
        [Required]
        public DateTime InicioContrato { get; set; }
        [Required]
        public DateTime TerminoContrato { get; set; }
        [Required]
        public DateTime InicioAcreditacion{ get; set; }
        [Required]
        public DateTime TerminoAcreditacion { get; set; }
        [Required]
        public bool Activo { get; set; }
    }
}

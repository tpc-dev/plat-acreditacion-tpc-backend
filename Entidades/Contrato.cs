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
        public int ContratoUsuarioId { get; set; }
        public ContratoUsuario ContratoUsuario { get; set; }
        [Required]
        public int AreaId { get; set; }
        public Area Area { get; set; }
        [Required]
        public int GerenciaId { get; set; }
        public Gerencia Gerencia { get; set; }
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

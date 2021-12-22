using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.DTOs
{
    public class NuevoContratoUsuarioDTO
    {
        [Required]
        public int adctpc1Id { get; set; }
        public int adctpc2Id { get; set; }
        [Required]
        public int adceeccId { get; set; }
        [Required]
        public int areaId { get; set; }
        public int area2Id { get; set; }
        [Required]
        public int gerenciaId { get; set; }
        public int gerencia2Id { get; set; }
        [Required]
        public int contratoId { get; set; }
    }
}

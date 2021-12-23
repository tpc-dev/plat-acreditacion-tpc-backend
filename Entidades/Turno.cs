using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.Entidades
{
    public class Turno
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime FechaInicio { get; set; }
        [Required]
        public DateTime FechaTermino { get; set; }
        [Required]
        public int DiasLaborales{ get; set; }
        [Required]
        public int DiasFestivos { get; set; }
        [Required]
        public int HorasSemana{ get; set; }
        public bool Activo { get; set; }
        [Required]
        public int ContratoId { get; set; }
        public Contrato Contrato { get; set; }
        [Required]
        public string Descripcion { get; set; } // ej: 7x7
        [Required]
        public int JornadaId { get; set; }
        public Jornada Jornada { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}

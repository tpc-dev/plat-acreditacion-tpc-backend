using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.Entidades
{
    public class TrabajadorTPC
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Rut { get; set; }
        [Required]
        public string Nombres { get; set; }
        [Required]
        public string ApellidoPaterno { get; set; }
        [Required]
        public string ApellidoMaterno { get; set; }
        [Required]
        public int GerenciaId { get; set; }
        public Gerencia Gerencia { get; set; }
        [Required]
        public int GeneroId { get; set; }
        public Genero Genero { get; set; }
        [Required]
        public int EstadoCivilId { get; set; }
        public EstadoCivil EstadoCivil { get; set; }
        [Required]
        public DateTime FechaNacimiento { get; set; }
        [Required]
        public int NivelEducacionalId { get; set; }
        public NivelEducacional NivelEducacional { get; set; }
        [Required]
        public int PaisId { get; set; }
        public Pais Pais { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

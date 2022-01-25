using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.Entidades
{
    public class Vehiculo
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Patente { get; set; }
        [Required]
        public string  Marca { get; set; }
        [Required]
        public string Modelo { get; set; }
        [Required]
        public int TipoVehiculoId { get; set; }
        public TipoVehiculo TipoVehiculo { get; set; }
        [Required]
        public int ChoferId { get; set; }
        public Chofer Chofer { get; set; }
        [Required]
        public int Year { get; set; }
        public bool Activo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

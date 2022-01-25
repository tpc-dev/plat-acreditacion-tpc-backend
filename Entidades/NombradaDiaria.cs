using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.Entidades
{
    public class NombradaDiaria
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public string HoraInicio { get; set; }
        [Required]
        public string HoraTermino { get; set; }
        [Required]
        public bool Activo { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
        [Required]
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}

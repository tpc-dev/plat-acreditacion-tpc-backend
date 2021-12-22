using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.Entidades
{
    public class ContratoUsuario
    {
        //[Required]
        //public int Id { get; set; }
        [Required]
        public int ContratoId { get; set; }
        public Contrato Contrato { get; set; }
        [Required]
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        // TODO AGREGAR RELACION USUARIO 
        public int GerenciaId { get; set; }
        public Gerencia Gerencia { get; set; }
        public int AreaId { get; set; }
        public Area Area { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
    }
}

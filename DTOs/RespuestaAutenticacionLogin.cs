using PlatAcreditacionTPCBackend.Entidades;

namespace PlatAcreditacionTPCBackend.DTOs
{
    public class RespuestaAutenticacionLogin
    {
        public string Token { get; set; }
        public DateTime Expiracion { get; set; }
        public Usuario Usuario { get; set; }
    }
}

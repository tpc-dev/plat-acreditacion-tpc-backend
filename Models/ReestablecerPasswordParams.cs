namespace PlatAcreditacionTPCBackend.Models
{
    public class ReestablecerPasswordParams
    {
        public string NewPassword { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
    }
}

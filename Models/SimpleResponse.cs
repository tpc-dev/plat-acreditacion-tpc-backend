using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.Models
{
    public class SimpleResponse
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Message { get; set; }
    }
}

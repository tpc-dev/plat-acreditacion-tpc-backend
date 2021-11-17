namespace PlatAcreditacionTPCBackend.Models
{
    public class SharePointAccessRequest
    {
        public string grant_type {  get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string resource { get; set; }

    }
}

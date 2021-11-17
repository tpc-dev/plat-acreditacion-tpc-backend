using PlatAcreditacionTPCBackend.Models;

namespace PlatAcreditacionTPCBackend.Servicios
{
    public interface ISharePointService
    {
        Task<SharePointToken> GetTokenAccess(SharePointAccessRequest sharePointAccessRequest);
        Task<SharePointToken> GetTokenAccessHttpResponseMessage(SharePointAccessRequest sharePointAccessRequest);
    }
}

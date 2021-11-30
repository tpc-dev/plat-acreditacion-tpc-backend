using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PlatAcreditacionTPCBackend.Models;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;

namespace PlatAcreditacionTPCBackend.Servicios
{
    public class SharePointService : ISharePointService
    {
        static HttpClient client = new HttpClient();
        SharePointToken sharePointToken = null;
        DateTime fechaAdquisicionToken = new();

        //private readonly MailSettings _mailSettings;
        public SharePointService()
        {
            //_mailSettings = mailSettings.Value;
        }

        public async Task<SharePointToken> GetTokenAccess(SharePointAccessRequest sharePointAccessRequest)
        {   
            if(sharePointToken == null || DateTime.Now>=fechaAdquisicionToken)
            {
                sharePointToken = await GetTokenAccessHttpResponseMessage(sharePointAccessRequest);
                fechaAdquisicionToken = DateTime.Now.AddSeconds(sharePointToken.expires_in);
                return sharePointToken;
            }

            return sharePointToken;
        }


        public async Task<SharePointToken> GetTokenAccessHttpResponseMessage(SharePointAccessRequest sharePointAccessRequest)
        {
            string path = "https://accounts.accesscontrol.windows.net/bb4e77c2-896b-4876-ba85-073a2bb991e6/tokens/OAuth/2/";


            HttpResponseMessage response = await client.PostAsync(path, new MultipartFormDataContent
            {
                { new StringContent("client_credentials"), "\"grant_type\""},
                { new StringContent("684b0e60-76ef-47b6-9b05-0a4fe916ad1d@bb4e77c2-896b-4876-ba85-073a2bb991e6"), "\"client_id\""},
                { new StringContent("+wNqx/w+K1gL15zlk/O5QyQ/BEbxuNDXKySIF7ki+ks="), "\"client_secret\""},
                { new StringContent("00000003-0000-0ff1-ce00-000000000000/terminalpuertocoquimbo.sharepoint.com@bb4e77c2-896b-4876-ba85-073a2bb991e6"), "\"resource\""},
            });

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6Imwzc1EtNTBjQ0g0eEJWWkxIVEd3blNSNzY4MCIsImtpZCI6Imwzc1EtNTBjQ0g0eEJWWkxIVEd3blNSNzY4MCJ9.eyJhdWQiOiIwMDAwMDAwMy0wMDAwLTBmZjEtY2UwMC0wMDAwMDAwMDAwMDAvdGVybWluYWxwdWVydG9jb3F1aW1iby5zaGFyZXBvaW50LmNvbUBiYjRlNzdjMi04OTZiLTQ4NzYtYmE4NS0wNzNhMmJiOTkxZTYiLCJpc3MiOiIwMDAwMDAwMS0wMDAwLTAwMDAtYzAwMC0wMDAwMDAwMDAwMDBAYmI0ZTc3YzItODk2Yi00ODc2LWJhODUtMDczYTJiYjk5MWU2IiwiaWF0IjoxNjM2NTU1OTkwLCJuYmYiOjE2MzY1NTU5OTAsImV4cCI6MTYzNjY0MjY5MCwiaWRlbnRpdHlwcm92aWRlciI6IjAwMDAwMDAxLTAwMDAtMDAwMC1jMDAwLTAwMDAwMDAwMDAwMEBiYjRlNzdjMi04OTZiLTQ4NzYtYmE4NS0wNzNhMmJiOTkxZTYiLCJuYW1laWQiOiI2ODRiMGU2MC03NmVmLTQ3YjYtOWIwNS0wYTRmZTkxNmFkMWRAYmI0ZTc3YzItODk2Yi00ODc2LWJhODUtMDczYTJiYjk5MWU2Iiwib2lkIjoiMjJhN2IxNzctY2Y5ZS00ZjM1LThlMzYtYTdhOWUwZTc5MmI4Iiwic3ViIjoiMjJhN2IxNzctY2Y5ZS00ZjM1LThlMzYtYTdhOWUwZTc5MmI4IiwidHJ1c3RlZGZvcmRlbGVnYXRpb24iOiJmYWxzZSJ9.BuX-U2d0X5Q5u9xKWuaRdFOxQ3_b_T0P8GTd27lk7vVD7Ims0UpJfMDycdnGA0VyqiAXPvCnPtnJhrXWv0Za4CGZ1WNDKxhfWyGrfM24Wba9uuXvXsYzVpCWiMelafHzD9xntp9F6qYlwoUSdjx_Dfio28DYTAvY3351D7agntpiJNVd5KJyFs7uhAflSRj7Fr0OzzTvXlKZphxp35fqo8gv5Wqx6kGX71k51yWCeBC7IkHzs_UCcWvQT0vNLkMuS7O47Zhpgb9mpLNyna7rfWSEDFfkHM-gi77NtPrfCg1uYG_hBDy27JLS6J5Aa-DRNHCWk-023Edd3hL-dnsbAw");

            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return await response.Content.ReadAsAsync<SharePointToken>();
        }
    }
}

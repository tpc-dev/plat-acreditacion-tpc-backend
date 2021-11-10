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

        //private readonly MailSettings _mailSettings;
        public SharePointService()
        {
            //_mailSettings = mailSettings.Value;
        }

        public Task<SharePointToken> GetTokenAccess(SharePointAccessRequest sharePointAccessRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<SharePointToken> GetTokenAccessHttpResponseMessage(SharePointAccessRequest sharePointAccessRequest)
        {
            string path = "https://accounts.accesscontrol.windows.net/bb4e77c2-896b-4876-ba85-073a2bb991e6/tokens/OAuth/2/";

            //  var content = new StringContent("grant_type:client_credentials",Encoding.UTF8,"application/json");


            var form = new JObject();
            form.Add("grant_type", "client_credentials");
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(path),
                Content = new StringContent(form.ToString(), Encoding.UTF8, "application/json"),
            };

            //System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
            //{
            //    Parameters: 
            //};

            HttpResponseMessage response = await client.PostAsync(path, new MultipartFormDataContent
            {
                { new StringContent("client_credentials"), "\"grant_type\""},
                { new StringContent("684b0e60-76ef-47b6-9b05-0a4fe916ad1d@bb4e77c2-896b-4876-ba85-073a2bb991e6"), "\"client_id\""},
                { new StringContent("+wNqx/w+K1gL15zlk/O5QyQ/BEbxuNDXKySIF7ki+ks="), "\"client_secret\""},
                { new StringContent("00000003-0000-0ff1-ce00-000000000000/terminalpuertocoquimbo.sharepoint.com@bb4e77c2-896b-4876-ba85-073a2bb991e6"), "\"resource\""},
            });

            return await response.Content.ReadAsAsync<SharePointToken>();

            //response.EnsureSuccessStatusCode();

            //SharePointToken sharePointToken = null;

            //var responseDos = await client.GetAsync(path);
            //if (responseDos.IsSuccessStatusCode)
            //{
            //    sharePointToken = await responseDos.Content.ReadAsAsync<SharePointToken>();
            //}
            //return sharePointToken;

            //var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            //if (responseBody!=null){

            //}

            //MultipartFormDataContent form = new MultipartFormDataContent();
            //HttpContent content = new StringContent("client_credentials");

            //form.Add(content, "grant_type");


            //var respuesta = await client.PostAsync(path, form);

            //if (respuesta != null)
            //{
            //    respuesta.StatusCode.ToString();
            //}


        }
    }
}

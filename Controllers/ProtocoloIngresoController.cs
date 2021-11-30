using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PlatAcreditacionTPCBackend.Entidades;
using PlatAcreditacionTPCBackend.Models;
using PlatAcreditacionTPCBackend.Servicios;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;

namespace PlatAcreditacionTPCBackend.Controllers
{

    [ApiController]
    [Route("api/protocolos-ingreso")]
    public class ProtocoloIngresoController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly ISharePointService sharePointService;

        public ProtocoloIngresoController(ApplicationDbContext context, ISharePointService sharePointService)
        {
            this.context = context;
            this.sharePointService = sharePointService;
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<ProtocoloIngreso>>> Get()
        {
            return await context.ProtocolosIngresos.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProtocoloIngreso>> Get(int id)
        {
            var protocoloIngreso = await context.ProtocolosIngresos.FirstOrDefaultAsync(x => x.Id == id);
            if (protocoloIngreso == null)
            {
                return NotFound();
            }

            return protocoloIngreso;
        }

        [HttpPost]
        public async Task<ActionResult> Post(ProtocoloIngreso protocoloIngreso)
        {
            context.Add(protocoloIngreso);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(ProtocoloIngreso protocoloIngreso, int id)
        {
            if (protocoloIngreso.Id != id)
            {
                return BadRequest("El id del protocolo no coincide con el id de la URL");
            }

            bool existe = await context.ProtocolosIngresos.AnyAsync(protocolo => protocolo.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Update(protocoloIngreso);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool existe = await context.ProtocolosIngresos.AnyAsync(protocolo => protocolo.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new ProtocoloIngreso() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }


        [HttpGet("encuesta-covid/{rut}")]
        public async Task<FormularioCovidContestado> GetEncuestaCovidPorRut(string rut)
        {
            SharePointToken sharePointToken =  await sharePointService.GetTokenAccess(null);

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sharePointToken.access_token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string path = "https://terminalpuertocoquimbo.sharepoint.com/sites/CapstonePruebas/_api/lists";
            HttpResponseMessage responseGet = await client.GetAsync(path);
            //return responseGet.Content.ReadFromJsonAsync<string>;
            //var contentStream = await responseGet.Content.ReadAsStreamAsync().Result; 
            string contentStream = await responseGet.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(contentStream);
            JToken values = json["value"];
            string guid ="" ;
            foreach (JToken e in values.ToArray<JToken>())
            {
                string titleItem = e["Title"].ToString();
                if (titleItem.Equals("CapturadorRespuestasCovid19"))
                {
                    //return e.ToString();
                    guid = e["Id"].ToString();
                }
            }
            //  return contentStream;
            //return values.ToString();

            string pathItems = "https://terminalpuertocoquimbo.sharepoint.com/sites/CapstonePruebas/_api/Web/Lists(guid'"+guid+"')/Items";
            HttpResponseMessage responseGetItems = await client.GetAsync(pathItems);
            string contentStreamItems = await responseGetItems.Content.ReadAsStringAsync();

            JObject jsonItem = JObject.Parse(contentStreamItems);
            JToken valuesItems = jsonItem["value"];

            bool existeRut = false;

            foreach (JToken e in valuesItems.ToArray<JToken>())
            {
                string rutItem = e["Rut"].ToString();
                if (rutItem.Equals(rut))
                {
                    //return e.ToString();
                    existeRut = true;
                    string fechaFormatead =  DateTime.Parse(e["Modified"].ToString()).ToString("dd/MM/yyyy");
                    //return existeRut.ToString();
                    FormularioCovidContestado formularioCovidContestado = new FormularioCovidContestado{
                        Modificado = DateTime.Parse(e["Modified"].ToString()).ToString("dd/MM/yyyy"),
                        Creado = DateTime.Parse(e["Modified"].ToString()).ToString("dd/MM/yyyy"),
                        Contestado = existeRut
                    };
                    
                    return formularioCovidContestado;
                }
            }
           

            return new FormularioCovidContestado { Contestado = false };
            //return contentStreamItems;

        }

    }
}

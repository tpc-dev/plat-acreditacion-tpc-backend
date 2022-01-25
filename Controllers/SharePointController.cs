using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatAcreditacionTPCBackend.Servicios;

namespace PlatAcreditacionTPCBackend.Controllers
{
    [ApiController]
    [Route("api/share-point")]
    public class SharePointController
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly ISharePointService sharePointService;

        public SharePointController(ApplicationDbContext context,  IMapper mapper, ISharePointService sharePointService)
        {
            this.context = context;
            this.mapper = mapper;
            this.sharePointService = sharePointService;
        }

        //[HttpGet("contrato/{contratoName}/archivo/{fileName}")]
        //public async Task<ActionResult> GetArchivoEnContrato(string contratoName, string fileName)
        //{
        //    //var data = await context.TipoRoles.Fir
        //    return Ok();
        //}
    }
}

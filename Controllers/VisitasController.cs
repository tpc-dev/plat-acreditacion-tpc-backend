using Microsoft.AspNetCore.Mvc;
using PlatAcreditacionTPCBackend.Entidades;

namespace PlatAcreditacionTPCBackend.Controllers
{
    [ApiController]
    [Route("api/visitas")]
    public class VisitasController : ControllerBase

    {

        [HttpGet]
        public ActionResult<List<Visita>> Get()
        {
            return new List<Visita>()
            {
                new Visita(){Id=1, Nombre="Tomas"},
                new Visita(){Id=2, Nombre="Felipe"}
            };
        }



    }
}

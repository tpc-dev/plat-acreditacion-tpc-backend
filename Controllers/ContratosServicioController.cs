using Microsoft.AspNetCore.Mvc;

namespace PlatAcreditacionTPCBackend.Controllers
{   
    [ApiController]
    [Route("api/contratos-servicio")]
    public class ContratosServicioController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ContratosServicioController(ApplicationDbContext applicationDbContext)
        {
            this._context = applicationDbContext;  
        }


    }
}

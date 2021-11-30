using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatAcreditacionTPCBackend.Entidades;

namespace PlatAcreditacionTPCBackend.Controllers
{
    [ApiController]
    [Route("api/registro-covid-accesos")]
    public class RegistroCovidAccesosController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public RegistroCovidAccesosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<RegistroCovidAccesos>>> Get()
        {
            return await context.RegistrosCovidAccesos.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(RegistroCovidAccesos registroCovidAccesos)
        {
            registroCovidAccesos.Fecha = DateTime.Now;
            context.Entry(registroCovidAccesos).State = EntityState.Modified;
            context.Add(registroCovidAccesos);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}

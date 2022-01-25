using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatAcreditacionTPCBackend.Entidades;

namespace PlatAcreditacionTPCBackend.Controllers
{

    [ApiController]
    [Route("api/eventos-envio")]
    public class EventoEnvioController : ControllerBase
    {
        private readonly ApplicationDbContextGenetec context;
        private readonly IMapper mapper;

        public EventoEnvioController(ApplicationDbContextGenetec context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<EventoEnvio>>> Get()
        {
            return await context.EventosEnvio.ToListAsync();
        }

        [HttpGet("trabajador/{rut}")]
        public async Task<ActionResult<List<EventoEnvio>>> GetPorRut(string rut)
        {
            DateTime ultimaSemana = DateTime.Now.AddDays(-7);
            return await context.EventosEnvio
                .Where(e => e.Rut == rut)
                .Where(e => e.FechaEvento > ultimaSemana)
                .OrderByDescending(e => e.FechaEvento)
                .Take(10)
                .ToListAsync();
        }

        //[HttpGet("activos")]
        //public  async Task<ActionResult<List<EventoEnvio>>> GetActivos()
        //{
        //    //return await context.EventosEnvio.ToListAsync();
        //    return await context.EventosEnvio.OrderByDescending(e=> e.FechaEvento).Take(10).ToListAsync();
        //}


        [HttpGet("{id:int}")]
        public async Task<ActionResult<EventoEnvio>> Get(int id)
        {
            var servicioCorreo = await context.EventosEnvio.FirstOrDefaultAsync(x => x.Id == id);
            if (servicioCorreo == null)
            {
                return NotFound();
            }

            return servicioCorreo;
        }

        [HttpPost]
        public async Task<ActionResult> Post(EventoEnvio servicioCorreo)
        {
            context.Add(servicioCorreo);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(EventoEnvio servicioCorreo, int id)
        {
            if (servicioCorreo.Id != id)
            {
                return BadRequest("El id del servicioCorreo no coincide con el id de la URL");
            }

            bool existe = await context.EventosEnvio.AnyAsync(servicioCorreo => servicioCorreo.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Update(servicioCorreo);
            await context.SaveChangesAsync();
            return Ok();
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool existe = await context.EventosEnvio.AnyAsync(servicioCorreo => servicioCorreo.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new EventoEnvio() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}

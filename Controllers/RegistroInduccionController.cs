using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatAcreditacionTPCBackend.Entidades;

namespace PlatAcreditacionTPCBackend.Controllers
{
    [ApiController]
    [Route("api/registro-induccion")]
    public class RegistroInduccionController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public RegistroInduccionController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<RegistroInduccion>>> Get()
        {
            return await context.RegistrosInduccion.ToListAsync();
        }

        [HttpGet("{rut}/last")]
        public async Task<ActionResult<RegistroInduccion>> GetUltimaInduccion(string rut)
        {
            return await context.RegistrosInduccion.Where(r => r.Rut == rut).FirstOrDefaultAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<RegistroInduccion>> Get(int id)
        {
            var registroInduccion = await context.RegistrosInduccion.FirstOrDefaultAsync(x => x.Id == id);
            if (registroInduccion == null)
            {
                return NotFound();
            }

            return registroInduccion;
        }

        [HttpPost]
        public async Task<ActionResult> Post(RegistroInduccion registroInduccion)
        {
            // VERiFICAR SI YA TIENE UNA INDUCCION COMPLETADA ACTIVA

            RegistroInduccion existeRegistro = await context.RegistrosInduccion.FirstOrDefaultAsync(x => x.Rut == registroInduccion.Rut);
            if (existeRegistro == null)
            {
                registroInduccion.FechaRealizacion = DateTime.Now;
                registroInduccion.FechaVencimiento = DateTime.Now.AddYears(2);
                context.Add(registroInduccion);
            }
            else
            {
                registroInduccion = existeRegistro;
                registroInduccion.FechaRealizacion = DateTime.Now;
                registroInduccion.FechaVencimiento = DateTime.Now.AddYears(2);
                context.Update(registroInduccion);    
            }

            await context.SaveChangesAsync();
            return Ok(registroInduccion);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(RegistroInduccion registroInduccion, int id)
        {
            if (registroInduccion.Id != id)
            {
                return BadRequest("El id del registro induccion no coincide con el id de la URL");
            }

            bool existe = await context.RegistrosInduccion.AnyAsync(v => v.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Update(registroInduccion);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool existe = await context.RegistrosInduccion.AnyAsync(v => v.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new RegistroInduccion() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}

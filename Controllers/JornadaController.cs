using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatAcreditacionTPCBackend.Entidades;

namespace PlatAcreditacionTPCBackend.Controllers
{
    [ApiController]
    [Route("api/jornadas")]
    public class JornadaController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public JornadaController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Jornada>>> Get()
        {
            return await context.Jornadas.ToListAsync();
        }

        [HttpGet("activos")]
        public async Task<ActionResult<List<Jornada>>> GetActivos()
        {
            return await context.Jornadas.Where(j=>j.Activo==true).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Jornada>> Get(int id)
        {
            var jornada = await context.Jornadas.FirstOrDefaultAsync(x => x.Id == id);
            if (jornada == null)
            {
                return NotFound();
            }

            return jornada;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Jornada jornada)
        {
            //bool existeContrato = await context.Contratos.AnyAsync(contrato => contrato.Id == jornada.ContratoId);
            //if (!existeContrato)
            //{
            //    return NotFound();
            //}
            // var nuevoTipoRolMapped = mapper.Map<TipoRol>(nuevoTipoRolDTO);
            context.Add(jornada);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Jornada jornada, int id)
        {
            if (jornada.Id != id)
            {
                return BadRequest("El id del jornada no coincide con el id de la URL");
            }

            bool existe = await context.Jornadas.AnyAsync(j => j.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Update(jornada);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool existe = await context.Jornadas.AnyAsync(jornada => jornada.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new Jornada() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}

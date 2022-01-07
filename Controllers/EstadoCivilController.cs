using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatAcreditacionTPCBackend.Entidades;

namespace PlatAcreditacionTPCBackend.Controllers
{
    [ApiController]
    [Route("api/estado-civil")]
    public class EstadoCivilController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public EstadoCivilController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<EstadoCivil>>> Get()
        {
            return await context.EstadosCivil.ToListAsync();
        }

        [HttpGet("activos")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<EstadoCivil>>> GetActivos()
        {
            return await context.EstadosCivil.Where(g => g.Activo == true).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<EstadoCivil>> Get(int id)
        {
            var estadoCivil = await context.EstadosCivil.FirstOrDefaultAsync(x => x.Id == id);
            if (estadoCivil == null)
            {
                return NotFound();
            }

            return estadoCivil;
        }

        [HttpPost]
        public async Task<ActionResult> Post(EstadoCivil estadoCivil)
        {
            context.Add(estadoCivil);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(EstadoCivil estadoCivil, int id)
        {
            if (estadoCivil.Id != id)
            {
                return BadRequest("El id del estado civil no coincide con el id de la URL");
            }

            bool existe = await context.EstadosCivil.AnyAsync(estadoCivil => estadoCivil.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Update(estadoCivil);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool existe = await context.EstadosCivil.AnyAsync(estadoCivil => estadoCivil.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new EstadoCivil() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}

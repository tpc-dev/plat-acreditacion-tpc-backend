using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatAcreditacionTPCBackend.Entidades;

namespace PlatAcreditacionTPCBackend.Controllers
{
    [ApiController]
    [Route("api/nivel-educacional")]
    public class NivelEducacionalController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public NivelEducacionalController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<NivelEducacional>>> Get()
        {
            return await context.NivelesEducacional.ToListAsync();
        }

        [HttpGet("activos")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<NivelEducacional>>> GetActivos()
        {
            return await context.NivelesEducacional.Where(n => n.Activo == true).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<NivelEducacional>> Get(int id)
        {
            var nivelEducacional = await context.NivelesEducacional.FirstOrDefaultAsync(x => x.Id == id);
            if (nivelEducacional == null)
            {
                return NotFound();
            }

            return nivelEducacional;
        }

        [HttpPost]
        public async Task<ActionResult> Post(NivelEducacional nivelEducacional)
        {
            context.Add(nivelEducacional);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(NivelEducacional nivelEducacional, int id)
        {
            if (nivelEducacional.Id != id)
            {
                return BadRequest("El id del nivelEducacional no coincide con el id de la URL");
            }

            bool existe = await context.NivelesEducacional.AnyAsync(nivelEducacional => nivelEducacional.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Update(nivelEducacional);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool existe = await context.NivelesEducacional.AnyAsync(nivelEducacional => nivelEducacional.Id == id);
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

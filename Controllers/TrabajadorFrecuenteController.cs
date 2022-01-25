using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatAcreditacionTPCBackend.DTOs;
using PlatAcreditacionTPCBackend.Entidades;

namespace PlatAcreditacionTPCBackend.Controllers
{
    [ApiController]
    [Route("api/trabajadores-frecuentes")]
    public class TrabajadorFrecuenteController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public TrabajadorFrecuenteController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<TrabajadorFrecuente>>> Get()
        {
            return await context.TrabajadoresFrecuente
                .ToListAsync();
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<TrabajadorFrecuente>> Get(int id)
        {
            var trabajador = await context.TrabajadoresFrecuente.FirstOrDefaultAsync(x => x.Id == id);
            if (trabajador == null)
            {
                return NotFound();
            }

            return trabajador;
        }

        [HttpPost]
        public async Task<ActionResult> Post(TrabajadorFrecuente trabajadorFrecuente)
        {

            bool existeTrabajadorRut = await context.TrabajadoresFrecuente.AnyAsync(g => g.Rut == trabajadorFrecuente.Rut);

            if (existeTrabajadorRut)
            {
                return NotFound("Ya hay un trabajador con este RUT");
            }

            trabajadorFrecuente.CreatedAt = DateTime.Now;
            trabajadorFrecuente.UpdatedAt = DateTime.Now;
            
            context.Add(trabajadorFrecuente);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(TrabajadorFrecuente trabajadorFrecuente, int id)
        {
            if (trabajadorFrecuente.Id != id)
            {
                return BadRequest("El id del trabajador no coincide con el id de la URL");
            }

            bool existe = await context.TrabajadoresFrecuente.AnyAsync(pais => pais.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Update(trabajadorFrecuente);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool existe = await context.TrabajadoresFrecuente.AnyAsync(t => t.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new TrabajadorFrecuente() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}

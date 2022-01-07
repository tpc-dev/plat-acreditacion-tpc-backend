using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatAcreditacionTPCBackend.Entidades;

namespace PlatAcreditacionTPCBackend.Controllers
{
    [ApiController]
    [Route("api/trabajadores")]
    public class TrabajadorController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public TrabajadorController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<Trabajador>>> Get()
        {
            return await context.Trabajadores.ToListAsync();
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<Trabajador>> Get(int id)
        {
            var trabajador = await context.Trabajadores.FirstOrDefaultAsync(x => x.Id == id);
            if (trabajador == null)
            {
                return NotFound();
            }

            return trabajador;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Trabajador trabajador)
        {
            context.Add(trabajador);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Trabajador trabajador, int id)
        {
            if (trabajador.Id != id)
            {
                return BadRequest("El id del trabajador no coincide con el id de la URL");
            }

            bool existe = await context.Trabajadores.AnyAsync(pais => pais.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Update(trabajador);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool existe = await context.Trabajadores.AnyAsync(t => t.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new Trabajador() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}

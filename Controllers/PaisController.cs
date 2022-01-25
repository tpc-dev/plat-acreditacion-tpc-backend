using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatAcreditacionTPCBackend.Entidades;

namespace PlatAcreditacionTPCBackend.Controllers
{
    [ApiController]
    [Route("api/paises")]
    public class PaisController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public PaisController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<Pais>>> Get()
        {
            return await context.Paises.ToListAsync();
        }

        [HttpGet("activos")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<Pais>>> GetActivos()
        {
            return await context.Paises.Where(n => n.Activo == true).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Pais>> Get(int id)
        {
            var pais = await context.Paises.FirstOrDefaultAsync(x => x.Id == id);
            if (pais == null)
            {
                return NotFound();
            }

            return pais;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Pais pais)
        {
            bool existePais = await context.Paises.AnyAsync(p => p.Nombre.ToUpper() == pais.Nombre.ToUpper().Trim());

            if (existePais)
            {
                return BadRequest("Ya existe un pais con este nombre");
            }

            context.Add(pais);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Pais pais, int id)
        {
            if (pais.Id != id)
            {
                return BadRequest("El id del pais no coincide con el id de la URL");
            }

            bool existe = await context.Paises.AnyAsync(pais => pais.Id == id);
            if (!existe)
            {
                return NotFound();
            }


            bool existePais = await context.Paises.AnyAsync(p => p.Nombre.ToUpper() == pais.Nombre.ToUpper().Trim());

            if (existePais)
            {
                return BadRequest("Ya existe un pais con este nombre");
            }

            context.Update(pais);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}/estado")]
        public async Task<ActionResult> Put(int id)
        {

            bool existe = await context.Paises.AnyAsync(pais => pais.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            Pais pais = await context.Paises.FirstOrDefaultAsync(p => p.Id == id);

            if (pais == null)
            {
                return BadRequest("No existe un pais con este Id");
            }

            pais.Activo = !pais.Activo;
            context.Entry(pais).State = EntityState.Modified;
            context.Update(pais);
            await context.SaveChangesAsync();
            return Ok();
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool existe = await context.Paises.AnyAsync(pais => pais.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new Pais() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatAcreditacionTPCBackend.Entidades;

namespace PlatAcreditacionTPCBackend.Controllers
{
    [ApiController]
    [Route("api/choferes")]
    public class ChoferController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ChoferController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<Chofer>>> Get()
        {
            return await context.Choferes.ToListAsync();
        }

        //[HttpGet("activos")]
        //public async Task<ActionResult<List<Vehiculo>>> GetActivos()
        //{
        //    return await context.Choferes.Where(c => c.Activo == true).ToListAsync();
        //}

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Chofer>> Get(int id)
        {
            var chofer = await context.Choferes.FirstOrDefaultAsync(x => x.Id == id);
            if (chofer == null)
            {
                return NotFound();
            }

            return chofer;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Chofer chofer)
        {
            context.Add(chofer);
            await context.SaveChangesAsync();
            return Ok(chofer);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Chofer chofer, int id)
        {
            if (chofer.Id != id)
            {
                return BadRequest("El id del Choder no coincide con el id de la URL");
            }

            bool existe = await context.Choferes.AnyAsync(v => v.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Update(chofer);
            await context.SaveChangesAsync();
            return Ok(chofer);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool existe = await context.Choferes.AnyAsync(v => v.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new Chofer() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}

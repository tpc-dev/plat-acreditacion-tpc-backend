using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatAcreditacionTPCBackend.Entidades;

namespace PlatAcreditacionTPCBackend.Controllers
{

    [ApiController]
    [Route("api/tipo-vehiculos")]
    public class TipoVehiculoController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public TipoVehiculoController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<TipoVehiculo>>> Get()
        {
            return await context.TipoVehiculos.ToListAsync();
        }

        [HttpGet("activos")]
        public async Task<ActionResult<List<TipoVehiculo>>> GetActivos()
        {
            return await context.TipoVehiculos.Where(c => c.Activo == true).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TipoVehiculo>> Get(int id)
        {
            var tipoVehiculo = await context.TipoVehiculos.FirstOrDefaultAsync(x => x.Id == id);
            if (tipoVehiculo == null)
            {
                return NotFound();
            }

            return tipoVehiculo;
        }

        [HttpPost]
        public async Task<ActionResult> Post(TipoVehiculo tipoVehiculo)
        {
            context.Add(tipoVehiculo);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(TipoVehiculo tipoVehiculo, int id)
        {
            if (tipoVehiculo.Id != id)
            {
                return BadRequest("El id del tipoVehiculo no coincide con el id de la URL");
            }

            bool existe = await context.TipoVehiculos.AnyAsync(tipoVehiculo => tipoVehiculo.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Update(tipoVehiculo);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool existe = await context.TipoVehiculos.AnyAsync(tipoVehiculo => tipoVehiculo.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new TipoVehiculo() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatAcreditacionTPCBackend.Entidades;

namespace PlatAcreditacionTPCBackend.Controllers
{
    [ApiController]
    [Route("api/vehiculos")]
    public class VehiculoController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public VehiculoController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<Vehiculo>>> Get()
        {
            return await context.Vehiculos.ToListAsync();
        }

        [HttpGet("activos")]
        public async Task<ActionResult<List<Vehiculo>>> GetActivos()
        {
            return await context.Vehiculos.Where(c => c.Activo == true).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Vehiculo>> Get(int id)
        {
            var vehiculo = await context.Vehiculos.FirstOrDefaultAsync(x => x.Id == id);
            if (vehiculo == null)
            {
                return NotFound();
            }

            return vehiculo;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Vehiculo vehiculo)
        {
            bool existeVehiculo = await context.Vehiculos.AnyAsync(v => v.Id == vehiculo.Id);
            if (!existeVehiculo)
            {
                return NotFound();
            }
            // var nuevoTipoRolMapped = mapper.Map<TipoRol>(nuevoTipoRolDTO);
            context.Add(vehiculo);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Vehiculo vehiculo, int id)
        {
            if (vehiculo.Id != id)
            {
                return BadRequest("El id del Vehiculo no coincide con el id de la URL");
            }

            bool existe = await context.Vehiculos.AnyAsync(v => v.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Update(vehiculo);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool existe = await context.Vehiculos.AnyAsync(v => v.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new Vehiculo() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("registro-acceso")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<RegistroAccesoVehiculoContrato>>> GetRegistroAccesos(ContratoVehiculo contratoVehiculo)
        {
            return await context.RegistroAccesosVehiculosContrato
                .Where(r => r.ContratoVehiculoContratoId == contratoVehiculo.ContratoId && r.ContratoVehiculoVehiculoId == contratoVehiculo.VehiculoId)
                .OrderByDescending(r => r.FechaEvento)
                .Take(5)
                .ToListAsync();
        }

        [HttpPost("registro-acceso/{tipo}")]
        public async Task<ActionResult> PostRegistroAcceso(ContratoVehiculo contratoVehiculo, string tipo)
        {

            RegistroAccesoVehiculoContrato registroAccesoVehiculoContrato = new RegistroAccesoVehiculoContrato
            {
                TipoEvento = tipo,
                FechaEvento = DateTime.Now,
                ContratoVehiculoContratoId = contratoVehiculo.ContratoId,
                ContratoVehiculoVehiculoId = contratoVehiculo.VehiculoId
            };

            context.Add(registroAccesoVehiculoContrato);
            await context.SaveChangesAsync();
            return Ok(registroAccesoVehiculoContrato);
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatAcreditacionTPCBackend.Entidades;

namespace PlatAcreditacionTPCBackend.Controllers
{
    [ApiController]
    [Route("api/cargos")]
    public class CargoController:ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CargoController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<Cargo>>> Get()
        {
            return await context.Cargos.ToListAsync();
        }

        [HttpGet("activos")]
        public async Task<ActionResult<List<Cargo>>> GetActivos()
        {
            return await context.Cargos.Where(c => c.Activo == true).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Cargo>> Get(int id)
        {
            var cargo = await context.Cargos.FirstOrDefaultAsync(x => x.Id == id);
            if (cargo == null)
            {
                return NotFound();
            }

            return cargo;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Cargo cargo)
        {   
            bool existeContrato = await context.Contratos.AnyAsync(contrato=>contrato.Id==cargo.ContratoId);
            if (!existeContrato)
            {
                return NotFound();
            }
           // var nuevoTipoRolMapped = mapper.Map<TipoRol>(nuevoTipoRolDTO);
            context.Add(cargo);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Cargo cargo, int id)
        {
            if (cargo.Id != id)
            {
                return BadRequest("El id del cargo no coincide con el id de la URL");
            }

            bool existe = await context.Cargos.AnyAsync(cargo => cargo.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Update(cargo);
            await context.SaveChangesAsync();
            return Ok();
        }


        //[HttpPut("{id:int}/estado/{estado:bool}")]
        //public async Task<ActionResult> ActulizarEstado(Cargo cargo, int id)
        //{
        //    if (cargo.Id != id)
        //    {
        //        return BadRequest("El id del cargo no coincide con el id de la URL");
        //    }

        //    bool existe = await context.Cargos.AnyAsync(tipoRol => tipoRol.Id == id);
        //    if (!existe)
        //    {
        //        return NotFound();
        //    }


        //    context.Update(cargo);
        //    await context.SaveChangesAsync();
        //    return Ok();
        //}

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool existe = await context.Cargos.AnyAsync(cargo => cargo.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new Cargo() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}

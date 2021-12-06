using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatAcreditacionTPCBackend.DTOs;
using PlatAcreditacionTPCBackend.Entidades;

namespace PlatAcreditacionTPCBackend.Controllers
{

    [ApiController]
    [Route("api/tipo-roles")]
    public class TipoRolesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public TipoRolesController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<TipoRol>>> Get()
        {
            return await context.TipoRoles.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TipoRol>> Get(int id)
        {
            var tipoRol = await context.TipoRoles.FirstOrDefaultAsync(x => x.Id == id);
            if (tipoRol == null)
            {
                return NotFound();
            }

            return tipoRol;
        }

        [HttpPost]
        public async Task<ActionResult> Post(NuevoTipoRolDTO nuevoTipoRolDTO)
        {
            var nuevoTipoRolMapped = mapper.Map<TipoRol>(nuevoTipoRolDTO);
            context.Add(nuevoTipoRolMapped);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(TipoRol tipoRol, int id)
        {
            if (tipoRol.Id != id)
            {
                return BadRequest("El id del tipo de rol no coincide con el id de la URL");
            }

            bool existe = await context.TipoRoles.AnyAsync(tipoRol => tipoRol.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Update(tipoRol);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool existe = await context.TipoRoles.AnyAsync(tipoRol => tipoRol.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new TipoRol() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}

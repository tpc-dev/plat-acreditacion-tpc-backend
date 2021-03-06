using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatAcreditacionTPCBackend.Entidades;

namespace PlatAcreditacionTPCBackend.Controllers
{
    [ApiController]
    [Route("api/empresas")]
    public class EmpresasController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public EmpresasController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<Empresa>>> Get()
        {
            return await context.Empresas.ToListAsync();
        }

        [HttpGet("en-acreditacion")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<Empresa>>> GetEmpresaEnAcreditacion(int id)
        {
           // id = 2 estado aceptado de creditacion
            return await context.Empresas.Include(x => x.EstadoAcreditacion).Where(x => x.EstadoAcreditacionId != 2 && x.Activo == true).ToListAsync();
        }


        [HttpGet("acreditadas")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<Empresa>>> GetEmpresaAcreditadas(int id)
        {
            // id = 2 estado aceptado de creditacion
            return await context.Empresas.Include(x => x.EstadoAcreditacion).Where(x => x.EstadoAcreditacionId == 2 && x.Activo == true).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Empresa empresa)
        {
            context.Add(empresa);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Empresa empresa, int id)
        {
            if (empresa.Id != id)
            {
                return BadRequest("El id del empresa no coincide con el id de la URL");
            }

            bool existe = await context.Empresas.AnyAsync(empresaS => empresaS.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Update(empresa);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool existe = await context.Empresas.AnyAsync(empresa => empresa.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new Empresa() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
